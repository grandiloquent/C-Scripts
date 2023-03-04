
#load "Shared.csx"
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
var str = GetVideoUri("AA18cGNj");

static string GetVideoUri(string str)
{
    try
    {
        
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://assets.msn.com/content/view/v2/Detail/en-us/" +
                                                               str);
        req.UserAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36";
        var res = (HttpWebResponse)req.GetResponse();
        var reader = new StreamReader(res.GetResponseStream());
        var obj = JsonDocument.Parse(reader.ReadToEnd());
        var videoMetadata = obj.RootElement.GetProperty("videoMetadata");
        var externalVideoFiles = videoMetadata.GetProperty("externalVideoFiles");
        var uri = string.Empty;

        for (var i = 0; i < externalVideoFiles.GetArrayLength(); i++)
        {
            JsonElement size;
            JsonElement width;
            if (externalVideoFiles[i].TryGetProperty("fileSize", out size) &&
                externalVideoFiles[i].TryGetProperty("width", out width))
            {
                // if (size.GetInt32() > lastSize)
                // { ;
                //     lastSize = size.GetInt32();
                // }
                if (width.GetInt32() == 1280)
                {
                    uri = externalVideoFiles[i].GetProperty("url").GetString();
                    break;
                }
            }
        }

        return uri;
    }
    catch (System.Exception e)
    {
        Console.WriteLine(e.Message);
    }

    return null;
}

if (str == null) return;
var file = $"{@"C:\Users\Administrator\Desktop\代码\语音".GetFileNameSequence()}.mp4";

var client = new HttpClientDownloadWithProgress(str, file);
client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
{
    Console.CursorLeft = 0;
    Console.Write($"[{progressPercentage}%] ({totalBytesDownloaded.GetHumanReadableFileSize()}/{GetHumanReadableFileSize(totalFileSize ?? 0)})");

    //Console.WriteLine($"{progressPercentage}% ({totalBytesDownloaded}/{totalFileSize})");
};

await client.StartDownload();
public class HttpClientDownloadWithProgress : IDisposable
{
    private readonly string _downloadUrl;
    private readonly string _destinationFilePath;

    private HttpClient _httpClient;

    public delegate void ProgressChangedHandler(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage);

    public event ProgressChangedHandler ProgressChanged;

    public HttpClientDownloadWithProgress(string downloadUrl, string destinationFilePath)
    {
        _downloadUrl = downloadUrl;
        _destinationFilePath = destinationFilePath;
    }

    public async Task StartDownload()
    {
        _httpClient = new HttpClient(new HttpClientHandler
        {
            Proxy = null,
            UseProxy = false
        })
        {

            Timeout = TimeSpan.FromDays(1)
        };
        if (File.Exists(_destinationFilePath))
            _httpClient.DefaultRequestHeaders.Range = new RangeHeaderValue(new FileInfo(_destinationFilePath).Length, null);

        using (var response = await _httpClient.GetAsync(_downloadUrl, HttpCompletionOption.ResponseHeadersRead))
        {
            foreach (var item in response.Headers)
            {

                Console.WriteLine($"{item.Key}: {string.Join(";", item.Value)}");
            }
            await DownloadFileFromHttpResponseMessage(response);

        }
    }

    private async Task DownloadFileFromHttpResponseMessage(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();

        var totalBytes = response.Content.Headers.ContentLength;

        using (var contentStream = await response.Content.ReadAsStreamAsync())
            await ProcessContentStream(totalBytes, contentStream);
    }

    private async Task ProcessContentStream(long? totalDownloadSize, Stream contentStream)
    {
        var totalBytesRead = 0L;
        var readCount = 0L;
        var buffer = new byte[8192];
        var isMoreToRead = true;

        using (var fileStream = new FileStream(_destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
        {
            do
            {
                var bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    isMoreToRead = false;
                    TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                    continue;
                }

                await fileStream.WriteAsync(buffer, 0, bytesRead);

                totalBytesRead += bytesRead;
                readCount += 1;

                if (readCount % 100 == 0)
                    TriggerProgressChanged(totalDownloadSize, totalBytesRead);
            }
            while (isMoreToRead);
        }
    }

    private void TriggerProgressChanged(long? totalDownloadSize, long totalBytesRead)
    {
        if (ProgressChanged == null)
            return;

        double? progressPercentage = null;
        if (totalDownloadSize.HasValue)
            progressPercentage = Math.Round((double)totalBytesRead / totalDownloadSize.Value * 100, 2);

        ProgressChanged(totalDownloadSize, totalBytesRead, progressPercentage);
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}


/*
dotnet script 下载MSN视频.csx
*/
