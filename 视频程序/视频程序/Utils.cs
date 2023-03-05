using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text.RegularExpressions;

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


/*
dotnet publish -r win-x64 --self-contained false -o C:\Users\Administrator\Desktop\代码\应用 -p:PublishSingleFile=true,AssemblyName=时间戳文本语音合成
*/
    async static Task GetAudio(ClientWebSocket connection, string fileName, string ttsText, string voice, string style)
    {
        var requestId = Guid.NewGuid().ToString("N").ToUpper();
        var timestamp = DateTime.Now.ToUniversalTime().ToString("yyy-MM-dd'T'HH:mm:ss.fff'Z'");
        await SendTextContent(connection, $@"Path: speech.config
X-RequestId: {requestId}
X-Timestamp: {timestamp}
Content-Type: application/json

{{""context"":{{""system"":{{""name"":""SpeechSDK"",""version"":""1.19.0"",""build"":""JavaScript"",""lang"":""JavaScript""}},""os"":{{""platform"":""Browser/Win32"",""name"":""Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36"",""version"":""5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.69 Safari/537.36""}}}}}}");

        await SendTextContent(connection, $@"Path: synthesis.context
X-RequestId: {requestId}
X-Timestamp: {timestamp}
Content-Type: application/json

{{""synthesis"":{{""audio"":{{""metadataOptions"":{{""bookmarkEnabled"":false,""sentenceBoundaryEnabled"":false,""visemeEnabled"":false,""wordBoundaryEnabled"":false}},""outputFormat"":""audio-24khz-96kbitrate-mono-mp3""}},""language"":{{""autoDetection"":false}}}}}}");
        //await SendSSML(connection, requestId, timestamp, ttsText);

        await SendTextContent(connection,
            // Xiaoxiao
            // Yunxi
            string.Format(@"Path: ssml
X-RequestId: {0}
X-Timestamp: {1}
Content-Type: application/ssml+xml

<speak xmlns=""http://www.w3.org/2001/10/synthesis"" xmlns:mstts=""http://www.w3.org/2001/mstts"" xmlns:emo=""http://www.w3.org/2009/10/emotionml"" version=""1.0"" xml:lang=""en-US""><voice name=""zh-CN-{3}Neural""><mstts:express-as style=""{4}"" ><prosody rate=""0%"" pitch=""0%"">{2}</prosody></mstts:express-as></voice></speak>"
                , requestId, DateTime.Now.ToUniversalTime().ToString("yyy-MM-dd'T'HH:mm:ss.fff'Z'"), ttsText, voice,
                style)
        );
        var data = await ReceiveAudio(connection);
        File.WriteAllBytes(fileName, data);
    }

    async static Task MsTts(string ttsText, string voice, string style)
    {
        var dir = Directory.GetCurrentDirectory();
        CreateDirectoryIfNotExists(dir);
        var i = 1;
        try
        {
            i = Directory.GetFiles(dir, "*.mp3")
                .Where(x => Regex.IsMatch(Path.GetFileNameWithoutExtension(x), "\\d+"))
                .Max(x => int.Parse(Regex.Match(Path.GetFileNameWithoutExtension(x), "\\d+").Value)) + 1;
        }
        catch
        {
            // ignored
        }


        // ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
        // | SecurityProtocolType.Tls
        // | SecurityProtocolType.Tls11
        // | SecurityProtocolType.Tls12;

        /*
         
          "westus",
                 "westus2",
                 "southcentralus",
                 "westcentralus",
                 "eastus",
                 "eastus2",
                 "westeurope",
                 "northeurope",
                 "southbrazil",
                 "eastaustralia",
                 "southeastasia",
                 "eastasia"
         */
        const string url =
            "wss://eastus.api.speech.microsoft.com/cognitiveservices/websocket/v1?TrafficType=AzureDemo&Authorization=bearer%20undefined&X-ConnectionId=";
        var connectionId = Guid.NewGuid().ToString("N").ToUpper();
        var uriBuilder = new UriBuilder(url + connectionId);

        var connection = new ClientWebSocket();
        connection.Options.Proxy = new WebProxy("127.0.0.1", 10809);
        //connection.Options.SetRequestHeader("Accept-Encoding", "gzip");
        connection.Options.SetRequestHeader("Origin", "https://azure.microsoft.com");

        //connection.Options.SetRequestHeader("User-Agent","Mozilla/5.0 (Linux; Android 7.1.2; M2012K11AC Build/N6F26Q; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/81.0.4044.117 Mobile Safari/537.36");
        await connection.ConnectAsync(uriBuilder.Uri, CancellationToken.None);

        var lines = Regex.Split(ttsText, "\\d{2}:\\d{2}:\\d{2}:\\d{2} - \\d{2}:\\d{2}:\\d{2}:\\d{2}")
            .Where(x => !string.IsNullOrWhiteSpace(x));
        foreach (var t in lines)
        {
            Console.WriteLine(t);
            var fileName = Path.Combine(dir, i + ".mp3");
            await GetAudio(connection, fileName, t.Trim(), voice, style);
            i++;
        }

        await connection.CloseAsync(WebSocketCloseStatus.NormalClosure, "close", CancellationToken.None);
    }

    static void CreateDirectoryIfNotExists(string path)
    {
        if (Directory.Exists(path))
            return;
        Directory.CreateDirectory(path);
    }

    async static Task SendTextContent(ClientWebSocket connection, String text)
    {
        var content = System.Text.Encoding.UTF8.GetBytes(text);
        var data = new System.ArraySegment<byte>(content);
        await connection.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
    }

    async static Task<byte[]> ReceiveAudio(ClientWebSocket connection)
    {
        var audioData = new List<byte>();
        var readingAudio = false;

        var audioSize = 0;
        var audioSizeRecord = new List<int>();

        while (true)
        {
            var rawData = new List<byte>();
            var sizeRecord = new List<int>();
            var receivedSize = 0;

            WebSocketReceiveResult receiveResult;
            while (true)
            {
                var buffer = new ArraySegment<byte>(new byte[1024]);
                receiveResult = await connection.ReceiveAsync(buffer, CancellationToken.None);

                var dataSegment = new ArraySegment<byte>(buffer.Array, buffer.Offset, receiveResult.Count);
                rawData.AddRange(dataSegment);

                receivedSize += receiveResult.Count;
                sizeRecord.Add(receiveResult.Count);

                if (receiveResult.EndOfMessage)
                    break;
            }
            //Console.WriteLine((receivedSize == rawData.Count).ToString());

            if (receiveResult.MessageType == WebSocketMessageType.Binary)
            {
                audioData.AddRange(rawData.Skip(
                    new UTF8Encoding(false).GetString(rawData.ToArray()).IndexOf("Path:audio") +
                    12));
                if (!readingAudio)
                    readingAudio = true;

                audioSize += receivedSize;
                audioSizeRecord.Add(receivedSize);
            }
            else if (receiveResult.MessageType == WebSocketMessageType.Text && readingAudio)
            {
                var textData = Encoding.UTF8.GetChars(rawData.ToArray());
                var text = new string(textData);
                if (text.Contains("Path:turn.end"))
                    break;
            }
            else if (receiveResult.MessageType == WebSocketMessageType.Close)
            {
                break;
            }
        }
        //Console.WriteLine("TotalSize: " + (audioData.Count == audioSize).ToString());

        return audioData.ToArray();
    }
}