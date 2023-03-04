
namespace 视频程序;

using System.Net;
using System.Text;
using System.Text.Json;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        var f = "note.txt".GetEntryPath();
        if (File.Exists(f))
        {
            textBox1.Text = File.ReadAllText(f);
        }

        this.Load += OnLoad;
        this.FormClosing += ((sender, args) =>
        {
            if(!string.IsNullOrWhiteSpace(textBox1.Text))
            File.WriteAllText(f, textBox1.Text);
        });
    }

    void OnLoad(object? sender, EventArgs args)
    {
        var kbh = new KeyboardShare();
        kbh.ConfigHook();

        kbh.KeyDown += (s, k) =>
        {
            if (KeyboardShare.isKeyPressed((int)Key.Menu))
            {
                switch (k.Key)
                {
                    case Key.Key0:
                        break;
                }
            }
            else
            {
                switch (k.Key)
                {
                    case Key.Key0:
                        break;
                    case Key.F4:
                        textBox1.SelectedText = $"{textBox1.SelectedText}{Environment.NewLine}{Environment.NewLine}{TranslateChinese(textBox1.SelectedText)}";
                        break;
                }
            }
        };
    }
    public static String TranslateChinese(string s)
    {
        //string q
        // http://translate.google.com/translate_a/single?client=gtx&sl=auto&tl=%s&dt=t&dt=bd&ie=UTF-8&oe=UTF-8&dj=1&source=icon&q=
        // en
        var req = WebRequest.Create(
                      "http://translate.google.com/translate_a/single?client=gtx&sl=auto&tl=zh&dt=t&dt=bd&ie=UTF-8&oe=UTF-8&dj=1&source=icon&q=" +WebUtility.UrlEncode(s));
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
}