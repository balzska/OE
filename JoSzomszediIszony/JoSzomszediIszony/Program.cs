using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoSzomszediIszony
{
    class Program
    {
        static void Main(string[] args)
        {
            //**********************************************
            //************ Valós program Eleje *************
            //**********************************************


            int sss = 0; //elso bemeneti paraméter
            int zzz = 0; //második bemeneti paraméter
            int szint = 1;
            int maxSzint = 0;
            int maxCsap = 0;
            Random rnd = new Random();
            int nyertes = -1;

            while (sss != -1)
            {

                //Szint változtatása nyertes kör esetén

                if (szint == 1)
                {
                    maxSzint = 3;
                    maxCsap = 2;
                }
                else
                {
                    if (nyertes == 0)
                    {
                        szint++;
                        int temp = rnd.Next(0, 1);
                        if (temp == 0)
                        {
                            maxSzint++;
                        }
                        else
                            maxCsap++;
                    }
                }
                Console.Clear();
                Console.WriteLine(szint+". SZINT");
                Console.ReadLine();
                Jatekos jatekos = new Jatekos(0); //0 = zold
                Jatekos gep = new Jatekos(1); //1 = piros
                Game cs = new Game(maxSzint, maxCsap, 1000);
                Kirajzolo k = new Kirajzolo(cs.Cs);


                int VIZj;
                int VIZg;

                //összes csap számának elmentése
                int rendszerMeret = 0;

                foreach (int i in cs.Cs)
                {
                    rendszerMeret++;
                }

                k.kirajzolo2(cs.Gyoker);
                cs.VizSzamolo(ref jatekos, cs.Gyoker);
                cs.VizSzamolo(ref gep, cs.Gyoker);

                int korSeged = 1;

                //KEZDŐDIK EGY KÖR

                while (jatekos.Pont < 1000 && gep.Pont < 1000)
                {
                    //1.0 kirajzolunk
                    Console.Clear();
                    Console.WriteLine();
                    k.kirajzolo2(cs.Gyoker);

                    EredmenyJelzo(jatekos, gep);

                    VIZj = jatekos.Vizhozam;
                    VIZg = gep.Vizhozam;

                    //2.0 az adott kör számától függően játékos, vagy gép lép
                    if (korSeged % 2 == 0)
                    {
                        Console.WriteLine("Te jössz!");

                        int gepnullasokelotte = 0;
                        int jatekosnullasokelotte = 0;
                        int gepnullasokutana = 0;
                        int jatekosnullasokutana = 0;

                        //2.1 megszámoljuk hány 0-s kivezetése van a gépnek és a játékosnak a kör előtt
                        nullaSzamolo(1, cs.Gyoker, ref gepnullasokelotte);
                        nullaSzamolo(0, cs.Gyoker, ref jatekosnullasokelotte);
                        //2.2 input
                        try
                        {
                            jatekosInput(cs, ref sss, ref zzz);
                        }
                        catch (rosszSzamformatum r)
                        {
                            Console.WriteLine(r.Message);
                            Console.ReadLine();
                            jatekosInput(cs, ref sss, ref zzz);
                        }
                        catch (vegeAJateknak v)
                        {
                            Console.WriteLine(v.Message);
                            Console.ReadLine();
                            break;
                        }
                        //2.3 az input alapján eltekerjük a kívánt csapot, majd betöltjük a vizet a rendszer tetejébe, végül újraszámoljuk a vízhozamokat
                        cs.valtoztato(cs.Gyoker, sss, zzz);
                        cs.Cs.vizbetolto(cs.Gyoker);
                        cs.VizSzamolo(ref jatekos, cs.Gyoker);
                        cs.VizSzamolo(ref gep, cs.Gyoker);

                        //2.4 megszámoljuk hány 0-s kivezetése van a gépnek és a játékosnak a kör után. ha kell, bónusz és málusz pontot osztunk
                        nullaSzamolo(1, cs.Gyoker, ref gepnullasokutana);
                        nullaSzamolo(0, cs.Gyoker, ref jatekosnullasokutana);
                        nullaOsszesito(jatekos, jatekosnullasokelotte, jatekosnullasokutana, gepnullasokelotte, gepnullasokutana);

                        //2.5 pontot osztunk
                        cs.pontSzamito(jatekos, VIZj);
                        cs.pontSzamito(gep, VIZg);

                        Console.ReadLine();

                        korSeged++;


                    }
                    else
                    {
                        Console.WriteLine("Most a gép jön!");
                        Console.ReadLine();

                        leiratkozo(cs.Gyoker);
                        int legjobbVizhozam = gep.Vizhozam;
                        int legjobbCsap = 0;
                        int legjobbAllas = 0;

                        int gepnullasokelotte = 0;
                        int jatekosnullasokelotte = 0;
                        int gepnullasokutana = 0;
                        int jatekosnullasokutana = 0;


                        nullaSzamolo(1, cs.Gyoker, ref gepnullasokelotte);
                        nullaSzamolo(0, cs.Gyoker, ref jatekosnullasokelotte);

                        //elmentjük a csap eredeti állását, majd 0-100ig 20asával kipróbáljuk az összes lehetőséget
                        //az a lehetőség kerül valós bevitelre, amelyik a legnagyobb vízhozamot eredményezi

                        int TEMPeredetiallas = 0;

                        for (int i = 0; i < rendszerMeret; i++)
                        {
                            kereso(cs.Gyoker, i, ref TEMPeredetiallas);
                            for (int j = 0; j <= 100; j = j + 20)
                            {
                                cs.valtoztato(cs.Gyoker, i, j);
                                cs.Cs.vizbetolto(cs.Gyoker);
                                cs.VizSzamolo(ref jatekos, cs.Gyoker);
                                cs.VizSzamolo(ref gep, cs.Gyoker);

                                if (gep.Vizhozam > legjobbVizhozam)
                                {
                                    legjobbVizhozam = gep.Vizhozam;
                                    legjobbCsap = i;
                                    legjobbAllas = j;
                                }

                            }
                            //visszaírjuk az eredeti értéket
                            cs.valtoztato(cs.Gyoker, i, TEMPeredetiallas);
                            cs.Cs.vizbetolto(cs.Gyoker);
                            cs.VizSzamolo(ref jatekos, cs.Gyoker);
                            cs.VizSzamolo(ref gep, cs.Gyoker);

                        }

                        //valóban beállítjuk a legjobbnak vélt lehetőséget

                        cs.bejaro(cs.Gyoker);
                        cs.valtoztato(cs.Gyoker, legjobbCsap, legjobbAllas);
                        cs.Cs.vizbetolto(cs.Gyoker);
                        cs.VizSzamolo(ref jatekos, cs.Gyoker);
                        cs.VizSzamolo(ref gep, cs.Gyoker);

                        nullaSzamolo(1, cs.Gyoker, ref gepnullasokutana);
                        nullaSzamolo(0, cs.Gyoker, ref jatekosnullasokutana);
                        nullaOsszesito(gep, gepnullasokelotte, gepnullasokutana, jatekosnullasokelotte, jatekosnullasokutana);

                        cs.pontSzamito(jatekos, VIZj);
                        cs.pontSzamito(gep, VIZg);

                        Console.WriteLine("A gép a " + legjobbCsap + ". csapot változtatta " + legjobbAllas + "-ra");
                        Console.ReadLine();
                        korSeged++;

                    }

                }


                //GAME OVER
                Console.Clear();
                Console.WriteLine();
                k.kirajzolo2(cs.Gyoker);

                EredmenyJelzo(jatekos, gep);

                Vege(gep, jatekos, ref nyertes);
                //GAME OVER VEGE

                //*********************************************
                //************ Valós program Vége *************
                //*********************************************
            }
        }


        public static void nullaSzamolo(int azonosito, Csap cs, ref int kimento)
        {
            if (cs != null)
            {
                if (cs.BefolyoViz == 0)
                {
                    if (cs.Tulaj == azonosito)
                    {
                        kimento++;
                    }
                }
                for (int i = 0; i < cs.Gyerekek.Length; i++)
                {
                    nullaSzamolo(azonosito, cs.Gyerekek[i], ref kimento);
                }
            }
        }

        public static void kereso(Csap cs, int keresett, ref int mentes)
        {
            if (cs != null)
            {
                if (cs.Sorszam == keresett)
                {
                    mentes = cs.Nyomas;
                }

                else
                {
                    for (int i = 0; i < cs.Gyerekek.Length; i++)
                    {
                        kereso(cs.Gyerekek[i], keresett, ref mentes);
                    }
                }
            }
        }

        public static void leiratkozo(Csap forras)
        {
            if (forras != null)
            {
                forras.n -= nullazva;
                for (int i = 0; i < forras.Gyerekek.Length; i++)
                {
                    leiratkozo(forras.Gyerekek[i]);
                }
            }
        }

        //KIÍRÓ FÜGGVÉNYEK

        public static void nullazva(int aaa)
        {
            Console.WriteLine(aaa + " nulla lett!");
            Console.ReadLine();
        }

        public static void jatekosInput(Game cs, ref int sss, ref int zzz)
        {
            Console.Write("Melyiket? ");
            sss = int.Parse(Console.ReadLine());
            if (sss == -1)
            {
                throw new vegeAJateknak();
            }
            Console.Write("Mire? ");
            zzz = int.Parse(Console.ReadLine());
            if (zzz < 0 || zzz > 100)
            {
                throw new rosszSzamformatum();
            }
        }

        public static void EredmenyJelzo(Jatekos jatekos, Jatekos gep)
        {
            Console.WriteLine();
            Console.WriteLine("Játekos pont: " + jatekos.Pont);
            Console.WriteLine("Gép pont: " + gep.Pont);
            Console.WriteLine("Játekos víz: " + jatekos.Vizhozam);
            Console.WriteLine("Gép víz: " + gep.Vizhozam);
        }


        public static void nullaOsszesito(Jatekos dominansjatekos, int eelotte, int eutana, int kelotte, int kutana)
        {
            if (eelotte < eutana)
            {
                dominansjatekos.Pont = dominansjatekos.Pont - ((eutana - eelotte) * 100);
                if (dominansjatekos.Azonosito == 0)
                {
                    Console.WriteLine("Kinulláztad " + (eutana - eelotte) + " db csapodat. " + ((eutana - eelotte) * 100) + " máluszpontot kaptál!");
                }
                else
                {
                    Console.WriteLine("A gép kinullázta " + (eutana - eelotte) + " db csapját. " + ((eutana - eelotte) * 100) + " máluszpontot kapott!");
                }
            }
            if (kelotte < kutana)
            {
                dominansjatekos.Pont = dominansjatekos.Pont + ((kutana - kelotte) * 100);
                if (dominansjatekos.Azonosito == 0)
                {
                    Console.WriteLine("Kinulláztad a gép " + (kutana - kelotte) + " db csapját. " + ((kutana - kelotte) * 100) + " bónuszpontot kaptál!");
                }
                else
                {
                    Console.WriteLine("A gép kinullázta " + (kutana - kelotte) + " db csapodat. " + ((kutana - kelotte) * 100) + " bónuszpontot kapott!");
                }
            }
        }

        public static void Vege(Jatekos gep, Jatekos jatekos, ref int nyertes)
        {
            if (jatekos.Pont > gep.Pont)
            {
                nyertes = 0;
                Console.WriteLine();
                Console.WriteLine("****************************************");
                Console.WriteLine("*Gratulálok! Te nyertél!****************");
                Console.WriteLine("****************************************");
                Console.WriteLine();
                Console.WriteLine("GÉP: " + gep.Pont + " pont");
                Console.WriteLine("Játékos: " + jatekos.Pont + " pont");
                Console.WriteLine();
                Console.WriteLine("Ugrás a következő szintre!");
                Console.ReadLine();
            }
            if (jatekos.Pont < gep.Pont)
            {
                nyertes = 1;
                Console.WriteLine();
                Console.WriteLine("****************************************");
                Console.WriteLine("*Sajnálom, a gép nyert!*****************");
                Console.WriteLine("****************************************");
                Console.WriteLine();
                Console.WriteLine("GÉP: " + gep.Pont + " pont");
                Console.WriteLine("Játékos: " + jatekos.Pont + " pont");
                Console.WriteLine();
                Console.WriteLine("Maradunk ezen a szinten!");
                Console.ReadLine();
                
            }
            else
            {
                nyertes = 1;
                Console.WriteLine();
                Console.WriteLine("****************************************");
                Console.WriteLine("*Döntetlen!*****************");
                Console.WriteLine("****************************************");
                Console.WriteLine();
                Console.WriteLine("GÉP: " + gep.Pont + " pont");
                Console.WriteLine("Játékos: " + jatekos.Pont + " pont");
                Console.WriteLine();
                Console.WriteLine("Maradunk ezen a szinten!");
                Console.ReadLine();
                
            }
        }

    }
}
