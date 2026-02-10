using System;
using System.Collections.Generic;
using System.IO;
using VisionAlignChamber.Interfaces;
using VisionAlignChamber.Hardware.Common;
using VisionAlignChamber.Config;

namespace VisionAlignChamber.Hardware.Ajin
{
    /// <summary>
    /// 아진 EtherCAT 모션 컨트롤러 구현
    /// </summary>
    public class AjinMotionController : IMotionController
    {
        #region Fields

        private bool _isInitialized = false;
        private bool _disposed = false;
        private int _axisCount = 0;
        private Dictionary<int, AxisParameter> _axisParameters = new Dictionary<int, AxisParameter>();

        #endregion

        #region Properties

        public bool IsInitialized => _isInitialized;
        public int AxisCount => _axisCount;

        #endregion

        #region 초기화

        /// <summary>
        /// 모션 컨트롤러 초기화
        /// </summary>
        public bool Initialize()
        {
            if (_isInitialized)
                return true;

            try
            {
                // 라이브러리 열기
                uint result = CAXL.AxlOpen(7);
                if (result != AjinErrorCode.AXT_RT_SUCCESS && result != AjinErrorCode.AXT_RT_OPEN_ALREADY)
                {
                    throw new MotionException(result, $"AxlOpen failed: {AjinErrorCode.GetErrorMessage(result)}");
                }

                // 모션 파라미터 파일 로드 (.mot)
                LoadMotionParameterFile();

                // 모션 모듈 존재 확인
                uint isMotion = 0;
                result = CAXM.AxmInfoIsMotionModule(ref isMotion);
                if (result != AjinErrorCode.AXT_RT_SUCCESS || isMotion == 0)
                {
                    throw new MotionException("Motion module not found");
                }

                // 축 개수 확인
                result = CAXM.AxmInfoGetAxisCount(ref _axisCount);
                if (result != AjinErrorCode.AXT_RT_SUCCESS)
                {
                    throw new MotionException(result, $"AxmInfoGetAxisCount failed: {AjinErrorCode.GetErrorMessage(result)}");
                }

                // 축 파라미터 초기화
                for (int i = 0; i < _axisCount; i++)
                {
                    _axisParameters[i] = new AxisParameter { AxisNo = i, Name = $"Axis{i}" };
                }

                _isInitialized = true;
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AjinMotionController Initialize Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 모션 컨트롤러 종료
        /// </summary>
        public void Close()
        {
            if (!_isInitialized)
                return;

            try
            {
                // 모든 축 서보 OFF
                for (int i = 0; i < _axisCount; i++)
                {
                    ServoOff(i);
                }

                // 라이브러리 닫기
                CAXL.AxlClose();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AjinMotionController Close Error: {ex.Message}");
            }
            finally
            {
                _isInitialized = false;
            }
        }

        #endregion

        #region 서보 제어

        /// <summary>
        /// 서보 ON
        /// </summary>
        public bool ServoOn(int axisNo)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            uint result = CAXM.AxmSignalServoOn(axisNo, 1);
            return AjinErrorCode.IsSuccess(result);
        }

        /// <summary>
        /// 서보 OFF
        /// </summary>
        public bool ServoOff(int axisNo)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            uint result = CAXM.AxmSignalServoOn(axisNo, 0);
            return AjinErrorCode.IsSuccess(result);
        }

        /// <summary>
        /// 서보 ON 상태 확인
        /// </summary>
        public bool IsServoOn(int axisNo)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            uint onOff = 0;
            uint result = CAXM.AxmSignalIsServoOn(axisNo, ref onOff);
            return AjinErrorCode.IsSuccess(result) && onOff == 1;
        }

        #endregion

        #region 알람

        /// <summary>
        /// 알람 상태 확인
        /// </summary>
        public bool IsAlarm(int axisNo)
        {
            if (!CheckInitialized()) return true;
            if (!CheckAxisNo(axisNo)) return true;

            uint alarm = 0;
            uint result = CAXM.AxmSignalReadServoAlarm(axisNo, ref alarm);
            return AjinErrorCode.IsSuccess(result) && alarm != 0;
        }

