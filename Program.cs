/*
 * Da bi primer radio, potrebno je napraviti bazu podataka koristeci prilozenu .sql skriptu.
 * 
 * Program je stukturiran na sledeci nacin:
 * 
 *     DAO     - Rad sa bazom podataka
 *     MODEL   - Klase koje modeluju entitete
 *     UI      - Interakcija sa korisnikom
 *     UTILS   - Pomocne datoteke
 *     PROGRAM - Glavna petlja programa
 */

using System;
using System.Data.SqlClient;
using Termin09Primer03.ui;
using Termin09Primer03.utils;

namespace Termin09Primer03
{
    class Program
    {
        public static SqlConnection conn;

        static void LoadConnection()
        {
            try
            {
                // Ostvarivanje konekcije
                string connectionStringZaPoKuci = "Server=.\\SQLEXPRESS;Database=DotNetKurs;Integrated Security=True;MultipleActiveResultSets=True";
                string connectionStringNaKursu = "Server=.\\SQLEXPRESS;Database=DotNetKurs;User ID=sa;Password=SqlServer2016;MultipleActiveResultSets=True";

                // Parametar "MultipleActiveResultSets=True" je neophodan kada zelimo da imamo istovremeno
                // otvorena dva data readera ka bazi podataka. Zasto je u ovom programu to neophodno?
                conn = new SqlConnection(connectionStringZaPoKuci);
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void Main(string[] args)
        {
            LoadConnection();

            int odluka = -1;
            while (odluka != 0)
            {
                IspisiMenu();

                Console.Write("opcija:");
                odluka = IO.OcitajCeoBroj();

                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz iz programa");
                        break;
                    case 1:
                        StudentUI.Menu();
                        break;
                    case 2:
                        PredmetUI.Menu();
                        break;
                    case 3:
                        PohadjaUI.Menu();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda");
                        break;
                }
            }
        }

        // Ispis teksta osnovnih opcija
        public static void IspisiMenu()
        {
            Console.WriteLine("Studentska Sluzba - Osnovne opcije:");
            Console.WriteLine("\tOpcija broj 1 - Rad sa studentima");
            Console.WriteLine("\tOpcija broj 2 - Rad sa predmetima");
            Console.WriteLine("\tOpcija broj 3 - Rad sa pohadjanjem predmeta");
            Console.WriteLine("\t\t ...");
            Console.WriteLine("\tOpcija broj 0 - IZLAZ IZ PROGRAMA");
        }
    }
}
