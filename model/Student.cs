using System.Collections.Generic;

namespace Termin09Primer03.model
{
    class Student
    {
        public int Id { set; get; }
        public string Ime { set; get; }
        public string Prezime { set; get; }
        public string Grad { set; get; }
        public string Indeks { set; get; }
        public List<IspitnaPrijava> Prijave { set; get; }
        public List<Predmet> Predmeti { set; get; }

        public Student(int id, string indeks, string ime, string prezime,
                string grad)
        {
            Prijave = new List<IspitnaPrijava>();
            Predmeti = new List<Predmet>();

            Id = id;
            Indeks = indeks;
            Ime = ime;
            Prezime = prezime;
            Grad = grad;
        }

        public override string ToString()
        {
            string s = "Student [" + Indeks + " " + Ime + " " + Prezime + " "
                    + Grad + "]";
            foreach (Predmet p in Predmeti)
                s += p.Naziv + ",";

            return s;
        }
    }
}
