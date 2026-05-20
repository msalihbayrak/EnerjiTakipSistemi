using System;

namespace EnerjiTakipSistemi.Models
{
    public class EnerjiVerisi
    {
        public int Id { get; set; }
        public string EnerjiTuru { get; set; } // Elektrik, Su, Doğalgaz vb.
        public decimal Tutar { get; set; }     // Fatura tutarı veya miktar
        public DateTime Tarih { get; set; }    // Kayıt tarihi

        // Eğer ileride verilerin hangi kullanıcıya ait olduğunu ayırmak istersek diye:
        public string KullaniciEmail { get; set; }
    }
}