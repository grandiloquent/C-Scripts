using Renci.SshNet;

var lines = File.ReadAllLines("key.txt");

//DownloadNginx(lines[1],lines[2],lines[3],lines[0]);
static void DownloadNginx(string host, string username, string password, string dir)
{
    using var sftpClient = new SftpClient(host, username, password);
    sftpClient.Connect();
    var f = "/etc/nginx/nginx.conf";
    using var o = new FileStream(Path.Combine(dir, Path.GetFileName(f)), FileMode.OpenOrCreate);
    sftpClient.DownloadFile(f, o);
}

static void ListDirectory(string host, string username, string password, string dir)
{
    using var ftp = new SftpClient(host, username, password);
    ftp.Connect();
    ftp.ListDirectory(dir).OrderBy(x => x.LastWriteTimeUtc).ToList().ForEach(x =>
    {
        if (x.IsRegularFile)
        {
            Console.WriteLine($"{x.Name}");
        }
    });
}

static void UpLoadFile(string host, string username, string password, string dir)
{
    using var sftpClient = new SftpClient(host, username, password);
    sftpClient.Connect();
    var source = @"C:\Users\Administrator\Desktop\9431200_lucidu.cn_nginx\9431200_lucidu.cn.key";
    var destination = "/etc/nginx/5311019_lucidu.cn.key";
    using var o = new FileStream(source, FileMode.OpenOrCreate);
    sftpClient.UploadFile(o, destination);
    using var sshClient = new SshClient(host, username, password);
    sshClient.Connect();
    sshClient.RunCommand("nginx -s reload");
}

UpLoadFile(lines[1], lines[2], lines[3], lines[0]);
;
//ListDirectory(lines[1], lines[2], lines[3], "/root/bin/static/");
/*
dotnet publish -r win-x64 --self-contained false -o C:\Users\Administrator\Desktop\应用\应用 -p:PublishSingleFile=true,AssemblyName=服务器
*/