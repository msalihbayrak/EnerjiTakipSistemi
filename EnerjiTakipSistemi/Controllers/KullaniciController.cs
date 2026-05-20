using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using EnerjiTakipSistemi.Models;
using EnerjiTakipSistemi.DataAccess;

namespace EnerjiTakipSistemi.Controllers
{
    public class KullaniciController
    {
        public string KayitYap(string ad, string soyad, string email, string sifre)
        {
            Kullanici yeniKullanici = new Kullanici();
            yeniKullanici.Ad = ad;
            yeniKullanici.Soyad = soyad;
            yeniKullanici.Email = email;
            yeniKullanici.Sifre = sifre;

            if (string.IsNullOrEmpty(sifre) || string.IsNullOrEmpty(email))
            {
                return "E-posta ve şifre alanları boş bırakılamaz!";
            }

            KullaniciDal dal = new KullaniciDal();
            dal.Ekle(yeniKullanici);

            return "Kayıt Başarılı! Sisteme hoş geldin, " + ad;
        }

        public bool GirisKontrol(string email, string sifre)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre))
            {
                return false;
            }

            KullaniciDal dal = new KullaniciDal();
            return dal.GirisYap(email, sifre);
        }

        // MAİL GÖNDERME METODU
        public string SifreHatirlat(string email)
        {
            if (string.IsNullOrEmpty(email)) return "Lütfen e-posta adresinizi girin.";

            KullaniciDal dal = new KullaniciDal();
            string veritabanindakiSifre = dal.SifreGetir(email);

            if (string.IsNullOrEmpty(veritabanindakiSifre))
            {
                return "Bu e-posta adresine kayıtlı bir hesap bulunamadı!";
            }

            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;

                client.Credentials = new NetworkCredential("msalihbayrakk@gmail.com", "mrxotujykpxqmdgs");

                MailMessage mesaj = new MailMessage();
                mesaj.From = new MailAddress("msalihbayrakk@gmail.com", "Enerji Takip Sistemi");
                mesaj.To.Add(email);
                mesaj.Subject = "Şifre Hatırlatma Talebi";
                mesaj.Body = "Sistemimize kayıtlı şifreniz: " + veritabanindakiSifre;

                client.Send(mesaj);
                return "Şifreniz e-posta adresinize başarıyla gönderildi!";
            }
            catch (Exception ex)
            {
                return "Mail gönderilirken bir hata oluştu: " + ex.Message;
            }
        }

        
        public DataTable KullaniciListesiniAl()
        {
            KullaniciDal dal = new KullaniciDal();
            return dal.KullanicilariGetir();
        }
        // YENİ EKLENEN: Silme işlemini Dal'a ileten metot
        public void TuketimSil(int id)
        {
            EnerjiVerisiDal dal = new EnerjiVerisiDal();
            dal.VeriSil(id);
        }
        // Profil Güncelleme Kontrolü
        public string ProfilGuncelle(string ad, string soyad, string email, string yeniSifre)
        {
            try
            {
                // Boş alan kontrolü
                if (string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(soyad) || string.IsNullOrWhiteSpace(yeniSifre))
                {
                    return "Lütfen güncellenecek ad, soyad ve yeni şifre alanlarını boş bırakmayın!";
                }

                KullaniciDal dal = new KullaniciDal();
                Kullanici k = new Kullanici();
                k.Ad = ad;
                k.Soyad = soyad;
                k.Email = email; // Hangi kullanıcının güncelleneceğini belirten anahtar
                k.Sifre = yeniSifre;

                dal.KullaniciGuncelle(k);
                return "Basarili";
            }
            catch (Exception ex)
            {
                return "Güncelleme sırasında hata oluştu: " + ex.Message;
            }
        }
    }
}