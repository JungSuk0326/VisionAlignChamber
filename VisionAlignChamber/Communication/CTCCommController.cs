using System;
using System.Net;
using CommObject;
using ObjectCommunication.Models;
using VisionAlignChamber.Communication.Interfaces;
using VisionAlignChamber.Log;

namespace VisionAlignChamber.Communication
{
    /// <summary>
    /// CTC(EFEM) 통신 컨트롤러
    /// - VisionAlignChamber가 서버 역할
    /// - CTC(EFEM)로부터 연결 수락
    /// - CTC로부터 명령 수신 (준비 확인, 측정 시작 등)
    /// - CTC에게 응답/상태/결과/알람 전송
    /// </summary>
    public class CTCCommController : ICTCCommunication
    {
        #region Fields

        private readonly CTCCommServer _server;
        private readonly int _port;
        private bool _disposed;

        #endregion

        #region Events

        /// <summary>
        /// 연결 상태 변경 이벤트
        /// </summary>
        public event Action<IPEndPoint, bool> OnConnectionChanged;

        /// <summary>
        /// 통합 이벤트 - 모든 객체 수신
        /// </summary>
        public event Action<CommObjectBase> OnObjectReceived;

        /// <summary>
        /// 명령 수신 이벤트
        /// </summary>
        public event Action<CommandObject> OnCommandReceived;

        /// <summary>
        /// 레시피 데이터 수신 이벤트
        /// </summary>
        public event Action<RecipeDataObject> OnRecipeDataReceived;

        /// <summary>
        /// IO 데이터 수신 이벤트
        /// </summary>
        public event Action<IoObject> OnIoReceived;

        /// <summary>
        /// 트레이스 데이터 수신 이벤트
        /// </summary>
        public event Action<TraceDataObject> OnTraceDataReceived;

        #endregion

        #region Properties

        /// <summary>
        /// 현재 상태
        /// </summary>
        public StatusObject CurrentStatus { get; set; }

        /// <summary>
        /// 서버 리스닝 여부
        /// </summary>
        public bool IsListening => _server?.IsListening ?? false;

        /// <summary>
        /// 연결된 클라이언트 수
        /// </summary>
        public int ClientCount => _server?.ClientCount ?? 0;

        #endregion

        #region Constructor

        public CTCCommController(int port)
        {
            _port = port;
            _server = new CTCCommServer(port);
            CurrentStatus = new StatusObject();

            EventLink();
        }

        #endregion

        #region Public Methods - Lifecycle

        /// <summary>
        /// 서버 시작
        /// </summary>
        public void Start()
        {
            _server.Start();
            LogManager.System.Info($"[CTCComm] Server Started on port {_port} - Waiting for CTC connection...");
        }

        /// <summary>
        /// 서버 정지
        /// </summary>
        public void Stop()
        {
            _server.Stop();
            LogManager.System.Info("[CTCComm] Server Stopped");
        }

        #endregion

        #region Public Methods - Send

        /// <summary>
        /// 응답 전송
        /// </summary>
        public void SendResponse(CommandObject.eCMD command, bool isOk, string reason = "")
        {
            var response = new ResponseObject
            {
                ReturnCommand = command.ToString(),
                Ack = isOk ? ResponseObject.eAck.OK : ResponseObject.eAck.NotOk,
                Reason = reason
            };
            _server.Send(response);
            LogManager.System.Info($"[CTCComm] Sent Response: {command} - {(isOk ? "OK" : $"NOT OK ({reason})")}");
        }

        /// <summary>
        /// 현재 상태 전송
        /// </summary>
        public void SendStatus()
        {
            _server.Send(CurrentStatus.Clone());
            LogManager.System.Debug($"[CTCComm] Sent Status: Equip={CurrentStatus.EquipmentStatus}, Process={CurrentStatus.ProcessState}, Run={CurrentStatus.RunState}");
        }

        /// <summary>
        /// 상태 전송 (객체 지정)
        /// </summary>
        public void SendStatus(StatusObject status)
        {
            CurrentStatus = status;
            _server.Send(status);
            LogManager.System.Debug($"[CTCComm] Sent Status: Equip={status.EquipmentStatus}, Process={status.ProcessState}, Run={status.RunState}");
        }

        /// <summary>
        /// 결과 전송
        /// </summary>
        public void SendResult(ResultObject result)
        {
            _server.Send(result);
            LogManager.System.Info("[CTCComm] Sent Result");
        }

        /// <summary>
        /// 알람 전송
        /// </summary>
        public void SendAlarm(int alarmId, string alarmName, string description)
        {
            var alarm = new AlarmObject
            {
                AlarmID = alarmId,
                AlarmName = alarmName,
                Description = description,
                RaisedAlarmTime = DateTime.Now
            };
            _server.Send(alarm);
            LogManager.Alarm.Warn($"[CTCComm] Sent ALARM: ID={alarmId}, Name={alarmName}");
        }

        /// <summary>
        /// 알람 히스토리 전송
        /// </summary>
        public void SendAlarmHistory(AlarmHistoryObject alarmHistory)
        {
            _server.Send(alarmHistory);
            LogManager.System.Info("[CTCComm] Sent Alarm History");
        }

