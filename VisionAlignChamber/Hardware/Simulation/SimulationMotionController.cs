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
        private Dictionary<int, double> _targetPositions = new Dictionary<int, double>();
        private Dictionary<int, bool> _servoOn = new Dictionary<int, bool>();
        private Dictionary<int, bool> _isHomed = new Dictionary<int, bool>();
        private Dictionary<int, DateTime> _moveStartTime = new Dictionary<int, DateTime>();
        private Dictionary<int, int> _moveDurationMs = new Dictionary<int, int>();

        // 시뮬레이션 모션 딜레이 (ms)
        private const int DEFAULT_MOTION_DELAY_MS = 500;

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

        #region Limit 센서

        public bool GetLimitStatus(int axisNo, out bool plusLimit, out bool minusLimit)
        {
            plusLimit = false;
            minusLimit = false;

            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;

            // 시뮬레이션: 리밋 센서 항상 OFF (감지 안됨)
            return true;
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

            // 시뮬레이션: 목표 위치 설정 및 이동 시간 계산
            _targetPositions[axisNo] = position;
            _moveStartTime[axisNo] = DateTime.Now;

            // 이동 거리에 따른 딜레이 계산 (최소 200ms, 최대 2000ms)
            double currentPos = _positions.ContainsKey(axisNo) ? _positions[axisNo] : 0;
            double distance = Math.Abs(position - currentPos);
            int delayMs = Math.Max(200, Math.Min(2000, (int)(distance / (velocity > 0 ? velocity : 1) * 1000)));
            _moveDurationMs[axisNo] = delayMs;

            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} MoveAbs to {position} (delay: {delayMs}ms)");
            return true;
        }

        public bool MoveRel(int axisNo, double distance, double velocity, double accel, double decel)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;

            double currentPos = _positions.ContainsKey(axisNo) ? _positions[axisNo] : 0;
            double targetPos = currentPos + distance;
            _targetPositions[axisNo] = targetPos;
            _moveStartTime[axisNo] = DateTime.Now;

            // 이동 거리에 따른 딜레이 계산 (최소 200ms, 최대 2000ms)
            int delayMs = Math.Max(200, Math.Min(2000, (int)(Math.Abs(distance) / (velocity > 0 ? velocity : 1) * 1000)));
            _moveDurationMs[axisNo] = delayMs;

            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} MoveRel by {distance} (delay: {delayMs}ms)");
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
            if (!_isInitialized || !CheckAxisNo(axisNo)) return true;

            // 이동 중인지 확인
            if (!_moveStartTime.ContainsKey(axisNo) || !_moveDurationMs.ContainsKey(axisNo))
                return true;

            // 경과 시간 확인
            var elapsed = (DateTime.Now - _moveStartTime[axisNo]).TotalMilliseconds;
            if (elapsed >= _moveDurationMs[axisNo])
            {
                // 이동 완료 - 목표 위치로 설정
                if (_targetPositions.ContainsKey(axisNo))
                {
                    _positions[axisNo] = _targetPositions[axisNo];
                }
                return true;
            }

            // 아직 이동 중 - 현재 위치 보간
            if (_targetPositions.ContainsKey(axisNo))
            {
                double startPos = _positions.ContainsKey(axisNo) ? _positions[axisNo] : 0;
                double targetPos = _targetPositions[axisNo];
                double progress = elapsed / _moveDurationMs[axisNo];
                // 실제 위치는 업데이트하지 않고 이동 중임을 표시
            }

            return false;
        }

        #endregion

        #region 원점 복귀

        public bool HomeMove(int axisNo)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;

            // 시뮬레이션: 원점 복귀 시작 (딜레이 적용)
            _targetPositions[axisNo] = 0;
            _moveStartTime[axisNo] = DateTime.Now;
            _moveDurationMs[axisNo] = 1000; // 원점 복귀 1초
            _isHomed[axisNo] = false; // 아직 완료 안됨

            System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} HomeMove started");
            return true;
        }

        public bool IsHomeDone(int axisNo)
        {
            if (!_isInitialized || !CheckAxisNo(axisNo)) return false;

            // 이미 완료된 경우
            if (_isHomed.ContainsKey(axisNo) && _isHomed[axisNo])
                return true;

            // 원점 복귀 중인지 확인
            if (!_moveStartTime.ContainsKey(axisNo) || !_moveDurationMs.ContainsKey(axisNo))
                return false;

            // 경과 시간 확인
            var elapsed = (DateTime.Now - _moveStartTime[axisNo]).TotalMilliseconds;
            if (elapsed >= _moveDurationMs[axisNo])
            {
                // 원점 복귀 완료
                _positions[axisNo] = 0;
                _isHomed[axisNo] = true;
                System.Diagnostics.Debug.WriteLine($"[Simulation] Axis {axisNo} HomeMove completed");
                return true;
            }

            return false;
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
                    int axisNo = axisNos[i];
                    double velocity = (velocities != null && i < velocities.Length) ? velocities[i] : 10000;
                    MoveAbs(axisNo, positions[i], velocity, 0, 0);
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
            //return axisNo >= 0 && axisNo < _axisCount;
            return true;
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
