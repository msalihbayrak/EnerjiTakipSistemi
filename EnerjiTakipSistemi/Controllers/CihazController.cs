using EnerjiTakipSistemi.DataAccess;
using EnerjiTakipSistemi.Models;
using System;
using System.Data;

namespace EnerjiTakipSistemi.Controllers
{
    public class CihazController
    {
        public string CihazEkle(string cihazAdi, string gucMetni, string adetMetni)
        {
            // 1. Boşluk Kontrolü
            if (string.IsNullOrEmpty(cihazAdi) || string.IsNullOrEmpty(gucMetni) || string.IsNullOrEmpty(adetMetni))
            {
                return "Lütfen tüm alanları doldurun!";
            }

            // 2. Sayı Kontrolü (Güç ve Adet sadece rakam olmalı)
            int guc, adet;
            if (!int.TryParse(gucMetni, out guc) || !int.TryParse(adetMetni, out adet))
            {
                return "Güç ve Adet kısımlarına sadece rakam giriniz!";
            }

            // 3. Her şey tamamsa modeli oluştur ve DAL'a yolla
            Cihaz yeniCihaz = new Cihaz();
            yeniCihaz.CihazAdi = cihazAdi;
            yeniCihaz.GucTuketimi = guc;
            yeniCihaz.Adet = adet;

            CihazDal dal = new CihazDal();
            dal.CihazEkle(yeniCihaz);

            return "Basarili";
        }

        public DataTable CihazListesiniAl()
        {
            CihazDal dal = new CihazDal();
            return dal.CihazlariGetir();
        }

        public void CihazSil(int id)
        {
            CihazDal dal = new CihazDal();
            dal.CihazSil(id);
        }
    }
}