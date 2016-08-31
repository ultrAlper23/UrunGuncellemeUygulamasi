using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UrunGuncelleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("server=.; database=northwnd; integrated security=true");
        DataTable dt = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select top 10 ProductID, ProductName, UnitPrice, CategoryID from Products order by newID() ", con);

            adp.Fill(dt);
            gvProducts.DataSource = dt;
            gvProducts.Columns["ProductID"].Visible = false;
          
        }

        private void gvProducts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dt.Rows[e.RowIndex]["ProductID"]);

            int kolon = e.ColumnIndex;
            string kolonAdi = gvProducts.Columns[kolon].Name;//hangi kolonda değişiklik yapılmış

            SqlCommand update = new SqlCommand(string.Format("update products set {0}=@p where ProductID=@id", kolonAdi), con);//set işlemi kolon adına göre yapılacak.
            
            update.Parameters.AddWithValue("@p",
                gvProducts.CurrentCell.Value);//geçerli olan hücrenin değerini aktar.

            update.Parameters.AddWithValue("@id", id);
            con.Open();
            update.ExecuteNonQuery();
            con.Close();
        }
    }
}
