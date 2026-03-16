using System;
using System.Data.SqlClient; 
using EnerjiTakipSistemi.Entities; 

namespace EnerjiTakipSistemi.DataAccess
{
    public class KullaniciDal
    {
        
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\EnerjiDB.mdf;Integrated Security=True");

        
        public void Ekle(Kullanici kullanici)
        {
            
            if (baglanti.State == System.Data.ConnectionState.Closed)
            {
                baglanti.Open();
            }

            
            
            SqlCommand komut = new SqlCommand("INSERT INTO [Table] (Ad, Soyad, Email, Sifre, KayitTarihi) VALUES (@p1, @p2, @p3, @p4, @p5)", baglanti);

            // 4. Formdan (nesneden) gelen verileri SQL komutunun içine güvenli bir şekilde yerleştiriyoruz
            komut.Parameters.AddWithValue("@p1", kullanici.Ad);
            komut.Parameters.AddWithValue("@p2", kullanici.Soyad);
            komut.Parameters.AddWithValue("@p3", kullanici.Email);
            komut.Parameters.AddWithValue("@p4", kullanici.Sifre);
            komut.Parameters.AddWithValue("@p5", kullanici.KayitTarihi);

            
            komut.ExecuteNonQuery();

            
            baglanti.Close();
        }
    }
}