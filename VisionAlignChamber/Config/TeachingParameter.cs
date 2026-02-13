using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace VisionAlignChamber.Config
{
    /// <summary>
    /// 티칭 파라미터 관리 클래스
    /// 4축 Vision Align Chamber의 티칭 포지션을 관리
    /// </summary>
    public class TeachingParameter
    {
        #region Win32 API for INI File

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string defaultValue,
            StringBuilder retVal,
            int size,
            string filePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool WritePrivateProfileString(
            string section,
            string key,
            string value,
            string filePath);

        #endregion

        #region Constants

        /// <summary>
        /// 파라미터 파일 경로
        /// </summary>
        public static string ParameterFilePath
        {
            get
            {
                return Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Config",
                    "Parameter.ini");
            }
        }

        #endregion

        #region Singleton

        private static TeachingParameter _instance;
        private static readonly object _lock = new object();

        public static TeachingParameter Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new TeachingParameter();
                        }
                    }
                }
                return _instance;
            }
        }

        private TeachingParameter()
        {
            // 설정 파일이 없으면 기본값으로 생성
            if (!File.Exists(ParameterFilePath))
            {
                CreateDefaultParameterFile();
            }
            Load();
        }

        #endregion

        #region Axis Position Classes

        /// <summary>
        /// 단일 축 위치 정보
        /// </summary>
        public class AxisPosition
        {
            public double Position { get; set; }
            public double Velocity { get; set; }
            public double Accel { get; set; }
            public double Decel { get; set; }

            public AxisPosition()
            {
                Position = 0;
                Velocity = 10000;
                Accel = 50000;
                Decel = 50000;
            }

            public AxisPosition(double position, double velocity = 10000, double accel = 50000, double decel = 50000)
            {
                Position = position;
                Velocity = velocity;
                Accel = accel;
                Decel = decel;
            }
        }

        /// <summary>
        /// 4축 통합 위치 정보 (Position Set)
        /// </summary>
        public class PositionSet
        {
            public string Name { get; set; }
            public AxisPosition ChuckZ { get; set; }
            public AxisPosition CenteringL { get; set; }
            public AxisPosition CenteringR { get; set; }
            public AxisPosition Theta { get; set; }

            public PositionSet()
            {
                Name = "";
                ChuckZ = new AxisPosition();
                CenteringL = new AxisPosition();
                CenteringR = new AxisPosition();
                Theta = new AxisPosition();
            }

            public PositionSet(string name)
            {
                Name = name;
                ChuckZ = new AxisPosition();
                CenteringL = new AxisPosition();
                CenteringR = new AxisPosition();
                Theta = new AxisPosition();
            }
        }

        #endregion

        #region Teaching Positions

        // Chuck Z 포지션 (Axis 0)
        public double ChuckZ_Down { get; set; }
        public double ChuckZ_Vision { get; set; }
        public double ChuckZ_Eddy { get; set; }

        // Centering L 포지션 (Axis 1)
        public double CenterL_Open { get; set; }
        public double CenterL_MinCtr { get; set; }  // FOV용 최소 센터링 위치

        // Centering R 포지션 (Axis 2)
        public double CenterR_Open { get; set; }
        public double CenterR_MinCtr { get; set; }  // FOV용 최소 센터링 위치

        // Theta 포지션 (Axis 3)
        public double Theta_Home { get; set; }
        public double Theta_ScanStart { get; set; }
        public double Theta_ScanEnd { get; set; }  // 스캔 종료 위치 (= ScanStart + 360)

        // Velocity/Accel/Decel Settings
        public double DefaultVelocity { get; set; }
        public double DefaultAccel { get; set; }
        public double DefaultDecel { get; set; }

        // Vision Scan Parameters
        public double ScanStepAngle { get; set; }  // 스캔 시 스텝 각도
        public int ScanImageCount { get; set; }    // 스캔 이미지 수

        #endregion

        #region Position Set Properties

        /// <summary>
        /// WAFER_RECEIVE 포지션 세트 (Step 1)
        /// CTC Robot이 Pin에 웨이퍼 안착
        /// </summary>
        public PositionSet WaferReceive
        {
            get
            {
                return new PositionSet("WAFER_RECEIVE")
                {
                    ChuckZ = new AxisPosition(ChuckZ_Down, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringL = new AxisPosition(CenterL_Open, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringR = new AxisPosition(CenterR_Open, DefaultVelocity, DefaultAccel, DefaultDecel),
                    Theta = new AxisPosition(Theta_Home, DefaultVelocity, DefaultAccel, DefaultDecel)
                };
            }
        }

        /// <summary>
        /// PRE_CENTER 포지션 세트 (Step 2)
        /// FOV 확보를 위한 최소 센터링
        /// </summary>
        public PositionSet PreCenter
        {
            get
            {
                return new PositionSet("PRE_CENTER")
                {
                    ChuckZ = new AxisPosition(ChuckZ_Down, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringL = new AxisPosition(CenterL_MinCtr, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringR = new AxisPosition(CenterR_MinCtr, DefaultVelocity, DefaultAccel, DefaultDecel),
                    Theta = new AxisPosition(Theta_Home, DefaultVelocity, DefaultAccel, DefaultDecel)
                };
            }
        }

        /// <summary>
        /// READY 포지션 세트 (Step 3)
        /// Vision 스캔 준비
        /// </summary>
        public PositionSet Ready
        {
            get
            {
                return new PositionSet("READY")
                {
                    ChuckZ = new AxisPosition(ChuckZ_Vision, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringL = new AxisPosition(CenterL_Open, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringR = new AxisPosition(CenterR_Open, DefaultVelocity, DefaultAccel, DefaultDecel),
                    Theta = new AxisPosition(Theta_ScanStart, DefaultVelocity, DefaultAccel, DefaultDecel)
                };
            }
        }

        /// <summary>
        /// SCAN_START 포지션 세트 (Step 4)
        /// Vision 스캔 시작 위치
        /// </summary>
        public PositionSet ScanStart
        {
            get
            {
                return new PositionSet("SCAN_START")
                {
                    ChuckZ = new AxisPosition(ChuckZ_Vision, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringL = new AxisPosition(CenterL_Open, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringR = new AxisPosition(CenterR_Open, DefaultVelocity, DefaultAccel, DefaultDecel),
                    Theta = new AxisPosition(Theta_ScanStart, DefaultVelocity, DefaultAccel, DefaultDecel)
                };
            }
        }

        /// <summary>
        /// CENTER_RADIUS 포지션 세트 (Step 7)
        /// Vision Radius 값으로 센터링
        /// </summary>
        public PositionSet CenterRadius
        {
            get
            {
                return new PositionSet("CENTER_RADIUS")
                {
                    ChuckZ = new AxisPosition(ChuckZ_Down, DefaultVelocity, DefaultAccel, DefaultDecel),
                    // CenteringL/R은 Vision 결과 Radius 값으로 동적 설정
                    CenteringL = new AxisPosition(CenterL_Open, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringR = new AxisPosition(CenterR_Open, DefaultVelocity, DefaultAccel, DefaultDecel),
                    Theta = new AxisPosition(Theta_ScanStart, DefaultVelocity, DefaultAccel, DefaultDecel)
                };
            }
        }

        /// <summary>
        /// EDDY 포지션 세트 (Step 8)
        /// Eddy 측정 위치
        /// </summary>
        public PositionSet Eddy
        {
            get
            {
                return new PositionSet("EDDY")
                {
                    ChuckZ = new AxisPosition(ChuckZ_Eddy, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringL = new AxisPosition(CenterL_Open, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringR = new AxisPosition(CenterR_Open, DefaultVelocity, DefaultAccel, DefaultDecel),
                    Theta = new AxisPosition(Theta_ScanStart, DefaultVelocity, DefaultAccel, DefaultDecel)
                };
            }
        }

        /// <summary>
        /// WAFER_RELEASE 포지션 세트 (Step 10)
        /// 웨이퍼 배출 위치
        /// </summary>
        public PositionSet WaferRelease
        {
            get
            {
                return new PositionSet("WAFER_RELEASE")
                {
                    ChuckZ = new AxisPosition(ChuckZ_Down, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringL = new AxisPosition(CenterL_Open, DefaultVelocity, DefaultAccel, DefaultDecel),
                    CenteringR = new AxisPosition(CenterR_Open, DefaultVelocity, DefaultAccel, DefaultDecel),
                    // Theta는 최종 정렬 각도 유지 (HOLD)
                    Theta = new AxisPosition(Theta_ScanStart, DefaultVelocity, DefaultAccel, DefaultDecel)
                };
            }
        }

        #endregion

        #region Load / Save

        /// <summary>
        /// 파라미터 파일에서 설정 로드
        /// </summary>
        public void Load()
        {
            // Chuck Z
            ChuckZ_Down = GetDouble("ChuckZ", "Down", 0);
            ChuckZ_Vision = GetDouble("ChuckZ", "Vision", 100);
            ChuckZ_Eddy = GetDouble("ChuckZ", "Eddy", 50);

            // Centering L
            CenterL_Open = GetDouble("CenteringL", "Open", 0);
            CenterL_MinCtr = GetDouble("CenteringL", "MinCtr", 50);

            // Centering R
            CenterR_Open = GetDouble("CenteringR", "Open", 0);
            CenterR_MinCtr = GetDouble("CenteringR", "MinCtr", 50);

            // Theta
            Theta_Home = GetDouble("Theta", "Home", 0);
            Theta_ScanStart = GetDouble("Theta", "ScanStart", -180);
            Theta_ScanEnd = GetDouble("Theta", "ScanEnd", 180);

            // Motion Parameters
            DefaultVelocity = GetDouble("Motion", "DefaultVelocity", 10000);
            DefaultAccel = GetDouble("Motion", "DefaultAccel", 50000);
            DefaultDecel = GetDouble("Motion", "DefaultDecel", 50000);

            // Vision Scan Parameters
            ScanStepAngle = GetDouble("VisionScan", "StepAngle", 15);
            ScanImageCount = GetInt("VisionScan", "ImageCount", 24);

            System.Diagnostics.Debug.WriteLine($"TeachingParameter loaded from: {ParameterFilePath}");
        }

        /// <summary>
        /// 파라미터 파일에 설정 저장
        /// </summary>
        public void Save()
        {
            // Config 폴더 생성
            string configDir = Path.GetDirectoryName(ParameterFilePath);
            if (!Directory.Exists(configDir))
            {
                Directory.CreateDirectory(configDir);
            }

            // Chuck Z
            WriteValue("ChuckZ", "Down", ChuckZ_Down.ToString());
            WriteValue("ChuckZ", "Vision", ChuckZ_Vision.ToString());
            WriteValue("ChuckZ", "Eddy", ChuckZ_Eddy.ToString());

            // Centering L
            WriteValue("CenteringL", "Open", CenterL_Open.ToString());
            WriteValue("CenteringL", "MinCtr", CenterL_MinCtr.ToString());

            // Centering R
            WriteValue("CenteringR", "Open", CenterR_Open.ToString());
            WriteValue("CenteringR", "MinCtr", CenterR_MinCtr.ToString());

            // Theta
            WriteValue("Theta", "Home", Theta_Home.ToString());
            WriteValue("Theta", "ScanStart", Theta_ScanStart.ToString());
            WriteValue("Theta", "ScanEnd", Theta_ScanEnd.ToString());

            // Motion Parameters
            WriteValue("Motion", "DefaultVelocity", DefaultVelocity.ToString());
            WriteValue("Motion", "DefaultAccel", DefaultAccel.ToString());
            WriteValue("Motion", "DefaultDecel", DefaultDecel.ToString());

            // Vision Scan Parameters
            WriteValue("VisionScan", "StepAngle", ScanStepAngle.ToString());
            WriteValue("VisionScan", "ImageCount", ScanImageCount.ToString());

            System.Diagnostics.Debug.WriteLine($"TeachingParameter saved to: {ParameterFilePath}");
        }

        /// <summary>
        /// 기본 파라미터 파일 생성
        /// </summary>
        private void CreateDefaultParameterFile()
        {
            string configDir = Path.GetDirectoryName(ParameterFilePath);
            if (!Directory.Exists(configDir))
            {
                Directory.CreateDirectory(configDir);
            }

            string defaultContent = @"; Vision Align Chamber Teaching Parameters
; 4-Axis Teaching Position Configuration

[ChuckZ]
; Chuck Z Axis (Axis 0) Positions [pulse]
Down=0
Vision=100000
Eddy=50000

[CenteringL]
; Centering L Axis (Axis 1) Positions [pulse]
Open=0
MinCtr=50000

[CenteringR]
; Centering R Axis (Axis 2) Positions [pulse]
Open=0
MinCtr=50000

[Theta]
; Theta Axis (Axis 3) Positions [pulse or degree]
Home=0
ScanStart=-180
ScanEnd=180

[Motion]
; Default Motion Parameters
DefaultVelocity=10000
DefaultAccel=50000
DefaultDecel=50000

[VisionScan]
; Vision Scan Parameters
StepAngle=15
ImageCount=24
";
            File.WriteAllText(ParameterFilePath, defaultContent, Encoding.UTF8);
            System.Diagnostics.Debug.WriteLine($"Default Parameter.ini created: {ParameterFilePath}");
        }

        #endregion

        #region INI File Helpers

        private string ReadValue(string section, string key, string defaultValue = "")
        {
            if (!File.Exists(ParameterFilePath))
                return defaultValue;

            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, ParameterFilePath);
            return sb.ToString();
        }

        private bool WriteValue(string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, ParameterFilePath);
        }

        private double GetDouble(string section, string key, double defaultValue = 0.0)
        {
            string value = ReadValue(section, key, defaultValue.ToString());
            return double.TryParse(value, out double result) ? result : defaultValue;
        }

        private int GetInt(string section, string key, int defaultValue = 0)
        {
            string value = ReadValue(section, key, defaultValue.ToString());
            return int.TryParse(value, out int result) ? result : defaultValue;
        }

        #endregion
    }
}