        /// <summary>
        /// 알람 클리어
        /// </summary>
        public bool ClearAlarm(int axisNo)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            // 알람 리셋 신호 ON
            uint result = CAXM.AxmSignalServoAlarmReset(axisNo, 1);
            if (!AjinErrorCode.IsSuccess(result)) return false;

            System.Threading.Thread.Sleep(100);

            // 알람 리셋 신호 OFF
            result = CAXM.AxmSignalServoAlarmReset(axisNo, 0);
            return AjinErrorCode.IsSuccess(result);
        }

        #endregion

        #region 위치

        /// <summary>
        /// 실제 위치 읽기 (엔코더 피드백)
        /// </summary>
        public double GetActualPosition(int axisNo)
        {
            if (!CheckInitialized()) return 0;
            if (!CheckAxisNo(axisNo)) return 0;

            double pos = 0;
            uint result = CAXM.AxmStatusGetActPos(axisNo, ref pos);
            return AjinErrorCode.IsSuccess(result) ? pos : 0;
        }

        /// <summary>
        /// 명령 위치 읽기
        /// </summary>
        public double GetCommandPosition(int axisNo)
        {
            if (!CheckInitialized()) return 0;
            if (!CheckAxisNo(axisNo)) return 0;

            double pos = 0;
            uint result = CAXM.AxmStatusGetCmdPos(axisNo, ref pos);
            return AjinErrorCode.IsSuccess(result) ? pos : 0;
        }

        /// <summary>
        /// 실제 위치 설정 (원점 설정 등)
        /// </summary>
        public bool SetActualPosition(int axisNo, double position)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            uint result = CAXM.AxmStatusSetActPos(axisNo, position);
            if (!AjinErrorCode.IsSuccess(result)) return false;

            result = CAXM.AxmStatusSetCmdPos(axisNo, position);
            return AjinErrorCode.IsSuccess(result);
        }

        #endregion

        #region 단축 이동

        /// <summary>
        /// 절대 위치 이동
        /// </summary>
        public bool MoveAbs(int axisNo, double position, double velocity, double accel, double decel)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            // 절대 좌표 모드 설정
            CAXM.AxmMotSetAbsRelMode(axisNo, 0); // POS_ABS_MODE = 0

            uint result = CAXM.AxmMovePos(axisNo, position, velocity, accel, decel);
            return AjinErrorCode.IsSuccess(result);
        }

        /// <summary>
        /// 상대 위치 이동
        /// </summary>
        public bool MoveRel(int axisNo, double distance, double velocity, double accel, double decel)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            // 상대 좌표 모드 설정
            CAXM.AxmMotSetAbsRelMode(axisNo, 1); // POS_REL_MODE = 1

            uint result = CAXM.AxmMovePos(axisNo, distance, velocity, accel, decel);
            return AjinErrorCode.IsSuccess(result);
        }

        /// <summary>
        /// 속도 이동 (JOG)
        /// </summary>
        public bool MoveVelocity(int axisNo, double velocity, double accel, double decel)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            uint result = CAXM.AxmMoveVel(axisNo, velocity, accel, decel);
            return AjinErrorCode.IsSuccess(result);
        }

        #endregion

        #region 정지

        /// <summary>
        /// 감속 정지
        /// </summary>
        public bool Stop(int axisNo)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            uint result = CAXM.AxmMoveSStop(axisNo);
            return AjinErrorCode.IsSuccess(result);
        }

        /// <summary>
        /// 비상 정지 (즉시 정지)
        /// </summary>
        public bool EmergencyStop(int axisNo)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            uint result = CAXM.AxmMoveEStop(axisNo);
            return AjinErrorCode.IsSuccess(result);
        }

        /// <summary>
        /// 이동 완료 확인
        /// </summary>
        public bool IsMotionDone(int axisNo)
        {
            if (!CheckInitialized()) return true;
            if (!CheckAxisNo(axisNo)) return true;

            uint status = 0;
            uint result = CAXM.AxmStatusReadInMotion(axisNo, ref status);
            // status: 0 = 정지, 1 = 이동중
            return AjinErrorCode.IsSuccess(result) && status == 0;
        }

        #endregion

        #region 원점 복귀

        /// <summary>
        /// 원점 복귀 시작
        /// </summary>
        public bool HomeMove(int axisNo)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            uint result = CAXM.AxmHomeSetStart(axisNo);
            return AjinErrorCode.IsSuccess(result);
        }

        /// <summary>
        /// 원점 복귀 완료 확인
        /// </summary>
        public bool IsHomeDone(int axisNo)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            uint homeResult = 0;
            uint result = CAXM.AxmHomeGetResult(axisNo, ref homeResult);
            // homeResult: 0=진행중, 1=성공, 다른값=실패
            return AjinErrorCode.IsSuccess(result) && homeResult == 1;
        }

        #endregion

        #region 다축 동기 이동

        /// <summary>
        /// 다축 절대 위치 동기 이동
        /// </summary>
        public bool MoveAbsMulti(int[] axisNos, double[] positions, double[] velocities, double[] accels, double[] decels)
        {
            if (!CheckInitialized()) return false;
            if (axisNos == null || positions == null || velocities == null || accels == null || decels == null)
                return false;
            if (axisNos.Length != positions.Length || axisNos.Length != velocities.Length ||
                axisNos.Length != accels.Length || axisNos.Length != decels.Length)
                return false;

            // 모든 축 절대 좌표 모드 설정
            foreach (int axisNo in axisNos)
            {
                CAXM.AxmMotSetAbsRelMode(axisNo, 0); // POS_ABS_MODE = 0
            }

            uint result = CAXM.AxmMoveStartMultiPos(axisNos.Length, axisNos, positions, velocities, accels, decels);
            return AjinErrorCode.IsSuccess(result);
        }

        /// <summary>
        /// 모든 축 비상 정지
        /// </summary>
        public bool EmergencyStopAll()
        {
            if (!CheckInitialized()) return false;

            bool success = true;
            for (int i = 0; i < _axisCount; i++)
            {
                if (!EmergencyStop(i))
                    success = false;
            }
            return success;
        }

        #endregion

        #region 파라미터

        /// <summary>
        /// 축 파라미터 설정
        /// </summary>
        public bool SetAxisParameter(int axisNo, AxisParameter param)
        {
            if (!CheckInitialized()) return false;
            if (!CheckAxisNo(axisNo)) return false;

            _axisParameters[axisNo] = param.Clone();
            return true;
        }

        /// <summary>
        /// 축 파라미터 조회
        /// </summary>
        public AxisParameter GetAxisParameter(int axisNo)
        {
            if (!CheckAxisNo(axisNo)) return null;
            return _axisParameters.ContainsKey(axisNo) ? _axisParameters[axisNo] : null;
        }

        #endregion

        #region Helper Methods

        private bool CheckInitialized()
        {
            if (!_isInitialized)
            {
                System.Diagnostics.Debug.WriteLine("AjinMotionController is not initialized");
                return false;
            }
            return true;
        }

        private bool CheckAxisNo(int axisNo)
        {
            if (axisNo < 0 || axisNo >= _axisCount)
            {
                System.Diagnostics.Debug.WriteLine($"Invalid axis number: {axisNo} (valid: 0~{_axisCount - 1})");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 모션 파라미터 파일(.mot) 로드
        /// </summary>
        private void LoadMotionParameterFile()
        {
            string motFilePath = AppSettings.MotParamFilePath;

            if (!File.Exists(motFilePath))
            {
                System.Diagnostics.Debug.WriteLine($"Motion parameter file not found: {motFilePath} (using default parameters)");
                return;
            }

            uint result = CAXM.AxmMotLoadParaAll(motFilePath);
            if (result == AjinErrorCode.AXT_RT_SUCCESS)
            {
                System.Diagnostics.Debug.WriteLine($"Motion parameter file loaded: {motFilePath}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"AxmMotLoadParaAll failed: {AjinErrorCode.GetErrorMessage(result)} (File: {motFilePath})");
            }
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
                    Close();
                }
                _disposed = true;
            }
        }

        ~AjinMotionController()
        {
            Dispose(false);
        }

        #endregion
    }
}
