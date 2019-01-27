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
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection("SERVER=LAPTOP-PDQV3R54\\SQLEXPRESS; Database=otopark;  Integrated Security=True;");
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtkullanici.Text == "" || txtsifre.Text == "")
            {
                MessageBox.Show("Lütfen ilgili alanları doldurunuz..","Bilgilendirme penceresi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText="select * from uyebilgi where uyekullaniciadi ='"+txtkullanici.Text+"' AND uyesifre=" +txtsifre.Text+"";
                baglanti.Open();
                dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    anamenu ana = new anamenu();
                    this.Hide();
                    ana.ShowDialog();
                    
                }
                else 
                {
                    MessageBox.Show("Yanlış kullanıcı adı veya şifre girdiniz","Bilgilendirme penceresi" ,MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                    txtkullanici.Focus();
                }
                baglanti.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        DialogResult cevap=MessageBox.Show("Çıkış Yapmak İstiyor Musunuz?" , "Bilgilendirme Penceresi",MessageBoxButtons.YesNo);
        if (cevap == DialogResult.Yes)
        {
            Application.Exit();
        }
            if(cevap == DialogResult.No)
            {
                Form1 f1=new Form1();
                f1.Show();

            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            txtkullanici.Focus();
            label3.Text = "Tarih : " + DateTime.Now.ToLongDateString();
            label4.Text = "Saat : " + DateTime.Now.ToLongTimeString();
            txtsifre.Text = "123456";
            txtkullanici.Text = "admin";
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            label4.Text = "Saat : " + DateTime.Now.ToLongTimeString();
           
        }

        private void txtkullanici_Click(object sender, EventArgs e)
        {
        }

        private void txtkullanici_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtsifre_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
