using AForge.Video;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ZXing;

namespace QR_Launcher
{
    public partial class Core : Form
    {
        public Core()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.qr;
            Notify.Icon = Properties.Resources.qr;
            Setting.Instance = new Setting();
            TaskBox.Instance = new TaskBox();
            br = new BarcodeReader();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit the program.
            //Release the camera
            //Make sure we save the pref files
            //Then leave
            Prefs.Save();
            if(Setting.cam.IsRunning) Setting.cam.Stop();
            Application.Exit();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting.Instance.Show();
        }
        static string LastRead = "";
        static Bitmap frame;
        public static void IncomingFrame(object sender, NewFrameEventArgs e)
        {
            frame = (Bitmap)e.Frame.Clone();
        }
        private static void RunTask(string s)
        {
            List<string> tasks = Prefs.GetTask(s);
            ProcessStartInfo psi = new ProcessStartInfo();
            int pos;
            foreach (string task in tasks)
            {
                if((pos = task.IndexOf(" ")) > 0)
                {
                    psi.FileName = task.Substring(0, pos);
                    psi.Arguments = task.Substring(pos+1);
                }
                else psi.FileName = task;
                new Process() { StartInfo = psi }.Start();
            }
        }
        BarcodeReader br;
        private void DoTick(object sender, EventArgs e) {
            //do analysis here and run the tasks.
            if(frame != null)
            {
                Result read = br.Decode(frame);
                string package = read.ToString();
                if (package != null && package.StartsWith("QRL.") && package != LastRead)
                {
                    LastRead = package;
                    RunTask(package.Substring(4));
                }
                else LastRead = null;
            }
        }

        private void AddNewTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskBox.Instance.Show();
        }
    }
}
