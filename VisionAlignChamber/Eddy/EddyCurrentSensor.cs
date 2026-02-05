using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using VisionAlignChamber.Interfaces;

namespace VisionAlignChamber.Eddy
{
    /// <summary>
    /// Eddy Current 센서 구현
    /// Modbus TCP 프로토콜을 사용하여 통신
    /// </summary>
    public class EddyCurrentSensor : IEddyCurrentSensor
    {
        #region Constants

        private const int CONNECTION_TIMEOUT_MS = 1000;
        private const int READ_WRITE_TIMEOUT_MS = 3000;
        private const ushort ZERO_COIL_ADDRESS = 9970;
        private const ushort DATA_START_ADDRESS = 1;
        private const ushort DATA_REGISTER_COUNT = 4;

        #endregion

        #region Fields

        private TcpClient _tcpClient;
        private NetworkStream _stream;
        private readonly object _lockObj = new object();
        private ushort _transactionId = 0;
        private bool _disposed = false;

        #endregion

        #region Properties

        /// <summary>
        /// 연결 상태
        /// </summary>
        public bool IsConnected => _tcpClient?.Connected ?? false;

        #endregion

        #region Connection

        /// <summary>
        /// 센서 연결
        /// </summary>
        public bool Connect(string ip, int port = 502)
        {
            try
            {
                if (IsConnected)
                {
                    Disconnect();
                }

                _tcpClient = new TcpClient();

                Task connectTask = _tcpClient.ConnectAsync(ip, port);

                if (connectTask.Wait(CONNECTION_TIMEOUT_MS))
                {
                    _stream = _tcpClient.GetStream();
                    _stream.ReadTimeout = READ_WRITE_TIMEOUT_MS;
                    _stream.WriteTimeout = READ_WRITE_TIMEOUT_MS;

                    System.Diagnostics.Debug.WriteLine($"EddyCurrentSensor: Connected to {ip}:{port}");
                    return true;
                }
                else
                {
                    _tcpClient.Close();
                    System.Diagnostics.Debug.WriteLine("EddyCurrentSensor: Connection timeout");
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EddyCurrentSensor: Connection failed - {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 연결 해제
        /// </summary>
        public void Disconnect()
        {
            try
            {
                _stream?.Close();
                _stream = null;
                _tcpClient?.Close();
                _tcpClient = null;
                System.Diagnostics.Debug.WriteLine("EddyCurrentSensor: Disconnected");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EddyCurrentSensor: Disconnect error - {ex.Message}");
            }
        }

        #endregion

        #region Measurement

        /// <summary>
        /// 영점 설정
        /// </summary>
        public bool SetZero()
        {
            try
            {
                return WriteSingleCoil(1, ZERO_COIL_ADDRESS, true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EddyCurrentSensor: SetZero error - {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 측정값 읽기
        /// </summary>
        public double GetData()
        {
            try
            {
                var readValue = ReadInputRegisters(1, DATA_START_ADDRESS, DATA_REGISTER_COUNT);

                if (readValue == null || readValue.Length < DATA_REGISTER_COUNT)
                {
                    return 0;
                }

                // 4개의 ushort를 8바이트 배열로 변환
                byte[] bytes = new byte[8];
                for (int i = 0; i < readValue.Length; i++)
                {
                    byte[] temp = BitConverter.GetBytes(readValue[i]).Reverse().ToArray();
                    Array.Copy(temp, 0, bytes, i * 2, 2);
                }

                // ASCII 문자열로 변환 후 double 파싱
                string sValue = Encoding.ASCII.GetString(bytes).Trim();

                if (double.TryParse(sValue, out double result))
                {
                    return result;
                }

                System.Diagnostics.Debug.WriteLine($"EddyCurrentSensor: Failed to parse value - {sValue}");
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EddyCurrentSensor: GetData error - {ex.Message}");
                return 0;
            }
        }

        #endregion

        #region Modbus TCP Implementation

        /// <summary>
        /// Input Register 읽기 (Function Code 04)
        /// </summary>
        private ushort[] ReadInputRegisters(byte unitId, ushort startAddress, ushort count)
        {
            try
            {
                byte[] pdu = new byte[5];
                pdu[0] = 0x04; // Function Code
                pdu[1] = (byte)(startAddress >> 8);
                pdu[2] = (byte)(startAddress & 0xFF);
                pdu[3] = (byte)(count >> 8);
                pdu[4] = (byte)(count & 0xFF);

                byte[] header = CreateMBAPHeader(unitId, (ushort)(pdu.Length + 1));
                byte[] request = new byte[header.Length + pdu.Length];
                Array.Copy(header, 0, request, 0, header.Length);
                Array.Copy(pdu, 0, request, header.Length, pdu.Length);

                byte[] response = SendRequest(request);
                if (response == null || response.Length < 9)
                {
                    return null;
                }

                byte functionCode = response[7];
                if (functionCode == 0x84) // Error response
                {
                    byte errorCode = response[8];
                    System.Diagnostics.Debug.WriteLine($"Modbus Error: 0x{errorCode:X2}");
                    return null;
                }

                ushort[] registers = new ushort[count];
                for (int i = 0; i < count; i++)
                {
                    int offset = 9 + (i * 2);
                    registers[i] = (ushort)((response[offset] << 8) | response[offset + 1]);
                }

                return registers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ReadInputRegisters error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Single Coil 쓰기 (Function Code 05)
        /// </summary>
        private bool WriteSingleCoil(byte unitId, ushort coilAddress, bool value)
        {
            try
            {
                byte[] pdu = new byte[5];
                pdu[0] = 0x05; // Function Code
                pdu[1] = (byte)(coilAddress >> 8);
                pdu[2] = (byte)(coilAddress & 0xFF);
                pdu[3] = value ? (byte)0xFF : (byte)0x00;
                pdu[4] = 0x00;

                byte[] header = CreateMBAPHeader(unitId, (ushort)(pdu.Length + 1));
                byte[] request = new byte[header.Length + pdu.Length];
                Array.Copy(header, 0, request, 0, header.Length);
                Array.Copy(pdu, 0, request, header.Length, pdu.Length);

                byte[] response = SendRequest(request);
                if (response == null || response.Length < 12)
                {
                    return false;
                }

                byte functionCode = response[7];
                if (functionCode == 0x85) // Error response
                {
                    byte errorCode = response[8];
                    System.Diagnostics.Debug.WriteLine($"Modbus Error: 0x{errorCode:X2}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WriteSingleCoil error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// MBAP 헤더 생성
        /// </summary>
        private byte[] CreateMBAPHeader(byte unitId, ushort dataLength)
        {
            ushort transId = _transactionId++;

            return new byte[]
            {
                (byte)(transId >> 8),      // Transaction ID High
                (byte)(transId & 0xFF),    // Transaction ID Low
                0x00,                       // Protocol ID High
                0x00,                       // Protocol ID Low
                (byte)(dataLength >> 8),   // Length High
                (byte)(dataLength & 0xFF), // Length Low
                unitId                      // Unit ID
            };
        }

        /// <summary>
        /// Modbus TCP 요청 전송 및 응답 수신
        /// </summary>
        private byte[] SendRequest(byte[] request)
        {
            lock (_lockObj)
            {
                try
                {
                    if (_stream == null || !IsConnected)
                    {
                        return null;
                    }

                    // 요청 전송
                    _stream.Write(request, 0, request.Length);
                    _stream.Flush();

                    // MBAP 헤더 읽기 (7바이트)
                    byte[] header = new byte[7];
                    int headerRead = _stream.Read(header, 0, 7);
                    if (headerRead != 7)
                    {
                        throw new Exception("Failed to read MBAP header");
                    }

                    // 데이터 길이 파싱 (바이트 4-5)
                    int dataLength = (header[4] << 8) | header[5];

                    // 나머지 데이터 읽기
                    byte[] data = new byte[dataLength - 1]; // UnitID는 이미 읽음
                    int dataRead = _stream.Read(data, 0, data.Length);
                    if (dataRead != data.Length)
                    {
                        throw new Exception("Failed to read response data");
                    }

                    // 전체 응답 조합
                    byte[] response = new byte[7 + data.Length];
                    Array.Copy(header, 0, response, 0, 7);
                    Array.Copy(data, 0, response, 7, data.Length);

                    return response;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Communication error: {ex.Message}");
                    return null;
                }
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
                    Disconnect();
                }
                _disposed = true;
            }
        }

        ~EddyCurrentSensor()
        {
            Dispose(false);
        }

        #endregion
    }
}
