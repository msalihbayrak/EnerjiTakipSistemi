using System;
using System.Windows.Forms;
using EnerjiTakipSistemi.Controllers;

namespace EnerjiTakipSistemi
{
    public partial class FrmCihazlar : Form
    {
        public FrmCihazlar()
        {
            InitializeComponent();
        }

        // Tıpkı Ana Menü'deki gibi anında yenileme motorumuz
        private void EkraniGuncelle()
        {
            CihazController controller = new CihazController();
            dataGridView1.DataSource = controller.CihazListesiniAl();
        }

        private void FrmCihazlar_Load(object sender, EventArgs e)
        {
            EkraniGuncelle();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            CihazController controller = new CihazController();
            string sonuc = controller.CihazEkle(txtCihazAdi.Text, txtGuc.Text, txtAdet.Text);

            if (sonuc == "Basarili")
            {
                MessageBox.Show("Cihaz başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCihazAdi.Clear();
                txtGuc.Clear();
                txtAdet.Clear();
                EkraniGuncelle(); // Listeyi anında yenile
            }
            else
            {
                MessageBox.Show(sonuc, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DialogResult cevap = MessageBox.Show("Seçili cihazı silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (cevap == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    CihazController controller = new CihazController();
                    controller.CihazSil(id);

                    MessageBox.Show("Cihaz başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EkraniGuncelle(); // Listeyi anında yenile
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için listeden bir cihaz seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}