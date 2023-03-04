
#load "Shared.csx"

var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
DateTime.UtcNow.ToString("yyyyMMdd")
);
if (!Directory.Exists(dir))
    Directory.CreateDirectory(dir);
// dotnet script 创建桌面文件夹.csx
// dotnet script 创建桌面文件夹.csx -c release
