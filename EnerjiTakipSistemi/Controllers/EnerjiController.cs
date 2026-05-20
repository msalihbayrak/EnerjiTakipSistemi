using System;
using System.Data;
using EnerjiTakipSistemi.Models;
using EnerjiTakipSistemi.DataAccess;

namespace EnerjiTakipSistemi.Controllers
{
    public class EnerjiController
    {
        // 1. Veri Ekleme Metodu
        public string TuketimEkle(string enerjiTuru, string tutarMetni, DateTime tarih)
        {
            if (string.IsNullOrEmpty(enerjiTuru) || string.IsNullOrEmpty(tutarMetni))
            {
                return "Lütfen enerji türünü ve tutarı boş bırakmayın!";
            }

            decimal tutar;
            if (!decimal.TryParse(tutarMetni, out tutar))
            {
                return "Lütfen tutar kısmına sadece rakam giriniz!";
            }

            EnerjiVerisi yeniVeri = new EnerjiVerisi();
            yeniVeri.EnerjiTuru = enerjiTuru;
            yeniVeri.Tutar = tutar;
            yeniVeri.Tarih = tarih;

            EnerjiVerisiDal dal = new EnerjiVerisiDal();
            dal.VeriEkle(yeniVeri);

            return "Basarili";
        }

        // 2. Tabloyu Arayüze Taşıyan Metot
        public DataTable EnerjiListesiniAl()
        {
            EnerjiVerisiDal dal = new EnerjiVerisiDal();
            return dal.VerileriGetir();
        }

        // 3. Silme İşlemini Yapan Metot
        public void TuketimSil(int id)
        {
            EnerjiVerisiDal dal = new EnerjiVerisiDal();
            dal.VeriSil(id);
        }
        // YENİ EKLENEN: Grafik verilerini Dal'dan alıp Form'a taşıyan metot
        public DataTable GrafikIcinVeriAl()
        {
            EnerjiVerisiDal dal = new EnerjiVerisiDal();
            return dal.GrafikVerileriniGetir();
        }
        // Veri Güncelleme Kontrolü
        public string TuketimGuncelle(int id, string enerjiTuru, string tutarText, DateTime tarih)
        {
            try
            {
                // Boş alan kontrolü
                if (string.IsNullOrWhiteSpace(enerjiTuru) || string.IsNullOrWhiteSpace(tutarText))
                    return "Enerji türü ve tutar boş bırakılamaz!";

                // Tutarın rakam olup olmadığını kontrol et
                if (!decimal.TryParse(tutarText, out decimal tutar))
                    return "Lütfen tutar kısmına sadece geçerli bir rakam giriniz!";

                // Model ve Dal bağlantısı
                EnerjiVerisiDal dal = new EnerjiVerisiDal();
                EnerjiVerisi veri = new EnerjiVerisi();
                veri.EnerjiTuru = enerjiTuru;
                veri.Tutar = tutar;
                veri.Tarih = tarih;

                dal.VeriGuncelle(id, veri);
                return "Basarili";
            }
            catch (Exception ex)
            {
                return "Güncelleme sırasında hata oluştu: " + ex.Message;
            }
        }
    }
}