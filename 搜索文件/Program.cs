using System.Text.RegularExpressions;

var dir = @"C:\Users\Administrator\Desktop\Resources";

var files = Directory.GetFiles(dir, "*.cs", SearchOption.AllDirectories);
var ls = new List<string>();
foreach (var element in files)
{
	if (File.ReadAllText(element).IndexOf(@"DownloadNginx") != -1)
	{
	    ls.Add(element);
	}

}
File.WriteAllLines(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "files.txt"), ls);

		
/*
dotnet publish -r win-x64 --self-contained false -o C:\Users\Administrator\Desktop\应用\应用 -p:PublishSingleFile=true,AssemblyName=搜索文件
*/