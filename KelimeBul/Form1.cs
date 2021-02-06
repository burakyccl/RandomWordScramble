using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace KelimeBul
{
    public partial class Form1 : Form
    {
        // Sozluk konumunu degistirmeyi unutmayin. Databaseimden cok memnun degilim ama bulabildigim buydu.

        public const string sozlukKonum = @"C:\Users\182802019\source\repos\KelimeBul\KelimeBul\";
        public const string sozlukAd = @"sozluk.txt";

        List<Kelime> bulunacakKelimeList = new List<Kelime>();
        List<Kelime> sozlukList = new List<Kelime>();

        char[] harfUnsuz = "bcçdfgğhjklmnprsştvyz".ToCharArray();
        char[] harfUnlu = "aeıioöuü".ToCharArray();
        char[] alfabe = "aeıioöuübcçdfgğhjklmnprsştvyz".ToCharArray();
        Random r = new Random();
        int i = 0;
        int puan = 0;
        string kelimeRandom;

        public Form1()
        {
            InitializeComponent();
        }

        private void randomBtn_Click(object sender, EventArgs e)
        {
            kelimeRandom = string.Empty;
            bulunacakKelimeList.Clear();
            label1.Text = "";
            i = 0;
            
            while (i < 5)       //8 adet(3 unlu 5 unsuz) random harf atama ve yazdirma.
            {
                int x = r.Next(harfUnsuz.Length);
                kelimeRandom += harfUnsuz[x];
                label1.Text += "{ " + harfUnsuz[x] + " } ";
                i++;
            }
            i = 0;
            while (i < 3)
            {
                int x = r.Next(harfUnlu.Length);
                kelimeRandom += harfUnlu[x];
                label1.Text += "{ " + harfUnlu[x] + " } ";
                i++;
            }
            
            kelimeBulBtn.Enabled = true;
        }

        private void kelimeBulBtn_Click(object sender, EventArgs e)
        {
            string temp = kelimeRandom;
            for (i = 0; i < 29; i++)
            {
                kelimeRandom += alfabe[i];      //1 adet Joker harfi de alacak sekilde harflerin tüm kombinasyonunu cikarma.
                var kombinasyon =
                    from n in Enumerable.Range(0, 1 << kelimeRandom.Length)
                    let p =
                        from b in Enumerable.Range(1, kelimeRandom.Length)
                        select (n & (1 << b)) == 0 ? (char?) null : kelimeRandom[b]
                    select string.Join(string.Empty, p);

                foreach (var kombi in kombinasyon)
                {
                    bulunacakKelimeList.Add(new Kelime(kombi.Trim()));  //Listeye Atama
                }
                kelimeRandom = temp;
            }

            using (StreamReader sr = new StreamReader(sozlukKonum + sozlukAd))
            {
                // Sozluk Okumasi

                while (sr.Peek() != -1)
                {
                    string satir = sr.ReadLine();
                    sozlukList.Add(new Kelime(satir)); // Satirlarin okunup hem normal hem sortlu hallerinin listeye atama
                }
            }
            
            foreach (Kelime kelime in bulunacakKelimeList)          //Her kombinasyonun once uzunluk ardindan sortlu uzunluk esitlik karsilastirilmasi yapilarak
            {                                                       //Kelimenin Listboxa yazdirilmasi
                List<Kelime> uzunlukEsitKelimeList = sozlukList.FindAll(sozlukKelime => sozlukKelime.BuKelime.Length == kelime.BuKelime.Length);

                foreach (Kelime uzunlukEsitKelime in uzunlukEsitKelimeList)
                {
                    if (uzunlukEsitKelime.SiraliKelime.Equals(kelime.SiraliKelime))  // opt == opt abcd ise Top, pot yazdir
                    {
                        if (uzunlukEsitKelime.BuKelime.Length == 7)
                        {
                            if (listBox6.Items.Contains(uzunlukEsitKelime.BuKelime))
                            {
                                continue;
                            }
                            listBox6.Items.Add(uzunlukEsitKelime.BuKelime);
                        }
                        else if (uzunlukEsitKelime.BuKelime.Length == 6)
                        {
                            if (listBox5.Items.Contains(uzunlukEsitKelime.BuKelime))
                            {
                                continue;
                            }
                            listBox5.Items.Add(uzunlukEsitKelime.BuKelime);
                        }
                        else if (uzunlukEsitKelime.BuKelime.Length == 5)
                        {
                            if (listBox4.Items.Contains(uzunlukEsitKelime.BuKelime))
                            {
                                continue;
                            }
                            listBox4.Items.Add(uzunlukEsitKelime.BuKelime);
                        }
                        else if (uzunlukEsitKelime.BuKelime.Length == 4)
                        {
                            if (listBox3.Items.Contains(uzunlukEsitKelime.BuKelime))
                            {
                                continue;
                            }
                            listBox3.Items.Add(uzunlukEsitKelime.BuKelime);
                        }
                        else if (uzunlukEsitKelime.BuKelime.Length == 3)
                        {
                            if (listBox2.Items.Contains(uzunlukEsitKelime.BuKelime))
                            {
                                continue;
                            }
                            listBox2.Items.Add(uzunlukEsitKelime.BuKelime);
                        }
                    }
                }
            }
            kelimeBulBtn.Enabled = false;
        }

        private void SifirlaBtn_Click(object sender, EventArgs e)       //Sifirla 
        {
            Application.Restart();
            Environment.Exit(0);
        }

        // Puan Hesaplamasi
        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            puan += 3;
            puanLbl.Text = puan.ToString();
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            puan += 4;
            puanLbl.Text = puan.ToString();
        }

        private void listBox4_DoubleClick(object sender, EventArgs e)
        {
            puan += 5;
            puanLbl.Text = puan.ToString();
        }

        private void listBox5_DoubleClick(object sender, EventArgs e)
        {
            puan += 7;
            puanLbl.Text = puan.ToString();
        }

        private void listBox6_DoubleClick(object sender, EventArgs e)
        {
            puan += 9;
            puanLbl.Text = puan.ToString();
        }

        private void listBox7_DoubleClick(object sender, EventArgs e)
        {
            puan += 11;
            puanLbl.Text = puan.ToString();
        }

        private void listBox8_DoubleClick(object sender, EventArgs e)
        {
            puan += 15;
            puanLbl.Text = puan.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Puan Hesaplamak İçin Bulunan Kelimenin Üstüne Çift Tıklayınız.");
        }

        private void boxtemizleBtn_Click(object sender, EventArgs e)    //Listbox clear
        {
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            listBox7.Items.Clear();
            listBox8.Items.Clear();
        }
    }
}
