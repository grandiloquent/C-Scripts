
var dir = @"C:\Users\Administrator\Desktop\Resources";

var files = Directory.GetFiles(dir, "*.cs", SearchOption.AllDirectories);
var ls = new List<string>();
foreach (var element in files)
{
    if (File.ReadAllText(element).IndexOf(@"-video_size") != -1)
    {
        ls.Add(element);
    }
}
File.WriteAllLines(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "files.txt"), ls);

// dotnet script 搜索代码.csx
// dotnet script 搜索代码.csx -c release