using System;
using System.Collections.Generic;

namespace VisionAlignChamber.Core
{
    /// <summary>
    /// 전역 이벤트 관리자 (싱글톤 패턴)
    /// 애플리케이션 전역에서 이벤트 발행/구독을 관리합니다.
    /// </summary>
    /// <remarks>
    /// 사용 예시:
    /// - 발행: EventManager.Publish("ImageGrabbed", bitmap);
    /// - 구독: EventManager.Subscribe("ImageGrabbed", data => ProcessImage((Bitmap)data));
    /// - 해제: EventManager.Unsubscribe("ImageGrabbed", handler);
    /// </remarks>
    public static class EventManager
    {
        #region Fields

        private static readonly object _lock = new object();
        private static readonly Dictionary<string, List<Action<object>>> _subscribers = new Dictionary<string, List<Action<object>>>();

        #endregion

        #region Event Names (상수 정의)

        /// <summary>이미지 획득 완료</summary>
        public const string ImageGrabbed = "ImageGrabbed";

        /// <summary>검사 완료</summary>
        public const string InspectionComplete = "InspectionComplete";

        /// <summary>시스템 상태 변경</summary>
        public const string SystemStateChanged = "SystemStateChanged";

        /// <summary>알람 발생</summary>
        public const string AlarmOccurred = "AlarmOccurred";

        /// <summary>알람 해제</summary>
        public const string AlarmCleared = "AlarmCleared";

        /// <summary>모션 완료</summary>
        public const string MotionComplete = "MotionComplete";

        /// <summary>Control Authority 변경</summary>
        public const string ControlAuthorityChanged = "ControlAuthorityChanged";

        /// <summary>로그 메시지</summary>
        public const string LogMessage = "LogMessage";

        /// <summary>상태 메시지 업데이트</summary>
        public const string StatusMessage = "StatusMessage";

        #endregion

        #region Public Methods

        /// <summary>
        /// 이벤트 발행
        /// </summary>
        /// <param name="eventName">이벤트 이름</param>
        /// <param name="data">전달할 데이터 (optional)</param>
        public static void Publish(string eventName, object data = null)
        {
            if (string.IsNullOrEmpty(eventName))
                return;

            List<Action<object>> handlers = null;

            lock (_lock)
            {
                if (_subscribers.TryGetValue(eventName, out var list))
                {
                    // 복사본을 만들어 순회 중 수정 방지
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
        /// <param name="eventName">이벤트 이름</param>
        /// <param name="handler">이벤트 핸들러</param>
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
        /// <param name="eventName">이벤트 이름</param>
        /// <param name="handler">이벤트 핸들러</param>
        public static void Unsubscribe(string eventName, Action<object> handler)
        {
            if (string.IsNullOrEmpty(eventName) || handler == null)
                return;

            lock (_lock)
            {
                if (_subscribers.TryGetValue(eventName, out var list))
                {
                    list.Remove(handler);

                    // 빈 리스트 정리
                    if (list.Count == 0)
                    {
                        _subscribers.Remove(eventName);
                    }
                }
            }
        }

        /// <summary>
        /// 특정 이벤트의 모든 구독 해제
        /// </summary>
        /// <param name="eventName">이벤트 이름</param>
        public static void UnsubscribeAll(string eventName)
        {
            if (string.IsNullOrEmpty(eventName))
                return;

            lock (_lock)
            {
                _subscribers.Remove(eventName);
            }
        }

        /// <summary>
        /// 모든 이벤트 구독 해제 (앱 종료 시 호출)
        /// </summary>
        public static void Clear()
        {
            lock (_lock)
            {
                _subscribers.Clear();
            }
        }

        /// <summary>
        /// 특정 이벤트의 구독자 수 반환 (디버깅용)
        /// </summary>
        public static int GetSubscriberCount(string eventName)
        {
            lock (_lock)
            {
                if (_subscribers.TryGetValue(eventName, out var list))
                {
                    return list.Count;
                }
                return 0;
            }
        }

        #endregion
    }
}
