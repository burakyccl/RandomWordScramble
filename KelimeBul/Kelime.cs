using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KelimeBul
{
    class Kelime
    {
        public string BuKelime { get; }
        public string SiraliKelime { get; }

        public Kelime(string gelenKelime)  //constructor
        {
            this.BuKelime = gelenKelime; // Kelimenin kendisi : top
            SiraliKelime = kelimeHarfSirala(gelenKelime); // Sortlu hali : opt
        }

        public string kelimeHarfSirala(string kelime) // Gelen Stringleri kelimleri abc.. olarak duzenli sırala fonksiyonu
        {                                              // Boylece Karsilastirma yapilabilecek.
            char[] kelimeHarfler = kelime.ToCharArray();
            Array.Sort(kelimeHarfler);
            return new string(kelimeHarfler);
        }
    }
}
