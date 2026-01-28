/****************************************************************************
*****************************************************************************
**
** File Name
** ---------
**
** AXHS.CS
**
** COPYRIGHT (c) AJINEXTEK Co., LTD
**
*****************************************************************************
*****************************************************************************
**
** Description
** -----------
** Resource Define Header File
** 
**
*****************************************************************************
*****************************************************************************
**
** Source Change Indices
** ---------------------
** 
** (None)
**
**
*****************************************************************************
*****************************************************************************
**
** Website
** ---------------------
**
** http://www.ajinextek.com
**
*****************************************************************************
*****************************************************************************
*/

using System;
using System.Runtime.InteropServices;
using System.Collections.Specialized;

// Define DCB Structure
public struct DCB
{
    public uint DCBLength;
    public uint BaudRate;
    public BitVector32 Flags;

    public ushort wReserved;        // not currently used 
    public ushort XonLim;           // transmit XON threshold 
    public ushort XoffLim;          // transmit XOFF threshold             

    public byte ByteSize;
    public byte Parity;
    public byte StopBits;

    public sbyte XonChar;          // Tx and Rx XON character 
    public sbyte XoffChar;         // Tx and Rx XOFF character 
    public sbyte ErrorChar;        // error replacement character 
    public sbyte EofChar;          // end of input character 
    public sbyte EvtChar;          // received event character 
    public ushort wReserved1;       // reserved; do not use     

    public static readonly int fBinary;
    public static readonly int fParity;
    public static readonly int fOutxCtsFlow;
    public static readonly int fOutxDsrFlow;
    public static readonly BitVector32.Section fDtrControl;
    public static readonly int fDsrSensitivity;
    public static readonly int fTXContinueOnXoff;
    public static readonly int fOutX;
    public static readonly int fInX;
    public static readonly int fErrorChar;
    public static readonly int fNull;
    public static readonly BitVector32.Section fRtsControl;
    public static readonly int fAbortOnError;

    static DCB()
    {
        // Create Boolean Mask
        int previousMask;
        fBinary = BitVector32.CreateMask();
        fParity = BitVector32.CreateMask(fBinary);
        fOutxCtsFlow = BitVector32.CreateMask(fParity);
        fOutxDsrFlow = BitVector32.CreateMask(fOutxCtsFlow);
        previousMask = BitVector32.CreateMask(fOutxDsrFlow);
        previousMask = BitVector32.CreateMask(previousMask);
        fDsrSensitivity = BitVector32.CreateMask(previousMask);
        fTXContinueOnXoff = BitVector32.CreateMask(fDsrSensitivity);
        fOutX = BitVector32.CreateMask(fTXContinueOnXoff);
        fInX = BitVector32.CreateMask(fOutX);
        fErrorChar = BitVector32.CreateMask(fInX);
        fNull = BitVector32.CreateMask(fErrorChar);
        previousMask = BitVector32.CreateMask(fNull);
        previousMask = BitVector32.CreateMask(previousMask);
        fAbortOnError = BitVector32.CreateMask(previousMask);

        // Create section Mask
        BitVector32.Section previousSection;
        previousSection = BitVector32.CreateSection(1);
        previousSection = BitVector32.CreateSection(1, previousSection);
        previousSection = BitVector32.CreateSection(1, previousSection);
        previousSection = BitVector32.CreateSection(1, previousSection);
        fDtrControl = BitVector32.CreateSection(2, previousSection);
        previousSection = BitVector32.CreateSection(1, fDtrControl);
        previousSection = BitVector32.CreateSection(1, previousSection);
        previousSection = BitVector32.CreateSection(1, previousSection);
        previousSection = BitVector32.CreateSection(1, previousSection);
        previousSection = BitVector32.CreateSection(1, previousSection);
        previousSection = BitVector32.CreateSection(1, previousSection);
        fRtsControl = BitVector32.CreateSection(3, previousSection);
        previousSection = BitVector32.CreateSection(1, fRtsControl);
    }

    public bool Binary
    {
        get { return Flags[fBinary]; }
        set { Flags[fBinary] = value; }
    }

    public bool CheckParity
    {
        get { return Flags[fParity]; }
        set { Flags[fParity] = value; }
    }

    public bool OutxCtsFlow
    {
        get { return Flags[fOutxCtsFlow]; }
        set { Flags[fOutxCtsFlow] = value; }
    }

    public bool OutxDsrFlow
    {
        get { return Flags[fOutxDsrFlow]; }
        set { Flags[fOutxDsrFlow]  = value; }
    }

    public int DtrControl
    {
        get { return Flags[fDtrControl]; }
        set { Flags[fDtrControl] = (int)value; }
    }

    public bool DsrSensitivity
    {
        get { return Flags[fDsrSensitivity]; }
        set { Flags[fDsrSensitivity] = value; }
    }

    public bool TxContinueOnXoff
    {
        get { return Flags[fTXContinueOnXoff]; }
        set { Flags[fTXContinueOnXoff] = value; }
    }

    public bool OutX
    {
        get { return Flags[fOutX]; }
        set { Flags[fOutX] = value; }
    }

    public bool InX
    {
        get { return Flags[fInX]; }
        set { Flags[fInX] = value; }
    }

    public bool ReplaceErrorChar
    {
        get { return Flags[fErrorChar]; }
        set { Flags[fErrorChar] = value; }
    }

    public bool Null
    {
        get { return Flags[fNull]; }
        set { Flags[fNull] = value; }
    }

    public int RtsControl
    {
        get { return Flags[fRtsControl]; }
        set { Flags[fRtsControl]  = (int)value; }
    }

    public bool AbortOnError
    {
        get { return Flags[fAbortOnError]; }
        set { Flags[fAbortOnError] = value; }
    }
}

// Define COMMTIMEOUTS Structure
public struct COMMTIMEOUTS
{
    public int ReadIntervalTimeout;
    public int ReadTotalTimeoutMultiplier;
    public int ReadTotalTimeoutConstant;
    public int WriteTotalTimeoutMultiplier;
    public int WriteTotalTimeoutConstant;
}

// Define OVERLAPPED Structure
public struct OVERLAPPED
{
    public uint Internal;
    public uint InternalHigh;
    public uint Offset;
    public uint OffsetHigh;
    public IntPtr Pointer;
    public IntPtr hEvent;
}

// Define COMSTAT Structure
public struct COMSTAT
{
    public const uint fCtsHold = 0x1;
    public const uint fDsrHold = 0x2;
    public const uint fRlsdHold = 0x4;
    public const uint fXoffHold = 0x8;
    public const uint fXoffSent = 0x10;
    public const uint fEof = 0x20;
    public const uint fTxim = 0x40;
    public UInt32 Flags;
    public UInt32 cbInQue;
    public UInt32 cbOutQue;
}

// Define Baseboard
public enum AXT_BASE_BOARD:uint
{
    AXT_UNKNOWN                                             = 0x00,    // Unknown Baseboard
    AXT_BIHR                                                = 0x01,    // ISA bus, Half size
    AXT_BIFR                                                = 0x02,    // ISA bus, Full size
    AXT_BPHR                                                = 0x03,    // PCI bus, Half size
    AXT_BPFR                                                = 0x04,    // PCI bus, Full size
    AXT_BV3R                                                = 0x05,    // VME bus, 3U size
    AXT_BV6R                                                = 0x06,    // VME bus, 6U size
    AXT_BC3R                                                = 0x07,    // cPCI bus, 3U size
    AXT_BC6R                                                = 0x08,    // cPCI bus, 6U size
    AXT_BEHR                                                = 0x09,    // PCIe bus, Half size
    AXT_BEFR                                                = 0x0A,    // PCIe bus, Full size
    AXT_BEHD                                                = 0x0B,    // PCIe bus, Half size, DB-32T
    AXT_FMNSH4D                                             = 0x52,    // ISA bus, Full size, DB-32T, SIO-2V03 * 2
    AXT_PCI_DI64R                                           = 0x43,    // PCI bus, Digital IN 64ch
    AXT_PCIE_DI64R                                          = 0x44,    // PCIe bus, Digital IN 64ch
    AXT_PCI_DO64R                                           = 0x53,    // PCI bus, Digital OUT 64ch
    AXT_PCIE_DO64R                                          = 0x54,    // PCIe bus, Digital OUT 64ch
    AXT_PCI_DB64R                                           = 0x63,    // PCI bus, Digital IN 32ch, OUT 32ch
    AXT_PCIE_DB64R                                          = 0x64,    // PCIe bus, Digital IN 32ch, OUT 32ch
    AXT_BPFR_COM                                            = 0x70,    // PCI bus, Full size, For serial function modules.
    AXT_PCIN204                                             = 0x82,    // PCI bus, Half size On-Board 2 Axis controller.
    AXT_BPHD                                                = 0x83,    // PCI bus, Half size, DB-32T
    AXT_PCIN404                                             = 0x84,    // PCI bus, Half size On-Board 4 Axis controller.    
    AXT_PCIN804                                             = 0x85,    // PCI bus, Half size On-Board 8 Axis controller.
    AXT_PCIEN804                                            = 0x86,    // PCIe bus, Half size On-Board 8 Axis controller.
    AXT_PCIEN404                                            = 0x87,    // PCIe bus, Half size On-Board 4 Axis controller.
    AXT_PCI_AIO1602HR                                       = 0x93,    // PCI bus, Half size, AI-16ch, AO-2ch AI16HR
    AXT_PCI_R1604                                           = 0xC1,    // PCI bus[PCI9030], Half size, RTEX based 16 axis controller
    AXT_PCI_R3204                                           = 0xC2,    // PCI bus[PCI9030], Half size, RTEX based 32 axis controller
    AXT_PCI_R32IO                                           = 0xC3,    // PCI bus[PCI9030], Half size, RTEX based IO only.
    AXT_PCI_REV2                                            = 0xC4,    // Reserved.
    AXT_PCI_R1604MLII                                       = 0xC5,    // PCI bus[PCI9030], Half size, Mechatrolink-II 16/32 axis controller.
    AXT_PCI_R0804MLII                                       = 0xC6,    // PCI bus[PCI9030], Half size, Mechatrolink-II 08 axis controller.
    AXT_PCI_Rxx00MLIII                                      = 0xC7,    // PCI Board, Mechatrolink III AXT, PCI Bus[PCI9030], Half size, Max.32 Slave module support
    AXT_PCIE_Rxx00MLIII                                     = 0xC8,    // PCI Express Board, Mechatrolink III AXT, PCI Bus[], Half size, Max.32 Slave module support
    AXT_PCP2_Rxx00MLIII                                     = 0xC9,    // PCI/104-Plus Board, Mechatrolink III AXT, PCI Bus[], Half size, Max.32 Slave module support
    AXT_PCI_R1604SIIIH                                      = 0xCA,    // PCI bus[PCI9030], Half size, SSCNET III / H 16/32 axis controller.
    AXT_PCI_R32IOEV                                         = 0xCB,    // PCI bus[PCI9030], Half size, RTEX based IO only. Economic version.
    AXT_PCIE_R0804RTEX                                      = 0xCC,    // PCIe bus, Half size, Half size, RTEX based 08 axis controller.
    AXT_PCIE_R1604RTEX                                      = 0xCD,    // PCIe bus, Half size, Half size, RTEX based 16 axis controller.
    AXT_PCIE_R2404RTEX                                      = 0xCE,    // PCIe bus, Half size, Half size, RTEX based 24 axis controller.
    AXT_PCIE_R3204RTEX                                      = 0xCF,    // PCIe bus, Half size, Half size, RTEX based 32 axis controller.
    AXT_PCIE_Rxx04SIIIH                                     = 0xD0,    // PCIe bus, Half size, SSCNET III / H 16(or 32)-axis(CAMC-QI based) controller.
    AXT_PCIE_Rxx00SIIIH                                     = 0xD1,    // PCIe bus, Half size, SSCNET III / H Max. 32-axis(DSP Based) controller.
    AXT_ETHERCAT_RTOSV5                                     = 0xD2,    // EtherCAT, RTOS(On Time), Version 5.29
    AXT_PCI_Nx04_A                                          = 0xD3,    // PCI bus, Half size On-Board x Axis controller For Rtos.
    AXT_PCI_R3200MLIII_IO                                   = 0xD4,    // PCI Board, Mechatrolink III AXT, PCI Bus[PCI9030], Half size, Max.32 IO only	controller
    AXT_PCIE_Rxx05MLIII                                     = 0xD5,    // PCIe bus, Half size, Mechatrolink III 32 axis(DSP Based) controller.
    AXT_PCIE_Rxx05SIIIH                                     = 0xD6,    // PCIe bus, Half size, Sscnet III / H  32 axis(DSP Based) controller.
    AXT_PCIE_Rxx05RTEX                                      = 0xD7     // PCIe bus, Half size, RTEX 32 axis(DSP Based) controller.
}

