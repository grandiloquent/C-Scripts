
using System.Diagnostics;

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

        var source = @"C:\Users\Administrator\Desktop\代码\脚本\视频程序\视频程序\Utils.cs";
        if(!File.Exists(source))
            File.WriteAllText(source,string.Empty);
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
                    case Key.NumPad9:
                        Process.Start(@"C:\Program Files\Adobe\Adobe After Effects 2023\Support Files\AfterFX.exe");
                        break;
                    case Key.F4:
                        textBox1.SelectedText = $"{textBox1.SelectedText}{Environment.NewLine}{Environment.NewLine}{Utils.TranslateChinese(textBox1.SelectedText)}";
                        break;
                    case Key.F10:
                        Utils.DownloadMsnVideo();
                        break;
                }
            }
        };
    }
}