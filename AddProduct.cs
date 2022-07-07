using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode;

namespace RaktarKezeloDiploma
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-96C041G\SQLEXPRESS;Initial Catalog=Storage;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                //SqlCommand cmd = new SqlCommand("INSERT INTO Product VALUES (2,'Nike Mercurial', 3, 'comfy',1,2);",conn);
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Product VALUES (" + textID.Text + ",'" + textTitle.Text + "'," + textQuantity.Text + ",'"
                    + richDesc.Text + "'," + textPrice.Text + "," + comboCategory.SelectedValue + "," + comboBrand.SelectedValue + ")", conn);
                int rows = cmd.ExecuteNonQuery();
                //
                QrCodeEncodingOptions options = new QrCodeEncodingOptions();
                options = new QrCodeEncodingOptions
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = 300,
                    Height = 300,
                };
                var writer = new BarcodeWriter();
                writer.Format = BarcodeFormat.QR_CODE;
                writer.Options = options;
                //qr generalasa textID mezobol
                var qr = new ZXing.BarcodeWriter();
                qr.Options = options;
                qr.Format = ZXing.BarcodeFormat.QR_CODE;
                var result = new Bitmap(qr.Write(textID.Text.Trim()));
                //var result = new Bitmap(qr.Write("2"));
                pictureBox1.Image = result;
                //Save qr code
                string dir = @"D:\Diplomamunka\QRCODES\products";
                var dialog = new SaveFileDialog();
                dialog.Filter = "PNG(.png)|*.png| JPEG(.jpg)|*.jpg| BMP(.bmp)|*.bmp| GIF(.gif)|*.gif";
                dialog.InitialDirectory = dir;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(dialog.FileName);
                }
                SqlCommand cmdPosition = new SqlCommand(@"INSERT INTO ProductPosition Values(" + textID.Text + ",'"
                    + comboColumn.SelectedItem + "','" + comboRow.SelectedItem + "','" + comboSector.SelectedItem + "'," + textID.Text + ")", conn);
                int query = cmdPosition.ExecuteNonQuery();
                if (rows > 0 && query > 0) MessageBox.Show("Sikeresen hozzáadva az adatbázishoz!");
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
        public void FillCategory()
        {
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT CategoryID,CategoryName from Category ORDER BY CategoryID ASC", conn);
                SqlDataReader reader = comm.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                comboCategory.ValueMember = "CategoryID";
                comboCategory.DisplayMember = "CategoryName";
                comboCategory.DataSource = dt;
                comboCategory.SelectedIndex = 0;
                reader.Close();
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
        public void FillBrand()
        {
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT BrandID,BrandName,BrandTypeID from Brand ORDER BY BrandID ASC", conn);
                SqlDataReader reader = comm.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                comboBrand.ValueMember = "BrandID";
                comboBrand.DisplayMember = "BrandName";
                comboBrand.DataSource = dt;
                comboBrand.SelectedIndex = 0;
                reader.Close();
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
        public void FillRow()
        {
            comboRow.Items.Add(1);
            comboRow.Items.Add(2);
            comboRow.Items.Add(3);
            comboRow.Items.Add(4);
            comboRow.Items.Add(5);
            comboRow.SelectedIndex = 0;
        }
        public void FillColumn()
        {
            for (int i = 1; i < 6; i++)
            {
                comboColumn.Items.Add(i);
            }
            
            comboColumn.SelectedIndex = 0;
        }
        public void FillSector()
        {
            comboSector.Items.Add('A');
            comboSector.Items.Add('B');
            comboSector.Items.Add('C');
            comboSector.Items.Add('D');
            comboSector.SelectedIndex = 0;
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            FillCategory();
            FillBrand();
            FillRow();
            FillColumn();
            FillSector();
        }
    }
}