// Define Module
public enum AXT_MODULE:uint
{
    AXT_UNKNOWN                                             = 0x00,    // Unknown Baseboard                                         
    AXT_SMC_2V01                                            = 0x01,    // CAMC-5M, 2 Axis
    AXT_SMC_2V02                                            = 0x02,    // CAMC-FS, 2 Axis
    AXT_SMC_1V01                                            = 0x03,    // CAMC-5M, 1 Axis
    AXT_SMC_1V02                                            = 0x04,    // CAMC-FS, 1 Axis
    AXT_SMC_2V03                                            = 0x05,    // CAMC-IP, 2 Axis
    AXT_SMC_4V04                                            = 0x06,    // CAMC-QI, 4 Axis
    AXT_SMC_R1V04A4                                         = 0x07,    // CAMC-QI, 1 Axis, For RTEX A4 slave only
    AXT_SMC_1V03                                            = 0x08,    // CAMC-IP, 1 Axis
    AXT_SMC_R1V04                                           = 0x09,    // CAMC-QI, 1 Axis, For RTEX SLAVE only(Pulse Output Module)
    AXT_SMC_R1V04MLIISV                                     = 0x0A,    // CAMC-QI, 1 Axis, For Sigma-X series.
    AXT_SMC_R1V04MLIIPM                                     = 0x0B,    // 2 Axis, For Pulse output series(JEPMC-PL2910).
    AXT_SMC_2V04                                            = 0x0C,    // CAMC-QI, 2 Axis
    AXT_SMC_R1V04A5                                         = 0x0D,    // CAMC-QI, 1 Axis, For RTEX A5N slave only
    AXT_SMC_R1V04MLIICL                                     = 0x0E,    // CAMC-QI, 1 Axis, For MLII Convex Linear only
    AXT_SMC_R1V04MLIICR                                     = 0x0F,    // CAMC-QI, 1 Axis, For MLII Convex Rotary only
    AXT_SMC_R1V04PM2Q                                       = 0x10,    // CAMC-QI, 2 Axis, For RTEX SLAVE only(Pulse Output Module)
    AXT_SMC_R1V04PM2QE                                      = 0x11,    // CAMC-QI, 4 Axis, For RTEX SLAVE only(Pulse Output Module)
    AXT_SMC_R1V04MLIIORI                                    = 0x12,    // CAMC-QI, 1 Axis, For MLII Oriental Step Driver only
    AXT_SMC_R1V04A6                                         = 0x13,    // CAMC-QI, 1 Axis, For RTEX A5N slave only
    AXT_SMC_R1V04SIIIHMIV                                   = 0x14,    // CAMC-QI, 1 Axis, For SSCNETIII/H MRJ4
    AXT_SMC_R1V04SIIIHMIV_R                                 = 0x15,    // CAMC-QI, 1 Axis, For SSCNETIII/H MRJ4
    AXT_SMC_R1V04SIIIHME                                    = 0x16,    // CAMC-QI, 1 Axis, For SSCNETIII/H MRJE
    AXT_SMC_R1V04SIIIHME_R                                  = 0x17,    // CAMC-QI, 1 Axis, For SSCNETIII/H MRJE
    AXT_SMC_R1V04MLIIS7                                     = 0x1D,    // CAMC-QI, 1 Axis, For ML-II/YASKWA Sigma7(SGD7S)
    AXT_SMC_R1V04MLIIISV                                    = 0x20,    // DSP, 1 Axis, For ML-3 SigmaV/YASKAWA only 
    AXT_SMC_R1V04MLIIIPM                                    = 0x21,    // DSP, 1 Axis, For ML-3 SLAVE/AJINEXTEK only(Pulse Output Module)
    AXT_SMC_R1V04MLIIISV_MD                                 = 0x22,    // DSP, 1 Axis, For ML-3 SigmaV-MD/YASKAWA only (Multi-Axis Control amp)
    AXT_SMC_R1V04MLIIIS7S                                   = 0x23,    // DSP, 1 Axis, For ML-3 Sigma7S/YASKAWA only
    AXT_SMC_R1V04MLIIIS7W                                   = 0x24,    // DSP, 2 Axis, For ML-3 Sigma7W/YASKAWA only(Dual-Axis control amp)
    AXT_SMC_R1V04MLIIIRS2                                   = 0x25,    // DSP, 1 Axis, For ML-3 RS2A/SANYO DENKY
    AXT_SMC_R1V04MLIIIS7_MD                                 = 0x26,    // DSP, 1 Axis, For ML-3 Sigma7-MD/YASKAWA only (Multi-Axis Control amp)
    AXT_SMC_R1V04PM2QSIIIH                                  = 0X60,    // CAMC-QI, 2Axis For SSCNETIII/H SLAVE only(Pulse Output Module)
    AXT_SMC_R1V04PM4QSIIIH                                  = 0X61,    // CAMC-QI, 4Axis For SSCNETIII/H SLAVE only(Pulse Output Module)
    AXT_SIO_RCNT2SIIIH                                      = 0X62,    // Counter slave module, Reversible counter, 2 channels, For SSCNETIII/H Only
    AXT_SIO_RDI32SIIIH                                      = 0X63,    // DI slave module, SSCNETIII AXT, IN 32-Channel
    AXT_SIO_RDO32SIIIH                                      = 0X64,    // DO slave module, SSCNETIII AXT, OUT 32-Channel
    AXT_SIO_RDB32SIIIH                                      = 0X65,    // DB slave module, SSCNETIII AXT, IN 16-Channel, OUT 16-Channel
    AXT_SIO_RAI16SIIIH                                      = 0X66,    // AI slave module, SSCNETIII AXT, Analog IN 16ch, 16 bit
    AXT_SIO_RAO08SIIIH                                      = 0X67,    // A0 slave module, SSCNETIII AXT, Analog OUT 8ch, 16 bit
    AXT_SMC_R1V04PM2QSIIIH_R                                = 0X68,    // CAMC-QI, 2Axis For SSCNETIII/H SLAVE only(Pulse Output Module) 
    AXT_SMC_R1V04PM4QSIIIH_R                                = 0X69,    // CAMC-QI, 4Axis For SSCNETIII/H SLAVE only(Pulse Output Module) 
    AXT_SIO_RCNT2SIIIH_R                                    = 0X6A,    // Counter slave module, Reversible counter, 2 channels, For SSCNETIII/H Only 
    AXT_SIO_RDI32SIIIH_R                                    = 0X6B,    // DI slave module, SSCNETIII AXT, IN 32-Channel 
    AXT_SIO_RDO32SIIIH_R                                    = 0X6C,    // DO slave module, SSCNETIII AXT, OUT 32-Channel 
    AXT_SIO_RDB32SIIIH_R                                    = 0X6D,    // DB slave module, SSCNETIII AXT, IN 16-Channel, OUT 16-Channel 
    AXT_SIO_RAI16SIIIH_R                                    = 0X6E,    // AI slave module, SSCNETIII AXT, Analog IN 16ch, 16 bit 
    AXT_SIO_RAO08SIIIH_R                                    = 0X6F,    // A0 slave module, SSCNETIII AXT, Analog OUT 8ch, 16 bit 
    AXT_SIO_RDI32MLIII                                      = 0x70,    // DI slave module, MechatroLink III AXT, IN 32-Channel NPN
    AXT_SIO_RDO32MLIII                                      = 0x71,    // DO slave module, MechatroLink III AXT, OUT 32-Channel  NPN
    AXT_SIO_RDB32MLIII                                      = 0x72,    // DB slave module, MechatroLink III AXT, IN 16-Channel, OUT 16-Channel  NPN
    AXT_SIO_RDI32MSMLIII                                    = 0x73,    // DI slave module, MechatroLink III M-SYSTEM, IN 32-Channel
    AXT_SIO_RDO32AMSMLIII                                   = 0x74,    // DO slave module, MechatroLink III M-SYSTEM, OUT 32-Channel
    AXT_SIO_RDI32PMLIII                                     = 0x75,    // DI slave module, MechatroLink III AXT, IN 32-Channel PNP
    AXT_SIO_RDO32PMLIII                                     = 0x76,    // DO slave module, MechatroLink III AXT, OUT 32-Channel  PNP
    AXT_SIO_RDB32PMLIII                                     = 0x77,    // DB slave module, MechatroLink III AXT, IN 16-Channel, OUT 16-Channel  PNP
    AXT_SIO_RDI16MLIII                                      = 0x78,    // DI slave module, MechatroLink III M-SYSTEM, IN 16-Channel
    AXT_SIO_UNDEFINEMLIII                                   = 0x79,    // IO slave module, MechatroLink III Any, Max. IN 480-Channel, Max. OUT 480-Channel
    AXT_SIO_RDI32MLIIISFA                                   = 0x7A,    // DI slave module, MechatroLink III AXT(SFA), IN 32-Channel NPN
    AXT_SIO_RDO32MLIIISFA                                   = 0x7B,    // DO slave module, MechatroLink III AXT(SFA), OUT 32-Channel  NPN
    AXT_SIO_RDB32MLIIISFA                                   = 0x7C,    // DB slave module, MechatroLink III AXT(SFA), IN 16-Channel, OUT 16-Channel  NPN
    AXT_SIO_RDB128MLIIIAI                                   = 0x7D,    // Intelligent I/O (Product by IAI), For MLII only
    AXT_SIO_RSIMPLEIOMLII                                   = 0x7E,    // Digital IN/Out xxch, Simple I/O sereies, MLII only.
    AXT_SIO_RDO16AMLIII                                     = 0x7F,    // DO slave module, MechatroLink III M-SYSTEM, OUT 16-Channel, NPN
    AXT_SIO_RDI16MLII                                       = 0x80,    // DISCRETE INPUT MODULE, 16 points (Product by M-SYSTEM), For MLII only
    AXT_SIO_RDO16AMLII                                      = 0x81,    // NPN TRANSISTOR OUTPUT MODULE, 16 points (Product by M-SYSTEM), For MLII only
    AXT_SIO_RDO16BMLII                                      = 0x82,    // PNP TRANSISTOR OUTPUT MODULE, 16 points (Product by M-SYSTEM), For MLII only 
    AXT_SIO_RDB96MLII                                       = 0x83,    // Digital IN/OUT(Selectable), MAX 96 points, For MLII only
    AXT_SIO_RDO32RTEX                                       = 0x84,    // Digital OUT 32ch
    AXT_SIO_RDI32RTEX                                       = 0x85,    // Digital IN  32ch
    AXT_SIO_RDB32RTEX                                       = 0x86,    // Digital IN/OUT 32ch
    AXT_SIO_RDO32RTEXNT1_D1                                 = 0x87,    // Digital OUT 32ch IntekPlus only
    AXT_SIO_RDI32RTEXNT1_D1                                 = 0x88,    // Digital IN  32ch IntekPlus only
    AXT_SIO_RDB32RTEXNT1_D1                                 = 0x89,    // Digital IN/OUT 32ch IntekPlus only
    AXT_SIO_RDO16BMLIII                                   	= 0x8A,    // DO slave module, MechatroLink III M-SYSTEM, OUT 16-Channel, PNP
    AXT_SIO_RDB32ML2NT1                                   	= 0x8B,    // DB slave module, MechatroLink2 AJINEXTEK NT1 Series
    AXT_SIO_RDB128YSMLIII                                   = 0x8C,    // IO slave module, MechatroLink III Any, Max. IN 480-Channel, Max. OUT 480-Channel
    AXT_SIO_DI32_P                                          = 0x92,    // Digital IN  32ch, PNP type(source type)
    AXT_SIO_DO32T_P                                         = 0x93,    // Digital OUT 32ch, Power TR, PNT type(source type)
    AXT_SIO_RDB128MLII                                      = 0x94,    // Digital IN  64ch / OUT 64ch, MLII only(JEPMC-IO2310)
    AXT_SIO_RDI32                                           = 0x95,    // Digital IN  32ch, For RTEX only
    AXT_SIO_RDO32                                           = 0x96,    // Digital OUT 32ch, For RTEX only
    AXT_SIO_DI32                                            = 0x97,    // Digital IN  32ch
    AXT_SIO_DO32P                                           = 0x98,    // Digital OUT 32ch
    AXT_SIO_DB32P                                           = 0x99,    // Digital IN  16ch / OUT 16ch
    AXT_SIO_RDB32T                                          = 0x9A,    // Digital IN  16ch / OUT 16ch, For RTEX only
    AXT_SIO_DO32T                                           = 0x9E,    // Digital OUT 16ch, Power TR OUTPUT
    AXT_SIO_DB32T                                           = 0x9F,    // Digital IN  16ch / OUT 16ch, Power TR OUTPUT
    AXT_SIO_RAI16RB                                         = 0xA0,    // A0h(160) : AI 16Ch, 16 bit, For RTEX only
    AXT_SIO_AI4RB                                           = 0xA1,    // A1h(161) : AI 4Ch, 12 bit
    AXT_SIO_AO4RB                                           = 0xA2,    // A2h(162) : AO 4Ch, 12 bit
    AXT_SIO_AI16H                                           = 0xA3,    // A3h(163) : AI 4Ch, 16 bit
    AXT_SIO_AO8H                                            = 0xA4,    // A4h(164) : AO 4Ch, 16 bit
    AXT_SIO_AI16HB                                          = 0xA5,    // A5h(165) : AI 16Ch, 16 bit (SIO-AI16HR(input module))
    AXT_SIO_AO2HB                                           = 0xA6,    // A6h(166) : AO 2Ch, 16 bit  (SIO-AI16HR(output module))
    AXT_SIO_RAI8RB                                          = 0xA7,    // A7h(167) : AI 8Ch, 16 bit, For RTEX only        
    AXT_SIO_RAO4RB                                          = 0xA8,    // A8h(168) : AO 4Ch, 16 bit, For RTEX only
    AXT_SIO_RAI4MLII                                        = 0xA9,    // A9h(169) : AI 4Ch, 16 bit, For MLII only
    AXT_SIO_RAO2MLII                                        = 0xAA,    // AAh(170) : AO 2Ch, 16 bit, For MLII only
    AXT_SIO_RAVCI4MLII                                      = 0xAB,    // DC VOLTAGE/CURRENT INPUT MODULE, 4 points (Product by M-SYSTEM), For MLII only
    AXT_SIO_RAVO2MLII                                       = 0xAC,    // DC VOLTAGE OUTPUT MODULE, 2 points (Product by M-SYSTEM), For MLII only
    AXT_SIO_RACO2MLII                                       = 0xAD,    // DC CURRENT OUTPUT MODULE, 2 points (Product by M-SYSTEM), For MLII only
    AXT_SIO_RATI4MLII                                       = 0xAE,    // THERMOCOUPLE INPUT MODULE, 4 points (Product by M-SYSTEM), For MLII only
    AXT_SIO_RARTDI4MLII                                     = 0xAF,    // RTD INPUT MODULE, 4 points (Product by M-SYSTEM), For MLII only
    AXT_SIO_RCNT2MLII                                       = 0xB0,    // Counter slave module, Reversible counter, 2 channels (Product by YASKWA), For MLII only
    AXT_SIO_CN2CH                                           = 0xB1,    // Counter Module, 2 channels, Remapped ID, Actual ID is (0xA8)
    AXT_SIO_RCNT2RTEX                                       = 0xB2,    // Counter slave module, Reversible counter, 2 channels, For RTEX Only
    AXT_SIO_RCNT2MLIII                                      = 0xB3,    // Counter slave moudle, MechatroLink III AXT, 2 ch, Trigger per channel
    AXT_SIO_RHPC4MLIII                                      = 0xB4,    // Counter slave moudle, MechatroLink III AXT, 4 ch
    AXT_SIO_RAI16RTEX                                       = 0xC0,    // ANALOG VOLTAGE INPUT(+- 10V) 16 Channel RTEX 
    AXT_SIO_RAO08RTEX                                       = 0xC1,    // ANALOG VOLTAGE OUTPUT(+- 10V) 08 Channel RTEX
    AXT_SIO_RAI8MLIII                                       = 0xC2,    // AI slave module, MechatroLink III AXT, Analog IN 8ch, 16 bit
    AXT_SIO_RAI16MLIII                                      = 0xC3,    // AI slave module, MechatroLink III AXT, Analog IN 16ch, 16 bit
    AXT_SIO_RAO4MLIII                                       = 0xC4,    // A0 slave module, MechatroLink III AXT, Analog OUT 4ch, 16 bit
    AXT_SIO_RAO8MLIII                                       = 0xC5,    // A0 slave module, MechatroLink III AXT, Analog OUT 8ch, 16 bit
    AXT_SIO_RAVO4MLII                                       = 0xC6,    // DC VOLTAGE OUTPUT MODULE, 4 points (Product by M-SYSTEM), For MLII only
    AXT_SIO_RAV04MLIII                                      = 0xC7,    // AO Slave module, MechatroLink III M-SYSTEM Voltage output module
    AXT_SIO_RAVI4MLIII                                      = 0xC8,    // AI Slave module, MechatroLink III M-SYSTEM Voltage/Current input module
    AXT_SIO_RAI16MLIIISFA                                   = 0xC9,    // AI slave module, MechatroLink III AXT(SFA), Analog IN 16ch, 16 bit
    AXT_SIO_RDB32MSMLIII                                    = 0xCA,    // DIO slave module, MechatroLink III M-SYSTEM, IN 16-Channel, OUT 16-Channel
    AXT_SIO_RAVI4MLII										= 0xCB,    // DC VOLTAGE/CURRENT INPUT MODULE, 4 points (Product by M-SYSTEM), For MLII only
    AXT_COM_234R                                            = 0xD3,    // COM-234R
    AXT_COM_484R                                            = 0xD4,    // COM-484R
    AXT_COM_234IDS                                          = 0xD5,    // COM-234IDS
    AXT_COM_484IDS                                          = 0xD6,    // COM-484IDS
    AXT_SIO_AO4F                                            = 0xD7,    // AO 4Ch, 16 bit
    AXT_SIO_AI8F                                            = 0xD8,    // AI 8Ch, 16 bit
    AXT_SIO_AI8AO4F                                         = 0xD9,    // AI 8Ch, AO 4Ch, 16 bit
    AXT_SIO_HPC4                                            = 0xDA,    // External Encoder module for 4Channel with Trigger function.
    AXT_ECAT_MOTION                                         = 0xE1,    // EtherCAT Motion Module
    AXT_ECAT_DIO                                            = 0xE2,    // EtherCAT DIO Module 
    AXT_ECAT_AIO                                            = 0xE3,    // EtherCAT AIO Module
    AXT_ECAT_SERIAL                                         = 0xE4,    // EtherCAT Serial COM(RS232C) Module
    AXT_ECAT_COUPLER                                        = 0xE5,    // EtherCAT Coupler Module
    AXT_ECAT_CNT                                            = 0xE6,    // EtherCAT Count Module
    AXT_ECAT_UNKNOWN                                        = 0xE7,    // EtherCAT Unknown Module
    AXT_SMC_4V04_A                                          = 0xEA,    // Nx04_A
}

public enum AXT_FUNC_RESULT:uint
{
    AXT_RT_SUCCESS                                          = 0,       // API Function successful
    AXT_RT_OPEN_ERROR                                       = 1001,    // Library not open
    AXT_RT_OPEN_ALREADY                                     = 1002,    // Library is already open and in use
    AXT_RT_NOT_INITIAL                                      = 1052,    // Serial module is not initialized
    AXT_RT_NOT_OPEN                                         = 1053,    // Library initialization failed    

    AXT_RT_NOT_SUPPORT_VERSION                              = 1054,    // Unsupported Hardware
    AXT_RT_LOCK_FILE_MISMATCH                               = 1055,    // Lock file and current scan information do not match
    AXT_RT_MASTER_VERSION_MISMATCH                          = 1056,    // Library and EtherCAT Master versions do not match
    AXT_RT_NOT_RUN_EZMANAGER                                = 1057,    // EzManager not running
    AXT_RT_NOT_FIND_BIN_FILE                                = 1058,    // Can not find BIN file
    AXT_RT_NOT_FIND_ENI_FILE                                = 1059,    // Can not find ENI file
    AXT_RT_NOT_FIND_CONFIG_FILE                             = 1060,    // Can not find Config file
    AXT_RT_RTOS_OPEN_ERROR                                  = 1061,    // RTOS Open failed
    AXT_RT_SLAVE_CONFIG_ERROR                               = 1062,    // RTOS Slave Config failed
    AXT_RT_SLAVE_OP_TIMEOUT_WARNING                         = 1063,    // Timeout occurs while waiting for slaves to enter OP mode
    AXT_RT_SLAVE_NOT_OP                                     = 1064,    // There is a Slave other than OP mode.

    AXT_RT_INVALID_HARDWARE                                 = 1100,    // Invalid board
    AXT_RT_INVALID_BOARD_NO                                 = 1101,    // Invalid board number
    AXT_RT_INVALID_MODULE_POS                               = 1102,    // Invalid module location
    AXT_RT_INVALID_LEVEL                                    = 1103,    // Invalid level
    AXT_RT_INVALID_VARIABLE                                 = 1104,    // Invalid variable
    AXT_RT_INVALID_MODULE_NO                                = 1105,    // Invalid module number
    AXT_RT_INVALID_NO                                       = 1106,    // Invalid number

    AXT_RT_ERROR_VERSION_READ                               = 1151,    // Unable to read library version
    AXT_RT_NETWORK_ERROR                                    = 1152,    // Hardware network error
    AXT_RT_NETWORK_LOCK_MISMATCH                            = 1153,    // Board lock information does not match current scan information

    AXT_RT_1ST_BELOW_MIN_VALUE                              = 1160,    // The first argument value is less than the minimum value.
    AXT_RT_1ST_ABOVE_MAX_VALUE                              = 1161,    // The first argument value is greater than the maximum value.
    AXT_RT_2ND_BELOW_MIN_VALUE                              = 1170,    // Second argument is less than minimum
    AXT_RT_2ND_ABOVE_MAX_VALUE                              = 1171,    // Second argument is greater than maximum
    AXT_RT_3RD_BELOW_MIN_VALUE                              = 1180,    // The third argument value is less than the minimum value.
    AXT_RT_3RD_ABOVE_MAX_VALUE                              = 1181,    // The third argument value is greater than the maximum value.
    AXT_RT_4TH_BELOW_MIN_VALUE                              = 1190,    // The fourth argument value is less than the minimum value.
    AXT_RT_4TH_ABOVE_MAX_VALUE                              = 1191,    // The fourth argument value is greater than the maximum value.
    AXT_RT_5TH_BELOW_MIN_VALUE                              = 1200,    // Fifth factor value is less than minimum value
    AXT_RT_5TH_ABOVE_MAX_VALUE                              = 1201,    // Fifth argument is greater than maximum
    AXT_RT_6TH_BELOW_MIN_VALUE                              = 1210,    // The sixth factor value is less than the minimum value.
    AXT_RT_6TH_ABOVE_MAX_VALUE                              = 1211,    // The sixth argument value is greater than the maximum value.
    AXT_RT_7TH_BELOW_MIN_VALUE                              = 1220,    // The seventh argument value is less than the minimum value.
    AXT_RT_7TH_ABOVE_MAX_VALUE                              = 1221,    // Seventh factor greater than maximum
    AXT_RT_8TH_BELOW_MIN_VALUE                              = 1230,    // Eighth argument value is less than minimum value
    AXT_RT_8TH_ABOVE_MAX_VALUE                              = 1231,    // Eighth argument is greater than maximum
    AXT_RT_9TH_BELOW_MIN_VALUE                              = 1240,    // The ninth factor value is less than the minimum value.
    AXT_RT_9TH_ABOVE_MAX_VALUE                              = 1241,    // The 9th argument value is greater than the maximum value
    AXT_RT_10TH_BELOW_MIN_VALUE                             = 1250,    // The tenth argument value is smaller than the minimum value.
    AXT_RT_10TH_ABOVE_MAX_VALUE                             = 1251,    // The tenth argument is greater than the maximum.
    AXT_RT_11TH_BELOW_MIN_VALUE                             = 1252,    // The eleventh argument value is smaller than the minimum value
    AXT_RT_11TH_ABOVE_MAX_VALUE                             = 1253,    // The eleventh argument value is greater than the maximum value.

    AXT_RT_AIO_OPEN_ERROR                                   = 2001,    // AIO module open failure
    AXT_RT_AIO_NOT_MODULE                                   = 2051,    // No AIO module
    AXT_RT_AIO_NOT_EVENT                                    = 2052,    // Failed to read AIO event
    AXT_RT_AIO_INVALID_MODULE_NO                            = 2101,    // Invalid AIO module
    AXT_RT_AIO_INVALID_CHANNEL_NO                           = 2102,    // Invalid AIO channel number
    AXT_RT_AIO_INVALID_USE                                  = 2106,    // Can not use AIO function
    AXT_RT_AIO_INVALID_TRIGGER_MODE                         = 2107,    // Invalid trigger mode
    AXT_RT_AIO_EXTERNAL_DATA_EMPTY                          = 2108,    // No external data value
    AXT_RT_AIO_INVALID_VALUE                                = 2109,    // Invalid value setting
    AXT_RT_AIO_UPG_ALEADY_ENABLED                           = 2110,    // AO UPG function is already enabled

    AXT_RT_DIO_OPEN_ERROR                                   = 3001,    // DIO module open failure
    AXT_RT_DIO_NOT_MODULE                                   = 3051,    // No DIO module
    AXT_RT_DIO_NOT_INTERRUPT                                = 3052,    // DIO interrupt not set
    AXT_RT_DIO_INVALID_MODULE_NO                            = 3101,    // Invalid DIO module number
    AXT_RT_DIO_INVALID_OFFSET_NO                            = 3102,    // Invalid DIO OFFSET number
    AXT_RT_DIO_INVALID_LEVEL                                = 3103,    // Invalid DIO level
    AXT_RT_DIO_INVALID_MODE                                 = 3104,    // Invalid DIO mode
    AXT_RT_DIO_INVALID_VALUE                                = 3105,    // Invalid value setting
    AXT_RT_DIO_INVALID_USE                                  = 3106,    // Can not use DIO function

    AXT_RT_CNT_OPEN_ERROR                                   = 3201,    // CNT module open failure
    AXT_RT_CNT_NOT_MODULE                                   = 3251,    // No CNT module
    AXT_RT_CNT_NOT_INTERRUPT                                = 3252,    // CNT interrupt not set
    AXT_RT_CNT_INVALID_MODULE_NO                            = 3301,    // Invalid CNT module number
    AXT_RT_CNT_INVALID_CHANNEL_NO                           = 3302,    // Invalid channel number
    AXT_RT_CNT_INVALID_OFFSET_NO                            = 3303,    // Invalid INT OFFSET number
    AXT_RT_CNT_INVALID_LEVEL                                = 3304,    // Invalid CNT level
    AXT_RT_CNT_INVALID_MODE                                 = 3305,    // Invalid CNT mode
    AXT_RT_CNT_INVALID_VALUE                                = 3306,    // Invalid value setting
    AXT_RT_CNT_INVALID_USE                                  = 3307,    // Can not use CNT function

    AXT_RT_COM_OPEN_ERROR                                   = 3401,    // COM port open failure
    AXT_RT_COM_NOT_OPEN                                     = 3402,    // COM Port Not Opened
    AXT_RT_COM_ALREADY_IN_USE                               = 3403,    // Using COM port
    AXT_RT_COM_NOT_MODULE                                   = 3451,    // No COM port
    AXT_RT_COM_NOT_INTERRUPT                                = 3452,    // COM interrupt not set
    AXT_RT_COM_INVALID_MODULE_NO                            = 3501,    // Invalid COM module number
    AXT_RT_COM_INVALID_PORT_NO                              = 3502,    // Invalid channel number
    AXT_RT_COM_INVALID_OFFSET_NO                            = 3503,    // Invalid COM OFFSET number
    AXT_RT_COM_INVALID_LEVEL                                = 3504,    // Invalid COM level
    AXT_RT_COM_INVALID_MODE                                 = 3505,    // Invalid COM mode
    AXT_RT_COM_INVALID_VALUE                                = 3506,    // Invalid value setting
    AXT_RT_COM_INVALID_USE                                  = 3507,    // Can not use COM function
    AXT_RT_COM_INVALID_BAUDRATE                             = 3508,    // Invalid baudrate setting

