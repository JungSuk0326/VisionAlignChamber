using System;

namespace VisionAlignChamber.Interfaces
{
    /// <summary>
    /// Eddy Current 센서 인터페이스
    /// Modbus TCP를 통해 거리/갭 측정
    /// </summary>
    public interface IEddyCurrentSensor : IDisposable
    {
        #region Connection

        /// <summary>
        /// 연결 상태
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 센서 연결
        /// </summary>
        /// <param name="ip">IP 주소</param>
        /// <param name="port">포트 번호 (기본 502)</param>
        /// <returns>연결 성공 여부</returns>
        bool Connect(string ip, int port = 502);

        /// <summary>
        /// 연결 해제
        /// </summary>
        void Disconnect();

        #endregion

        #region Measurement

        /// <summary>
        /// 영점 설정
        /// </summary>
        /// <returns>성공 여부</returns>
        bool SetZero();

        /// <summary>
        /// 측정값 읽기
        /// </summary>
        /// <returns>측정값 (단위: 센서 설정에 따름)</returns>
        double GetData();

        #endregion
    }
}
