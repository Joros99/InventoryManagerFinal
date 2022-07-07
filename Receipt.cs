using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Borders;
using ZXing;
using ZXing.QrCode;

namespace RaktarKezeloDiploma
{
    public partial class Receipt : Form
    {
        public Receipt()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-96C041G\SQLEXPRESS;Initial Catalog=Storage;Integrated Security=True");
        DataTable dt = new DataTable();
        public static List<product_class> list = new List<product_class>();
        SqlDataReader reader;
        DataRowView dataRowView;
        string item,payment;
        product_class product_Class;
        int id,quantity,price,counter,sn,sum,paymenttype;
        string ord_fname, ord_lname, city, address, postal, phone;
        public void FillProduct()
        {
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT ProductID,ProductTitle from Product ORDER BY ProductID ASC", conn);
                reader = comm.ExecuteReader();
                dt.Load(reader);
                comboProduct.ValueMember = "ProductID";
                comboProduct.DisplayMember = "ProductTitle";
                comboProduct.DataSource = dt;
                comboProduct.SelectedIndex = 0;
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
        public void FillNumber()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT ProductQuantity from Product WHERE ProductID='" + comboProduct.SelectedValue + "'", conn);
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
        public void VoidNumbers()
        {
            int listNumbers = comboNumber.Items.Count;
            for (int i = 1; i < listNumbers + 1; i++)
            {
                comboNumber.Items.Remove(i);
            }
        }

        private void btnAddByQR_Click(object sender, EventArgs e)
        {
            AddWithQRCODE add = new AddWithQRCODE(this);
            add.ShowDialog();
        }

        private void Receipt_Load(object sender, EventArgs e)
        {
            FillProduct();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListReceipts listReceipts = new ListReceipts();
            listReceipts.ShowDialog();
        }

        private void comboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            VoidNumbers();
            FillNumber();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            ++counter;
            id = (int)comboProduct.SelectedValue;
            quantity = (int)comboNumber.SelectedItem;
            price = getPrice();
            dataRowView = (DataRowView)comboProduct.SelectedItem;
            item = dataRowView["ProductTitle"].ToString();
            product_Class = new product_class(id, item, quantity, price);
            orderBox.Items.Add(counter + ". " + id + " - " + item + " x " + quantity + " = " + price*quantity);
            list.Add(product_Class); 
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            getValueFromList();
            product_class delete = new product_class();
            delete.ID = id;
            orderBox.Items.Remove(orderBox.SelectedItem);
            foreach (product_class item in list.ToList())
            {
                if (item.ID==delete.ID) { 
                    list.Remove(item);
                }
            }
            counter--;
        }

        private void btn_order_Click(object sender, EventArgs e)
        {
            //credentials
            ord_fname = textName.Text;
            ord_lname = textLName.Text;
            city = textCity.Text;
            address = textAddress.Text;
            postal = textPostal.Text;
            phone = textPhone.Text;
            DateTime dateTime = DateTime.Now;
            string filename = "receipt" + ord_fname + ord_lname + dateTime.Year + dateTime.Month + dateTime.Day + ".pdf";
            string path = @"D:\\Diplomamunka\";
            PdfWriter writer = new PdfWriter(path+filename);

            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            LineSeparator ls = new LineSeparator(new SolidLine());
            Image image = new Image(ImageDataFactory.Create(@"D:\\Diplomamunka\logo.png"))
                .SetHeight(200)
                .SetWidth(200);
            
            Table header = new Table(2, false)
                .UseAllAvailableWidth();
            Table infos = new Table(2, false)
                .UseAllAvailableWidth();
            Cell logo = new Cell(1, 1)
                .Add(image)
                .SetBorder(Border.NO_BORDER)
                .SetVerticalAlignment(VerticalAlignment.TOP);
            Cell credentials = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER);
            Cell place = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER);
            Cell bankCred = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            header.AddCell(logo);

