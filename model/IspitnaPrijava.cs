namespace Termin09Primer03.model
{
    class IspitnaPrijava
    {
        public Predmet PredmetPrijave { set; get; }
        public IspitniRok Rok { set; get; }
        Student StudentPrijave { set; get; }
        public int Teorija { set; get; }
        public int Zadaci { set; get; }

        public IspitnaPrijava(Predmet predmet, Student student, IspitniRok rok,
                int teorija, int zadaci)
        {
            PredmetPrijave = predmet;
            StudentPrijave = student;
            Rok = rok;
            Teorija = teorija;
            Zadaci = zadaci;
        }

        public override string ToString()
        {
            return "IspitnaPrijava [predmet=" + PredmetPrijave + ", rok=" + Rok
                    + ", teorija=" + Teorija + ", zadaci=" + Zadaci + "]";
        }

        // Na osnovu rezultata o ispitu koji se cuvaju u poljima klase, racunamo ocenu studenta
        public int SracunajOcenu()
        {
            int bodovi = Teorija + Zadaci;
            int ocena;
            if (bodovi >= 95)
                ocena = 10;
            else if (bodovi >= 85)
                ocena = 9;
            else if (bodovi >= 75)
                ocena = 8;
            else if (bodovi >= 65)
                ocena = 7;
            else if (bodovi >= 55)
                ocena = 6;
            else
                ocena = 5;

            return ocena;
        }
    }
}
