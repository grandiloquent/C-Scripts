using System.Linq;

var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
DateTime.UtcNow.ToString("yyyy年MM月dd日")
);
if (!Directory.Exists(dir))
    Directory.CreateDirectory(dir);
var f = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

if (!Directory.Exists(f)) return;
var files = Directory.GetFiles(f);
files.AsParallel().ForAll(x =>
{
    var ext = Path.GetExtension(x);
    if (string.IsNullOrWhiteSpace(ext))
    {
        ext = ".Unknown";
    }
    var t = Path.Combine(dir, ext.ToUpper());
    if (!Directory.Exists(t)) Directory.CreateDirectory(t);
    var j = Path.Combine(t, Path.GetFileName(x));
    while (File.Exists(j))
    {
        var date = DateTime.Now;
        var dateString = $"{date.Year}-{date.Month.ToString().PadLeft(2, '0')}-{date.Day.ToString().PadLeft(2, '0')} {date.Hour.ToString().PadLeft(2, '0')}-{date.Minute.ToString().PadLeft(2, '0')}-{date.Second.ToString().PadLeft(2, '0')}";
        j = Path.Combine(t, $"{Path.GetFileNameWithoutExtension(x)} {dateString}{Path.GetExtension(x)}");
    }

    try
    {
        File.Move(x, j);
    }
    catch
    {

    }
});
// https://github.com/dotnet-script/dotnet-script
// https://github.com/dotnet-script/dotnet-script/issues/378
// dotnet script 整理桌面.csx
// dotnet script publish 整理桌面.csx -r win10-x64
// dotnet script build 整理桌面.csx -r win10-x64
