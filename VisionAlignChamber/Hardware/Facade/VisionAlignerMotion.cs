using System;
using System.Threading;
using System.Threading.Tasks;
using VisionAlignChamber.Interfaces;

namespace VisionAlignChamber.Hardware.Facade
{
    /// <summary>
    /// Vision Aligner 전용 Motion Facade
    /// 복잡한 축 번호 대신 의미있는 메서드로 모션 제어
    /// </summary>
    public class VisionAlignerMotion
    {
        #region Fields

        private readonly IMotionController _motion;
        private readonly HardwareMapping _mapping;

        #endregion

        #region Constructor

        public VisionAlignerMotion(IMotionController motionController, HardwareMapping mapping)
        {
            _motion = motionController ?? throw new ArgumentNullException(nameof(motionController));
            _mapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
        }

        #endregion

        #region Initialization

        /// <summary>
        /// 모션 컨트롤러 초기화 상태
        /// </summary>
        public bool IsInitialized => _motion.IsInitialized;

        /// <summary>
        /// 모션 컨트롤러 초기화
        /// </summary>
        /// <returns>성공 여부</returns>
        public bool Initialize()
        {
            return _motion.Initialize();
        }

        /// <summary>
        /// 모션 컨트롤러 종료
        /// </summary>
        public void Close()
        {
            _motion.Close();
        }

        /// <summary>
        /// 사용 가능한 축 개수
        /// </summary>
        public int AxisCount => _motion.AxisCount;

        /// <summary>
        /// 축 활성화 여부 확인 (INI 설정)
        /// </summary>
        public bool IsAxisEnabled(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return info.Enabled;
        }

        #endregion

        #region Servo Control

        /// <summary>
        /// 단일 축 서보 ON
        /// </summary>
        public bool ServoOn(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return _motion.ServoOn(info.AxisNo);
        }

        /// <summary>
        /// 단일 축 서보 OFF
        /// </summary>
        public bool ServoOff(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return _motion.ServoOff(info.AxisNo);
        }

        /// <summary>
        /// 단일 축 서보 ON 상태 확인
        /// </summary>
        public bool IsServoOn(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return _motion.IsServoOn(info.AxisNo);
        }

        /// <summary>
        /// 모든 축 서보 ON
        /// </summary>
        public bool ServoOnAll()
        {
            bool result = true;
            result &= ServoOn(VAMotionAxis.WedgeUpDown);
            result &= ServoOn(VAMotionAxis.ChuckRotation);
            result &= ServoOn(VAMotionAxis.CenteringStage_1);
            result &= ServoOn(VAMotionAxis.CenteringStage_2);
            return result;
        }

        /// <summary>
        /// 모든 축 서보 OFF
        /// </summary>
        public bool ServoOffAll()
        {
            bool result = true;
            result &= ServoOff(VAMotionAxis.WedgeUpDown);
            result &= ServoOff(VAMotionAxis.ChuckRotation);
            result &= ServoOff(VAMotionAxis.CenteringStage_1);
            result &= ServoOff(VAMotionAxis.CenteringStage_2);
            return result;
        }

        /// <summary>
        /// 모든 축 서보 ON 상태 확인
        /// </summary>
        public bool IsAllServoOn()
        {
            return IsServoOn(VAMotionAxis.WedgeUpDown) &&
                   IsServoOn(VAMotionAxis.ChuckRotation) &&
                   IsServoOn(VAMotionAxis.CenteringStage_1) &&
                   IsServoOn(VAMotionAxis.CenteringStage_2);
        }

        #endregion

        #region Alarm Control

        /// <summary>
        /// 단일 축 알람 상태 확인
        /// </summary>
        public bool IsAlarm(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return _motion.IsAlarm(info.AxisNo);
        }

        /// <summary>
        /// 단일 축 알람 클리어
        /// </summary>
        public bool ClearAlarm(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return _motion.ClearAlarm(info.AxisNo);
        }

        /// <summary>
        /// 모든 축 알람 상태 확인
        /// </summary>
        public bool IsAnyAlarm()
        {
            return IsAlarm(VAMotionAxis.WedgeUpDown) ||
                   IsAlarm(VAMotionAxis.ChuckRotation) ||
                   IsAlarm(VAMotionAxis.CenteringStage_1) ||
                   IsAlarm(VAMotionAxis.CenteringStage_2);
        }

