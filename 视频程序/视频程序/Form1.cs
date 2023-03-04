namespace 视频程序;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
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
                }
            }
        };
    }
}