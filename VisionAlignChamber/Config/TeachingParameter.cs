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
            // 축 설정 초기화
            WedgeUpDown = new AxisConfig();
            ChuckRotation = new AxisConfig();
            CenteringStage1 = new AxisConfig();
            CenteringStage2 = new AxisConfig();

            // 설정 파일이 없으면 기본값으로 생성
            if (!File.Exists(ParameterFilePath))
            {
                CreateDefaultParameterFile();
            }
            Load();
        }

        #endregion

        #region Axis Configuration Classes

        /// <summary>
        /// 축 설정 정보 (번호, 활성화, 속도/가속도/감속도)
        /// </summary>
        public class AxisConfig
        {
            public int AxisNo { get; set; }
            public bool Enabled { get; set; }
            public double Velocity { get; set; }
            public double Accel { get; set; }
            public double Decel { get; set; }
            public double JogVelocity { get; set; }
            public double JogAccel { get; set; }
            public double JogDecel { get; set; }

            public AxisConfig()
            {
                AxisNo = 0;
                Enabled = true;
                Velocity = 10000;
                Accel = 50000;
                Decel = 50000;
                JogVelocity = 5000;
                JogAccel = 30000;
                JogDecel = 30000;
            }

            public AxisConfig(int axisNo, bool enabled, double velocity, double accel, double decel,
                double jogVelocity = 5000, double jogAccel = 30000, double jogDecel = 30000)
            {
                AxisNo = axisNo;
                Enabled = enabled;
                Velocity = velocity;
                Accel = accel;
                Decel = decel;
                JogVelocity = jogVelocity;
                JogAccel = jogAccel;
                JogDecel = jogDecel;
            }
        }

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

        #region Axis Configurations

        /// <summary>
        /// Wedge Up/Down (Chuck Z) 축 설정
        /// </summary>
        public AxisConfig WedgeUpDown { get; set; }

        /// <summary>
        /// Chuck Rotation (Theta) 축 설정
        /// </summary>
        public AxisConfig ChuckRotation { get; set; }

        /// <summary>
        /// Centering Stage 1 (Centering L) 축 설정
        /// </summary>
        public AxisConfig CenteringStage1 { get; set; }

        /// <summary>
        /// Centering Stage 2 (Centering R) 축 설정
        /// </summary>
        public AxisConfig CenteringStage2 { get; set; }

        #endregion

        #region Teaching Positions

        // Chuck Z 포지션 (Axis 0)
        public double ChuckZ_Down { get; set; }
        public double ChuckZ_Vision { get; set; }
        public double ChuckZ_Vacuum { get; set; }

        // Centering L 포지션 (Axis 1)
        public double CenterL_Open { get; set; }
        public double CenterL_MinCtr { get; set; }  // FOV용 최소 센터링 위치

        // Centering R 포지션 (Axis 2)
        public double CenterR_Open { get; set; }
        public double CenterR_MinCtr { get; set; }  // FOV용 최소 센터링 위치

        // Theta 포지션 (Axis 3)
        public double Theta_Home { get; set; }

        // Vision Scan Parameters
        public double ScanStepAngle { get; set; }  // 스캔 시 스텝 각도
        public int ScanImageCount { get; set; }    // 스캔 이미지 수
        public double ScanWidthOffset { get; set; } // Width Offset

        // PN Check Parameters (ms)
        public int PNHoldTime { get; set; }     // 판정 유지 시간
        public int PNTimeout { get; set; }      // 판정 타임아웃
        public int PNPollInterval { get; set; } // 폴링 인터벌

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
                    ChuckZ = new AxisPosition(ChuckZ_Down, WedgeUpDown.Velocity, WedgeUpDown.Accel, WedgeUpDown.Decel),
                    CenteringL = new AxisPosition(CenterL_Open, CenteringStage1.Velocity, CenteringStage1.Accel, CenteringStage1.Decel),
                    CenteringR = new AxisPosition(CenterR_Open, CenteringStage2.Velocity, CenteringStage2.Accel, CenteringStage2.Decel),
                    Theta = new AxisPosition(Theta_Home, ChuckRotation.Velocity, ChuckRotation.Accel, ChuckRotation.Decel)
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
                    ChuckZ = new AxisPosition(ChuckZ_Down, WedgeUpDown.Velocity, WedgeUpDown.Accel, WedgeUpDown.Decel),
                    CenteringL = new AxisPosition(CenterL_MinCtr, CenteringStage1.Velocity, CenteringStage1.Accel, CenteringStage1.Decel),
                    CenteringR = new AxisPosition(CenterR_MinCtr, CenteringStage2.Velocity, CenteringStage2.Accel, CenteringStage2.Decel),
                    Theta = new AxisPosition(Theta_Home, ChuckRotation.Velocity, ChuckRotation.Accel, ChuckRotation.Decel)
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
                    ChuckZ = new AxisPosition(ChuckZ_Vision, WedgeUpDown.Velocity, WedgeUpDown.Accel, WedgeUpDown.Decel),
                    CenteringL = new AxisPosition(CenterL_Open, CenteringStage1.Velocity, CenteringStage1.Accel, CenteringStage1.Decel),
                    CenteringR = new AxisPosition(CenterR_Open, CenteringStage2.Velocity, CenteringStage2.Accel, CenteringStage2.Decel),
                    Theta = new AxisPosition(Theta_Home, ChuckRotation.Velocity, ChuckRotation.Accel, ChuckRotation.Decel)
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
                    ChuckZ = new AxisPosition(ChuckZ_Vision, WedgeUpDown.Velocity, WedgeUpDown.Accel, WedgeUpDown.Decel),
                    CenteringL = new AxisPosition(CenterL_Open, CenteringStage1.Velocity, CenteringStage1.Accel, CenteringStage1.Decel),
                    CenteringR = new AxisPosition(CenterR_Open, CenteringStage2.Velocity, CenteringStage2.Accel, CenteringStage2.Decel),
                    Theta = new AxisPosition(Theta_Home, ChuckRotation.Velocity, ChuckRotation.Accel, ChuckRotation.Decel)
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
                    ChuckZ = new AxisPosition(ChuckZ_Down, WedgeUpDown.Velocity, WedgeUpDown.Accel, WedgeUpDown.Decel),
                    // CenteringL/R은 Vision 결과 Radius 값으로 동적 설정
                    CenteringL = new AxisPosition(CenterL_Open, CenteringStage1.Velocity, CenteringStage1.Accel, CenteringStage1.Decel),
                    CenteringR = new AxisPosition(CenterR_Open, CenteringStage2.Velocity, CenteringStage2.Accel, CenteringStage2.Decel),
                    Theta = new AxisPosition(Theta_Home, ChuckRotation.Velocity, ChuckRotation.Accel, ChuckRotation.Decel)
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
                    ChuckZ = new AxisPosition(ChuckZ_Vacuum, WedgeUpDown.Velocity, WedgeUpDown.Accel, WedgeUpDown.Decel),
                    CenteringL = new AxisPosition(CenterL_Open, CenteringStage1.Velocity, CenteringStage1.Accel, CenteringStage1.Decel),
                    CenteringR = new AxisPosition(CenterR_Open, CenteringStage2.Velocity, CenteringStage2.Accel, CenteringStage2.Decel),
                    Theta = new AxisPosition(Theta_Home, ChuckRotation.Velocity, ChuckRotation.Accel, ChuckRotation.Decel)
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
                    ChuckZ = new AxisPosition(ChuckZ_Down, WedgeUpDown.Velocity, WedgeUpDown.Accel, WedgeUpDown.Decel),
                    CenteringL = new AxisPosition(CenterL_Open, CenteringStage1.Velocity, CenteringStage1.Accel, CenteringStage1.Decel),
                    CenteringR = new AxisPosition(CenterR_Open, CenteringStage2.Velocity, CenteringStage2.Accel, CenteringStage2.Decel),
                    // Theta는 최종 정렬 각도 유지 (HOLD)
                    Theta = new AxisPosition(Theta_Home, ChuckRotation.Velocity, ChuckRotation.Accel, ChuckRotation.Decel)
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
            // Axis Configurations
            WedgeUpDown.AxisNo = GetInt("Motion_Axis", "WedgeUpDown", 0);
            WedgeUpDown.Enabled = GetBool("Motion_Axis", "WedgeUpDown_Enabled", true);
            WedgeUpDown.Velocity = GetDouble("Motion_Axis", "WedgeUpDown_Velocity", 10000);
            WedgeUpDown.Accel = GetDouble("Motion_Axis", "WedgeUpDown_Accel", 50000);
            WedgeUpDown.Decel = GetDouble("Motion_Axis", "WedgeUpDown_Decel", 50000);
            WedgeUpDown.JogVelocity = GetDouble("Motion_Axis", "WedgeUpDown_JogVelocity", 5000);
            WedgeUpDown.JogAccel = GetDouble("Motion_Axis", "WedgeUpDown_JogAccel", 30000);
            WedgeUpDown.JogDecel = GetDouble("Motion_Axis", "WedgeUpDown_JogDecel", 30000);

            ChuckRotation.AxisNo = GetInt("Motion_Axis", "ChuckRotation", 1);
            ChuckRotation.Enabled = GetBool("Motion_Axis", "ChuckRotation_Enabled", true);
            ChuckRotation.Velocity = GetDouble("Motion_Axis", "ChuckRotation_Velocity", 10);
            ChuckRotation.Accel = GetDouble("Motion_Axis", "ChuckRotation_Accel", 20);
            ChuckRotation.Decel = GetDouble("Motion_Axis", "ChuckRotation_Decel", 20);
            ChuckRotation.JogVelocity = GetDouble("Motion_Axis", "ChuckRotation_JogVelocity", 5);
            ChuckRotation.JogAccel = GetDouble("Motion_Axis", "ChuckRotation_JogAccel", 10);
            ChuckRotation.JogDecel = GetDouble("Motion_Axis", "ChuckRotation_JogDecel", 10);

            CenteringStage1.AxisNo = GetInt("Motion_Axis", "CenteringStage_1", 2);
            CenteringStage1.Enabled = GetBool("Motion_Axis", "CenteringStage_1_Enabled", true);
            CenteringStage1.Velocity = GetDouble("Motion_Axis", "CenteringStage_1_Velocity", 4);
            CenteringStage1.Accel = GetDouble("Motion_Axis", "CenteringStage_1_Accel", 8);
            CenteringStage1.Decel = GetDouble("Motion_Axis", "CenteringStage_1_Decel", 8);
            CenteringStage1.JogVelocity = GetDouble("Motion_Axis", "CenteringStage_1_JogVelocity", 2);
            CenteringStage1.JogAccel = GetDouble("Motion_Axis", "CenteringStage_1_JogAccel", 4);
            CenteringStage1.JogDecel = GetDouble("Motion_Axis", "CenteringStage_1_JogDecel", 4);

            CenteringStage2.AxisNo = GetInt("Motion_Axis", "CenteringStage_2", 3);
            CenteringStage2.Enabled = GetBool("Motion_Axis", "CenteringStage_2_Enabled", true);
            CenteringStage2.Velocity = GetDouble("Motion_Axis", "CenteringStage_2_Velocity", 4);
            CenteringStage2.Accel = GetDouble("Motion_Axis", "CenteringStage_2_Accel", 8);
            CenteringStage2.Decel = GetDouble("Motion_Axis", "CenteringStage_2_Decel", 8);
            CenteringStage2.JogVelocity = GetDouble("Motion_Axis", "CenteringStage_2_JogVelocity", 2);
            CenteringStage2.JogAccel = GetDouble("Motion_Axis", "CenteringStage_2_JogAccel", 4);
            CenteringStage2.JogDecel = GetDouble("Motion_Axis", "CenteringStage_2_JogDecel", 4);

            // Chuck Z
            ChuckZ_Down = GetDouble("ChuckZ", "Down", 0);
            ChuckZ_Vision = GetDouble("ChuckZ", "Vision", 100000);
            ChuckZ_Vacuum = GetDouble("ChuckZ", "Vacuum", 50000);

            // Centering L
            CenterL_Open = GetDouble("CenteringL", "Open", 0);
            CenterL_MinCtr = GetDouble("CenteringL", "MinCtr", 50000);

            // Centering R
            CenterR_Open = GetDouble("CenteringR", "Open", 0);
            CenterR_MinCtr = GetDouble("CenteringR", "MinCtr", 50000);

            // Theta
            Theta_Home = GetDouble("Theta", "Home", 0);

            // Vision Scan Parameters
            ScanStepAngle = GetDouble("VisionScan", "StepAngle", 15);
            ScanImageCount = GetInt("VisionScan", "ImageCount", 24);
            ScanWidthOffset = GetDouble("VisionScan", "WidthOffset", 0.4);

            // PN Check Parameters
            PNHoldTime = GetInt("PNCheck", "HoldTime", 1000);
            PNTimeout = GetInt("PNCheck", "Timeout", 5000);
            PNPollInterval = GetInt("PNCheck", "PollInterval", 50);

            // 기존 파일에 누락된 섹션이 있으면 기본값으로 기록
            EnsureSectionExists();

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

            // Axis Configurations
            WriteValue("Motion_Axis", "WedgeUpDown", WedgeUpDown.AxisNo.ToString());
            WriteValue("Motion_Axis", "WedgeUpDown_Enabled", WedgeUpDown.Enabled.ToString().ToLower());
            WriteValue("Motion_Axis", "WedgeUpDown_Velocity", WedgeUpDown.Velocity.ToString());
            WriteValue("Motion_Axis", "WedgeUpDown_Accel", WedgeUpDown.Accel.ToString());
            WriteValue("Motion_Axis", "WedgeUpDown_Decel", WedgeUpDown.Decel.ToString());
            WriteValue("Motion_Axis", "WedgeUpDown_JogVelocity", WedgeUpDown.JogVelocity.ToString());
            WriteValue("Motion_Axis", "WedgeUpDown_JogAccel", WedgeUpDown.JogAccel.ToString());
            WriteValue("Motion_Axis", "WedgeUpDown_JogDecel", WedgeUpDown.JogDecel.ToString());

            WriteValue("Motion_Axis", "ChuckRotation", ChuckRotation.AxisNo.ToString());
            WriteValue("Motion_Axis", "ChuckRotation_Enabled", ChuckRotation.Enabled.ToString().ToLower());
            WriteValue("Motion_Axis", "ChuckRotation_Velocity", ChuckRotation.Velocity.ToString());
            WriteValue("Motion_Axis", "ChuckRotation_Accel", ChuckRotation.Accel.ToString());
            WriteValue("Motion_Axis", "ChuckRotation_Decel", ChuckRotation.Decel.ToString());
            WriteValue("Motion_Axis", "ChuckRotation_JogVelocity", ChuckRotation.JogVelocity.ToString());
            WriteValue("Motion_Axis", "ChuckRotation_JogAccel", ChuckRotation.JogAccel.ToString());
            WriteValue("Motion_Axis", "ChuckRotation_JogDecel", ChuckRotation.JogDecel.ToString());

            WriteValue("Motion_Axis", "CenteringStage_1", CenteringStage1.AxisNo.ToString());
            WriteValue("Motion_Axis", "CenteringStage_1_Enabled", CenteringStage1.Enabled.ToString().ToLower());
            WriteValue("Motion_Axis", "CenteringStage_1_Velocity", CenteringStage1.Velocity.ToString());
            WriteValue("Motion_Axis", "CenteringStage_1_Accel", CenteringStage1.Accel.ToString());
            WriteValue("Motion_Axis", "CenteringStage_1_Decel", CenteringStage1.Decel.ToString());
            WriteValue("Motion_Axis", "CenteringStage_1_JogVelocity", CenteringStage1.JogVelocity.ToString());
            WriteValue("Motion_Axis", "CenteringStage_1_JogAccel", CenteringStage1.JogAccel.ToString());
            WriteValue("Motion_Axis", "CenteringStage_1_JogDecel", CenteringStage1.JogDecel.ToString());

            WriteValue("Motion_Axis", "CenteringStage_2", CenteringStage2.AxisNo.ToString());
            WriteValue("Motion_Axis", "CenteringStage_2_Enabled", CenteringStage2.Enabled.ToString().ToLower());
            WriteValue("Motion_Axis", "CenteringStage_2_Velocity", CenteringStage2.Velocity.ToString());
            WriteValue("Motion_Axis", "CenteringStage_2_Accel", CenteringStage2.Accel.ToString());
            WriteValue("Motion_Axis", "CenteringStage_2_Decel", CenteringStage2.Decel.ToString());
            WriteValue("Motion_Axis", "CenteringStage_2_JogVelocity", CenteringStage2.JogVelocity.ToString());
            WriteValue("Motion_Axis", "CenteringStage_2_JogAccel", CenteringStage2.JogAccel.ToString());
            WriteValue("Motion_Axis", "CenteringStage_2_JogDecel", CenteringStage2.JogDecel.ToString());

            // Chuck Z
            WriteValue("ChuckZ", "Down", ChuckZ_Down.ToString());
            WriteValue("ChuckZ", "Vision", ChuckZ_Vision.ToString());
            WriteValue("ChuckZ", "Vacuum", ChuckZ_Vacuum.ToString());

            // Centering L
            WriteValue("CenteringL", "Open", CenterL_Open.ToString());
            WriteValue("CenteringL", "MinCtr", CenterL_MinCtr.ToString());

            // Centering R
            WriteValue("CenteringR", "Open", CenterR_Open.ToString());
            WriteValue("CenteringR", "MinCtr", CenterR_MinCtr.ToString());

            // Theta
            WriteValue("Theta", "Home", Theta_Home.ToString());

            // Vision Scan Parameters
            WriteValue("VisionScan", "StepAngle", ScanStepAngle.ToString());
            WriteValue("VisionScan", "ImageCount", ScanImageCount.ToString());
            WriteValue("VisionScan", "WidthOffset", ScanWidthOffset.ToString());

            // PN Check Parameters
            WriteValue("PNCheck", "HoldTime", PNHoldTime.ToString());
            WriteValue("PNCheck", "Timeout", PNTimeout.ToString());
            WriteValue("PNCheck", "PollInterval", PNPollInterval.ToString());

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
; Sequence: Receive -> PreCtr(FOV) -> Ready -> Scan(0->360, xN) -> Align(Center+Theta) -> Eddy -> Release