    AXT_RT_MOTION_OPEN_ERROR                                = 4001,    // Motion Library Open Failure
    AXT_RT_MOTION_NOT_MODULE                                = 4051,    // No motion modules installed in the system
    AXT_RT_MOTION_NOT_INTERRUPT                             = 4052,    // Failed to read interrupt result
    AXT_RT_MOTION_NOT_INITIAL_AXIS_NO                       = 4053,    // Failed to initialize corresponding axis motion
    AXT_RT_MOTION_NOT_IN_CONT_INTERPOL                      = 4054,    // Continuous interpolation stop command was executed while continuous interpolation driving was not in progress.
    AXT_RT_MOTION_NOT_PARA_READ                             = 4055,    // Origin drive setting parameter failed to load
    AXT_RT_MOTION_INVALID_AXIS_NO                           = 4101,    // The axis does not exist
    AXT_RT_MOTION_INVALID_METHOD                            = 4102,    // Incorrect setting required for driving the axis
    AXT_RT_MOTION_INVALID_USE                               = 4103,    // 'uUse' parameter value is set incorrectly
    AXT_RT_MOTION_INVALID_LEVEL                             = 4104,    // 'uLevel' parameter value is set incorrectly
    AXT_RT_MOTION_INVALID_BIT_NO                            = 4105,    // The corresponding bit in the general-purpose I / O is set incorrectly
    AXT_RT_MOTION_INVALID_STOP_MODE                         = 4106,    // Motion stop mode setting value is wrong
    AXT_RT_MOTION_INVALID_TRIGGER_MODE                      = 4107,    // Trigger setup mode set incorrectly
    AXT_RT_MOTION_INVALID_TRIGGER_LEVEL                     = 4108,    // Trigger output level setting is incorrect
    AXT_RT_MOTION_INVALID_SELECTION                         = 4109,    // 'uSelection' argument is set to a value other than COMMAND or ACTUAL
    AXT_RT_MOTION_INVALID_TIME                              = 4110,    // Trigger output time value is set incorrectly
    AXT_RT_MOTION_INVALID_FILE_LOAD                         = 4111,    // File with motion settings can not be loaded
    AXT_RT_MOTION_INVALID_FILE_SAVE                         = 4112,    // Failed to save file to save motion settings
    AXT_RT_MOTION_INVALID_VELOCITY                          = 4113,    // Motion drive speed value is set to 0 and motion error occurs
    AXT_RT_MOTION_INVALID_ACCELTIME                         = 4114,    // Motion drive acceleration time value is set to 0, resulting in motion error
    AXT_RT_MOTION_INVALID_PULSE_VALUE                       = 4115,    // Input pulse value is set to less than 0 when setting motion unit.
    AXT_RT_MOTION_INVALID_NODE_NUMBER                       = 4116,    // Position or speed override function is active during motion stop
    AXT_RT_MOTION_INVALID_TARGET                            = 4117,    // Returns the flag about the cause of stopping the multi-axis motion.

    AXT_RT_MOTION_ERROR_IN_NONMOTION                        = 4151,    // It should be motion-driven but not motion-driven
    AXT_RT_MOTION_ERROR_IN_MOTION                           = 4152,    // Execute other motion drive function during motion drive
    AXT_RT_MOTION_ERROR                                     = 4153,    // Error occurred while executing multi-axis drive stop function
    AXT_RT_MOTION_ERROR_GANTRY_ENABLE                       = 4154,    // When gantry enable is enabled
    AXT_RT_MOTION_ERROR_GANTRY_AXIS                         = 4155,    // When the gantry axis has entered the master channel (axis) number (0 ~ (maximum number of axes - 1)) incorrectly
    AXT_RT_MOTION_ERROR_MASTER_SERVOON                      = 4156,    // When the master shaft is not warmed up
    AXT_RT_MOTION_ERROR_SLAVE_SERVOON                       = 4157,    // When the slave axis is not warmed up
    AXT_RT_MOTION_INVALID_POSITION                          = 4158,    // When it is not in a valid location
    AXT_RT_ERROR_NOT_SAME_MODULE                            = 4159,    // If you are not in the same module
    AXT_RT_ERROR_NOT_SAME_BOARD                             = 4160,    // If it is not on the same board
    AXT_RT_ERROR_NOT_SAME_PRODUCT                           = 4161,    // If the products are different
    AXT_RT_NOT_CAPTURED                                     = 4162,    // When the location is not saved
    AXT_RT_ERROR_NOT_SAME_IC                                = 4163,    // When it is not in the same chip
    AXT_RT_ERROR_NOT_GEARMODE                               = 4164,    // When gear mode can not be converted
    AXT_ERROR_CONTI_INVALID_AXIS_NO                         = 4165,    // When the continuous interpolation axis mapping is not a valid axis
    AXT_ERROR_CONTI_INVALID_MAP_NO                          = 4166,    // When the continuous mapping is not a valid mapping number
    AXT_ERROR_CONTI_EMPTY_MAP_NO                            = 4167,    // When the continuous interpolation mapping number is empty
    AXT_RT_MOTION_ERROR_CACULATION                          = 4168,    // When a calculation error occurs
    AXT_RT_ERROR_MOVE_SENSOR_CHECK                          = 4169,    // Error sensor (Alarm, EMG, Limit, etc.) is detected before continuous interpolation drive
                                                                       
    AXT_ERROR_HELICAL_INVALID_AXIS_NO                       = 4170,    // Helical axis mapping is not a valid axis
    AXT_ERROR_HELICAL_INVALID_MAP_NO                        = 4171,    // Helical mapping is not a valid mapping number
    AXT_ERROR_HELICAL_EMPTY_MAP_NO                          = 4172,    // Helical hopping number is empty

    AXT_ERROR_SPLINE_INVALID_AXIS_NO                        = 4180,    // Spline axis mapping is not a valid axis
    AXT_ERROR_SPLINE_INVALID_MAP_NO                         = 4181,    // Not a valid mapping number in spline mapping
    AXT_ERROR_SPLINE_EMPTY_MAP_NO                           = 4182,    // Spline mapping number is empty
    AXT_ERROR_SPLINE_NUM_ERROR                              = 4183,    // Invalid spline point number
    AXT_RT_MOTION_INTERPOL_VALUE                            = 4184,    // Invalid interpolation input value
    AXT_RT_ERROR_NOT_CONTIBEGIN                             = 4185,    // Do not call the CONTIBEGIN function when performing continuous interpolation
    AXT_RT_ERROR_NOT_CONTIEND                               = 4186,    // Do not call the CONTIEND function when performing continuous interpolation
                                                                       
    AXT_RT_MOTION_HOME_SEARCHING                            = 4201,    // Use other motion functions while looking for home sensor
    AXT_RT_MOTION_HOME_ERROR_SEARCHING                      = 4202,    // When you are looking for home sensor, you are forced to stop by someone or something outside
    AXT_RT_MOTION_HOME_ERROR_START                          = 4203,    // Origin search can not be started due to initialization problem
    AXT_RT_MOTION_HOME_ERROR_GANTRY                         = 4204,    // Can not enable gantry while looking for home
                                                                       
    AXT_RT_MOTION_READ_ALARM_WAITING                        = 4210,    // Waiting for alarm code result from SERVOPACK
    AXT_RT_MOTION_READ_ALARM_NO_REQUEST                     = 4211,    // Alarm code return command is not issued to SERVOPACK
    AXT_RT_MOTION_READ_ALARM_TIMEOUT                        = 4212,    // Servo pack alarm read timeout (1sec or more)
    AXT_RT_MOTION_READ_ALARM_FAILED                         = 4213,    // Servo pack alarm read failure
    AXT_RT_MOTION_READ_ALARM_UNKNOWN                        = 4220,    // Unknown alarm code
    AXT_RT_MOTION_READ_ALARM_FILES                          = 4221,    // Alarm information file does not exist at the specified position
    AXT_RT_MOTION_READ_ALARM_NOT_DETECTED                   = 4222,    // No alarms
                                                                       
    AXT_RT_MOTION_POSITION_OUTOFBOUND                       = 4251,    // The set position value is larger than the set maximum value or smaller than the minimum value.
    AXT_RT_MOTION_PROFILE_INVALID                           = 4252,    // Incorrect driving speed profile setting
    AXT_RT_MOTION_VELOCITY_OUTOFBOUND                       = 4253,    // Drive speed value is set larger than maximum value
    AXT_RT_MOTION_MOVE_UNIT_IS_ZERO                         = 4254,    // Drive unit value is set to 0
    AXT_RT_MOTION_SETTING_ERROR                             = 4255,    // Incorrect speed, acceleration, jerk, profile settings
    AXT_RT_MOTION_IN_CONT_INTERPOL                          = 4256,    // During continuous interpolation drive, drive start or restart function was executed. 
    AXT_RT_MOTION_DISABLE_TRIGGER                           = 4257,    // Trigger output is in the Disable state
    AXT_RT_MOTION_INVALID_CONT_INDEX                        = 4258,    // Invalid continuous interpolation Index value setting
    AXT_RT_MOTION_CONT_QUEUE_FULL                           = 4259,    // Continuous interpolation queue of motion chip is Full
    AXT_RT_PROTECTED_DURING_SERVOON                         = 4260,    // Can not be used when servo is on
    AXT_RT_HW_ACCESS_ERROR                                  = 4261,    // Memory Read / Write Failure
                                                                       
    AXT_RT_HW_DPRAM_CMD_WRITE_ERROR_LV1                     = 4262,    // DPRAM Comamnd Write Failure Level1
    AXT_RT_HW_DPRAM_CMD_WRITE_ERROR_LV2                     = 4263,    // DPRAM Comamnd Write Failure Level2
    AXT_RT_HW_DPRAM_CMD_WRITE_ERROR_LV3                     = 4264,    // DPRAM Comamnd Write Failure Level3
    AXT_RT_HW_DPRAM_CMD_READ_ERROR_LV1                      = 4265,    // DPRAM Comamnd Read Failure Level1
    AXT_RT_HW_DPRAM_CMD_READ_ERROR_LV2                      = 4266,    // DPRAM Comamnd Read Failure Level2
    AXT_RT_HW_DPRAM_CMD_READ_ERROR_LV3                      = 4267,    // DPRAM Comamnd Read Failure Level3
                                                                       
    AXT_RT_COMPENSATION_SET_PARAM_FIRST                     = 4300,    // The first of the calibration parameters is set incorrectly
    AXT_RT_COMPENSATION_NOT_INIT			                = 4301,	   // Calibration table function not initialized
    AXT_RT_COMPENSATION_POS_OUTOFBOUND		                = 4302,	   // Position value is not in range
    AXT_RT_COMPENSATION_BACKLASH_NOT_INIT	                = 4303,	   // Backlash compensation function not initialized
    AXT_RT_COMPENSATION_INVALID_ENTRY                       = 4304,	   // The entry number is invalid.
                                                                       
    AXT_RT_SEQ_NOT_IN_SERVICE                               = 4400,    // Resource allocation failure while executing sequential drive function
    AXT_ERROR_SEQ_INVALID_MAP_NO                            = 4401,    // Sequence drive function Running mapping number or more.
    AXT_ERROR_INVALID_AXIS_NO                               = 4402,    // Above axis number in function setting parameter.
    AXT_RT_ERROR_NOT_SEQ_NODE_BEGIN                         = 4403,    // The sequential driven node input start function was not called.
    AXT_RT_ERROR_NOT_SEQ_NODE_END                           = 4404,    // The sequential driven node input end function was not called.
    AXT_RT_ERROR_NO_NODE                                    = 4405,    // No sequential drive node input.
    AXT_RT_ERROR_SEQ_STOP_TIMEOUT                           = 4406,    // Timeout occurred.
    AXT_RT_ERROR_INVALID_SEQ_MASTER_AXIS_NO                 = 4407,    // Sequential drive Master axis is invalid.

	AXT_RT_ERROR_RING_COUNTER_ENABLE                        = 4420,    // Ring Counter function is in use
    AXT_RT_ERROR_RING_COUNTER_OUT_OF_RANGE                  = 4421,    // Ring Counter In Use Out-of-Range Command Location Invocation (POS_ABS_LONG_MODE or POS_ABS_SHORT_MODE)
	AXT_RT_ERROR_SOFT_LIMIT_ENABLE                          = 4430,    // Software Limit feature is enabled
    AXT_RT_M3_COMMUNICATION_FAILED                          = 4500,    // ML3 : communication failure
    AXT_RT_MOTION_ONE_OF_AXES_IS_NOT_M3                     = 4501,    // ML3 : none of the configured ML3 nodes have motion nodes
    AXT_RT_MOTION_BIGGER_VEL_THEN_MAX_VEL                   = 4502,    // ML3 : Greater than the set maximum speed of the specified axis
    AXT_RT_MOTION_SMALLER_VEL_THEN_MAX_VEL                  = 4503,    // ML3 : Less than the set maximum speed for the specified axis
    AXT_RT_MOTION_ACCEL_MUST_BIGGER_THEN_ZERO               = 4504,    // ML3 : The acceleration of the specified axis is greater than 0
    AXT_RT_MOTION_SMALL_ACCEL_WITH_UNIT_PULSE               = 4505,    // ML3 : UnitPulse applied acceleration greater than 0
    AXT_RT_MOTION_INVALID_INPUT_ACCEL                       = 4506,    // ML3 : Incorrect acceleration input for the specified axis
    AXT_RT_MOTION_SMALL_DECEL_WITH_UNIT_PULSE               = 4507,    // ML3 : UnitPulse applies deceleration greater than 0
    AXT_RT_MOTION_INVALID_INPUT_DECEL                       = 4508,    // ML3 : Incorrect deceleration input for the specified axis
    AXT_RT_MOTION_SAME_START_AND_CENTER_POS                 = 4509,    // ML3 : Start point and center point of circular interpolation are equal.
    AXT_RT_MOTION_INVALID_JERK                              = 4510,    // ML3 : Invalid jerk input for the specified axis
    AXT_RT_MOTION_INVALID_INPUT_VALUE                       = 4511,    // ML3 : Invalid Input Value for Specified Axis
    AXT_RT_MOTION_NOT_SUPPORT_PROFILE                       = 4512,    // ML3 : Unavailable speed profile
    AXT_RT_MOTION_INPOS_UNUSED                              = 4513,    // ML3 : In position Not used
    AXT_RT_MOTION_AXIS_IN_SLAVE_STATE                       = 4514,    // ML3 : The specified axis is not in the slave state
    AXT_RT_MOTION_AXES_ARE_NOT_SAME_BOARD                   = 4515,    // ML3 : Specified axes are not on the same board
    AXT_RT_MOTION_ERROR_IN_ALARM                            = 4516,    // ML3 : The specified axis is in an alarm state
    AXT_RT_MOTION_ERROR_IN_EMGN                             = 4517,    // ML3 : The specified axis is in the emergency stop state
    AXT_RT_MOTION_CAN_NOT_CHANGE_COORD_NO                   = 4518,    // ML3 : Coordinator number can not be converted
    AXT_RT_MOTION_INVALID_INTERNAL_RADIOUS                  = 4519,    // ML3 : X, Y axis radius discrepancy of circular interpolation
    AXT_RT_MOTION_CONTI_QUEUE_FULL                          = 4521,    // ML3 : The queue of interpolation is full
    AXT_RT_MOTION_SAME_START_AND_END_POSITION               = 4522,    // ML3 : Start point and end point of circular interpolation are the same.
    AXT_RT_MOTION_INVALID_ANGLE                             = 4523,    // ML3 : Angle of circular interpolation exceeded 360 degrees
    AXT_RT_MOTION_CONTI_QUEUE_EMPTY                         = 4524,    // ML3 : Interpolation queue is empty
    AXT_RT_MOTION_ERROR_GEAR_ENABLE                         = 4525,    // ML3 : The specified axis is already in the link set state
    AXT_RT_MOTION_ERROR_GEAR_AXIS                           = 4526,    // ML3 : The specified axis is not a link axis
    AXT_RT_MOTION_ERROR_NO_GANTRY_ENABLE                    = 4527,    // ML3 : The specified axis is not in the gantry configuration state
    AXT_RT_MOTION_ERROR_NO_GEAR_ENABLE                      = 4528,    // ML3 : The specified axis is not in the link set state
    AXT_RT_MOTION_ERROR_GANTRY_ENABLE_FULL                  = 4529,    // ML3 : Gantry configuration is full
    AXT_RT_MOTION_ERROR_GEAR_ENABLE_FULL                    = 4530,    // ML3 : Link setting is full
    AXT_RT_MOTION_ERROR_NO_GANTRY_SLAVE                     = 4531,    // ML3 : The specified axis is not in the gantry slave configuration state
    AXT_RT_MOTION_ERROR_NO_GEAR_SLAVE                       = 4532,    // ML3 : The specified axis is not in link slave setup state
    AXT_RT_MOTION_ERROR_MASTER_SLAVE_SAME                   = 4533,    // ML3 : Master axis and slave axis are the same
    AXT_RT_MOTION_NOT_SUPPORT_HOMESIGNAL                    = 4534,    // ML3 : Home signal of the specified axis is not supported
    AXT_RT_MOTION_ERROR_NOT_SYNC_CONNECT                    = 4535,    // ML3 : The specified axis is not in the sink connection state
    AXT_RT_MOTION_OVERFLOW_POSITION                         = 4536,    // ML3 : Drive position value for the specified axis is overflow
    AXT_RT_MOTION_ERROR_INVALID_CONTIMAPAXIS                = 4537,    // ML3 : No specified coordinate system axis mapping for interpolation operation
    AXT_RT_MOTION_ERROR_INVALID_CONTIMAPSIZE                = 4538,    // ML3 : Specified Coordinate System Axis Mapping Axis Size Is Invalid for Interpolation
    AXT_RT_MOTION_ERROR_IN_SERVO_OFF                        = 4539,    // ML3 : Specified axis is servo OFF
    AXT_RT_MOTION_ERROR_POSITIVE_LIMIT                      = 4540,    // ML3 : The specified axis is (+) limit ON
    AXT_RT_MOTION_ERROR_NEGATIVE_LIMIT                      = 4541,    // ML3 : The specified axis is (-) limit ON
    AXT_RT_MOTION_ERROR_OVERFLOW_SWPROFILE_NUM              = 4542,    // ML3 : The number of supported profiles for specified axes overflowed
    AXT_RT_PROTECTED_DURING_INMOTION                        = 4543,    // ML3 : Can not be used with in_motion

    AXT_RT_DATA_FLASH_NOT_EXIST                             = 5000,
    AXT_RT_DATA_FLASH_BUSY                                  = 5001,

    AXT_RT_QUEUE_CMD_ERROR                                  = 5010,
    AXT_RT_QUEUE_CMD_WAIT_ERROR                             = 5011,
    AXT_RT_QUEUE_CMD_WAIT_TIMEOUT                           = 5012,

    AXT_RT_QUEUE_RSP_ERROR                                  = 5015,
    AXT_RT_QUEUE_RSP_WAIT_ERROR                             = 5016,
    AXT_RT_QUEUE_RSP_WAIT_TIMEOUT                           = 5017,

    AXT_RT_MOTION_STILL_CONTI_MOTION                        = 5018,        // During continuous interpolation drive, calls such as WriteClear and SetAxisMpa were called.

    AXT_RT_MOTION_INVALD_SET                                = 6000,    
    AXT_RT_MOTION_INVALD_RESET                              = 6001,    
    AXT_RT_MOTION_INVALD_ENABLE                             = 6002,

    AXT_RT_LICENSE_INVALID                                  = 6500,        // Invalid License

