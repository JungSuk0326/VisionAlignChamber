using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace VisionAlignChamber.Hardware.IO
{
    /// <summary>
    /// I/O 및 Motion 채널 매핑 관리
    /// Settings.ini 파일에서 매핑 정보를 로드
    /// </summary>
    public class IOMapping
    {
        #region Win32 API

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(
            string section, string key, string defaultValue,
            StringBuilder retVal, int size, string filePath);

        #endregion

        #region Fields

        private readonly string _configFilePath;
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

        public IOMapping(string configFilePath)
        {
            _configFilePath = configFilePath;
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
            if (!File.Exists(_configFilePath))
            {
                SetDefaultMapping();
                return;
            }

            LoadDIMapping();
            LoadDOMapping();
            LoadAxisMapping();
        }

        private void LoadDIMapping()
        {
            foreach (VADigitalInput di in Enum.GetValues(typeof(VADigitalInput)))
            {
                string key = di.ToString();
                string value = ReadValue("IO_DI", key, "0,0");
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
                string value = ReadValue("IO_DO", key, "0,0");
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
                int axisNo = ReadInt("Motion_Axis", key, 0);

                double velocity = ReadDouble("Motion_Axis", $"{key}_Velocity", 10000);
                double accel = ReadDouble("Motion_Axis", $"{key}_Accel", 50000);
                double decel = ReadDouble("Motion_Axis", $"{key}_Decel", 50000);

                _axisMapping[axis] = new AxisInfo(axisNo, key)
                {
                    DefaultVelocity = velocity,
                    DefaultAccel = accel,
                    DefaultDecel = decel
                };
            }
        }

        private void SetDefaultMapping()
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

            // Axis 기본값
            int axisNo = 0;
            foreach (VAMotionAxis axis in Enum.GetValues(typeof(VAMotionAxis)))
            {
                _axisMapping[axis] = new AxisInfo(axisNo++, axis.ToString());
            }
        }

        private string ReadValue(string section, string key, string defaultValue)
        {
            StringBuilder sb = new StringBuilder(256);
            GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, _configFilePath);
            return sb.ToString();
        }

        private int ReadInt(string section, string key, int defaultValue)
        {
            string value = ReadValue(section, key, defaultValue.ToString());
            return int.TryParse(value, out int result) ? result : defaultValue;
        }

        private double ReadDouble(string section, string key, double defaultValue)
        {
            string value = ReadValue(section, key, defaultValue.ToString());
            return double.TryParse(value, out double result) ? result : defaultValue;
        }

        #endregion
    }
}
