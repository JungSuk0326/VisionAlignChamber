using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VisionAlignChamber.Interlock
{
    /// <summary>
    /// 인터락 조건 감시 항목
    /// </summary>
    public class MonitorItem
    {
        /// <summary>
        /// 관련 인터락 ID
        /// </summary>
        public int InterlockId { get; set; }

        /// <summary>
        /// 조건 체크 함수 (true면 알람 발생)
        /// </summary>
        public Func<bool> ConditionCheck { get; set; }

        /// <summary>
        /// 감시 활성화 여부
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 마지막 체크 결과
        /// </summary>
        public bool LastCheckResult { get; set; }

        /// <summary>
        /// 추가 메시지 생성 함수 (선택)
        /// </summary>
        public Func<string> GetAdditionalMessage { get; set; }
    }

    /// <summary>
    /// 인터락 모니터 (백그라운드 조건 감시)
    /// 주기적으로 조건을 체크하여 자동으로 알람을 발생/해제합니다.
    /// </summary>
    public class InterlockMonitor : IDisposable
    {
        #region Fields

        private readonly object _lock = new object();
        private readonly List<MonitorItem> _monitorItems = new List<MonitorItem>();
        private CancellationTokenSource _cts;
        private Task _monitorTask;
        private bool _isRunning;
        private int _monitorIntervalMs = 100; // 기본 100ms

        #endregion

        #region Properties

        /// <summary>
        /// 감시 실행 중 여부
        /// </summary>
        public bool IsRunning => _isRunning;

        /// <summary>
        /// 감시 주기 (밀리초)
        /// </summary>
        public int MonitorIntervalMs
        {
            get => _monitorIntervalMs;
            set => _monitorIntervalMs = Math.Max(10, value); // 최소 10ms
        }

        /// <summary>
        /// 등록된 감시 항목 수
        /// </summary>
        public int MonitorItemCount
        {
            get
            {
                lock (_lock)
                {
                    return _monitorItems.Count;
                }
            }
        }

        #endregion

        #region Monitor Item Management

        /// <summary>
        /// 감시 항목 등록
        /// </summary>
        /// <param name="interlockId">인터락 ID</param>
        /// <param name="conditionCheck">조건 체크 함수 (true면 알람 발생)</param>
        /// <param name="getAdditionalMessage">추가 메시지 생성 함수 (선택)</param>
        public void RegisterMonitorItem(int interlockId, Func<bool> conditionCheck, Func<string> getAdditionalMessage = null)
        {
            if (conditionCheck == null)
                return;

            lock (_lock)
            {
                // 기존 항목 제거 후 추가
                _monitorItems.RemoveAll(m => m.InterlockId == interlockId);

                _monitorItems.Add(new MonitorItem
                {
                    InterlockId = interlockId,
                    ConditionCheck = conditionCheck,
                    GetAdditionalMessage = getAdditionalMessage,
                    IsEnabled = true
                });
            }
        }

        /// <summary>
        /// 감시 항목 제거
        /// </summary>
        public void UnregisterMonitorItem(int interlockId)
        {
            lock (_lock)
            {
                _monitorItems.RemoveAll(m => m.InterlockId == interlockId);
            }
        }

        /// <summary>
        /// 감시 항목 활성화/비활성화
        /// </summary>
        public void SetMonitorItemEnabled(int interlockId, bool enabled)
        {
            lock (_lock)
            {
                var item = _monitorItems.Find(m => m.InterlockId == interlockId);
                if (item != null)
                {
                    item.IsEnabled = enabled;
                }
            }
        }

        /// <summary>
        /// 모든 감시 항목 제거
        /// </summary>
        public void ClearMonitorItems()
        {
            lock (_lock)
            {
                _monitorItems.Clear();
            }
        }

        #endregion

        #region Monitor Control

        /// <summary>
        /// 감시 시작
        /// </summary>
        public void Start()
        {
            if (_isRunning)
                return;

            _cts = new CancellationTokenSource();
            _isRunning = true;

            _monitorTask = Task.Run(() => MonitorLoop(_cts.Token));

            System.Diagnostics.Debug.WriteLine("[InterlockMonitor] Started");
        }

        /// <summary>
        /// 감시 중지
        /// </summary>
        public void Stop()
        {
            if (!_isRunning)
                return;

            _cts?.Cancel();

            try
            {
                _monitorTask?.Wait(1000); // 최대 1초 대기
            }
            catch (AggregateException)
            {
                // Task 취소 예외 무시
            }

            _isRunning = false;
            _cts?.Dispose();
            _cts = null;

            System.Diagnostics.Debug.WriteLine("[InterlockMonitor] Stopped");
        }

        /// <summary>
        /// 단일 체크 수행 (수동 호출용)
        /// </summary>
        public void CheckOnce()
        {
            PerformCheck();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 감시 루프
        /// </summary>
        private async void MonitorLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    PerformCheck();
                    await Task.Delay(_monitorIntervalMs, token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[InterlockMonitor] Error in monitor loop: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 조건 체크 수행
        /// </summary>
        private void PerformCheck()
        {
            List<MonitorItem> itemsToCheck;

            lock (_lock)
            {
                itemsToCheck = new List<MonitorItem>(_monitorItems);
            }

            foreach (var item in itemsToCheck)
            {
                if (!item.IsEnabled)
                    continue;

                try
                {
                    bool currentResult = item.ConditionCheck();

                    // 상태 변화 감지
                    if (currentResult != item.LastCheckResult)
                    {
                        item.LastCheckResult = currentResult;

                        if (currentResult)
                        {
                            // 조건 충족 -> 알람 발생
                            string additionalMsg = item.GetAdditionalMessage?.Invoke();
                            InterlockManager.Instance.RaiseAlarm(item.InterlockId, "Monitor", additionalMsg);
                        }
                        else
                        {
                            // 조건 해제 -> 알람 해제
                            InterlockManager.Instance.ClearAlarm(item.InterlockId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[InterlockMonitor] Error checking interlock {item.InterlockId}: {ex.Message}");
                }
            }
        }

        #endregion

        #region IDisposable

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Stop();
                ClearMonitorItems();
            }

            _disposed = true;
        }

        ~InterlockMonitor()
        {
            Dispose(false);
        }

        #endregion
    }
}
