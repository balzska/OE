using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JoSzomszediIszony
{
    class Kirajzolo
    {
        CsapRendszer rndsz;
        int seged;
        
        public CsapRendszer Rndsz
        {
            get { return rndsz; }
        }

        public Kirajzolo(CsapRendszer cs)
        {
            this.rndsz = cs;
            seged = rndsz.MaxSzint;
        }

        public void kirajzolo2(Csap cs)
        {
            //************ MŰKÖDŐ FELTÖLTÉS**************
            if (cs.Gyerekek[0] == null)
            {
                
                if (cs.Tulaj == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;                
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

            }

            Console.WriteLine();
            Console.Write("|"+cs.Sorszam + "| " + cs.Szint + " " + cs.Nyomas+" "+cs.BefolyoViz);
            Console.ResetColor();
            if (cs.Gyerekek != null)
            {
                for (int i = 0; i < cs.Gyerekek.Length; i++)
                {
                    if (cs.Gyerekek[i] != null)
                    {
                       
                        kirajzolo2(cs.Gyerekek[i]);
                    }
                }
            }
        }
    }
}
