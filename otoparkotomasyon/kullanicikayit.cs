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
    public partial class kullanicikayit : Form
    {
        public kullanicikayit()
        {
            InitializeComponent();
        }


        SqlConnection baglanti = new SqlConnection("SERVER=LAPTOP-PDQV3R54\\SQLEXPRESS; Database=otopark;  Integrated Security=True;");
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        SqlDataReader dr;


        private void button1_Click(object sender, EventArgs e)
        {
            if (txtad.Text == "" || txtkullaniciadi.Text == "" || txtsifre.Text == "" || txtsoyad.Text == "")
            {
                MessageBox.Show("Lütfen ilgili alanları doldurunuz","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText="insert into uyebilgi(uyeadi,uyesoyadi,uyekullaniciadi,uyesifre) Values (@uyeadi,@uyesoyadi,@uyekullaniciadi,@uyesifre)";
                cmd.Parameters.AddWithValue("@uyeadi",txtad.Text);
                cmd.Parameters.AddWithValue("@uyesoyadi",txtsoyad.Text);
                cmd.Parameters.AddWithValue("@uyekullaniciadi",txtkullaniciadi.Text);
                cmd.Parameters.AddWithValue("@uyesifre",txtsifre.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt başarıyla yapılmıştır","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
                baglanti.Close();
                txtad.Text = "";
                txtkullaniciadi.Text = "";
                txtsifre.Text="";
                txtsoyad.Text = "";
                txtkullaniciadi.Focus();
                dataGridView1.Refresh();
                verileri_cek();


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtad.Text == "" || txtkullaniciadi.Text == "" || txtsifre.Text == "" || txtsoyad.Text == "")
            {
                MessageBox.Show("Lütfen ilgili alanları doldurunuz","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "update  uyebilgi set uyekullaniciadi=@uyekullaniciadi, uyesifre=@uyesifre, uyeadi=@uyeadi, uyesoyadi=@uyesoyadi where uyekullaniciadi='"+txtkullaniciadi.Text+"'  ";
                cmd.Parameters.AddWithValue("@uyeadi", txtad.Text);
                cmd.Parameters.AddWithValue("@uyesoyadi", txtsoyad.Text);
                cmd.Parameters.AddWithValue("@uyekullaniciadi", txtkullaniciadi.Text);
                cmd.Parameters.AddWithValue("@uyesifre", txtsifre.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Güncelleme başarıyla yapılmıştır","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
                baglanti.Close();
                txtad.Text = "";
                txtkullaniciadi.Text = "";
                txtsifre.Text = "";
                txtsoyad.Text = "";
                txtkullaniciadi.Focus();
                dataGridView1.Refresh();
                verileri_cek();
            }
        }

        void verileri_cek()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from uyebilgi", baglanti);
            baglanti.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            dataGridView1.Refresh();
        }

        private void kullanicikayit_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            verileri_cek();
            dataGridView1.Refresh();
        }



        private void button3_Click(object sender, EventArgs e) //SİLME İŞLEMİ
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "delete from uyebilgi where uyekullaniciadi='"+txtkullaniciadi.Text+"'";
            komut.ExecuteNonQuery();
            komut.Dispose();
            baglanti.Close();
            ds.Clear();
            verileri_cek();
            MessageBox.Show("Silme işlemi başarıyla gerçekleştirildi","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
            txtad.Text = "";
            txtkullaniciadi.Text = "";
            txtsifre.Text = "";
            txtsoyad.Text = "";

        }


        int tindex = 0;

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                tindex = dataGridView1.SelectedRows[0].Index;
                if (tindex >= 0)
                {
                   txtkullaniciadi.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    txtad.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    txtsoyad.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    txtsifre.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "123456")
            {
                groupBox1.Enabled = true;
            }
            else
                MessageBox.Show("Admin şifresi yanlış", "Bilgilendirme penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
           
        }
    }
}
