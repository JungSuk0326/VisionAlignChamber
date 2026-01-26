using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Sensor.Delcom
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

        private void _Client_OnDisConnected()
        {

        }

        private void _Client_OnConnected()
        {

        }

        public bool Connect(string ip, int port)
        {
            try
            {
                _tcpClient = new TcpClient();
                _tcpClient.Connect(ip, port);
                _stream = _tcpClient.GetStream();
                _stream.ReadTimeout = 3000;
                _stream.WriteTimeout = 3000;
                Console.WriteLine($"Connected to {ip}:{port}");
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Disconnect error: {ex.Message}");
            }
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
        /// Coil 읽기 (Function Code 01)
        /// </summary>
        public bool[] ReadCoils(byte unitId, ushort startAddress, ushort count)
        {
            try
            {
                byte[] pdu = new byte[5];
                pdu[0] = 0x01; // Function Code
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
                if (functionCode == 0x81)
                {
                    byte errorCode = response[8];
                    Console.WriteLine($"Modbus Error: 0x{errorCode:X2}");
                    return null;
                }

                byte byteCount = response[8];
                bool[] coils = new bool[count];

                for (int i = 0; i < count; i++)
                {
                    int byteIndex = 9 + (i / 8);
                    int bitIndex = i % 8;
                    coils[i] = ((response[byteIndex] >> bitIndex) & 0x01) == 1;
                }

                Console.WriteLine($"Read Coils: Unit={unitId}, Addr={startAddress}, Count={count}");
                for (int i = 0; i < coils.Length; i++)
                {
                    Console.WriteLine($"  Coil[{startAddress + i}] = {coils[i]}");
                }

                return coils;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Read error: {ex.Message}");
                return null;
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

        /// <summary>
        /// Multiple Coils 쓰기 (Function Code 15)
        /// </summary>
        public bool WriteMultipleCoils(byte unitId, ushort startAddress, bool[] values)
        {
            try
            {
                int byteCount = (values.Length + 7) / 8;
                byte[] pdu = new byte[6 + byteCount];
                pdu[0] = 0x0F; // Function Code
                pdu[1] = (byte)(startAddress >> 8);
                pdu[2] = (byte)(startAddress & 0xFF);
                pdu[3] = (byte)(values.Length >> 8);
                pdu[4] = (byte)(values.Length & 0xFF);
                pdu[5] = (byte)byteCount;

                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i])
                    {
                        int byteIndex = 6 + (i / 8);
                        int bitIndex = i % 8;
                        pdu[byteIndex] |= (byte)(1 << bitIndex);
                    }
                }

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
                if (functionCode == 0x8F)
                {
                    byte errorCode = response[8];
                    Console.WriteLine($"Modbus Error: 0x{errorCode:X2}");
                    return false;
                }

                Console.WriteLine($"Write Multiple Coils: Unit={unitId}, Addr={startAddress}, Count={values.Length}");
                for (int i = 0; i < values.Length; i++)
                {
                    Console.WriteLine($"  Coil[{startAddress + i}] = {values[i]}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Write error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Multiple Registers 쓰기 (Function Code 16)
        /// </summary>
        public bool WriteMultipleRegisters(byte unitId, ushort startAddress, ushort[] values)
        {
            try
            {
                byte byteCount = (byte)(values.Length * 2);
                byte[] pdu = new byte[6 + byteCount];
                pdu[0] = 0x10; // Function Code
                pdu[1] = (byte)(startAddress >> 8);
                pdu[2] = (byte)(startAddress & 0xFF);
                pdu[3] = (byte)(values.Length >> 8);
                pdu[4] = (byte)(values.Length & 0xFF);
                pdu[5] = byteCount;

                for (int i = 0; i < values.Length; i++)
                {
                    pdu[6 + (i * 2)] = (byte)(values[i] >> 8);
                    pdu[7 + (i * 2)] = (byte)(values[i] & 0xFF);
                }

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
                if (functionCode == 0x90)
                {
                    byte errorCode = response[8];
                    Console.WriteLine($"Modbus Error: 0x{errorCode:X2}");
                    return false;
                }

                Console.WriteLine($"Write Multiple: Unit={unitId}, Addr={startAddress}, Count={values.Length}");
                for (int i = 0; i < values.Length; i++)
                {
                    Console.WriteLine($"  Register[{startAddress + i}] = {values[i]}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Write error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Single Register 쓰기 (Function Code 06)
        /// </summary>
        public bool WriteSingleRegister(byte unitId, ushort registerAddress, ushort value)
        {
            try
            {
                byte[] pdu = new byte[5];
                pdu[0] = 0x06; // Function Code
                pdu[1] = (byte)(registerAddress >> 8);
                pdu[2] = (byte)(registerAddress & 0xFF);
                pdu[3] = (byte)(value >> 8);
                pdu[4] = (byte)(value & 0xFF);

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
                if (functionCode == 0x86)
                {
                    byte errorCode = response[8];
                    Console.WriteLine($"Modbus Error: 0x{errorCode:X2}");
                    return false;
                }

                Console.WriteLine($"Write Single: Unit={unitId}, Addr={registerAddress}, Value={value}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Write error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Holding Register 읽기 (Function Code 03)
        /// </summary>
        public ushort[] ReadHoldingRegisters(byte unitId, ushort startAddress, ushort count)
        {
            try
            {
                // PDU 생성 (Function Code + Start Address + Count)
                byte[] pdu = new byte[5];
                pdu[0] = 0x03; // Function Code
                pdu[1] = (byte)(startAddress >> 8);
                pdu[2] = (byte)(startAddress & 0xFF);
                pdu[3] = (byte)(count >> 8);
                pdu[4] = (byte)(count & 0xFF);

                // MBAP 헤더 생성
                byte[] header = CreateMBAPHeader(unitId, (ushort)(pdu.Length + 1));

                // 전체 요청 패킷 생성
                byte[] request = new byte[header.Length + pdu.Length];
                Array.Copy(header, 0, request, 0, header.Length);
                Array.Copy(pdu, 0, request, header.Length, pdu.Length);

                // 요청 전송 및 응답 수신
                byte[] response = SendRequest(request);
                if (response == null || response.Length < 9)
                {
                    Console.WriteLine("Invalid response");
                    return null;
                }

                // 응답 파싱
                byte functionCode = response[7];
                if (functionCode == 0x83) // Error
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

                Console.WriteLine($"Read HR: Unit={unitId}, Addr={startAddress}, Count={count}");
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


    }
}
