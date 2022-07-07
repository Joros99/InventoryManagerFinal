using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ZXing;
using AForge.Video;
using AForge.Video.DirectShow;

namespace RaktarKezeloDiploma
{
    public partial class AddWithQRCODE : Form
    {
        public AddWithQRCODE(Receipt _receipt)
        {
            InitializeComponent();
            this.receipt = _receipt;
        }
        Receipt receipt = new Receipt();
        SqlDataReader reader;
        public static string code = "";
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCapture;
        int price;
        protected SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-96C041G\SQLEXPRESS;Initial Catalog=Storage;Integrated Security=True");
        /*public AddWithQRCODE(List<product_class> allSourcesAsParameter)
        {
            list2 = allSourcesAsParameter;
        }*/
        private void button1_Click(object sender, EventArgs e)
        {
            videoCapture = new VideoCaptureDevice(filterInfoCollection[comboDevices.SelectedIndex].MonikerString);
            videoCapture.NewFrame += VideoCapture_NewFrame;
            videoCapture.Start();
            timer1.Start();
        }
        private void VideoCapture_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        private void AddWithQRCODE_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                comboDevices.Items.Add(filterInfo.Name);

            comboDevices.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                BarcodeReader barcode = new BarcodeReader();
                Result result = barcode.Decode((Bitmap)pictureBox1.Image);
                if (result != null)
                {
                    VoidNumbers();
                    timer1.Stop();
                    videoCapture.SignalToStop();
                    code = result.ToString();
                    getProduct();
                    FillNumber();
                }
            }
        }

        private void AddWithQRCODE_FormClosed(object sender, FormClosedEventArgs e)
        {
            videoCapture.SignalToStop();
        }
        public void FillNumber()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT ProductQuantity from Product WHERE ProductID='" + code + "'", conn);
                int QNUM = (int)cmd.ExecuteScalar();
                for (int i = 1; i < QNUM + 1; i++)
                {
                    comboNumber.Items.Add(i);
                }
                comboNumber.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        public void getProduct()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand comm = new SqlCommand("SELECT ProductTitle,ProductPrice from Product WHERE ProductID='" + code + "'", conn);
                reader = comm.ExecuteReader();
                bool recordFound = reader.Read();
                if (recordFound) {
                    textProduct.Text = reader["ProductTitle"].ToString();
                    price = (int)reader["ProductPrice"];
                    textProduct.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public void VoidNumbers()
        {
            int listNumbers = comboNumber.Items.Count;
            for (int i = 1; i < listNumbers + 1; i++)
            {
                comboNumber.Items.Remove(i);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            product_class product_Class = new product_class();
            product_Class.ID = Convert.ToInt32(code);
            product_Class.Name = textProduct.Text;
            product_Class.Quantity = (int)comboNumber.SelectedItem;
            product_Class.Price = price;
            Receipt.list.Add(product_Class);
            receipt.AddByQR(product_Class);
            this.Close();
        }
    }
}
