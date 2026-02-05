using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EddyDemo
{

    public class eddyCurrent
    {
        ushort _transactionId = 0;

        private TcpClient _tcpClient;
        private NetworkStream _stream;

        private object _lockObj;

        public eddyCurrent()
        {
            _lockObj = new object();
        }

        public bool Connect(string ip, int port)
        {
            try
            {
                _tcpClient = new TcpClient();

                Task connectTask = _tcpClient.ConnectAsync(ip, port);

                if (connectTask.Wait(1000))
                {
                    // 연결 성공
                    _stream = _tcpClient.GetStream();
                    _stream.ReadTimeout = 3000;
                    _stream.WriteTimeout = 3000;

                    Console.WriteLine($"Connected to {ip}:{port}");
                  
                }
                else
                {
                    // 타임아웃
                  
                    _tcpClient.Close();
                    Console.WriteLine("연결 타임아웃");
                    return false;
                }



           //     _tcpClient.Connect(ip, port);
              
             
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                _stream?.Close();
                _tcpClient?.Close();
                Console.WriteLine("Disconnected");
                _tcpClient = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Disconnect error: {ex.Message}");
            }
        }



        public bool  SetZero()
        {
            try
            {
                // return WriteSingleCoil(1, 00001, true);

                return WriteSingleCoil(1, 9970, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Disconnect error: {ex.Message}");
              
            }

            return false;
        }



        public double GetData()
        {
            double  getData = 0;
            try
            {
                //read
                var readValue = ReadInputRegisters(1, 1, 4);

                if (readValue != null)
                {
                    byte[] bytes = new byte[8];
                    string sValue = "";
                    // 각 ushort를 2바이트씩 byte 배열로 변환
                    for (int i = 0; i < readValue.Length; i++)
                    {
                        byte[] temp = BitConverter.GetBytes(readValue[i]).Reverse().ToArray();

                        Array.Copy(temp, 0, bytes, i * 2, 2);
                    }

                    sValue = Encoding.ASCII.GetString(bytes);

                    getData = double.Parse(sValue);



                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Disconnect error: {ex.Message}");
            }

            return getData;
        }

        // ====================================================================================================================================
        //                               아래는   내부 기능 개발 
        // ====================================================================================================================================



        /// <summary>
        /// Input Register 읽기 (Function Code 04)
        /// </summary>
        public ushort[] ReadInputRegisters(byte unitId, ushort startAddress, ushort count)
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
                    Console.WriteLine("Invalid response");
                    return null;
                }

                byte functionCode = response[7];
                if (functionCode == 0x84)
                {
                    byte errorCode = response[8];
                    Console.WriteLine($"Modbus Error: 0x{errorCode:X2}");
                    return null;
                }

                byte byteCount = response[8];
                ushort[] registers = new ushort[count];

                for (int i = 0; i < count; i++)
                {
                    int offset = 9 + (i * 2);
                    registers[i] = (ushort)((response[offset] << 8) | response[offset + 1]);
                }

                Console.WriteLine($"Read IR: Unit={unitId}, Addr={startAddress}, Count={count}");
                for (int i = 0; i < registers.Length; i++)
                {
                    Console.WriteLine($"  Register[{startAddress + i}] = {registers[i]}");
                }

                return registers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Read error: {ex.Message}");
                return null;
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
                    Console.WriteLine($"Communication error: {ex.Message}");
                    return null;
                }
            }
        }

        /// Single Coil 쓰기 (Function Code 05)
        /// </summary>
        public bool WriteSingleCoil(byte unitId, ushort coilAddress, bool value)
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
                    Console.WriteLine("Invalid response");
                    return false;
                }

                byte functionCode = response[7];
                if (functionCode == 0x85)
                {
                    byte errorCode = response[8];
                    Console.WriteLine($"Modbus Error: 0x{errorCode:X2}");
                    return false;
                }

                Console.WriteLine($"Write Single Coil: Unit={unitId}, Addr={coilAddress}, Value={value}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Write error: {ex.Message}");
                return false;
            }
        }




    }
}
