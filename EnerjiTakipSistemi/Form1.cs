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

            // Controller'dan gelen cevabı alıyoruz
            string sonucMesaji = controller.KayitYap(txtAd.Text, txtSoyad.Text, txtEmail.Text, txtSifre.Text);

            // Gelen mesajda "Başarılı" kelimesi geçiyorsa:
            if (sonucMesaji.Contains("Başarılı"))
            {
                // 1. Başarı mesajını göster
                MessageBox.Show(sonucMesaji, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 2. Kutuları temizle (ileride tekrar girilirse diye)
                txtAd.Clear();
                txtSoyad.Clear();
                txtEmail.Clear();
                txtSifre.Clear();

                // 3. Giriş ekranını aç ve bu ekranı (Kayıt ekranını) gizle
                FrmGiris girisEkrani = new FrmGiris();
                girisEkrani.Show();
                this.Hide();
            }
            else
            {
                // Eğer hata varsa (boş bırakıldı vs.) ekranı kapatma, sadece hatayı göster
                MessageBox.Show(sonucMesaji, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}