;=============================================================================
; Motion Axis Configuration
;=============================================================================

[Motion_Axis]
; Wedge Up/Down Stage (Servo) - Chuck Z
WedgeUpDown=0
WedgeUpDown_Enabled=true
WedgeUpDown_Velocity=10000
WedgeUpDown_Accel=50000
WedgeUpDown_Decel=50000
WedgeUpDown_JogVelocity=5000
WedgeUpDown_JogAccel=30000
WedgeUpDown_JogDecel=30000

; Chuck Rotation (DD Motor) - Theta
ChuckRotation=1
ChuckRotation_Enabled=true
ChuckRotation_Velocity=10
ChuckRotation_Accel=20
ChuckRotation_Decel=20
ChuckRotation_JogVelocity=5
ChuckRotation_JogAccel=10
ChuckRotation_JogDecel=10

; Centering Stage 1 (Stepping) - Centering L
CenteringStage_1=2
CenteringStage_1_Enabled=true
CenteringStage_1_Velocity=4
CenteringStage_1_Accel=8
CenteringStage_1_Decel=8
CenteringStage_1_JogVelocity=2
CenteringStage_1_JogAccel=4
CenteringStage_1_JogDecel=4

; Centering Stage 2 (Stepping) - Centering R
CenteringStage_2=3
CenteringStage_2_Enabled=true
CenteringStage_2_Velocity=4
CenteringStage_2_Accel=8
CenteringStage_2_Decel=8
CenteringStage_2_JogVelocity=2
CenteringStage_2_JogAccel=4
CenteringStage_2_JogDecel=4

