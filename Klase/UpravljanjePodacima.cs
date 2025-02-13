﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systemri
{
    public static class UpravljanjePodacima
    {
        public static List<Proizvod> FiltrirajProizvodePoKategoriji(Kategorija_Proizvoda kategorija, List<Proizvod> proizvodi) 
        {
            List<Proizvod> returnMe = new List<Proizvod>();
            foreach (Proizvod proizvod in proizvodi) 
            {
                if (proizvod.Kategorija_proizvoda_ID == kategorija.ID_kategorije_proizvoda) 
                {
                    returnMe.Add(proizvod);
                }
            }
            return returnMe;
        }

        internal static int VratiIDKategorije(Kategorija_Proizvoda kategorija) 
        {
            return kategorija.ID_kategorije_proizvoda;
        }

        internal static string VratiIDMjerneJedinice(Mjerna_Jedinica jedinica) 
        {
            return jedinica.ID_mjerne_jedinice;
        }

        internal static int VratiIDDobavljaca(Dobavljac dobavljac) 
        {
            return dobavljac.ID_dobavljaca;
        }

        internal static int VratiIDProizvodaca(Proizvodac proizvodac) 
        {
            return proizvodac.ID_proizvodaca;
        }

        internal static Proizvod SmanjiKolicinu(Proizvod proizvod, int kolicina) 
        {
            proizvod.Kolicina_proizvoda -= kolicina;
            return proizvod;
        }
        internal static Proizvod PovecajKolicinu(Proizvod proizvod, int kolicina)
        {
            proizvod.Kolicina_proizvoda += kolicina;
            return proizvod;
        }

        internal static Proizvod PrebaciUNovi(Proizvod proizvod)
        {
            Proizvod novi = new Proizvod
            {
                ID_proizvoda = proizvod.ID_proizvoda,
                Naziv_proizvoda = proizvod.Naziv_proizvoda,
                Cijena_proizvoda = proizvod.Cijena_proizvoda,
                Kolicina_proizvoda = proizvod.Kolicina_proizvoda,
                Popust = proizvod.Popust,
                Postotak_popusta = proizvod.Postotak_popusta,
                Kategorija_proizvoda_ID = proizvod.Kategorija_proizvoda_ID,
                Dobavljac_ID = proizvod.Dobavljac_ID,
                Proizvodac_ID = proizvod.Proizvodac_ID,
                Mjerna_jedinica_ID = proizvod.Mjerna_jedinica_ID,
                Podruznica_ID = proizvod.Podruznica_ID
            };

            return novi;
        }

        internal static List<Proizvod> DodajNaRacun(Proizvod odabrani,List<Proizvod> racun, int unos) 
        {
            if (odabrani.Kolicina_proizvoda >= unos)
            {
                UpravljanjePodacima.SmanjiKolicinu(odabrani, unos);
                Proizvod stari = racun.FirstOrDefault(x => x.ID_proizvoda == odabrani.ID_proizvoda);
                if (stari != null)
                {
                    stari = UpravljanjePodacima.PovecajKolicinu(stari, unos);
                }
                else
                {
                    Proizvod novi = PrebaciUNovi(odabrani);
                    novi.Kolicina_proizvoda = unos;
                    racun.Add(novi);
                    racun.OrderBy(x => x.ID_proizvoda);
                }
                return racun;
            }
            else
            {
                throw new Exception("Nedostupna kolicina proizvoda!");
            }
        }

        internal static void OduzmiKolicinuSaProizvoda(List<Proizvod> proizvodi, List<Proizvod> racun) 
        {
            foreach (Proizvod item in racun) 
            {
                Proizvod proizvodZaSmanjenje = proizvodi.FirstOrDefault(x => x.ID_proizvoda == item.ID_proizvoda);
                if(proizvodZaSmanjenje != null) proizvodZaSmanjenje.Kolicina_proizvoda -= item.Kolicina_proizvoda;
            }
        }

        internal static void DodajKolicinuProizodaNaProizvode(List<Proizvod> proizvods, Proizvod proizvod)
        {
            Proizvod proizvodZaPovecanje = proizvods.FirstOrDefault(x => x.ID_proizvoda == proizvod.ID_proizvoda);
            if (proizvodZaPovecanje != null) proizvodZaPovecanje.Kolicina_proizvoda += proizvod.Kolicina_proizvoda;
        }

        internal static void DodajJedanProizodaNaProizvode(List<Proizvod> proizvods, Proizvod proizvod)
        {
            Proizvod proizvodZaPovecanje = proizvods.FirstOrDefault(x => x.ID_proizvoda == proizvod.ID_proizvoda);
            if (proizvodZaPovecanje != null) proizvodZaPovecanje.Kolicina_proizvoda += 1;
        }

        public static string VratiUlogu(int id)
        {
            switch (id)
            {
                case 1: return "Skladistar";
                case 2: return "Blagajnik";
                case 3: return "Upravitelj";
                case 4: return "Administrator";
                default: return null;
            }
        }

        public static int VratiIDUloge(string naziv) 
        {
            switch (naziv) 
            {
                case "Skladistar": return 1;
                case "Blagajnik": return 2;
                case "Upravitelj": return 3;
                case "Administrator": return 4;
                default: return 0;
            }
        }

        public static string GenerirajLozinku(int length)
        {
            const string slova = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder rezultat = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                rezultat.Append(slova[rnd.Next(slova.Length)]);
            }
            return rezultat.ToString();
        }


    }
}
