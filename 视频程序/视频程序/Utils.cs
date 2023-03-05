using System.Net.Http.Headers;

namespace 视频程序;

using System.Net;
using System.Text;
using System.Text.Json;

public static class Utils
{
    public static String TranslateChinese(string s)
    {
        //string q
        // http://translate.google.com/translate_a/single?client=gtx&sl=auto&tl=%s&dt=t&dt=bd&ie=UTF-8&oe=UTF-8&dj=1&source=icon&q=
        // en
        var req = WebRequest.Create(
            "http://translate.google.com/translate_a/single?client=gtx&sl=auto&tl=zh&dt=t&dt=bd&ie=UTF-8&oe=UTF-8&dj=1&source=icon&q=" +
            WebUtility.UrlEncode(s));
        var res = req.GetResponse();
        using (var reader = new StreamReader(res.GetResponseStream()))
        {
            var obj =
                (JsonElement)JsonSerializer.Deserialize<Dictionary<String, dynamic>>(reader.ReadToEnd())["sentences"];
            var sb = new StringBuilder();
            for (int i = 0; i < obj.GetArrayLength(); i++)
            {
                sb.Append(obj[i].GetProperty("trans").GetString()).Append(' ');
            }

            // Regex.Replace(sb.ToString().Trim(), "[ ](?=[a-zA-Z0-9])", m => "_").ToLower();
            // std::string {0}(){{\n}}
            //return string.Format("{0}", Regex.Replace(sb.ToString().Trim(), " ([a-zA-Z0-9])", m => m.Groups[1].Value.ToUpper()).Decapitalize());
            return sb.ToString().Trim();
            /*
			 sb.ToString().Trim();
			 */
            // ClipboardShare.SetText(sb.ToString().Trim().Camel().Decapitalize());
        }
        //Clipboard.SetText(string.Format(@"{0}", TransAPI.Translate(Clipboard.GetText())));
    }

    private static async Task DownloadMSNVideo(string id, string destinationFilePath)
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://assets.msn.com/content/view/v2/Detail/en-us/" +
                                                               id);
        req.UserAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36";
        var res = (HttpWebResponse)req.GetResponse();
        var reader = new StreamReader(res.GetResponseStream());
        var obj = JsonDocument.Parse(reader.ReadToEnd());
        var videoMetadata = obj.RootElement.GetProperty("videoMetadata");
        var externalVideoFiles = videoMetadata.GetProperty("externalVideoFiles");
        var downloadUrl = string.Empty;

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
                    downloadUrl = externalVideoFiles[i].GetProperty("url").GetString();
                    break;
                }
            }
        }

        var httpClient = new HttpClient(new HttpClientHandler
        {
            Proxy = null,
            UseProxy = false
        })
        {
            Timeout = TimeSpan.FromDays(1)
        };
        if (File.Exists(destinationFilePath))
            httpClient.DefaultRequestHeaders.Range =
                new RangeHeaderValue(new FileInfo(destinationFilePath).Length, null);

        using var response = await httpClient.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead);
        /*foreach (var item in response.Headers)
            {

                Console.WriteLine($"{item.Key}: {string.Join(";", item.Value)}");
            }*/
        response.EnsureSuccessStatusCode();

        var totalBytes = response.Content.Headers.ContentLength;

        await using var contentStream = await response.Content.ReadAsStreamAsync();
        await ProcessContentStream(destinationFilePath, totalBytes, contentStream);
    }

    private static async Task ProcessContentStream(string destinationFilePath, long? totalDownloadSize,
        Stream contentStream)
    {
        var totalBytesRead = 0L;
        var readCount = 0L;
        var buffer = new byte[8192];
        var isMoreToRead = true;

        await using var fileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write,
            FileShare.None, 8192, true);
        do
        {
            var bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0)
            {
                isMoreToRead = false;
                //TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                continue;
            }

            await fileStream.WriteAsync(buffer, 0, bytesRead);

            totalBytesRead += bytesRead;
            readCount += 1;

            //if (readCount % 100 == 0)
            //  TriggerProgressChanged(totalDownloadSize, totalBytesRead);
        } while (isMoreToRead);
    }

    public static async Task DownloadMsnVideo()
    {
        var dir = $"{DateTime.Now:yyyyMMdd}".GetDesktopPath();
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        var video = Path.Combine(dir, "0.mp4");
        var id = Clipboard.GetText();
        await DownloadMSNVideo(id, video);
    }
}