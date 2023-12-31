using System;
using System.Collections.Generic;
using Termin09Primer03.dao;
using Termin09Primer03.model;
using Termin09Primer03.utils;

namespace Termin09Primer03.ui
{
    class PohadjaUI
    {
        private static void IspisiMenu()
        {
            Console.WriteLine("Rad sa predmetima studenta - opcije:");
            Console.WriteLine("\tOpcija broj 1 - predmeti koje student pohadja");
            Console.WriteLine("\tOpcija broj 2 - studenti koji pohadjaju predmet");
            Console.WriteLine("\tOpcija broj 3 - dodavanje studenta na predmet");
            Console.WriteLine("\tOpcija broj 4 - uklanjanje studenta sa predmeta");
            Console.WriteLine("\t\t ...");
            Console.WriteLine("\tOpcija broj 0 - Nazad");
        }

        public static void Menu()
        {
            int odluka = -1;
            while (odluka != 0)
            {
                IspisiMenu();
                Console.Write("opcija:");
                odluka = IO.OcitajCeoBroj();
                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz");
                        break;
                    case 1:
                        IspisiPredmeteZaStudenta();
                        break;
                    case 2:
                        IspisiStudenteZaPredmet();
                        break;
                    case 3:
                        dodajStudentaNaPredmet();
                        break;
                    case 4:
                        ukloniStudentaSaPredmeta();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda");
                        break;
                }
            }
        }

        private static void IspisiPredmeteZaStudenta()
        {
            // Najpre pronadjemo studenta za kojeg zelimo ispis predmeta
            Student student = StudentUI.PronadjiStudenta();

            if (student != null)
            {
                // Ukoliko smo ga pronasli, zahtevamo od baze listu predmeta ovog studenta
                List<Predmet> predmeti = PohadjaDAO.GetPredmetiByStudentId(
                        Program.conn, student.Id);

                // Ispisujemo dobijenu listu predmeta
                foreach(Predmet p in predmeti)
                {
                    Console.WriteLine(p);
                }
            }
        }

        private static void IspisiStudenteZaPredmet()
        {
            // Najpre pronadjemo predmet za koji zelimo ispis studenata
            Predmet predmet = PredmetUI.PronadjiPredmet();
            if (predmet != null)
            {
                // Ukoliko smo pronasli predmet, zahtevamo od baze listu studenata koji ga pohadjaju
                List<Student> studenti = PohadjaDAO.GetStudentiByPredmetId(
                        Program.conn, predmet.Id);

                // Ispisujemo dobijenu listu studenata
                foreach(Student s in studenti)
                {
                    Console.WriteLine(s);
                }
            }
        }

        private static void dodajStudentaNaPredmet()
        {
            // Najpre pronadjemo studenta kojeg zelimo da dodamo na predmet
            Student student = StudentUI.PronadjiStudenta();

            // Pronadjemo predmet na koji zelimo da dodamo studenta
            Predmet predmet = PredmetUI.PronadjiPredmet();

            // Ukoliko je uspesan pronalazak i predmeta i studenta
            if (student != null && predmet != null)
            {
                // Onda njihovu relaciju uspostavljamo ubacivanjem novog sloga u tabelu pohadja
                PohadjaDAO.Add(Program.conn, student.Id, predmet.Id);
            }
        }

        private static void ukloniStudentaSaPredmeta()
        {
            // Najpre pronadjemo studenta kojeg zelimo da uklonimo sa predmeta
            Student student = StudentUI.PronadjiStudenta();

            // Pronadjemo predmet sa kojeg zelimo da ukloniko studenta
            Predmet predmet = PredmetUI.PronadjiPredmet();

            // Ukoliko je uspesan pronalazak i predmeta i studenta
            if (student != null && predmet != null)
            {
                // Onda njihovu relaciju brisemo izbacivanjem sloga iz tabele pohadja
                PohadjaDAO.Delete(Program.conn, student.Id, predmet.Id);
            }
        }
    }
}
