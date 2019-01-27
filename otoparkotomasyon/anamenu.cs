using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otoparkotomasyon
{
    public partial class anamenu : Form
    {
        public anamenu()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            kullanicikayit kayit = new kullanicikayit();
            kayit.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            aracbilgi bilgi = new aracbilgi();
            bilgi.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            aracgiris gir = new aracgiris();
            gir.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            kullanicikayit kayit = new kullanicikayit();
            kayit.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            report rp = new report();
            rp.ShowDialog();
        }
    }
}
