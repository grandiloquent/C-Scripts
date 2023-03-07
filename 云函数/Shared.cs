using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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

    public static string GetDesktopFileName(this string fileName)
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
    }
    public static void CreateDirectoryIfNotExists(this string path)
    {
        if (Directory.Exists(path))
            return;
        Directory.CreateDirectory(path);
    }
    public static string GetHashForString(this string str)
    {
        // https://github.com/aspnet/Mvc/blob/master/src/Microsoft.AspNetCore.Mvc.Razor/Infrastructure/DefaultFileVersionProvider.cs
        using (var sha256 = new SHA256CryptoServiceProvider())
        {
            var hash = sha256.ComputeHash(new UTF8Encoding(false).GetBytes(str));
            return Base64UrlEncode(hash);
        }
    }
    private static string Base64UrlEncode(byte[] input)
    {
        return Base64UrlEncode(input, offset: 0, count: input.Length);
    }

    private static string Base64UrlEncode(byte[] input, int offset, int count)
    {
        // Special-case empty input
        if (count == 0)
        {
            return string.Empty;
        }

        var buffer = new char[GetArraySizeRequiredToEncode(count)];
        var numBase64Chars = Base64UrlEncode(input, offset, buffer, outputOffset: 0, count: count);
        return new String(buffer, startIndex: 0, length: numBase64Chars);
    }

    private static int GetArraySizeRequiredToEncode(int count)
    {
        var numWholeOrPartialInputBlocks = checked(count + 2) / 3;
        return checked(numWholeOrPartialInputBlocks * 4);
    }

    private static int Base64UrlEncode(byte[] input, int offset, char[] output, int outputOffset, int count)
    {
        var arraySizeRequired = GetArraySizeRequiredToEncode(count);
        // Special-case empty input.
        if (count == 0)
        {
            return 0;
        }

        // Use base64url encoding with no padding characters. See RFC 4648, Sec. 5.
        // Start with default Base64 encoding.
        var numBase64Chars = Convert.ToBase64CharArray(input, offset, count, output, outputOffset);
        // Fix up '+' -> '-' and '/' -> '_'. Drop padding characters.
        for (var i = outputOffset; i - outputOffset < numBase64Chars; i++)
        {
            var ch = output[i];
            if (ch == '+')
            {
                output[i] = '-';
            }
            else if (ch == '/')
            {
                output[i] = '_';
            }
            else if (ch == '=')
            {
                // We've reached a padding character; truncate the remainder.
                return i - outputOffset;
            }
        }

        return numBase64Chars;
    }
}