
using System.IO.Compression;
 
var os = new FileStream(DateTime.UtcNow.ToString("yyyyMMdd")+".zip", FileMode.OpenOrCreate);
var zip = new ZipArchive(os, ZipArchiveMode.Create,false);
var dir=@"C:\Users\Administrator\Desktop\代码\脚本";
var files=Directory.GetFiles(dir,"*.csx");
foreach (var item in files)
{
    zip.CreateEntryFromFile(item, item.Substring(dir.Length + 1).Replace('\\', '/'));
}
zip.Dispose();

// dotnet script 压缩脚本.csx
// dotnet script 压缩脚本.csx -c release
