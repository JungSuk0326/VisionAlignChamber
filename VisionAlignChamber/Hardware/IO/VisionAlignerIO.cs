using System;
using System.Collections.Generic;
using VisionAlignChamber.Interfaces;

namespace VisionAlignChamber.Hardware.IO
{
    /// <summary>
    /// Vision Aligner 전용 I/O Facade
    /// 복잡한 채널 번호 대신 의미있는 메서드로 I/O 제어
    /// </summary>
    public class VisionAlignerIO
    {
        #region Fields

        private readonly IDigitalIO _dio;
        private readonly IOMapping _mapping;

        #endregion

        #region Constructor

        public VisionAlignerIO(IDigitalIO digitalIO, IOMapping mapping)
        {
            _dio = digitalIO ?? throw new ArgumentNullException(nameof(digitalIO));
            _mapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
        }

        #endregion

        #region Generic I/O Methods

        /// <summary>
        /// Digital Input 읽기
        /// </summary>
        public bool GetInput(VADigitalInput input)
        {
            var ch = _mapping.GetDIChannel(input);
            return _dio.ReadInputBit(ch.ModuleNo, ch.Channel);
        }

        /// <summary>
        /// Digital Output 쓰기
        /// </summary>
        public void SetOutput(VADigitalOutput output, bool value)
        {
            var ch = _mapping.GetDOChannel(output);
            _dio.WriteOutputBit(ch.ModuleNo, ch.Channel, value);
        }

        /// <summary>
        /// Digital Output 읽기
        /// </summary>
        public bool GetOutput(VADigitalOutput output)
        {
            var ch = _mapping.GetDOChannel(output);
            return _dio.ReadOutputBit(ch.ModuleNo, ch.Channel);
        }

        #endregion

        #region Wafer Sensor

        /// <summary>
        /// 센서 1 웨이퍼 감지 확인
        /// </summary>
        public bool IsSensor1WaferDetected()
        {
            return GetInput(VADigitalInput.Sensor1_Wafer_Check);
        }

        /// <summary>
        /// 센서 2 웨이퍼 감지 확인
        /// </summary>
        public bool IsSensor2WaferDetected()
        {
            return GetInput(VADigitalInput.Sensor2_Wafer_Check);
        }

        /// <summary>
        /// 모든 센서에서 웨이퍼가 감지되었는지 확인
        /// </summary>
        public bool IsWaferDetectedOnAllSensors()
        {
            return IsSensor1WaferDetected() && IsSensor2WaferDetected();
        }

        #endregion

        #region Lift Pin Vacuum & Blow

        /// <summary>
        /// Lift Pin 진공 솔레노이드 제어
        /// </summary>
        public void SetLiftPinVacuum(bool on)
        {
            SetOutput(VADigitalOutput.LiftPin_VaccumSol, on);
        }

        /// <summary>
        /// Lift Pin 블로우 솔레노이드 제어
        /// </summary>
        public void SetLiftPinBlow(bool on)
        {
            SetOutput(VADigitalOutput.LiftPin_BlowSol, on);
        }

        /// <summary>
        /// Lift Pin 진공 솔레노이드 상태 확인
        /// </summary>
        public bool IsLiftPinVacuumOn()
        {
            return GetOutput(VADigitalOutput.LiftPin_VaccumSol);
        }

        /// <summary>
        /// Lift Pin 블로우 솔레노이드 상태 확인
        /// </summary>
        public bool IsLiftPinBlowOn()
        {
            return GetOutput(VADigitalOutput.LiftPin_BlowSol);
        }

        #endregion

        #region Chuck Vacuum & Blow

        /// <summary>
        /// Chuck 진공 솔레노이드 제어
        /// </summary>
        public void SetChuckVacuum(bool on)
        {
            SetOutput(VADigitalOutput.Chuck_VacuumSol, on);
        }

        /// <summary>
        /// Chuck 블로우 솔레노이드 제어
        /// </summary>
        public void SetChuckBlow(bool on)
        {
            SetOutput(VADigitalOutput.Chuck_BlowSol, on);
        }

        /// <summary>
        /// Chuck 진공 솔레노이드 상태 확인
        /// </summary>
        public bool IsChuckVacuumOn()
        {
            return GetOutput(VADigitalOutput.Chuck_VacuumSol);
        }

        /// <summary>
        /// Chuck 블로우 솔레노이드 상태 확인
        /// </summary>
        public bool IsChuckBlowOn()
        {
            return GetOutput(VADigitalOutput.Chuck_BlowSol);
        }

        #endregion

        #region PN Check Sensor

        /// <summary>
        /// PN Check P 센서 상태 확인
        /// </summary>
        public bool IsPNCheckP()
        {
            return GetInput(VADigitalInput.PN_Check_P);
        }

        /// <summary>
        /// PN Check N 센서 상태 확인
        /// </summary>
        public bool IsPNCheckN()
        {
            return GetInput(VADigitalInput.PN_Check_N);
        }

        /// <summary>
        /// PN Check 상태 확인 (P와 N 모두 확인)
        /// </summary>
        public (bool P, bool N) GetPNCheckStatus()
        {
            return (IsPNCheckP(), IsPNCheckN());
        }

        #endregion

        #region Vision Light

        /// <summary>
        /// Vision Light 제어
        /// </summary>
        public void SetVisionLight(bool on)
        {
            SetOutput(VADigitalOutput.Vision_Light, on);
        }

        /// <summary>
        /// Vision Light 상태 확인
        /// </summary>
        public bool IsVisionLightOn()
        {
            return GetOutput(VADigitalOutput.Vision_Light);
        }

        #endregion

        #region Status Summary

        /// <summary>
        /// 전체 I/O 상태 요약
        /// </summary>
        public IOStatusSummary GetStatusSummary()
        {
            return new IOStatusSummary
            {
                Sensor1WaferDetected = IsSensor1WaferDetected(),
                Sensor2WaferDetected = IsSensor2WaferDetected(),
                PNCheckP = IsPNCheckP(),
                PNCheckN = IsPNCheckN(),
                LiftPinVacuum = IsLiftPinVacuumOn(),
                LiftPinBlow = IsLiftPinBlowOn(),
                ChuckVacuum = IsChuckVacuumOn(),
                ChuckBlow = IsChuckBlowOn(),
                VisionLightOn = IsVisionLightOn()
            };
        }

        #endregion
    }

    /// <summary>
    /// I/O 상태 요약 정보
    /// </summary>
    public class IOStatusSummary
    {
        public bool Sensor1WaferDetected { get; set; }
        public bool Sensor2WaferDetected { get; set; }
        public bool PNCheckP { get; set; }
        public bool PNCheckN { get; set; }
        public bool LiftPinVacuum { get; set; }
        public bool LiftPinBlow { get; set; }
        public bool ChuckVacuum { get; set; }
        public bool ChuckBlow { get; set; }
        public bool VisionLightOn { get; set; }
    }
}
