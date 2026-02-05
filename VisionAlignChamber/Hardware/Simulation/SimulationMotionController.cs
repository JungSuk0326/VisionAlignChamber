using System;
using System.Collections.Generic;
using VisionAlignChamber.Interfaces;

namespace VisionAlignChamber.Hardware.Simulation
{
    /// <summary>
    /// 시뮬레이션용 모션 컨트롤러
    /// 실제 하드웨어 없이 UI 테스트용
    /// </summary>
    public class SimulationMotionController : IMotionController
    {
        #region Fields

        private bool _isInitialized = false;
        private bool _disposed = false;
        private int _axisCount = 4; // WedgeUpDown, ChuckRotation, CenteringStage_1, CenteringStage_2
        private Dictionary<int, double> _positions = new Dictionary<int, double>();
        private Dictionary<int, bool> _servoOn = new Dictionary<int, bool>();
        private Dictionary<int, bool> _isHomed = new Dictionary<int, bool>();

        #endregion

        #region Properties

        public bool IsInitialized => _isInitialized;
        public int AxisCount => _axisCount;

        #endregion

        #region 초기화

        public bool Initialize()
        {
            if (_isInitialized)
                return true;

            // 시뮬레이션 초기화
            for (int i = 0; i < _axisCount; i++)
            {
                _positions[i] = 0;
                _servoOn[i] = false;
                _isHomed[i] = false;
            }

            _isInitialized = true;
            System.Diagnostics.Debug.WriteLine("[Simulation] MotionController initialized");
            return true;
        }

        public void Close()
        {
            if (!_isInitialized)
                return;

            _isInitialized = false;
            System.Diagnostics.Debug.WriteLine("[Simulation] MotionController closed");
        }

        #endregion

        #region 서보 제어

        public bool ServoOn(int axisNo)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;
            _servoOn[axisNo] = true;
            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} Servo ON");
            return true;
        }

        public bool ServoOff(int axisNo)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;
            _servoOn[axisNo] = false;
            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} Servo OFF");
            return true;
        }

        public bool IsServoOn(int axisNo)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;
            return _servoOn.ContainsKey(axisNo) && _servoOn[axisNo];
        }

        #endregion

        #region 알람

        public bool IsAlarm(int axisNo)
        {
            // 시뮬레이션에서는 알람 없음
            return false;
        }

        public bool ClearAlarm(int axisNo)
        {
            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} Alarm cleared");
            return true;
        }

        #endregion

        #region 위치

        public double GetActualPosition(int axisNo)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return 0;
            return _positions.ContainsKey(axisNo) ? _positions[axisNo] : 0;
        }

        public double GetCommandPosition(int axisNo)
        {
            return GetActualPosition(axisNo);
        }

        public bool SetActualPosition(int axisNo, double position)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;
            _positions[axisNo] = position;
            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} position set to {position}");
            return true;
        }

        #endregion

        #region 단축 이동

        public bool MoveAbs(int axisNo, double position, double velocity, double accel, double decel)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;

            // 시뮬레이션: 즉시 목표 위치로 설정
            _positions[axisNo] = position;
            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} MoveAbs to {position}");
            return true;
        }

        public bool MoveRel(int axisNo, double distance, double velocity, double accel, double decel)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;

            double currentPos = _positions.ContainsKey(axisNo) ? _positions[axisNo] : 0;
            _positions[axisNo] = currentPos + distance;
            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} MoveRel by {distance}");
            return true;
        }

        public bool MoveVelocity(int axisNo, double velocity, double accel, double decel)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;
            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} MoveVelocity at {velocity}");
            return true;
        }

        #endregion

        #region 정지

        public bool Stop(int axisNo)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;
            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} Stop");
            return true;
        }

        public bool EmergencyStop(int axisNo)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;
            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} EmergencyStop");
            return true;
        }

        public bool IsMotionDone(int axisNo)
        {
            // 시뮬레이션에서는 항상 완료
            return true;
        }

        #endregion

        #region 원점 복귀

        public bool HomeMove(int axisNo)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;

            // 시뮬레이션: 즉시 원점으로 설정
            _positions[axisNo] = 0;
            _isHomed[axisNo] = true;
            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} HomeMove completed");
            return true;
        }

        public bool IsHomeDone(int axisNo)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;
            return _isHomed.ContainsKey(axisNo) && _isHomed[axisNo];
        }

        #endregion

        #region 다축 동기 이동

        public bool MoveAbsMulti(int[] axisNos, double[] positions, double[] velocities, double[] accels, double[] decels)
        {
            if (!_isInitialized) return false;
            if (axisNos == null || positions == null) return false;

            for (int i = 0; i < axisNos.Length; i++)
            {
                if (CheckAxisNo(axisNos[i]))
                {
                    _positions[axisNos[i]] = positions[i];
                }
            }

            System.Diagnostics.Debug.WriteLine($"[Simulation] MoveAbsMulti for {axisNos.Length} axes");
            return true;
        }

        public bool EmergencyStopAll()
        {
            System.Diagnostics.Debug.WriteLine("[Simulation] EmergencyStopAll");
            return true;
        }

        #endregion

        #region Helper Methods

        private bool CheckAxisNo(int axisNo)
        {
            return axisNo >= 0 && axisNo < _axisCount;
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

        ~SimulationMotionController()
        {
            Dispose(false);
        }

        #endregion
    }
}
