using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;


[DllImport("user32.dll")]
static extern void SendInput(int nInputs, ref INPUT pInputs, int cbsize);

const int INPUT_KEYBOARD = 1;
const int KEYEVENTF_KEYDOWN = 0x0;
const int KEYEVENTF_KEYUP = 0x2;
const int KEYEVENTF_EXTENDEDKEY = 0x1;

static void SendKey(int key, bool isExtend = false)
{
    INPUT input = GetKeyDownInput(key, isExtend);

    SendInput(1, ref input, Marshal.SizeOf(input));
    //Thread.Sleep(100); // wait
    input = GetKeyUpInput(input, isExtend);
    SendInput(1, ref input, Marshal.SizeOf(input));
    //Console.WriteLine("{0}:{1}","SendKey",Marshal.SizeOf(input));
}

static INPUT GetKeyUpInput(INPUT input, bool isExtend)
{
    input.ki.dwFlags = ((isExtend) ? KEYEVENTF_EXTENDEDKEY : 0x0) | KEYEVENTF_KEYUP;
    return input;
}

static INPUT GetKeyDownInput(int key, bool isExtend)
{
    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-input
    // https://learn.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-sendinput
    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-keybdinput
    // https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
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

[DllImport("user32.dll", CharSet = CharSet.Auto)]
static extern bool TranslateMessage([In, Out] ref MSG msg);

[DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
static extern int DispatchMessage([In] ref MSG msg);

[DllImport("user32.dll")]
static extern sbyte GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin,
    uint wMsgFilterMax);

[DllImport("user32.dll")]
static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

IntPtr _thread1 = IntPtr.Zero;
bool _threadRun = false;

void FashiStrong()
{
    IntPtr thread = IntPtr.Zero;
    if (_thread1 == IntPtr.Zero)
    {
        _thread1 = Kernel32.CreateThread(IntPtr.Zero, 0, new THREAD_START_ROUTINE((v) =>
        {

            while (true)
            {
                 SendKey(0x32);
               // keybd_event(0x32, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                //keybd_event(0x32, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                Kernel32.Sleep(1000);

                /*
                 int count = 0;
                SendKey(0x32); // 键盘2

                 if (count == 9)
                {
                    count = 0;

                    SendKey(0x31);
                    Kernel32.Sleep(1000);
                    SendKey(0x33);
                    Kernel32.Sleep(1000);
                }

                count++;*/
            }
        }), IntPtr.Zero, 0, thread);
        _threadRun = true;
    }
    else
    {
        if (_threadRun)
        {
            Kernel32.SuspendThread(_thread1);
        }
        else
        {
            Kernel32.ResumeThread(_thread1);
        }

        _threadRun = !_threadRun;
    }
}

var kbh = new KeyboardShare();
kbh.ConfigHook();
kbh.KeyDown += (s, k) =>
{
    switch (k.Key)
    {
        case Key.F9:
            FashiStrong();
            break;
    }
};
MSG message;
while (GetMessage(out message, IntPtr.Zero, 0, 0) != 0)
{
    TranslateMessage(ref message);
    DispatchMessage(ref message);
}

/////////////////

public class KeyboardShare
{
    [DllImport("User32.dll")]
    private static extern IntPtr SetWindowsHookExA(HookID hookID, KeyboardHookProc lpfn, IntPtr hmod,
        int dwThreadId);

    [DllImport("User32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr hook, int code, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    public delegate IntPtr KeyboardHookProc(int code, IntPtr wParam, IntPtr lParam);

    private event KeyboardHookProc keyhookevent;
    private IntPtr hookPtr;

    public KeyboardShare()
    {
        this.keyhookevent += KeyboardHook_keyhookevent;
    }

    private IntPtr KeyboardHook_keyhookevent(int code, IntPtr wParam, IntPtr lParam)
    {
        KeyStaus ks = (KeyStaus)wParam.ToInt32();
        KeyboardHookStruct khs = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
        KeyEvent ke = ks == KeyStaus.KeyDown || ks == KeyStaus.SysKeyDown ? KeyDown : KeyUp;
        if (ke != null)
        {
            ke.Invoke(this, new KeyEventArg()
            {
                Key = khs.Key,
                KeyStaus = ks
            });
        }

        return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
    }

    public void ConfigHook()
    {
        hookPtr = SetWindowsHookExA(HookID.Keyboard_LL, keyhookevent, IntPtr.Zero, 0);
        if (hookPtr == null)
            throw new Exception();
    }

    public delegate void KeyEvent(object sender, KeyEventArg e);

    public event KeyEvent KeyDown;
    public event KeyEvent KeyUp;
}

[StructLayout(LayoutKind.Explicit, Size = 20)]
public struct KeyboardHookStruct
{
    [FieldOffset(0)] public Key Key;
    [FieldOffset(4)] public int ScanCode;
    [FieldOffset(8)] public int Flags;
    [FieldOffset(12)] public int Time;
    [FieldOffset(16)] public IntPtr dwExtraInfo;
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

public enum KeyStaus
{
    KeyDown = 0x0100,
    KeyUp = 0x0101,
    SysKeyDown = 0x0104,
    SysKeyUp = 0x0105
}

public class KeyEventArg
{
    public Key Key;
    public KeyStaus KeyStaus;
}

public enum HookID
{
    Callwndproc = 4,
    Callwndprocert = 12,
    Cbt = 5,
    Debug = 9,
    Foregroundidle = 11,
    GetMessage = 3,
    JournalPlayback = 1,
    JournalRecord = 0,
    Keyboard = 2,
    Keyboard_LL = 13,
    Mouse = 7,
    MouseLL = 14,
    MsgFilter = -1,
    Shell = 10,
    SysmsgFilter = 6
}

public enum Key
{
    LeftButton = 0x01,
    RightButton = 0x02,
    Cancel = 0x03,
    MiddleButton = 0x04,
    XButton1 = 0x05,
    XButton2 = 0x06,
    BackSpace = 0x08,
    Tab = 0x09,
    Clear = 0x0C,
    Return = 0x0D,
    Enter = Return,
    Shift = 0x10,
    Control = 0x11,
    Menu = 0x12,
    Pause = 0x13,
    CapsLock = 0x14,
    IMEKana = 0x15,
    IMEHanguel = IMEKana,
    IMEHangul = IMEKana,
    IMEJunja = 0x17,
    IMEFinal = 0x18,
    IMEHanja = 0x19,
    IMEKanji = IMEHanja,
    Escape = 0x1B,
    IMEConvert = 0x1C,
    IMENonConvvert = 0x1D,
    IMEAccept = 0x1E,
    IMEModeChange = 0x1F,
    SpaceBar = 0x20,
    PageUp = 0x21,
    PageDown = 0x22,
    End = 0x23,
    Home = 0x24,
    Left = 0x25,
    Up = 0x26,
    Right = 0x27,
    Down = 0x28,
    Select = 0x29,
    Print = 0x2A,
    Execute = 0x2B,
    Snapshot = 0x2C,
    Insert = 0x2D,
    Delete = 0x2E,
    Help = 0x2F,
    Key0 = 0x30,
    Key1 = 0x31,
    Key2 = 0x322,
    Key3 = 0x33,
    Key4 = 0x34,
    Key5 = 0x35,
    Key6 = 0x36,
    Key7 = 0x37,
    Key8 = 0x38,
    Key9 = 0x39,
    KeyA = 0x41,
    KeyB = 0x42,
    KeyC = 0x43,
    KeyD = 0x44,
    KeyE = 0x45,
    KeyF = 0x46,
    KeyG = 0x47,
    KeyH = 0x48,
    KeyI = 0x49,
    KeyJ = 0x4A,
    KeyK = 0x4B,
    KeyL = 0x4C,
    KeyM = 0x4D,
    KeyN = 0x4E,
    KeyO = 0x4F,
    KeyP = 0x50,
    KeyQ = 0x51,
    KeyR = 0x52,
    KeyS = 0x53,
    KeyT = 0x54,
    KeyU = 0x55,
    KeyV = 0x56,
    KeyW = 0x57,
    KeyX = 0x58,
    KeyY = 0x59,
    KeyZ = 0x5A,
    LeftWinKey = 0x5B,
    RightWinKey = 0x5C,
    AppsKey = 0x5D,
    Sleep = 0x5F,
    NumPad0 = 0x60,
    NumPad1 = 0x61,
    NumPad2 = 0x62,
    NumPad3 = 0x63,
    NumPad4 = 0x64,
    NumPad5 = 0x65,
    NumPad6 = 0x66,
    NumPad7 = 0x67,
    NumPad8 = 0x68,
    NumPad9 = 0x69,
    Multiply = 0x6A,
    Add = 0x6B,
    Separator = 0x6C,
    Subtract = 0x6D,
    Decimal = 0x6E,
    Divide = 0x6F,
    F1 = 0x70,
    F2 = 0x71,
    F3 = 0x72,
    F4 = 0x73,
    F5 = 0x74,
    F6 = 0x75,
    F7 = 0x76,
    F8 = 0x77,
    F9 = 0x78,
    F10 = 0x79,
    F11 = 0x7A,
    F12 = 0x7B,
    F13 = 0x7C,
    F14 = 0x7D,
    F15 = 0x7E,
    F16 = 0x7F,
    F17 = 0x80,
    F18 = 0x81,
    F19 = 0x82,
    F20 = 0x83,
    F21 = 0x84,
    F22 = 0x85,
    F23 = 0x86,
    F24 = 0x87,
    NumLock = 0x90,
    ScrollLock = 0x91,
    OEM92 = 0x92,
    OEM93 = 0x93,
    OEM94 = 0x94,
    OEM95 = 0x95,
    OEM96 = 0x96,
    LeftShfit = 0xA0,
    RightShfit = 0xA1,
    LeftCtrl = 0xA2,
    RightCtrl = 0xA3,
    LeftMenu = 0xA4,
    RightMenu = 0xA5,
    BrowserBack = 0xA6,
    BrowserForward = 0xA7,
    BrowserRefresh = 0xA8,
    BrowserStop = 0xA9,
    BrowserSearch = 0xAA,
    BrowserFavorites = 0xAB,
    BrowserHome = 0xAC,
    BrowserVolumeMute = 0xAD,
    BrowserVolumeDown = 0xAE,
    BrowserVolumeUp = 0xAF,
    MediaNextTrack = 0xB0,
    MediaPreviousTrack = 0xB1,
    MediaStop = 0xB2,
    MediaPlayPause = 0xB3,
    LaunchMail = 0xB4,
    LaunchMediaSelect = 0xB5,
    LaunchApp1 = 0xB6,
    LaunchApp2 = 0xB7,
    OEM1 = 0xBA,
    OEMPlus = 0xBB,
    OEMComma = 0xBC,
    OEMMinus = 0xBD,
    OEMPeriod = 0xBE,
    OEM2 = 0xBF,
    OEM3 = 0xC0,
    OEM4 = 0xDB,
    OEM5 = 0xDC,
    OEM6 = 0xDD,
    OEM7 = 0xDE,
    OEM8 = 0xDF,
    OEM102 = 0xE2,
    IMEProcess = 0xE5,
    Packet = 0xE7,
    Attn = 0xF6,
    CrSel = 0xF7,
    ExSel = 0xF8,
    EraseEOF = 0xF9,
    Play = 0xFA,
    Zoom = 0xFB,
    PA1 = 0xFD,
    OEMClear = 0xFE
}

[StructLayout(LayoutKind.Sequential)]
public struct MSG
{
    public IntPtr hwnd;
    public int message;
    public IntPtr wParam;
    public IntPtr lParam;
    public int time;
    public int pt_x;
    public int pt_y;
}

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
    [FieldOffset(0)] public int type;
    [FieldOffset(4)] public MOUSEINPUT no;
    [FieldOffset(4)] public KEYBDINPUT ki;
    [FieldOffset(4)] public HARDWAREINPUT hi;
};

// dotnet script 游戏法师.csx
// dotnet script 游戏法师.csx -c release