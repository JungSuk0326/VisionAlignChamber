/****************************************************************************
*****************************************************************************
**
** File Name
** ----------
**
** AXL.CS
**
** COPYRIGHT (c) AJINEXTEK Co., LTD
**
*****************************************************************************
*****************************************************************************
**
** Description
** -----------
** Ajinextek Library Header File
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

public class CAXL
{


//========== Library initialization =================================================================================

// Library initialization
[DllImport("AXL.dll")] public static extern uint AxlOpen(int lIrqNo);


// Do not reset to hardware chip when library is initialized.
[DllImport("AXL.dll")] public static extern uint AxlOpenNoReset(uint lIrqNo);

// Exit from library use
[DllImport("AXL.dll")] public static extern int AxlClose();

// Verify if the library is initialized
[DllImport("AXL.dll")] public static extern int AxlIsOpened();


// Use Interrupt
[DllImport("AXL.dll")] public static extern uint AxlInterruptEnable();

// Not use Interrput
[DllImport("AXL.dll")] public static extern uint AxlInterruptDisable();

//========== library and base board information =================================================================================

// Verify the number of registered base board
[DllImport("AXL.dll")] public static extern uint AxlGetBoardCount(ref int lpBoardCount);
// Verify the library version
[DllImport("AXL.dll")] public static extern uint AxlGetLibVersion(ref byte szVersion);

// Verify Network models Module Status
[DllImport("AXL.dll")] public static extern uint AxlGetModuleNodeStatus(int nBoardNo, int nModulePos);

// Verify Board Status
[DllImport("AXL.dll")] public static extern uint AxlGetBoardStatus(int nBoardNo);

// return Configuration Lock status of Network product.
// *wpLockMode  : DISABLE(0), ENABLE(1)
[DllImport("AXL.dll")] public static extern uint AxlGetLockMode(int nBoardNo, ref uint upLockMode);


// Function to check whether all slaves of specified network type are operational
// NETWORK_TYPE_ALL(0)
// NETWORK_TYPE_RTEX(1)
// NETWORK_TYPE_MLII(2)
// NETWORK_TYPE_MLIII(3)
// NETWORK_TYPE_SIIIH(4)
// NETWORK_TYPE_ECAT(5)
[DllImport("AXL.dll")] public static extern uint AxlIsConnectedAllSlaves(uint uNetworkType);

[DllImport("AXL.dll")] public static extern uint AxlGetReturnCodeInfo(uint dwReturnCode, int lReturnInfoSize, ref int lpRecivedSize, ref char[] szReturnInfo);
[DllImport("AXL.dll")] public static extern uint AxlGetReturnCodeInfo(uint dwReturnCode, int lReturnInfoSize, ref int lpRecivedSize, ref string szReturnInfo);

//========= Log Level =================================================================================
// Set message level to be output to EzSpy
// uLevel : 0 - 3 set
// LEVEL_NONE(0)    : ALL Message don't Output
// LEVEL_ERROR(1)   : Error Message Output
// LEVEL_RUNSTOP(2) : Run/Stop relative Message Output during Motion.
// LEVEL_FUNCTION(3): ALL Message don't Output
[DllImport("AXL.dll")] public static extern uint AxlSetLogLevel(uint uLevel);
// Verify message level to be output to EzSpy
[DllImport("AXL.dll")] public static extern uint AxlGetLogLevel(ref uint upLevel);


//========== MLIII =================================================================================
// The API for start searching each module of network product.
[DllImport("AXL.dll")] public static extern uint AxlScanStart(int lBoardNo, long lNet);
// The API for connect all modules of each board.
[DllImport("AXL.dll")] public static extern uint AxlBoardConnect(int lBoardNo, long lNet);
// The API for disconnect all modules of each board.
[DllImport("AXL.dll")] public static extern uint AxlBoardDisconnect(int lBoardNo, long lNet);

[DllImport("AXL.dll")] public static extern uint AxlReadFanSpeed(int lBoardNo, ref double dpFanSpeed);

[DllImport("AXL.dll")] public static extern uint AxlEzSpyUserLog(string szUserLog);


}
