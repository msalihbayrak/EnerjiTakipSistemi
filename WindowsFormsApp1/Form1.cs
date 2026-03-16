using System;
using System.Windows.Forms;

// İŞTE BURAYI SİSTEMİN KAFASI KARIŞMASIN DİYE ESKİ İSMİYLE BIRAKIYORUZ:
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            // Nesnemizi oluştururken yeni verdiğimiz ismi kullanmaya devam ediyoruz
            EnerjiTakipSistemi.Entities.Kullanici yeniKullanici = new EnerjiTakipSistemi.Entities.Kullanici();

            yeniKullanici.Ad = txtAd.Text;
            yeniKullanici.Soyad = txtSoyad.Text;
            yeniKullanici.Email = txtEmail.Text;
            yeniKullanici.Sifre = txtSifre.Text;

            MessageBox.Show("Veriler başarıyla nesneye alındı! Hoş geldin, " + yeniKullanici.Ad + " " + yeniKullanici.Soyad);
        }

    }
}