            Table dateTable = new Table(1, false);
            Cell receiptCode = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.JUSTIFIED)
                .Add(new Paragraph("A számla kódja: "+ord_fname[0].ToString() + ord_lname[0].ToString() + "-" + dateTime.Month + dateTime.Day +dateTime.Year));
            Cell dateIssued = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.JUSTIFIED)
                .Add(new Paragraph("Dátum: " + dateTime.ToShortDateString()));
            Cell placeIssued = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.JUSTIFIED)
                .Add(new Paragraph("Kiadatási hely: Szabadka"));
            dateTable.AddCell(receiptCode);
            dateTable.AddCell(dateIssued);
            dateTable.AddCell(placeIssued);
            place.Add(dateTable);

            //order info table
            Table table = new Table(2, false);
            Cell NameCell = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetBorderTop(new SolidBorder(2))
                .SetBorderLeft(new SolidBorder(2))
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(ord_fname));
            Cell NameCell2 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetBorderTop(new SolidBorder(2))
                .SetBorderRight(new SolidBorder(2))
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(ord_lname));
            Cell PostalCell = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetBorderLeft(new SolidBorder(2))
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(postal));
            Cell CityCell = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetBorderRight(new SolidBorder(2))
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(city)); 
            Cell AddressCell = new Cell(2, 2)
                .SetBorder(Border.NO_BORDER)
                .SetBorderLeft(new SolidBorder(2))
                .SetBorderRight(new SolidBorder(2))
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(address)); 
            Cell PhoneCell = new Cell(2, 2)
                .SetBorder(Border.NO_BORDER)
                .SetBorderLeft(new SolidBorder(2))
                .SetBorderRight(new SolidBorder(2))
                .SetBorderBottom(new SolidBorder(2))
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(phone));
            table.AddCell(NameCell);
            table.AddCell(NameCell2);
            table.AddCell(PostalCell);
            table.AddCell(CityCell);
            table.AddCell(AddressCell);
            table.AddCell(PhoneCell);

            //jobb felso sarokban levo informaciok
            Table company = new Table(2, false);
            Cell companyName = new Cell(2, 2)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("The Warehouse"))
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBold();
            Cell pib = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("PIB: 112356535"))
                .SetTextAlignment(TextAlignment.RIGHT);
            Cell mb = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("MB: 66047725"))
                .SetTextAlignment(TextAlignment.RIGHT);
            Cell infospace = new Cell(2, 2)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Bankszámla:"))
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBold();
            Cell bank = new Cell(2, 2)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("OTP BANK: 325-9300702053249-75"))
                .SetTextAlignment(TextAlignment.RIGHT);

            company.AddCell(companyName);
            company.AddCell(pib);
            company.AddCell(mb);
            company.AddCell(infospace);
            company.AddCell(bank);

            credentials.Add(table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT));
            bankCred.Add(company.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT));

            infos.AddCell(place);
            infos.AddCell(credentials);
            header.AddCell(bankCred);

            document.Add(header);
            document.Add(ls);
            document.Add(infos);
            document.Add(new Paragraph(" "));
            document.Add(ls);
            document.Add(new Paragraph("Tételek:"));
            //product table
            Table productTable = new Table(5, false)
                .UseAllAvailableWidth();
            Cell snCell = new Cell(1, 1)
                .Add(new Paragraph("Sorszám"))
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(1))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(9);
            Cell titleCell = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(1))
                .Add(new Paragraph("A termék neve"))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(9);
            Cell quantityCell = new Cell(1, 1)
                .Add(new Paragraph("Darabszám"))
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(1))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(9);
            Cell priceCell = new Cell(1, 1)
                .Add(new Paragraph("Ár"))
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(1))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(9);
            Cell sumCell = new Cell(1, 1)
                .Add(new Paragraph("Összesen"))
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(1))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(9);
            productTable.AddCell(snCell);
            productTable.AddCell(titleCell);
            productTable.AddCell(quantityCell);
            productTable.AddCell(priceCell);
            productTable.AddCell(sumCell);
            foreach (var items in list)
            {
                ++sn;
                sum += items.Quantity * items.Price;
                Cell snCellItem = new Cell(1, 1)
                .Add(new Paragraph(sn.ToString()))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER);
                Cell titleCellItem = new Cell(1, 1)
                    .Add(new Paragraph(items.Name))
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.CENTER);
                Cell quantityCellItem = new Cell(1, 1)
                    .Add(new Paragraph(items.Quantity.ToString()))
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.CENTER);
                Cell priceCellItem = new Cell(1, 1)
                    .Add(new Paragraph(items.Price.ToString()))
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.CENTER);
                Cell sumCellItem = new Cell(1, 1)
                    .Add(new Paragraph(""+items.Quantity*items.Price))
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.CENTER);
                
                productTable.AddCell(snCellItem);
                productTable.AddCell(titleCellItem);
                productTable.AddCell(quantityCellItem);
                productTable.AddCell(priceCellItem);
                productTable.AddCell(sumCellItem);
            }
            document.Add(new Paragraph(" "));
            Cell space = new Cell(1, 4)
                    .Add(new Paragraph(""))
                    .SetBorder(Border.NO_BORDER)
                    .SetBorderTop(new SolidBorder(1));
            Cell maxSumCellItem = new Cell(1, 1)
                    .Add(new Paragraph(sum.ToString() + " din"))
                    .SetBorder(Border.NO_BORDER)
                    .SetBorderTop(new SolidBorder(1))
                    .SetBorderBottom(new SolidBorder(1))
                    .SetBorderLeft(new SolidBorder(1))
                    .SetBorderRight(new SolidBorder(1))
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBold();
            productTable.AddCell(space);
            productTable.AddCell(maxSumCellItem);
            document.Add(productTable);
            if (radioCash.Checked) { 
                payment = "Készpénz";
                paymenttype = 1;
                document.Add(new Paragraph("A fizetés módja: " + payment));
                document.Add(new Paragraph("Megjegyzés:").SetFontSize(10));
                document.Add(new Paragraph("A kiszállítást a cégünk végzi. A tranzakciót készpénz módján történik, az adó az ár 20%-át képezi. Köszönjük, hogy nálunk vásárolt!").SetTextAlignment(TextAlignment.JUSTIFIED).SetFontSize(10));
            }
            else if (radioBankTransfer.Checked) { 
                payment = "Banki átutalás";
                paymenttype = 2;
                document.Add(new Paragraph("A fizetés módja: " + payment));
                document.Add(new Paragraph("Megjegyzés:").SetFontSize(10));
                document.Add(new Paragraph("A kiszállítást a cégünk végzi. Az összeget a feltüntett számlára kell befizetni, az adó az ár 20%-át képezi. \nKöszönjük, hogy nálunk vásárolt!").SetTextAlignment(TextAlignment.JUSTIFIED).SetFontSize(10));
            }
            document.Add(new Paragraph(""));
            document.Add(new Paragraph(""));
            document.Add(new Paragraph(""));
            //signature spaces
            Table signatures = new Table(3, false)
                .UseAllAvailableWidth();
            Cell sig1 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetBorderTop(new SolidBorder(1))
                .Add(new Paragraph("A megbízott aláírása"))
                .SetTextAlignment(TextAlignment.CENTER); 
            Cell sig2 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetBorderTop(new SolidBorder(1))
                .Add(new Paragraph("A megrendelő aláírása"))
                .SetTextAlignment(TextAlignment.CENTER);
            Cell spc = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetWidth(100)
                .Add(new Paragraph(" "));
            signatures.AddCell(sig1.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT)
                .SetVerticalAlignment(VerticalAlignment.BOTTOM));
            signatures.AddCell(spc)
                .SetVerticalAlignment(VerticalAlignment.BOTTOM);
            signatures.AddCell(sig2.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT)
                .SetVerticalAlignment(VerticalAlignment.BOTTOM));
            document.Add(signatures);

            //qr code on the receipt
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 300,
                Height = 300,
            };
            var writerQrCode = new BarcodeWriter();
            writerQrCode.Format = BarcodeFormat.QR_CODE;
            writerQrCode.Options = options;
            //qr generalasa textOrderID mezobol
            var qr = new ZXing.BarcodeWriter();
            qr.Options = options;
            qr.Format = ZXing.BarcodeFormat.QR_CODE;
            var result = new System.Drawing.Bitmap(qr.Write(textOrderID.Text.Trim()));
            PictureBox pb = new PictureBox();
            pb.Image = result;
            result.Save(@"D:\Diplomamunka\QRCODES\receipts\" + textOrderID.Text + ".png");
            
            Image qrImage = new Image(ImageDataFactory.Create(@"D:\\Diplomamunka\QRCODES\receipts\" + textOrderID.Text + ".png"))
                .SetHeight(128)
                .SetWidth(128)
                .SetFixedPosition(450f,25f);
            
            Table footer = new Table(3, false)
                .UseAllAvailableWidth()
                .SetFixedPosition(30f,10f,525f);
            Cell contact = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Tel: +381616573770\nEmail: majorosnorbert99@gmail.com"))
                .SetTextAlignment(TextAlignment.LEFT)
                .SetBorderTop(new SolidBorder(1))
                .SetFontSize(8);
            Cell contactSpace = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph(""))
                .SetBorderTop(new SolidBorder(1));
            Cell footerAddress = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Batinska 95\n24000 Szabadka, Szerbia"))
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBorderTop(new SolidBorder(1))
                .SetFontSize(8);
            footer.AddCell(contact);
            footer.AddCell(contactSpace);
            footer.AddCell(footerAddress);



            document.Add(qrImage.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT));
            document.Add(footer);
            document.Close();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO Orders VALUES ('" + textOrderID.Text + "','" + ord_fname + "','" + ord_lname + "','" + Convert.ToInt32(postal) + "','" + city + "','" + address + "','" + phone + "'," + "GETDATE()" + ",'" + Convert.ToInt32(paymenttype) + "')", conn);
                int rows = cmd.ExecuteNonQuery();
                int rowsOP;
                foreach (var items in list)
                {
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO OrderProduct VALUES ('" + items.ID + "','" + items.Quantity + "','" + Convert.ToInt32(textOrderID.Text) + "')", conn);
                    rowsOP = cmd2.ExecuteNonQuery();
                }
                if (rows > 0)
                    MessageBox.Show("Sikeresen hozzáadva az adatbázishoz");
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
        public void getValueFromList() {
            var itm = orderBox.SelectedItem;
            string str = itm.ToString();
            var strID = str.Split(' ', 3);
            id = Convert.ToInt32(strID[1]);
        }
        public int getPrice()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT ProductPrice from Product WHERE ProductID='" + comboProduct.SelectedValue + "'", conn);
                price = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return price;
        }
        public void AddByQR(product_class product_Class) {
            ++counter;
            orderBox.Items.Add(counter + ". " + product_Class.ID + " - " + product_Class.Name + " x " + product_Class.Quantity + " = " + product_Class.Price * product_Class.Quantity);
        }
    }
}