    AXT_RT_MONITOR_IN_OPERATION                             = 6600,        // Monitor function currently active
    AXT_RT_MONITOR_NOT_OPERATION                            = 6601,        // Monitor function currently inactive
    AXT_RT_MONITOR_EMPTY_QUEUE                              = 6602,        // Empty Monitor data queue
    AXT_RT_MONITOR_INVALID_TRIGGER_OPTION                   = 6603,        // Trigger setting is not valid
    AXT_RT_MONITOR_EMPTY_ITEM                               = 6604,        // Empty item
    AXT_RT_MACRO_INVALID_MACRO_NO                           = 6700,
    AXT_RT_MACRO_INVALID_NODE_NO                            = 6701,
    AXT_RT_MACRO_INVALID_STOP_MODE                          = 6702,
	AXT_RT_MACRO_MEMORY_MISMATCH							= 6703,
	AXT_RT_MACRO_CONTROL_LOCKED								= 6704,
    AXT_RT_MACRO_NOT_NODE_BEGIN                             = 6710,        // 
    AXT_RT_MACRO_NOT_NODE_END                               = 6711,        // 
    AXT_RT_MACRO_ALREADY_BEGIN                              = 6712,  
    AXT_RT_MACRO_NODE_EMPTY                                 = 6713,        // 
    AXT_RT_MACRO_IN_OPERATION                               = 6714,        // 
    AXT_RT_MACRO_NOT_OPERATION                              = 6715,        //           
    AXP_RT_MACRO_NOT_SUPPORT_FUNCTION                       = 6716,        // 
    AXP_RT_MACRO_NODE_FULL                                  = 6717,        // 
    AXP_RT_MACRO_NODE_CHECK_ERROR                           = 6720,        // 
    AXT_RT_MACRO_NOT_CHECKED                                = 6721,        //     

    AXT_MK_RT_INVALID_AXIS                                  = 7100,
    AXT_MK_RT_INVALID_AXIS_SIZE                             = 7101,
    AXT_MK_RT_INVALID_COORD                                 = 7102,
    AXT_MK_RT_INVALID_COORD_SIZE                            = 7103,
    AXT_MK_RT_INVALID_AXIS_MAP                              = 7104,
    AXT_MK_RT_INVALID_AXIS_MAP_SIZE                         = 7105,
    AXT_MK_RT_INVALID_VEL                                   = 7106,
    AXT_MK_RT_INVALID_END_VEL                               = 7107,
    AXT_MK_RT_INVALID_ACCEL                                 = 7108,
    AXT_MK_RT_INVALID_DECEL                                 = 7109,
    AXT_MK_RT_INVALID_ABS_REL                               = 7110,
    AXT_MK_RT_INVALID_PROFILE                               = 7111,
    AXT_MK_RT_INVALID_STOP_DECEL                            = 7112,
    AXT_MK_RT_INVALID_STOP_TIME                             = 7113,
    AXT_MK_RT_INVALID_ACCEL_JERK_RATE                       = 7114,
    AXT_MK_RT_INVALID_DECEL_JERK_RATE                       = 7115,
    AXT_MK_RT_INVALID_ACCEL_UNIT                            = 7116,
    AXT_MK_RT_INVALID_DISTANCE                              = 7117,
    AXT_MK_RT_INVALID_ANGLE                                 = 7118,
    AXT_MK_RT_INVALID_BIT                                   = 7119,
    AXT_MK_RT_INVALID_PORT                                  = 7120,
    AXT_MK_RT_INVALID_SPLINE_INDEX                          = 7121,
    AXT_MK_RT_INVALID_THREAD                                = 7122,
    AXT_MK_RT_INVALID_TIMER                                 = 7123,
    AXT_MK_RT_INVALID_SEGMENT_COUNT                         = 7124,
    AXT_MK_RT_INVALID_SEGMENT_NO                            = 7125,
    AXT_MK_RT_INVALID_NODE_NO                               = 7126,
    AXT_MK_RT_INVALID_HWQ_COUNT                             = 7127,
    AXT_MK_RT_INVALID_NODE_SIZE                             = 7128,
    AXT_MK_RT_INVALID_STOP_NODE_SIZE                        = 7129,
    AXT_MK_RT_INVALID_SPLINE_SIZE                           = 7130,
    AXT_MK_RT_INVALID_LINE_LINE_FILLET                      = 7131,
    AXT_MK_RT_INVALID_LINE_ARC_FILLET                       = 7132,
    AXT_MK_RT_INVALID_ARC_LINE_FILLET                       = 7133,
    AXT_MK_RT_INVALID_ARC_ARC_FILLET                        = 7134,
    AXT_MK_RT_INVALID_RESET_FILLET                          = 7135,
    AXT_MK_RT_INVALID_TASK                                  = 7136,
    AXT_MK_RT_INVALID_ROUND_INDEX                           = 7137,
    AXT_MK_RT_INVALID_LOG_DATA                              = 7138,
    AXT_MK_RT_INVALID_LOG10_DATA                            = 7139,
    AXT_MK_RT_INVALID_PORT_NO                               = 7140,
    AXT_MK_RT_INVALID_BAUD_RATE                             = 7141,
    AXT_MK_RT_INVALID_STOP_BIT                              = 7142,
    AXT_MK_RT_INVALID_PARITY                                = 7143,
    AXT_MK_RT_INVALID_EDGE                                  = 7144,
    AXT_MK_RT_INVALID_STOP_MODE                             = 7145,
    AXT_MK_RT_INVALID_TRIGGER_TIME                          = 7146,
    AXT_MK_RT_INVALID_TRIGGER_LEVEL                         = 7147,
    AXT_MK_RT_INVALID_TRIGGER_SELECT                        = 7148,
    AXT_MK_RT_INVALID_TRIGGER_INTERRUPT                     = 7149,
    AXT_MK_RT_INVALID_TRIGGER_METHOD                        = 7150,
    AXT_MK_RT_INVALID_TRIGGER_POSITION                      = 7151,
    AXT_MK_RT_INVALID_TRIGGER_INDEX                         = 7152,
    AXT_MK_RT_INVALID_ECAM_DATA                             = 7153,
    AXT_MK_RT_INVALID_ECAM_POSITION                         = 7154,
    AXT_MK_RT_INVALID_EGEAR_SIZE                            = 7155,
    AXT_MK_RT_INVALID_INDEX                                 = 7156,
    AXT_MK_RT_INVALID_MOTION_MODE                           = 7157,
    AXT_MK_RT_INVALID_SIGNAL                                = 7158,
    AXT_MK_RT_INVALID_STOP_DISTANCE                         = 7159,
    AXT_MK_RT_INVALID_DIRECTION                             = 7160,
    AXT_MK_RT_INVALID_ZERO_VELOCITY                         = 7161,
    AXT_MK_RT_INVALID_COORDINATE_INMOTION                   = 7162,
    AXT_MK_RT_INVALID_COORDINATE_MOTIONDONE                 = 7163,
    AXT_MK_RT_INVALID_BLENDING_MODE                         = 7164,
    AXT_MK_RT_INVALID_BLENDING_VALUE                        = 7165,
    AXT_MK_RT_INVALID_BLENDING_RATIO                        = 7166,
    AXT_MK_RT_INVALID_EGEAR_RATIO                           = 7167,
    AXT_MK_RT_INVALID_SLAVE_AXIS                            = 7168,
    AXT_MK_RT_INVALID_OPERATION_MODE                        = 7169,

    AXT_MK_RT_INVALID_CONTIQ_DISABLE                        = 7170,
    AXT_MK_RT_INVALID_CONTIQ_MODE                           = 7171,
    AXT_MK_RT_INVALID_CONTIQ_ANGLE                          = 7172,
    AXT_MK_RT_INVALID_CONTIQ_VELRATE                        = 7173,
    AXT_MK_RT_INVALID_CONTIQ_FILLET                         = 7174,

    AXT_MK_RT_CONTIQ_AUTO_VEL                               = 7175,
    AXT_MK_RT_CONTIQ_AUTO_ARC                               = 7176,
    AXT_MK_RT_CONTIQ_LINE                                   = 7177,
    AXT_MK_RT_CONTIQ_CIRCLE                                 = 7178,
    AXT_MK_RT_CONTIQ_ARC                                    = 7179,
    AXT_MK_RT_CONTIQ_SYNC_SLAVE                             = 7180,
    AXT_MK_RT_INVALID_SEGMENT_OUTPUT_SIZE                   = 7181,
    AXT_MK_RT_INVALID_SEGMENT_NUMBER                        = 7182,
    AXT_MK_RT_INVALID_TRIG_COUNT                            = 7183,
    AXT_MK_RT_INVALID_TRIG_TIME                             = 7184,
    AXT_MK_RT_NOT_CAPTURED                                  = 7185,
    AXT_MK_RT_INVALID_CAPTURE_INDEX                         = 7186,
    AXT_MK_RT_INVALID_LEVEL                                 = 7187,
    AXT_MK_RT_INVALID_SEGMENT_OUTPUT_MODE                   = 7188,
    AXT_MK_RT_INVALID_SEGMENT_OUTPUT_VALUE                  = 7189,
    AXT_MK_RT_INVALID_SEGMENT_OUTPUT_RATIO                  = 7190,

    AXT_MK_RT_INVALID_SPLINE_POINT_SIZE                     = 7191,
    AXT_MK_RT_INVALID_INMOTION                              = 7192,

    AXT_MK_RT_ENABLE_CONTIQ                                 = 7200,
    AXT_MK_RT_DISABLE_CONTIQ                                = 7201,
    AXT_MK_RT_ENABLE_CONTIQ_SYNC                            = 7202,
    AXT_MK_RT_DISABLE_CONTIQ_SYNC                           = 7203,
    AXT_MK_RT_ENABLE_CONTI                                  = 7204,
    AXT_MK_RT_DISABLE_CONTI                                 = 7205,
    AXT_MK_RT_ENABLE_EGEAR                                  = 7206,
    AXT_MK_RT_DISABLE_EGEAR                                 = 7207,
    AXT_MK_RT_ENABLE_TASK                                   = 7208,
    AXT_MK_RT_DISABLE_TASK                                  = 7209,
    AXT_MK_RT_DISABLE_PORT_NO                               = 7210,

    AXT_MK_RT_ALREADY_OPEN                                  = 7300,
    AXT_MK_RT_ALREADY_CLOSE                                 = 7301,

    AXT_MK_RT_ERROR_HOME                                    = 7400,
    AXT_MK_RT_ERROR_MOTION                                  = 7401,
    AXT_MK_RT_ERROR_INSTOPPING                              = 7402,
    AXT_MK_RT_ERROR_TIME_OUT                                = 7403,
    AXT_MK_RT_ERROR_BUFFER_FULL                             = 7404,
    AXT_MK_RT_ERROR_DATA_CREATE                             = 7405,
    AXT_MK_RT_ERROR_CALCULATION                             = 7406,

    AXT_MK_RT_ERROR_QUEUE_FULL                              = 7500,
    AXT_MK_RT_ERROR_QUEUE_NULL                              = 7501,

    AXT_MK_RT_ERROR_ECAM_TABLE                              = 7502,

    AXT_MK_RT_ERROR_SPLINE_POSITION                         = 7503,

    AXT_MK_RT_INVALID_CIRCULAR_POINT                        = 7510,
    AXT_MK_RT_INVALID_POINT                                 = 7511,
    AXT_MK_RT_INVALID_QUEUE_SIZE                            = 7512,
    AXT_MK_RT_INVALID_POSITION                              = 7513,
    AXT_MK_RT_INVALID_ROTATION                              = 7514,

    AXT_MK_RT_INVALID_TABLE                                 = 7600,
    AXT_MK_RT_INVALID_TABLE_NO                              = 7601,
    AXT_MK_RT_INVALID_TABLE_DATA                            = 7602,
    AXT_MK_RT_INVALID_POSITION_SIZE                         = 7603,

    AXT_MK_RT_INVALID_TABLE_ENABLED                         = 7604,
    AXT_MK_RT_INVALID_TABLE_NOT_ENABLED                     = 7605,
    AXT_MK_RT_INVALID_TABLE_NONE                            = 7606,
    AXT_MK_RT_INVALID_GET_TABLE                             = 7607,
    AXT_MK_RT_INVALID_ENABLE_TABLE                          = 7608,
    AXT_MK_RT_INVALID_DISABLE_TABLE                         = 7609,

    AXT_MK_RT_INVALID_SET                                   = 7610,
    AXT_MK_RT_INVALID_RESET                                 = 7611,
    AXT_MK_RT_INVALID_ENABLE                                = 7612,

    AXT_MK_RT_NOT_SUPPORT                                   = 7900,
    AXT_MK_RT_ERROR                                         = 7901,
    AXT_MK_RT_INVLID_FUNCTION_TYPE                          = 7902,

    AXT_MK_RT_INVALID_ROBOT_SIZE                            = 7700,
    AXT_MK_RT_INVALID_ROBOT_AXIS_SIZE                       = 7701,
    AXT_MK_RT_INVALID_ROBOT_COORD_SIZE                      = 7702,
    AXT_MK_RT_INVALID_ROBOT_NO                              = 7703,
    AXT_MK_RT_INVALID_ROBOT_COORD                           = 7704,
    AXT_MK_RT_INVALID_ROBOT_LIMIT                           = 7705,
    AXT_MK_RT_INVALID_ROBOT_POS_LIMIT                       = 7706,
    AXT_MK_RT_INVALID_ROBOT_NEG_LIMIT                       = 7707,
    AXT_MK_RT_INVALID_ROBOT_VEL_LIMIT                       = 7708,
    AXT_MK_RT_INVALID_ROBOT_ACCEL_LIMIT                     = 7709,
    AXT_MK_RT_INVALID_ROBOT_DECEL_LIMIT                     = 7710,
    AXT_MK_RT_ERROR_ROBOT_CALCULATION                       = 7711,
    AXT_MK_RT_INVALID_FRAME                                 = 7712,
    AXT_MK_RT_INVALID_FRAME_NO                              = 7713,
    AXT_MK_RT_INVALID_FRAME_TYPE                            = 7714,
    AXT_MK_RT_INVALID_OBJECT_NO                             = 7715,
    AXT_MK_RT_INVALID_ROBOT_SYNC                            = 7716,
    AXT_MK_RT_INVALID_ROBOT_SYNC_MOTION                     = 7717,
    AXT_MK_RT_INVALID_ROBOT_SYNC_ENABLE                     = 7718,
    AXT_MK_RT_INVALID_ROBOT_SYNC_DISABLE                    = 7719,
    AXT_MK_RT_INVALID_ROBOT_SYNC_MOTION_MODE                = 7720,
    AXT_MK_RT_INVALID_ROBOT_SYNC_WORK_COORD                 = 7721,
    AXT_MK_RT_INVALID_CAPTURE_POS_NO                        = 7722,
    
    AXT_MK_RT_INVALID_ROBOT_CAPTURE_POS                     = 7730,
    AXT_MK_RT_INVALID_ROBOT_AXIS                            = 7731,
    AXT_MK_RT_INVALID_WORK_NEGATIVE_LIMIT                   = 7732,
    AXT_MK_RT_INVALID_WORK_POSITIVE_LIMIT                   = 7733,

    AXT_MK_RT_INVALID_TOOL_NO                               = 7740,

    AXT_MK_RT_INVALID_FREQUENCY_SIZE                        = 7800,
    AXT_MK_RT_INVALID_IMPULSE_COUNT                         = 7801,
    AXT_MK_RT_INVALID_AMPLITUDE                             = 7802,

    AXT_MK_RT_INVALID_INPUT_SHAPER__NONE                    = 7803,
    AXT_MK_RT_INVALID_INPUT_SHAPER_ENABLED                  = 7804,

    AXT_MK_RT_INVALID_ARRAY_SIZE                            = 7805

}

public enum AXT_BOOLEAN:uint
{
    FALSE,
    TRUE
}

public enum AXT_NETWORK_TYPE:uint
{
    NETWORK_TYPE_MIN    = 0,
    NETWORK_TYPE_ALL    = NETWORK_TYPE_MIN,
    NETWORK_TYPE_RTEX,
    NETWORK_TYPE_MLII,
    NETWORK_TYPE_MLIII,
    NETWORK_TYPE_SIIIH,
    NETWORK_TYPE_ECAT,
    NETWORK_TYPE_MAX    = NETWORK_TYPE_ECAT
}

public enum AXT_LOG_LEVEL:uint
{
    LEVEL_NONE,
    LEVEL_ERROR,
    LEVEL_RUNSTOP,
    LEVEL_FUNCTION
}

public enum AXT_EXISTENCE:uint
{
    STATUS_NOTEXIST,
    STATUS_EXIST
}

public enum AXT_USE:uint
{
    DISABLE,
    ENABLE
}

public enum AXT_AIO_TRIGGER_MODE:uint
{
    DISABLE_MODE                       = 0,
    NORMAL_MODE                        = 1,
    TIMER_MODE                         = 2,
    EXTERNAL_MODE                      = 3
}

public enum AXT_AIO_FULL_MODE:uint
{
    NEW_DATA_KEEP,
    CURR_DATA_KEEP
}

public enum AXT_AIO_EVENT_MASK:uint
{
    DATA_EMPTY      = 0x01,
    DATA_MANY       = 0x02,
    DATA_SMALL      = 0x04,
    DATA_FULL       = 0x08
}

public enum AXT_AIO_INTERRUPT_MASK:uint
{
    ADC_DONE        = 0x00,
    SCAN_END        = 0x01,
    FIFO_HALF_FULL  = 0x02,
    NO_SIGNAL       = 0x03
}

public enum AXT_AIO_EVENT_MODE:uint
{
    AIO_EVENT_DATA_RESET     = 0x00,
    AIO_EVENT_DATA_UPPER     = 0x01,
    AIO_EVENT_DATA_LOWER     = 0x02,
    AIO_EVENT_DATA_FULL      = 0x03,
    AIO_EVENT_DATA_EMPTY     = 0x04
}

public enum AXT_AIO_FIFO_STATUS:uint
{
    FIFO_DATA_EXIST   = 0,
    FIFO_DATA_EMPTY   = 1,
    FIFO_DATA_HALF    = 2,
    FIFO_DATA_FULL    = 6
}

public enum AXT_AIO_EXTERNAL_STATUS:uint
{
    EXTERNAL_DATA_DONE      = 0,
    EXTERNAL_DATA_FINE      = 1,
    EXTERNAL_DATA_HALF      = 2,
    EXTERNAL_DATA_FULL      = 3,
    EXTERNAL_COMPLETE       = 4
}

public enum AXT_DIO_EDGE:uint
{
    DOWN_EDGE,
    UP_EDGE
}

public enum AXT_DIO_STATE:uint
{
    OFF_STATE,
    ON_STATE
}

public enum AXT_MOTION_STOPMODE:uint
{
    EMERGENCY_STOP,
    SLOWDOWN_STOP
}

public enum AXT_MOTION_EDGE:uint
{    
    SIGNAL_DOWN_EDGE,
    SIGNAL_UP_EDGE,
    SIGNAL_LOW_LEVEL,
    SIGNAL_HIGH_LEVEL
}

public enum AXT_MOTION_SELECTION:uint
{
    COMMAND,
    ACTUAL
}

public enum AXT_MOTION_TRIGGER_MODE:uint
{
    PERIOD_MODE,
    ABS_POS_MODE
}

public enum AXT_MOTION_LEVEL_MODE:uint
{
    LOW,
    HIGH,
    UNUSED,
    USED
}

public enum AXT_MOTION_ABSREL:uint
{
    POS_ABS_MODE,
    POS_REL_MODE,
    POS_ABS_LONG_MODE,
    POS_ABS_SHORT_MODE
}

public enum AXT_MOTION_ENCODER_TYPE:uint
{
    ENCODER_TYPE_INCREMENTAL,
    ENCODER_TYPE_ABSOLUTE,
    ENCODER_TYPE_NONE
}

public enum AXT_MOTION_PROFILE_MODE:uint
{
    SYM_TRAPEZOIDE_MODE,
    ASYM_TRAPEZOIDE_MODE,
    QUASI_S_CURVE_MODE,
    SYM_S_CURVE_MODE,
    ASYM_S_CURVE_MODE,
    SYM_TRAP_M3_SW_MODE,     //ML-3 Only, Support Velocity profile
    ASYM_TRAP_M3_SW_MODE,    //ML-3 Only, Support Velocity profile
    SYM_S_M3_SW_MODE,        //ML-3 Only, Support Velocity profile
    ASYM_S_M3_SW_MODE        //ML-3 Only, Support Velocity profile
}

public enum AXT_MOTION_SIGNAL_LEVEL:uint
{
    INACTIVE,
    ACTIVE
}

