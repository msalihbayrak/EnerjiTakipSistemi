using System.Data;
using System.Data.SqlClient;
using EnerjiTakipSistemi.Models;

namespace EnerjiTakipSistemi.DataAccess
{
    public class KullaniciDal
    {
         
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\EnerjiSistemiDB.mdf;Integrated Security=True");

        public void Ekle(Kullanici kullanici)
        {
            if (baglanti.State == System.Data.ConnectionState.Closed) baglanti.Open();

            SqlCommand komut = new SqlCommand("INSERT INTO Kullanicilar (Ad, Soyad, Email, Sifre, KayitTarihi) VALUES (@p1, @p2, @p3, @p4, @p5)", baglanti);
            komut.Parameters.AddWithValue("@p1", kullanici.Ad);
            komut.Parameters.AddWithValue("@p2", kullanici.Soyad);
            komut.Parameters.AddWithValue("@p3", kullanici.Email);
            komut.Parameters.AddWithValue("@p4", kullanici.Sifre);
            komut.Parameters.AddWithValue("@p5", kullanici.KayitTarihi);

            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public bool GirisYap(string email, string sifre)
        {
            bool kullaniciVarMi = false;
            if (baglanti.State == System.Data.ConnectionState.Closed) baglanti.Open();

            SqlCommand komut = new SqlCommand("SELECT * FROM Kullanicilar WHERE Email=@p1 AND Sifre=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1", email);
            komut.Parameters.AddWithValue("@p2", sifre);

            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                kullaniciVarMi = true;
            }

            dr.Close();
            baglanti.Close();
            return kullaniciVarMi;
        }

        
        public string SifreGetir(string email)
        {
            string sifre = "";

            if (baglanti.State == System.Data.ConnectionState.Closed) baglanti.Open();

            SqlCommand komut = new SqlCommand("SELECT Sifre FROM Kullanicilar WHERE Email=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", email);

            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                sifre = dr["Sifre"].ToString();
            }

            dr.Close();
            baglanti.Close();

            return sifre;
        }
        public DataTable KullanicilariGetir()
        {
            DataTable tablo = new DataTable(); 
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();

            
            SqlCommand komut = new SqlCommand("SELECT Ad, Soyad, Email, KayitTarihi FROM Kullanicilar", baglanti);

            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(tablo); 

            baglanti.Close();
            return tablo;
        }
        // Kullanıcı Bilgilerini Güncelleme Metodu
        public void KullaniciGuncelle(Kullanici kullanici)
        {
            if (baglanti.State == System.Data.ConnectionState.Closed) baglanti.Open();

            // Sisteme giriş yapılan Email'i referans alarak Ad, Soyad ve Şifreyi güncelliyoruz
            SqlCommand komut = new SqlCommand("UPDATE Kullanicilar SET Ad=@p1, Soyad=@p2, Sifre=@p3 WHERE Email=@p4", baglanti);
            komut.Parameters.AddWithValue("@p1", kullanici.Ad);
            komut.Parameters.AddWithValue("@p2", kullanici.Soyad);
            komut.Parameters.AddWithValue("@p3", kullanici.Sifre);
            komut.Parameters.AddWithValue("@p4", kullanici.Email); // Kimin güncelleneceğini Email'den anlıyoruz

            komut.ExecuteNonQuery();
            baglanti.Close();
        }
    }
}