;=============================================================================
; Teaching Positions
;=============================================================================

[ChuckZ]
; Chuck Z Axis (Axis 0) Positions [pulse]
Down=0
Vision=100000
Vacuum=50000

[CenteringL]
; Centering L Axis (Axis 2) Positions [pulse]
Open=0
MinCtr=50000

[CenteringR]
; Centering R Axis (Axis 3) Positions [pulse]
Open=0
MinCtr=50000

[Theta]
; Theta Axis (Axis 1) Positions [pulse or degree]
Home=0

;=============================================================================
; Vision Scan Parameters
;=============================================================================

[VisionScan]
StepAngle=15
ImageCount=24
WidthOffset=0.4

;=============================================================================
; PN Check Parameters (ms)
;=============================================================================

[PNCheck]
HoldTime=1000
Timeout=5000
PollInterval=50
";
            File.WriteAllText(ParameterFilePath, defaultContent, Encoding.UTF8);
            System.Diagnostics.Debug.WriteLine($"Default Parameter.ini created: {ParameterFilePath}");
        }

        #endregion

        /// <summary>
        /// 기존 Parameter.ini에 누락된 섹션이 있으면 현재 값(기본값)을 기록
        /// 새 섹션이 코드에 추가되었지만 기존 파일에 없는 경우를 자동 보정
        /// </summary>
        private void EnsureSectionExists()
        {
            // [PNCheck] 섹션 존재 여부 확인
            if (string.IsNullOrEmpty(ReadValue("PNCheck", "HoldTime", "")))
            {
                WriteValue("PNCheck", "HoldTime", PNHoldTime.ToString());
                WriteValue("PNCheck", "Timeout", PNTimeout.ToString());
                WriteValue("PNCheck", "PollInterval", PNPollInterval.ToString());
                System.Diagnostics.Debug.WriteLine("EnsureSectionExists: [PNCheck] section created with defaults.");
            }

            // [VisionScan] WidthOffset 키 누락 확인
            if (string.IsNullOrEmpty(ReadValue("VisionScan", "WidthOffset", "")))
            {
                WriteValue("VisionScan", "WidthOffset", ScanWidthOffset.ToString());
                System.Diagnostics.Debug.WriteLine("EnsureSectionExists: [VisionScan] WidthOffset created with default.");
            }
        }

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

        private bool GetBool(string section, string key, bool defaultValue = false)
        {
            string value = ReadValue(section, key, defaultValue ? "true" : "false");
            return value.ToLower() == "true" || value == "1";
        }

        #endregion
    }
}
