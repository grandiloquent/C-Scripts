namespace 视频程序;

using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

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

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern short GetKeyState(int keyCode);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool TranslateMessage([In, Out] ref MSG msg);

    [DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    public static extern int DispatchMessage([In] ref MSG msg);

    [DllImport("user32.dll")]
    public static extern sbyte GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin,
        uint wMsgFilterMax);

    public static bool isKeyPressed(int keyCode)
    {
        return (GetKeyState(keyCode) & 0x8000) != 0;
    }
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

public static class ClipboardShare
{
    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr SetClipboardData(uint uFormat, IntPtr data);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GlobalLock(IntPtr hMem);

    [DllImport("Kernel32.dll", SetLastError = true)]
    static extern int GlobalSize(IntPtr hMem);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GlobalUnlock(IntPtr hMem);

    [DllImport("User32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool IsClipboardFormatAvailable(uint format);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll")]
    static extern bool EmptyClipboard();

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool CloseClipboard();

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    public static extern int DragQueryFile(IntPtr hDrop, int iFile, StringBuilder lpszFile, int cch);

    [DllImport("User32.dll", SetLastError = true)]
    static extern IntPtr GetClipboardData(uint uFormat);

    const uint cfUnicodeText = 13;

    public static void OpenClipboard()
    {
        var num = 10;
        while (true)
        {
            if (OpenClipboard(IntPtr.Zero))
            {
                break;
            }

            if (--num == 0)
            {
                ThrowWin32();
            }

            System.Threading.Thread.Sleep(100);
        }
    }

    public static void SetText(string text)
    {
        OpenClipboard();
        EmptyClipboard();
        IntPtr hGlobal = IntPtr.Zero;
        try
        {
            var bytes = (text.Length + 1) * 2;
            hGlobal = Marshal.AllocHGlobal(bytes);
            if (hGlobal == IntPtr.Zero)
            {
                ThrowWin32();
            }

            var target = GlobalLock(hGlobal);
            if (target == IntPtr.Zero)
            {
                ThrowWin32();
            }

            try
            {
                Marshal.Copy(text.ToCharArray(), 0, target, text.Length);
            }
            finally
            {
                GlobalUnlock(target);
            }

            // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setclipboarddata
            if (SetClipboardData(cfUnicodeText, hGlobal) == IntPtr.Zero)
            {
                ThrowWin32();
            }

            hGlobal = IntPtr.Zero;
        }
        finally
        {
            if (hGlobal != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(hGlobal);
            }

            CloseClipboard();
        }
    }

    // https://github.com/nanoant/ChromeSVG2Clipboard/blob/e135818eb25be5f5f1076a3746b675e9228657d1/ChromeClipboardHost/Program.cs
    static void ThrowWin32()
    {
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    public static string GetText()
    {
        if (!IsClipboardFormatAvailable(cfUnicodeText))
        {
            return null;
        }

        IntPtr handle = IntPtr.Zero;
        IntPtr pointer = IntPtr.Zero;
        try
        {
            OpenClipboard();
            handle = GetClipboardData(cfUnicodeText);
            if (handle == IntPtr.Zero)
            {
                return null;
            }

            pointer = GlobalLock(handle);
            if (pointer == IntPtr.Zero)
            {
                return null;
            }

            var size = GlobalSize(handle);
            var buff = new byte[size];
            Marshal.Copy(pointer, buff, 0, size);
            return Encoding.Unicode.GetString(buff).TrimEnd('\0');
        }
        finally
        {
            if (pointer != IntPtr.Zero)
            {
                GlobalUnlock(handle);
            }

            CloseClipboard();
        }
    }

    public static IEnumerable<string> GetFileNames()
    {
        if (!IsClipboardFormatAvailable(15))
        {
            var n = GetText();
            if (Directory.Exists(n) || File.Exists(n))
            {
                return new string[] { n };
            }

            return null;
        }

        IntPtr handle = IntPtr.Zero;
        try
        {
            OpenClipboard();
            handle = GetClipboardData(15);
            if (handle == IntPtr.Zero)
            {
                return null;
            }

            var count = DragQueryFile(handle, unchecked((int)0xFFFFFFFF), null, 0);
            if (count == 0)
            {
                return Enumerable.Empty<string>();
            }

            var sb = new StringBuilder(260);
            var files = new string[count];
            for (var i = 0; i < count; i++)
            {
                var charlen = DragQueryFile(handle, i, sb, sb.Capacity);
                var s = sb.ToString();
                if (s.Length > charlen)
                {
                    s = s.Substring(0, charlen);
                }

                files[i] = s;
            }

            return files;
        }
        finally
        {
            CloseClipboard();
        }
    }
}

public static class Strings
{
    public const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
    public const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;