public enum AXT_MOTION_HOME_RESULT:uint
{
    HOME_RESERVED                                           = 0x00,    // ML3
    HOME_SUCCESS                                            = 0x01,    // Home search complete
    HOME_SEARCHING                                          = 0x02,    // Home searching
    HOME_ERR_GNT_RANGE                                      = 0x10,    // Gantry origin search error, setting error between two axes
    HOME_ERR_USER_BREAK                                     = 0x11,    // Home search stopped by user
    HOME_ERR_VELOCITY                                       = 0x12,    // Origin search speed error error occurred
    HOME_ERR_AMP_FAULT                                      = 0x13,    // Servo pack alarm occurrence error
    HOME_ERR_NEG_LIMIT                                      = 0x14,    // (-) Direction drive (+) Limit sensor detection error
    HOME_ERR_POS_LIMIT                                      = 0x15,    // (+) Direction drive (-) Limit sensor detection error
    HOME_ERR_NOT_DETECT                                     = 0x16,    // The specified signal can not be detected.
    HOME_ERR_SETTING                                        = 0x17,    // User setting parameter error
    HOME_ERR_SERVO_OFF                                      = 0x18,    // Servo Off
    HOME_ERR_TIMEOUT                                        = 0x20,    // Timeout
    HOME_ERR_FUNCALL                                        = 0x30,    // Function call failed
    HOME_ERR_HOME_METHOD                                    = 0x31,    // Unsupported home search method
    HOME_ERR_COUPLING                                       = 0x40,    // Gantry Master to Slave Over Distance protection
    HOME_ERR_UNKNOWN                                        = 0xFF     // Unknown error
}

public enum AXT_MOTION_UNIV_INPUT:uint
{
    UIO_INP0,
    UIO_INP1,
    UIO_INP2,
    UIO_INP3,
    UIO_INP4,
    UIO_INP5
}

public enum AXT_MOTION_UNIV_OUTPUT:uint
{
    UIO_OUT0,
    UIO_OUT1,
    UIO_OUT2,
    UIO_OUT3,
    UIO_OUT4,
    UIO_OUT5
}

public enum AXT_MOTION_DETECT_DOWN_START_POINT:uint
{
    AutoDetect,
    RestPulse
}

public enum AXT_MOTION_PULSE_OUTPUT:uint
{
    OneHighLowHigh                                          = 0x0,    // 1pulse method, PULSE(Active High), Forward(DIR=Low)  / Reverse(DIR=High)
    OneHighHighLow                                          = 0x1,    // 1pulse method, PULSE(Active High), Forward(DIR=High) / Reverse(DIR=Low)
    OneLowLowHigh                                           = 0x2,    // 1pulse method, PULSE(Active Low),  Forward(DIR=Low)  / Reverse(DIR=High)
    OneLowHighLow                                           = 0x3,    // 1pulse method, PULSE(Active Low),  Forward(DIR=High) / Reverse(DIR=Low)
    TwoCcwCwHigh                                            = 0x4,    // 2pulse method, PULSE(CCW:Reverse),  DIR(CW:Forward),  Active High     
    TwoCcwCwLow                                             = 0x5,    // 2pulse method, PULSE(CCW:Reverse),  DIR(CW:Forward),  Active Low     
    TwoCwCcwHigh                                            = 0x6,    // 2pulse method, PULSE(CW:Forward),   DIR(CCW:Reverse), Active High
    TwoCwCcwLow                                             = 0x7,    // 2pulse method, PULSE(CW:Forward),   DIR(CCW:Reverse), Active Low
    TwoPhase                                                = 0x8,    // 2phase(90' Phase difference),  PULSE lead DIR(CW: Forward), PULSE lag DIR(CCW:Reverse)
    TwoPhaseReverse                                         = 0x9     // 2phase(90' Phase difference),  PULSE lead DIR(CCW: Forward), PULSE lag DIR(CW:Reverse)
}

public enum AXT_MOTION_EXTERNAL_COUNTER_INPUT:uint
{
    ObverseUpDownMode                                       = 0x0,    // Forward Up/Down
    ObverseSqr1Mode                                         = 0x1,    // Forward 1multiplication
    ObverseSqr2Mode                                         = 0x2,    // Forward 2multiplication
    ObverseSqr4Mode                                         = 0x3,    // Forward 4multiplication
    ReverseUpDownMode                                       = 0x4,    // Reverse Up/Down
    ReverseSqr1Mode                                         = 0x5,    // Reverse 1multiplication
    ReverseSqr2Mode                                         = 0x6,    // Reverse 2multiplication
    ReverseSqr4Mode                                         = 0x7     // Reverse 4multiplication
}

public enum AXT_MOTION_ACC_UNIT:uint
{
    UNIT_SEC2                 = 0x0,       // unit/sec2
    SEC                       = 0x1        // sec
}

public enum AXT_MOTION_MOVE_DIR:uint
{
    DIR_CCW                                                 = 0x0,    // Counterclockwise, reverse (jog drive reference)
    DIR_CW                                                  = 0x1     // Clockwise, forward (jog drive reference)
}

public enum AXT_MOTION_RADIUS_DISTANCE:uint
{
    SHORT_DISTANCE                                          = 0x0,    // Short arc travel
    LONG_DISTANCE                                           = 0x1     // Long arc travel
}

public enum AXT_MOTION_POS_TYPE : uint
{
    POSITION_LIMIT                                          = 0x0,    // Operate within full range
    POSITION_BOUND                                          = 0x1     // Position range cyclic type
}

public enum AXT_MOTION_INTERPOLATION_AXIS:uint
{
    INTERPOLATION_AXIS2                                     = 0x0,    // Use 2 axes as interpolation
    INTERPOLATION_AXIS3                                     = 0x1,    // Use 3 axes as interpolation
    INTERPOLATION_AXIS4                                     = 0x2     // Use 4 axes as interpolation
}

public enum AXT_MOTION_CONTISTART_NODE:uint
{
    CONTI_NODE_VELOCITY                                     = 0x0,    // Speed specified interpolation mode
    CONTI_NODE_MANUAL                                       = 0x1,    // Node acceleration / deceleration interpolation mode
    CONTI_NODE_AUTO                                         = 0x2     // Automatic acceleration / deceleration interpolation mode
}

public enum AXT_MOTION_HOME_DETECT:uint
{
    PosEndLimit                                             = 0x0,    // +Elm(End limit) + Direction limit sensor signal
    NegEndLimit                                             = 0x1,    // -Elm(End limit) - Direction limit sensor signal
    PosSloLimit                                             = 0x2,    // +Slm(Slow Down limit) signal - Unused
    NegSloLimit                                             = 0x3,    // -Slm(Slow Down limit) signal - Unused
    HomeSensor                                              = 0x4,    // IN0(ORG)  Origin sensor signal
    EncodZPhase                                             = 0x5,    // IN1(Z phase)  Encoder Z phase signal
    UniInput02                                              = 0x6,    // IN2(Universal) Universal input 2 signal
    UniInput03                                              = 0x7,    // IN3(Universal) Universal input 3 signal
    UniInput04                                              = 0x8,    // IN4(Universal) Universal input 4 signal
    UniInput05                                              = 0x9,    // IN5(Universal) Universal input 5 signal
    TorqueLimit                                             = 0x10,   // Using Motor Torque Limit Signal
    HomingMethod                                            = 0x64,   // Homing Method Base
    HomingMethod_01                                         = 0x65,   // Homing Method Start(90 ~ 137) 
    //...
    HomingMethod_37                                         = 0x89,   // Homing Method Start(90 ~ 137)  
}

public enum AXT_MOTION_INPUT_FILTER_SIGNAL_DEF:uint
{
    END_LIMIT                                               = 0x10,    // End limit +/- direction limit sensor signal
    INP_ALARM                                               = 0x11,    // Inposition/Alarm signal
    UIN_00_01                                               = 0x12,    // Home/Z-Phase signal
    UIN_02_04                                               = 0x13,    // UIN 2, 3, 4 signal    
}

public enum AXT_MOTION_MPG_INPUT_METHOD:uint
{
    MPG_DIFF_ONE_PHASE                                      = 0x0,    // MPG input method One Phase
    MPG_DIFF_TWO_PHASE_1X                                   = 0x1,    // MPG input method TwoPhase1
    MPG_DIFF_TWO_PHASE_2X                                   = 0x2,    // MPG input method TwoPhase2
    MPG_DIFF_TWO_PHASE_4X                                   = 0x3,    // MPG input method TwoPhase4
    MPG_LEVEL_ONE_PHASE                                     = 0x4,    // MPG input method Level One Phase
    MPG_LEVEL_TWO_PHASE_1X                                  = 0x5,    // MPG input method Level Two Phase1
    MPG_LEVEL_TWO_PHASE_2X                                  = 0x6,    // MPG input method Level Two Phase2
    MPG_LEVEL_TWO_PHASE_4X                                  = 0x7     // MPG input method Level Two Phase4
}

public enum AXT_MOTION_SENSOR_INPUT_METHOD:uint
{
    SENSOR_METHOD1                                          = 0x0,    // General drive
    SENSOR_METHOD2                                          = 0x1,    // Low speed drive before sensor signal detection. Normal drive after signal detection
    SENSOR_METHOD3                                          = 0x2     // Low-speed drive
}

public enum AXT_MOTION_HOME_CRC_SELECT:uint
{
    CRC_SELECT1                                             = 0x0,    // Position clear not used, residual pulse clear disabled
    CRC_SELECT2                                             = 0x1,    // Use position clear, clear residual pulse
    CRC_SELECT3                                             = 0x2,    // Position clear not used, residual pulse clear used
    CRC_SELECT4                                             = 0x3     // Use position clear, use residual pulse clear
}

public enum AXT_MOTION_IPDETECT_DESTINATION_SIGNAL:uint
{
    PElmNegativeEdge                                        = 0x0,    // +Elm(End limit) falling edge
    NElmNegativeEdge                                        = 0x1,    // -Elm(End limit) falling edge
    PSlmNegativeEdge                                        = 0x2,    // +Slm(Slowdown limit) falling edge
    NSlmNegativeEdge                                        = 0x3,    // -Slm(Slowdown limit) falling edge
    In0DownEdge                                             = 0x4,    // IN0(ORG) falling edge
    In1DownEdge                                             = 0x5,    // IN1(Z phase) falling edge
    In2DownEdge                                             = 0x6,    // IN2(Universal) falling edge
    In3DownEdge                                             = 0x7,    // IN3(Universal) falling edge
    PElmPositiveEdge                                        = 0x8,    // +Elm(End limit) uprising edge
    NElmPositiveEdge                                        = 0x9,    // -Elm(End limit) uprising edge
    PSlmPositiveEdge                                        = 0xa,    // +Slm(Slowdown limit) uprising edge
    NSlmPositiveEdge                                        = 0xb,    // -Slm(Slowdown limit) uprising edge
    In0UpEdge                                               = 0xc,    // IN0(ORG) uprising edge
    In1UpEdge                                               = 0xd,    // IN1(Z phase) uprising edge
    In2UpEdge                                               = 0xe,    // IN2(Universal) uprising edge
    In3UpEdge                                               = 0xf     // IN3(Universal) uprising edge
}

public enum AXT_MOTION_IPEND_STATUS:uint
{
    IPEND_STATUS_SLM                                        = 0x0001,    // Bit 0, Limit Stop by input of deceleration stop signal
    IPEND_STATUS_ELM                                        = 0x0002,    // Bit 1, Limit Termination by emergency stop signal input
    IPEND_STATUS_SSTOP_SIGNAL                               = 0x0004,    // Bit 2, End by deceleration stop signal input
    IPEND_STATUS_ESTOP_SIGNAL                               = 0x0008,    // Bit 3, Termination by emergency stop signal input
    IPEND_STATUS_SSTOP_COMMAND                              = 0x0010,    // Bit 4, End by deceleration stop command
    IPEND_STATUS_ESTOP_COMMAND                              = 0x0020,    // Bit 5, Termination by emergency stop command
    IPEND_STATUS_ALARM_SIGNAL                               = 0x0040,    // Bit 6, Termination by input of alarm signal
    IPEND_STATUS_DATA_ERROR                                 = 0x0080,    // Bit 7, Termination due to data setting error
    IPEND_STATUS_DEVIATION_ERROR                            = 0x0100,    // Bit 8, Termination due to out-of-sync error
    IPEND_STATUS_ORIGIN_DETECT                              = 0x0200,    // Bit 9, End by origin detection
    IPEND_STATUS_SIGNAL_DETECT                              = 0x0400,    // Bit 10, End by signal detection (Signal search-1/2 drive termination)
    IPEND_STATUS_PRESET_PULSE_DRIVE                         = 0x0800,    // Bit 11, Preset pulse drive end
    IPEND_STATUS_SENSOR_PULSE_DRIVE                         = 0x1000,    // Bit 12, Sensor pulse drive end
    IPEND_STATUS_LIMIT                                      = 0x2000,    // Bit 13, Termination by Limit complete stop
    IPEND_STATUS_SOFTLIMIT                                  = 0x4000,    // Bit 14, End by soft limit
    IPEND_STATUS_INTERPOLATION_DRIVE                        = 0x8000     // Bit 15, Termination by interpolation drive.
}

public enum AXT_MOTION_IPDRIVE_STATUS:uint
{
    IPDRIVE_STATUS_BUSY                                     = 0x00001,    // Bit 0, BUSY(in Drive)
    IPDRIVE_STATUS_DOWN                                     = 0x00002,    // Bit 1, DOWN(Decelerating)
    IPDRIVE_STATUS_CONST                                    = 0x00004,    // Bit 2, CONST(At constant speed)
    IPDRIVE_STATUS_UP                                       = 0x00008,    // Bit 3, UP(Accelerating)
    IPDRIVE_STATUS_ICL                                      = 0x00010,    // Bit 4, ICL(Internal position counter < Internal position counter comparison value)
    IPDRIVE_STATUS_ICG                                      = 0x00020,    // Bit 5, ICG(Internal position counter > Internal position counter comparison value)
    IPDRIVE_STATUS_ECL                                      = 0x00040,    // Bit 6, ECL(External position counter < External position counter comparison value)
    IPDRIVE_STATUS_ECG                                      = 0x00080,    // Bit 7, ECG(External position counter > External position counter comparison value)
    IPDRIVE_STATUS_DRIVE_DIRECTION                          = 0x00100,    // Bit 8, Driver direction signal(0=CW/1=CCW)
    IPDRIVE_STATUS_COMMAND_BUSY                             = 0x00200,    // Bit 9, Executing the command
    IPDRIVE_STATUS_PRESET_DRIVING                           = 0x00400,    // Bit 10, Preset pulse drive
    IPDRIVE_STATUS_CONTINUOUS_DRIVING                       = 0x00800,    // Bit 11, Continuouse speed drive
    IPDRIVE_STATUS_SIGNAL_SEARCH_DRIVING                    = 0x01000,    // Bit 12, Signal search-1/2 drive
    IPDRIVE_STATUS_ORG_SEARCH_DRIVING                       = 0x02000,    // Bit 13, Home detection drive
    IPDRIVE_STATUS_MPG_DRIVING                              = 0x04000,    // Bit 14, MPG drive
    IPDRIVE_STATUS_SENSOR_DRIVING                           = 0x08000,    // Bit 15, Sensor positioning drive
    IPDRIVE_STATUS_L_C_INTERPOLATION                        = 0x10000,    // Bit 16, During linear / circular interpolation
    IPDRIVE_STATUS_PATTERN_INTERPOLATION                    = 0x20000,    // Bit 17, During bit pattern interpolation
    IPDRIVE_STATUS_INTERRUPT_BANK1                          = 0x40000,    // Bit 18, Interrupt occurs at bank1
    IPDRIVE_STATUS_INTERRUPT_BANK2                          = 0x80000     // Bit 19, Interrupt occurs at bank2
}

public enum AXT_MOTION_IPINTERRUPT_BANK1:uint
{
    IPINTBANK1_DONTUSE                  = 0x00000000,   // INTERRUT DISABLED.
    IPINTBANK1_DRIVE_END                = 0x00000001,   // Bit 0, Drive end(default value : 1).
    IPINTBANK1_ICG                      = 0x00000002,   // Bit 1, INCNT is greater than INCNTCMP.
    IPINTBANK1_ICE                      = 0x00000004,   // Bit 2, INCNT is equal with INCNTCMP.
    IPINTBANK1_ICL                      = 0x00000008,   // Bit 3, INCNT is less than INCNTCMP.
    IPINTBANK1_ECG                      = 0x00000010,   // Bit 4, EXCNT is greater than EXCNTCMP.
    IPINTBANK1_ECE                      = 0x00000020,   // Bit 5, EXCNT is equal with EXCNTCMP.
    IPINTBANK1_ECL                      = 0x00000040,   // Bit 6, EXCNT is less than EXCNTCMP.
    IPINTBANK1_SCRQEMPTY                = 0x00000080,   // Bit 7, Script control queue is empty.
    IPINTBANK1_CAPRQEMPTY               = 0x00000100,   // Bit 8, Caption result data queue is empty.
    IPINTBANK1_SCRREG1EXE               = 0x00000200,   // Bit 9, Script control register-1 command is executed.
    IPINTBANK1_SCRREG2EXE               = 0x00000400,   // Bit 10, Script control register-2 command is executed.
    IPINTBANK1_SCRREG3EXE               = 0x00000800,   // Bit 11, Script control register-3 command is executed.
    IPINTBANK1_CAPREG1EXE               = 0x00001000,   // Bit 12, Caption control register-1 command is executed.
    IPINTBANK1_CAPREG2EXE               = 0x00002000,   // Bit 13, Caption control register-2 command is executed.
    IPINTBANK1_CAPREG3EXE               = 0x00004000,   // Bit 14, Caption control register-3 command is executed.
    IPINTBANK1_INTGGENCMD               = 0x00008000,   // Bit 15, Interrupt generation command is executed(0xFF)
    IPINTBANK1_DOWN                     = 0x00010000,   // Bit 16, At starting point for deceleration drive.
    IPINTBANK1_CONT                     = 0x00020000,   // Bit 17, At starting point for constant speed drive.
    IPINTBANK1_UP                       = 0x00040000,   // Bit 18, At starting point for acceleration drive.
    IPINTBANK1_SIGNALDETECTED           = 0x00080000,   // Bit 19, Signal assigned in MODE1 is detected.
    IPINTBANK1_SP23E                    = 0x00100000,   // Bit 20, Current speed is equal with rate change point RCP23.
    IPINTBANK1_SP12E                    = 0x00200000,   // Bit 21, Current speed is equal with rate change point RCP12.
    IPINTBANK1_SPE                      = 0x00400000,   // Bit 22, Current speed is equal with speed comparison data(SPDCMP).
    IPINTBANK1_INCEICM                  = 0x00800000,   // Bit 23, INTCNT(1'st counter) is equal with ICM(1'st count minus limit data)
    IPINTBANK1_SCRQEXE                  = 0x01000000,   // Bit 24, Script queue command is executed When SCRCONQ's 30 bit is '1'.
    IPINTBANK1_CAPQEXE                  = 0x02000000,   // Bit 25, Caption queue command is executed When CAPCONQ's 30 bit is '1'.
    IPINTBANK1_SLM                      = 0x04000000,   // Bit 26, NSLM/PSLM input signal is activated.
    IPINTBANK1_ELM                      = 0x08000000,   // Bit 27, NELM/PELM input signal is activated.
    IPINTBANK1_USERDEFINE1              = 0x10000000,   // Bit 28, Selectable interrupt source 0(refer "0xFE" command).
    IPINTBANK1_USERDEFINE2              = 0x20000000,   // Bit 29, Selectable interrupt source 1(refer "0xFE" command).
    IPINTBANK1_USERDEFINE3              = 0x40000000,   // Bit 30, Selectable interrupt source 2(refer "0xFE" command).
    IPINTBANK1_USERDEFINE4              = 0x80000000    // Bit 31, Selectable interrupt source 3(refer "0xFE" command).
}

