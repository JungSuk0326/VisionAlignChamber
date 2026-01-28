/****************************************************************************
*****************************************************************************
**
** File Name
** ---------
**
** AXP.CS
**
** COPYRIGHT (c) AJINEXTEK Co., LTD
**
*****************************************************************************
*****************************************************************************
**
** Description
** -----------
** Ajinextek Macro Library Header File
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

using System.Runtime.InteropServices;

public class CAXP
{
    // Starts node registration in the specified macro.
    // lMacroNo     : 0 ~ 7
    [DllImport("AXL.dll")] public static extern uint AxpMacroBeginNode(int nMacroNo);    
    // Terminates node registration to the specified macro.
    // lMacroNo     : 0 ~ 7
    [DllImport("AXL.dll")] public static extern uint AxpMacroEndNode(int nMacroNo);    
    // Delete all nodes registered in the specified macro.
    // lMacroNo     : Specifies the macro number to delete all registered nodes.
    //  [-1]        : If -1, all nodes registered in all macros are deleted.
    //  [0 ~ 7]     : Delete all nodes registered in the specified macro.
    [DllImport("AXL.dll")] public static extern uint AxpMacroWriteClear(int nMacroNo);
    
    // Registers the node in the specified macro.
    // lMacroNo     : 0 ~ 7
    // uFunction    : Specifies the function to register in the macro. { MACRO_FUNCTION }
    //  [0] MACRO_FUNC_CALL         : Call another macro (called macro exits or returns if it returns)
    //  [1] MACRO_FUNC_JUMP         : Jump to another macro or node. (Does not return to JUMP position)
    //  [2] MACRO_FUNC_RETURN       : Return to the calling macro
    //  [3] MACRO_FUNC_REPEAT       : Repeatedly driven the number of times between the specified nodes in the current position
    //  [4] MACRO_FUNC_SET_OUTPUT   : Outputs Digital Output and Analog Output
    //  [5] MACRO_FUNC_WAIT         : Waits for the specified time
    //  [6] MACRO_FUNC_STOP         : Stop macro driving
    // dpArg[16] : An array of arguments to the specified function
    // ¡Ø Note: The number of arrays must be 16
    //  [0] MACRO_FUNC_CALL
    //      dArg[0] Macro Number
    //  [1] MACRO_FUNC_JUMP
    //      dArg[0] Jump Type       : [0] MACRO_JUMP_MACRO, [1] MACRO_JUMP_NODE
    //      dArg[1] Macro number or node number
    //  [2] MACRO_FUNC_RETURN       : No arguments
    //  [3] MACRO_FUNC_REPEAT       : Repetitive drive element
    //      dArg[0] Number of times to repeat
    //      dArg[1] Node number to repeat at current location (must be lower than current node's number)
    //  [4] MACRO_FUNC_SET_OUTPUT
    //      dArg[0] Output Type     : [0] MACRO_DIGITAL_OUTPUT, [1] MACRO_ANALOG_OUTPUT
    //      dArg[1] DO module number or AO channel number
    //      dArg[2] Data Type
    //        - For Digital Output  : [0] MACRO_DATA_BIT, [1] MACRO_DATA_BYTE, [2] MACRO_DATA_WORD, [3] MACRO_DATA_DWORD, [4] MACRO_DATA_BYTE12 
    //        - For Analog Output   : [5] MACRO_DATA_VOLTAGE, [6] MACRO_DATA_DIGIT
    //      dArg[3] Offset or Byte Size
    //        - For Digital Output  : Offset / ByteSize(For MACRO_DATA_BYTE12 Type)
    //        - For Analog Output   : Reserved
    //      dArg[4] Ãâ·Â °ª
    //        - For Digital Output  : Digital Output according to the setting of Data Type,
    //        - For Analog Output   : Output voltage or output Digit
    //      dArg[4] ~ dArg[15] Byte Output value (For MACRO_DATA_BYTE12 Type)
    //        - Outputs the specified byte (maximum 12 Bytes) at once, regardless of the number of output modules.
    //  [5] MACRO_FUNC_WAIT
    //      dArg[0] waiting time(ms): Waits for the specified time
    //  [6] MACRO_FUNC_STOP
    //      dArg[0] Stop mode       : [0] MACRO_QUICK_STOP, [1] MACRO_SLOW_STOP
    [DllImport("AXL.dll")] public static extern uint AxpMacroAddNode(int nMacroNo, uint uFunction, ref double dpArg);
    // Returns the value set in the node of the specified macro. (The return value is the value set by the AxpMacroAddNode function)
    [DllImport("AXL.dll")] public static extern uint AxpMacroGetNode(int nMacroNo, int nNodeNo, ref uint upFunction, ref double dpArg);
    // Checks the registered nodes in the specified macro and returns the node location where the error occurred and the error code.
    // ¡Ø Note: Be sure to validate nodes registered with this function so that you can run macros.
    // lMacroNo         : 0 ~ 7
    // *lpErrorNodeNo   : Node number where the error occurred
    // *upErrorCode     : Error code for the node where the error occurred
    [DllImport("AXL.dll")] public static extern uint AxpMacroCheckNodeAll(int nMacroNo, ref int npErrorNodeNo, ref uint upErrorCode);
    // Starts the specified macro.
    // ¡Ø Note: Be sure to validate the nods registered with the AxpMacroCheckNodeAll function before you can run the macro.
    // lMacroNo         : 0 ~ 7
    // uCondition       : Set macro start condition
    //  [0] MACRO_START_READY       : Drive to ready state
    //  [1] MACRO_START_IMMEDIATE   : Immediate drive
    // bLockResource    : Set whether to prohibit the control for the digital input module and analog output channels registered in the macro.
    //  [FALSE]         : Allow control
    //  [TRUE]          : Prohibit control
    // lRepeatCount     : Set the total number of repetitions of macro driving
    //  [-1]            : Infinitely repeatable drive
    //  [0]             : Do not drive repeatedly
    //  [1~ ]           : Repeatedly driven as many times as specified
    [DllImport("AXL.dll")] public static extern uint AxpMacroStart(int nMacroNo, uint uCondition, bool bLockResource, int nRepeatCount);
    // Returns the driving condition of the specified macro.
    // lMacroNo         : 0 ~ 7
    [DllImport("AXL.dll")] public static extern uint AxpMacroGetStartInfo(int nMacroNo, ref uint upCondition, ref bool bpLockResource, ref int npRepeatCount);
    // Stop driving the specified macro.
    // lMacroNo         : -1 ~ 7
    //  [-1]            : If it is -1, stop all macros.
    // uStopMode        : Specifies the drive stop mode. (Meaning there is a motion drive shaft in the registered macro)
    //  [0] MACRO_QUICK_STOP
    //  [1] MACRO_SLOW_STOP
    [DllImport("AXL.dll")] public static extern uint AxpMacroStop(int nMacroNo, uint uStopMode);
    // Returns the running status of the specified macro.
    // lMacroNo         : 0 ~ 7
    // *upStatus        : The running state of the macro
    //  [0] MACRO_STATUS_STOP   : Macro stop state
    //  [1] MACRO_STATUS_READY  : Macro ready state
    //  [2] MACRO_STATUS_RUN    : Macro running state
    //  [3] MACRO_STATUS_ERROR  : Macro error status (stop status)
    // *lpRepeatCount   : Returns the total number of iterations of the macro
    [DllImport("AXL.dll")] public static extern uint AxpMacroReadRunStatus(int nMacroNo, ref uint upStatus, ref int npRepeatCount);
    // Returns the number of nodes registered in the specified macro and currently running node information.
    // lMacroNo         : 0 ~ 7
    // *lpCurNodeNo     : Number of currently running node
    // *lpTotalNodeNum  : Total number of nodes in the specified macro
    [DllImport("AXL.dll")] public static extern uint AxpMacroGetNodeNum(int nMacroNo, ref int npCurNodeNo, ref int npTotalNodeNum);
    // Returns the drive information for the specified macro and function.
    // lMacroNo         : 0 ~ 7
    // uFunction        : Specifies the function whose information is to be verified. { MACRO_FUNCTION }
    //  [0] MACRO_FUNC_CALL
    //  [1] MACRO_FUNC_JUMP
    //  [2] MACRO_FUNC_RETURN
    //  [3] MACRO_FUNC_REPEAT
    //  [4] MACRO_FUNC_SET_OUTPUT
    //  [5] MACRO_FUNC_WAIT
    //  [6] MACRO_FUNC_STOP
    // dpReturnData[16]  : Return value for the specified function
    // ¡Ø Note: The number of arrays must be 16
    //  [0] MACRO_FUNC_CALL
    //      dArg[0] Node number of the last called CALL instruction
    //      dArg[1] Returns the index of the CALL commands in the specified macro
    //  [1] MACRO_FUNC_JUMP
    //      dArg[0] Node number of the last called JUMP command
    //      dArg[1] Returns the index of the JUMP command's arguments within the specified macro
    //  [2] MACRO_FUNC_RETURN       : No arguments
    //      dArg[0] Node number of the last called RETURN command
    //      dArg[1] Returns the index of the RETURN command arguments within the specified macro.
    //  [3] MACRO_FUNC_REPEAT       : Repetitive drive element
    //      dArg[0] Node number of the last called REPEAT command
    //      dArg[1] Returns the index of the REPEAT command arguments within the specified macro.
    //      dArg[2] Returns the number of iterations set in the last called REPEAT command
    //      dArg[3] Returns the number of repetitions in the last called REPEAT command
    //  [4] MACRO_FUNC_SET_OUTPUT
    //      dArg[0] Node number of the last called SET_OUTPUT command
    //      dArg[1] Returns the index of the SET_OUTPUT command arguments in the specified macro
    //      dArg[2] Returns the output value of the last called SET_OUTPUT command
    //      dArg[2~13] Byte output value when Data Type is "[4] MACRO_DATA_BYTE12"
    //  [5] MACRO_FUNC_WAIT
    //      dArg[0] Node number of the last called WAIT instruction
    //      dArg[1] Returns the index of the WAIT command arguments in the specified macro
    //      dArg[2] Returns the wait time set by the last called WAIT command
    //      dArg[3] Returns the time waited on the last called WAIT command
    //  [6] MACRO_FUNC_STOP
    //      dArg[0] Node number of the last called STOP instruction
    //      dArg[1] Returns the index of the STOP command arguments within the specified macro.
    [DllImport("AXL.dll")] public static extern uint AxpMacroReadFunctionStatus(int nMacroNo, uint uFunction, ref double dpReturnData);
}