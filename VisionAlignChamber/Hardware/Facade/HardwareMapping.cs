using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace VisionAlignChamber.Hardware.Facade
{
    /// <summary>
    /// I/O 및 Motion 채널 매핑 관리
    /// I/O: Settings.ini, Motion Axis: Parameter.ini에서 로드
    /// </summary>
    public class HardwareMapping
    {
        #region Win32 API

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(
            string section, string key, string defaultValue,
            StringBuilder retVal, int size, string filePath);

        #endregion

        #region Fields

        private readonly string _ioConfigFilePath;      // Settings.ini (I/O 매핑)
        private readonly string _axisConfigFilePath;    // Parameter.ini (Axis 매핑)
        private readonly Dictionary<VADigitalInput, IOChannelInfo> _diMapping;
        private readonly Dictionary<VADigitalOutput, IOChannelInfo> _doMapping;
        private readonly Dictionary<VAMotionAxis, AxisInfo> _axisMapping;

        #endregion

        #region Properties

        /// <summary>
        /// Digital Input 매핑
        /// </summary>
        public IReadOnlyDictionary<VADigitalInput, IOChannelInfo> DIMapping => _diMapping;

        /// <summary>
        /// Digital Output 매핑
        /// </summary>
        public IReadOnlyDictionary<VADigitalOutput, IOChannelInfo> DOMapping => _doMapping;

        /// <summary>
        /// Motion Axis 매핑
        /// </summary>
        public IReadOnlyDictionary<VAMotionAxis, AxisInfo> AxisMapping => _axisMapping;

        #endregion

        #region Constructor

        /// <summary>
        /// HardwareMapping 생성자
        /// </summary>
        /// <param name="ioConfigFilePath">I/O 매핑 설정 파일 경로 (Settings.ini)</param>
        /// <param name="axisConfigFilePath">Axis 매핑 설정 파일 경로 (Parameter.ini), null이면 ioConfigFilePath와 같은 폴더의 Parameter.ini 사용</param>
        public HardwareMapping(string ioConfigFilePath, string axisConfigFilePath = null)
        {
            _ioConfigFilePath = ioConfigFilePath;

            // axisConfigFilePath가 없으면 같은 폴더의 Parameter.ini 사용
            if (string.IsNullOrEmpty(axisConfigFilePath))
            {
                string configDir = Path.GetDirectoryName(ioConfigFilePath);
                _axisConfigFilePath = Path.Combine(configDir, "Parameter.ini");
            }
            else
            {
                _axisConfigFilePath = axisConfigFilePath;
            }

            _diMapping = new Dictionary<VADigitalInput, IOChannelInfo>();
            _doMapping = new Dictionary<VADigitalOutput, IOChannelInfo>();
            _axisMapping = new Dictionary<VAMotionAxis, AxisInfo>();

            LoadMapping();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// DI 채널 정보 가져오기
        /// </summary>
        public IOChannelInfo GetDIChannel(VADigitalInput input)
        {
            if (_diMapping.TryGetValue(input, out var info))
                return info;

            throw new KeyNotFoundException($"DI mapping not found: {input}");
        }

        /// <summary>
        /// DO 채널 정보 가져오기
        /// </summary>
        public IOChannelInfo GetDOChannel(VADigitalOutput output)
        {
            if (_doMapping.TryGetValue(output, out var info))
                return info;

            throw new KeyNotFoundException($"DO mapping not found: {output}");
        }

        /// <summary>
        /// Motion Axis 정보 가져오기
        /// </summary>
        public AxisInfo GetAxisInfo(VAMotionAxis axis)
        {
            if (_axisMapping.TryGetValue(axis, out var info))
                return info;

            throw new KeyNotFoundException($"Axis mapping not found: {axis}");
        }

        /// <summary>
        /// 축 모션 파라미터 업데이트 (TeachingParameter Save 시 호출)
        /// </summary>
        public void UpdateAxisParameters(VAMotionAxis axis, double velocity, double accel, double decel)
        {
            if (_axisMapping.TryGetValue(axis, out var info))
            {
                info.DefaultVelocity = velocity;
                info.DefaultAccel = accel;
                info.DefaultDecel = decel;
            }
        }

        /// <summary>
        /// 매핑 다시 로드
        /// </summary>
        public void Reload()
        {
            _diMapping.Clear();
            _doMapping.Clear();
            _axisMapping.Clear();
            LoadMapping();
        }

        #endregion

        #region Private Methods

        private void LoadMapping()
        {
            // I/O 매핑 로드 (Settings.ini)
            if (File.Exists(_ioConfigFilePath))
            {
                LoadDIMapping();
                LoadDOMapping();
            }
            else
            {
                SetDefaultIOMapping();
            }

            // Axis 매핑 로드 (Parameter.ini)
            if (File.Exists(_axisConfigFilePath))
            {
                LoadAxisMapping();
            }
            else
            {
                SetDefaultAxisMapping();
            }
        }

        private void LoadDIMapping()
        {
            foreach (VADigitalInput di in Enum.GetValues(typeof(VADigitalInput)))
            {
                string key = di.ToString();
                string value = ReadValue(_ioConfigFilePath, "IO_DI", key, "0,0");
                var parts = value.Split(',');

                int moduleNo = 0;
                int channel = 0;

                if (parts.Length >= 2)
                {
                    int.TryParse(parts[0].Trim(), out moduleNo);
                    int.TryParse(parts[1].Trim(), out channel);
                }
                else if (parts.Length == 1)
                {
                    int.TryParse(parts[0].Trim(), out channel);
                }

                _diMapping[di] = new IOChannelInfo(moduleNo, channel, key);
            }
        }

        private void LoadDOMapping()
        {
            foreach (VADigitalOutput dout in Enum.GetValues(typeof(VADigitalOutput)))
            {
                string key = dout.ToString();
                string value = ReadValue(_ioConfigFilePath, "IO_DO", key, "0,0");
                var parts = value.Split(',');

                int moduleNo = 0;
                int channel = 0;

                if (parts.Length >= 2)
                {
                    int.TryParse(parts[0].Trim(), out moduleNo);
                    int.TryParse(parts[1].Trim(), out channel);
                }
                else if (parts.Length == 1)
                {
                    int.TryParse(parts[0].Trim(), out channel);
                }

                _doMapping[dout] = new IOChannelInfo(moduleNo, channel, key);
            }
        }

        private void LoadAxisMapping()
        {
            foreach (VAMotionAxis axis in Enum.GetValues(typeof(VAMotionAxis)))
            {
                string key = axis.ToString();
                int axisNo = ReadInt(_axisConfigFilePath, "Motion_Axis", key, 0);

                // 축별 안전한 기본값 (ini 읽기 실패 시 fallback)
                GetAxisDefaults(axis, out double defVel, out double defAccel, out double defDecel);
                double velocity = ReadDouble(_axisConfigFilePath, "Motion_Axis", $"{key}_Velocity", defVel);
                double accel = ReadDouble(_axisConfigFilePath, "Motion_Axis", $"{key}_Accel", defAccel);
                double decel = ReadDouble(_axisConfigFilePath, "Motion_Axis", $"{key}_Decel", defDecel);
                bool enabled = ReadBool(_axisConfigFilePath, "Motion_Axis", $"{key}_Enabled", true);

                _axisMapping[axis] = new AxisInfo(axisNo, key)
                {
                    DefaultVelocity = velocity,
                    DefaultAccel = accel,
                    DefaultDecel = decel,
                    Enabled = enabled
                };
            }
        }

        private void GetAxisDefaults(VAMotionAxis axis, out double velocity, out double accel, out double decel)
        {
            switch (axis)
            {
                case VAMotionAxis.WedgeUpDown:
                    velocity = 10000; accel = 50000; decel = 50000;
                    break;
                case VAMotionAxis.ChuckRotation:
                    velocity = 10; accel = 20; decel = 20;
                    break;
                case VAMotionAxis.CenteringStage_1:
                case VAMotionAxis.CenteringStage_2:
                    velocity = 4; accel = 8; decel = 8;
                    break;
                default:
                    velocity = 1; accel = 1; decel = 1;
                    break;
            }
        }

        private void SetDefaultIOMapping()
        {
            // DI 기본값
            int diChannel = 0;
            foreach (VADigitalInput di in Enum.GetValues(typeof(VADigitalInput)))
            {
                _diMapping[di] = new IOChannelInfo(0, diChannel++, di.ToString());
            }

            // DO 기본값
            int doChannel = 0;
            foreach (VADigitalOutput dout in Enum.GetValues(typeof(VADigitalOutput)))
            {
                _doMapping[dout] = new IOChannelInfo(0, doChannel++, dout.ToString());
            }
        }

        private void SetDefaultAxisMapping()
        {
            // Axis 기본값 (축별 안전한 속도 적용)
            int axisNo = 0;
            foreach (VAMotionAxis axis in Enum.GetValues(typeof(VAMotionAxis)))
            {
                GetAxisDefaults(axis, out double defVel, out double defAccel, out double defDecel);
                _axisMapping[axis] = new AxisInfo(axisNo++, axis.ToString())
                {
                    DefaultVelocity = defVel,
                    DefaultAccel = defAccel,
                    DefaultDecel = defDecel
                };
            }
        }

        private string ReadValue(string filePath, string section, string key, string defaultValue)
        {
            StringBuilder sb = new StringBuilder(256);
            GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, filePath);
            return sb.ToString();
        }

        private int ReadInt(string filePath, string section, string key, int defaultValue)
        {
            string value = ReadValue(filePath, section, key, defaultValue.ToString());
            return int.TryParse(value, out int result) ? result : defaultValue;
        }

        private double ReadDouble(string filePath, string section, string key, double defaultValue)
        {
            string value = ReadValue(filePath, section, key, defaultValue.ToString());
            return double.TryParse(value, out double result) ? result : defaultValue;
        }

        private bool ReadBool(string filePath, string section, string key, bool defaultValue)
        {
            string value = ReadValue(filePath, section, key, defaultValue ? "true" : "false").ToLower();
            return value == "true" || value == "1" || value == "yes";
        }

        #endregion
    }
}
