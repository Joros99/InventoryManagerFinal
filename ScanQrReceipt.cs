using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZXing;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Data.SqlClient;

namespace RaktarKezeloDiploma
{
    public partial class ScanQrReceipt : Form
    {
        ListReceipts listReceipts = new ListReceipts();
        public static string code = "";
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCapture;
        protected SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-96C041G\SQLEXPRESS;Initial Catalog=Storage;Integrated Security=True");
        public ScanQrReceipt(ListReceipts _listreceipt)
        {
            this.listReceipts = _listreceipt;
            InitializeComponent();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            videoCapture = new VideoCaptureDevice(filterInfoCollection[comboDevices.SelectedIndex].MonikerString);
            videoCapture.NewFrame += VideoCapture_NewFrame;
            videoCapture.Start();
            timer1.Start();
        }
        private void VideoCapture_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pb_Camera.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void ScanQrReceipt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(videoCapture.IsRunning)
            videoCapture.SignalToStop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pb_Camera.Image != null)
            {
                BarcodeReader barcode = new BarcodeReader();
                Result result = barcode.Decode((Bitmap)pb_Camera.Image);
                if (result != null)
                {
                    timer1.Stop();
                    videoCapture.SignalToStop();
                    code = result.ToString();
                    listReceipts.searchByQRCode(code);
                    this.Close();
                }
            }
        }

        private void ScanQrReceipt_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                comboDevices.Items.Add(filterInfo.Name);

            comboDevices.SelectedIndex = 0;
        }
    }
}
