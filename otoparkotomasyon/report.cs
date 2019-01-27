using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace otoparkotomasyon
{
    public partial class report : Form
    {


        SqlConnection baglanti = new SqlConnection("SERVER=LAPTOP-PDQV3R54\\SQLEXPRESS; Database=otopark;  Integrated Security=True;");

        public report()
        {
            InitializeComponent();
        }

        void verilericek()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from aracbilgisi", baglanti);
            baglanti.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }


        void arayalimbakalim()
        {
            baglanti.Open();
            DataTable tablo = new DataTable();
            string vara, cumle;
            vara = textBox1.Text;
            cumle = "select * from aracbilgisi where aracplaka like '%" + textBox1.Text + "%'";
            SqlDataAdapter adaptr = new SqlDataAdapter(cumle, baglanti);
            adaptr.Fill(tablo);
            baglanti.Close();
            dataGridView1.DataSource = tablo;
        }

        private void report_Load(object sender, EventArgs e)
        {
            verilericek();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            arayalimbakalim();
        }

        void ariyoruzanam()
        {
            baglanti.Open();
            DataTable tablo = new DataTable();
            string vara, cumle;
            vara = textBox2.Text;
            cumle = "select * from aracbilgisi where aracyeri like '%" + textBox2.Text + "%'";
            SqlDataAdapter adaptr = new SqlDataAdapter(cumle, baglanti);
            adaptr.Fill(tablo);
            baglanti.Close();
            dataGridView1.DataSource = tablo;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox1.Enabled = true;
                textBox1.Focus();
                textBox2.Enabled = false;
            }
            else
                textBox1.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox2.Enabled = true;
                textBox2.Focus();
                textBox1.Enabled = false;
            }
            else
                textBox2.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ariyoruzanam();
        }
    }
}
