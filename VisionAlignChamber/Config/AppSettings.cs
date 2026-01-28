using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace VisionAlignChamber.Config
{
    /// <summary>
    /// 애플리케이션 설정 관리 클래스
    /// Settings.ini 파일에서 설정값을 읽어옴
    /// </summary>
    public static class AppSettings
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

        #region Fields

        private static string _settingsFilePath;
        private static bool _isInitialized = false;

        #endregion

        #region Properties

        /// <summary>
        /// 설정 파일 경로
        /// </summary>
        public static string SettingsFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(_settingsFilePath))
                {
                    _settingsFilePath = Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Config",
                        "Settings.ini");
                }
                return _settingsFilePath;
            }
            set { _settingsFilePath = value; }
        }

        /// <summary>
        /// 설정 파일 존재 여부
        /// </summary>
        public static bool SettingsFileExists => File.Exists(SettingsFilePath);

        #endregion

        #region DLL Path Properties

        /// <summary>
        /// AXL.dll 경로
        /// </summary>
        public static string AXL_DllPath
        {
            get => GetDllPath("DLL", "AXL_Path", "AXL.dll");
            set => WriteValue("DLL", "AXL_Path", value);
        }

        /// <summary>
        /// EzBasicAxl.dll 경로
        /// </summary>
        public static string EzBasicAxl_DllPath
        {
            get => GetDllPath("DLL", "EzBasicAxl_Path", "EzBasicAxl.dll");
            set => WriteValue("DLL", "EzBasicAxl_Path", value);
        }

        /// <summary>
        /// eMotionAlign.dll 경로
        /// </summary>
        public static string eMotionAlign_DllPath
        {
            get => GetDllPath("Vision", "eMotionAlign_Path", "eMotionAlign.dll");
            set => WriteValue("Vision", "eMotionAlign_Path", value);
        }

        /// <summary>
        /// OpenCvSharp.dll 경로
        /// </summary>
        public static string OpenCvSharp_DllPath
        {
            get => GetDllPath("Vision", "OpenCvSharp_Path", "OpenCvSharp.dll");
            set => WriteValue("Vision", "OpenCvSharp_Path", value);
        }

        #endregion

        #region Motion Properties

        /// <summary>
        /// 기본 속도
        /// </summary>
        public static double DefaultVelocity
        {
            get => GetDouble("Motion", "DefaultVelocity", 10000);
            set => WriteValue("Motion", "DefaultVelocity", value.ToString());
        }

        /// <summary>
        /// 기본 가속도
        /// </summary>
        public static double DefaultAccel
        {
            get => GetDouble("Motion", "DefaultAccel", 50000);
            set => WriteValue("Motion", "DefaultAccel", value.ToString());
        }

        /// <summary>
        /// 기본 감속도
        /// </summary>
        public static double DefaultDecel
        {
            get => GetDouble("Motion", "DefaultDecel", 50000);
            set => WriteValue("Motion", "DefaultDecel", value.ToString());
        }

        #endregion

        #region Camera Properties

        /// <summary>
        /// 이미지 너비
        /// </summary>
        public static int ImageWidth
        {
            get => GetInt("Camera", "ImageWidth", 5120);
            set => WriteValue("Camera", "ImageWidth", value.ToString());
        }

        /// <summary>
        /// 이미지 높이
        /// </summary>
        public static int ImageHeight
        {
            get => GetInt("Camera", "ImageHeight", 5120);
            set => WriteValue("Camera", "ImageHeight", value.ToString());
        }

        /// <summary>
        /// 이미지 개수
        /// </summary>
        public static int ImageCount
        {
            get => GetInt("Camera", "ImageCount", 24);
            set => WriteValue("Camera", "ImageCount", value.ToString());
        }

        /// <summary>
        /// 촬영 각도 간격
        /// </summary>
        public static double AngleStep
        {
            get => GetDouble("Camera", "AngleStep", 15.0);
            set => WriteValue("Camera", "AngleStep", value.ToString());
        }

        #endregion

        #region Initialization

        /// <summary>
        /// 설정 초기화 및 DLL 경로 설정
        /// 애플리케이션 시작 시 호출해야 함
        /// </summary>
        public static bool Initialize()
        {
            if (_isInitialized)
                return true;

            try
            {
                // 설정 파일이 없으면 기본 파일 생성
                if (!SettingsFileExists)
                {
                    CreateDefaultSettingsFile();
                }

                // DLL 검색 경로 설정
                SetDllSearchPaths();

                _isInitialized = true;
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AppSettings Initialize Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// DLL 검색 경로 설정
        /// </summary>
        private static void SetDllSearchPaths()
        {
            // AXL.dll 경로 설정
            string axlPath = ReadValue("DLL", "AXL_Path", "");
            if (!string.IsNullOrEmpty(axlPath))
            {
                string axlDir = Path.GetDirectoryName(axlPath);
                if (!string.IsNullOrEmpty(axlDir) && Directory.Exists(axlDir))
                {
                    AddDllDirectory(axlDir);
                    SetDllDirectory(axlDir);
                }
            }

            // EzBasicAxl.dll 경로 설정
            string ezPath = ReadValue("DLL", "EzBasicAxl_Path", "");
            if (!string.IsNullOrEmpty(ezPath))
            {
                string ezDir = Path.GetDirectoryName(ezPath);
                if (!string.IsNullOrEmpty(ezDir) && Directory.Exists(ezDir))
                {
                    AddDllDirectory(ezDir);
                }
            }
        }

        #endregion

        #region Read/Write Methods

        /// <summary>
        /// INI 파일에서 문자열 값 읽기
        /// </summary>
        public static string ReadValue(string section, string key, string defaultValue = "")
        {
            if (!SettingsFileExists)
                return defaultValue;

            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, SettingsFilePath);
            return sb.ToString();
        }

        /// <summary>
        /// INI 파일에 문자열 값 쓰기
        /// </summary>
        public static bool WriteValue(string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, SettingsFilePath);
        }

        /// <summary>
        /// 정수 값 읽기
        /// </summary>
        public static int GetInt(string section, string key, int defaultValue = 0)
        {
            string value = ReadValue(section, key, defaultValue.ToString());
            return int.TryParse(value, out int result) ? result : defaultValue;
        }

        /// <summary>
        /// 실수 값 읽기
        /// </summary>
        public static double GetDouble(string section, string key, double defaultValue = 0.0)
        {
            string value = ReadValue(section, key, defaultValue.ToString());
            return double.TryParse(value, out double result) ? result : defaultValue;
        }

        /// <summary>
        /// 불린 값 읽기
        /// </summary>
        public static bool GetBool(string section, string key, bool defaultValue = false)
        {
            string value = ReadValue(section, key, defaultValue ? "1" : "0");
            return value == "1" || value.ToLower() == "true";
        }

        /// <summary>
        /// DLL 경로 읽기 (빈 값이면 기본 파일명 반환)
        /// </summary>
        private static string GetDllPath(string section, string key, string defaultFileName)
        {
            string path = ReadValue(section, key, "");

            if (string.IsNullOrEmpty(path))
            {
                // 기본 경로 (실행 파일 폴더)
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defaultFileName);
            }

            // 상대 경로인 경우 절대 경로로 변환
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }

            return path;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// 기본 설정 파일 생성
        /// </summary>
        private static void CreateDefaultSettingsFile()
        {
            try
            {
                string defaultContent = @"[DLL]
; AJIN EtherCAT DLL 경로 설정
; 빈 값이면 기본 경로(실행 파일 폴더)에서 로드
; 절대 경로 또는 상대 경로 사용 가능

; AXL.dll 경로 (예: C:\AJINEXTEK\AXL\AXL.dll)
AXL_Path=

; EzBasicAxl.dll 경로 (예: C:\AJINEXTEK\AXL\EzBasicAxl.dll)
EzBasicAxl_Path=

[Vision]
; eMotionAlign.dll 경로 (비어있으면 기본 경로 사용)
eMotionAlign_Path=

; OpenCvSharp.dll 경로 (비어있으면 기본 경로 사용)
OpenCvSharp_Path=

[Motion]
; 기본 모션 파라미터
DefaultVelocity=10000
DefaultAccel=50000
DefaultDecel=50000

[Camera]
; 카메라 설정
ImageWidth=5120
ImageHeight=5120
ImageCount=24
AngleStep=15.0
";
                File.WriteAllText(SettingsFilePath, defaultContent, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CreateDefaultSettingsFile Error: {ex.Message}");
            }
        }

        #endregion

        #region DLL Loading Support

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool SetDllDirectory(string lpPathName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr AddDllDirectory(string NewDirectory);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// DLL을 명시적으로 로드
        /// </summary>
        public static bool LoadDll(string dllPath)
        {
            if (!File.Exists(dllPath))
            {
                System.Diagnostics.Debug.WriteLine($"DLL not found: {dllPath}");
                return false;
            }

            IntPtr handle = LoadLibrary(dllPath);
            if (handle == IntPtr.Zero)
            {
                int error = Marshal.GetLastWin32Error();
                System.Diagnostics.Debug.WriteLine($"LoadLibrary failed for {dllPath}, Error: {error}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// AJIN DLL들을 미리 로드
        /// </summary>
        public static bool PreloadAjinDlls()
        {
            bool success = true;

            // AXL.dll 로드
            string axlPath = AXL_DllPath;
            if (File.Exists(axlPath))
            {
                if (!LoadDll(axlPath))
                    success = false;
            }

            // EzBasicAxl.dll 로드
            string ezPath = EzBasicAxl_DllPath;
            if (File.Exists(ezPath))
            {
                if (!LoadDll(ezPath))
                    success = false;
            }

            return success;
        }

        #endregion
    }
}