public enum AXT_MOTION_IPINTERRUPT_BANK2:uint
{
    IPINTBANK2_DONTUSE                  = 0x00000000,   // INTERRUT DISABLED.
    IPINTBANK2_L_C_INP_Q_EMPTY          = 0x00000001,   // Bit 0, Linear/Circular interpolation parameter queue is empty.
    IPINTBANK2_P_INP_Q_EMPTY            = 0x00000002,   // Bit 1, Bit pattern interpolation queue is empty.
    IPINTBANK2_ALARM_ERROR              = 0x00000004,   // Bit 2, Alarm input signal is activated.
    IPINTBANK2_INPOSITION               = 0x00000008,   // Bit 3, Inposition input signal is activated.
    IPINTBANK2_MARK_SIGNAL_HIGH         = 0x00000010,   // Bit 4, Mark input signal is activated.
    IPINTBANK2_SSTOP_SIGNAL             = 0x00000020,   // Bit 5, SSTOP input signal is activated.
    IPINTBANK2_ESTOP_SIGNAL             = 0x00000040,   // Bit 6, ESTOP input signal is activated.
    IPINTBANK2_SYNC_ACTIVATED           = 0x00000080,   // Bit 7, SYNC input signal is activated.
    IPINTBANK2_TRIGGER_ENABLE           = 0x00000100,   // Bit 8, Trigger output is activated.
    IPINTBANK2_EXCNTCLR                 = 0x00000200,   // Bit 9, External(2'nd) counter is cleard by EXCNTCLR setting.
    IPINTBANK2_FSTCOMPARE_RESULT_BIT0   = 0x00000400,   // Bit 10, ALU1's compare result bit 0 is activated.
    IPINTBANK2_FSTCOMPARE_RESULT_BIT1   = 0x00000800,   // Bit 11, ALU1's compare result bit 1 is activated.
    IPINTBANK2_FSTCOMPARE_RESULT_BIT2   = 0x00001000,   // Bit 12, ALU1's compare result bit 2 is activated.
    IPINTBANK2_FSTCOMPARE_RESULT_BIT3   = 0x00002000,   // Bit 13, ALU1's compare result bit 3 is activated.
    IPINTBANK2_FSTCOMPARE_RESULT_BIT4   = 0x00004000,   // Bit 14, ALU1's compare result bit 4 is activated.
    IPINTBANK2_SNDCOMPARE_RESULT_BIT0   = 0x00008000,   // Bit 15, ALU2's compare result bit 0 is activated.
    IPINTBANK2_SNDCOMPARE_RESULT_BIT1   = 0x00010000,   // Bit 16, ALU2's compare result bit 1 is activated.
    IPINTBANK2_SNDCOMPARE_RESULT_BIT2   = 0x00020000,   // Bit 17, ALU2's compare result bit 2 is activated.
    IPINTBANK2_SNDCOMPARE_RESULT_BIT3   = 0x00040000,   // Bit 18, ALU2's compare result bit 3 is activated.
    IPINTBANK2_SNDCOMPARE_RESULT_BIT4   = 0x00080000,   // Bit 19, ALU2's compare result bit 4 is activated.
    IPINTBANK2_L_C_INP_Q_LESS_4         = 0x00100000,   // Bit 20, Linear/Circular interpolation parameter queue is less than 4.
    IPINTBANK2_P_INP_Q_LESS_4           = 0x00200000,   // Bit 21, Pattern interpolation parameter queue is less than 4.
    IPINTBANK2_XSYNC_ACTIVATED          = 0x00400000,   // Bit 22, X axis sync input signal is activated.
    IPINTBANK2_YSYNC_ACTIVATED          = 0x00800000,   // Bit 23, Y axis sync input siangl is activated.
    IPINTBANK2_P_INP_END_BY_END_PATTERN = 0x01000000    // Bit 24, Bit pattern interpolation is terminated by end pattern.
    //    IPINTBANK2_                   = 0x02000000,   // Bit 25, Don't care.
    //    IPINTBANK2_                   = 0x04000000,   // Bit 26, Don't care.
    //    IPINTBANK2_                   = 0x08000000,   // Bit 27, Don't care.
    //    IPINTBANK2_                   = 0x10000000,   // Bit 28, Don't care.
    //    IPINTBANK2_                   = 0x20000000,   // Bit 29, Don't care.
    //    IPINTBANK2_                   = 0x40000000,   // Bit 30, Don't care.
    //    IPINTBANK2_                   = 0x80000000    // Bit 31, Don't care.
}

public enum AXT_MOTION_IPMECHANICAL_SIGNAL:uint
{
    IPMECHANICAL_PELM_LEVEL             = 0x0001,       // Bit 0, +Limit emergency stop signal input Level
    IPMECHANICAL_NELM_LEVEL             = 0x0002,       // Bit 1, -Limit emergency stop signal input Level
    IPMECHANICAL_PSLM_LEVEL             = 0x0004,       // Bit 2, +limit decelerate stop signal input Level
    IPMECHANICAL_NSLM_LEVEL             = 0x0008,       // Bit 3, -limit decelerate stop signal input Level
    IPMECHANICAL_ALARM_LEVEL            = 0x0010,       // Bit 4, Alarm signal input Level
    IPMECHANICAL_INP_LEVEL              = 0x0020,       // Bit 5, InPos signal input Level
    IPMECHANICAL_ENC_DOWN_LEVEL         = 0x0040,       // Bit 6, encoder DOWN(B phase) signal input Level
    IPMECHANICAL_ENC_UP_LEVEL           = 0x0080,       // Bit 7, encoder UP(A phase) signal input Level
    IPMECHANICAL_EXMP_LEVEL             = 0x0100,       // Bit 8, EXMP signal input Level
    IPMECHANICAL_EXPP_LEVEL             = 0x0200,       // Bit 9, EXPP signal input Level
    IPMECHANICAL_MARK_LEVEL             = 0x0400,       // Bit 10, MARK# signal input Level
    IPMECHANICAL_SSTOP_LEVEL            = 0x0800,       // Bit 11, SSTOP signal input Level
    IPMECHANICAL_ESTOP_LEVEL            = 0x1000,       // Bit 12, ESTOP signal input Level
    IPMECHANICAL_SYNC_LEVEL             = 0x2000,       // Bit 13, SYNC signal input Level
    IPMECHANICAL_MODE8_16_LEVEL         = 0x4000        // Bit 14, MODE8_16 signal input Level
}


public enum AXT_MOTION_QIDETECT_DESTINATION_SIGNAL:uint
{
    Signal_PosEndLimit                  = 0x0,          // +Elm(End limit) + direction limit sensor signal
    Signal_NegEndLimit                  = 0x1,          // -Elm(End limit) - direction limit sensor signal
    Signal_PosSloLimit                  = 0x2,          // +Slm(Slow Down limit) signal - Not-used
    Signal_NegSloLimit                  = 0x3,          // -Slm(Slow Down limit) signal - Not-used
    Signal_HomeSensor                   = 0x4,          // IN0(ORG)  home sensor signal
    Signal_EncodZPhase                  = 0x5,          // IN1(Z phase)  Encoder Z phase signal
    Signal_UniInput02                   = 0x6,          // IN2(universal) universal input number 2 signal
    Signal_UniInput03                   = 0x7           // IN3(universal) universal input number 3 signal
}

public enum AXT_MOTION_QIMECHANICAL_SIGNAL:uint
{
    QIMECHANICAL_PELM_LEVEL             = 0x00001,      // Bit 0, +Limit emergency stop signal current state
    QIMECHANICAL_NELM_LEVEL             = 0x00002,      // Bit 1, -Limit emergency stop signal current state
    QIMECHANICAL_PSLM_LEVEL             = 0x00004,      // Bit 2, +limit decelerate stop signal current state
    QIMECHANICAL_NSLM_LEVEL             = 0x00008,      // Bit 3, -limit decelerate stop signal current state
    QIMECHANICAL_ALARM_LEVEL            = 0x00010,      // Bit 4, Alarm signal current state
    QIMECHANICAL_INP_LEVEL              = 0x00020,      // Bit 5, InPos signal current state
    QIMECHANICAL_ESTOP_LEVEL            = 0x00040,      // Bit 6, emergency stop signal(ESTOP) current state
    QIMECHANICAL_ORG_LEVEL              = 0x00080,      // Bit 7, home signal current state 
    QIMECHANICAL_ZPHASE_LEVEL           = 0x00100,      // Bit 8, Z phase input signal current state 
    QIMECHANICAL_ECUP_LEVEL             = 0x00200,      // Bit 9, ECUP terminal signal state
    QIMECHANICAL_ECDN_LEVEL             = 0x00400,      // Bit 10, ECDN terminal signal state
    QIMECHANICAL_EXPP_LEVEL             = 0x00800,      // Bit 11, EXPP terminal signal state
    QIMECHANICAL_EXMP_LEVEL             = 0x01000,      // Bit 12, EXMP terminal signal state
    QIMECHANICAL_SQSTR1_LEVEL           = 0x02000,      // Bit 13, SQSTR1 terminal signal state
    QIMECHANICAL_SQSTR2_LEVEL           = 0x04000,      // Bit 14, SQSTR2 terminal signal state
    QIMECHANICAL_SQSTP1_LEVEL           = 0x08000,      // Bit 15, SQSTP1 terminal signal state
    QIMECHANICAL_SQSTP2_LEVEL           = 0x10000,      // Bit 16, SQSTP2 terminal signal state
    QIMECHANICAL_MODE_LEVEL             = 0x20000       // Bit 17, MODE terminal signal state
}                                       
                                        
public enum AXT_MOTION_QIEND_STATUS:uint
{
    QIEND_STATUS_0                      = 0x00000001,   // Bit 0, exit by forward direction limit signal (PELM)
    QIEND_STATUS_1                      = 0x00000002,   // Bit 1, exit by reverse direction limit signal (NELM)
    QIEND_STATUS_2                      = 0x00000004,   // Bit 2, exit by forward direction additional limit signal (PSLM)
    QIEND_STATUS_3                      = 0x00000008,   // Bit 3, exit by reverse direction additional limit signal (NSLM)
    QIEND_STATUS_4                      = 0x00000010,   // Bit 4, exit by forward direction soft limit emergency stop function
    QIEND_STATUS_5                      = 0x00000020,   // Bit 5, exit by reverse direction soft limit emergency stop function
    QIEND_STATUS_6                      = 0x00000040,   // Bit 6, exit by forward direction soft limit decelerate stop function
    QIEND_STATUS_7                      = 0x00000080,   // Bit 7, exit by reverse direction soft limit decelerate stop function
    QIEND_STATUS_8                      = 0x00000100,   // Bit 8, drive exit by servo alarm function
    QIEND_STATUS_9                      = 0x00000200,   // Bit 9, drive exit by emergency stop signal input
    QIEND_STATUS_10                     = 0x00000400,   // Bit 10, drive exit by emergency stop command
    QIEND_STATUS_11                     = 0x00000800,   // Bit 11, drive exit by decelerate stop command
    QIEND_STATUS_12                     = 0x00001000,   // Bit 12, drive exit by entire axes emergency stop command 
    QIEND_STATUS_13                     = 0x00002000,   // Bit 13, drive exit by sync stop function #1(SQSTP1)
    QIEND_STATUS_14                     = 0x00004000,   // Bit 14, drive exit by sync stop function #2(SQSTP2)
    QIEND_STATUS_15                     = 0x00008000,   // Bit 15, encoder input (ECUP,ECDN) error occurrence
    QIEND_STATUS_16                     = 0x00010000,   // Bit 16, MPG input (EXPP,EXMP) error occurrence
    QIEND_STATUS_17                     = 0x00020000,   // Bit 17, exit with successful home search
    QIEND_STATUS_18                     = 0x00040000,   // Bit 18, exit with successful signal search
    QIEND_STATUS_19                     = 0x00080000,   // Bit 19, drive exit by interpolation data abnormality
    QIEND_STATUS_20                     = 0x00100000,   // Bit 20, abnormal drive stop occurrence
    QIEND_STATUS_21                     = 0x00200000,   // Bit 21, MPG function block pulse buffer overflow occurrence
    QIEND_STATUS_22                     = 0x00400000,   // Bit 22, DON'CARE
    QIEND_STATUS_23                     = 0x00800000,   // Bit 23, DON'CARE
    QIEND_STATUS_24                     = 0x01000000,   // Bit 24, DON'CARE
    QIEND_STATUS_25                     = 0x02000000,   // Bit 25, DON'CARE
    QIEND_STATUS_26                     = 0x04000000,   // Bit 26, DON'CARE
    QIEND_STATUS_27                     = 0x08000000,   // Bit 27, DON'CARE
    QIEND_STATUS_28                     = 0x10000000,   // Bit 28, current/last move drive direction
    QIEND_STATUS_29                     = 0x20000000,   // Bit 29, in output of remaining pulse clear
    QIEND_STATUS_30                     = 0x40000000,   // Bit 30, abnormal drive stop cause state
    QIEND_STATUS_31                     = 0x80000000    // Bit 31, interpolation drive data error state
}

public enum AXT_MOTION_QIDRIVE_STATUS:uint
{
    QIDRIVE_STATUS_0                    = 0x0000001,    // Bit 0, BUSY(in drive move)
    QIDRIVE_STATUS_1                    = 0x0000002,    // Bit 1, DOWN(in deceleration)
    QIDRIVE_STATUS_2                    = 0x0000004,    // Bit 2, CONST(in constant speed)
    QIDRIVE_STATUS_3                    = 0x0000008,    // Bit 3, UP(in acceleration)
    QIDRIVE_STATUS_4                    = 0x0000010,    // Bit 4, in move of continuous drive
    QIDRIVE_STATUS_5                    = 0x0000020,    // Bit 5, in move of assigned distance drive
    QIDRIVE_STATUS_6                    = 0x0000040,    // Bit 6, in move of MPG drive
    QIDRIVE_STATUS_7                    = 0x0000080,    // Bit 7, in move of home search drive
    QIDRIVE_STATUS_8                    = 0x0000100,    // Bit 8, in move of signal search drive
    QIDRIVE_STATUS_9                    = 0x0000200,    // Bit 9, in move of interpolation drive
    QIDRIVE_STATUS_10                   = 0x0000400,    // Bit 10, in move of Slave drive
    QIDRIVE_STATUS_11                   = 0x0000800,    // Bit 11, current move drive direction (different indication information in interpolation drive)
    QIDRIVE_STATUS_12                   = 0x0001000,    // Bit 12, in waiting for servo position completion after pulse output
    QIDRIVE_STATUS_13                   = 0x0002000,    // Bit 13, in move of straight line interpolation drive
    QIDRIVE_STATUS_14                   = 0x0004000,    // Bit 14, in move of circular interpolation drive
    QIDRIVE_STATUS_15                   = 0x0008000,    // Bit 15, in pulse output
    QIDRIVE_STATUS_16                   = 0x0010000,    // Bit 16, drive reserved data number(start)(0-7)
    QIDRIVE_STATUS_17                   = 0x0020000,    // Bit 17, drive reserved data number (middle)(0-7)
    QIDRIVE_STATUS_18                   = 0x0040000,    // Bit 18, drive reserved data number (end)(0-7)
    QIDRIVE_STATUS_19                   = 0x0080000,    // Bit 19, drive reversed Queue is cleared
    QIDRIVE_STATUS_20                   = 0x0100000,    // Bit 20, drive reversed Queue is full
    QIDRIVE_STATUS_21                   = 0x0200000,    // Bit 21, velocity mode of current move drive (start)
    QIDRIVE_STATUS_22                   = 0x0400000,    // Bit 22, velocity mode of current move drive (end)
    QIDRIVE_STATUS_23                   = 0x0800000,    // Bit 23, MPG buffer #1 Full
    QIDRIVE_STATUS_24                   = 0x1000000,    // Bit 24, MPG buffer #2 Full
    QIDRIVE_STATUS_25                   = 0x2000000,    // Bit 25, MPG buffer #3 Full
    QIDRIVE_STATUS_26                   = 0x4000000     // Bit 26, MPG buffer data OverFlow
}                                       

public enum AXT_MOTION_QIINTERRUPT_BANK1:uint
{
    QIINTBANK1_DISABLE                  = 0x00000000,   // INTERRUT DISABLED.
    QIINTBANK1_0                        = 0x00000001,   // Bit 0,  when drive set interrupt occurrence use is exit
    QIINTBANK1_1                        = 0x00000002,   // Bit 1,  when drive is exited
    QIINTBANK1_2                        = 0x00000004,   // Bit 2,  when drive is started
    QIINTBANK1_3                        = 0x00000008,   // Bit 3,  counter #1 < comparator #1 event occurrence
    QIINTBANK1_4                        = 0x00000010,   // Bit 4,  counter #1 = comparator #1 event occurrence
    QIINTBANK1_5                        = 0x00000020,   // Bit 5,  counter #1 > comparator #1 event occurrence
    QIINTBANK1_6                        = 0x00000040,   // Bit 6,  counter #2 < comparator #2 event occurrence
    QIINTBANK1_7                        = 0x00000080,   // Bit 7,  counter #2 = comparator #2 event occurrence
    QIINTBANK1_8                        = 0x00000100,   // Bit 8,  counter #2 > comparator #2 event occurrence
    QIINTBANK1_9                        = 0x00000200,   // Bit 9,  counter #3 < comparator #3 event occurrence
    QIINTBANK1_10                       = 0x00000400,   // Bit 10, counter #3 = comparator #3 event occurrence
    QIINTBANK1_11                       = 0x00000800,   // Bit 11, counter #3 > comparator #3 event occurrence
    QIINTBANK1_12                       = 0x00001000,   // Bit 12, counter #4 < comparator #4 event occurrence
    QIINTBANK1_13                       = 0x00002000,   // Bit 13, counter #4 = comparator #4 event occurrence
    QIINTBANK1_14                       = 0x00004000,   // Bit 14, counter #4 < comparator #4 event occurrence
    QIINTBANK1_15                       = 0x00008000,   // Bit 15, counter #5 < comparator #5 event occurrence
    QIINTBANK1_16                       = 0x00010000,   // Bit 16, counter #5 = comparator #5 event occurrence
    QIINTBANK1_17                       = 0x00020000,   // Bit 17, counter #5 > comparator #5 event occurrence
    QIINTBANK1_18                       = 0x00040000,   // Bit 18, timer #1 event occurrence
    QIINTBANK1_19                       = 0x00080000,   // Bit 19, timer #2 event occurrence
    QIINTBANK1_20                       = 0x00100000,   // Bit 20, drive reservation setting Queue is cleared
    QIINTBANK1_21                       = 0x00200000,   // Bit 21, drive reservation setting Queue is full
    QIINTBANK1_22                       = 0x00400000,   // Bit 22, trigger occurring distance period/absolute position Queue is cleared
    QIINTBANK1_23                       = 0x00800000,   // Bit 23, trigger occurring distance period/absolute position Queue is full
    QIINTBANK1_24                       = 0x01000000,   // Bit 24, trigger signal occurrence event
    QIINTBANK1_25                       = 0x02000000,   // Bit 25, script #1 command reservation setting Queue is cleared
    QIINTBANK1_26                       = 0x04000000,   // Bit 26, script #2 command reservation setting Queue is cleared
    QIINTBANK1_27                       = 0x08000000,   // Bit 27, script #3 initialized with execution of command reservation setting register
    QIINTBANK1_28                       = 0x10000000,   // Bit 28, script #4 initialized with execution of command reservation setting register
    QIINTBANK1_29                       = 0x20000000,   // Bit 29, servo alarm signal is permitted
    QIINTBANK1_30                       = 0x40000000,   // Bit 30, |CNT1| - |CNT2| >= |CNT4| event occurrence
    QIINTBANK1_31                       = 0x80000000    // Bit 31, interrupt occurrence command |INTGEN| execution
}                                     
                                      
