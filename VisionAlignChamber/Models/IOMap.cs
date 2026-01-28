using System;
using System.Collections.Generic;

namespace VisionAlignChamber.Models
{
    /// <summary>
    /// IO 매핑 정의
    /// </summary>
    public static class IOMap
    {
        /// <summary>
        /// 입력 (DI) 정의
        /// </summary>
        public static class Input
        {
            // ===== 모듈 0 =====
            public const int EMO = 0;                    // 비상 정지
            public const int DOOR_OPEN = 1;              // 도어 오픈
            public const int START_SW = 2;               // 시작 스위치
            public const int STOP_SW = 3;                // 정지 스위치
            public const int RESET_SW = 4;               // 리셋 스위치

            // ===== 센서 =====
            public const int WAFER_DETECT = 10;          // 웨이퍼 감지
            public const int WAFER_ON_CHUCK = 11;        // 척 위 웨이퍼 감지
            public const int VACUUM_ON = 12;             // 진공 ON 확인
            public const int AIR_PRESSURE_OK = 13;       // 공압 정상

            // ===== 축 홈 센서 =====
            public const int AXIS0_HOME = 20;            // 축0 홈 센서
            public const int AXIS1_HOME = 21;            // 축1 홈 센서
            public const int AXIS2_HOME = 22;            // 축2 홈 센서
            public const int AXIS3_HOME = 23;            // 축3 홈 센서

            // ===== 축 리밋 센서 =====
            public const int AXIS0_LIMIT_PLUS = 30;      // 축0 + 리밋
            public const int AXIS0_LIMIT_MINUS = 31;     // 축0 - 리밋
            public const int AXIS1_LIMIT_PLUS = 32;      // 축1 + 리밋
            public const int AXIS1_LIMIT_MINUS = 33;     // 축1 - 리밋
        }

        /// <summary>
        /// 출력 (DO) 정의
        /// </summary>
        public static class Output
        {
            // ===== 램프 =====
            public const int LAMP_RED = 0;               // 적색 램프
            public const int LAMP_YELLOW = 1;            // 황색 램프
            public const int LAMP_GREEN = 2;             // 녹색 램프
            public const int BUZZER = 3;                 // 부저

            // ===== 솔레노이드 밸브 =====
            public const int VACUUM_SOL = 10;            // 진공 솔레노이드
            public const int BLOW_SOL = 11;              // 블로우 솔레노이드
            public const int CLAMP_SOL = 12;             // 클램프 솔레노이드

            // ===== 조명 =====
            public const int LIGHT_ON = 20;              // 조명 ON
            public const int LIGHT_STROBE = 21;          // 스트로브 조명
        }
    }

    /// <summary>
    /// IO 포인트 정보
    /// </summary>
    public class IOPoint
    {
        /// <summary>
        /// 모듈 번호
        /// </summary>
        public int ModuleNo { get; set; }

        /// <summary>
        /// 비트 번호
        /// </summary>
        public int BitNo { get; set; }

        /// <summary>
        /// IO 이름
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 설명
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 입력/출력 구분
        /// </summary>
        public bool IsOutput { get; set; }

        /// <summary>
        /// 액티브 로우 여부
        /// </summary>
        public bool ActiveLow { get; set; }

        public IOPoint(int moduleNo, int bitNo, string name, bool isOutput = false, bool activeLow = false)
        {
            ModuleNo = moduleNo;
            BitNo = bitNo;
            Name = name;
            IsOutput = isOutput;
            ActiveLow = activeLow;
        }

        public override string ToString()
        {
            return $"[{ModuleNo}.{BitNo}] {Name} ({(IsOutput ? "DO" : "DI")})";
        }
    }

    /// <summary>
    /// IO 테이블
    /// </summary>
    public class IOTable
    {
        private Dictionary<string, IOPoint> _inputs = new Dictionary<string, IOPoint>();
        private Dictionary<string, IOPoint> _outputs = new Dictionary<string, IOPoint>();

        /// <summary>
        /// 입력 포인트 추가
        /// </summary>
        public void AddInput(string key, int moduleNo, int bitNo, string name, bool activeLow = false)
        {
            _inputs[key] = new IOPoint(moduleNo, bitNo, name, false, activeLow);
        }

        /// <summary>
        /// 출력 포인트 추가
        /// </summary>
        public void AddOutput(string key, int moduleNo, int bitNo, string name, bool activeLow = false)
        {
            _outputs[key] = new IOPoint(moduleNo, bitNo, name, true, activeLow);
        }

        /// <summary>
        /// 입력 포인트 조회
        /// </summary>
        public IOPoint GetInput(string key)
        {
            return _inputs.TryGetValue(key, out var point) ? point : null;
        }

        /// <summary>
        /// 출력 포인트 조회
        /// </summary>
        public IOPoint GetOutput(string key)
        {
            return _outputs.TryGetValue(key, out var point) ? point : null;
        }

        /// <summary>
        /// 모든 입력 포인트
        /// </summary>
        public IEnumerable<IOPoint> AllInputs => _inputs.Values;

        /// <summary>
        /// 모든 출력 포인트
        /// </summary>
        public IEnumerable<IOPoint> AllOutputs => _outputs.Values;
    }
}
