using System;
using System.Collections.Generic;
using VisionAlignChamber.Interfaces;

namespace VisionAlignChamber.Hardware.Simulation
{
    /// <summary>
    /// 시뮬레이션용 디지털 IO
    /// 실제 하드웨어 없이 UI 테스트용
    /// </summary>
    public class SimulationDigitalIO : IDigitalIO
    {
        #region Fields

        private bool _isInitialized = false;
        private bool _disposed = false;
        private int _moduleCount = 1;

        // 입력/출력 상태 저장 (모듈번호, 비트번호) -> 값
        private Dictionary<(int module, int bit), bool> _inputBits = new Dictionary<(int, int), bool>();
        private Dictionary<(int module, int bit), bool> _outputBits = new Dictionary<(int, int), bool>();

        #endregion

        #region Properties

        public bool IsInitialized => _isInitialized;
        public int ModuleCount => _moduleCount;

        #endregion

        #region 초기화

        public bool Initialize()
        {
            if (_isInitialized)
                return true;

            // 시뮬레이션 초기화 - 모든 비트 OFF
            _inputBits.Clear();
            _outputBits.Clear();

            _isInitialized = true;
            System.Diagnostics.Debug.WriteLine("[Simulation] DigitalIO initialized");
            return true;
        }

        public void Close()
        {
            if (!_isInitialized)
                return;

            _isInitialized = false;
            System.Diagnostics.Debug.WriteLine("[Simulation] DigitalIO closed");
        }

        #endregion

        #region 입력 (DI)

        public bool ReadInputBit(int moduleNo, int bitNo)
        {
            if (!_isInitialized) return false;

            var key = (moduleNo, bitNo);
            return _inputBits.ContainsKey(key) && _inputBits[key];
        }

        public uint ReadInputByte(int moduleNo, int offset)
        {
            if (!_isInitialized) return 0;

            uint value = 0;
            for (int i = 0; i < 8; i++)
            {
                if (ReadInputBit(moduleNo, offset * 8 + i))
                {
                    value |= (uint)(1 << i);
                }
            }
            return value;
        }

        public uint ReadInputWord(int moduleNo, int offset)
        {
            if (!_isInitialized) return 0;

            uint value = 0;
            for (int i = 0; i < 16; i++)
            {
                if (ReadInputBit(moduleNo, offset * 16 + i))
                {
                    value |= (uint)(1 << i);
                }
            }
            return value;
        }

        public uint ReadInputDWord(int moduleNo, int offset)
        {
            if (!_isInitialized) return 0;

            uint value = 0;
            for (int i = 0; i < 32; i++)
            {
                if (ReadInputBit(moduleNo, offset * 32 + i))
                {
                    value |= (uint)(1 << i);
                }
            }
            return value;
        }

        /// <summary>
        /// 시뮬레이션용: 입력 비트 설정 (테스트용)
        /// </summary>
        public void SetInputBit(int moduleNo, int bitNo, bool value)
        {
            _inputBits[(moduleNo, bitNo)] = value;
            System.Diagnostics.Debug.WriteLine($"[Simulation] DI[{moduleNo},{bitNo}] = {value}");
        }

        #endregion

        #region 출력 (DO)

        public bool WriteOutputBit(int moduleNo, int bitNo, bool value)
        {
            if (!_isInitialized) return false;

            _outputBits[(moduleNo, bitNo)] = value;
            System.Diagnostics.Debug.WriteLine($"[Simulation] DO[{moduleNo},{bitNo}] = {value}");
            return true;
        }

        public bool WriteOutputByte(int moduleNo, int offset, uint value)
        {
            if (!_isInitialized) return false;

            for (int i = 0; i < 8; i++)
            {
                bool bitValue = (value & (1 << i)) != 0;
                _outputBits[(moduleNo, offset * 8 + i)] = bitValue;
            }
            return true;
        }

        public bool WriteOutputWord(int moduleNo, int offset, uint value)
        {
            if (!_isInitialized) return false;

            for (int i = 0; i < 16; i++)
            {
                bool bitValue = (value & (1 << i)) != 0;
                _outputBits[(moduleNo, offset * 16 + i)] = bitValue;
            }
            return true;
        }

        public bool WriteOutputDWord(int moduleNo, int offset, uint value)
        {
            if (!_isInitialized) return false;

            for (int i = 0; i < 32; i++)
            {
                bool bitValue = (value & (1 << i)) != 0;
                _outputBits[(moduleNo, offset * 32 + i)] = bitValue;
            }
            return true;
        }

        #endregion

        #region 출력 상태 읽기

        public bool ReadOutputBit(int moduleNo, int bitNo)
        {
            if (!_isInitialized) return false;

            var key = (moduleNo, bitNo);
            return _outputBits.ContainsKey(key) && _outputBits[key];
        }

        public uint ReadOutputByte(int moduleNo, int offset)
        {
            if (!_isInitialized) return 0;

            uint value = 0;
            for (int i = 0; i < 8; i++)
            {
                if (ReadOutputBit(moduleNo, offset * 8 + i))
                {
                    value |= (uint)(1 << i);
                }
            }
            return value;
        }

        public uint ReadOutputWord(int moduleNo, int offset)
        {
            if (!_isInitialized) return 0;

            uint value = 0;
            for (int i = 0; i < 16; i++)
            {
                if (ReadOutputBit(moduleNo, offset * 16 + i))
                {
                    value |= (uint)(1 << i);
                }
            }
            return value;
        }

        public uint ReadOutputDWord(int moduleNo, int offset)
        {
            if (!_isInitialized) return 0;

            uint value = 0;
            for (int i = 0; i < 32; i++)
            {
                if (ReadOutputBit(moduleNo, offset * 32 + i))
                {
                    value |= (uint)(1 << i);
                }
            }
            return value;
        }

        #endregion

        #region 유틸리티

        public bool ToggleOutput(int moduleNo, int bitNo)
        {
            if (!_isInitialized) return false;

            bool current = ReadOutputBit(moduleNo, bitNo);
            return WriteOutputBit(moduleNo, bitNo, !current);
        }

        public bool ClearAllOutputs()
        {
            if (!_isInitialized) return false;

            _outputBits.Clear();
            System.Diagnostics.Debug.WriteLine("[Simulation] All outputs cleared");
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

        ~SimulationDigitalIO()
        {
            Dispose(false);
        }

        #endregion
    }
}
