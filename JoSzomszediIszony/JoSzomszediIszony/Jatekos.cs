using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoSzomszediIszony
{
    class Jatekos
    {
        int pont;

        public int Pont
        {
            get { return pont; }
            set {   pont = value; }
        }

        int vizhozam;

        public int Vizhozam
        {
            get { return vizhozam; }
            set { vizhozam = value; }
        }

        int nullasCsapok;
        //ki kell majd nullázni
        public int NullasCsapok
        {
            get { return nullasCsapok; }
            set { nullasCsapok = value; }
        }

        int azonosito;

        public int Azonosito
        {
            get { return azonosito; }
        }

        public Jatekos(int azonosito)
        {
            nullasCsapok = 0;
            pont = 0;
            vizhozam = 0;
            this.azonosito = azonosito;
        }

        
    }
}
