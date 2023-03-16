
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Windows
{
	public static class User32
	{
		public const int SW_HIDE = 0;
		public const int SW_SHOW = 5;
		public const int SW_SHOWNORMAL = 1;
		public const int SW_SHOWMAXIMIZED = 3;
		public const int SW_RESTORE = 9;
		[DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr hWnd);
		[DllImport("user32.dll")] public static extern bool AllowSetForegroundWindow(uint dwProcessId);
		[DllImport("user32.dll")] public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	}
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			User32.AllowSetForegroundWindow((uint)Process.GetCurrentProcess().Id);
			User32.SetForegroundWindow(Handle);
			User32.ShowWindow(Handle, User32.SW_SHOWNORMAL); 

			TopLevel = true;
			TopMost = true;
			var fileName = Path.Combine(Path.GetDirectoryName(
				              System.Reflection.Assembly.GetEntryAssembly().Location
			              ), "notes.txt");
			if (File.Exists(fileName)) {
				textBox1.Text = File.ReadAllText(fileName);
			}
		}
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			var fileName = Path.Combine(Path.GetDirectoryName(
				            System.Reflection.Assembly.GetEntryAssembly().Location
			            ), "notes.txt");
		
				
			File.WriteAllText(fileName, textBox1.Text);
		
		}

		private void textBox1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F1)
			{
				if (textBox1.TextLength > 0)
				{
					var start = textBox1.SelectionStart;
					var end = textBox1.SelectionStart + textBox1.SelectionLength;
					if (start == textBox1.TextLength)
					{
						start--;
					}
					while (start>0 && !(char.IsWhiteSpace(textBox1.Text[start])))
					{ 
						start--;
					}
					while (end+1<textBox1.Text.Length&& !(char.IsWhiteSpace(textBox1.Text[end+1])))
					{
						end++;
					}

					if (end == textBox1.TextLength)
					{
						end--;
					}
					Clipboard.SetText(textBox1.Text.Substring(start,end-start+1));
				}
			
			}
		}
	}
}
