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
    public partial class aracgiris : Form
    {

        SqlConnection baglanti = new SqlConnection("SERVER=LAPTOP-PDQV3R54\\SQLEXPRESS; Database=otopark;  Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr,dr2;
        int islem_id, parkfiyat,aracid;
        bool park_sorgu = false;
        string query2 = "Select @@Identity";
        void verilericek()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from aracbilgisi where cikissaati IS NULL", baglanti);

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
            cumle = "select * from aracbilgisi where aracplaka like '%" + textBox6.Text + "%'";
            SqlDataAdapter adaptr = new SqlDataAdapter(cumle, baglanti);
            adaptr.Fill(tablo);
            baglanti.Close();
            dataGridView1.DataSource = tablo;

        }


        public aracgiris()
        {
            InitializeComponent();
        }
        void goster()
        {
                baglanti.Open();
                string normal = "SELECT * FROM alan";
                cmd = new SqlCommand(normal, baglanti);
                dr = cmd.ExecuteReader();
                int i = 0;
                int t = 0;
                int ust = 30;
                while (dr.Read())
                {
                    Button btn = new Button();
                    btn.AutoSize = false;
                    btn.Width = 110;
                    btn.Height = 50;
                    btn.Top = ust;
                    if (i % 3 == 0 && i != 0)
                    {
                        btn.Top = ust + 60;
                        ust += 60;
                        i = 0;
                    }
                    btn.Left = i * 120;
                    btn.ForeColor = Color.White;
                    btn.Name = dr["id"].ToString();
                    btn.Text = dr["parkyeri"].ToString();


                    if (int.Parse(dr["durum"].ToString()) == 1)
                    {
                        btn.BackColor = Color.Green;
                    }
                    else if (int.Parse(dr["durum"].ToString()) == 2)
                    {
                        btn.BackColor = Color.Red;
                    }
                    btn.TabStop = false;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    parkyerleri.Controls.Add(btn);
                    btn.Click += new EventHandler(btn_Click);
                    i++;
                    t++;
                }
                cmd.Dispose();
                baglanti.Close();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            if (panel1.Visible == false) { panel1.Visible = true;  }
            Button btn = (Button)sender;
            islem_id = int.Parse(btn.Name);
            islemkontrol(islem_id);
            textBox8.Text = btn.Text.ToString();
            if (park_sorgu)
            {
                park_disable();
                aracbilgicek(aracid);
            }
            textBox8.Text = btn.Text.ToString();
        }
        void islemkontrol(int a)
        {
            
            baglanti.Open();
            string bul = "SELECT * FROM alan where  id=" + a + "";
            cmd = new SqlCommand(bul, baglanti);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (int.Parse(dr["durum"].ToString()) == 1)
                {
                    park_sorgu = false;
                    islem_durum.Text = "Park Yeri Boş";
                    islem_durum.ForeColor = Color.Green;
                    park_visable();
                }
                if (int.Parse(dr["durum"].ToString()) == 2)
                {
                    islem_durum.ForeColor = Color.Red;
                    aracid = int.Parse(dr["aracturu"].ToString());
                    islem_durum.Text = "Park Yeri Dolu";
                    park_sorgu = true;
                }
            }

            dr.Close();
            string fiyatbul = "SELECT * FROM aracturu where  Id=" + 1 + "";
            SqlCommand komut = new SqlCommand(fiyatbul, baglanti);
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                parkfiyat = int.Parse(dr["ucret"].ToString());
            }
            komut.Dispose();
            cmd.Dispose();
            baglanti.Close();
        }

        void aracbilgicek(int id)
        {
            baglanti.Open();
            string bul = "SELECT * FROM aracbilgisi where  Id=" + id + "";
            SqlCommand cmd2 = new SqlCommand(bul, baglanti);
            dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                textBox1.Text = dr2["aracplaka"].ToString();
                comboBox1.Text = dr2["aractipi"].ToString();
                dateTimePicker1.Text = dr2["girissaati"].ToString();

                textBox2.Text = dr2["aracplaka"].ToString();
                textBox3.Text = dr2["aractipi"].ToString();
                textBox4.Text = dr2["aracyeri"].ToString();
                textBox5.Text = dr2["girissaati"].ToString();
                DateTime girisimiz = Convert.ToDateTime(dr2["girissaati"].ToString());
                DateTime cikisimiz = dateTimePicker2.Value;
                TimeSpan fark = cikisimiz - girisimiz;
                int kalinangun = fark.Hours;
                if (kalinangun == 0) kalinangun = 1;
                textBox7.Text = (kalinangun * int.Parse(dr2["ucret"].ToString())).ToString();
            }
            dr2.Close();
            cmd.Dispose();
            baglanti.Close();
        }


        void park_visable()
        {
            textBox1.Clear();
            textBox8.Clear();
            kaydet.Enabled = true;
            textBox1.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBox1.Enabled = true;
            cikisver.Visible = false; ;
        }
        void park_disable()
        {
            textBox1.Clear();
            textBox8.Clear();
            cikisver.Visible = true;
            kaydet.Enabled = false;
            textBox1.Enabled = false;
            textBox8.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBox1.Enabled = false;
        }
        private void aracgiris_Load(object sender, EventArgs e)
        {
            verilericek();
            //PANEL BAŞTA GÖZÜKMEYECEK
            panel1.Visible = false;
            panel2.Visible = false;
            
            goster();
            SqlDataReader oku;
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText="select * from aracturu";
            oku = cmd.ExecuteReader();
            while(oku.Read())
            {
                comboBox1.Items.Add(oku[1]);
                ucret.Items.Add(oku[2]);
            }
            baglanti.Close();
            comboBox1.SelectedIndex = 0;
        }


       
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            arayalimbakalim();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void araçGirişToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void araçÇıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }


       

        void butonolustur()
        {
            int x = 60; int y = 20; int ls = 1; int lx = 0;
            for (int i = 0; i < 12; i++)
            {
                if (i % 3 == 0 || i == 0)
                {
                    y += 30;
                    x = 100;
                    lx = x;
                    Button lbl = new Button();
                    Point lblyer = new Point(lx - 50, y + 5);
                    lbl.Location = lblyer;
                    lbl.Width = 40;
                    lbl.Text = ls.ToString() + ".Ders";
                    lbl.Name = "lbl" + i.ToString();
                    ls++;
                    groupBox1.Controls.Add(lbl);
                    lx += 20;
                }
                MaskedTextBox txt = new MaskedTextBox();
                Point yer = new Point(x, y);
                txt.Name = "txt" + i.ToString();
                txt.Location = yer;
                txt.Width = 35; txt.Height = 25;
                txt.Mask = "00:00";
                this.Controls.Add(txt);
                groupBox1.Controls.Add(txt);
                x += 60;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
     

                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                string park_duzen = "update alan set aracturu=@aracturu,durum=@durum where id=" + islem_id + "";
                SqlCommand komutduzen = new SqlCommand(park_duzen, baglanti);
                komutduzen.Parameters.AddWithValue("@aracturu", DBNull.Value); 
                komutduzen.Parameters.AddWithValue("@durum", 1);
                komutduzen.ExecuteNonQuery();

                string aracguncel = "update aracbilgisi set cikissaati=@cikissaati where Id=" + aracid;
                SqlCommand cmd = new SqlCommand(aracguncel, baglanti);
                cmd.Parameters.AddWithValue("@cikissaati", Convert.ToDateTime(dateTimePicker2.Value.ToLongTimeString()));
                cmd.ExecuteNonQuery();

                string ekle = "insert into odeme(aracId,aracplaka,girissaati,cikissaati,ucret) values (@aracId,@aracplaka,@girissaati,@cikissaati,@ucret)";
                SqlCommand komut = new SqlCommand(ekle, baglanti);
                komut.Parameters.AddWithValue("@aracId", aracid);
                komut.Parameters.AddWithValue("@aracplaka", textBox2.Text);
                komut.Parameters.AddWithValue("@girissaati", Convert.ToDateTime(dateTimePicker1.Value.ToLongTimeString()));
                komut.Parameters.AddWithValue("@cikissaati", Convert.ToDateTime(dateTimePicker1.Value.ToLongTimeString()));
                komut.Parameters.AddWithValue("@ucret", textBox7.Text);
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglanti.Close();
                MessageBox.Show("Çıkış İşlemi Tamamlandı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Guncelle();
        }
        int tindex;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                tindex = dataGridView1.SelectedRows[0].Index;
                if (tindex >= 0)
                {
                    textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                //    Convert.ToDateTime(dateTimePicker2.Value.ToLongTimeString) = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                }
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cikisver_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }
        void cikistemizle(){
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox7.Clear();
        }
        public void Guncelle()
        {
            this.Visible = false;
            this.Dispose();
            Form aracgiris = new aracgiris();
            aracgiris.ShowDialog();
        }
        private void kaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            // ----- EKLEME BAŞLA ----- //

            string ekle = "insert into aracbilgisi(aracplaka,aractipi,aracyeri,girissaati,ucret,park_id) values (@aracplaka,@aractipi,@aracyeri,@girissaati,@ucret,@park_id)";
            SqlCommand komut = new SqlCommand(ekle, baglanti);
            komut.Parameters.AddWithValue("@aracplaka", textBox1.Text);
            komut.Parameters.AddWithValue("@aractipi", comboBox1.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@aracyeri", textBox8.Text);
            komut.Parameters.AddWithValue("@girissaati", Convert.ToDateTime(dateTimePicker1.Value.ToLongTimeString()));
            komut.Parameters.AddWithValue("@ucret", ucret.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@park_id", islem_id);
            komut.ExecuteNonQuery();
            komut.CommandText = query2;
            int aracid = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            string park_duzen = "update alan set aracturu=@aracturu,durum=@durum where id=" + islem_id + "";
            SqlCommand komutduzen = new SqlCommand(park_duzen, baglanti);
            komutduzen.Parameters.AddWithValue("@aracturu", aracid);
            komutduzen.Parameters.AddWithValue("@durum", 2);
            komutduzen.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Kayıt İşlemi Tamamlandı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Guncelle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucret.SelectedIndex = comboBox1.SelectedIndex;
        }
    }
}
        
        

