using System;

namespace VisionAlignChamber.Models
{
    /// <summary>
    /// 시스템 제어권 (누가 제어하는가)
    /// </summary>
    public enum ControlAuthority
    {
        /// <summary>
        /// 로컬 제어 (UI)
        /// Manual/Setup 모드 사용 가능
        /// </summary>
        Local,

        /// <summary>
        /// 원격 제어 (CTC)
        /// Auto 모드만 사용, UI 조작 불가
        /// </summary>
        Remote,

        /// <summary>
        /// 잠금 상태 (EMO/Interlock)
        /// 모든 제어 불가
        /// </summary>
        Locked
    }
}
