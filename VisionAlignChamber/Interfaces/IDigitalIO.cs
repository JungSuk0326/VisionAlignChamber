using System;

namespace VisionAlignChamber.Interfaces
{
    /// <summary>
    /// 디지털 IO 인터페이스
    /// DI/DO 제어를 위한 추상화 계층
    /// </summary>
    public interface IDigitalIO : IDisposable
    {
        #region 초기화

        /// <summary>
        /// 디지털 IO 초기화
        /// </summary>
        /// <returns>성공 여부</returns>
        bool Initialize();

        /// <summary>
        /// 디지털 IO 종료
        /// </summary>
        void Close();

        /// <summary>
        /// 초기화 상태
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// IO 모듈 개수
        /// </summary>
        int ModuleCount { get; }

        #endregion

        #region 입력 (DI)

        /// <summary>
        /// 입력 비트 읽기
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="bitNo">비트 번호 (0~31)</param>
        /// <returns>입력 상태 (true: ON, false: OFF)</returns>
        bool ReadInputBit(int moduleNo, int bitNo);

        /// <summary>
        /// 입력 바이트 읽기 (8비트)
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="offset">오프셋</param>
        /// <returns>입력값</returns>
        uint ReadInputByte(int moduleNo, int offset);

        /// <summary>
        /// 입력 워드 읽기 (16비트)
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="offset">오프셋</param>
        /// <returns>입력값</returns>
        uint ReadInputWord(int moduleNo, int offset);

        /// <summary>
        /// 입력 더블워드 읽기 (32비트)
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="offset">오프셋</param>
        /// <returns>입력값</returns>
        uint ReadInputDWord(int moduleNo, int offset);

        #endregion

        #region 출력 (DO)

        /// <summary>
        /// 출력 비트 쓰기
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="bitNo">비트 번호 (0~31)</param>
        /// <param name="value">출력값 (true: ON, false: OFF)</param>
        /// <returns>성공 여부</returns>
        bool WriteOutputBit(int moduleNo, int bitNo, bool value);

        /// <summary>
        /// 출력 바이트 쓰기 (8비트)
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="offset">오프셋</param>
        /// <param name="value">출력값</param>
        /// <returns>성공 여부</returns>
        bool WriteOutputByte(int moduleNo, int offset, uint value);

        /// <summary>
        /// 출력 워드 쓰기 (16비트)
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="offset">오프셋</param>
        /// <param name="value">출력값</param>
        /// <returns>성공 여부</returns>
        bool WriteOutputWord(int moduleNo, int offset, uint value);

        /// <summary>
        /// 출력 더블워드 쓰기 (32비트)
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="offset">오프셋</param>
        /// <param name="value">출력값</param>
        /// <returns>성공 여부</returns>
        bool WriteOutputDWord(int moduleNo, int offset, uint value);

        #endregion

        #region 출력 상태 읽기

        /// <summary>
        /// 출력 비트 상태 읽기
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="bitNo">비트 번호</param>
        /// <returns>출력 상태</returns>
        bool ReadOutputBit(int moduleNo, int bitNo);

        /// <summary>
        /// 출력 바이트 상태 읽기 (8비트)
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="offset">오프셋</param>
        /// <returns>출력값</returns>
        uint ReadOutputByte(int moduleNo, int offset);

        /// <summary>
        /// 출력 워드 상태 읽기 (16비트)
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="offset">오프셋</param>
        /// <returns>출력값</returns>
        uint ReadOutputWord(int moduleNo, int offset);

        /// <summary>
        /// 출력 더블워드 상태 읽기 (32비트)
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="offset">오프셋</param>
        /// <returns>출력값</returns>
        uint ReadOutputDWord(int moduleNo, int offset);

        #endregion

        #region 유틸리티

        /// <summary>
        /// 출력 토글
        /// </summary>
        /// <param name="moduleNo">모듈 번호</param>
        /// <param name="bitNo">비트 번호</param>
        /// <returns>성공 여부</returns>
        bool ToggleOutput(int moduleNo, int bitNo);

        /// <summary>
        /// 모든 출력 OFF
        /// </summary>
        /// <returns>성공 여부</returns>
        bool ClearAllOutputs();

        #endregion
    }
}
