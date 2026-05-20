using System;
using System.Data;
using System.Data.SqlClient;
using EnerjiTakipSistemi.Models;

namespace EnerjiTakipSistemi.DataAccess
{
    public class CihazDal
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\EnerjiSistemiDB.mdf;Integrated Security=True");

        public void CihazEkle(Cihaz cihaz)
        {
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();

            SqlCommand komut = new SqlCommand("INSERT INTO Cihazlar (CihazAdi, GucTuketimi, Adet) VALUES (@p1, @p2, @p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", cihaz.CihazAdi);
            komut.Parameters.AddWithValue("@p2", cihaz.GucTuketimi);
            komut.Parameters.AddWithValue("@p3", cihaz.Adet);

            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public DataTable CihazlariGetir()
        {
            DataTable tablo = new DataTable();
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();

            // Ekranda şık durması için AS ile sütun isimlerini Türkçe yapıyoruz
            SqlCommand komut = new SqlCommand("SELECT Id AS 'Kayıt No', CihazAdi AS 'Cihaz Adı', GucTuketimi AS 'Güç Tüketimi (Watt)', Adet FROM Cihazlar", baglanti);

            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(tablo);

            baglanti.Close();
            return tablo;
        }

        public void CihazSil(int id)
        {
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();

            SqlCommand komut = new SqlCommand("DELETE FROM Cihazlar WHERE Id = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", id);

            komut.ExecuteNonQuery();
            baglanti.Close();
        }
    }
}