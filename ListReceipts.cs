using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RaktarKezeloDiploma
{
    public partial class ListReceipts : Form
    {
        public ListReceipts()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-96C041G\SQLEXPRESS;Initial Catalog=Storage;Integrated Security=True");
        DataTable dt = new DataTable();
        int index;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string type = "";
            if (textName.Text.Length!=0 && !maskedFrom.MaskFull && !maskedUntil.MaskFull && textCode.Text.Length == 0)
            {
                type = "nev";
            }
            else if (textName.Text.Length == 0 && maskedFrom.MaskFull && maskedUntil.MaskFull && textCode.Text.Length == 0)
            {
                type = "evek";
            }
            else if (textName.Text.Length != 0 && maskedFrom.MaskFull && maskedUntil.MaskFull && textCode.Text.Length == 0)
            {
                type = "nev-evek";
            }
            else if (textName.Text.Length == 0 && !maskedFrom.MaskFull && !maskedUntil.MaskFull && textCode.Text.Length != 0)
            {
                type = "kod";
            }
            try
            {
                conn.Open();
                switch (type)
                {
                    case "nev":
                        dt.Columns.Clear();
                        dt.Rows.Clear();
                        SqlCommand nameCommand = new SqlCommand("SELECT OrderID AS 'Azonosító', OrderLastName + ' ' + OrderFirstName As 'A megrendelő teljes neve', OrderPostal AS 'Irányítószám', OrderCity AS 'Helység', OrderAddress AS 'Cím', OrderPhoneNumber AS 'Telefonszám' FROM Orders WHERE OrderFirstName LIKE '%" + textName.Text + "' OR OrderLastName LIKE '%" + textName.Text + "'",conn);
                        SqlDataAdapter adapter = new SqlDataAdapter(nameCommand);
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        break;
                    case "evek":
                        dt.Columns.Clear();
                        dt.Rows.Clear();
                        SqlCommand yearCommand = new SqlCommand("SELECT OrderID as 'Azonosító', OrderLastName + ' ' + OrderFirstName As 'A megrendelő teljes neve', OrderPostal AS 'Irányítószám', OrderCity AS 'Helység', OrderAddress AS 'Cím', OrderPhoneNumber AS 'Telefonszám' FROM Orders WHERE OrderDate BETWEEN '" + maskedFrom.Text + "' AND '" + maskedUntil.Text + "'" , conn);
                        SqlDataAdapter yearAdapter = new SqlDataAdapter(yearCommand);
                        yearAdapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        break;
                    case "nev-evek":
                        dt.Columns.Clear();
                        dt.Rows.Clear();
                        SqlCommand nameYearCommand = new SqlCommand("SELECT OrderID as 'Azonosító', OrderLastName + ' ' + OrderFirstName As 'A megrendelő teljes neve', OrderPostal AS 'Irányítószám', OrderCity AS 'Helység', OrderAddress AS 'Cím', OrderPhoneNumber AS 'Telefonszám' FROM Orders WHERE (OrderFirstName LIKE '%" + textName.Text + "' OR OrderLastName LIKE '%" + textName.Text + "') AND (OrderDate BETWEEN '" + maskedFrom.Text + "' AND '" + maskedUntil.Text + "')", conn);
                        SqlDataAdapter nameYearAdapter = new SqlDataAdapter(nameYearCommand);
                        nameYearAdapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        break;
                    case "kod":
                        dt.Columns.Clear();
                        dt.Rows.Clear();
                        SqlCommand codeCommand = new SqlCommand("SELECT OrderID as 'Azonosító', OrderLastName + ' ' + OrderFirstName As 'A megrendelő teljes neve', OrderPostal AS 'Irányítószám', OrderCity AS 'Helység', OrderAddress AS 'Cím', OrderPhoneNumber AS 'Telefonszám' FROM Orders WHERE OrderID=" + textCode.Text, conn);
                        SqlDataAdapter codeAdapter = new SqlDataAdapter(codeCommand);
                        codeAdapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        break;
                    default:
                        MessageBox.Show("Keresési módszerek:\n -Keresés név vagy vezetéknév alapján\n -Keresés egy adott perióduson belül\n -Keresés sorszám alapján");
                        break;
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

        private void btn_Content_Click(object sender, EventArgs e)
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ProductTitle AS 'A termék megnevezése',Quantity AS 'Darabszám' FROM OrderProduct op join Product p on op.ProductID=p.ProductID WHERE op.OrderID=" + index ,conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
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

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                index = (int)row.Cells[0].Value;
            }
        }

        private void btn_byQR_Click(object sender, EventArgs e)
        {
            ScanQrReceipt scanQrReceipt = new ScanQrReceipt(this);
            scanQrReceipt.ShowDialog();
        }
        public void searchByQRCode(string code) {
            dt.Columns.Clear();
            dt.Rows.Clear();
            SqlCommand QRCommand = new SqlCommand("SELECT OrderID as 'Azonosító', OrderLastName + ' ' + OrderFirstName As 'A megrendelő teljes neve', OrderPostal AS 'Irányítószám', OrderCity AS 'Helység', OrderAddress AS 'Cím', OrderPhoneNumber AS 'Telefonszám' FROM Orders WHERE OrderID=" + ScanQrReceipt.code, conn);
            SqlDataAdapter QRAdapter = new SqlDataAdapter(QRCommand);
            QRAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