public enum AXT_MOTION_QIINTERRUPT_BANK2:uint
{
    QIINTBANK2_DISABLE                  = 0x00000000,   // INTERRUT DISABLED.
    QIINTBANK2_0                        = 0x00000001,   // Bit 0,  script #1 reading command result Queue is full
    QIINTBANK2_1                        = 0x00000002,   // Bit 1,  script #2 reading command result Queue is full
    QIINTBANK2_2                        = 0x00000004,   // Bit 2,  script #3 reading command result register is renewed with new data
    QIINTBANK2_3                        = 0x00000008,   // Bit 3,  script #4 reading command result register is renewed with new data
    QIINTBANK2_4                        = 0x00000010,   // Bit 4,  when reservation command of script #1 is executed, command set by interrupt occurrence is executed
    QIINTBANK2_5                        = 0x00000020,   // Bit 5,  when reservation command of script #2 is executed, command set by interrupt occurrence is executed
    QIINTBANK2_6                        = 0x00000040,   // Bit 6,  when reservation command of script #3 is executed, command set by interrupt occurrence is executed
    QIINTBANK2_7                        = 0x00000080,   // Bit 7,  when reservation command of script #4 is executed, command set by interrupt occurrence is executed
    QIINTBANK2_8                        = 0x00000100,   // Bit 8,  start drive
    QIINTBANK2_9                        = 0x00000200,   // Bit 9,  drive using servo position determination completion(InPos) function, exit condition occurrence
    QIINTBANK2_10                       = 0x00000400,   // Bit 10, event selection #1 condition occurrence to use during event counter operation
    QIINTBANK2_11                       = 0x00000800,   // Bit 11, event selection #2 condition occurrence to use during event counter operation
    QIINTBANK2_12                       = 0x00001000,   // Bit 12, SQSTR1 signal is permitted
    QIINTBANK2_13                       = 0x00002000,   // Bit 13, SQSTR2 signal is permitted
    QIINTBANK2_14                       = 0x00004000,   // Bit 14, UIO0 terminal signal is changed to '1'
    QIINTBANK2_15                       = 0x00008000,   // Bit 15, UIO1 terminal signal is changed to '1'
    QIINTBANK2_16                       = 0x00010000,   // Bit 16, UIO2 terminal signal is changed to '1'
    QIINTBANK2_17                       = 0x00020000,   // Bit 17, UIO3 terminal signal is changed to '1'
    QIINTBANK2_18                       = 0x00040000,   // Bit 18, UIO4 terminal signal is changed to '1'
    QIINTBANK2_19                       = 0x00080000,   // Bit 19, UIO5 terminal signal is changed to '1'
    QIINTBANK2_20                       = 0x00100000,   // Bit 20, UIO6 terminal signal is changed to '1'
    QIINTBANK2_21                       = 0x00200000,   // Bit 21, UIO7 terminal signal is changed to '1'
    QIINTBANK2_22                       = 0x00400000,   // Bit 22, UIO8 terminal signal is changed to '1'
    QIINTBANK2_23                       = 0x00800000,   // Bit 23, UIO9 terminal signal is changed to '1'
    QIINTBANK2_24                       = 0x01000000,   // Bit 24, UIO10 terminal signal is changed to '1'
    QIINTBANK2_25                       = 0x02000000,   // Bit 25, UIO11 terminal signal is changed to '1'
    QIINTBANK2_26                       = 0x04000000,   // Bit 26, error stop condition (LMT, ESTOP, STOP, ESTOP, CMD, ALARM) occurrence
    QIINTBANK2_27                       = 0x08000000,   // Bit 27, data setting error is occurred during interpolation
    QIINTBANK2_28                       = 0x10000000,   // Bit 28, Don't Care
    QIINTBANK2_29                       = 0x20000000,   // Bit 29, limit signal (PELM, NELM) is inputted
    QIINTBANK2_30                       = 0x40000000,   // Bit 30, additional limit signal (PSLM, NSLM) is inputted
    QIINTBANK2_31                       = 0x80000000    // Bit 31, Emergency stop signal (ESTOP) is input
}                                
public enum AXT_EVENT:uint       
{
    WM_USER                            = 0x0400,
    WM_AXL_INTERRUPT                    = (WM_USER + 1001)
}

public enum AXT_NETWORK_STATUS : uint
{
    NET_STATUS_DISCONNECTED  = 1,
    NET_STATUS_LOCK_MISMATCH = 5,
    NET_STATUS_CONNECTED     = 6
}

public struct MOTION_INFO
{
    public double dCmdPos;      // Command position[0x01]
    public double dActPos;      // Encoder position[0x02]
    public uint uMechSig;       // Mechanical Signal[0x04]
    public uint uDrvStat;       // Driver Status[0x08]
    public uint uInput;         // Universal Signal Input[0x10]
    public uint uOutput;        // Universal Signal Output[0x10]
    public uint uMask;          // Read Setting Mask Ex) 0x1F All status read
}

public enum AXT_MOTION_OVERRIDE_MODE : uint
{
    OVERRIDE_POS_START                                     = 0,
    OVERRIDE_POS_END                                       = 1,
    OVERRIDE_POS_AUTO                                      = 2
}

public enum AXT_MOTION_PROFILE_PRIORITY : uint
{
    PRIORITY_VELOCITY        = 0,
    PRIORITY_ACCELTIME       = 1
}

public enum AXT_MOTION_FUNC_RETURN_MODE_DEF : uint
{
    FUNC_RETURN_IMMEDIATE       = 0,
    FUNC_RETURN_BLOCKING        = 1,
    FUNC_RETURN_NON_BLOCKING    = 2
}

public enum AXT_MOTION_BACKLASH_DIR : uint
{
	BACKLASH_DIR_P                                         = 0,
	BACKLASH_DIR_N                                         = 1,
	BACKLASH_DIR_U                                         = 2
}

public enum AXT_MOTION_MLIII_CONNECTION_STATUS : uint
{
    COM_CONNECT                 = 0,
    COM_DISCONNECT              = 1,
    COM_OFFLINE                 = 2
}

public enum MONITOR_SELECT_INFORMATION : uint
{
    _MONI_APOS_ = 0,     // feedback position  : current position of the motor
    _MONI_CPOS_,         // command position   : command position after acceleration/deceleration filter
    _MONI_PERR_,         // position error     : Position error of the control loop
    _MONI_LPOS1_,        // latched position 1 : motor position 1 latched by the latch signal 
    _MONI_LPOS2_,        // latched position 2 : motor position 2 latched by the latch signal
    _MONI_FSPD_,         // feedback speed     : current speed of the motor 
    _MONI_CSPD_,         // reference speed    : command speed of the motor
    _MONI_TRQ_,          // torque(force) reference : command torque(force) of the motor
    _MONI_ALARM_,        // detailed information on the current alarm : current alarm/warning (2-byte data, higher 2 bytes fixed as 0x0000)
    _MONI_MPOS_,         // command position(optional) : the details of the monitor data are specified in the product specifications. example : internal command position of the control loop
    _MONI_RES1_,         // reserved
    _MONI_RES2_,         // reserved
    _MONI_CMN1_,         // common monitor 1 : selects the monitor data specified at common parameter 89.(for the contents of the monitor data, refer to common parameter 89)
    _MONI_CMN2_,         // common monitor 2 : selects the monitor data specified at common parameter 8A.(for the contents of the monitor data, refer to common parameter 8A)
    _MONI_OMN1_,         // optional monitor 1 : selects the monitor data specified by parameter.(the contents of the monitor data depend on the product specifications)
    _MONI_OMN2_          // optional monitor 2 : selects the monitor data specified by parameter.(the contents of the monitor data depend on the product specifications)
}

public class CAXHS
{
    public delegate void AXT_INTERRUPT_PROC(int nActiveNo, uint uFlag);

    public readonly static uint WM_USER = 0x0400;
    public readonly static uint WM_AXL_INTERRUPT = (WM_USER + 1001);
    public readonly static uint MAX_SERVO_ALARM_HISTORY = 15;

    public static int  AXIS_EVN(int nAxisNo) 
    {
        nAxisNo = (nAxisNo - (nAxisNo % 2));
        return nAxisNo;
    }

    public static int  AXIS_ODD(int nAxisNo) 
    {
        nAxisNo = (nAxisNo + ((nAxisNo + 1) % 2));
        return nAxisNo;
    }

    public static int  AXIS_QUR(int nAxisNo) 
    {
        nAxisNo = (nAxisNo % 4);
        return nAxisNo;
    }

    public static int  AXIS_N04(int nAxisNo, int nPos) 
    {
        nAxisNo = (((nAxisNo / 4) * 4) + nPos);
        return nAxisNo;
    }

    public static int  AXIS_N01(int nAxisNo) 
    {
        nAxisNo = ((nAxisNo % 4) >> 2);
        return nAxisNo;
    }

    public static int  AXIS_N02(int nAxisNo) 
    {
        nAxisNo = ((nAxisNo % 4)  % 2);
        return nAxisNo;
    }

    public static int    m_SendAxis            =0;

    public const int F_50M_CLK = 50000000;			/* 50.000 MHz */
}

public enum CNTPORT_DATA_WRITE : uint
{
    CnCommand	= 0x10,
    CnData1		= 0x12,
    CnData2		= 0x14,
    CnData3		= 0x16,
    CnData4		= 0x18,
    CnData12    = 0x44,
    CnData34    = 0x46
}

public enum _CNTRAM_DATA : uint
{
    CnRamAddr1	= 0x28,
    CnRamAddr2	= 0x2A,
    CnRamAddr3	= 0x2C,
    CnRamAddrx1 = 0x48,
    CnRamAddr23 = 0x4A
}

public enum _PHASE_SEL
{
    CnAbPhase	= 0x0,
    CnZPhase	= 0x1
}

public enum _COUNTER_INPUT
{
    CnUpDownMode = 0x0,					// Up/Down
    CnSqr1Mode   = 0x1,					// 1 multiplication
    CnSqr2Mode   = 0x2,					// 2 multiplication
    CnSqr4Mode   = 0x3					// 4 multiplication
}

public enum _CNTCOMMAND
{
    // CH-1 Group Register
    CnCh1CounterRead					= 0x10,				// CH1 COUNTER READ, 24BIT
    CnCh1CounterWrite					= 0x90,				// CH1 COUNTER WRITE
    CnCh1CounterModeRead				= 0x11,				// CH1 COUNTER MODE READ, 8BIT
    CnCh1CounterModeWrite				= 0x91,				// CH1 COUNTER MODE WRITE
    CnCh1TriggerRegionLowerDataRead		= 0x12,				// CH1 TRIGGER REGION LOWER DATA READ, 24BIT
    CnCh1TriggerRegionLowerDataWrite	= 0x92,				// CH1 TRIGGER REGION LOWER DATA WRITE
    CnCh1TriggerRegionUpperDataRead		= 0x13,				// CH1 TRIGGER REGION UPPER DATA READ, 24BIT
    CnCh1TriggerRegionUpperDataWrite	= 0x93,				// CH1 TRIGGER REGION UPPER DATA WRITE
    CnCh1TriggerPeriodRead				= 0x14,				// CH1 TRIGGER PERIOD READ, 24BIT, RESERVED
    CnCh1TriggerPeriodWrite				= 0x94,				// CH1 TRIGGER PERIOD WRITE
    CnCh1TriggerPulseWidthRead			= 0x15,				// CH1 TRIGGER PULSE WIDTH READ
    CnCh1TriggerPulseWidthWrite			= 0x95,				// CH1 RIGGER PULSE WIDTH WRITE
    CnCh1TriggerModeRead				= 0x16,				// CH1 TRIGGER MODE READ
    CnCh1TriggerModeWrite				= 0x96,				// CH1 RIGGER MODE WRITE
    CnCh1TriggerStatusRead				= 0x17,				// CH1 TRIGGER STATUS READ
    CnCh1NoOperation_97					= 0x97,				// 
    CnCh1TriggerEnable					= 0x98,
    CnCh1TriggerDisable					= 0x99,
    CnCh1TimeTriggerFrequencyRead		= 0x1A,
    CnCh1TimeTriggerFrequencyWrite		= 0x9A,
    CnCh1ComparatorValueRead			= 0x1B,
    CnCh1ComparatorValueWrite			= 0x9B,
    CnCh1CompareatorConditionRead		= 0x1D,
    CnCh1CompareatorConditionWrite		= 0x9D,
    
    // CH-2 Group Register
    CnCh2CounterRead					= 0x20,				// CH2 COUNTER READ, 24BIT
    CnCh2CounterWrite					= 0xA1,				// CH2 COUNTER WRITE
    CnCh2CounterModeRead				= 0x21,				// CH2 COUNTER MODE READ, 8BIT
    CnCh2CounterModeWrite				= 0xA1,				// CH2 COUNTER MODE WRITE
    CnCh2TriggerRegionLowerDataRead		= 0x22,				// CH2 TRIGGER REGION LOWER DATA READ, 24BIT
    CnCh2TriggerRegionLowerDataWrite	= 0xA2,				// CH2 TRIGGER REGION LOWER DATA WRITE
    CnCh2TriggerRegionUpperDataRead		= 0x23,				// CH2 TRIGGER REGION UPPER DATA READ, 24BIT
    CnCh2TriggerRegionUpperDataWrite	= 0xA3,				// CH2 TRIGGER REGION UPPER DATA WRITE
    CnCh2TriggerPeriodRead				= 0x24,				// CH2 TRIGGER PERIOD READ, 24BIT, RESERVED
    CnCh2TriggerPeriodWrite				= 0xA4,				// CH2 TRIGGER PERIOD WRITE
    CnCh2TriggerPulseWidthRead			= 0x25,				// CH2 TRIGGER PULSE WIDTH READ, 24BIT
    CnCh2TriggerPulseWidthWrite			= 0xA5,				// CH2 RIGGER PULSE WIDTH WRITE
    CnCh2TriggerModeRead				= 0x26,				// CH2 TRIGGER MODE READ
    CnCh2TriggerModeWrite				= 0xA6,				// CH2 RIGGER MODE WRITE
    CnCh2TriggerStatusRead				= 0x27,				// CH2 TRIGGER STATUS READ
    CnCh2NoOperation_A7					= 0xA7,				//
    CnCh2TriggerEnable					= 0xA8,
    CnCh2TriggerDisable					= 0xA9,
    CnCh2TimeTriggerFrequencyRead		= 0x2A,
    CnCh2TimeTriggerFrequencyWrite		= 0xAA,
    CnCh2ComparatorValueRead			= 0x2B,
    CnCh2ComparatorValueWrite			= 0xAB,
    CnCh2CompareatorConditionRead		= 0x2D,
    CnCh2CompareatorConditionWrite		= 0xAD,

    // Ram Access Group Register
    CnRamDataWithRamAddress				= 0x30,				// READ RAM DATA WITH RAM ADDR PORT ADDRESS
    CnRamDataWrite						= 0xB0,				// RAM DATA WRITE
    CnRamDataRead						= 0x31				// RAM DATA READ, 32BIT
}
public enum AXT_MOTION_JOINT_MODE : uint
{
    AxisMode         = 0x0,
    JointMode        = 0x1,
    ToolMode         = 0x2

}

public enum AXT_MONITOR_SIGNAL_TYPE
{
    // Motion 
    eMonitorSignalType_CmdPos = 0,
    eMonitorSignalType_ActPos,
    eMonitorSignalType_CmdVel,
    eMonitorSignalType_ActVel,    
    eMonitorSignalType_CmdAccDec,
    eMonitorSignalType_ActAccDec,
    eMonitorSignalType_PosErr,
    eMonitorSignalType_InMotion,
    eMonitorSignalType_ActTorque,
    eMonitorSignalType_PositionDemand,
    eMonitorSignalType_VelocityDemand,
    eMonitorSignalType_TorqueDemand,
    eMonitorSignalType_MotionAiValue,
    eMonitorSignalType_MotionDiValue,
    eMonitorSignalType_MotionDoValue,
    eMonitorSignalType_Event_ContiEndNode,

    // Digital I/O
    eMonitorSignalType_DiValue,
    eMonitorSignalType_DoValue,

    // Analog I/O
    eMonitorSignalType_AiValue,
    eMonitorSignalType_AoValue,
    MAX_eMonitorSignalType
}

public enum AXT_MONITOR_OPERATOR_TYPE
{
    eMonitorOperationType_Grater = 0,
    eMonitorOperationType_Smaller,
    eMonitorOperationType_RisingEdge,
    eMonitorOperationType_FallingEdge,
    MAX_eMonitorOperationType
}

public enum AXT_MONITOR_START_OPTION
{
    eMonitorStartOption_Immediately = 0,
    eMonitorStartOption_Trigger,
    MAX_eMonitorStartOption
}

public enum AXT_MONITOR_OVERFLOW_OPTION
{
    eMonitorOverflowOption_IgnoreQueueFull = 0,
    eMonitorOverflowOption_WaitQueueFull,
    MAX_eMonitorOverflowOption
}

public enum _HPCPORT_DATA_WRITE : uint
{
    HpcReset          = 0x06,            // Software reset.
    HpcCommand        = 0x10,
    HpcData12         = 0x12,            // MSB of data port(31 ~ 16 bit)
    HpcData34         = 0x14,            // LSB of data port(15 ~ 0 bit)
    HpcCmStatus       = 0x1C
}

public enum _HPCPORT_CH_STAUTS : uint
{
    HpcCh1Mech        = 0x20,
    HpcCh1Status      = 0x22,
    HpcCh2Mech        = 0x30,
    HpcCh2Status      = 0x32,
    HpcCh3Mech        = 0x40,
    HpcCh3Status      = 0x42,
    HpcCh4Mech        = 0x50,
    HpcCh4Status      = 0x52
}

public enum _HPCPORT_ETC : uint
{
    HpcDiIntFlag      = 0x60,
    HpcDiIntRiseMask  = 0x62,
    HpcDiIntFallMask  = 0x64,
    HpcCompIntFlag    = 0x66,
    HpcCompIntMask    = 0x68,
    HpcDinData        = 0x6A,
    HpcDoutData       = 0x6C
}

public enum _HPCRAM_DATA : uint
{
    HpcRamAddr1       = 0x70,            // MSB of Ram address(31  ~ 16 bit)
    HpcRamAddr2       = 0x72             // LSB of Ram address(15  ~ 0 bit)
}


/*
--1) count_mode
--  0 bit : reverse.(0: normal, 1:reverse)
--  1 bit : count source selection.(0 : A/B 2 phase signal, 1 :  Z phase signal)
--  2~3 bit : count mode
--      "00" up/Down mode 
--      "01" 2 phase, 1times
--      "10" 2 phase, 2times
--      "11" 2 phase, 4times
*/

/*
-- Description : Trigger pulse generation block for SIO-CN2CH
-- trig_mode
-- 0 bit : trigger signal active level.
-- 1 bit : reserved.
-- 2 bit : trigger signal output method, valid when trig_mode(1) = '0', (0 : pulse out, 1 : level out)
-- 3 bit : reserved.
-- 5~4 bit : Trigger method
--    "00" : absolute mode[with ram data].
--    "01" : timer mode.
--    "10" : absolute mode[with fifo].
--    "11" : periodic mode.
-- 6 bit : direction valid check enable/disable when periodic mode.
-- 7 bit : direction valid when periodic mode.
*/

/* CNT COMMAND LIST                        */
public enum _HPCCOMMAND
{
    // CH-1 Group Register
    HpcCh1CounterRead                   = 0x10,                // CH1 COUNTER READ, 32BIT
    HpcCh1CounterWrite                  = 0x90,                // CH1 COUNTER WRITE, 32BIT
    HpcCh1CounterModeRead               = 0x11,                // CH1 COUNTER MODE READ, 4BIT
    HpcCh1CounterModeWrite              = 0x91,                // CH1 COUNTER MODE WRITE, 4BIT
    HpcCh1TriggerRegionLowerDataRead    = 0x12,                // CH1 TRIGGER REGION LOWER DATA READ, 31BIT
    HpcCh1TriggerRegionLowerDataWrite   = 0x92,                // CH1 TRIGGER REGION LOWER DATA WRITE
    HpcCh1TriggerRegionUpperDataRead    = 0x13,                // CH1 TRIGGER REGION UPPER DATA READ, 31BIT
    HpcCh1TriggerRegionUpperDataWrite   = 0x93,                // CH1 TRIGGER REGION UPPER DATA WRITE
    HpcCh1TriggerPeriodRead             = 0x14,                // CH1 TRIGGER PERIOD READ, 31BIT
    HpcCh1TriggerPeriodWrite            = 0x94,                // CH1 TRIGGER PERIOD WRITE
    HpcCh1TriggerPulseWidthRead         = 0x15,                // CH1 TRIGGER PULSE WIDTH READ, 31BIT
    HpcCh1TriggerPulseWidthWrite        = 0x95,                // CH1 RIGGER PULSE WIDTH WRITE
    HpcCh1TriggerModeRead               = 0x16,                // CH1 TRIGGER MODE READ, 8BIT
    HpcCh1TriggerModeWrite              = 0x96,                // CH1 RIGGER MODE WRITE
    HpcCh1TriggerStatusRead             = 0x17,                // CH1 TRIGGER STATUS READ, 8BIT
    HpcCh1NoOperation_97                = 0x97,                // Reserved.
    HpcCh1NoOperation_18                = 0x17,                // Reserved.
    HpcCh1TriggerEnable                 = 0x98,                // CH1 TRIGGER ENABLE.
    HpcCh1NoOperation_19                = 0x19,                // Reserved.
    HpcCh1TriggerDisable                = 0x99,                // CH1 TRIGGER DISABLE.
    HpcCh1TimeTriggerFrequencyRead      = 0x1A,                // CH1 TRIGGER FREQUNCE INFO. WRITE, 28BIT
    HpcCh1TimeTriggerFrequencyWrite     = 0x9A,                // CH1 TRIGGER FREQUNCE INFO. READ
    HpcCh1Comparator1ValueRead          = 0x1B,                // CH1 COMPAREATOR1 READ, 31BIT
    HpcCh1Comparator1ValueWrite         = 0x9B,                // CH1 COMPAREATOR1 WRITE, 31BIT
    HpcCh1Comparator2ValueRead          = 0x1C,                // CH1 COMPAREATOR2 READ, 31BIT
    HpcCh1Comparator2ValueWrite         = 0x9C,                // CH1 COMPAREATOR2 WRITE, 31BIT
    HpcCh1CompareatorConditionRead      = 0x1D,                // CH1 COMPAREATOR CONDITION READ, 4BIT
    HpcCh1CompareatorConditionWrite     = 0x9D,                // CH1 COMPAREATOR CONDITION WRITE, 4BIT
    HpcCh1AbsTriggerTopPositionRead     = 0x1E,                // CH1 ABS TRIGGER POSITION READ, 31BIT
    HpcCh1AbsTriggerPositionWrite       = 0x9E,                // CH1 ABS TRIGGER POSITION WRITE, 31BIT
    HpcCh1AbsTriggerFifoStatusRead      = 0x1F,                // CH1 ABS TRIGGER POSITION FIFO STATUS READ, 16BIT
    HpcCh1AbsTriggerPositionClear       = 0x9F,                // CH1 ABS TRIGGER POSITION FIFO CLEAR

