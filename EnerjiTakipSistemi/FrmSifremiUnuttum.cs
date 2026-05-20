using System;
using System.Windows.Forms;
using EnerjiTakipSistemi.Controllers;

namespace EnerjiTakipSistemi
{
    public partial class FrmSifremiUnuttum : Form
    {
        public FrmSifremiUnuttum()
        {
            InitializeComponent();
        }

        private void btnSifreGonder_Click(object sender, EventArgs e)
        {
            KullaniciController controller = new KullaniciController();

            // Controller'dan mail gönderme sonucunu alıyoruz
            string sonucMesaji = controller.SifreHatirlat(txtEmail.Text);

            // Eğer mesajda "başarıyla" kelimesi geçiyorsa işlem tamamdır
            if (sonucMesaji.Contains("başarıyla"))
            {
                // 1. Bilgi mesajını göster
                MessageBox.Show(sonucMesaji, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 2. Bu pencereyi kapat (Böylece arkadaki giriş ekranına geri dönülür)
                this.Close();
            }
            else
            {
                // Bir hata varsa (E-posta bulunamadı vb.) sadece uyarı ver
                MessageBox.Show(sonucMesaji, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}