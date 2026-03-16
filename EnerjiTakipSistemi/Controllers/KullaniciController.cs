using System;
using EnerjiTakipSistemi.Entities; 
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
    }
}