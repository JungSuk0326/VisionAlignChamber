/****************************************************************************
*****************************************************************************
**
** File Name
** ----------
**
** AXA.CS
**
** COPYRIGHT (c) AJINEXTEK Co., LTD
**
*****************************************************************************
*****************************************************************************
**
** Description
** -----------
** Ajinextek Analog Library Header File
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

public class CAXA
{
//========== Board and verification API group of module information =================================================================================
//Verify if AIO module exists
[DllImport("AXL.dll")] public static extern uint AxaInfoIsAIOModule(ref uint upStatus);


//Verify AIO module number
[DllImport("AXL.dll")] public static extern uint AxaInfoGetModuleNo(int lBoardNo, int lModulePos, ref int lpModuleNo);


//Verify the number of AIO module
[DllImport("AXL.dll")] public static extern uint AxaInfoGetModuleCount(ref int lpModuleCount);


//Verify the number of input channels of specified module
[DllImport("AXL.dll")] public static extern uint AxaInfoGetInputCount(int lModuleNo, ref int lpCount);


//Verify the number of output channels of specified module
[DllImport("AXL.dll")] public static extern uint AxaInfoGetOutputCount(int lModuleNo, ref int lpCount);


//Verify the first channel number of specified module
[DllImport("AXL.dll")] public static extern uint AxaInfoGetChannelNoOfModuleNo(int lModuleNo, ref int lpChannelNo);


// Verify the first Input channel number of specified module (Inputmodule, Integration for input/output Module)
[DllImport("AXL.dll")] public static extern uint AxaInfoGetChannelNoAdcOfModuleNo(int lModuleNo, ref int lpChannelNo);


// Verify the first output channel number of specified module (Inputmodule, Integration for input/output Module)
[DllImport("AXL.dll")] public static extern uint AxaInfoGetChannelNoDacOfModuleNo(int lModuleNo, ref int lpChannelNo);


//Verify base board number, module position and module ID with specified module number
[DllImport("AXL.dll")] public static extern uint AxaInfoGetModule(int lModuleNo, ref int lpBoardNo, ref int lpModulePos, ref uint upModuleID);

// Verify specified module board : Sub ID, module name, module description
//===============================================/
// support product : EtherCAT
// upModuleSubID   : EtherCAT SubID(for distinguish between EtherCAT modules)
// szModuleName         : model name of module(50 Bytes)
// szModuleDescription  : description of module(80 Bytes)
//======================================================//

//Verify Module status of specified module board
[DllImport("AXL.dll")] public static extern uint AxaInfoGetModuleEx(int lModuleNo, ref uint upModuleSubID, System.Text.StringBuilder szModuleName, System.Text.StringBuilder szModuleDescription);


[DllImport("AXL.dll")] public static extern uint AxaInfoGetModuleStatus(int lModuleNo);


//========== API group of input module information search ====================================================================================
//Verify module number with specified input channel number
[DllImport("AXL.dll")] public static extern uint AxaiInfoGetModuleNoOfChannelNo(int lChannelNo, ref int lpModuleNo);


//Verify the number of entire channels of analog input module
[DllImport("AXL.dll")] public static extern uint AxaiInfoGetChannelCount(ref int lpChannelCount);


//========== API group for setting and verifying of input module interrupt ============================================================
//Use window message, callback API or event method in order to get event message into specified channel. Use for the time of collection action( refer AxaStartMultiChannelAdc ) of continuous data by H/W timer
//(Timer Trigger Mode, External Trigger Mode)
//Use Windows message and callback function or event method to receive an interrupt message to the specified module.
//========= Interrupt-related function ======================================================================================
//The callback function method has the advantage that the event can be notified very quickly because the callback function is called immediately at the time of the occurrence of the event.
//But, The main process is stalled until the callback function is completely terminated.
//In other words, Care must be taken when there is work load in the callback function.
//The event method is a method of continuously detecting and processing the occurrence of an interrupt by using a thread.
//The event method has the disadvantage that system resources are occupied by thread, but it has the advantage that interrupts can be detected and processed the fastest.
//This method is not commonly used. but it is used when quick interrupt handling is main concern.
//The event method uses a specific thread to monitor the occurrence of an event, and it works independently of the main process.
//So, This is the recommended method because it enables the most efficient use of resources in a multiprocessor system.
//Use a Window message or callback function to receive interrupt message.
//(Message handle, Message ID, Callback function, Interrupt event)
//    hWnd        : Used to receive window handles and window messages. If not, enter NULL.
//    uMessage    : Massage of window handle. To not use this or use the default value, enter 0.
//    proc        : Pointer of the function to be called when an interrupt occurs. To not use this, enter NULL.
//    pEvent      : Event handling when using event method.
[DllImport("AXL.dll")] public static extern uint AxaiEventSetChannel(int lModuleNo, IntPtr hWnd, uint uMessage, CAXHS.AXT_INTERRUPT_PROC pProc, ref uint pEvent);


//Set whether to use event in specified input channel
//======================================================//
// uUse       : DISABLE(0)    // Event Disable
//            : ENABLE(1)     // Event Enable
//======================================================//
[DllImport("AXL.dll")] public static extern uint AxaiEventSetChannelEnable(int lChannelNo, uint uUse);


//Verify whether to use event in specified input channel
//======================================================//
// *upUse     : DISABLE(0)    // Event Disable
//            : ENABLE(1)     // Event Enable
//======================================================//
[DllImport("AXL.dll")] public static extern uint AxaiEventGetChannelEnable(int lChannelNo, ref uint upUse);


//Set whether to use event in specified multiple input channels
//======================================================//
//lSize       : Number of input channel
// uUse       : DISABLE(0)    // Event Disable
//            : ENABLE(1)     // Event Enable
//======================================================//
[DllImport("AXL.dll")] public static extern uint AxaiEventSetMultiChannelEnable(int lSize, int[] lpChannelNo, uint uUse);


//Set kind of event in specified input channel
//======================================================//
// uMask      : DATA_EMPTY(1)
//            : DATA_MANY(2)
//            : DATA_SMALL(3)
//            : DATA_FULL(4)
//======================================================//
[DllImport("AXL.dll")] public static extern uint AxaiEventSetChannelMask(int lChannelNo, uint uMask);


//Verify kind of event in specified input channel
//======================================================//
// *upMask    : DATA_EMPTY(1)
//            : DATA_MANY(2)
//            : DATA_SMALL(3)
//            : DATA_FULL(4)
//======================================================//
[DllImport("AXL.dll")] public static extern uint AxaiEventGetChannelMask(int lChannelNo, ref uint upMask);


//Set kind of event in specified multiple input channels
//==============================================================================//
// uMask      : DATA_EMPTY(1)
//            : DATA_MANY(2)
//            : DATA_SMALL(3)
//            : DATA_FULL(4)
//==============================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiEventSetMultiChannelMask(int lSize, int[] lpChannelNo, uint uMask);


//Verify event occurrence position
//==============================================================================//
// *upMode    : AIO_EVENT_DATA_UPPER(1)
//            : AIO_EVENT_DATA_LOWER(2)
//            : AIO_EVENT_DATA_FULL(3)
//            : AIO_EVENT_DATA_EMPTY(4)
//==============================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiEventRead(ref int lpChannelNo, ref uint upMode);


//Set interrupt mask of specified module. (SIO-AI4RB is not supportive.)
//==================================================================================================//
// uMask    : SCAN_END(1)
//          : FIFO_HALF_FULL(2)
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiInterruptSetModuleMask(int lModuleNo, uint uMask);


//Verify interrupt mask of specified module
//==================================================================================================//
// *upMask  : SCAN_END(1)
//          : FIFO_HALF_FULL(2)
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiInterruptGetModuleMask(int lModuleNo, ref uint upMask);


//========== API group for setting and verifying of input module parameter ========================================================================
//Set the input voltage range in specified input channel
//==================================================================================================//
// dMinVolt : -10V Fix
// dMaxVolt : 10V Fix
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiSetRange(int lChannelNo, double dMinVolt, double dMaxVolt);


//Verify the input voltage range in specified input channel
//==================================================================================================//
// *dpMaxVolt : -10V Fix
// *dpMaxVolt : 10V Fix
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiGetRange(int lChannelNo, ref double dpMinVolt, ref double dpMaxVolt);


//Set the allowed input voltage range in specified multiple input Modules
//==================================================================================================//
// lModuleNo   : Analog Module Number
//
// RTEX AI16F
// Mode -5~+5  : dMinVolt = -5, dMaxVolt = +5
// Mode -10~+10: dMinVolt = -10, dMaxVolt = +10
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiSetRangeModule(int lModuleNo, double dMinVolt, double dMaxVolt);


//Verify the input voltage range in specified input Module
//==================================================================================================//
// lModuleNo   : Analog Module Number
//
// RTEX AI16F
// *dMinVolt   : -5V, -10V
// *dMaxVolt   : +5V, +10V
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiGetRangeModule(int lModuleNo, ref double dMinVolt, ref double dMaxVolt);



//Set the allowed input voltage range in specified multiple input channels
//==================================================================================================//
// AXT_SIO_RAI8RB
// dMinVolt   : -10V Fix
// dMaxVolt   : 10V Fix
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiSetMultiRange(int lSize, int[] lpChannelNo, double dMinVolt, double dMaxVolt);


//Set trigger mode in the specified input module
//==================================================================================================//
// uTriggerMode : NORMAL_MODE(1)
//              : TIMER_MODE(2)
//              : EXTERNAL_MODE(3)
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiSetTriggerMode(int lModuleNo, uint uTriggerMode);


//Verify trigger mode in the specified input module
//==================================================================================================//
// *upTriggerMode : NORMAL_MODE(1)
//                : TIMER_MODE(2)
//                : EXTERNAL_MODE(3)
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiGetTriggerMode(int lModuleNo, ref uint upTriggerMode);


//Set offset of specified input module by mVolt (mV) unit. Max -100~100mVolt
//==================================================================================================//
// dMiliVolt    : -100 ~ 100
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiSetModuleOffsetValue(int lModuleNo, double dMiliVolt);


//Verify offset value of specified input module. mVolt unit(mV)
//==================================================================================================//
// *dpMiliVolt    : -100 ~ 100
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiGetModuleOffsetValue(int lModuleNo, ref double dpMiliVolt);


//========== Software Timer (Normal Trigger Mode) group ===================================================================================
//Software Trigger Mode API, Convert analog input value to A/D in the specified input channel by user , then return it in voltage value
[DllImport("AXL.dll")] public static extern uint AxaiSwReadVoltage(int lChannelNo, ref double dpVolt);


//Software Trigger Mode API, Return analog input value in digit value to specified input channel
[DllImport("AXL.dll")] public static extern uint AxaiSwReadDigit(int lChannelNo, ref uint upDigit);


//Software Trigger Mode API, Return analog input value in voltage value to specified multiple input channels
[DllImport("AXL.dll")] public static extern uint AxaiSwReadMultiVoltage(int lSize, int[] lpChannelNo, double[] dpVolt);


//Software Trigger Mode API, Return analog input value in digit value to specified multiple input channels
[DllImport("AXL.dll")] public static extern uint AxaiSwReadMultiDigit(int lSize, int[] lpChannelNo, uint[] upDigit);


//========== Hardware Timer (Timer Trigger Mode + External Trigger Mode) group ===================================================================================
//Hardware Trigger Mode API, Set setting value in order to use immediate mode in specified multiple channels
[DllImport("AXL.dll")] public static extern uint AxaiHwSetMultiAccess(int lSize, int[] lpChannelNo, int[] lpWordSize);


//Hardware Trigger Mode API, Convert A/D as much as number of specified, then return the voltage value
[DllImport("AXL.dll")] public static extern uint AxaiHwStartMultiAccess(double[,] dpBuffer);


//Set sampling interval to specified module by frequency unit(Hz)
//==================================================================================================//
// dSampleFreq    : 10 ~ 100000
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiHwSetSampleFreq(int lModuleNo, double dSampleFreq);


//Verify the setting value of sampling interval to specified module by frequency unit(Hz)
//==================================================================================================//
// *dpSampleFreq  : 10 ~ 100000
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiHwGetSampleFreq(int lModuleNo, ref double dpSampleFreq);


//Set sampling interval to specified module by time unit (uSec)
//==================================================================================================//
// dSamplePeriod  : 100000 ~ 1000000000
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiHwSetSamplePeriod(int lModuleNo, double dSamplePeriod);


//Verify setting value of sampling interval to specified module by time unit(uSec)
//==================================================================================================//
// *dpSamplePeriod: 100000 ~ 1000000000
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiHwGetSamplePeriod(int lModuleNo, ref double dpSamplePeriod);


//Set control method when the buffer is full in specified input channel
//==================================================================================================//
// uFullMode    : NEW_DATA_KEEP(0)
//              : CURR_DATA_KEEP(1)
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiHwSetBufferOverflowMode(int lChannelNo, uint uFullMode);


//Verify control method when the buffer is full in specified input channel
//==================================================================================================//
// *upFullMode  : NEW_DATA_KEEP(0)
//              : CURR_DATA_KEEP(1)
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiHwGetBufferOverflowMode(int lChannelNo, ref uint upFullMode);


//control method when the buffer is full in specified multiple input channels
//==================================================================================================//
// uFullMode    : NEW_DATA_KEEP(0)
//              : CURR_DATA_KEEP(1)
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiHwSetMultiBufferOverflowMode(int lSize, int[] lpChannelNo, uint uFullMode);


//Set the upper limit and lower limit of buffer in specified input channel
[DllImport("AXL.dll")] public static extern uint AxaiHwSetLimit(int lChannelNo, int lLowLimit, int lUpLimit);


//Verify the upper limit and lower limit of buffer in specified input channel
[DllImport("AXL.dll")] public static extern uint AxaiHwGetLimit(int lChannelNo, ref int lpLowLimit, ref int lpUpLimit);


//Set the upper limit and lower limit of buffer in multiple input channels
[DllImport("AXL.dll")] public static extern uint AxaiHwSetMultiLimit(int lSize, int[] lpChannelNo, int lLowLimit, int lUpLimit);


//Start A/D conversion using H/W timer in specified multiple channels
[DllImport("AXL.dll")] public static extern uint AxaiHwStartMultiChannel(int lSize, int[] lpChannelNo, int lBuffSize);


//Start A/D conversion using H/W timer in specified single channels
[DllImport("AXL.dll")] public static extern uint AxaiHwStartSingleChannelAdc(int lChannelNo, int lBuffSize);


//Stop continuous signal A/D conversion used H/W timer.
[DllImport("AXL.dll")] public static extern uint AxaiHwStopSingleChannelAdc(int lChannelNo);


//After starting of A/D conversion in specified multiple channels, manage filtering as much as specified and return into voltage
//==================================================================================================//
// lSize           : Number of input channels to use
// *lpChannelNo    : An array of channel numbers to use
// lFilterCount    : Number of data for filtering
// lBuffSize       : Number of buffers which are assigned to each channel
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiHwStartMultiFilter(int lSize, int[] lpChannelNo, int lFilterCount, int lBuffSize);


//Stop continuous signal A/D conversion used H/W timer
[DllImport("AXL.dll")] public static extern uint AxaiHwStopMultiChannel(int lModuleNo);


//Inspect the numbers of data in memory buffer of specified input channel
[DllImport("AXL.dll")] public static extern uint AxaiHwReadDataLength(int lChannelNo, ref int lpDataLength);


//Read A/D conversion data used H/W timer in specified input channel by voltage value
[DllImport("AXL.dll")] public static extern uint AxaiHwReadSampleVoltage(int lChannelNo, ref int lpSize, ref double dpVolt);


//Read A/D conversion data used H/W timer in specified input channel by digit value
[DllImport("AXL.dll")] public static extern uint AxaiHwReadSampleDigit(int lChannelNo, ref int lpSize, ref uint upDigit);


//========== API group of input module state check ===================================================================================
//Inspect if there is no data in memory buffer of specified input channel
//==================================================================================================//
// *upEmpty   : FALSE(0)
//            : TRUE(1)
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiHwIsBufferEmpty(int lChannelNo, ref uint upEmpty);


//Inspect if the data is more than the upper limit specified in memory buffer of specified input channel
//==================================================================================================//
// *upUpper   : FALSE(0)
//            : TRUE(1)
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiHwIsBufferUpper(int lChannelNo, ref uint upUpper);


//Inspect if the data is less than the upper limit specified in memory buffer of specified input channel
//==================================================================================================//
// *upLower   : FALSE(0)
//            : TRUE(1)
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiHwIsBufferLower(int lChannelNo, ref uint upLower);


//==External Trigger Mode Function
//Start external trigger mode of the selected channels of specified input module.
//==================================================================================================//
// lSize           : Number of channels to use external trigger on specified input module
// *lpChannelPos   : Index of channels to use external trigger on specified input module
[DllImport("AXL.dll")] public static extern uint AxaiExternalStartADC(int lModuleNo, int lSize, ref int lpChannelPos);
//Stop external trigger mode of specified input module.
[DllImport("AXL.dll")] public static extern uint AxaiExternalStopADC(int lModuleNo);
//Return FIFO status of specified input module
//==================================================================================================//
// *dwpStatus      : FIFO_DATA_EXIST(0)
//                 : FIFO_DATA_EMPTY(1)
//                 : FIFO_DATA_HALF(2)
//                 : FIFO_DATA_FULL(6)
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaiExternalReadFifoStatus(int lModuleNo, ref uint upStatus);
//Read converted A/D value from external signal of specified input module.
// lSize           : Number of channels to read converted A/D value in specified input module
// *lpChannelPos   : Index of channels to read converted A/D value in specified input module
// lDataSize       : Number of maximum data to read converted A/D value by external trigger
// lBuffSize       : Size of externally allocated data buffer
// lStartDataPos   : Starting location of data buffer saving
// *dpVolt         : Two-demensional array pointer(for receiving value of converted to A/D)
// *lpRetDataSize  : Actually assigned number to data buffer(converted A/D value)
// *dwpStatus      : Return FIFO status when read converted A/D value from Fifo(H/W Buffer)
[DllImport("AXL.dll")] public static extern uint AxaiExternalReadVoltage(int lModuleNo, int lSize, ref int lpChannelPos, int lDataSize, int lBuffSize, int lStartDataPos, double[,] dpVolt, ref int lpRetDataSize, ref uint upStatus);

//========== API group of output module information search ========================================================================================
//Verify module number with specified output channel number
[DllImport("AXL.dll")] public static extern uint AxaoInfoGetModuleNoOfChannelNo(int lChannelNo, ref int lpModuleNo);


//Verify entire number of channel of analog output module
[DllImport("AXL.dll")] public static extern uint AxaoInfoGetChannelCount(ref int lpChannelCount);


//========== API group for output module setting and verification ========================================================================================
//Set output voltage range in specified output channel
//==================================================================================================//
// dMinVolt    : -10V Fix
// dMaxVolt    : 10V Fix
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaoSetRange(int lChannelNo, double dMinVolt, double dMaxVolt);


//Verify output voltage range in specified output channel
//==================================================================================================//
// dMinVolt    : -10V Fix
// dMaxVolt    : 10V Fix
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaoGetRange(int lChannelNo, ref double dpMinVolt, ref double dpMaxVolt);


//Set output voltage range in specified multiple output channels
//==================================================================================================//
// dMinVolt    : -10V Fix
// dMaxVolt    : 10V Fix
//==================================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaoSetMultiRange(int lSize, int[] lpChannelNo, double dMinVolt, double dMaxVolt);


//The Input voltage is output in specified output channel
[DllImport("AXL.dll")] public static extern uint AxaoWriteVoltage(int lChannelNo, double dVolt);


//The Input voltage is output in specified multiple output channel
[DllImport("AXL.dll")] public static extern uint AxaoWriteMultiVoltage(int lSize, int[] lpChannelNo, double[] dpVolt);


//Verify voltage which is output in specified output channel
[DllImport("AXL.dll")] public static extern uint AxaoReadVoltage(int lChannelNo, ref double dpVolt);


//Verify voltage which is output in specified multiple output channels
[DllImport("AXL.dll")] public static extern uint AxaoReadMultiVoltage(int lSize, int[] lpChannelNo, double[] dpVolt);

//============================ AXA User Define Pattern Generator ============================
// API function for setting 'Channel User Define Pattern Generator'
// Outputting pattern every time depending on AxaoPgSetUserInterval setting
// lLoopCnt       : '0'(Repeat input pattern infinitely), 'value' : Keep last pattern, After outputting input pattern for specified number of times
//                : (MAX : 60000)
// lPatternSize   : Number of input pattern(MAX : 8192)
[DllImport("AXL.dll")] public static extern uint AxaoPgSetUserPatternGenerator(int lChannelNo, int lLoopCnt, int lPatternSize, ref double dpPattern);

// API function for getting 'user define pattern generator'
[DllImport("AXL.dll")] public static extern uint AxaoPgGetUserPatternGenerator(int lChannelNo, ref int lpLoopCnt, ref int lpPatternSize, ref double dpPattern);

// API function for setting 'pattern generator interval' of corresponding channel
// Unit : us(Default resolution : 500uSec)
[DllImport("AXL.dll")] public static extern uint AxaoPgSetUserInterval(int lChannelNo, double dInterval);

// API function for getting 'pattern generator interval' of corresponding channel
[DllImport("AXL.dll")] public static extern uint AxaoPgGetUserInterval(int lChannelNo, ref double dpInterval);

// API function for getting 'Pattern Index / Loop Cnt' of corresponding channel
// In the case of status, following status can be included.
// lpIndexNum : The index of current user pattern
// lpLoopCnt : Number of currently running loop
// dwpInBusy : Driving status of Pattern Generator
[DllImport("AXL.dll")] public static extern uint AxaoPgGetStatus(int lChannelNo, ref int lpIndexNum, ref int lpLoopCnt, ref uint dwpInBusy);


// API function for start 'User Define Pattern Generator' of corresponding channel ('AO' output start)
// Inputting start channel number as an array
// Start pattern generator function simultaneously for input channel.
[DllImport("AXL.dll")] public static extern uint AxaoPgSetUserStart(ref int lpChannelNo, int lSize);


// API function for stopping 'User Define Pattern Generator' of corresponding channel ('AO' output stop)
// Output value is switched to 0 volt when output is stopped
[DllImport("AXL.dll")] public static extern uint AxaoPgSetUserStop(ref int lpChannelNo, int lSize);


// API function for clearing Pattern Data(Reset all areas to 0x00)
[DllImport("AXL.dll")] public static extern uint AxaPgSetUserDataReset(int lChannelNo);


// API function for setting output status by each channel when specified output module network is broken
//===============================================================================================//
// lChannelNo  : Channel number(Distributed slave products only)
// dwSetValue  : Setting value
//             : 1 --> Analog Max
//             : 2 --> Analog MIN
//             : 3 --> User Vaule(Default user value : 0V, You can change this value by 'AxaoSetNetworkErrorUserValue()')
//             : 4 --> Analog 0 V
//===============================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaoSetNetworkErrorAct(int lChannelNo, uint dwSetValue);

// API function for setting output status in bytes when specified output module network is broken
//===============================================================================================//
// lChannelNo  : Channel number(Distributed slave products only)
// dVolt       : Analog output voltage(User defined value)
//===============================================================================================//
[DllImport("AXL.dll")] public static extern uint AxaoSetNetworkErrorUserValue(int lChannelNo, uint dVolt);


}
