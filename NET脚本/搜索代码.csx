using System.Text.RegularExpressions;

//var dir = @"C:\Users\Administrator\Desktop\Resources";
var dir = @"D:\资源\精选\视频";

var files = Directory.GetFiles(dir, "*.srt", SearchOption.AllDirectories);
var ls = new List<string>();
foreach (var element in files)
{
    // if (File.ReadAllText(element).IndexOf(@"-video_size") != -1)
    // {
    //     ls.Add(element);
    // }RegexOptions.IngoreCase
    if (Regex.IsMatch(File.ReadAllText(element),"Healing brush",RegexOptions.IgnoreCase))
    {
        ls.Add(element);
    }
}
File.WriteAllLines(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "files.txt"), ls);

// dotnet script 搜索代码.csx
// dotnet script 搜索代码.csx -c release