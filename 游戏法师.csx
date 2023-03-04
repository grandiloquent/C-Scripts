
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;

[StructLayout(LayoutKind.Sequential)]
public struct MOUSEINPUT
{
    public int dx;
    public int dy;
    public int mouseData;
    public int dwFlags;
    public int time;
    public int dwExtraInfo;
};

[StructLayout(LayoutKind.Sequential)]
public struct KEYBDINPUT
{
    public short wVk;
    public short wScan;
    public int dwFlags;
    public int time;
    public int dwExtraInfo;
};

[StructLayout(LayoutKind.Sequential)]
public struct HARDWAREINPUT
{
    public int uMsg;
    public short wParamL;
    public short wParamH;
};

[StructLayout(LayoutKind.Explicit)]
public struct INPUT
{
    [FieldOffset(0)]
    public int type;
    [FieldOffset(4)]
    public MOUSEINPUT no;
    [FieldOffset(4)]
    public KEYBDINPUT ki;
    [FieldOffset(4)]
    public HARDWAREINPUT hi;
};
[DllImport("user32.dll")]
public static extern void SendInput(int nInputs, ref INPUT pInputs, int cbsize);
const int INPUT_KEYBOARD = 1;
const int KEYEVENTF_KEYDOWN = 0x0;
const int KEYEVENTF_KEYUP = 0x2;
const int KEYEVENTF_EXTENDEDKEY = 0x1;
public static void SendKey(int key, bool isExtend = false)
{

    INPUT input = GetKeyDownInput(key, isExtend);
    SendInput(1, ref input, Marshal.SizeOf(input));
    Thread.Sleep(100); // wait
    input = GetKeyUpInput(input, isExtend);
    SendInput(1, ref input, Marshal.SizeOf(input));
}
static INPUT GetKeyUpInput(INPUT input, bool isExtend)
{
    input.ki.dwFlags = ((isExtend) ? KEYEVENTF_EXTENDEDKEY : 0x0) | KEYEVENTF_KEYUP;
    return input;
}
static INPUT GetKeyDownInput(int key, bool isExtend)
{
    return new INPUT
    {
        type = INPUT_KEYBOARD,
        ki = new KEYBDINPUT()
        {
            wVk = (short)key,
            wScan = 0,
            dwFlags = ((isExtend) ? KEYEVENTF_EXTENDEDKEY : 0x0) | KEYEVENTF_KEYDOWN,
            time = 0,
            dwExtraInfo = 0
        },
    };
}
public delegate uint THREAD_START_ROUTINE(IntPtr lpThreadParameter);
// https://github.com/Wiladams/TOAPI/blob/70c0dd060970853efda5e6d02ed0951571dfa9ec/TOAPI.Kernel32/Kernel32_Thread.cs
// https://github.com/NgonPhi/BTL-OS/blob/b98adafd6e05ec044c1f5896336de52d61075fff/Main/Main/ThreadM.cs
// https://github.com/VanessaNiculae/Thread-App/blob/30e840b311e24f007f34245c1a2d797a255ca0e7/lab5/WinApiClass.cs
public static class Kernel32
{
    // AttachThreadInput
    // CreateRemoteThread

    // CreateThread
    [DllImportAttribute("kernel32.dll", EntryPoint = "CreateThread")]
    public static extern IntPtr CreateThread(IntPtr lpThreadAttributes,
        uint dwStackSize, THREAD_START_ROUTINE lpStartAddress,
        IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    // ExitThread
    [DllImportAttribute("kernel32.dll", EntryPoint = "ExitThread")]
    public static extern void ExitThread(uint dwExitCode);

    // GetCurrentThread
    [DllImport("kernel32.dll")]
    public static extern IntPtr GetCurrentThread();

    // GetCurrentThreadId
    [DllImport("kernel32.dll")]
    public static extern uint GetCurrentThreadId();

    // GetExitCodeThread
    // GetThreadId
    [DllImportAttribute("kernel32.dll", EntryPoint = "GetThreadId")]
    public static extern uint GetThreadId(IntPtr Thread);

    // GetThreadIOPendingFlag
    // GetThreadPriority
    // GetThreadPriorityBoost

    // GetThreadTimes

    // OpenThread
    [DllImport("kernel32.dll", EntryPoint = "OpenThread")]
    public static extern IntPtr OpenThread(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
        uint dwThreadId);

    // ResumeThread
    [DllImport("kernel32.dll")]
    public static extern int ResumeThread(IntPtr hThread);

    // SetThreadAffinityMask
    // SetThreadIdealProcessor
    // SetThreadPriority
    // SetThreadPriorityBoost
    // SetThreadStackGuarantee

    // Sleep
    [DllImportAttribute("kernel32.dll", EntryPoint = "Sleep")]
    public static extern void Sleep(uint dwMilliseconds);

    // SleepEx
    [DllImportAttribute("kernel32.dll", EntryPoint = "SleepEx")]
    public static extern uint SleepEx(uint dwMilliseconds, [MarshalAs(UnmanagedType.Bool)] bool bAlertable);

    // SuspendThread
    [DllImport("kernel32.dll")]
    public static extern int SuspendThread(IntPtr hThread);

    // SwithToThread
    // TerminateThread
    // ThreadProc
    // TlsAlloc
    // TlsFree
    // TlsGetValue
    // TlsSetValue
    // WaitForInputIdle

    // Thread Pooling
    // BindIoCompletionCallback
    // QueueUserWorkItem


    [DllImport("kernel32.dll")]
    public static extern uint GetThreadLocale();

    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public extern static bool SetThreadLocale(uint Locale);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern Int32 WaitForSingleObject(SafeWaitHandle hHandle, Int32 dwMilliseconds);

}
// dotnet script 游戏法师.csx
// dotnet script 游戏法师.csx -c release
