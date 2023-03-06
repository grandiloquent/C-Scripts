
#load "Shared.csx"

var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
DateTime.Now.ToString("yyyyMMdd")
);
if (!Directory.Exists(dir))
    Directory.CreateDirectory(dir);
var autoit=Path.Combine(dir,"AutoIt");
if(!Directory.Exists(autoit))
Directory.CreateDirectory(autoit);
for(var i=0;i<10;i++){
    var sub=Path.Combine(dir,(i+1).ToString().PadLeft(3,'0'));
    if(!Directory.Exists(sub))
Directory.CreateDirectory(sub);
}
for(var i=0;i<100;i++){
    File.WriteAllText(Path.Combine(autoit,(i+1).ToString().PadLeft(3,'0')+".au3"),string.Empty);
}
// dotnet script 创建桌面文件夹.csx
// dotnet script 创建桌面文件夹.csx -c release
