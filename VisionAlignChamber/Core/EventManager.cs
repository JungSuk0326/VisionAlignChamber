using System;
using System.Collections.Generic;

namespace VisionAlignChamber.Core
{
    /// <summary>
    /// UI 알림용 전역 이벤트 관리자 (싱글톤)
    /// </summary>
    /// <remarks>
    /// 용도:
    /// - 상태 변경 → UI 갱신 알림
    /// - 알람 발생/해제 알림
    /// - 상태 메시지 표시
    ///
    /// 모듈 간 직접 통신은 C# 이벤트를 사용합니다.
    /// (예: VisionAlignerSequence.StepChanged 이벤트)
    /// </remarks>
    public static class EventManager
    {
        #region Fields

        private static readonly object _lock = new object();
        private static readonly Dictionary<string, List<Action<object>>> _subscribers = new Dictionary<string, List<Action<object>>>();

        #endregion

        #region Event Names

        /// <summary>시스템 상태 변경 (UI 갱신용)</summary>
        public const string SystemStateChanged = "SystemStateChanged";

        /// <summary>알람 발생</summary>
        public const string AlarmOccurred = "AlarmOccurred";

        /// <summary>알람 해제</summary>
        public const string AlarmCleared = "AlarmCleared";

        /// <summary>제어 권한 변경</summary>
        public const string ControlAuthorityChanged = "ControlAuthorityChanged";

        /// <summary>상태 메시지 (상태바용)</summary>
        public const string StatusMessage = "StatusMessage";

        #endregion

        #region Public Methods

        /// <summary>
        /// 이벤트 발행
        /// </summary>
        public static void Publish(string eventName, object data = null)
        {
            if (string.IsNullOrEmpty(eventName))
                return;

            List<Action<object>> handlers = null;

            lock (_lock)
            {
                if (_subscribers.TryGetValue(eventName, out var list))
                {
                    handlers = new List<Action<object>>(list);
                }
            }

            if (handlers != null)
            {
                foreach (var handler in handlers)
                {
                    try
                    {
                        handler?.Invoke(data);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[EventManager] Error in handler for '{eventName}': {ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 이벤트 구독
        /// </summary>
        public static void Subscribe(string eventName, Action<object> handler)
        {
            if (string.IsNullOrEmpty(eventName) || handler == null)
                return;

            lock (_lock)
            {
                if (!_subscribers.TryGetValue(eventName, out var list))
                {
                    list = new List<Action<object>>();
                    _subscribers[eventName] = list;
                }

                if (!list.Contains(handler))
                {
                    list.Add(handler);
                }
            }
        }

        /// <summary>
        /// 이벤트 구독 해제
        /// </summary>
        public static void Unsubscribe(string eventName, Action<object> handler)
        {
            if (string.IsNullOrEmpty(eventName) || handler == null)
                return;

            lock (_lock)
            {
                if (_subscribers.TryGetValue(eventName, out var list))
                {
                    list.Remove(handler);

                    if (list.Count == 0)
                    {
                        _subscribers.Remove(eventName);
                    }
                }
            }
        }

        /// <summary>
        /// 모든 이벤트 구독 해제 (앱 종료 시)
        /// </summary>
        public static void Clear()
        {
            lock (_lock)
            {
                _subscribers.Clear();
            }
        }

        #endregion
    }
}