    [DllImport("kernel32.dll", EntryPoint = "LCMapStringA")]
    public static extern int LCMapString(int Locale, int dwMapFlags, byte[] lpSrcStr, int cchSrc, byte[] lpDestStr,
        int cchDest);

    private static string Concatenate(this IEnumerable<string> strings,
        Func<StringBuilder, string, StringBuilder> builderFunc)
    {
        return strings.Aggregate(new StringBuilder(), builderFunc).ToString();
    }

    // https://crates.io/crates/convert_case
    public static string Camel(this string value)
    {
        return
            Regex.Replace(
                Regex.Replace(value, "[\\-_ ]+([a-zA-Z])", m => m.Groups[1].Value.ToUpper()),
                "\\s+",
                ""
            );
    }

    public static String Capitalize(this String s)
    {
        if (string.IsNullOrEmpty(s))
            return s;
        if (s.Length == 1)
            return s.ToUpper();
        if (char.IsUpper(s[0]))
            return s;
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public static string ConcatenateLines(this IEnumerable<string> strings)
    {
        return Concatenate(strings, (StringBuilder builder, string nextValue) => builder.AppendLine(nextValue));
    }

    public static string Concatenates(this IEnumerable<string> strings, string separator)
    {
        return Concatenate(strings,
            (StringBuilder builder, string nextValue) => builder.Append(nextValue).Append(separator));
    }

    public static string Concatenates(this IEnumerable<string> strings)
    {
        return Concatenate(strings, (builder, nextValue) => builder.Append(nextValue));
    }

    public static String Decapitalize(this String s)
    {
        if (string.IsNullOrEmpty(s))
            return s;
        if (s.Length == 1)
            return s.ToUpper();
        if (char.IsLower(s[0]))
            return s;
        return char.ToLower(s[0]) + s.Substring(1);
    }

    public static string RemoveWhiteSpaceLines(this string str)
    {
        return string.Join(Environment.NewLine,
            str.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Where(i => !string.IsNullOrWhiteSpace(i)));
    }

    public static string SubstringAfter(this string value, char delimiter)
    {
        var index = value.IndexOf(delimiter);
        if (index == -1)
            return value;
        else
            return value.Substring(index + 1);
    }

    public static string SubstringAfter(this string value, string delimiter)
    {
        var index = value.IndexOf(delimiter);
        if (index == -1)
            return value;
        else
            return value.Substring(index + delimiter.Length);
    }

    public static string SubstringAfterLast(this string value, char delimiter)
    {
        var index = value.LastIndexOf(delimiter);
        if (index == -1)
            return value;
        else
            return value.Substring(index + 1);
    }

    public static string SubstringBefore(this string value, char delimiter)
    {
        var index = value.IndexOf(delimiter);
        if (index == -1)
            return value;
        else
            return value.Substring(0, index);
    }

    public static string SubstringBefore(this string value, string delimiter)
    {
        var index = value.IndexOf(delimiter);
        if (index == -1)
            return value;
        else
            return value.Substring(0, index);
    }

    public static string SubstringBeforeLast(this string value, string delimiter)
    {
        var index = value.LastIndexOf(delimiter);
        if (index == -1)
            return value;
        else
            return value.Substring(0, index);
    }

    public static IEnumerable<string> ToBlocks(this string value)
    {
        var count = 0;
        StringBuilder sb = new StringBuilder();
        List<string> ls = new List<string>();
        foreach (var t in value)
        {
            sb.Append(t);
            switch (t)
            {
                case '{':
                    count++;
                    continue;
                case '}':
                {
                    count--;
                    if (count == 0)
                    {
                        ls.Add(sb.ToString());
                        sb.Clear();
                    }

                    continue;
                }
            }
        }

        return ls;
    }

    //转化方法
    public static string ToTraditional(string source, int type)
    {
        byte[] srcByte2 = Encoding.Default.GetBytes(source);
        byte[] desByte2 = new byte[srcByte2.Length];
        LCMapString(2052, type, srcByte2, -1, desByte2, srcByte2.Length);
        string des2 = Encoding.Default.GetString(desByte2);
        return des2;
    }

    public static string UpperCamel(this string value)
    {
        return value.Camel().Capitalize();
    }

    public static string JavaScriptStringEncode(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        StringBuilder b = null;
        int startIndex = 0;
        int count = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char c = value[i];

            // Append the unhandled characters (that do not require special treament)
            // to the string builder when special characters are detected.
            if (CharRequiresJavaScriptEncoding(c))
            {
                if (b == null)
                    b = new StringBuilder(value.Length + 5);

                if (count > 0)
                {
                    b.Append(value, startIndex, count);
                }

                startIndex = i + 1;
                count = 0;

                switch (c)
                {
                    case '\r':
                        b.Append("\\r");
                        break;
                    case '\t':
                        b.Append("\\t");
                        break;
                    case '\"':
                        b.Append("\\\"");
                        break;
                    case '\\':
                        b.Append("\\\\");
                        break;
                    case '\n':
                        b.Append("\\n");
                        break;
                    case '\b':
                        b.Append("\\b");
                        break;
                    case '\f':
                        b.Append("\\f");
                        break;
                    default:
                        AppendCharAsUnicodeJavaScript(b, c);
                        break;
                }
            }
            else
            {
                count++;
            }
        }

        if (b == null)
        {
            return value;
        }

        if (count > 0)
        {
            b.Append(value, startIndex, count);
        }

        return b.ToString();
    }

