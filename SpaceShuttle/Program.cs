using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpaceShuttle
{
    struct Shuttles
    {
        public string Kuldkod;
        public string KilovDatum;
        public string SikloNev;
        public int Nap;
        public int Ora;
        public string LegitamaszNev;
        public int LegenysegSzam;

        public Shuttles(string Input)
        {
            var Vag = Input.Split(';');
            this.Kuldkod = Vag[0];
            this.KilovDatum = Vag[1];
            this.SikloNev = Vag[2];
            this.Nap = int.Parse(Vag[3]);
            this.Ora = int.Parse(Vag[4]);
            this.LegitamaszNev = Vag[5];
            this.LegenysegSzam = int.Parse(Vag[6]);
        }
    }

    internal class Program
    {

        static List<Shuttles> Shuttles = new List<Shuttles>();


        static void Main(string[] args)
        {
            Feladat02();
            Feladat03();
            Feladat04();
            Feladat05();
            Feladat06();
            Feladat07();
            Feladat08();
            Feladat09();
            Feladat10();

            Console.ReadKey();
        }

        private static void Feladat10()
        {
            List<string> Siklo = new List<string>();
            foreach (var Shuttle in Shuttles)
            {
                if (!Siklo.Contains(Shuttle.SikloNev)) Siklo.Add(Shuttle.SikloNev);
            }
            StreamWriter SW = new StreamWriter(@"ursiklok.txt", false, Encoding.UTF8);

            List<double> ShuttleTime = new List<double>();
            double Count = 0;
            foreach (var Counter in Siklo)
            {
                foreach (var Shuttle in Shuttles)
                {
                    if (Counter==Shuttle.SikloNev) Count += Shuttle.Nap;
                }
                ShuttleTime.Add(Count);
            }

            foreach (var Write in Siklo)
            {
                SW.WriteLine($"{Write} : ");
            }
            SW.Close();
        }

        private static void Feladat09()
        {
            double KozpontNum = 0;
            foreach (var Allomas in Shuttles)
            {
                if (Allomas.LegitamaszNev == "Kennedy") KozpontNum++;
            }
            double Szazalaek = (KozpontNum*100) / Shuttles.Count;
            Console.WriteLine($"9. Feladat: \n\t A küldetések {Szazalaek:0.00}%-a fejeződött be az kennedy űrközpontban.");
        }

        private static void Feladat08()
        {
            Console.Write("8. Feladat: \n\t Évszám: ");
            string UserRequest = Console.ReadLine();
            int KuldSzam = 0;
            foreach (var Kuld in Shuttles)
            {
                if (Kuld.KilovDatum.Contains(UserRequest)) KuldSzam++;
            }
            Console.WriteLine(KuldSzam>0? $"\t Ebben az évben {KuldSzam} küldetés volt." : "\t Ebben az évben nem indult küldetés");
        }

        private static void Feladat07()
        {
            int time = int.MinValue;
            string Nev = "", KuldKod = "";
            foreach (var item in Shuttles)
            {
                if(item.Nap*24+item.Ora>time)
                {
                    Nev = item.SikloNev;
                    KuldKod = item.Kuldkod;
                    time = item.Nap * 24 + item.Ora;
                }
            }
            Console.WriteLine($"7. Feladat: \n\t A leghosszabb ideig a {Nev} volt az űrben a {KuldKod} küldetés során. \n\t Összesen {time} órát volt távol a Földtől");
        }

        private static void Feladat06()
        {
            int Astronaut = 0;
            foreach (var item in Shuttles)
            {
                if (item.SikloNev == "Columbia" && item.KilovDatum == "2003.01.16") Astronaut = item.LegenysegSzam;
            }
            Console.WriteLine($"6. Feladat: \n\t {Astronaut} asztronauta volt a Columbia fedélzetén annak utolsó útján.");
        }

        private static void Feladat05()
        {
            int Utas = 0;
            foreach (var utas in Shuttles)
            {
                if (utas.LegenysegSzam < 5) Utas++;
            }
            Console.WriteLine($"5. Feladat: \n\t Összesen {Utas} alkalommal küldtek kevesebb, mint 5 embert az űrbe.");
        }

        private static void Feladat04()
        {
            int OsszUtas = 0;
            foreach (var Utas in Shuttles)
            {
                OsszUtas += Utas.LegenysegSzam;
            }
            Console.WriteLine($"4. Feladat: \n\t {OsszUtas} utas indult az űrbe összesen.");
        }

        private static void Feladat03()
        {
            Console.WriteLine($"3. Feladat: \n\t Összesen {Shuttles.Count} alkalommal indítottak űrhajót.");
        }

        private static void Feladat02()
        {
            StreamReader sr = new StreamReader(@"kuldetesek.csv", Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                Shuttles.Add(new Shuttles(sr.ReadLine()));
            }
            sr.Close();
        }
    }
}
