using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QR_Launcher
{
    public partial class Setting : Form
    {
        FilterInfoCollection filters;
        private bool Lock = false;
        public static VideoCaptureDevice cam;
        public static Setting Instance;
        public Setting()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.qr;
            using (RegistryKey startup = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                checkBox1.Checked = (startup.GetValue("QRLauncher") != null);
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            filters = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo device in filters)
            {
                CameraDropdown.Items.Add(device.Name + "{"+device.MonikerString+"}");
            }
            if (cam != null) {
                string target = Prefs.GetPref("camera","default");
                for (int i = 0; i < CameraDropdown.Items.Count; i++)
                    if ((string)CameraDropdown.Items[i] == target)
                    {
                        CameraDropdown.SelectedIndex = i;
                        break;
                    }
            }
        }

        private void CameraDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Lock) return;
            if (cam != null)
            {
                cam.SignalToStop();
                cam.WaitForStop();
            }
            cam = new VideoCaptureDevice(filters[CameraDropdown.SelectedIndex].MonikerString);
            Prefs.SetCamera(filters[CameraDropdown.SelectedIndex]);
            cam.NewFrame += FrameTick;
            cam.Start();
        }
        public void FrameTick(object sender, NewFrameEventArgs e)
        {
            CameraPreview.Image = (Bitmap)e.Frame.Clone();
        }

        private void Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            restoreCamera();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            restoreCamera();
            TaskBox.Instance = new TaskBox();
            TaskBox.Instance.Show();
            Close();
        }
        private void restoreCamera()
        {
            if (cam != null)
            {
                cam.SignalToStop();
                cam.WaitForStop();
                cam = new VideoCaptureDevice(filters[CameraDropdown.SelectedIndex].MonikerString);
                cam.NewFrame += Core.IncomingFrame;
                cam.Start();
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            using (RegistryKey startup = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            if (checkBox1.Checked)
            {
                    //create shortcut
                    startup.SetValue("QRLauncher", Application.ExecutablePath);
            }
            else
            {
                    //remove shortcut
                    startup.DeleteValue("QRLauncher");
            }
        }
    }
}
