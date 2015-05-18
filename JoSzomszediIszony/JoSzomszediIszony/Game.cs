using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoSzomszediIszony
{
    class Game
    {
        Csap gyoker;
        CsapRendszer cs;
        public CsapRendszer Cs
        {
            get { return cs; }
        }

        public Csap Gyoker
        {
            get { return gyoker; }
            set { gyoker = value; }
        }

        public Game(int maxSzint, int maxCsap, int vizBe)
        {
            gyoker = new Csap(100, vizBe , maxCsap, 0, 0);
            cs = new CsapRendszer(gyoker, maxSzint, maxCsap);

            for (int i = 1; i < maxSzint+1; i++)
            {
                cs.feltolto(ref gyoker, i);
            }
            cs.vizbetolto(gyoker);
            int temp = 1;
            cs.csapTulaj(ref gyoker, ref temp);

            bejaro(gyoker);
        }

        public void valtoztato(Csap csap, int valtoztatniKivant, int ertek) 
        {
            if (csap != null)
            {
                if (csap.Sorszam == valtoztatniKivant)
                {
                    csap.Nyomas = ertek;
                }

                else
                {
                    for (int i = 0; i < csap.Gyerekek.Length; i++)
                    {
                        valtoztato(csap.Gyerekek[i], valtoztatniKivant, ertek);
                    }
                }
            }
        }

        public int pontSzamito(Jatekos a, int korabbiVizhozam)
        {
            if (a.Vizhozam > korabbiVizhozam)
            {
                a.Pont = a.Pont + a.Vizhozam - korabbiVizhozam;
            }
            return a.Pont;
        }


        public void VizSzamolo(ref Jatekos jatekos, Csap gyoker)
        {
            if (gyoker!=null)
            {
                if (gyoker.Szint == 0)
                {
                    jatekos.Vizhozam = 0;
                }
                if (gyoker.Tulaj == jatekos.Azonosito)
                {
                    jatekos.Vizhozam = jatekos.Vizhozam + gyoker.BefolyoViz;
                }
                for (int i = 0; i < gyoker.Gyerekek.Length; i++)
                {
                    VizSzamolo(ref jatekos, gyoker.Gyerekek[i]);
                }
            }
        }

        public void bejaro(Csap forras)
        {
            if (forras != null)
            {
                forras.n += Program.nullazva;
                for (int i = 0; i < forras.Gyerekek.Length; i++)
                {
                    bejaro(forras.Gyerekek[i]);
                }
            }
        }     
    }
}
