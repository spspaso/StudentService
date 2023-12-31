using System;

namespace Termin09Primer03.model
{
    class IspitniRok
    {
        public int Id { set; get; }
        public string Naziv { set; get; }
        public DateTime Pocetak { set; get; }
        public DateTime Kraj { set; get; }

        public IspitniRok(int id, string naziv, DateTime pocetak, DateTime kraj)
        {
            Id = id;
            Naziv = naziv;
            Pocetak = pocetak;
            Kraj = kraj;
        }

        public override string ToString()
        {
            return "Rok [id=" + Id + ", naziv=" + Naziv + ", pocetak=" + Pocetak
                    + ", kraj=" + Kraj + "]";
        }
    }
}
