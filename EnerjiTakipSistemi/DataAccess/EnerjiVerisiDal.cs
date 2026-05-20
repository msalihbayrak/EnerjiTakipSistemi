using System;
using System.Data;
using System.Data.SqlClient;
using EnerjiTakipSistemi.Models;

namespace EnerjiTakipSistemi.DataAccess
{
    public class EnerjiVerisiDal
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\EnerjiSistemiDB.mdf;Integrated Security=True");

        // 1. Veri Ekleme Metodu
        public void VeriEkle(EnerjiVerisi veri)
        {
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();

            SqlCommand komut = new SqlCommand("INSERT INTO EnerjiVerileri (EnerjiTuru, Tutar, Tarih) VALUES (@p1, @p2, @p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", veri.EnerjiTuru);
            komut.Parameters.AddWithValue("@p2", veri.Tutar);
            komut.Parameters.AddWithValue("@p3", veri.Tarih);

            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        // 2. Verileri Tabloya Getirme Metodu (Silme işlemi için Id sütunu eklendi)
        public DataTable VerileriGetir()
        {
            DataTable tablo = new DataTable();
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();

            SqlCommand komut = new SqlCommand("SELECT Id AS 'Kayıt No', EnerjiTuru AS 'Enerji Türü', Tutar AS 'Fatura Tutarı (TL)', Tarih FROM EnerjiVerileri", baglanti);

            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(tablo);

            baglanti.Close();
            return tablo;
        }

        // 3. Veri Silme Metodu
        public void VeriSil(int id)
        {
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();

            SqlCommand komut = new SqlCommand("DELETE FROM EnerjiVerileri WHERE Id = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", id);

            komut.ExecuteNonQuery();
            baglanti.Close();
        }
        // 4. Veri Güncelleme Metodu
        public void VeriGuncelle(int id, EnerjiVerisi veri)
        {
            if (baglanti.State == System.Data.ConnectionState.Closed) baglanti.Open();

            // Seçilen ID'ye göre Enerji Türü, Tutar ve Tarihi güncelliyoruz
            SqlCommand komut = new SqlCommand("UPDATE EnerjiVerileri SET EnerjiTuru=@p1, Tutar=@p2, Tarih=@p3 WHERE Id=@p4", baglanti);
            komut.Parameters.AddWithValue("@p1", veri.EnerjiTuru);
            komut.Parameters.AddWithValue("@p2", veri.Tutar);
            komut.Parameters.AddWithValue("@p3", veri.Tarih);
            komut.Parameters.AddWithValue("@p4", id); // Hangi satırın güncelleneceğini ID'den anlıyoruz

            komut.ExecuteNonQuery();
            baglanti.Close();
        }
        // 5. Grafiğe özel, verileri türüne göre toplayıp getiren metot
        public DataTable GrafikVerileriniGetir()
        {
            DataTable tablo = new DataTable();
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();

            // Aynı enerji türlerini grupla (GROUP BY) ve tutarlarını topla (SUM)
            SqlCommand komut = new SqlCommand("SELECT EnerjiTuru, SUM(Tutar) AS ToplamTutar FROM EnerjiVerileri GROUP BY EnerjiTuru", baglanti);

            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(tablo);

            baglanti.Close();
            return tablo;
        }
    }
}