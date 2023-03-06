using System.Diagnostics;
using System.Text.RegularExpressions;

static string GetDir()
{
    var dir = $"{DateTime.Now:yyyyMMdd}".GetDesktopPath();
    if (!Directory.Exists(dir))
    {
        Directory.CreateDirectory(dir);
    }

    /*
     var i = 1;
    var dir = $"{DateTime.Now:yyyyMMdd}{i.ToString().PadLeft(2, '0')}".GetDesktopPath();
    while (Directory.Exists(dir))
    {
        i++;
        dir = $"{DateTime.Now:yyyyMMdd}{i.ToString().PadLeft(2, '0')}".GetDesktopPath();
    }

     */
    return dir;
}


 static void MakeVideo(string s)
    {
        var file = Directory.GetFiles(GetDir(), "*.mp4").First();
if (string.IsNullOrEmpty(file))
{
    Console.WriteLine("[错误]请复制文件后再试");
    return;
}

Console.WriteLine($"[信息]处理视频文件：{file}");
var dir = Path.GetDirectoryName(file);
Console.WriteLine($"[信息]工作目录：{dir}");

var lines = Regex.Matches(s, "(\\d{2}):(\\d{2}):(\\d{2}):(\\d{2}) - ", RegexOptions.Multiline)
    .Cast<Match>();
var list = new List<string>();
var times = new List<double>();
var filesv = new List<string>();
var numbers = new List<string>();
for (int i = 0; i < lines.Count(); i++)
{
    Console.WriteLine(lines.ElementAt(i));
    var time = Math.Round((
        int.Parse(lines.ElementAt(i).Groups[1].Value)
        * 3600 +
        int.Parse(lines.ElementAt(i).Groups[2].Value) * 60 +
        int.Parse(lines.ElementAt(i).Groups[3].Value) +
        int.Parse(lines.ElementAt(i).Groups[4].Value) * 1.0 / 29.97
    ) * 1000, 2);
    times.Add((time / 1000));

    //list.Add(string.Format(@"ffmpeg -i {0}.mp4 -i {1}.mp3 -filter_complex ""[0:a]volume=1[a0]; [1:a]volume=1.98,adelay={3}|{3}[a1]; [a0][a1]amix=inputs=2[a]"" -map 0:v -map ""[a]"" -c:v copy -c:a mp3 {2}.mp4 -y", i + 1, i + 1, i + 2, time));
    //list.Add(string.Format(@"ffmpeg -i {0}.mp4 -i {1}.mp3 -filter_complex ""[0:a]volume=0.5[a0]; [1:a]volume=1.98,adelay={3}|{3}[a1]; [a0][a1]amix=inputs=2[a]"" -map 0:v -map ""[a]"" -c:v copy -c:a mp3 {2}.mp4 -y", i + 1, i + 1, i + 2, time));


    // volume=1.98,|{time}
    list.Add(@$"[{i + 1}:a]adelay={time}|{time}:all=1[a{i + 1}]");
    //list.Add(string.Format(@"ffmpeg -i {0}.mp4 -i {1}.mp3 -filter_complex ""[0:a]volume=1.98[a0]; [1:a]volume=1.98,adelay={3}|{3}[a1]; [a0][a1]amix=inputs=2[a]"" -map 0:v -map ""[a]"" -c:v copy -c:a mp3 {2}.mp4 -y", i + 1, i + 1, i + 2, time));

    filesv.Add($"-i {i + 1}.mp3");
    numbers.Add($"[a{i}]");
}

/*
ffmpeg -i 1.mp4 {string.Join(" ",files)} -filter_complex "[0:a]volume=1[a0]; [1:a]volume=1.98,adelay=734.07|734.07[a1];[2:a]volume=1.98,adelay=29900.9|29900.9[a2];[a0][a1][a2]amix=inputs=3[a]" -map 0:v -map "[a]" -c:v copy -c:a mp3 2.mp4 -y
*/
var files = Directory.GetFiles(dir, "*.mp3")
    .OrderBy(x => int.Parse(Path.GetFileNameWithoutExtension(x))).ToList();
var argList = new List<string>();

for (int i = 0; i < files.Count(); i++)
{
    var tfile = TagLib.File.Create(files[i]);
    TimeSpan duration = tfile.Properties.Duration;
    argList.Add($"between(t,{times[i]},{times[i] + Math.Round(duration.TotalSeconds, 2)})");
}

var arguments =
    $@"-y -i ""{file}"" -af volume=0.1:enable='{string.Join("+", argList)}' 1.mp4";
Console.WriteLine($"[信息]执行命令：ffmpeg {arguments}");
// https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo?view=net-7.0
var p = Process.Start(new ProcessStartInfo()
{
    FileName = "ffmpeg",
    Arguments = arguments,
    WorkingDirectory = dir,
    UseShellExecute = false,
    CreateNoWindow = true
});
p.WaitForExit();
arguments =
    $@" -i 1.mp4 {string.Join(" ", filesv)} -filter_complex ""[0:a]volume=1[a0];{string.Join(";", list)};{string.Join("", numbers)}[a{numbers.Count}]amix=inputs={numbers.Count + 1}:normalize=0[a]"" -map 0:v -map ""[a]"" -c:v copy -c:a mp3 3.mp4 -y";
Console.WriteLine($"[信息]执行命令：ffmpeg {arguments}");
// https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo?view=net-7.0
p = Process.Start(new ProcessStartInfo()
{
    FileName = "ffmpeg",
    Arguments = arguments,
    WorkingDirectory = dir,
    UseShellExecute = false,
    CreateNoWindow = true
});
p.WaitForExit();
    }

MakeVideo(ClipboardShare.GetText().Trim());
/*
dotnet publish -r win-x64 --self-contained false -o C:\Users\Administrator\Desktop\应用\应用 -p:PublishSingleFile=true,AssemblyName=视频合成
*/