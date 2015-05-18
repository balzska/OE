using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoSzomszediIszony
{
    
    class CsapRendszer: IEnumerable
    {
        Csap forras;
        int maxSzint;
        int maxCsap;
        
        string visszaErtek = "ny";

        public int MaxCsap
        {
            get { return maxCsap; }
        }

        public int MaxSzint
        {
            get { return maxSzint; }
        }

        public string VisszaErtek
        {
            get { return visszaErtek; }
            set { visszaErtek = value; }
        }
        

        public CsapRendszer(Csap forras, int maxSzint, int maxCsap)
        {
            this.forras = forras;
            this.maxSzint = maxSzint;
            this.maxCsap = maxCsap;
        }

        //************** Enumerator Eleje **************

        public IEnumerator GetEnumerator()
        {
            List<int> Lista = new List<int>();
            _preorder(Lista, forras);
            return Lista.GetEnumerator();
        }

        

        private void _preorder(List<int> l, Csap cs)
        {
            if (cs != null)
            {
                if (visszaErtek == "ny")
                {
                    l.Add(cs.Nyomas);
                }
                /*if (visszaErtek == "b")
                {
                    l.Add(cs.BefolyoViz);
                }
                 */

                if (visszaErtek == "sz")
                {
                    l.Add(cs.Szint);
                }
                if (visszaErtek == "srsz")
                {
                    l.Add(cs.Sorszam);
                }   
            }
            for (int i = 0; i < cs.Gyerekek.Length; i++)
            {
                if (cs.Gyerekek[i] != null)
                {
                    _preorder(l, cs.Gyerekek[i]);
                }
            }
  
        }

        //************** Enumerator Vege **************

        //************** Feltolto Eleje **************
        Random rnd = new Random();
        int srsz = 1;
        public void feltolto(ref Csap f, int sznt)
        {
            if (f.Gyerekek[0] == null)
            {
                for (int i = 0; i < f.Gyerekek.Length; i++)
                {
                    f.Gyerekek[i] = new Csap(rnd.Next(100), 0, rnd.Next(1,maxCsap+1), sznt, srsz);
                    srsz++;
                }
            }
            else
            {
                for (int j = 0; j < f.Gyerekek.Length; j++)
                {
                    feltolto(ref f.Gyerekek[j], sznt);
                }
            }
        }

        public void csapTulaj(ref Csap cs, ref int szamolo)
        {
            if (cs != null)
            {
                if (cs.Gyerekek[0] == null)
                {
                    if (szamolo % 2 == 0)
                    {
                        cs.Tulaj = 1;
                    }
                    else
                        cs.Tulaj = 0;
                    szamolo++;
                }
                else
                {
                    for (int i = 0; i < cs.Gyerekek.Length; i++)
                    {
                        csapTulaj(ref cs.Gyerekek[i], ref szamolo);
                    }
                }
            }
        }

        //************** Feltolto Vege **************


        //************** Viz Betoltes Eleje **************
        //
        //     Elv: postorder bejárás
        //     Képlet: befolyoViz / sum(gyerekekNyomasa) * adottGyerekNyomasa
        //
        //************************************************

        public void vizbetolto(Csap forras)
        {
            if (forras.Gyerekek[0] != null)
            {
                int seged1 = 0;
                for (int i = 0; i < forras.Gyerekek.Length; i++)
                {
                    seged1 = seged1 + forras.Gyerekek[i].Nyomas;
                }

            
                for (int j = 0; j < forras.Gyerekek.Length; j++)
                {
                    if (seged1 > 0)
                    {
                        float seged2 = ((float)forras.BefolyoViz / (float)seged1) * (float)forras.Gyerekek[j].Nyomas;
                        forras.Gyerekek[j].BefolyoViz = (int)seged2;
                    }
                    else
                        forras.Gyerekek[j].BefolyoViz = 0;
                }
                for (int k = 0; k < forras.Gyerekek.Length; k++)
                {
                    vizbetolto(forras.Gyerekek[k]);
                }
            }

        }

        public void vizhozamSzamito(Jatekos j, Csap cs)
        {
            if (cs != null)
            {
                if (cs.Tulaj == j.Azonosito)
                {
                    j.Vizhozam = j.Vizhozam + cs.BefolyoViz;                  
                }
                for (int i = 0; i < cs.Gyerekek.Length; i++)
                {
                    vizhozamSzamito(j, cs.Gyerekek[i]);
                }
            }
        }

        //************** Viz Betoltes Vege **************
    }
}
