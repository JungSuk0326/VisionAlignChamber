using System;
using System.Threading;
using System.Threading.Tasks;
using VisionAlignChamber.Interfaces;

namespace VisionAlignChamber.Hardware.IO
{
    /// <summary>
    /// Vision Aligner 전용 Motion Facade
    /// 복잡한 축 번호 대신 의미있는 메서드로 모션 제어
    /// </summary>
    public class VisionAlignerMotion
    {
        #region Fields

        private readonly IMotionController _motion;
        private readonly IOMapping _mapping;

        #endregion

        #region Constructor

        public VisionAlignerMotion(IMotionController motionController, IOMapping mapping)
        {
            _motion = motionController ?? throw new ArgumentNullException(nameof(motionController));
            _mapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
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
            return _motion.MoveAbsolute(info.AxisNo, position, vel, info.DefaultAccel, info.DefaultDecel);
        }

        /// <summary>
        /// 상대 위치 이동
        /// </summary>
        public bool MoveRelative(VAMotionAxis axis, double distance, double? velocity = null)
        {
            var info = _mapping.GetAxisInfo(axis);
            double vel = velocity ?? info.DefaultVelocity;
            return _motion.MoveRelative(info.AxisNo, distance, vel, info.DefaultAccel, info.DefaultDecel);
        }

        /// <summary>
        /// 현재 위치 읽기
        /// </summary>
        public double GetPosition(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return _motion.GetPosition(info.AxisNo);
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
            return _motion.WaitForDone(info.AxisNo, timeoutMs);
        }

        /// <summary>
        /// 모션 중인지 확인
        /// </summary>
        public bool IsMoving(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return _motion.IsMoving(info.AxisNo);
        }

        /// <summary>
        /// 홈 위치로 이동
        /// </summary>
        public bool MoveHome(VAMotionAxis axis)
        {
            var info = _mapping.GetAxisInfo(axis);
            return _motion.MoveHome(info.AxisNo);
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

        #region Centering Stage

        /// <summary>
        /// Centering Stage 이동
        /// </summary>
        /// <param name="position">이동 위치 (pulse)</param>
        public bool CenteringStageMove(double position, double? velocity = null)
        {
            return MoveAbsolute(VAMotionAxis.CenteringStage, position, velocity);
        }

        /// <summary>
        /// Centering Stage 상대 이동
        /// </summary>
        public bool CenteringStageMoveRelative(double distance, double? velocity = null)
        {
            return MoveRelative(VAMotionAxis.CenteringStage, distance, velocity);
        }

        /// <summary>
        /// Centering Stage 현재 위치
        /// </summary>
        public double GetCenteringStagePosition()
        {
            return GetPosition(VAMotionAxis.CenteringStage);
        }

        /// <summary>
        /// Centering Stage 홈
        /// </summary>
        public bool CenteringStageHome()
        {
            return MoveHome(VAMotionAxis.CenteringStage);
        }

        /// <summary>
        /// Centering Stage Wafer 방향으로 이동 (마진 적용)
        /// </summary>
        /// <param name="waferEdgePosition">웨이퍼 가장자리 위치</param>
        /// <param name="marginUm">마진 (um)</param>
        public bool CenteringStageMoveToWafer(double waferEdgePosition, double marginUm = 20)
        {
            // 마진을 pulse로 변환 (스펙에 따라 조정)
            double marginPulse = marginUm * 10; // 예: 1um = 10 pulse
            double targetPosition = waferEdgePosition - marginPulse;
            return CenteringStageMove(targetPosition);
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
            Stop(VAMotionAxis.CenteringStage);
        }

        /// <summary>
        /// 모든 축 비상 정지
        /// </summary>
        public void EmergencyStopAll()
        {
            EmergencyStop(VAMotionAxis.WedgeUpDown);
            EmergencyStop(VAMotionAxis.ChuckRotation);
            EmergencyStop(VAMotionAxis.CenteringStage);
        }

        /// <summary>
        /// 모든 축 홈
        /// </summary>
        public bool HomeAll()
        {
            bool result = true;
            result &= WedgeStageHome();
            result &= ChuckHome();
            result &= CenteringStageHome();
            return result;
        }

        /// <summary>
        /// 모든 축 모션 중인지 확인
        /// </summary>
        public bool IsAnyAxisMoving()
        {
            return IsMoving(VAMotionAxis.WedgeUpDown) ||
                   IsMoving(VAMotionAxis.ChuckRotation) ||
                   IsMoving(VAMotionAxis.CenteringStage);
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
                CenteringStagePosition = GetCenteringStagePosition(),
                CenteringStageMoving = IsMoving(VAMotionAxis.CenteringStage)
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
        public double CenteringStagePosition { get; set; }
        public bool CenteringStageMoving { get; set; }
    }
}
