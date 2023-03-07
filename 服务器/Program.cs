using Renci.SshNet;

var lines = File.ReadAllLines("key.txt");

DownloadNginx(lines[1],lines[2],lines[3]);
static void DownloadNginx(string host,string username,string password)
{
	
	using var sftpClient = new SftpClient(host, username, password);
	sftpClient.Connect();
	var f = "/etc/nginx/nginx.conf";
	var dir = @"C:\Users\Administrator\Desktop\应用";
	using var o = new FileStream(Path.Combine(dir, Path.GetFileName(f)), FileMode.OpenOrCreate);
	sftpClient.DownloadFile(f, o);
}
		
/*
dotnet publish -r win-x64 --self-contained false -o C:\Users\Administrator\Desktop\应用\应用 -p:PublishSingleFile=true,AssemblyName=服务器
*/