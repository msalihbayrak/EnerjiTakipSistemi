using System;
using System.Windows.Forms;
using EnerjiTakipSistemi.Controllers; 

namespace EnerjiTakipSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            
            KullaniciController controller = new KullaniciController();

             
            
            string sonucMesaji = controller.KayitYap(txtAd.Text, txtSoyad.Text, txtEmail.Text, txtSifre.Text);

            
            MessageBox.Show(sonucMesaji);

            
            txtAd.Clear();
            txtSoyad.Clear();
            txtEmail.Clear();
            txtSifre.Clear();
        }
    }
}