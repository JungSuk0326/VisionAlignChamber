using System;

namespace VisionAlignChamber.Interlock
{
    /// <summary>
    /// 알람 심각도 레벨
    /// </summary>
    public enum AlarmSeverity
    {
        /// <summary>정보성 (동작에 영향 없음)</summary>
        Info = 0,

        /// <summary>경고 (주의 필요, 동작 가능)</summary>
        Warning = 1,

        /// <summary>에러 (해당 기능 사용 불가)</summary>
        Error = 2,

        /// <summary>심각 (시스템 정지 필요)</summary>
        Critical = 3
    }

    /// <summary>
    /// 알람 카테고리
    /// </summary>
    public enum AlarmCategory
    {
        /// <summary>시스템 관련</summary>
        System,

        /// <summary>모션 관련</summary>
        Motion,

        /// <summary>비전 관련</summary>
        Vision,

        /// <summary>센서 관련</summary>
        Sensor,

        /// <summary>I/O 관련</summary>
        IO,

        /// <summary>통신 관련</summary>
        Communication,

        /// <summary>사용자 정의</summary>
        User
    }

    /// <summary>
    /// 인터락 정의 클래스
    /// 각 인터락 조건과 관련 정보를 정의합니다.
    /// </summary>
    public class InterlockDefinition
    {
        #region Properties

        /// <summary>
        /// 인터락 ID (고유 식별자)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 인터락 코드 (예: "IL001", "MTN_001")
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 인터락 이름
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 인터락 설명
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 심각도 레벨
        /// </summary>
        public AlarmSeverity Severity { get; set; }

        /// <summary>
        /// 알람 카테고리
        /// </summary>
        public AlarmCategory Category { get; set; }

        /// <summary>
        /// 활성화 여부
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 자동 복구 가능 여부
        /// </summary>
        public bool AutoRecoverable { get; set; } = false;

        /// <summary>
        /// 복구 방법 안내
        /// </summary>
        public string RecoveryGuide { get; set; }

        #endregion

        #region Constructor

        public InterlockDefinition()
        {
        }

        public InterlockDefinition(int id, string code, string name, AlarmSeverity severity, AlarmCategory category)
        {
            Id = id;
            Code = code;
            Name = name;
            Severity = severity;
            Category = category;
        }

        #endregion
    }

    /// <summary>
    /// 알람 발생 정보 클래스
    /// 실제 발생한 알람의 상세 정보를 담습니다.
    /// </summary>
    public class AlarmInfo
    {
        #region Properties

        /// <summary>
        /// 인터락 정의 참조
        /// </summary>
        public InterlockDefinition Definition { get; set; }

        /// <summary>
        /// 알람 발생 시각
        /// </summary>
        public DateTime OccurredTime { get; set; }

        /// <summary>
        /// 알람 해제 시각 (null이면 미해제)
        /// </summary>
        public DateTime? ClearedTime { get; set; }

        /// <summary>
        /// 활성 상태 여부
        /// </summary>
        public bool IsActive => ClearedTime == null;

        /// <summary>
        /// 추가 메시지
        /// </summary>
        public string AdditionalMessage { get; set; }

        /// <summary>
        /// 발생 소스 (어디서 발생했는지)
        /// </summary>
        public string Source { get; set; }

        #endregion

        #region Constructor

        public AlarmInfo()
        {
            OccurredTime = DateTime.Now;
        }

        public AlarmInfo(InterlockDefinition definition, string source = null, string additionalMessage = null)
        {
            Definition = definition;
            OccurredTime = DateTime.Now;
            Source = source;
            AdditionalMessage = additionalMessage;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 알람 해제 처리
        /// </summary>
        public void Clear()
        {
            ClearedTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Definition?.Code}] {Definition?.Name} - {OccurredTime:yyyy-MM-dd HH:mm:ss}";
        }

        #endregion
    }
}