    private static bool CharRequiresJavaScriptEncoding(char c)
    {
        return
            c < 0x20 // control chars always have to be encoded
            || c == '\"' // chars which must be encoded per JSON spec
            || c == '\\'
            || c == '\'' // HTML-sensitive chars encoded for safety
            || c == '<'
            || c == '>'
            || (c == '&')
            ||
            c == '\u0085' // newline chars (see Unicode 6.2, Table 5-1 [http://www.unicode.org/versions/Unicode6.2.0/ch05.pdf]) have to be encoded
            || c == '\u2028'
            || c == '\u2029';
    }

    private static void AppendCharAsUnicodeJavaScript(StringBuilder builder, char c)
    {
        builder.AppendFormat("\\u{0:x4}", (int)c);
    }

    public static string GetDesktopPath(this string f)
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), f);
    }

    public static string GetEntryPath(this string f)
    {
        var dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        return Path.Combine(dir, f);
    }

    public static int GetFileNameSequence(this string dir)
    {
        var files = Directory.GetFiles(dir);
        var max = 0;
        var regex = new Regex("\\d+");
        foreach (var file in files)
        {
            var match = regex.Match(Path.GetFileNameWithoutExtension(file));
            if (match.Success)
            {
                var t = 0;
                if (int.TryParse(match.Value, out t) && t > max)
                {
                    max = t;
                }
            }
        }
        return max + 1;
    }
}