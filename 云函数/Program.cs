using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using HtmlAgilityPack;
using Renci.SshNet;

var lines = File.ReadAllLines("key.txt");
var directory = lines[0];
PublishPages3(directory);
UploadStaticFiles(lines[1],lines[2],lines[3],directory + @"\static\resource");
Process.Start(new ProcessStartInfo
{
    FileName = "cmd",
    Arguments = string.Format(
        "/K go run main.go"),
    WorkingDirectory = directory,
});

 
var euphoria = "miniprogram".GetDesktopFileName();
euphoria.CreateDirectoryIfNotExists();
var p = Process.Start(new ProcessStartInfo
{
    FileName = "cmd",
    Arguments = string.Format(
        "/C set GOOS=linux&&set CGO_ENABLED=0&&go build -o \"{0}\" main.go", Path.Combine(euphoria, "main")),
    WorkingDirectory = directory
});
p.WaitForExit();

static void PublishPages3(String directory)
{
    if (Directory.Exists(directory + @"\static\resource"))
        Directory.Delete(directory + @"\static\resource", true);

    var dir = directory + @"\static";


    var files = Directory.GetFiles(dir, "*.html", SearchOption.AllDirectories);

    var buf = new StringBuilder();
    /*
     const string dir = @"C:\Users\Administrator\go\src\awesomeProject";
    var files = new [] {
        @"static\wechat\admin\users.html",
        @"static\wechat\admin\lesson.html",
    };
    
    const string dir = @"C:\Users\Administrator\go\src\psycho";
    var files = new [] {
        @"static\x\x.html",
        @"static\videos\videos.html",
    };
     */

    Process.GetProcesses()
        .Where(x => x.ProcessName == "main" || x.ProcessName == "cmd")
        .ToList().ForEach(x => x.Kill());


    foreach (var element in files)
    {
        var sb = new StringBuilder();
        var subfix = element.Contains(@"\admin\") ? "1" : "3";
        var prefix = element.Contains(@"\admin\") ? "admin." : "";
        buf.AppendFormat(@"
case ""{0}{1}"":
w.Write(templates.{2}Data{3});
break
", prefix, Path.GetFileNameWithoutExtension(element), Path.GetFileNameWithoutExtension(element).Capitalize(), subfix);

        sb.AppendLine(CreateGzipPage(element, subfix));
        (directory + @"\templates").CreateDirectoryIfNotExists();
        var f = directory + @"\templates\" + Path.GetFileNameWithoutExtension(element) + subfix + ".go";

        //(dir + @"\templates").CreateDirectoryIfNotExists();
        File.WriteAllText(f, string.Format(@"package templates
			
			{0}
", sb));
    }
}

static string CreateGzipPage(string fullName, string suffix)
{
    var hd = new HtmlAgilityPack.HtmlDocument();
    hd.LoadHtml(File.ReadAllText(fullName));
    var sb = new StringBuilder();
    var styles = hd.DocumentNode.SelectNodes("//style");
    if (styles != null && styles.Any())
    {
        foreach (var element in styles)
        {
            sb.AppendLine(element.InnerText);
            element.Remove();
        }
    }

    var dir = Path.GetDirectoryName(fullName);
    var links = hd.DocumentNode.SelectNodes(
        "//link[not(starts-with(@href,'https://') or starts-with(@href,'http://'))]");
    if (links != null && links.Any())
    {
        foreach (var element in links)
        {
            sb.AppendLine(File.ReadAllText(Path.Combine(dir, element.GetAttributeValue("href", ""))));
            element.Remove();
        }
    }

    var htmlNode2 = hd.DocumentNode.SelectSingleNode("//head");
    var style = hd.CreateElement("link");
    style.SetAttributeValue("rel", "stylesheet");
    var f = sb.ToString().GetHashForString() + ".css";
    var fd = fullName.SubstringBefore(@"\static\") + @"\static\resource";
    if (!Directory.Exists(fd))
    {
        Directory.CreateDirectory(fd);
    }

    File.WriteAllText(Path.Combine(fd, f), sb.ToString());
    style.SetAttributeValue("href", "https://static.lucidu.cn/" + f);

    htmlNode2.AppendChild(style);
    sb.Clear();
    var scriptss = hd.DocumentNode.SelectNodes("//script[not(@src)]");
    if (scriptss != null && scriptss.Any())
    {
        foreach (var element in scriptss)
        {
            sb.AppendLine(element.InnerText);
            element.Remove();
        }
    }

    var scripts =
        hd.DocumentNode.SelectNodes("//script[not(starts-with(@src,'https://') or starts-with(@src,'http://'))]");
    if (scripts != null && scripts.Any())
    {
        foreach (var element in scripts)
        {
            sb.AppendLine(File.ReadAllText(Path.Combine(dir, element.GetAttributeValue("src", ""))));
            element.Remove();
        }
    }

    var script = hd.CreateElement("script");
    f = sb.ToString().GetHashForString() + ".js";
    File.WriteAllText(Path.Combine(fd, f), sb.ToString());
    script.SetAttributeValue("src", "https://static.lucidu.cn/" + f);
    //script.AppendChild(hd.CreateTextNode(sb.ToString()));
    hd.DocumentNode.SelectSingleNode("//body").AppendChild(script);
    //if (fullName.EndsWith("videos.html"))
    //	File.WriteAllText("1.txt".GetDesktopFileName(), hd.DocumentNode.OuterHtml);
    var buf = new UTF8Encoding(false).GetBytes(hd.DocumentNode.OuterHtml);
    //if (fullName.EndsWith("y.html") || fullName.EndsWith("x.html") || fullName.EndsWith("videos.html")) {
    var ms = new MemoryStream();
    var z = new GZipStream(ms, CompressionMode.Compress, true);
    z.Write(buf, 0, buf.Length);
    z.Dispose();
    var s = string.Join(",", ms.ToArray().Select(i => i.ToString()));
    return string.Format("var {0}Data{2} =[]byte{{{1}}}", Path.GetFileNameWithoutExtension(fullName).Capitalize(), s,
        suffix);

    
}

static void UploadStaticFiles(string host,string username,string password,string dir )
{
    using var ftp = new SftpClient(host, username, password);
    ftp.Connect();
    var files = Directory.GetFiles(dir);
    var s = new MemoryStream();
    var zip = new ZipArchive(s, ZipArchiveMode.Create, true);
    foreach (var element in files)
    {
        Debug.WriteLine(element);
        var demoFile = zip.CreateEntry(Path.GetFileName(element));
        using var entryStream = demoFile.Open();
        using var streamWriter = new StreamWriter(entryStream);
        using var fileToCompressStream = new MemoryStream(File.ReadAllBytes(element));
        fileToCompressStream.CopyTo(entryStream);

    }

    s.Flush();
    zip.Dispose();
    s.Seek(0, SeekOrigin.Begin);
    ftp.UploadFile(s, "/root/static.zip");
    using var sshClient = new SshClient(host, username, password);
    sshClient.Connect();
    Debug.WriteLine(sshClient.RunCommand("unzip -o  /root/static.zip -d /root/bin/static").Result);
 
}

		
/*
dotnet publish -r win-x64 --self-contained false -o C:\Users\Administrator\Desktop\应用\应用 -p:PublishSingleFile=true,AssemblyName=云函数
*/