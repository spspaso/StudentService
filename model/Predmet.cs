using System.Collections.Generic;

namespace Termin09Primer03.model
{
    class Predmet
    {
        public int Id { set; get; }
        public string Naziv { set; get; }

        public List<Student> Studenti { set; get; }

        public Predmet(int id, string naziv)
        {
            Id = id;
            Naziv = naziv;
            Studenti = new List<Student>();
        }

        public override string ToString()
        {
            return "Predmet [id=" + Id + ", naziv=" + Naziv + "]";
        }
    }
}