        /// <summary>
        /// IO 데이터 전송
        /// </summary>
        public void SendIoData(IoObject ioData)
        {
            _server.Send(ioData);
            LogManager.IO.Debug("[CTCComm] Sent IO Data");
        }

        /// <summary>
        /// 트레이스 데이터 전송
        /// </summary>
        public void SendTraceData(TraceDataObject traceData)
        {
            _server.Send(traceData);
            LogManager.Trace.Debug("[CTCComm] Sent Trace Data");
        }

        /// <summary>
        /// 레시피 목록 전송
        /// </summary>
        public void SendRecipeList(RecipeListObject recipeList)
        {
            _server.Send(recipeList);
            LogManager.System.Info("[CTCComm] Sent Recipe List");
        }

        /// <summary>
        /// 레시피 데이터 전송
        /// </summary>
        public void SendRecipeData(RecipeDataObject recipeData)
        {
            _server.Send(recipeData);
            LogManager.System.Info("[CTCComm] Sent Recipe Data");
        }

        #endregion

        #region Public Methods - Status Update

        /// <summary>
        /// 장비 상태 업데이트
        /// </summary>
        public void UpdateEquipmentStatus(StatusObject.eEquipStatus status)
        {
            CurrentStatus.EquipmentStatus = status;
        }

        /// <summary>
        /// 프로세스 상태 업데이트
        /// </summary>
        public void UpdateProcessState(StatusObject.eProcessState state)
        {
            CurrentStatus.ProcessState = state;
        }

        /// <summary>
        /// 전송 상태 업데이트
        /// </summary>
        public void UpdateTransferStatus(StatusObject.eTransferStatus status)
        {
            CurrentStatus.TransferStatus = status;
        }

        /// <summary>
        /// 실행 상태 업데이트
        /// </summary>
        public void UpdateRunState(StatusObject.eRunStatus state)
        {
            CurrentStatus.RunState = state;
        }

        /// <summary>
        /// 웨이퍼 존재 여부 업데이트
        /// </summary>
        public void UpdateWaferPresence(bool isWaferOn)
        {
            CurrentStatus.IsWaferOn = isWaferOn;
        }

        #endregion

        #region Private Methods

        private void EventLink()
        {
            _server.ConnectionStateChange += OnServerConnectionChanged;
            _server.OnRcvObject += OnServerObjectReceived;
            _server.OnCommLog += OnServerLog;
        }

        private void EventUnlink()
        {
            _server.ConnectionStateChange -= OnServerConnectionChanged;
            _server.OnRcvObject -= OnServerObjectReceived;
            _server.OnCommLog -= OnServerLog;
        }

        private void OnServerConnectionChanged(object sender, IPEndPoint endPoint, bool connect)
        {
            LogManager.System.Info($"[CTCComm] CTC {endPoint} - {(connect ? "Connected" : "Disconnected")}");
            OnConnectionChanged?.Invoke(endPoint, connect);
        }

        private void OnServerObjectReceived(object sender, IPEndPoint senderEndPoint, CommObjectBase obj)
        {
            LogManager.System.Debug($"[CTCComm] Received: {obj.GetType().Name} from {senderEndPoint}");

            // 통합 이벤트 호출
            OnObjectReceived?.Invoke(obj);

            // 타입별 처리
            switch (obj)
            {
                case CommandObject cmd:
                    HandleCommand(cmd);
                    break;

                case RecipeDataObject rcp:
                    OnRecipeDataReceived?.Invoke(rcp);
                    break;

                case IoObject io:
                    OnIoReceived?.Invoke(io);
                    break;

                case TraceDataObject trace:
                    OnTraceDataReceived?.Invoke(trace);
                    break;

                case ResponseObject response:
                    // CTC로부터의 응답 처리 (필요시)
                    LogManager.System.Debug($"[CTCComm] Response: {response.ReturnCommand} - {response.Ack}");
                    break;

                case StatusObject status:
                    // CTC 상태 수신 (필요시)
                    LogManager.System.Debug($"[CTCComm] CTC Status received");
                    break;

                case TransferObject transfer:
                    // Transfer 상태 처리
                    LogManager.System.Debug($"[CTCComm] Transfer object received");
                    break;

                default:
                    LogManager.System.Warn($"[CTCComm] Unknown object type: {obj.GetType().Name}");
                    break;
            }
        }

        private void HandleCommand(CommandObject cmd)
        {
            LogManager.System.Info($"[CTCComm] Command: {cmd.Command}");
            OnCommandReceived?.Invoke(cmd);

            // 기본 자동 응답 처리
            switch (cmd.Command)
            {
                case CommandObject.eCMD.GetModuleStatus:
                    // 모듈 상태 조회
                    SendStatus();
                    break;

                case CommandObject.eCMD.GetWaferCheck:
                    // 웨이퍼 존재 여부 확인 - 상태 전송
                    SendStatus();
                    break;

                case CommandObject.eCMD.MeasurementStart:
                    break;
                // 나머지 명령은 OnCommandReceived 이벤트를 통해 외부에서 처리
                // 예: Initialize, MeasurementStart, TransferReady 등
            }
        }

        private void OnServerLog(object sender, string log)
        {
            LogManager.System.Debug(log);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    EventUnlink();
                    _server?.Dispose();
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
