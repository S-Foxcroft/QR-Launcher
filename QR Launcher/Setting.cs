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
        }

        private void CameraDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cam != null && cam.IsRunning) cam.Stop();
            cam = new VideoCaptureDevice(filters[CameraDropdown.SelectedIndex].MonikerString);
            cam.NewFrame -= Core.IncomingFrame;
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
            cam.Stop();
            cam.NewFrame -= FrameTick;
            cam.NewFrame += Core.IncomingFrame;
            cam.Start();
            Hide();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
