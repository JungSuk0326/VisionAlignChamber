using System;
using System.Net;
using CommObject;
using ObjectCommunication.Models;

namespace VisionAlignChamber.Communication.Interfaces
{
    /// <summary>
    /// CTC(EFEM) 통신 인터페이스
    /// </summary>
    public interface ICTCCommunication : IDisposable
    {
        /// <summary>
        /// 서버 리스닝 여부
        /// </summary>
        bool IsListening { get; }

        /// <summary>
        /// 연결된 클라이언트 수
        /// </summary>
        int ClientCount { get; }

        /// <summary>
        /// 현재 상태
        /// </summary>
        StatusObject CurrentStatus { get; set; }

        /// <summary>
        /// 서버 시작
        /// </summary>
        void Start();

        /// <summary>
        /// 서버 정지
        /// </summary>
        void Stop();

        /// <summary>
        /// 응답 전송
        /// </summary>
        void SendResponse(CommandObject.eCMD command, bool isOk, string reason = "");

        /// <summary>
        /// 상태 전송
        /// </summary>
        void SendStatus();

        /// <summary>
        /// 상태 전송 (객체 지정)
        /// </summary>
        void SendStatus(StatusObject status);

        /// <summary>
        /// 결과 전송
        /// </summary>
        void SendResult(ResultObject result);

        /// <summary>
        /// 알람 전송
        /// </summary>
        void SendAlarm(int alarmId, string alarmName, string description);

        #region Events

        /// <summary>
        /// 연결 상태 변경 이벤트
        /// </summary>
        event Action<IPEndPoint, bool> OnConnectionChanged;

        /// <summary>
        /// 명령 수신 이벤트
        /// </summary>
        event Action<CommandObject> OnCommandReceived;

        /// <summary>
        /// 객체 수신 이벤트 (통합)
        /// </summary>
        event Action<CommObjectBase> OnObjectReceived;

        #endregion
    }
}
