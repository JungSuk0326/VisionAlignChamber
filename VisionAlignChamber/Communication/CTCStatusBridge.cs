using System;
using CommObject;
using VisionAlignChamber.Communication.Interfaces;
using VisionAlignChamber.Core;
using VisionAlignChamber.Models;
using VisionAlignChamber.Log;

namespace VisionAlignChamber.Communication
{
    /// <summary>
    /// AppState ↔ StatusObject 변환 브릿지
    /// AppState 변경 시 자동으로 StatusObject 변환 + CTC 전송
    /// </summary>
    public class CTCStatusBridge : IDisposable
    {
        #region Fields

        private readonly ICTCCommunication _ctcComm;
        private Action<object> _stateChangedHandler;
        private bool _disposed;

        #endregion

        #region Constructor

        public CTCStatusBridge(ICTCCommunication ctcComm)
        {
            _ctcComm = ctcComm ?? throw new ArgumentNullException(nameof(ctcComm));
            Subscribe();
        }

        #endregion

        #region AppState → StatusObject 변환

        /// <summary>
        /// 현재 AppState를 StatusObject로 변환
        /// </summary>
        public StatusObject BuildStatusObject()
        {
            var app = AppState.Current;
            var status = new StatusObject
            {
                EquipmentStatus = ConvertEquipStatus(app),
                ProcessState = ConvertProcessState(app),
                TransferStatus = ConvertTransferStatus(app),
                IsWaferOn = app.IsWaferExist,
                LastError = app.IsEmergencyStop ? "Emergency Stop" : "",
            };

            return status;
        }

        /// <summary>
        /// AppState → eEquipStatus 변환
        /// 우선순위: EMO/Error → Fault, Local → PM, 그 외 → Normal
        /// </summary>
        private StatusObject.eEquipStatus ConvertEquipStatus(AppState app)
        {
            if (app.IsEmergencyStop || app.SystemStatus == SystemStatus.Error || app.SystemStatus == SystemStatus.EMO)
                return StatusObject.eEquipStatus.Fault;

            if (app.ControlAuthority == ControlAuthority.Local)
                return StatusObject.eEquipStatus.PM;

            return StatusObject.eEquipStatus.Normal;
        }

        /// <summary>
        /// AppState → eProcessState 변환
        /// Running → Execute, Paused → Pause, 그 외 → Idle
        /// </summary>
        private StatusObject.eProcessState ConvertProcessState(AppState app)
        {
            switch (app.SystemStatus)
            {
                case SystemStatus.Running:
                    return StatusObject.eProcessState.Execute;
                case SystemStatus.Paused:
                    return StatusObject.eProcessState.Pause;
                default:
                    return StatusObject.eProcessState.Idle;
            }
        }

        /// <summary>
        /// AppState → eTransferStatus 변환
        /// Local/EMO/Error/Running → NotReady, 그 외 → 시퀀스에서 관리하는 값 유지
        /// </summary>
        private StatusObject.eTransferStatus ConvertTransferStatus(AppState app)
        {
            // Local(PM), 에러, 비상정지, 동작 중에는 Transfer 불가
            if (app.ControlAuthority == ControlAuthority.Local
                || app.IsEmergencyStop
                || app.SystemStatus == SystemStatus.Error
                || app.SystemStatus == SystemStatus.EMO
                || app.SystemStatus == SystemStatus.Running)
            {
                return StatusObject.eTransferStatus.NotReady;
            }

            // 그 외에는 시퀀스/핸들러에서 설정한 값 유지
            return _ctcComm.CurrentStatus.TransferStatus;
        }

        #endregion

        #region 자동 상태 전송

        /// <summary>
        /// AppState 변경 이벤트 구독
        /// </summary>
        private void Subscribe()
        {
            _stateChangedHandler = OnAppStateChanged;
            EventManager.Subscribe(EventManager.SystemStateChanged, _stateChangedHandler);
        }

        /// <summary>
        /// AppState 변경 시 StatusObject 변환 + CTC 전송
        /// </summary>
        private void OnAppStateChanged(object data)
        {
            var propertyName = data as string;
            if (string.IsNullOrEmpty(propertyName)) return;

            // CTC 전송이 필요한 상태 변경만 처리
            switch (propertyName)
            {
                case nameof(AppState.SystemStatus):
                case nameof(AppState.IsWaferExist):
                case nameof(AppState.IsEmergencyStop):
                case nameof(AppState.ControlAuthority):
                    SendCurrentStatus();
                    break;
            }
        }

        /// <summary>
        /// 현재 상태를 CTC에 전송
        /// </summary>
        public void SendCurrentStatus()
        {
            try
            {
                var status = BuildStatusObject();
                _ctcComm.SendStatus(status);
                LogManager.System.Debug($"[StatusBridge] Sent → Equip={status.EquipmentStatus}, Process={status.ProcessState}, Transfer={status.TransferStatus}, Wafer={status.IsWaferOn}");
            }
            catch (Exception ex)
            {
                LogManager.System.Error($"[StatusBridge] SendStatus Error: {ex.Message}");
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (_disposed) return;

            if (_stateChangedHandler != null)
            {
                EventManager.Unsubscribe(EventManager.SystemStateChanged, _stateChangedHandler);
                _stateChangedHandler = null;
            }

            _disposed = true;
        }

        #endregion
    }
}
