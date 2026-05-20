using System;
using System.Windows.Forms;
using EnerjiTakipSistemi.Controllers;

namespace EnerjiTakipSistemi
{
    public partial class FrmProfil : Form
    {
        // Ana menüden gelen kullanıcı bilgilerini saklayacağız
        private string _mevcutAd, _mevcutSoyad, _mevcutEmail, _mevcutSifre;

        public FrmProfil(string ad, string soyad, string email, string sifre)
        {
            InitializeComponent();
            _mevcutAd = ad;
            _mevcutSoyad = soyad;
            _mevcutEmail = email;
            _mevcutSifre = sifre;
        }

        private void FrmProfil_Load(object sender, EventArgs e)
        {
            // Form açıldığında mevcut bilgileri kutulara yazdır
            txtAd.Text = _mevcutAd;
            txtSoyad.Text = _mevcutSoyad;
            txtEmail.Text = _mevcutEmail;
            txtSifre.Text = _mevcutSifre;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            KullaniciController controller = new KullaniciController();
            string sonuc = controller.ProfilGuncelle(txtAd.Text, txtSoyad.Text, txtEmail.Text, txtSifre.Text);

            if (sonuc == "Basarili")
            {
                MessageBox.Show("Profil bilgileriniz başarıyla güncellendi! Değişikliklerin her yerde aktif olması için lütfen sistemi yeniden başlatın.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Sayfayı kapat
            }
            else
            {
                MessageBox.Show(sonuc, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}