/****************************************************************************
*****************************************************************************
**
** File Name
** ---------
**
** AXS.CS
**
** COPYRIGHT (c) AJINEXTEK Co., LTD
**
*****************************************************************************
*****************************************************************************
**
** Description
** -----------
** Ajinextek Motion Library Header File
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

public class CAXS
{
    //========== Board and module verification functions(Info) - Infomation ===============================================================
    // Returns the board number, module location, and module ID of the corresponding port.
    [DllImport("AXL.dll")] public static extern uint AxsInfoGetPort(int nAxisNo, ref int npBoardNo, ref int npModulePos, ref uint upModuleID);

    // Get the specified module board : Sub ID, module name, module description
    //===============================================/
    // support product      : EtherCAT
    // upModuleSubID        : EtherCAT SubID(for distinguish between EtherCAT modules)
    // szModuleName         : model name of module(50 Bytes)
    // szModuleDescription  : description of module(80 Bytes)
    //======================================================//
    [DllImport("AXL.dll")] public static extern uint AxsInfoGetPortEx(int nPortNo, ref uint upModuleSubID, System.Text.StringBuilder szModuleName, System.Text.StringBuilder szModuleDescription);

    // Returns whether the serial module exists.
    [DllImport("AXL.dll")] public static extern uint AxsInfoIsSerialModule(ref uint upStatus);

    // Returns whether the port is valid.
    [DllImport("AXL.dll")] public static extern uint AxsInfoIsInvalidPortNo(int nPortNo);

    // Returns whether the port is controllable.
    [DllImport("AXL.dll")] public static extern uint AxsInfoGetPortStatus(int nPortNo);

    // Returns the number of valid communication ports in the system.
    [DllImport("AXL.dll")] public static extern uint AxsInfoGetPortCount(ref int npPortCount);

    // Returns the first axis number of the board / module.
    [DllImport("AXL.dll")] public static extern uint AxsInfoGetFirstPortNo(int nBoardNo, int nModulePos, ref int npPortNo);

    // Returns the first communication port number of the board.
    [DllImport("AXL.dll")] public static extern uint AxsInfoGetBoardFirstPortNo(int nBoardNo, int nModulePos, ref int lpPortNo);

    //========== Serial communication function(Port) =================================================================================
    // Open communication port. PortOpen can be used only by one application.
    // lBaudRate : 300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200
    // lDataBits : 7, 8 
    // lStopBits : 1, 2
    // lParity   : [0]None, [1]Even, [2]Odd
    // dwFlagsAndAttributes : Reserved
    [DllImport("AXL.dll")] public static extern uint AxsPortOpen(int nPortNo, int nBaudRate, int nDataBits, int nStopBits, int nParity, uint dwFlagsAndAttributes);
     // Close the communication port.
    [DllImport("AXL.dll")] public static extern uint AxsPortClose(int nPortNo);
 
    // Set communication port (communication buffer is not initialized)
    // lpDCB->BaudRate  : 300, 600, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200
    // lpDCB->ByteSize  : 7, 8  
    // lpDCB->StopBits  : 1, 2
    // lpDCB->Parity    : [0]None, [1]Even, [2]Odd
    [DllImport("kernel32.dll")] public static extern uint AxsPortSetCommState(int nPortNo, ref DCB lpDCB);
    // Get the communication port setting value.
    [DllImport("kernel32.dll")] public static extern uint AxsPortGetCommState(int nPortNo, ref DCB lpDCB);
    // Set the communication port timeout value.
    // lpCommTimeouts->ReadIntervalTimeout          : Set timeout time between strings after string input starts (milliseconds)
    // lpCommTimeouts->ReadTotalTimeoutMultiplier;  : Timeout time setting for one character string at the communication speed set in the read operation (milliseconds)
    // lpCommTimeouts->ReadTotalTimeoutConstant;    : Timeout time setting excluding the timeout for the number of characters to be input (milliseconds)
    // lpCommTimeouts->WriteTotalTimeoutMultiplier; : Timeout time setting for one character string at the communication speed set in writing operation (milliseconds)
    // lpCommTimeouts->WriteTotalTimeoutConstant;   : Timeout time setting excluding the timeout for the number of characters to be transferred (milliseconds)
    [DllImport("kernel32.dll")] public static extern uint AxsPortSetCommTimeouts(int nPortNo, ref COMMTIMEOUTS lpCommTimeouts);
    // Get the timeout value of the communication port.
    [DllImport("kernel32.dll")] public static extern uint AxsPortGetCommTimeouts(int nPortNo, ref COMMTIMEOUTS lpCommTimeouts);
    // Erase the device's error flag or check the number of data sent and received.
    // lpErrors : 
    //      [1]CE_RXOVER:       Receive buffer overflow
    //      [2]CE_OVERRUN:      Receive Buffer Overrun Error
    //      [4]CE_RXPARITY:     Receive data parity bit error
    //      [8]CE_FRAME:        Receive framing error
    // lpStat->cbInQue :        Number of data input to receive buffer
    // lpStat->cbOutQue:        Number of data remaining in the transmit buffer
    [DllImport("kernel32.dll")] public static extern uint AxsPortClearCommError(int nPortNo, out uint lpErrors, ref COMSTAT lpStat);
    // Stop sending data
    [DllImport("AXL.dll")] public static extern uint AxsPortSetCommBreak(int nPortNo);
    // Resume data transmission
    [DllImport("AXL.dll")] public static extern uint AxsPortClearCommBreak(int nPortNo);    
    // Stop sending or receiving or clear buffer
    // dwFlags: 
    //      [1]PURGE_TXABORT:    Stop writing
    //      [2]PURGE_RXABORT:    Stop Reading
    //      [4]PURGE_TXCLEAR:    Clear if there is data in the transmit buffer
    //      [8]PURGE_RXCLEAR:    Clear if there is data in the receive buffer
    [DllImport("AXL.dll")] public static extern uint AxsPortPurgeComm(int nPortNo, uint dwFlags);
    // Writes data to the serial port
    // lpBuffer :                The pointer value of the buffer that holds the data to write to the device.
    // nNumberOfBytesToWrite :   Number of bytes of actual data in lpBuffer
    // lpNumberOfBytesWritten :  Returns the number of bytes actually written (in case of None Overrapped)
    // lpOverlapped :            Point value of OVERLAPPED structure for asynchronous
    [DllImport("kernel32.dll")] public static extern uint AxsPortWriteFile(int nPortNo, IntPtr lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, ref OVERLAPPED lpOverlapped);
    // Read data from serial port
    // lpBuffer :               The pointer value of the buffer that holds the data to write to the device.
    // nNumberOfBytesToRead :   The size of the buffer pointed to by lpBuffer in bytes
    // lpNumberOfBytesRead :    Returns the number of bytes actually read (in case of None Overrapped)
    // lpOverlapped :           Point value of OVERLAPPED structure for asynchronous
    [DllImport("kernel32.dll")] public static extern uint AxsPortReadFile(int nPortNo, IntPtr lpBuffer, uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, ref OVERLAPPED lpOverlapped);
    // Returns the result of the serial port overlapped operation
    // lpOverlapped->hEvent :       The event handle to be signaled after the transfer is complete. Set this value before using the AxsPortWriteFile, AxsPortReadFile function.
    // lpNumberOfBytesTransferred:  Variable pointer to get the actual byte size of the transfer
    // bWait:                       Determines the processing when the I / O operation is not completed
    //      [0]: Wait for I / O operation to finish
    //      [1]: Returns even if the I / O operation is not completed
    [DllImport("AXL.dll")] public static extern uint AxsPortGetOverlappedResult(int nPortNo, ref OVERLAPPED lpOverlapped, out uint lpNumberOfBytesTransferred, bool bWait);

    // Returns the last error code on the serial port
    // [  0]ERROR_SUCCESS           No error
    // [  2]ERROR_FILE_NOT_FOUND    Invalid communication port
    // [  5]ERROR_ACCESS_DENIED     When the communication port is in use
    // [995]ERROR_OPERATION_ABORTED The I/O operation has been aborted because of either a thread exit or an application request.
    // [996]ERROR_IO_INCOMPLETE     In Overrapped mode, if the write operation is not completed or a timeout occurs
    // [997]ERROR_IO_PENDING        I / O operation is in progress when in overrapped mode
    [DllImport("AXL.dll")] public static extern uint AxsPortGetLastError(ref uint dwpErrCode);
    [DllImport("AXL.dll")] public static extern uint AxsPortSetLastError(uint dwErrCode);

}
