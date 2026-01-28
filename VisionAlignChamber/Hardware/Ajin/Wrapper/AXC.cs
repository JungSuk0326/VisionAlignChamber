/****************************************************************************
*****************************************************************************
**
** File Name
** ----------
**
** AXC.CS
**
** COPYRIGHT (c) AJINEXTEK Co., LTD
**
*****************************************************************************
*****************************************************************************
**
** Description
** -----------
** Ajinextek Counter Library Header File
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

public class CAXC
{

//========== Board and verification API group of module information
	// Verifying If CNT module exists
	[DllImport("AXL.dll")] public static extern uint AxcInfoIsCNTModule(ref uint upStatus);
	
	
	// Verifying CNT module number
	[DllImport("AXL.dll")] public static extern uint AxcInfoGetModuleNo(int lBoardNo, int lModulePos, ref int lpModuleNo);
	
	
	// Verifying the number of CNT I/O module
	[DllImport("AXL.dll")] public static extern uint AxcInfoGetModuleCount(ref int lpModuleCount);
	
	
	// Verifying the Number of counter input channels for specified module
	[DllImport("AXL.dll")] public static extern uint AxcInfoGetChannelCount(int lModuleNo, ref int lpCount);
	
	
	// Verifying the total number of channels in system mounted counter.
	[DllImport("AXL.dll")] public static extern uint AxcInfoGetTotalChannelCount(ref int lpChannelCount);
	
	
	// Verifying base board number, module position and module ID with specified module number
	[DllImport("AXL.dll")] public static extern uint AxcInfoGetModule(int lModuleNo, ref int lpBoardNo, ref int lpModulePos, ref uint upModuleID);
	
	
	// Verifying specified module board : Sub ID, module name, module description
	//======================================================/
	// support product : EtherCAT
	// upModuleSubID   : EtherCAT SubID(for distinguish between EtherCAT modules)
	// szModuleName         : model name of module(50 Bytes)
	// szModuleDescription  : description of module(80 Bytes)
	//======================================================//
	[DllImport("AXL.dll")] public static extern uint AxcInfoGetModuleEx(int lModuleNo, ref uint upModuleSubID, System.Text.StringBuilder szModuleName, System.Text.StringBuilder szModuleDescription);
	
	
	//Verifying Module status of specified module board
	[DllImport("AXL.dll")] public static extern uint AxcInfoGetModuleStatus(int lModuleNo);
	
	
	[DllImport("AXL.dll")] public static extern uint AxcInfoGetFirstChannelNoOfModuleNo(int lModuleNo, ref int lpChannelNo);
	    [DllImport("AXL.dll")] public static extern uint AxcInfoGetModuleNoOfChannelNo(int lChannelNo, ref int lpModuleNo);
	
	
	// Setting encoder input method of counter module
	// dwMethod --> 0x00 : Sign and pulse, x1 multiplication
	// dwMethod --> 0x01 : Phase-A and phase-B pulses, x1 multiplication
	// dwMethod --> 0x02 : Phase-A and phase-B pulses, x2 multiplication
	// dwMethod --> 0x03 : Phase-A and phase-B pulses, x4 multiplication
	// dwMethod --> 0x08 : Sign and pulse, x2 multiplication
	// dwMethod --> 0x09 : Increment and decrement pulses, x1 multiplication
	// dwMethod --> 0x0A : Increment and decrement pulses, x2 multiplication
	// SIO-CN2CH/HPC4
	// dwMethod --> 0x00 : Up/Down method, A phase : pulse, B phase : direction
	// dwMethod --> 0x01 : Phase-A and phase-B pulses, x1 multiplication
	// dwMethod --> 0x02 : Phase-A and phase-B pulses, x2 multiplication
	// dwMethod --> 0x03 : Phase-A and phase-B pulses, x4 multiplication
	[DllImport("AXL.dll")] public static extern uint AxcSignalSetEncInputMethod(int lChannelNo, uint dwMethod);
	
	
	// Setting encoder input method of counter module
	// *dwpUpMethod --> 0x00 : Sign and pulse, x1 multiplication
	// *dwpUpMethod --> 0x01 : Phase-A and phase-B pulses, x1 multiplication
	// *dwpUpMethod --> 0x02 : Phase-A and phase-B pulses, x2 multiplication
	// *dwpUpMethod --> 0x03 : Phase-A and phase-B pulses, x4 multiplication
	// *dwpUpMethod --> 0x08 : Sign and pulse, x2 multiplication
	// *dwpUpMethod --> 0x09 : Increment and decrement pulses, x1 multiplication
	// *dwpUpMethod --> 0x0A : Increment and decrement pulses, x2 multiplication
	// SIO-CN2CH/HPC4
	// dwMethod --> 0x00 : Up/Down method, A phase : pulse, B phase : direction
	// dwMethod --> 0x01 : Phase-A and phase-B pulses, x1 multiplication
	// dwMethod --> 0x02 : Phase-A and phase-B pulses, x2 multiplication
	// dwMethod --> 0x03 : Phase-A and phase-B pulses, x4 multiplication
	[DllImport("AXL.dll")] public static extern uint AxcSignalGetEncInputMethod(int lChannelNo, ref uint dwpUpMethod);
	
	
	// Setting trigger of counter module
	// dwMode -->  0x00 : Latch
	// dwMode -->  0x01 : State
	// dwMode -->  0x02 : Special State    --> SIO-CN2CH only
	// SIO-CN2CH
	// dwMode -->  0x00 : absolute position trigger or period position trigger.
	// caution : Each product has different functions. So, it need to use distinctively.
	// dwMode -->  0x01 : Time period trigger(Set to AxcTriggerSetFreq)
	// In the case of SIO-HPC4
	// dwMode -->  0x00 : absolute mode[with ram data].
	// dwMode -->  0x01 : timer mode.
	// dwMode -->  0x02 : absolute mode[with fifo].
	// dwMode -->  0x03 : periodic mode.[Default]
	// For CNT_RECAT_SC_10
	// Setting the value of Trigger Mode for each table int the counter module.
	// dwMode : Trigger Mode 
	//	 [0] CCGC_CNT_RANGE_TRIGGER   : Trigger Output when it is within the acceptable range set at the specified trigger position
	//	 [2] CCGC_CNT_DISTANCE_PERIODIC_TRIGGER : The mode in which a trigger is output to a position equally spaced within the acceptable range set at the specified trigger position.
	//	 [3] CCGC_CNT_PATTERN_TRIGGER : Regardless of where the specified number of outputs a frequency set trigger mode.
	//	 [4] CCGC_CNT_POSITION_ON_OFF_TRIGGER : Hold the trigger output at the specified trigger position (Set in the same way as CCGC_CNT_RANGE_TRIGGER.
	//											Output starts at odd number of TargerPosition and turns off at even number of positions.) 
	//	 [5] CCGC_CNT_AREA_ON_OFF_TRIGGER : Specified low from position upper position to trigger to maintain output mode.

	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetFunction(int lChannelNo, uint dwMode);
	
	
	// Verifying trigger setting of counter module
	// *dwMode -->  0x00 : Latch
	// *dwMode -->  0x01 : State
	// *dwMode -->  0x02 : Special State
	// dwMode -->  0x00 : absolute position trigger or period position trigger.
	// caution : Each product has different functions. So, it need to use distinctively.
	// dwMode -->  0x01 : Time period trigger(Set to AxcTriggerSetFreq)
	// In the case of SIO-HPC4
	// dwMode -->  0x00 : absolute mode[with ram data].
	// dwMode -->  0x01 : timer mode.
	// dwMode -->  0x02 : absolute mode[with fifo].
	// dwMode -->  0x03 : periodic mode.[Default]
	// For CNT_RECAT_SC_10
	// Setting the value of Trigger Mode for each table int the counter module.
	// dwpMode : Trigger Mode 
	//	 [0] CCGC_CNT_RANGE_TRIGGER   : Trigger Output when it is within the acceptable range set at the specified trigger position
	//	 [2] CCGC_CNT_DISTANCE_PERIODIC_TRIGGER : The mode in which a trigger is output to a position equally spaced within the acceptable range set at the specified trigger position.
	//	 [3] CCGC_CNT_PATTERN_TRIGGER : Regardless of where the specified number of outputs a frequency set trigger mode.
	//	 [4] CCGC_CNT_POSITION_ON_OFF_TRIGGER : Hold the trigger output at the specified trigger position (Set in the same way as CCGC_CNT_RANGE_TRIGGER.
	//											Output starts at odd number of TargerPosition and turns off at even number of positions.) 
	//	 [5] CCGC_CNT_AREA_ON_OFF_TRIGGER : Specified low from position upper position to trigger to maintain output mode.

	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetFunction(int lChannelNo, ref uint dwpMode);
	
	
	// dwUsage --> 0x00 : Trigger Not use
	// dwUsage --> 0x01 : Trigger use
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetNotchEnable(int lChannelNo, uint dwUsage);
	
	
	// *dwUsage --> 0x00 : Trigger Not use
	// *dwUsage --> 0x01 : Trigger use
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetNotchEnable(int lChannelNo, ref uint dwpUsage);
	
	
	
	
	// Setting capture polarity of counter module.(External latch input polarity)
	// dwCapturePol --> 0x00 : Signal OFF -> ON
	// dwCapturePol --> 0x01 : Signal ON -> OFF
	[DllImport("AXL.dll")] public static extern uint AxcSignalSetCaptureFunction(int lChannelNo, uint dwCapturePol);
	
	
	// Verifying capture polarity setting of counter module.(External latch input polarity)
	// *dwCapturePol --> 0x00 : Signal OFF -> ON
	// *dwCapturePol --> 0x01 : Signal ON -> OFF
	[DllImport("AXL.dll")] public static extern uint AxcSignalGetCaptureFunction(int lChannelNo, ref uint dwpCapturePol);
	
	
	// Verifying capture position of counter module.(External latch)
	// *dbpCapturePos --> Location of Capture position
	[DllImport("AXL.dll")] public static extern uint AxcSignalGetCapturePos(int lChannelNo, ref double dbpCapturePos);
	
	
	// Verifying counter value of counter module.
	// *dbpActPos --> Counter value
	[DllImport("AXL.dll")] public static extern uint AxcStatusGetActPos(int lChannelNo, ref double dbpActPos);
	
	
	// Setting counter value to dbActPos of counter module.
	[DllImport("AXL.dll")] public static extern uint AxcStatusSetActPos(int lChannelNo, double dbActPos);
	
	
	// Setting trigger position of counter module.
	// Trigger position of counter module can be set up to two.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetNotchPos(int lChannelNo, double dbLowerPos, double dbUpperPos);
	
	
	// Verifying trigger position of counter module.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetNotchPos(int lChannelNo, ref double dbpLowerPos, ref double dbpUpperPos);
	
	
	// Forced trigger output of counter module.
	// dwOutVal --> 0x00 : Trigger output '0'
	// dwOutVal --> 0x01 : Trigger output '1'
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetOutput(int lChannelNo, uint dwOutVal);
	
	
	// Verifying status of counter module.
	// Bit '0' : Carry (Only 1scan turns 'ON' When current counter value is changed to '0' over the upper limit by additional pulse)
	// Bit '1' : Borrow (Only 1scan turns 'ON' When current counter value is changed to lower limit under the '0' by subtraction pulse)
	// Bit '2' : Trigger output status
	// Bit '3' : Latch input status
	[DllImport("AXL.dll")] public static extern uint AxcStatusGetChannel(int lChannelNo, ref uint dwpChannelStatus);
	
	
	
	
	// API for SIO-CN2CH only
	//
	// Setting position unit of counter module
	// ex) If you need 1000pulse for move 1mm distance, Setting like -> dMoveUnitPerPulse = 0.001
	//     And then, setting location-related values of all functions to 'mm' unit.
	[DllImport("AXL.dll")] public static extern uint AxcMotSetMoveUnitPerPulse(int lChannelNo, double dMoveUnitPerPulse);
	
	
	// Verifying position unit of counter module.
	[DllImport("AXL.dll")] public static extern uint AxcMotGetMoveUnitPerPulse(int lChannelNo, ref double dpMoveUnitPerPuls);
	
	
	// Setting inversion function of encoder input counter of counter module.
	// dwReverse --> 0x00 : No reversal.
	// dwReverse --> 0x01 : Reversal.
	[DllImport("AXL.dll")] public static extern uint AxcSignalSetEncReverse(int lChannelNo, uint dwReverse);
	
	
	// Returns whether the function of inverting the encoder input counter of the counter module is set.
	[DllImport("AXL.dll")] public static extern uint AxcSignalGetEncReverse(int lChannelNo, ref uint dwpReverse);
	
	
	// Selecting encoder input signal of counter module.
	// dwSource -->  0x00 : 2(A/B)-Phase signal.
	// dwSource -->  0x01 : Z-Phase signal.(No directionality.)
	[DllImport("AXL.dll")] public static extern uint AxcSignalSetEncSource(int lChannelNo, uint dwSource);
	
	
	// Verifying encoder input signal selection setting of counter module.
	[DllImport("AXL.dll")] public static extern uint AxcSignalGetEncSource(int lChannelNo, ref uint dwpSource);
	
	
	// Setting lower limit value of trigger output range of counter module.
	// Setting lower limit value of range to generate trigger output with position period.(In the case of position period trigger product)
	// Setting standard position application of trigger information of start address of ram.(In the case of absolute location product)
	// Unit : Set by 'AxcMotSetMoveUnitPerPulse'.
	// Note : Lower limit value should be set smaller than upper limit value.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetBlockLowerPos(int lChannelNo, double dLowerPosition);
	
	
	// Verifying lower limit value of trigger output range of counter module.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetBlockLowerPos(int lChannelNo, ref double dpLowerPosition);
	
	
	// Verifying upper limit value of trigger output range of counter module.
	// Setting upper limit value of range to generate trigger output with position period.(In the case of position period trigger product).
	// In the case of the product of absolute position trigger, used as location where trigger information is applied of last address of RAM(Trigger information was set).
	// Unit : Setting from 'AxcMotSetMoveUnitPerPulse'
	// Note : Upper limit value should be set bigger than lower limit value.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetBlockUpperPos(int lChannelNo, double dUpperPosition);
	// Setting lower limit value of trigger output range of counter module.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetBlockUpperPos(int lChannelNo, ref double dpUpperrPosition);
	
	
	// Setting position period used for position peroid mode trigger of counter module.
	// Unit : Sets from 'AxcMotSetMoveUnitPerPulse'.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetPosPeriod(int lChannelNo, double dPeriod);
	
	
	// Verifying position period used for position peroid mode trigger of counter module.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetPosPeriod(int lChannelNo, ref double dpPeriod);
	
	
	// Setting a valid function about position increase/decrease when using trigger of position period mode of counter module.
	// dwDirection -->  0x00 : Output every trigger period.(About counter increase/decrease)
	// dwDirection -->  0x01 : Output every trigger period.(About counter increase)
	// dwDirection -->  0x01 : Output every trigger period.(About counter decrease)
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetDirectionCheck(int lChannelNo, uint dwDirection);
	// Verifying setting value of 'AxcTriggerSetDirectionCheck'.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetDirectionCheck(int lChannelNo, ref uint dwpDirection);
	
	
	// Setting position period and range at once.(About trigger function of position period mode)
	// Positioning unit : Set by 'AxcMotSetMoveUnitPerPulse'.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetBlock(int lChannelNo, double dLower, double dUpper, double dABSod);
	
	
	// Verifying setting value of 'AxcTriggerSetBlock'.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetBlock(int lChannelNo, ref double dpLower, ref double dpUpper, ref double dpABSod);
	
	
	// Setting pulse width of trigger output of counter module.
	// Unit : uSec
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetTime(int lChannelNo, double dTrigTime);
	
	
	// Verifying setting value of 'AxcTriggerSetTime'.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetTime(int lChannelNo, ref double dpTrigTime);
	
	
	// Setting output level of trigger output pulse of counter module.
	// dwLevel -->  0x00 : Low level output.(When trigger is output)
	// dwLevel -->  0x01 : High level output.(When trigger is output)
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetLevel(int lChannelNo, uint dwLevel);
	// Verifying setting value of 'AxcTriggerSetLevel'.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetLevel(int lChannelNo, ref uint dwpLevel);
	
	
	// Setting frequency for Frequency trigger output function of counter module.
	// Unit : Hz, Range : 1Hz ~ 500 kHz
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetFreq(int lChannelNo, uint dwFreqency);
	// Verifying setting value of 'AxcTriggerSetFreq'.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetFreq(int lChannelNo, ref uint dwpFreqency);


    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetTriggerOutCount(int lChannelNo, ref uint dwpTriggerOutCount);
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetTriggerOutCount(int lChannelNo, uint dwTriggerOutCount);



	// Setting the value of universal output about appoint channel of counter module.
	// dwOutput range : 0x00 ~ 0x0F, Four universal outputs for each channel.
	[DllImport("AXL.dll")] public static extern uint AxcSignalWriteOutput(int lChannelNo, uint dwOutput);
	
	
	// Verifying the value of universal output about appoint channel of counter module.
	[DllImport("AXL.dll")] public static extern uint AxcSignalReadOutput(int lChannelNo, ref uint dwpOutput);
	
	
	// Setting the value by bit of universal output about appoint channel of counter module.
	// lBitNo range : 0 ~ 3, Four universal outputs for each channel.
	[DllImport("AXL.dll")] public static extern uint AxcSignalWriteOutputBit(int lChannelNo, int lBitNo, uint uOnOff);
	// Verifying the value by bit of universal output about appoint channel of counter module.
	// lBitNo range : 0 ~ 3
	[DllImport("AXL.dll")] public static extern uint AxcSignalReadOutputBit(int lChannelNo, int lBitNo, ref uint upOnOff);
	
	
	// Check the general-purpose input value for the designated channel of the counter module.
	[DllImport("AXL.dll")] public static extern uint AxcSignalReadInput(int lChannelNo, ref uint dwpInput);
	
	
	// Verifying the value by bit of universal input about appoint channel of counter module.
	// lBitNo range : 0 ~ 3
	[DllImport("AXL.dll")] public static extern uint AxcSignalReadInputBit(int lChannelNo, int lBitNo, ref uint upOnOff);
	
	
	// Activating trigger output of counter module.
	// Sets whether the trigger output will be finally output according to the currently set function.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetEnable(int lChannelNo, uint dwUsage);
	
	
	// Verifying setting value of 'AxcTriggerSetEnable'.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetEnable(int lChannelNo, ref uint dwpUsage);
	
	
	// Verifying RAM contents for absolute trigger function of counter module.
	// dwAddr range : 0x0000 ~ 0x1FFFF;
	[DllImport("AXL.dll")] public static extern uint AxcTriggerReadAbsRamData(int lChannelNo, uint dwAddr, ref uint dwpData);
	
	
	// Setting RAM contents for absolute trigger function of counter module.
	// dwAddr range : 0x0000 ~ 0x1FFFF;
	[DllImport("AXL.dll")] public static extern uint AxcTriggerWriteAbsRamData(int lChannelNo, uint dwAddr, uint dwData);
	
	
	// Setting RAM contents at once for absolute trigger function of counter module.
	// dwTrigNum range : ~ 0x20000  In the case of *RTEX CNT2  0x200*
	// dwDirection --> 0x0 : Input trigger information first about lower limit trigger position. Used for trigger output(In direction of increasing position).
	// dwDirection --> 0x1 : Input trigger information first about upper limit trigger position. Used for trigger output(In direction of decreasing position).

	// FOR CNT_RECAT_SC_10 :(dwDirection -> Reserved).
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetAbs(int lChannelNo, uint dwTrigNum, uint[] dwTrigPos, uint dwDirection);
	
	
	// Set the contents of the RAM required for the absolute position trigger function of the counter module at once.
	// dwTrigNum range : 0 ~ 0x20000 (RTEX CNT2 : 0 ~ 0x200)
	// dwDirection     : (0) Enter the trigger information for the lower limit trigger position. Used for trigger output in the direction of increasing position.
	//                   (1) Enter the trigger information for the upper limit trigger position. Used for trigger output in the direction of decreasing position.
	// dTrigPos        : Use trigger position of counter module as double type
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetAbsDouble(int lChannelNo, uint dwTrigNum, double[] dTrigPos, uint dwDirection);
	// Setting the value of Encoder Input signal to be assigned to the counter module.
	// dwEncoderInput [0-3]: One of the four encoder signals entered into the counter module.
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetEncoderInput(int lModuleNo, uint dwEncoderInput1, uint dwEncoderInput2);
	// Verifying the value of assigned encoder input signal from the counter module
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetEncoderInput(int lModuleNo, ref uint dwpEncoderInput1, ref uint dwpEncoderInput2);
	// Setting the value of Level of the trigger output assigned to each table in the counter module
	// uLevel : Active Level of Trigger Output Signal
	//	 [0]  : 'Low' level output at trigger output.
	//	 [1]  : 'High' level output at trigger output.
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerLevel(int lModuleNo, int lTablePos, uint uLevel);
	// Verifying the value of Level of the trigger output specified in each table in the counter module
	[DllImport("AXL.dll")] public static extern uint AxcTableGetTriggerLevel(int lModuleNo, int lTablePos, ref uint upLevel);
	// Setting the value of Level of the Pulse width assigned to each table in the counter module
	// dTriggerTimeUSec : [Default 500ms], Specify in us units
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerTime(int lModuleNo, int lTablePos, double dTriggerTimeUSec);
	// Verifying the value of Pulse width of the trigger output specified in each table in the counter module
    [DllImport("AXL.dll")] public static extern uint AxcTableGetTriggerTime(int lModuleNo, int lTablePos, ref double dpTriggerTimeUSec);
	// Setting the value of Encoder Input signal to be assigned to the counter module.
	// uEncoderInput1 [0-3]:  One of the four encoder signals entered into the counter module.
	// uEncoderInput2 [0-3]:  One of the four encoder signals entered into the counter module.
    [DllImport("AXL.dll")] public static extern uint AxcTableSetEncoderInput(int lModuleNo, int lTablePos, uint uEncoderInput1, uint uEncoderInput2);
	// Verifying the value of assigned encoder input signal from the counter module
    [DllImport("AXL.dll")] public static extern uint AxcTableGetEncoderInput(int lModuleNo, int lTablePos, ref uint upEncoderInput1, ref uint upEncoderInput2);

	// Setting the value of Trigger output port to be assigned to each table in the couynter module.
	// uTriggerOutport [0x0-0xF]: Bit0: Trigger Output 0, Bit1: Trigger Output 1, Bit2: Trigger Output, Bit3: Trigger Output 
	// Ex) 0x3(3)	: Output a trigger signal to output 0,1
	//	   0xF(255) : Output a trigger signal to output 0,1,2,3
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerOutport(int lModuleNo, int lTablePos, uint uTriggerOutport);
	// Verifying the value of Trigger output port from the counter module
    [DllImport("AXL.dll")] public static extern uint AxcTableGetTriggerOutport(int lModuleNo, int lTablePos, ref uint upTriggerOutport);
	//Setting the value of Error Range for the trigger position set for each table int the counter module.
	// dErrorRange	: Error Range for the position of the trigger
    [DllImport("AXL.dll")] public static extern uint AxcTableSetErrorRange(int lModuleNo, int lTablePos, double dErrorRange);
	// Verifying the value of Error Range for the trigger position set for each table int the counter module.
	[DllImport("AXL.dll")] public static extern uint AxcTableGetErrorRange(int lModuleNo, int lTablePos, ref double dpErrorRange);
	// Generate one shot trigger with the information set on each table in the counter module(trigger output port, trigger pulse width)
	// �� Note : 1) If the trigger is disabled, this function automatically make enable
	//			 2) Incase of Trigger Mode (HPC4_PATTERN_TRIGGER), thie function automatically change trigger mode to HPC4_RANGE_TRIGGER
	//			 3) Incase of Trigger Mode (CCGC_CNT_PATTERN_TRIGGER), thie function automatically change trigger mode to CCGC_CNT_RANGE_TRIGGER
    [DllImport("AXL.dll")] public static extern uint AxcTableTriggerOneShot(int lModuleNo, int lTablePos);
	// Trigger with the Information set on each table in the counter module (Trigger output port, trigger pulse width), at the frequency set by the specified number.
	// lTriggerCount	 : Number of trigger outputs to generate at the specified frequency
	// uTriggerFrequency : Frequency Value
	// �� ���� :  1) If the trigger is disabled, this function automatically make enable 
	//			 2) Incase of Trigger Mode (Not HPC4_PATTERN_TRIGGER), thie function automatically change trigger mode to HPC4_PATTERN_TRIGGER
	//			 3) Incase of Trigger Mode (Not CCGC_CNT_PATTERN_TRIGGER), thie function automatically change trigger mode to CCGC_CNT_PATTERN_TRIGGER
    [DllImport("AXL.dll")] public static extern uint AxcTableTriggerPatternShot(int lModuleNo, int lTablePos, int lTriggerCount, uint uTriggerFrequency);
	// Return the value of trigger setup information(frequency, counter) set for each table in the counter module.
    [DllImport("AXL.dll")] public static extern uint AxcTableGetPatternShotData(int lModuleNo, int lTablePos, ref int lpTriggerCount, ref uint upTriggerFrequency);
	// For SIO-HPC4
	// Setting the value of Trigger Mode for each table int the counter module.
	// uTrigMode : Trigger Mode 
	//	 [0] HPC4_RANGE_TRIGGER   : Trigger Output when it is within the acceptable range set at the specified trigger position
	//	 [1] HPC4_VECTOR_TRIGGER  : The mode in which a trigger is output when the vector direction matches the allowable range set at the specified trigger position.
	//	 [3] HPC4_PATTERN_TRIGGER : Regardless of where the specified number of outputs a frequency set trigger mode.
	
	// For CNT_RECAT_SC_10
	// Setting the value of Trigger Mode for each table int the counter module.
	// uTrigMode : Trigger Mode 
	//	 [0] CCGC_CNT_RANGE_TRIGGER   : Trigger Output when it is within the acceptable range set at the specified trigger position
	//	 [1] CCGC_CNT_VECTOR_TRIGGER  : The mode in which a trigger is output when the vector direction matches the allowable range set at the specified trigger position.
	//	 [2] CCGC_CNT_DISTANCE_PERIODIC_TRIGGER : The mode in which a trigger is output to a position equally spaced within the acceptable range set at the specified trigger position.
	//	 [3] CCGC_CNT_PATTERN_TRIGGER : Regardless of where the specified number of outputs a frequency set trigger mode.
	//	 [4] CCGC_CNT_POSITION_ON_OFF_TRIGGER : Hold the trigger output at the specified trigger position (Set in the same way as CCGC_CNT_RANGE_TRIGGER.
	//											Output starts at odd number of TargerPosition and turns off at even number of positions.) 
	//	 [5] CCGC_CNT_AREA_ON_OFF_TRIGGER : Specified low from position upper position to trigger to maintain output mode.
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerMode(int lModuleNo, int lTablePos, uint uTrigMode);
	// Verifying the value of Trigger Mode for each table int the counter module.
	[DllImport("AXL.dll")] public static extern uint AxcTableGetTriggerMode(int lModuleNo, int lTablePos, ref uint upTrigMode);
	// Initialize the cumulative number of triggers output for each table in the counter module.
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerCountClear(int lModuleNo, int lTablePos);
	// Setting the information to set the trigger signal in the absolute Position (For 2D)
	// lTriggerDataCount : Total number of trigger information to set
	//	 [-1, 0]		 : Initializing registered trigger information data
	// dpTriggerData	 : Absolute position trigger information (number of corresponding arrays should be lTriggerDataCount * 2)
	//	 *[0, 1]		 : X[0], Y[0] 
	// lpTriggerCount	 : Set the number of triggers to be generated when the trigger condition is satisfied to the array
	// dpTriggerInterval : Set the interval in frequency to be maintained when triggering a trigger as continuously as TriggerCount
	// ��Note : 
	//	  1) The number of arrays for each transfer factor should be used with caution. Specifying an array that is less than the factors used internally may result in memory errors.
	//	  2) Trigger Mode automatically changed to HPC4_RANGE_TRIGGER
	//	  3) Disable Trigger inside the function, proceed to all settings, and enable it again after completion
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerData(int lModuleNo, int lTablePos, int lTriggerDataCount, double[] dpTriggerData, int[] lpTriggerCount, double[] dpTriggerInterval);
	// Set triggers set to output a trigger signal to each table of the counter module information.
	// �� Note : For each registered up to trigger the number of data on the table below when you don't know ahead of the number of triggers, such as data and use.
	// Ex)		1) AxcTableGetTriggerData(lModuleNo, lTablePos, &lTriggerDataCount, NULL, NULL, NULL);
	//			2) dpTriggerData	 = new double[lTriggerDataCount * 2];
	//			3) lpTriggerCount	 = new long[lTriggerDataCount];
	//			4) dpTriggerInterval = new double[lTriggerDataCount];  
    [DllImport("AXL.dll")] public static extern uint AxcTableGetTriggerData(int lModuleNo, int lTablePos, ref int lpTriggerDataCount, double[] dpTriggerData, int[] lpTriggerCount, double[] dpTriggerInterval);
	// For SIO-HPC4 ///////////////////////////
	// Setting the information required to output the trigger signal at each table in the counter module in a different way than the AxcTableSetTriggerData function.
	// lTriggerDataCount : Total number of trigger information to set
	// uOption : specify the data entry method for the dpTriggerData array
	//	 [0]   : Enter the order X Pos[0], Y Pos[0], X Pos[1], and Y Pos[1] into the dpTriggerData array.
	//	 [1]   : Enter X Pos[0], Y Pos[0], Count, Inteval, X Pos[1], Y Pos[1], Count, and Inteval in order of dpTriggerData array.
	// ��Note : 
	//	  1) The number of arrays for each transfer factor should be used with caution. Specifying an array that is less than the factors used internally may result in memory errors.
	//	  2) Trigger Mode automatically changed to HPC4_RANGE_TRIGGER
	//	  3) Disable Trigger inside the function, proceed to all settings, and enable it again after completion
	// For CNT_RECAT_SC_10/////////////////////////// 
	// Setting the information required to output the trigger signal at each table in the counter module in a different way than the AxcTableSetTriggerData function.
	// lTriggerDataCount : Total number of trigger information to set
	// uOption : Reserved
	// dpTriggerData : Trigger Data
	//	Case : CCGC_CNT_RANGE_TRIGGER 
	//		Enter the order X Pos[0], Y Pos[0], X Pos[1], and Y Pos[1] into the dpTriggerData array.
	//	Case : CCGC_CNT_VECTOR_TRIGGER 
	//		In an array dptriggerdata x pos [0], y pos [0], x unitvector y and [0] unitvector [0] x pos y and [1] pos x, [1], unitvector y, [1], unitvector [1] Enter in the order.
	//	Case : CCGC_CNT_POSITION_ON_OFF_TRIGGER 
	//		Enter the order X Pos[0], Y Pos[0], X Pos[1], and Y Pos[1] into the dpTriggerData array.	
	// ��Note : 
	//	  1) The number of arrays for each transfer factor should be used with caution. Specifying an array that is less than the factors used internally may result in memory errors.
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerDataEx(int lModuleNo, int lTablePos, int lTriggerDataCount, uint uOption, double[] dpTriggerData);
	// For SIO-HPC4  ///////////////////////////
	// Set triggers set to output a trigger signal to each table of the counter module information.
	// �� Note : For each registered up to trigger the number of data on the table below when you don't know ahead of the number of triggers, such as data and use.
	// Ex)		1) AxcTableGetTriggerDataEx(lModuleNo, lTablePos, &lTriggerDataCount, &uOption, NULL);
	//			2) if(uOption == 0) : dpTriggerData 	= new double[lTriggerDataCount * 2];
	//			3) if(uOption == 1) : dpTriggerData 	= new double[lTriggerDataCount * 4];
	// For CNT_RECAT_SC_10/////////////////////////// 
	// Set triggers set to output a trigger signal to each table of the counter module information.
	// �� Note : For each registered up to trigger the number of data on the table below when you don't know ahead of the number of triggers, such as data and use.
	// uOption : Reserved
	// Ex)		1) AxcTableGetTriggerDataEx(lModuleNo, lTablePos, &lTriggerDataCount, &uOption, NULL);
	//	Case : CCGC_CNT_RANGE_TRIGGER 
	//		dpTriggerData	  = new double[lTriggerDataCount * 2];
	//	Case : CCGC_CNT_VECTOR_TRIGGER 
	//		dpTriggerData	  = new double[lTriggerDataCount * 4];
	//	Case : CCGC_CNT_POSITION_ON_OFF_TRIGGER 
	//		dpTriggerData	  = new double[lTriggerDataCount * 2];
    [DllImport("AXL.dll")] public static extern uint AxcTableGetTriggerDataEx(int lModuleNo, int lTablePos, ref int lpTriggerDataCount, ref uint upOption, double[] dpTriggerData);
	// Deletes all trigger data set in the specified table in the counter module and all data in the H/W FIFO.
    [DllImport("AXL.dll")] public static extern uint AxcTableSetTriggerDataClear(int lModuleNo, int lTablePos);
	// Enables the trigger output function of the specified table on the counter module.
	// uEnable : Whether or not to use the feature output a trigger settings.
	// �� Note : 1) DISABLE during trigger output causes the output to stop immediately
	//			 2) AxcTableTriggerOneShot, AxcTableGetPatternShotData,AxcTableSetTriggerData, AxcTableGetTriggerDataEx functions automatically call ENABLE
    [DllImport("AXL.dll")] public static extern uint AxcTableSetEnable(int lModuleNo, int lTablePos, uint uEnable);
	// Trigger of the table specified by the module output function is to verify whether the operation of the counter.
    [DllImport("AXL.dll")] public static extern uint AxcTableGetEnable(int lModuleNo, int lTablePos, ref uint upEnable);
	// Check the number generated using a table specifying the module's counter trigger.
	// lpTriggerCount : eturns the number of trigger outputs that have been output to date, initializes to the AxcTableSetTriggerCountClear function
    [DllImport("AXL.dll")] public static extern uint AxcTableReadTriggerCount(int lModuleNo, int lTablePos, ref int lpTriggerCount);
	// Check the status of the H/W trigger data FIFO assigned to the specified table in the counter module.
	// lpCount1[0~500] :Number of data entered in H/W FIFO where the first (X) position of the 2D trigger position data is being held
	// upStatus1 : Status of H/W FIFO with the first (X) position of the 2D trigger position data at rest
	//	 [Bit 0] : Data Empty
	//	 [Bit 1] : Data Full
	//	 [Bit 2] : Data Valid
	// lpCount2[0~500] : Number of data entered in H/W FIFO where the second (Y) position of the 2D trigger position data 
	// upStatus2 : Status of H/W FIFO with the second (y) position of the 2D trigger position data at rest
	//	 [Bit 0] : Data Empty
	//	 [Bit 1] : Data Full
	//	 [Bit 2] : Data Valid
    [DllImport("AXL.dll")] public static extern uint AxcTableReadFifoStatus(int lModuleNo, int lTablePos, ref int lpCount1, ref uint upStatus1, ref int lpCount2, ref uint upStatus2);
	// Check the current position data value of the H/W trigger data FIFO assigned to the specified table in the counter module.
	// dpTopData1 : 2D H/W First(X) Position Top Data.
	// dpTopData1 : 2D H/W Second(Y) Position Top Data
    [DllImport("AXL.dll")] public static extern uint AxcTableReadFifoData(int lModuleNo, int lTablePos, ref double dpTopData1, ref double dpTopData2);

	// Setting the value of dimension of counter module.
	// dwDimension : 1D/2D Value
	//	[0] : 1D
	//	[1] : 2D
	[DllImport("AXL.dll")] public static extern uint AxcTableSetDimension(int lModuleNo, int lTablePos, int dwDimension);
	// Verifying the value of dimension of counter module.
	// dwpDimension : 1D/2D Value
	//	[0] : 1D
	//	[1] : 2D
	[DllImport("AXL.dll")] public static extern uint AxcTableGetDimension(int lModuleNo, int lTablePos, ref uint dwpDimension);

	// Setting lower limit value of trigger output range of counter module.
	// Setting lower limit value of range to generate trigger output with position period.(In the case of position period trigger product)
	// Setting standard position application of trigger information of start address of ram.(In the case of absolute location product)
	// Unit : Set by 'AxcMotSetMoveUnitPerPulse'.
	// Note : Lower limit value should be set smaller than upper limit value.
	[DllImport("AXL.dll")] public static extern uint AxcTableSetBlockLowerPos(int lModuleNo, int lTablePos, double dLowerPosition1, double dLowerPosition2);
	// Verifying lower limit value of trigger output range of counter module.
	[DllImport("AXL.dll")] public static extern uint AxcTableGetBlockLowerPos(int lModuleNo, int lTablePos, ref double dpLowerPosition1, ref double dpLowerPosition2);
	// Setting upper limit value of trigger output range of counter module.
	// Setting upper limit value of range to generate trigger output with position period.(In the case of position period trigger product).
	// In the case of the product of absolute position trigger, used as location where trigger information is applied of last address of RAM(Trigger information was set).
	// Unit : Setting from 'AxcMotSetMoveUnitPerPulse'
	// Note : Upper limit value should be set bigger than lower limit value.
	[DllImport("AXL.dll")] public static extern uint AxcTableSetBlockUpperPos(int lModuleNo, int lTablePos, double dUpperPosition1, double dUpperPosition2);
	// Verifying upper limit value of trigger output range of counter module.
	[DllImport("AXL.dll")] public static extern uint AxcTableGetBlockUpperPos(int lModuleNo, int lTablePos, ref double dpUpperPosition1, ref double dpUpperPosition2);
	
	// Generates a trigger with the information set on each table in the counter module(trigger output port, trigger pulse width), equal spacing to the specified position within the allowable range.
	// dDistance	 : position interval of trigger made
	// �� Note : 1) If Trigger is disabled, this function is automatically causes the trigger with patterns through enable. 
	//			 2) If Trigger mode is not in CCGC_CNT_DISTANCE_PERIODIC_TRIGGER mode, this tunction automatically changes the trigger mode CCGC_CNT_DISTANCE_PERIODIC_TRIGGER
	[DllImport("AXL.dll")] public static extern uint AxcTableTriggerDistancePatternShot(int lModuleNo, int lTablePos, double dDistance);
	// Check the trigger setup information(interal between Trigger)set on each table in the counter module.
	[DllImport("AXL.dll")] public static extern uint AxcTableGetDistancePatternShotData(int lModuleNo, int lTablePos, ref double dpDistance);

	// For CNT_RECAT_SC_10/////////////////////////// 
	// Check the status of the H/W trigger data FIFO assigned to the specified channel in the counter module.
	// lpCount[0~500] :Number of data entered in H/W FIFO where the first position of the trigger position data is being held
	// upStatus : Status of H/W FIFO with the first position of the trigger position data at rest
	//	 [Bit 0] : Data Empty
	//	 [Bit 1] : Data Full
	//	 [Bit 2] : Data Valid
	[DllImport("AXL.dll")] public static extern uint AxcTriggerReadFifoStatus(int lChannelNo, ref int lpCount, ref uint upStatus); 
	
	// Check the current position data value of the H/W trigger data FIFO assigned to the specified channel in the counter module.
	// dpTopData : First Position Top Data.
	
	[DllImport("AXL.dll")] public static extern uint AxcTriggerReadFifoData(int lChannelNo, ref double dpTopData);

	// Set triggers set to output a trigger signal to each channel of the counter module information.
	// �� Note : For each registered up to trigger the number of data on the channel below when you don't know ahead of the number of triggers, such as data and use.
	// Ex)      1) AxcTriggerGetAbs(long lChannelNo, DWORD* dwpTrigNum, DWORD* dwTrigPos, DWORD dwDirection);
	//          2) dwpTrigPos     = new uint[dwpTrigPos];  
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetAbs(int lChannelNo, ref uint dwpTrigNum, uint[] dwpTrigPos);
	// Trigger with the Information set on each channel in the counter module (Trigger output port, trigger pulse width), at the frequency set by the specified number.
	// lTriggerCount	 : Number of trigger outputs to generate at the specified frequency
	// uTriggerFrequency : Frequency Value
	// �� ���� :  1) If the trigger is disabled, this function automatically make enable 
	//			 2) Incase of Trigger Mode (Not HPC4_PATTERN_TRIGGER), thie function automatically change trigger mode to HPC4_PATTERN_TRIGGER
	//			 3) Incase of Trigger Mode (Not CCGC_CNT_PATTERN_TRIGGER), thie function automatically change trigger mode to CCGC_CNT_PATTERN_TRIGGER
	[DllImport("AXL.dll")] public static extern uint AxcTriggerPatternShot(int lChannelNo, int lTriggerCount, uint uTriggerFrequency);

	// Return the value of trigger setup information(frequency, counter) set for each table in the counter module.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetPatternShotData(int lChannelNo, ref int lpTriggerCount, ref uint upTriggerFrequency);
	// Initialize the cumulative number of triggers output for each channel in the counter module.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetTriggerCountClear(int lChannelNo);
	// Check the number generated using a channel specifying the module's counter trigger.
	// lpTriggerCount : eturns the number of trigger outputs that have been output to date, initializes to the AxcTriggerSetTriggerCountClear function
	[DllImport("AXL.dll")] public static extern uint AxcTriggerReadTriggerCount(int lChannelNo, ref int lpTriggerCount);
	// Generates a trigger with the information set on each channel in the counter module(trigger output port, trigger pulse width), equal spacing to the specified position within the allowable range.
	// dDistance	 : position interval of trigger made
	// �� Note : 1) If Trigger is disabled, this function is automatically causes the trigger with patterns through enable. 
	//			 2) If Trigger mode is not in CCGC_CNT_DISTANCE_PERIODIC_TRIGGER mode, this tunction automatically changes the trigger mode CCGC_CNT_DISTANCE_PERIODIC_TRIGGER
	[DllImport("AXL.dll")] public static extern uint AxcTriggerDistancePatternShot(int lChannelNo,double dDistance);
	// Check the trigger setup information(interal between Trigger)set on each channel in the counter module.
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetDistancePatternShotData(int lChannelNo,ref double dpDistance);

	// Setting the value of Trigger output port to be assigned to each channel in the couynter module.
	// uTriggerOutport [0x0-0xF]: Bit0: Trigger Output 0, Bit1: Trigger Output 1, Bit2: Trigger Output, Bit3: Trigger Output 
	// Ex) 0x3(3)	: Output a trigger signal to output 0,1
	//	   0xF(255) : Output a trigger signal to output 0,1,2,3
	[DllImport("AXL.dll")] public static extern uint AxcTriggerSetTriggerOutport(int lChannelNo, uint dwTriggerOutPort);
	// Verifying the value of Trigger output port from the counter module
	[DllImport("AXL.dll")] public static extern uint AxcTriggerGetTriggerOutport(int lChannelNo, ref uint dwpTriggerOutPort);

}