        /// <summary>
        /// 모든 축 알람 클리어
        /// </summary>
        public bool ClearAlarmAll()
        {
            bool result = true;
            result &= ClearAlarm(VAMotionAxis.WedgeUpDown);
            result &= ClearAlarm(VAMotionAxis.ChuckRotation);
            result &= ClearAlarm(VAMotionAxis.CenteringStage_1);
            result &= ClearAlarm(VAMotionAxis.CenteringStage_2);
            return result;
        }

        #endregion

        #region Generic Motion Methods

        /// <summary>
        /// 축 번호 가져오기
        /// </summary>
        public int GetAxisNo(VAMotionAxis axis)
        {
            return _mapping.GetAxisInfo(axis).AxisNo;
        }

        /// <summary>
        /// 절대 위치 이동
        /// </summary>
        public bool MoveAbsolute(VAMotionAxis axis, double position, double? velocity = null)
        {
            var info = _mapping.GetAxisInfo(axis);
            double vel = velocity ?? info.DefaultVelocity;
            return _motion.MoveAbs(info.AxisNo, position, vel, info.DefaultAccel, info.DefaultDecel);
        }

        /// <summary>
        /// 상대 위치 이동
        /// </summary>
        public bool MoveRelative(VAMotionAxis axis, double distance, double? velocity = null)
        {
            var info = _mapping.GetAxisInfo(axis);
            double vel = velocity ?? info.DefaultVelocity;
            return _motion.MoveRel(info.AxisNo, distance, vel, info.DefaultAccel, info.DefaultDecel);
        }

        /// <summary>
        /// 현재 위치 읽기 (실제 위치)
        /// </summary>
        public double GetPosition(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return _motion.GetActualPosition(info.AxisNo);
        }

        /// <summary>
        /// 모션 정지
        /// </summary>
        public bool Stop(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return _motion.Stop(info.AxisNo);
        }

        /// <summary>
        /// 비상 정지
        /// </summary>
        public bool EmergencyStop(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return _motion.EmergencyStop(info.AxisNo);
        }

        /// <summary>
        /// 모션 완료 대기
        /// </summary>
        public bool WaitForDone(VAMotionAxis axis, int timeoutMs = 30000)
        {
            var info = _mapping.GetAxisInfo(axis);
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                if (_motion.IsMotionDone(info.AxisNo))
                    return true;
                System.Threading.Thread.Sleep(10);
            }
            return false;
        }

