using System;
using System.Windows.Forms;

namespace RaktarKezeloDiploma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ScanQRCode scan = new ScanQRCode();
            scan.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AddProduct add = new AddProduct();
            add.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Receipt receipt = new Receipt();
            receipt.ShowDialog();
        }
    }
}
