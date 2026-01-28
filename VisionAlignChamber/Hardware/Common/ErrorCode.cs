using System;

namespace VisionAlignChamber.Hardware.Common
{
    /// <summary>
    /// 아진 에러 코드 정의
    /// </summary>
    public static class AjinErrorCode
    {
        // ========== 공통 에러 코드 ==========
        public const uint AXT_RT_SUCCESS = 0;                          // 성공
        public const uint AXT_RT_OPEN_ERROR = 1001;                    // 라이브러리 열기 실패
        public const uint AXT_RT_OPEN_ALREADY = 1002;                  // 이미 열려있음
        public const uint AXT_RT_NOT_OPEN = 1053;                      // 라이브러리가 열리지 않음
        public const uint AXT_RT_NOT_SUPPORT_VERSION = 1054;           // 지원하지 않는 버전

        // ========== 파라미터 에러 코드 ==========
        public const uint AXT_RT_INVALID_AXIS_NO = 4101;               // 유효하지 않은 축 번호
        public const uint AXT_RT_INVALID_MODULE_NO = 4102;             // 유효하지 않은 모듈 번호
        public const uint AXT_RT_INVALID_PARAMETER = 4151;             // 유효하지 않은 파라미터

        // ========== 모션 에러 코드 ==========
        public const uint AXT_RT_MOTION_NOT_READY = 4201;              // 모션 준비 안됨
        public const uint AXT_RT_MOTION_SERVO_OFF = 4202;              // 서보 OFF 상태
        public const uint AXT_RT_MOTION_ALARM_ON = 4203;               // 알람 발생
        public const uint AXT_RT_MOTION_LIMIT_ON = 4204;               // 리밋 센서 감지
        public const uint AXT_RT_MOTION_BUSY = 4205;                   // 이동 중

        // ========== 통신 에러 코드 ==========
        public const uint AXT_RT_NETWORK_ERROR = 5001;                 // 네트워크 에러
        public const uint AXT_RT_NETWORK_SLAVE_ERROR = 5002;           // 슬레이브 에러

        /// <summary>
        /// 에러 코드를 문자열로 변환
        /// </summary>
        public static string GetErrorMessage(uint errorCode)
        {
            switch (errorCode)
            {
                case AXT_RT_SUCCESS:
                    return "성공";
                case AXT_RT_OPEN_ERROR:
                    return "라이브러리 열기 실패";
                case AXT_RT_OPEN_ALREADY:
                    return "이미 열려있음";
                case AXT_RT_NOT_OPEN:
                    return "라이브러리가 열리지 않음";
                case AXT_RT_NOT_SUPPORT_VERSION:
                    return "지원하지 않는 버전";
                case AXT_RT_INVALID_AXIS_NO:
                    return "유효하지 않은 축 번호";
                case AXT_RT_INVALID_MODULE_NO:
                    return "유효하지 않은 모듈 번호";
                case AXT_RT_INVALID_PARAMETER:
                    return "유효하지 않은 파라미터";
                case AXT_RT_MOTION_NOT_READY:
                    return "모션 준비 안됨";
                case AXT_RT_MOTION_SERVO_OFF:
                    return "서보 OFF 상태";
                case AXT_RT_MOTION_ALARM_ON:
                    return "알람 발생";
                case AXT_RT_MOTION_LIMIT_ON:
                    return "리밋 센서 감지";
                case AXT_RT_MOTION_BUSY:
                    return "이동 중";
                case AXT_RT_NETWORK_ERROR:
                    return "네트워크 에러";
                case AXT_RT_NETWORK_SLAVE_ERROR:
                    return "슬레이브 에러";
                default:
                    return $"알 수 없는 에러 (0x{errorCode:X8})";
            }
        }

        /// <summary>
        /// 에러 여부 확인
        /// </summary>
        public static bool IsError(uint errorCode)
        {
            return errorCode != AXT_RT_SUCCESS;
        }

        /// <summary>
        /// 성공 여부 확인
        /// </summary>
        public static bool IsSuccess(uint errorCode)
        {
            return errorCode == AXT_RT_SUCCESS;
        }
    }

    /// <summary>
    /// 모션 예외
    /// </summary>
    public class MotionException : Exception
    {
        public uint ErrorCode { get; }

        public MotionException(uint errorCode)
            : base(AjinErrorCode.GetErrorMessage(errorCode))
        {
            ErrorCode = errorCode;
        }

        public MotionException(uint errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public MotionException(string message)
            : base(message)
        {
            ErrorCode = 0;
        }

        public MotionException(string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = 0;
        }
    }
}