        /// <summary>
        /// 모션 완료 대기 (비동기)
        /// </summary>
        public async Task<bool> WaitForDoneAsync(VAMotionAxis axis, int timeoutMs = 30000, CancellationToken ct = default)
        {
            var info = _mapping.GetAxisInfo(axis);
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                ct.ThrowIfCancellationRequested();
                if (_motion.IsMotionDone(info.AxisNo))
                    return true;
                await Task.Delay(10, ct);
            }
            return false;
        }

        /// <summary>
        /// 절대 위치 이동 (비동기) - 이동 완료까지 대기
        /// </summary>
        public async Task<bool> MoveAbsoluteAsync(VAMotionAxis axis, double position, double? velocity = null, int timeoutMs = 30000, CancellationToken ct = default)
        {
            if (!MoveAbsolute(axis, position, velocity))
                return false;

            return await WaitForDoneAsync(axis, timeoutMs, ct);
        }

        /// <summary>
        /// 상대 위치 이동 (비동기) - 이동 완료까지 대기
        /// </summary>
        public async Task<bool> MoveRelativeAsync(VAMotionAxis axis, double distance, double? velocity = null, int timeoutMs = 30000, CancellationToken ct = default)
        {
            if (!MoveRelative(axis, distance, velocity))
                return false;

            return await WaitForDoneAsync(axis, timeoutMs, ct);
        }

        /// <summary>
        /// 모션 중인지 확인
        /// </summary>
        public bool IsMoving(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return !_motion.IsMotionDone(info.AxisNo);
        }

        /// <summary>
        /// 홈 위치로 이동
        /// </summary>
        public bool MoveHome(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return _motion.HomeMove(info.AxisNo);
        }

        #endregion

        #region Wedge Up/Down Stage

        /// <summary>
        /// Wedge Stage 상승
        /// </summary>
        /// <param name="position">상승 위치 (pulse)</param>
        public bool WedgeStageUp(double position, double? velocity = null)
        {
            return MoveAbsolute(VAMotionAxis.WedgeUpDown, position, velocity);
        }

        /// <summary>
        /// Wedge Stage 하강
        /// </summary>
        /// <param name="position">하강 위치 (pulse)</param>
        public bool WedgeStageDown(double position, double? velocity = null)
        {
            return MoveAbsolute(VAMotionAxis.WedgeUpDown, position, velocity);
        }

        /// <summary>
        /// Wedge Stage 현재 위치
        /// </summary>
        public double GetWedgeStagePosition()
        {
            return GetPosition(VAMotionAxis.WedgeUpDown);
        }

        /// <summary>
        /// Wedge Stage 홈
        /// </summary>
        public bool WedgeStageHome()
        {
            return MoveHome(VAMotionAxis.WedgeUpDown);
        }

        /// <summary>
        /// Wedge Stage 이동 (비동기)
        /// </summary>
        public async Task<bool> WedgeStageMoveAsync(double position, double? velocity = null, int timeoutMs = 30000, CancellationToken ct = default)
        {
            return await MoveAbsoluteAsync(VAMotionAxis.WedgeUpDown, position, velocity, timeoutMs, ct);
        }

        #endregion

        #region Chuck Rotation (DD Motor)

        /// <summary>
        /// Chuck 회전 (절대 각도)
        /// </summary>
        /// <param name="angle">회전 각도 (degree)</param>
        /// <param name="velocity">회전 속도</param>
        public bool ChuckRotateAbsolute(double angle, double? velocity = null)
        {
            // 각도를 pulse로 변환 (설정에 따라 조정 필요)
            double position = AngleToPulse(angle);
            return MoveAbsolute(VAMotionAxis.ChuckRotation, position, velocity);
        }

        /// <summary>
        /// Chuck 상대 회전
        /// </summary>
        /// <param name="angle">회전 각도 (degree)</param>
        public bool ChuckRotateRelative(double angle, double? velocity = null)
        {
            double distance = AngleToPulse(angle);
            return MoveRelative(VAMotionAxis.ChuckRotation, distance, velocity);
        }

        /// <summary>
        /// Chuck 현재 각도
        /// </summary>
        public double GetChuckAngle()
        {
            double position = GetPosition(VAMotionAxis.ChuckRotation);
            return PulseToAngle(position);
        }

        /// <summary>
        /// Chuck 홈 (0도)
        /// </summary>
        public bool ChuckHome()
        {
            return MoveHome(VAMotionAxis.ChuckRotation);
        }

        /// <summary>
        /// Chuck 90도 위치로 회전
        /// </summary>
        public bool ChuckRotateTo90()
        {
            return ChuckRotateAbsolute(90);
        }

        /// <summary>
        /// Chuck 180도 위치로 회전
        /// </summary>
        public bool ChuckRotateTo180()
        {
            return ChuckRotateAbsolute(180);
        }

        /// <summary>
        /// Chuck 270도 위치로 회전
        /// </summary>
        public bool ChuckRotateTo270()
        {
            return ChuckRotateAbsolute(270);
        }

        /// <summary>
        /// Chuck 회전 (비동기) - 절대 각도
        /// </summary>
        public async Task<bool> ChuckRotateAbsoluteAsync(double angle, double? velocity = null, int timeoutMs = 30000, CancellationToken ct = default)
        {
            double position = AngleToPulse(angle);
            return await MoveAbsoluteAsync(VAMotionAxis.ChuckRotation, position, velocity, timeoutMs, ct);
        }

        /// <summary>
        /// Chuck 상대 회전 (비동기)
        /// </summary>
        public async Task<bool> ChuckRotateRelativeAsync(double angle, double? velocity = null, int timeoutMs = 30000, CancellationToken ct = default)
        {
            double distance = AngleToPulse(angle);
            return await MoveRelativeAsync(VAMotionAxis.ChuckRotation, distance, velocity, timeoutMs, ct);
        }

        // 각도 <-> 펄스 변환 (DD Motor 스펙에 따라 조정)
        private const double PULSE_PER_DEGREE = 10000.0; // 예: 1도 = 10000 pulse

        private double AngleToPulse(double angle)
        {
            return angle * PULSE_PER_DEGREE;
        }

        private double PulseToAngle(double pulse)
        {
            return pulse / PULSE_PER_DEGREE;
        }

        #endregion

        #region Centering Stage 1

        /// <summary>
        /// Centering Stage 1 이동
        /// </summary>
        /// <param name="position">이동 위치 (pulse)</param>
        public bool CenteringStage1Move(double position, double? velocity = null)
        {
            return MoveAbsolute(VAMotionAxis.CenteringStage_1, position, velocity);
        }

        /// <summary>
        /// Centering Stage 1 상대 이동
        /// </summary>
        public bool CenteringStage1MoveRelative(double distance, double? velocity = null)
        {
            return MoveRelative(VAMotionAxis.CenteringStage_1, distance, velocity);
        }

        /// <summary>
        /// Centering Stage 1 현재 위치
        /// </summary>
        public double GetCenteringStage1Position()
        {
            return GetPosition(VAMotionAxis.CenteringStage_1);
        }

        /// <summary>
        /// Centering Stage 1 홈
        /// </summary>
        public bool CenteringStage1Home()
        {
            return MoveHome(VAMotionAxis.CenteringStage_1);
        }

        /// <summary>
        /// Centering Stage 1 이동 (비동기)
        /// </summary>
        public async Task<bool> CenteringStage1MoveAsync(double position, double? velocity = null, int timeoutMs = 30000, CancellationToken ct = default)
        {
            return await MoveAbsoluteAsync(VAMotionAxis.CenteringStage_1, position, velocity, timeoutMs, ct);
        }

        #endregion

        #region Centering Stage 2

        /// <summary>
        /// Centering Stage 2 이동
        /// </summary>
        /// <param name="position">이동 위치 (pulse)</param>
        public bool CenteringStage2Move(double position, double? velocity = null)
        {
            return MoveAbsolute(VAMotionAxis.CenteringStage_2, position, velocity);
        }

        /// <summary>
        /// Centering Stage 2 상대 이동
        /// </summary>
        public bool CenteringStage2MoveRelative(double distance, double? velocity = null)
        {
            return MoveRelative(VAMotionAxis.CenteringStage_2, distance, velocity);
        }

        /// <summary>
        /// Centering Stage 2 현재 위치
        /// </summary>
        public double GetCenteringStage2Position()
        {
            return GetPosition(VAMotionAxis.CenteringStage_2);
        }

        /// <summary>
        /// Centering Stage 2 홈
        /// </summary>
        public bool CenteringStage2Home()
        {
            return MoveHome(VAMotionAxis.CenteringStage_2);
        }

        /// <summary>
        /// Centering Stage 2 이동 (비동기)
        /// </summary>
        public async Task<bool> CenteringStage2MoveAsync(double position, double? velocity = null, int timeoutMs = 30000, CancellationToken ct = default)
        {
            return await MoveAbsoluteAsync(VAMotionAxis.CenteringStage_2, position, velocity, timeoutMs, ct);
        }

        #endregion

        #region Centering Stage (Both)

        /// <summary>
        /// 두 Centering Stage 동시 이동
        /// </summary>
        public bool CenteringStagesMoveSync(double position1, double position2, double? velocity = null)
        {
            bool result1 = CenteringStage1Move(position1, velocity);
            bool result2 = CenteringStage2Move(position2, velocity);
            return result1 && result2;
        }

        /// <summary>
        /// 두 Centering Stage 동시 홈
        /// </summary>
        public bool CenteringStagesHomeSync()
        {
            bool result1 = CenteringStage1Home();
            bool result2 = CenteringStage2Home();
            return result1 && result2;
        }

        /// <summary>
        /// Centering Stage Wafer 방향으로 이동 (마진 적용)
        /// </summary>
        /// <param name="waferEdgePosition1">스테이지1 웨이퍼 가장자리 위치</param>
        /// <param name="waferEdgePosition2">스테이지2 웨이퍼 가장자리 위치</param>
        /// <param name="marginUm">마진 (um)</param>
        public bool CenteringStagesMoveToWafer(double waferEdgePosition1, double waferEdgePosition2, double marginUm = 20)
        {
            // 마진을 pulse로 변환 (스펙에 따라 조정)
            double marginPulse = marginUm * 10; // 예: 1um = 10 pulse
            double targetPosition1 = waferEdgePosition1 - marginPulse;
            double targetPosition2 = waferEdgePosition2 - marginPulse;
            return CenteringStagesMoveSync(targetPosition1, targetPosition2);
        }

        /// <summary>
        /// 두 Centering Stage 동시 이동 (비동기)
        /// </summary>
        public async Task<bool> CenteringStagesMoveSyncAsync(double position1, double position2, double? velocity = null, int timeoutMs = 30000, CancellationToken ct = default)
        {
            // 이동 시작 (동시)
            bool start1 = CenteringStage1Move(position1, velocity);
            bool start2 = CenteringStage2Move(position2, velocity);

            if (!start1 || !start2)
                return false;

            // 완료 대기 (동시)
            var wait1 = WaitForDoneAsync(VAMotionAxis.CenteringStage_1, timeoutMs, ct);
            var wait2 = WaitForDoneAsync(VAMotionAxis.CenteringStage_2, timeoutMs, ct);

            await Task.WhenAll(wait1, wait2);

            return wait1.Result && wait2.Result;
        }

        #endregion

        #region All Axis Control

        /// <summary>
        /// 모든 축 정지
        /// </summary>
        public void StopAll()
        {
            Stop(VAMotionAxis.WedgeUpDown);
            Stop(VAMotionAxis.ChuckRotation);
            Stop(VAMotionAxis.CenteringStage_1);
            Stop(VAMotionAxis.CenteringStage_2);
        }

        /// <summary>
        /// 모든 축 비상 정지
        /// </summary>
        public void EmergencyStopAll()
        {
            EmergencyStop(VAMotionAxis.WedgeUpDown);
            EmergencyStop(VAMotionAxis.ChuckRotation);
            EmergencyStop(VAMotionAxis.CenteringStage_1);
            EmergencyStop(VAMotionAxis.CenteringStage_2);
        }

        /// <summary>
        /// 모든 축 홈
        /// </summary>
        public bool HomeAll()
        {
            bool result = true;
            result &= WedgeStageHome();
            result &= ChuckHome();
            result &= CenteringStage1Home();
            result &= CenteringStage2Home();
            return result;
        }

        /// <summary>
        /// 모든 축 모션 중인지 확인
        /// </summary>
        public bool IsAnyAxisMoving()
        {
            return IsMoving(VAMotionAxis.WedgeUpDown) ||
                   IsMoving(VAMotionAxis.ChuckRotation) ||
                   IsMoving(VAMotionAxis.CenteringStage_1) ||
                   IsMoving(VAMotionAxis.CenteringStage_2);
        }

        #endregion

        #region Status Summary

        /// <summary>
        /// 모션 상태 요약
        /// </summary>
        public MotionStatusSummary GetStatusSummary()
        {
            return new MotionStatusSummary
            {
                WedgeStagePosition = GetWedgeStagePosition(),
                WedgeStageMoving = IsMoving(VAMotionAxis.WedgeUpDown),
                ChuckAngle = GetChuckAngle(),
                ChuckMoving = IsMoving(VAMotionAxis.ChuckRotation),
                CenteringStage1Position = GetCenteringStage1Position(),
                CenteringStage1Moving = IsMoving(VAMotionAxis.CenteringStage_1),
                CenteringStage2Position = GetCenteringStage2Position(),
                CenteringStage2Moving = IsMoving(VAMotionAxis.CenteringStage_2)
            };
        }

        #endregion
    }

    /// <summary>
    /// 모션 상태 요약 정보
    /// </summary>
    public class MotionStatusSummary
    {
        public double WedgeStagePosition { get; set; }
        public bool WedgeStageMoving { get; set; }
        public double ChuckAngle { get; set; }
        public bool ChuckMoving { get; set; }
        public double CenteringStage1Position { get; set; }
        public bool CenteringStage1Moving { get; set; }
        public double CenteringStage2Position { get; set; }
        public bool CenteringStage2Moving { get; set; }
    }
}
