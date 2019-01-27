using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;


namespace otoparkotomasyon
{
    public partial class aracbilgi : Form
    {
        SqlConnection baglanti = new SqlConnection("SERVER=LAPTOP-PDQV3R54\\SQLEXPRESS; Database=otopark;  Integrated Security=True;");
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        
        SqlDataReader dr;

        void verilericek()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from aracbilgisi",baglanti);
            baglanti.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }

        public aracbilgi()
        {
            InitializeComponent();
        }

        private void aracbilgi_Load(object sender, EventArgs e)
        {
            verilericek();
            textBox7.Enabled = false;
            textBox9.Enabled = false;
        }

        void arayalimbakalim()
        {
            baglanti.Open();
            DataTable tablo = new DataTable();
            string vara, cumle;
            vara = textBox7.Text;
            cumle = "select * from aracbilgisi where aracplaka like '%" + textBox7.Text + "%'";
            SqlDataAdapter adaptr = new SqlDataAdapter(cumle, baglanti);
            adaptr.Fill(tablo);
            baglanti.Close();
            dataGridView1.DataSource = tablo;
        }


       void ariyoruzanam()
        {
            baglanti.Open();
            DataTable tablo = new DataTable();
            string vara, cumle;
            vara = textBox9.Text;
            cumle = "select * from aracbilgisi where aracyeri like '%" + textBox9.Text + "%'";
            SqlDataAdapter adaptr = new SqlDataAdapter(cumle, baglanti);
            adaptr.Fill(tablo);
            baglanti.Close();
            dataGridView1.DataSource = tablo;
        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox7.Enabled = true;
                textBox7.Focus();
                textBox9.Enabled = false;
            }
            else
                textBox7.Enabled = false ;
        }

        

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                textBox9.Enabled = true;
                textBox9.Focus();
                textBox7.Enabled = false;
            }
            else
                textBox9.Enabled = false;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            arayalimbakalim();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            ariyoruzanam();
        }
    }
}
