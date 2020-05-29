using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ZXing;

namespace QR_Launcher
{
    public partial class Core : Form
    {
        public static bool Running = false;
        public Core()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.qr;
            Notify.Icon = Properties.Resources.qr;
            br = new BarcodeReader();
            Prefs.Load();
            string s = Prefs.GetPref("camera", "NIL");
            if (s != "NIL")
            {
                FilterInfoCollection filters = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                int i = 0;
                foreach (FilterInfo device in filters)
                {
                    if (s == device.Name + "{" + device.MonikerString + "}")
                    {
                        Setting.cam = new VideoCaptureDevice(device.MonikerString);
                        break;
                    }
                    i++;
                }
            }
            Hide();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ticker.Stop();
            Running = false;
            if (Setting.cam != null)
            {
                Setting.cam.SignalToStop();
                Setting.cam.WaitForStop();
            }
            Prefs.Save();
            Setting.Instance.Close();
            Close();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting.Instance = new Setting();
            Setting.Instance.Show();
        }
        static string LastRead = "";
        static Bitmap frame;
        public static void IncomingFrame(object sender, NewFrameEventArgs e)
        {
            if(Running) frame = (Bitmap)e.Frame.Clone();
        }
        private static void RunTask(string s)
        {
            List<string> tasks = Prefs.GetTask(s);
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd",
                CreateNoWindow = true
            };
            foreach (string S in tasks)
            {
                string task = S;
                foreach (KeyValuePair<string, string> row in Prefs.Replacements) task = task.Replace("{"+row.Key+"}",row.Value);
                byte[] data = Encoding.UTF8.GetBytes(task);
                psi.Arguments = "/C start wrap "+Convert.ToBase64String(data);
                new Process() { StartInfo = psi }.Start();
            }
        }
        BarcodeReader br;
        int ticksSinceLast = 0;
        private void DoTick(object sender, EventArgs e) {
            //do analysis here and run the tasks.
            if (!Running) return;
            ticksSinceLast++;
            if(frame != null)
            {
                Result read = br.Decode(frame);
                string package = read==null ? "NIL" : read.ToString();
                if (package != "NIL" && package.StartsWith("QRL."))
                {
                    if (package != LastRead || ticksSinceLast > 500)
                    {
                        LastRead = package;
                        ticksSinceLast = 0;
                        RunTask(package.Substring(4));
                    }
                }
            }
        }

        private void AddNewTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskBox.Instance = new TaskBox();
            TaskBox.Instance.Show();
        }

        private void Core_Load(object sender, EventArgs e)
        {
            if(Setting.cam != null)
            {
                Setting.cam.NewFrame += IncomingFrame;
                Setting.cam.Start();
            }
            Running = true;
        }

        private void Notify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SettingsToolStripMenuItem_Click(sender, null);
        }
    }
}
