using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoSzomszediIszony
{
    public delegate void NullaLett(int jatekos);

    class Csap
    {

        /*
            tulaj = 0 -> zold
            tulaj = 1 -> piros
            alap = -1
         */
        
        int tulaj;

        public int Tulaj
        {
            get { return tulaj; }
            set { tulaj = value; }
        }

        int nyomas;
        int befolyoViz;
        int szint;
        int sorszam;
        Csap[] gyerekek;

        public event NullaLett n;

        public int Sorszam
        {
            get { return sorszam; }
        }
        
        public int Szint
        {
            get { return szint; }
        }


        public Csap[] Gyerekek
        {
            get { return gyerekek; }
        }

        public int Nyomas
        {
            get { return nyomas; }
            set 
            { 
                nyomas = value;               
            }
        }

        public int BefolyoViz
        {
            get { return befolyoViz; }
            set 
            {
                if (befolyoViz != value)
                {
                    befolyoViz = value;
                    if (n != null)
                    {
                        if (befolyoViz == 0)
                        {
                            n(this.sorszam);
                        }
                    }
                }
            }
        }

        public Csap(int nyomas, int befolyoViz, int meret, int szint, int sorszam)
        {

            this.nyomas = nyomas;
            this.befolyoViz = befolyoViz;
            this.gyerekek = new Csap[meret];
            this.szint = szint;
            this.sorszam = sorszam;
            tulaj = -1;

        }
    }
}
