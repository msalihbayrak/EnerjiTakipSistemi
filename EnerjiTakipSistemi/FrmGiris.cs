using System;
using System.Windows.Forms;
using EnerjiTakipSistemi.Controllers;

namespace EnerjiTakipSistemi
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            KullaniciController controller = new KullaniciController();

            bool girisBasariliMi = controller.GirisKontrol(txtGirisEmail.Text, txtGirisSifre.Text);

            if (girisBasariliMi == true)
            {
                // Giriş başarılıysa mesaj gösterme, direkt Ana Menüyü aç!
                FrmAnaMenu anaMenu = new FrmAnaMenu();
                anaMenu.Show();
                this.Hide(); // Giriş ekranını arka planda gizle
            }
            else
            {
                MessageBox.Show("Hata: E-posta veya şifre yanlış!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ŞİFREMİ UNUTTUM BUTONU
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmSifremiUnuttum sifreForm = new FrmSifremiUnuttum();
            sifreForm.ShowDialog();
        }

        // KAYIT OL BUTONU
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 kayitEkrani = new Form1();
            kayitEkrani.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmSifremiUnuttum sifreForm = new FrmSifremiUnuttum();
            sifreForm.ShowDialog();
        }
    }
}