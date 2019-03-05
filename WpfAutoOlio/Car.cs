using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WpfAutoOlio
{
    public class Car
    {
        // Ominaisuudet
        public string Merkki { get; set; }
        public string Malli { get; set; }
        public string Vari { get; set; }
        public string Vaihteisto { get; set; }

        public int VaihteidenMaara {
            get
            {
                return vaihteidenMaara;
            }
            set
            {
                if (Regex.IsMatch(value.ToString(), "[4-9]"))
                {
                    vaihteidenMaara = value;
                } else
                {
                    if (value == 0)
                    {
                        vaihteidenMaara = 0;
                    }
                    else
                    {
                        vaihteidenMaara = 0;
                        throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }
        private int vaihteidenMaara;
        public int Hevosvoimat { get; set; }

        public int Huippunopeus {
            get
            {
                return huippunopeus;
            }
            set
            {
                if ((value > 0) && (value <= 400))
                {
                    huippunopeus = value;
                } else
                {
                    huippunopeus = 0;
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        public int Nopeus { get; set; }

        public Boolean Kaynnissa { get; set; }

        // Huippunopeus
        private int huippunopeus;
        public void asetaHuippunopeus(int xHuippunopeus)
        {
            if ((xHuippunopeus >= 0) && (xHuippunopeus <= 400))
            {
                huippunopeus = xHuippunopeus;
            } else
            {
                huippunopeus = 0;
                throw new ArgumentOutOfRangeException();
            }
            
        }
        public int haeHuippunopeus()
        {
            return huippunopeus;
        }

        // Kiihdytys ja jarrutus
        public void Kiihdyta()
        {
            Nopeus++;
        }

        public void Jarruta()
        {
            Nopeus--;
        }

        // Moottorin käynnistys ja sammutus
        public void KaynnistaMoottori()
        {
            Kaynnissa = true;
        }

        public void SammutaMoottori()
        {
            Kaynnissa = false;
        }
    }
}