    // CH-2 Group Register
    HpcCh2CounterRead                   = 0x20,                // CH2 COUNTER READ, 32BIT
    HpcCh2CounterWrite                  = 0xA0,                // CH2 COUNTER WRITE, 32BIT
    HpcCh2CounterModeRead               = 0x21,                // CH2 COUNTER MODE READ, 4BIT
    HpcCh2CounterModeWrite              = 0xA1,                // CH2 COUNTER MODE WRITE, 4BIT
    HpcCh2TriggerRegionLowerDataRead    = 0x22,                // CH2 TRIGGER REGION LOWER DATA READ, 31BIT
    HpcCh2TriggerRegionLowerDataWrite   = 0xA2,                // CH2 TRIGGER REGION LOWER DATA WRITE
    HpcCh2TriggerRegionUpperDataRead    = 0x23,                // CH2 TRIGGER REGION UPPER DATA READ, 31BIT
    HpcCh2TriggerRegionUpperDataWrite   = 0xA3,                // CH2 TRIGGER REGION UPPER DATA WRITE
    HpcCh2TriggerPeriodRead             = 0x24,                // CH2 TRIGGER PERIOD READ, 31BIT
    HpcCh2TriggerPeriodWrite            = 0xA4,                // CH2 TRIGGER PERIOD WRITE
    HpcCh2TriggerPulseWidthRead         = 0x25,                // CH2 TRIGGER PULSE WIDTH READ, 31BIT
    HpcCh2TriggerPulseWidthWrite        = 0xA5,                // CH2 RIGGER PULSE WIDTH WRITE
    HpcCh2TriggerModeRead               = 0x26,                // CH2 TRIGGER MODE READ, 8BIT
    HpcCh2TriggerModeWrite              = 0xA6,                // CH2 RIGGER MODE WRITE
    HpcCh2TriggerStatusRead             = 0x27,                // CH2 TRIGGER STATUS READ, 8BIT
    HpcCh2NoOperation_97                = 0xA7,                // Reserved.
    HpcCh2NoOperation_18                = 0x27,                // Reserved.
    HpcCh2TriggerEnable                 = 0xA8,                // CH2 TRIGGER ENABLE.
    HpcCh2NoOperation_19                = 0x29,                // Reserved.
    HpcCh2TriggerDisable                = 0xA9,                // CH2 TRIGGER DISABLE.
    HpcCh2TimeTriggerFrequencyRead      = 0x2A,                // CH2 TRIGGER FREQUNCE INFO. WRITE, 28BIT
    HpcCh2TimeTriggerFrequencyWrite     = 0xAA,                // CH2 TRIGGER FREQUNCE INFO. READ
    HpcCh2Comparator1ValueRead          = 0x2B,                // CH2 COMPAREATOR1 READ, 31BIT
    HpcCh2Comparator1ValueWrite         = 0xAB,                // CH2 COMPAREATOR1 WRITE, 31BIT
    HpcCh2Comparator2ValueRead          = 0x2C,                // CH2 COMPAREATOR2 READ, 31BIT
    HpcCh2Comparator2ValueWrite         = 0xAC,                // CH2 COMPAREATOR2 WRITE, 31BIT
    HpcCh2CompareatorConditionRead      = 0x2D,                // CH2 COMPAREATOR CONDITION READ, 4BIT
    HpcCh2CompareatorConditionWrite     = 0xAD,                // CH2 COMPAREATOR CONDITION WRITE, 4BIT
    HpcCh2AbsTriggerTopPositionRead     = 0x2E,                // CH2 ABS TRIGGER POSITION READ, 31BIT
    HpcCh2AbsTriggerPositionWrite       = 0xAE,                // CH2 ABS TRIGGER POSITION WRITE, 31BIT
    HpcCh2AbsTriggerFifoStatusRead      = 0x2F,                // CH2 ABS TRIGGER POSITION FIFO STATUS READ, 16BIT
    HpcCh2AbsTriggerPositionClear       = 0xAF,                // CH2 ABS TRIGGER POSITION FIFO CLEAR

    // CH-3 Group Register
    HpcCh3CounterRead                   = 0x30,                // CH3 COUNTER READ, 32BIT
    HpcCh3CounterWrite                  = 0xB0,                // CH3 COUNTER WRITE, 32BIT
    HpcCh3CounterModeRead               = 0x31,                // CH3 COUNTER MODE READ, 4BIT
    HpcCh3CounterModeWrite              = 0xB1,                // CH3 COUNTER MODE WRITE, 4BIT
    HpcCh3TriggerRegionLowerDataRead    = 0x32,                // CH3 TRIGGER REGION LOWER DATA READ, 31BIT
    HpcCh3TriggerRegionLowerDataWrite   = 0xB2,                // CH3 TRIGGER REGION LOWER DATA WRITE
    HpcCh3TriggerRegionUpperDataRead    = 0x33,                // CH3 TRIGGER REGION UPPER DATA READ, 31BIT
    HpcCh3TriggerRegionUpperDataWrite   = 0xB3,                // CH3 TRIGGER REGION UPPER DATA WRITE
    HpcCh3TriggerPeriodRead             = 0x34,                // CH3 TRIGGER PERIOD READ, 31BIT
    HpcCh3TriggerPeriodWrite            = 0xB4,                // CH3 TRIGGER PERIOD WRITE
    HpcCh3TriggerPulseWidthRead         = 0x35,                // CH3 TRIGGER PULSE WIDTH READ, 31BIT
    HpcCh3TriggerPulseWidthWrite        = 0xB5,                // CH3 RIGGER PULSE WIDTH WRITE
    HpcCh3TriggerModeRead               = 0x36,                // CH3 TRIGGER MODE READ, 8BIT
    HpcCh3TriggerModeWrite              = 0xB6,                // CH3 RIGGER MODE WRITE
    HpcCh3TriggerStatusRead             = 0x37,                // CH3 TRIGGER STATUS READ, 8BIT
    HpcCh3NoOperation_97                = 0xB7,                // Reserved.
    HpcCh3NoOperation_18                = 0x37,                // Reserved.
    HpcCh3TriggerEnable                 = 0xB8,                // CH3 TRIGGER ENABLE.
    HpcCh3NoOperation_19                = 0x39,                // Reserved.
    HpcCh3TriggerDisable                = 0xB9,                // CH3 TRIGGER DISABLE.
    HpcCh3TimeTriggerFrequencyRead      = 0x3A,                // CH3 TRIGGER FREQUNCE INFO. WRITE, 28BIT
    HpcCh3TimeTriggerFrequencyWrite     = 0xBA,                // CH3 TRIGGER FREQUNCE INFO. READ
    HpcCh3Comparator1ValueRead          = 0x3B,                // CH3 COMPAREATOR1 READ, 31BIT
    HpcCh3Comparator1ValueWrite         = 0xBB,                // CH3 COMPAREATOR1 WRITE, 31BIT
    HpcCh3Comparator2ValueRead          = 0x3C,                // CH3 COMPAREATOR2 READ, 31BIT
    HpcCh3Comparator2ValueWrite         = 0xBC,                // CH3 COMPAREATOR2 WRITE, 31BIT
    HpcCh3CompareatorConditionRead      = 0x3D,                // CH3 COMPAREATOR CONDITION READ, 4BIT
    HpcCh3CompareatorConditionWrite     = 0xBD,                // CH3 COMPAREATOR CONDITION WRITE, 4BIT
    HpcCh3AbsTriggerTopPositionRead     = 0x3E,                // CH3 ABS TRIGGER POSITION READ, 31BIT
    HpcCh3AbsTriggerPositionWrite       = 0xBE,                // CH3 ABS TRIGGER POSITION WRITE, 31BIT
    HpcCh3AbsTriggerFifoStatusRead      = 0x3F,                // CH3 ABS TRIGGER POSITION FIFO STATUS READ, 16BIT
    HpcCh3AbsTriggerPositionClear       = 0xBF,                // CH3 ABS TRIGGER POSITION FIFO CLEAR

    // CH-4 Group Register
    HpcCh4CounterRead                   = 0x40,                // CH4 COUNTER READ, 32BIT
    HpcCh4CounterWrite                  = 0xC0,                // CH4 COUNTER WRITE, 32BIT
    HpcCh4CounterModeRead               = 0x41,                // CH4 COUNTER MODE READ, 4BIT
    HpcCh4CounterModeWrite              = 0xC1,                // CH4 COUNTER MODE WRITE, 4BIT
    HpcCh4TriggerRegionLowerDataRead    = 0x42,                // CH4 TRIGGER REGION LOWER DATA READ, 31BIT
    HpcCh4TriggerRegionLowerDataWrite   = 0xC2,                // CH4 TRIGGER REGION LOWER DATA WRITE
    HpcCh4TriggerRegionUpperDataRead    = 0x43,                // CH4 TRIGGER REGION UPPER DATA READ, 31BIT
    HpcCh4TriggerRegionUpperDataWrite   = 0xC3,                // CH4 TRIGGER REGION UPPER DATA WRITE
    HpcCh4TriggerPeriodRead             = 0x44,                // CH4 TRIGGER PERIOD READ, 31BIT
    HpcCh4TriggerPeriodWrite            = 0xC4,                // CH4 TRIGGER PERIOD WRITE
    HpcCh4TriggerPulseWidthRead         = 0x45,                // CH4 TRIGGER PULSE WIDTH READ, 31BIT
    HpcCh4TriggerPulseWidthWrite        = 0xC5,                // CH4 RIGGER PULSE WIDTH WRITE
    HpcCh4TriggerModeRead               = 0x46,                // CH4 TRIGGER MODE READ, 8BIT
    HpcCh4TriggerModeWrite              = 0xC6,                // CH4 RIGGER MODE WRITE
    HpcCh4TriggerStatusRead             = 0x47,                // CH4 TRIGGER STATUS READ, 8BIT
    HpcCh4NoOperation_97                = 0xC7,                // Reserved.
    HpcCh4NoOperation_18                = 0x47,                // Reserved.
    HpcCh4TriggerEnable                 = 0xC8,                // CH4 TRIGGER ENABLE.
    HpcCh4NoOperation_19                = 0x49,                // Reserved.
    HpcCh4TriggerDisable                = 0xC9,                // CH4 TRIGGER DISABLE.
    HpcCh4TimeTriggerFrequencyRead      = 0x4A,                // CH4 TRIGGER FREQUNCE INFO. WRITE, 28BIT
    HpcCh4TimeTriggerFrequencyWrite     = 0xCA,                // CH4 TRIGGER FREQUNCE INFO. READ
    HpcCh4Comparator1ValueRead          = 0x4B,                // CH4 COMPAREATOR1 READ, 31BIT
    HpcCh4Comparator1ValueWrite         = 0xCB,                // CH4 COMPAREATOR1 WRITE, 31BIT
    HpcCh4Comparator2ValueRead          = 0x4C,                // CH4 COMPAREATOR2 READ, 31BIT
    HpcCh4Comparator2ValueWrite         = 0xCC,                // CH4 COMPAREATOR2 WRITE, 31BIT
    HpcCh4CompareatorConditionRead      = 0x4D,                // CH4 COMPAREATOR CONDITION READ, 4BIT
    HpcCh4CompareatorConditionWrite     = 0xCD,                // CH4 COMPAREATOR CONDITION WRITE, 4BIT
    HpcCh4AbsTriggerTopPositionRead     = 0x4E,                // CH4 ABS TRIGGER POSITION READ, 31BIT
    HpcCh4AbsTriggerPositionWrite       = 0xCE,                // CH4 ABS TRIGGER POSITION WRITE, 31BIT
    HpcCh4AbsTriggerFifoStatusRead      = 0x4F,                // CH4 ABS TRIGGER POSITION FIFO STATUS READ, 16BIT
    HpcCh4AbsTriggerPositionClear       = 0xCF,                // CH4 ABS TRIGGER POSITION FIFO CLEAR

    // Ram Access Group Register
    HpcRamDataWithRamAddress            = 0x51,                // READ RAM DATA WITH RAM ADDR PORT ADDRESS
    HpcRamDataWrite                     = 0xD0,                // RAM DATA WRITE
    HpcRamDataRead                      = 0x50,                // RAM DATA READ, 32BIT

    // Debugging Registers    
    HpcCh1TrigPosIndexRead              = 0x60,                // CH1 Current RAM trigger index position on 32Bit data, 8BIT
    HpcCh1TrigBackwardDataRead          = 0x61,                // CH1 Current RAM trigger backward position data, 32BIT
    HpcCh1TrigCurrentDataRead           = 0x62,                // CH1 Current RAM trigger current position data, 32BIT
    HpcCh1TrigForwardDataRead           = 0x63,                // CH1 Current RAM trigger next position data, 32BIT
    HpcCh1TrigRamAddressRead            = 0x64,                // CH1 Current RAM trigger address, 20BIT

    HpcCh2TrigPosIndexRead              = 0x65,                // CH2 Current RAM trigger index position on 32Bit data, 8BIT
    HpcCh2TrigBackwardDataRead          = 0x66,                // CH2 Current RAM trigger backward position data, 32BIT
    HpcCh2TrigCurrentDataRead           = 0x67,                // CH2 Current RAM trigger current position data, 32BIT
    HpcCh2TrigForwardDataRead           = 0x68,                // CH2 Current RAM trigger next position data, 32BIT
    HpcCh2TrigRamAddressRead            = 0x69,                // CH2 Current RAM trigger address, 20BIT

    HpcCh3TrigPosIndexRead              = 0x70,                // CH3 Current RAM trigger index position on 32Bit data, 8BIT
    HpcCh3TrigBackwardDataRead          = 0x71,                // CH3 Current RAM trigger backward position data, 32BIT
    HpcCh3TrigCurrentDataRead           = 0x72,                // CH3 Current RAM trigger current position data, 32BIT
    HpcCh3TrigForwardDataRead           = 0x73,                // CH3 Current RAM trigger next position data, 32BIT
    HpcCh3TrigRamAddressRead            = 0x74,                // CH3 Current RAM trigger address, 20BIT

    HpcCh4TrigPosIndexRead              = 0x75,                // CH4 Current RAM trigger index position on 32Bit data, 8BIT
    HpcCh4TrigBackwardDataRead          = 0x76,                // CH4 Current RAM trigger backward position data, 32BIT
    HpcCh4TrigCurrentDataRead           = 0x77,                // CH4 Current RAM trigger current position data, 32BIT
    HpcCh4TrigForwardDataRead           = 0x78,                // CH4 Current RAM trigger next position data, 32BIT
    HpcCh4TrigRamAddressRead            = 0x79,                // CH4 Current RAM trigger address, 20BIT

    HpcCh1TestEnable                    = 0x81,                // CH1 test enable(Manufacturer only)
    HpcCh2TestEnable                    = 0x82,                // CH2 test enable(Manufacturer only)
    HpcCh3TestEnable                    = 0x83,                // CH3 test enable(Manufacturer only)
    HpcCh4TestEnable                    = 0x84,                // CH4 test enable(Manufacturer only)

    HpcTestFrequency                    = 0x8C,                // Test counter output frequency(32bit)
    HpcTestCountStart                   = 0x8D,                // Start test counter output with position(32bit signed).
    HpcTestCountEnd                     = 0x8E,                // End counter output.

    HpcCh1TrigVectorTopDataOfFifo       = 0x54,                // CH1 UnitVector X positin of FIFO top.
    HpcCh1TrigVectorFifoStatus          = 0x55,                // CH1 UnitVector X FIFO Status.
    HpcCh2TrigVectorTopDataOfFifo       = 0x56,                // CH2 UnitVector Y positin of FIFO top.
    HpcCh2TrigVectorFifoStatus          = 0x57,                // CH2 UnitVector Y FIFO Status.
    HpcCh1TrigVectorFifoPush            = 0xD2,                // CH1 UnitVector X position, fifo data push.
    HpcCh2TrigVectorFifoPush            = 0xD3                 // CH2 UnitVector Y position, fifo data push.
}

public enum AXT_MAX_MACRO:uint
{
    AXP_MAX_MACRO_SIZE      = 8,
    AXP_MAX_MACRO_NODE_NUM  = 64,
	AXP_MAX_MACRO_SET_ARG   = 16,
	AXP_MAX_MACRO_GET_DATA  = 16,
	AXP_MAX_MACRO_DATA_BYTE = 12
}

public enum AXT_MACRO_FUNCTION:uint
{
    MACRO_FUNC_CALL          = 0,
    MACRO_FUNC_JUMP          = 1,
    MACRO_FUNC_RETURN        = 2,
    MACRO_FUNC_REPEAT        = 3,
    MACRO_FUNC_SET_OUTPUT    = 4,
    MACRO_FUNC_WAIT          = 5,
    MACRO_FUNC_STOP          = 6,
    MACRO_FUNC_MAX
}

public enum AXT_MACRO_JUMP_TYPE:uint
{
    MACRO_JUMP_MACRO    = 0,
    MACRO_JUMP_NODE     = 1
}

public enum AXT_MACRO_SET_OUTPUT_TYPE:uint
{
    MACRO_DIGITAL_OUTPUT    = 0,
    MACRO_ANALOG_OUTPUT     = 1,   
    MACRO_MOTION_OUTPUT     = 2
}

public enum AXT_MACRO_SET_DATA_TYPE:uint
{
    MACRO_DATA_BIT          = 0,
    MACRO_DATA_BYTE         = 1,   
    MACRO_DATA_WORD         = 2,
    MACRO_DATA_DWORD        = 3,
    MACRO_DATA_BYTE12       = 4,
    MACRO_DATA_VOLTAGE      = 5,
    MACRO_DATA_DIGIT        = 6
}

public enum AXT_MACRO_STOP_MODE:uint
{
    MACRO_QUICK_STOP        = 0,
    MACRO_SLOW_STOP         = 1
}


public enum AXT_MACRO_START_CONDITION:uint
{
    MACRO_START_READY       = 0,   
    MACRO_START_IMMEDIATE   = 1,
}

public enum AXT_MACRO_RUN_STATUS:uint
{
    MACRO_STATUS_STOP   = 0,   
    MACRO_STATUS_READY  = 1,
    MACRO_STATUS_RUN    = 2,
    MACRO_STATUS_ERROR  = 3
}

public enum AXT_MACRO_ERROR_CODE:uint
{
    ERROR_INVALID_MACRO_NO          = 1,
    ERROR_INVALID_NODE_NO           = 2,
    ERROR_INVALID_MODULE_NO         = 3,
    ERROR_INVALID_AXIS_NO           = 4,
    ERROR_INVALID_CHANNEL_NO        = 5,
    ERROR_INVALID_JUMP_TYPE         = 6,
    ERROR_INVALID_OUTPUT_TYPE       = 7,
    ERROR_INVALID_OFFSET            = 8,
    ERROR_INVALID_STOP_MODE         = 9,
    ERROR_INVALID_START_CONDITION   = 10,
    ERROR_NOT_SUPPORT_MODULE        = 11,
    ERROR_NOT_READY_MACRO           = 12
}
