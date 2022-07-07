using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace RaktarKezeloDiploma
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-96C041G\SQLEXPRESS;Initial Catalog=Storage;Integrated Security=True");
        private void Product_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(@"SELECT p.ProductID,p.ProductTitle,p.ProductQuantity,p.ProductDescription,p.ProductPrice,c.CategoryName,b.BrandName,ps.PositionRow,ps.PositionColumn,ps.PositionSector
                                                      FROM Product p
                                                      join Category c on p.CategoryID=c.CategoryID
                                                      join Brand b on p.BrandID=b.BrandID
                                                      join ProductPosition ps on p.ProductID=ps.ProductID WHERE p.ProductID='" + ScanQRCode.code + "'", conn);
                SqlDataReader sqlDataReader = command.ExecuteReader();
                bool recordFound = sqlDataReader.Read();
                if (recordFound)
                {
                    lbl_ID.Text = sqlDataReader["ProductID"].ToString();
                    lbl_Title.Text = sqlDataReader["ProductTitle"].ToString();
                    lbl_Quantity.Text = sqlDataReader["ProductQuantity"].ToString();
                    lbl_description.Text = sqlDataReader["ProductDescription"].ToString();
                    lbl_price.Text = sqlDataReader["ProductPrice"].ToString();
                    lbl_category.Text = sqlDataReader["CategoryName"].ToString();
                    lbl_brand.Text = sqlDataReader["BrandName"].ToString();
                    lbl_Row.Text = sqlDataReader["PositionRow"].ToString();
                    lbl_Column.Text = sqlDataReader["PositionColumn"].ToString();
                    lbl_Sector.Text = sqlDataReader["PositionSector"].ToString();
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
    }
}
