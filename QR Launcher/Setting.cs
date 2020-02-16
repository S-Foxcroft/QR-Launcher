using AForge.Video;
using AForge.Video.DirectShow;
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
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            filters = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo device in filters)
            {
                CameraDropdown.Items.Add(device.Name + "{"+device.MonikerString+"}");
            }
            if (cam != null) {
                    if(cam.IsRunning) cam.Stop();
                    cam.NewFrame += FrameTick;
                    cam.NewFrame -= Core.IncomingFrame;
                    cam.Start();
                string target = Prefs.GetPref("camera","default");
                for (int i = 0; i < CameraDropdown.Items.Count; i++)
                    if ((string)CameraDropdown.Items[i] == target)
                    {
                        Lock = true;
                        CameraDropdown.SelectedIndex = i;
                        Lock = false;
                    }
            }
        }

        private void CameraDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Lock) return;
            if(cam != null && cam.IsRunning) cam.Stop();
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
            e.Cancel = true;
            if (cam != null)
            {
                cam.Stop();
                cam.NewFrame -= FrameTick;
                cam.NewFrame += Core.IncomingFrame;
                cam.Start();
            }
            Hide();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cam != null)
            {
                cam.Stop();
                cam.NewFrame -= FrameTick;
                cam.NewFrame += Core.IncomingFrame;
                cam.Start();
            }
            TaskBox.Instance.Show();
            Hide();
        }
    }
}
