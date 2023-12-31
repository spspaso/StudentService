/* Najpre brisemo sve tabele iz baze podataka. 
	Ovo radimo da bi mogli vise puta pokretati skript ne razmisljajuci o trenutnom sadrzaju baze. Ukoliko tabela ne postoji, naredba nece nista uraditi.
	Vazno:
		Ne moze se obrisati tabela ako postoji druga tabela koja ima spoljni kljuc na tu tabelu. Zato prvo brisemo one tabele koje pokazuju na druge tabele.
		Npr. ne moze se obrisati tabela 'predmeti' dok god postoji tabela 'predaje' koja ima spoljni kljuc na tu tabelu. Zato prvo brisemo tabelu 'predaje', pa tek onda 'predmeti'

*/ 

use DotNetKurs;

drop table if exists ispitne_prijave;
drop table if exists pohadja;
drop table if exists predaje;
drop table if exists ispitni_rokovi;
drop table if exists studenti;
drop table if exists nastavnici;
drop table if exists predmeti;


-- KREIRANJE TABELA

	create table studenti (
		student_id  integer identity(1,1), -- identity(1,1) - SUBP ce automatski odrediti vrednost ovog polja, ne treba navoditi vrednost pri ubacivanju novog sloga
		indeks varchar(20),
		ime  varchar(20),
		prezime  varchar(20),
		grad varchar(20),
		primary key(student_id) -- broj indeksa bi mogao biti primarni kljuc, ali je praksa da se uvede surogatni kljuc (to je ovo identity(1,1) polje student_id) o cijoj vrednosti baza vodi racuna i garantuje da je jedinstveno za svaki slog
	);
	
	create table nastavnici (
		nastavnik_id  integer identity(1,1),
		ime  varchar(20),
		prezime  varchar(20),
		zvanje varchar(20),
		primary key(nastavnik_id)
	);
	
	create table predmeti (
		predmet_id  integer identity(1,1),
		naziv varchar(20),
		primary key(predmet_id)
	);
	
	create table predaje (
		nastavnik_id  integer not null,
		predmet_id  integer not null,
		primary key(nastavnik_id, predmet_id),
	
		-- on delete cascade opisuje sta treba da se desi kada neko obrise nastavnika ciji id je referenciran od strane nekog reda u tabeli predaje
		foreign key (nastavnik_id)
			references nastavnici(nastavnik_id) on delete cascade,

		foreign key (predmet_id) 
			references predmeti(predmet_id) on delete cascade
	);
	
	create table pohadja (
		student_id  integer not null,
		predmet_id  integer not null,
		primary key(student_id, predmet_id),
	
		foreign key (student_id)
			references studenti(student_id) on delete cascade,
	
		foreign key (predmet_id)
			references predmeti(predmet_id) on delete cascade,
	); 
	
	
	create table ispitni_rokovi (
		rok_id integer identity(1,1),
		naziv varchar(20),
		pocetak date,
		kraj date,
		primary key(rok_id)
	); 
	
	create table ispitne_prijave (
		student_id integer not null,
		predmet_id integer not null,
		rok_id integer not null,
		teorija integer,
		zadaci integer,
	
	primary key (student_id, predmet_id, rok_id),
	
	foreign key (student_id)
	    references studenti(student_id) on delete cascade,

	foreign key (predmet_id)
	    references predmeti(predmet_id) on delete cascade,		
								                       	                        
	foreign key (rok_id)
	    references ispitni_rokovi(rok_id) on delete cascade
	);
	
-- UBACIVANJE PODATAKA

-- Obratiti paznju da se pri ubacivanju ne navode vrednosti za identity(1,1) polja, obzirom da ce SUBP automatski ovim poljima dodeliti vrednost
	insert into nastavnici (ime, prezime, zvanje) values ('Marko', 'Popovic', 'Docent');
	insert into nastavnici (ime, prezime, zvanje) values ('Milan', 'Janjic', 'Docent');
	insert into nastavnici (ime, prezime, zvanje) values ('Zeljko', 'Djuric', 'Asistent');
	
	insert into predmeti (naziv) values ('Algebra');
	insert into predmeti (naziv) values ('Analiza 1');
	
	insert into predaje values (1, 1); -- Ovako je napisano zbog jednostavnosti, ali je ovo potencijalna greska. Pretpostavlja se da ce prvom predmetu biti dodeljen identifikator 1, a ne mora to uvek da bude slucaj obzirom da SUBP odredjuje tu vrednost (npr. ako vec ima 5 predmeta u bazi podataka, novi predmet ce dobiti id 6)
	insert into predaje values (1, 2);
	insert into predaje values (2, 2);
	insert into predaje values (3, 1);
	
	insert into studenti (indeks, ime, prezime, grad) values ('E 1/12', 'Petar', 'Mihajlovic', 'Novi Sad');
	insert into studenti (indeks, ime, prezime, grad) values ('E 2/12', 'Dejan', 'Ivanovic', 'Loznica');
	insert into studenti (indeks, ime, prezime, grad) values ('E 3/12', 'Zoran', 'Kovacevic', 'Indjija');
	insert into studenti (indeks, ime, prezime, grad) values ('E 4/12', 'Marko', 'Popovic', 'Novi Sad');
	
	insert into pohadja values (1, 1);
	insert into pohadja values (1, 2);
	insert into pohadja values (2, 1);
	insert into pohadja values (3, 1);

	insert into ispitni_rokovi (naziv, pocetak, kraj) values ('Januarski', '2015-01-15', '2015-01-29');
	insert into ispitni_rokovi (naziv, pocetak, kraj) values ('Februarski', '2015-02-01', '2015-02-14');
	
	insert into ispitne_prijave values (1, 1, 1, 20, 70);
	insert into ispitne_prijave values (1, 2, 1, 10, 54);
	insert into ispitne_prijave values (2, 1, 1, 10, 10);
	insert into ispitne_prijave values (2, 1, 2, 40, 30);
	insert into ispitne_prijave values (3, 1, 1, 10, 30);