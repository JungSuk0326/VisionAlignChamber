using System;
using VisionAlignChamber.Interfaces;
using VisionAlignChamber.Hardware.Common;

namespace VisionAlignChamber.Hardware.Ajin
{
    /// <summary>
    /// 아진 EtherCAT 디지털 IO 구현
    /// </summary>
    public class AjinDigitalIO : IDigitalIO
    {
        #region Fields

        private bool _isInitialized = false;
        private bool _disposed = false;
        private int _moduleCount = 0;

        #endregion

        #region Properties

        public bool IsInitialized => _isInitialized;
        public int ModuleCount => _moduleCount;

        #endregion

        #region 초기화

        /// <summary>
        /// 디지털 IO 초기화
        /// </summary>
        public bool Initialize()
        {
            if (_isInitialized)
                return true;

            try
            {
                // 라이브러리 열기 확인 (이미 열려 있을 수 있음)
                if (CAXL.AxlIsOpened() == 0)
                {
                    uint result = CAXL.AxlOpen(7);
                    if (result != AjinErrorCode.AXT_RT_SUCCESS && result != AjinErrorCode.AXT_RT_OPEN_ALREADY)
                    {
                        throw new MotionException(result, $"AxlOpen failed: {AjinErrorCode.GetErrorMessage(result)}");
                    }
                }

                // DIO 모듈 존재 확인
                uint isDIO = 0;
                uint checkResult = CAXD.AxdInfoIsDIOModule(ref isDIO);
                if (checkResult != AjinErrorCode.AXT_RT_SUCCESS || isDIO == 0)
                {
                    throw new MotionException("DIO module not found");
                }

                // 모듈 개수 확인
                checkResult = CAXD.AxdInfoGetModuleCount(ref _moduleCount);
                if (checkResult != AjinErrorCode.AXT_RT_SUCCESS)
                {
                    throw new MotionException(checkResult, $"AxdInfoGetModuleCount failed: {AjinErrorCode.GetErrorMessage(checkResult)}");
                }

                _isInitialized = true;
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AjinDigitalIO Initialize Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 디지털 IO 종료
        /// </summary>
        public void Close()
        {
            if (!_isInitialized)
                return;

            try
            {
                // 모든 출력 OFF
                ClearAllOutputs();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AjinDigitalIO Close Error: {ex.Message}");
            }
            finally
            {
                _isInitialized = false;
            }
        }

        #endregion

        #region 입력 (DI)

        /// <summary>
        /// 입력 비트 읽기
        /// </summary>
        public bool ReadInputBit(int moduleNo, int bitNo)
        {
            if (!CheckInitialized()) return false;
            if (!CheckModuleNo(moduleNo)) return false;

            uint value = 0;
            uint result = CAXD.AxdiReadInportBit(moduleNo, bitNo, ref value);
            return AjinErrorCode.IsSuccess(result) && value != 0;
        }

        /// <summary>
        /// 입력 바이트 읽기 (8비트)
        /// </summary>
        public uint ReadInputByte(int moduleNo, int offset)
        {
            if (!CheckInitialized()) return 0;
            if (!CheckModuleNo(moduleNo)) return 0;

            uint value = 0;
            uint result = CAXD.AxdiReadInportByte(moduleNo, offset, ref value);
            return AjinErrorCode.IsSuccess(result) ? value : 0;
        }

        /// <summary>
        /// 입력 워드 읽기 (16비트)
        /// </summary>
        public uint ReadInputWord(int moduleNo, int offset)
        {
            if (!CheckInitialized()) return 0;
            if (!CheckModuleNo(moduleNo)) return 0;

            uint value = 0;
            uint result = CAXD.AxdiReadInportWord(moduleNo, offset, ref value);
            return AjinErrorCode.IsSuccess(result) ? value : 0;
        }

        /// <summary>
        /// 입력 더블워드 읽기 (32비트)
        /// </summary>
        public uint ReadInputDWord(int moduleNo, int offset)
        {
            if (!CheckInitialized()) return 0;
            if (!CheckModuleNo(moduleNo)) return 0;

            uint value = 0;
            uint result = CAXD.AxdiReadInportDword(moduleNo, offset, ref value);
            return AjinErrorCode.IsSuccess(result) ? value : 0;
        }

        #endregion

        #region 출력 (DO)

        /// <summary>
        /// 출력 비트 쓰기
        /// </summary>
        public bool WriteOutputBit(int moduleNo, int bitNo, bool value)
        {
            if (!CheckInitialized()) return false;
            if (!CheckModuleNo(moduleNo)) return false;

            uint result = CAXD.AxdoWriteOutportBit(moduleNo, bitNo, value ? 1u : 0u);
            return AjinErrorCode.IsSuccess(result);
        }

        /// <summary>
        /// 출력 바이트 쓰기 (8비트)
        /// </summary>
        public bool WriteOutputByte(int moduleNo, int offset, uint value)
        {
            if (!CheckInitialized()) return false;
            if (!CheckModuleNo(moduleNo)) return false;

            uint result = CAXD.AxdoWriteOutportByte(moduleNo, offset, value);
            return AjinErrorCode.IsSuccess(result);
        }

        /// <summary>
        /// 출력 워드 쓰기 (16비트)
        /// </summary>
        public bool WriteOutputWord(int moduleNo, int offset, uint value)
        {
            if (!CheckInitialized()) return false;
            if (!CheckModuleNo(moduleNo)) return false;

            uint result = CAXD.AxdoWriteOutportWord(moduleNo, offset, value);
            return AjinErrorCode.IsSuccess(result);
        }

        /// <summary>
        /// 출력 더블워드 쓰기 (32비트)
        /// </summary>
        public bool WriteOutputDWord(int moduleNo, int offset, uint value)
        {
            if (!CheckInitialized()) return false;
            if (!CheckModuleNo(moduleNo)) return false;

            uint result = CAXD.AxdoWriteOutportDword(moduleNo, offset, value);
            return AjinErrorCode.IsSuccess(result);
        }

        #endregion

        #region 출력 상태 읽기

        /// <summary>
        /// 출력 비트 상태 읽기
        /// </summary>
        public bool ReadOutputBit(int moduleNo, int bitNo)
        {
            if (!CheckInitialized()) return false;
            if (!CheckModuleNo(moduleNo)) return false;

            uint value = 0;
            uint result = CAXD.AxdoReadOutportBit(moduleNo, bitNo, ref value);
            return AjinErrorCode.IsSuccess(result) && value != 0;
        }

        /// <summary>
        /// 출력 바이트 상태 읽기 (8비트)
        /// </summary>
        public uint ReadOutputByte(int moduleNo, int offset)
        {
            if (!CheckInitialized()) return 0;
            if (!CheckModuleNo(moduleNo)) return 0;

            uint value = 0;
            uint result = CAXD.AxdoReadOutportByte(moduleNo, offset, ref value);
            return AjinErrorCode.IsSuccess(result) ? value : 0;
        }

        /// <summary>
        /// 출력 워드 상태 읽기 (16비트)
        /// </summary>
        public uint ReadOutputWord(int moduleNo, int offset)
        {
            if (!CheckInitialized()) return 0;
            if (!CheckModuleNo(moduleNo)) return 0;

            uint value = 0;
            uint result = CAXD.AxdoReadOutportWord(moduleNo, offset, ref value);
            return AjinErrorCode.IsSuccess(result) ? value : 0;
        }

        /// <summary>
        /// 출력 더블워드 상태 읽기 (32비트)
        /// </summary>
        public uint ReadOutputDWord(int moduleNo, int offset)
        {
            if (!CheckInitialized()) return 0;
            if (!CheckModuleNo(moduleNo)) return 0;

            uint value = 0;
            uint result = CAXD.AxdoReadOutportDword(moduleNo, offset, ref value);
            return AjinErrorCode.IsSuccess(result) ? value : 0;
        }

        #endregion

        #region 유틸리티

        /// <summary>
        /// 출력 토글
        /// </summary>
        public bool ToggleOutput(int moduleNo, int bitNo)
        {
            if (!CheckInitialized()) return false;
            if (!CheckModuleNo(moduleNo)) return false;

            bool currentValue = ReadOutputBit(moduleNo, bitNo);
            return WriteOutputBit(moduleNo, bitNo, !currentValue);
        }

        /// <summary>
        /// 모든 출력 OFF
        /// </summary>
        public bool ClearAllOutputs()
        {
            if (!CheckInitialized()) return false;

            bool success = true;
            for (int i = 0; i < _moduleCount; i++)
            {
                // 각 모듈의 모든 출력을 0으로 설정
                int outputCount = 0;
                CAXD.AxdInfoGetOutputCount(i, ref outputCount);

                // 32비트 단위로 클리어
                int dwordCount = (outputCount + 31) / 32;
                for (int j = 0; j < dwordCount; j++)
                {
                    if (!WriteOutputDWord(i, j * 4, 0))
                        success = false;
                }
            }
            return success;
        }

        #endregion

        #region Helper Methods

        private bool CheckInitialized()
        {
            if (!_isInitialized)
            {
                System.Diagnostics.Debug.WriteLine("AjinDigitalIO is not initialized");
                return false;
            }
            return true;
        }

        private bool CheckModuleNo(int moduleNo)
        {
            if (moduleNo < 0 || moduleNo >= _moduleCount)
            {
                System.Diagnostics.Debug.WriteLine($"Invalid module number: {moduleNo} (valid: 0~{_moduleCount - 1})");
                return false;
            }
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

        ~AjinDigitalIO()
        {
            Dispose(false);
        }

        #endregion
    }
}
