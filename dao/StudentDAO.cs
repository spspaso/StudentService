using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Termin09Primer03.model;

namespace Termin09Primer03.dao
{
    class StudentDAO
    {
        // Trazimo studenta sa datim ID-jem
        public static Student GetStudentById(SqlConnection conn, int id)
        {
            Student student = null;
            try
            {
                string query = "SELECT indeks, ime, prezime, grad " +
                                "FROM studenti WHERE student_id = "
                                + id;
                SqlCommand cmd = new SqlCommand(query, conn);

                // Da li je ovde moguc SQL injection i zasto nije?
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    string indeks = (string)rdr["indeks"];
                    string ime = (string)rdr["ime"];
                    string prezime = (string)rdr["prezime"];
                    string grad = (string)rdr["grad"];

                    student = new Student(id, indeks, ime, prezime, grad);
                }
                rdr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return student;
        }

        // Trazimo studenta sa datim indeksom
        public static Student GetStudentByIndeks(SqlConnection conn, string indeks)
        {
            Student student = null;
            try
            {
                string query = "SELECT student_id, ime, prezime, grad " +
                                "FROM studenti WHERE indeks = @indeks";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@indeks", indeks);
                SqlDataReader rdr = cmd.ExecuteReader();

                if(rdr.Read())
                {
                    int id = (int)rdr["student_id"];
                    string ime = (string)rdr["ime"];
                    string prezime = (string)rdr["prezime"];
                    string grad = (string)rdr["grad"];

                    student = new Student(id, indeks, ime, prezime, grad);
                }
                rdr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return student;
        }

        // Trazimo sve studente koji postoje u bazi podataka
        public static List<Student> GetAll(SqlConnection conn)
        {
            List<Student> retVal = new List<Student>();
            try
            {
                string query = "SELECT student_id, indeks, ime, prezime, grad FROM studenti ";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = (int)rdr["student_id"];
                    string indeks = (string)rdr["indeks"];
                    string ime = (string)rdr["ime"];
                    string prezime = (string)rdr["prezime"];
                    string grad = (string)rdr["grad"];

                    // Preuzimanje predmeta koje student pohadja
                    string upitZaPredmete = "select predmet_id from " +
                            "pohadja where student_id = " + id;
                    SqlCommand cmd2 = new SqlCommand(upitZaPredmete, conn);
                    SqlDataReader rdrPredmeti = cmd2.ExecuteReader();
                    List<Predmet> predmetiKojePohadja = new List<Predmet>();
                    while (rdrPredmeti.Read())
                    {
                        int idPredmeta = (int)rdrPredmeti["predmet_id"];
                        Predmet p = PredmetDAO.GetPredmetById(conn, idPredmeta);
                        predmetiKojePohadja.Add(p);
                    }
                    rdrPredmeti.Close();

                    Student student = new Student(id, indeks, ime, prezime, grad);
                    student.Predmeti = predmetiKojePohadja;
                    retVal.Add(student);
                }
                rdr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }

        // Ubacivanje novog studenta u bazu podataka
        public static bool Add(SqlConnection conn, Student student)
        {
            bool retVal = false;
            try
            {
                string update = "INSERT INTO studenti (indeks, ime, " +
                        "prezime, grad) values (@indeks, @ime, @prezime, @grad)";
                SqlCommand cmd = new SqlCommand(update, conn);
                
                cmd.Parameters.AddWithValue("@indeks", student.Indeks);
                cmd.Parameters.AddWithValue("@ime", student.Ime);
                cmd.Parameters.AddWithValue("@prezime", student.Prezime);
                cmd.Parameters.AddWithValue("@grad", student.Grad);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    retVal = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }

        // Menjanje podataka o studentu
        public static bool Update(SqlConnection conn, Student student)
        {
            bool retVal = false;
            try
            {
                string update = "UPDATE studenti SET indeks=@indeks, " +
                        "ime=@ime, prezime=@prezime, grad=@grad WHERE student_id=@student_id";
                SqlCommand cmd = new SqlCommand(update, conn);

                cmd.Parameters.AddWithValue("@indeks", student.Indeks);
                cmd.Parameters.AddWithValue("@ime", student.Ime);
                cmd.Parameters.AddWithValue("@prezime", student.Prezime);
                cmd.Parameters.AddWithValue("@grad", student.Grad);
                cmd.Parameters.AddWithValue("@student_id", student.Id);

                if (cmd.ExecuteNonQuery() == 1)
                    retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }

        // Brisanje studenta iz baze podataka
        public static bool Delete(SqlConnection conn, int id)
        {
            bool retVal = false;
            try
            {
                string update = "DELETE FROM studenti WHERE " +
                        "student_id = " + id;
                SqlCommand cmd = new SqlCommand(update, conn);

                if (cmd.ExecuteNonQuery() == 1)
                    retVal = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return retVal;
        }
    }
}
