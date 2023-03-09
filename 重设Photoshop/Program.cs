using System.Diagnostics;
using System.IO.Compression;
foreach (var process in Process.GetProcesses())
{
    if(process.ProcessName=="Photoshop")
        process.Kill();
}
Thread.Sleep(3000);
var file = @"C:\Users\Administrator\Desktop\应用\Adobe Photoshop 2023.zip";
var dir = @"C:\Users\Administrator\AppData\Roaming\Adobe\Adobe Photoshop 2023";
//ZipFile.CreateFromDirectory(dir,file);
Directory.Delete(dir,true);
ZipFile.ExtractToDirectory(file,dir);
/*
dotnet publish -r win-x64 --self-contained false -o C:\Users\Administrator\Desktop\应用\应用 -p:PublishSingleFile=true,AssemblyName=重设Photoshop
*/