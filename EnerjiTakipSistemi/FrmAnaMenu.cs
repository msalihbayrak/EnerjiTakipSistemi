using EnerjiTakipSistemi.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnerjiTakipSistemi
{
    public partial class FrmAnaMenu : Form
    {
        public FrmAnaMenu()
        {
            InitializeComponent();
        }

        // --- GÜNCELLENMİŞ ÖZEL MOTORUMUZ (UYARI SİSTEMİ EKLENDİ) ---
        private void EkraniGuncelle()
        {
            EnerjiTakipSistemi.Controllers.EnerjiController controller = new EnerjiTakipSistemi.Controllers.EnerjiController();

            // 1. Tabloyu Yenile
            dataGridView1.DataSource = controller.EnerjiListesiniAl();

            // 2. Grafiği Yenile
            System.Data.DataTable grafikTablosu = controller.GrafikIcinVeriAl();
            chart1.Series[0].Points.Clear(); // Eski grafiği temizle

            // Her güncellemede uyarıyı önce gizle (belki veri silinip limitin altına inilmiştir)
            lblUyari.Visible = false;

            foreach (System.Data.DataRow satir in grafikTablosu.Rows)
            {
                string tur = satir["EnerjiTuru"].ToString();
                decimal toplam = Convert.ToDecimal(satir["ToplamTutar"]);
                chart1.Series[0].Points.AddXY(tur, toplam);

                // --- UYARI SİSTEMİ (LİMİT KONTROLÜ) ---
                // Elektrik faturası toplamı 1000 TL'yi geçerse uyarı ver!
                if (tur == "Elektrik" && toplam > 1000)
                {
                    lblUyari.Text = "DİKKAT: Elektrik bütçesi (1000 TL) aşıldı! Güncel: " + toplam + " TL";
                    lblUyari.Visible = true;
                }

                // Su için de ayrı bir limit
                if (tur == "Su" && toplam > 500)
                {
                    lblUyari.Text = "DİKKAT: Su bütçesi (500 TL) aşıldı! Güncel: " + toplam + " TL";
                    lblUyari.Visible = true;
                }
            }
        }

        private void FrmAnaMenu_Load(object sender, EventArgs e)
        {
            // Form açıldığında verileri doldurmak için motoru 1 kez çalıştır
            EkraniGuncelle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Controller'ı çağırıyoruz
            EnerjiTakipSistemi.Controllers.EnerjiController controller = new EnerjiTakipSistemi.Controllers.EnerjiController();

            // Kutulardaki verileri Controller'a gönderiyoruz
            string sonuc = controller.TuketimEkle(cmbEnerjiTuru.Text, txtTutar.Text, dtpTarih.Value);

            if (sonuc == "Basarili")
            {
                MessageBox.Show("Enerji tüketim verisi başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Kayıt başarılı olduktan sonra ekranı yeni veri girişi için temizliyoruz
                txtTutar.Clear();
                cmbEnerjiTuru.SelectedIndex = -1; // Seçimi temizle
                dtpTarih.Value = DateTime.Now;    // Tarihi tekrar bugüne ayarla

                // Veri eklendiği an tabloyu, grafiği ve limitleri güncellemek için motoru çalıştır!
                EkraniGuncelle();
            }
            else
            {
                // Controller'dan gelen hata mesajını ekranda göster
                MessageBox.Show(sonuc, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Önce tabloda bir satır seçilmiş mi diye kontrol ediyoruz
            if (dataGridView1.CurrentRow != null)
            {
                // Yanlışlıkla basılmalara karşı kullanıcıdan onay istiyoruz
                DialogResult cevap = MessageBox.Show("Seçili veriyi silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Eğer kullanıcı çıkan uyarıya "Evet" derse:
                if (cevap == DialogResult.Yes)
                {
                    // Tablonun 0. sütunundaki (Yani 'Kayıt No' sütunundaki) Id değerini alıyoruz
                    int secilenId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                    // Controller'ı çağırıp "Bu Id'yi sil" diyoruz
                    EnerjiTakipSistemi.Controllers.EnerjiController controller = new EnerjiTakipSistemi.Controllers.EnerjiController();
                    controller.TuketimSil(secilenId);

                    MessageBox.Show("Veri başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Veri silindiği an tabloyu, grafiği ve limitleri güncellemek için motoru çalıştır!
                    EkraniGuncelle();
                }
            }
            else
            {
                // Eğer hiçbir satır seçilmeden butona basılırsa uyarı ver
                MessageBox.Show("Lütfen silmek için tablodan bir veri seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmCihazlar cihazEkrani = new FrmCihazlar();
            cihazEkrani.ShowDialog();
        }

        
        private void btnProfil_Click(object sender, EventArgs e)
        {
            // Formun beklediği 4 bilgiyi (Ad, Soyad, Email, Şifre) sırasıyla gönderiyoruz
            FrmProfil profilEkrani = new FrmProfil("Kullanıcı Adı", "Kullanıcı Soyadı", "kullanici@mail.com", "12345");
            profilEkrani.ShowDialog();
        }
        
            private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Tablodan bir veri seçilmiş mi kontrol et
            if (dataGridView1.CurrentRow != null)
            {
                // Tablodaki gizli ID numarasını al
                int secilenId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                // Controller'ı çağırıp güncelleme işlemini başlat
                EnerjiTakipSistemi.Controllers.EnerjiController controller = new EnerjiTakipSistemi.Controllers.EnerjiController();
                string sonuc = controller.TuketimGuncelle(secilenId, cmbEnerjiTuru.Text, txtTutar.Text, dtpTarih.Value);

                if (sonuc == "Basarili")
                {
                    MessageBox.Show("Tüketim verisi başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ekranı temizle ve motoru çalıştırıp grafikleri/tabloyu yenile
                    txtTutar.Clear();
                    cmbEnerjiTuru.SelectedIndex = -1;
                    EkraniGuncelle();
                }
                else
                {
                    MessageBox.Show(sonuc, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek için tablodan bir veri seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


            private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Tıklanan satır boş değilse (başlık vs değilse)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = dataGridView1.Rows[e.RowIndex];

                // Tablodaki verileri sol taraftaki kutulara otomatik doldur
                cmbEnerjiTuru.Text = satir.Cells[1].Value.ToString();
                txtTutar.Text = satir.Cells[2].Value.ToString();
                dtpTarih.Value = Convert.ToDateTime(satir.Cells[3].Value);
            }
        }
    }
    }
    
