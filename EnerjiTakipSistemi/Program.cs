using System;
using System.Windows.Forms;

namespace EnerjiTakipSistemi
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Programın ilk açacağı formu buradan ayarlıyoruz
            Application.Run(new FrmGiris());
        }
    }
}