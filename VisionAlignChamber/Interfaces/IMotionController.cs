using System;

namespace VisionAlignChamber.Interfaces
{
    /// <summary>
    /// 모션 컨트롤러 인터페이스
    /// 다축 모션 제어를 위한 추상화 계층
    /// </summary>
    public interface IMotionController : IDisposable
    {
        #region 초기화

        /// <summary>
        /// 모션 컨트롤러 초기화
        /// </summary>
        /// <returns>성공 여부</returns>
        bool Initialize();

        /// <summary>
        /// 모션 컨트롤러 종료
        /// </summary>
        void Close();

        /// <summary>
        /// 초기화 상태
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// 사용 가능한 축 개수
        /// </summary>
        int AxisCount { get; }

        #endregion

        #region 서보 제어

        /// <summary>
        /// 서보 ON
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <returns>성공 여부</returns>
        bool ServoOn(int axisNo);

        /// <summary>
        /// 서보 OFF
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <returns>성공 여부</returns>
        bool ServoOff(int axisNo);

        /// <summary>
        /// 서보 ON 상태 확인
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <returns>서보 ON 상태</returns>
        bool IsServoOn(int axisNo);

        #endregion

        #region 알람

        /// <summary>
        /// 알람 상태 확인
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <returns>알람 발생 여부</returns>
        bool IsAlarm(int axisNo);

        /// <summary>
        /// 알람 클리어
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <returns>성공 여부</returns>
        bool ClearAlarm(int axisNo);

        #endregion

        #region 위치

        /// <summary>
        /// 실제 위치 읽기 (엔코더 피드백)
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <returns>실제 위치</returns>
        double GetActualPosition(int axisNo);

        /// <summary>
        /// 명령 위치 읽기
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <returns>명령 위치</returns>
        double GetCommandPosition(int axisNo);

        /// <summary>
        /// 실제 위치 설정 (원점 설정 등)
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <param name="position">설정할 위치값</param>
        /// <returns>성공 여부</returns>
        bool SetActualPosition(int axisNo, double position);

        #endregion

        #region 단축 이동

        /// <summary>
        /// 절대 위치 이동
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <param name="position">목표 위치</param>
        /// <param name="velocity">속도</param>
        /// <param name="accel">가속도</param>
        /// <param name="decel">감속도</param>
        /// <returns>성공 여부</returns>
        bool MoveAbs(int axisNo, double position, double velocity, double accel, double decel);

        /// <summary>
        /// 상대 위치 이동
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <param name="distance">이동 거리</param>
        /// <param name="velocity">속도</param>
        /// <param name="accel">가속도</param>
        /// <param name="decel">감속도</param>
        /// <returns>성공 여부</returns>
        bool MoveRel(int axisNo, double distance, double velocity, double accel, double decel);

        /// <summary>
        /// 속도 이동 (JOG)
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <param name="velocity">속도 (양수: 정방향, 음수: 역방향)</param>
        /// <param name="accel">가속도</param>
        /// <param name="decel">감속도</param>
        /// <returns>성공 여부</returns>
        bool MoveVelocity(int axisNo, double velocity, double accel, double decel);

        #endregion

        #region 정지

        /// <summary>
        /// 감속 정지
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <returns>성공 여부</returns>
        bool Stop(int axisNo);

        /// <summary>
        /// 비상 정지 (즉시 정지)
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <returns>성공 여부</returns>
        bool EmergencyStop(int axisNo);

        /// <summary>
        /// 이동 완료 확인
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <returns>이동 완료 여부</returns>
        bool IsMotionDone(int axisNo);

        #endregion

        #region 원점 복귀

        /// <summary>
        /// 원점 복귀 시작
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <returns>성공 여부</returns>
        bool HomeMove(int axisNo);

        /// <summary>
        /// 원점 복귀 완료 확인
        /// </summary>
        /// <param name="axisNo">축 번호</param>
        /// <returns>원점 복귀 완료 여부</returns>
        bool IsHomeDone(int axisNo);

        #endregion

        #region 다축 동기 이동

        /// <summary>
        /// 다축 절대 위치 동기 이동
        /// </summary>
        /// <param name="axisNos">축 번호 배열</param>
        /// <param name="positions">목표 위치 배열</param>
        /// <param name="velocities">속도 배열</param>
        /// <param name="accels">가속도 배열</param>
        /// <param name="decels">감속도 배열</param>
        /// <returns>성공 여부</returns>
        bool MoveAbsMulti(int[] axisNos, double[] positions, double[] velocities, double[] accels, double[] decels);

        /// <summary>
        /// 모든 축 비상 정지
        /// </summary>
        /// <returns>성공 여부</returns>
        bool EmergencyStopAll();

        #endregion
    }
}
