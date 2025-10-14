using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database.Seed
{
    public static class PageSeed
    {
        public static void SeedData(this EntityTypeBuilder<Page> entity)
        {
            entity.HasData(
              new Page()
              {
                  Id=1,
                  Title="Matematicka logika",
                  Content= "Matematička ili moderna logika je grana matematike i logike koja se bavi prikazom tradicionalne logike simbolima (pa se još naziva i simboličkom logikom), pri čemu je sve potpuno definirano te nema mogućnosti različitog shvaćanja kao što je to često u tradicionalnoj logici. Matematička je logika osnova modernih računala - na njoj se temelji cijeli logički dio procesora (CPU).\r\n\r\nPovijest\r\nRazvitku teorije matematičke logike su pridonijeli su George Boole (Booleova algebra), koji je otkrio zakonitosti ovog područja (začetke je postavio u jednom svom djelu iz 1847.), te hrvatski matematičar Vatroslav Bertić (čije je djelo doduše mnogo manje poznato, ali osnove ovome je postavio u svom djelu iz 1846., bez poznavanja Booleovog djela).\r\n\r\nIskazna logika\r\nIskazna je logika dio matematičke logike koji se bavi isključivo iskazima (jednostavnim i složenim), a ne jednostavnijim oblicima (prirocima). Osnova iskazne logike je iskaz. On se može usporediti s izjavom a dijeli se na jednostavne i složene iskaze",
                  ImageUrl=null,
                  CreatedAt = new DateTime(2025, 10, 14, 5, 49, 05, DateTimeKind.Utc),
                  NotebookId=1
              },
              new Page()
              {
                  Id=2,
                  Title="Skupovi",
                  Content= "U matematici, skup se može shvatiti kao bilo koja kolekcija različitih apstraktnih objekata smatranim cjelinom. Iako se ovo čini jednostavnom idejom, skupovi su jedan od najvažnijih fundamentalnih koncepata u modernoj matematici.[1]:21 Matematička disciplina koja proučava moguće skupove, teorija skupova, je sadržajno bogata i aktivna.[2][3][4][5]\r\n\r\nTeorija skupova, stvorena tek krajem 19. stoljeća, je danas sveprisutni dio matematičkog obrazovanja, te se stoga u većini zemalja uvodi već u osnovnoj školi. Teorija skupova se može shvatiti kao osnova nad kojom može biti izgrađena gotovo cijela matematika, te kao ishodište iz kojeg gotovo cijela matematika može biti izvedena. Ovaj članak predstavlja kratak i osnovni uvod u ono što matematičari zovu \"intuitivna\" ili \"naivna\" teorija - za više detalja pogledati naivna teorija skupova. Za rigorozniji i moderniji aksiomatski pristup skupovima, razvijena je aksiomatska teorija skupova.",
                  ImageUrl=null,
                  CreatedAt=new DateTime(2025,10,14,6,46,04,DateTimeKind.Utc),
                  NotebookId=2
              },
              new Page()
              {
                  Id=3,
                  Title="Determinante",
                  Content= "U matematici je determinanta skalar svojstven kvadratnoj matrici koji ima mnoga korisna svojstva u linearnoj algebri. Označava se obično s \r\ndet\r\nA\r\n{\\displaystyle \\det A} ili \r\n|\r\nA\r\n|\r\n{\\displaystyle |A|}. Determinanta matrice koja je zadana svojim elementima može se označiti tako da se umjesto uglatih zagrada matrice napišu ravne zagrade determinante.",
                  ImageUrl=null,
                  CreatedAt = new DateTime(2025, 10, 14, 11, 46, 04, DateTimeKind.Utc),
                  NotebookId=3
              },
              new Page()
              {
                  Id=4,
                  Title="Historija jezika",
                  Content= "Bosanski jezik jest normativna varijanta srpskohrvatskog jezika[11][12][13] koji koriste uglavnom Bošnjaci, ali i značajan broj ostalih osoba bosanskohercegovačkog porijekla.\r\n\r\nBosanski je službeni jezik u Bosni i Hercegovini,[14] uz srpski i hrvatski, u Crnoj Gori jedan od službenih,[9][15] a regionalni, odnosno priznati manjinski jezik u Srbiji (Sandžak),[16] Sjevernoj Makedoniji i na Kosovu.[17]\r\n\r\nPrema popisu iz 2013, 1.866.585 stanovnika u Bosni i Hercegovini govori bosanskim jezikom, što je 52,86% stanovništva Bosne i Hercegovine.[1] Osim toga, u Srbiji se ovim jezikom služi 138.871 stanovnik[2], Crnoj Gori 33.077,[3] Hrvatskoj 16.856,[4] na Kosovu 28.898[6] i zapadnoj Evropi i Sjevernoj Americi 150.000 stanovnika te neutvrđen broj iseljenika u Turskoj (neki izvori pretpostavljaju 100.000 do 200.000 govornika; na popisu stanovništva u Turskoj 1965. godine 17.627 osoba navelo je bosanski kao maternji jezik, 2.345 osoba navelo je bosanski kao jedini jezik kojim govore, a 34.892 osobe navele su bosanski kao drugi jezik kojim se najbolje služe).[18]\r\n\r\nPisma bosanskog jezika su latinica i ćirilica, mada se ćirilica sve slabije koristi. Historijska pisma su bosančica i arebica.\r\n\r\nNauka koja se bavi bosanskim jezikom naziva se bosnistika.",
                  ImageUrl=null,
                  CreatedAt = new DateTime(2025, 10, 14, 9, 46, 04, DateTimeKind.Utc),
                  NotebookId=4
              },
              new Page()
              {
                  Id = 5,
                  Title = "Fonetika",
                  Content= "Fonetika je lingvistička disciplina koja proučava fizičke karakteristike govora, uključujući njegovu proizvodnju (artikulaciju), prenošenje (akustiku) i percipiranje (slušanje). Dok se fonetika bavi svim glasovima bez obzira na njihovu funkciju u jeziku, fonologija se fokusira na glasove kao jedinice koje imaju razliku značenja (foneme) unutar određenog jezika. ",
                  ImageUrl = null,
                  CreatedAt = new DateTime(2025, 10, 14, 4, 46, 04, DateTimeKind.Utc),
                  NotebookId = 5
              }, new Page()
              {
                  Id = 6,
                  Title = "Matematicka logika",
                  Content = "Matematička ili moderna logika je grana matematike i logike koja se bavi prikazom tradicionalne logike simbolima (pa se još naziva i simboličkom logikom), pri čemu je sve potpuno definirano te nema mogućnosti različitog shvaćanja kao što je to često u tradicionalnoj logici. Matematička je logika osnova modernih računala - na njoj se temelji cijeli logički dio procesora (CPU).\r\n\r\nPovijest\r\nRazvitku teorije matematičke logike su pridonijeli su George Boole (Booleova algebra), koji je otkrio zakonitosti ovog područja (začetke je postavio u jednom svom djelu iz 1847.), te hrvatski matematičar Vatroslav Bertić (čije je djelo doduše mnogo manje poznato, ali osnove ovome je postavio u svom djelu iz 1846., bez poznavanja Booleovog djela).\r\n\r\nIskazna logika\r\nIskazna je logika dio matematičke logike koji se bavi isključivo iskazima (jednostavnim i složenim), a ne jednostavnijim oblicima (prirocima). Osnova iskazne logike je iskaz. On se može usporediti s izjavom a dijeli se na jednostavne i složene iskaze",
                  ImageUrl = null,
                  CreatedAt = new DateTime(2025, 10, 14, 8, 49, 05, DateTimeKind.Utc),
                  NotebookId = 6
              },
              new Page()
              {
                  Id = 7,
                  Title = "Skupovi",
                  Content= "Skup (množina) u matematici je osnovni pojam moderne matematike.\r\n\r\nNeformalno, pod skupom se podrazumijeva \"svaka vrste kolekcije različitih predmeta\" (Georg Cantor). Na pojmu skupa stoji današnja matematika, jer upravo taj pojam se uzima, zajedno s logikom prvog reda, za gradnju matematike na aksiomima.\r\n\r\nSkup možemo zadati njegovim elementima (članovima) konačnim ili beskonačnim:\r\n\r\nS\r\n=\r\n{\r\n1\r\n,\r\n2\r\n,\r\n3\r\n,\r\n4\r\n,\r\n5\r\n,\r\n6\r\n}\r\n{\\displaystyle S=\\lbrace 1,2,3,4,5,6\\rbrace },\r\n\r\nT\r\n=\r\n{\r\na\r\n1\r\n,\r\na\r\n2\r\n,\r\na\r\n3\r\n,\r\na\r\n4\r\n,\r\na\r\n5\r\n,\r\na\r\n6\r\n,\r\n.\r\n.\r\n.\r\n}\r\n{\\displaystyle T=\\lbrace a_{1},a_{2},a_{3},a_{4},a_{5},a_{6},...\\rbrace }.\r\n\r\nČesto skup zadajemo i pomoću nekog pravila:\r\n\r\nS\r\n=\r\n{\r\nn\r\n∈\r\nN\r\n:\r\nn\r\n<\r\n7\r\n}\r\n{\\displaystyle S=\\lbrace n\\in \\mathbf {N} :n<7\\rbrace }.\r\n\r\nNeke skupove označavamo uvijek istim slovima\r\n\r\nN prirodni brojevi\r\nR realni brojevi\r\nQ racionalni brojevi...\r\nSkup koji nema ni jedan element naziva se prazni skup. Jednačina x2+1=0 u R nema rješenja.",
                  ImageUrl = null,
                  CreatedAt = new DateTime(2025, 10, 14, 2, 46, 04, DateTimeKind.Utc),
                  NotebookId = 7
              },
              new Page()
              {
                  Id = 8,
                  Title = "Matrice",
                  Content = "U matematici, matrica je pravougaona tabela brojeva, ili općenito, tabela koja se sastoji od apstraktnih objekata koji se mogu sabirati i množiti.\r\n\r\nMatrice se koriste za opisivanje linearnih jednačina, za praćenje koeficijenata linearnih transformacija, kao i za čuvanje podataka koji ovise od dva parametra. Matrice se mogu sabirati, množiti i razlagati na razne načine, što ih čini ključnim konceptom u linearnoj algebri i teoriji matrica.",
                  ImageUrl = null,
                  CreatedAt = new DateTime(2025, 10, 14, 12, 46, 04, DateTimeKind.Utc),
                  NotebookId = 8
              },
              new Page()
              {
                  Id = 9,
                  Title = "Tin Ujevic",
                  Content = "Rođen je u Dizdara kuli u Vrgorcu. Njegovo puno ime bilo je Augustin Josip Ujević, po starom običaju župe imotskih Poljica, gdje su svoj pokrštenoj djeci davana dva imena. Njegov otac, Ivan Ujević, bio je učitelj rodom iz Krivodola u Imotskoj krajini, dok mu je majka Bračanka, iz mjesta Milne. Tin je rođen kao jedno od petero djece od kojih su dvoje umrli još u djetinjstvu.\r\n\r\nS očeve strane je mogao naslijediti određen književni talent, pošto je on kao učitelj bio sklon književnosti te i sam pisao. Tin je prva tri razreda osnovne škole polazio u Imotskom, kada seli u Makarsku gdje završava osnovnoškolsko obrazovanje. 1902. odlazi u Split gdje se upisuje u klasičnu gimnaziju i živi u nadbiskupijskom sjemeništu. U svojoj trinaestoj godini počinje pisati pjesme od kojih ništa nije sačuvano (po njemu je njegovo prvo djelo kratak tekst \"Voda\" koji je završio u košu za smeće nekog urednika). 1909. godine Tin maturira u Splitu s odličnim uspjehom, odriče se mogućnosti zaređenja te odlazi u Zagreb upisujući studij hrvatskog jezika i književnosti, klasične filologije, filozofije i estetike na Filozofskom fakultetu u Zagrebu. Te iste godine objavio je svoj prvi sonet \"Za novim vidicima\" u časopisu \"Mlada Hrvatska\".",
                  ImageUrl = null,
                  CreatedAt = new DateTime(2025, 10, 14, 12, 28, 04, DateTimeKind.Utc),
                  NotebookId = 9
              },
              new Page()
              {
                  Id = 10,
                  Title = "Fonologija",
                  Content = "Fonologija je nauka koja proučava kako jezik iskorištava razlikovnu funkciju fonema radi komunikacije (od grč. φωνή, phone = glas, λόγος, lógos = riječ, govor, nauka). Ovo je disciplina koja proučava jezičku funkciju i ponašanje govornih jedinica. Najmanje jedinice koje imaju razlikovnu funkciju jesu fonemi (centralni pojam u fonologiji). Fonologija proučava sistem govornih jedinica (glasova) u jeziku, dok se fonetika bavi proučavanjem artikulacijskih i akustičnih obilježja glasova i govora. Uobičajeno je označiti fonološku transkripciju kosim zagradama / /.",
                  ImageUrl = null,
                  CreatedAt = new DateTime(2025, 10, 14, 6, 46, 04, DateTimeKind.Utc),
                  NotebookId = 10
              }, new Page()
              {
                  Id = 11,
                  Title = "Matematicka logika",
                  Content ="Matematička logika zasniva se na na zakonima matematičkog aparata i koristi se matematičkim metodama. Predmet matematičke logike je dokaz.\r\n\r\nPosmatrajmo rečenice\r\n\r\n4 je paran broj (istinita)\r\nTriglav je viši od Monblana (neistinita)\r\nZa ove rečenice možemo reći da li su istinite ili neistinite Za rečenice kao što su\r\n\r\nx<4\r\nDonesi vode\r\nne možemo utvrditi istinitost\r\n\r\nNauka koja se bavi proučavanjem oblika mišljenja i vezama između tih oblika je logika. Logičko mišljenje je sam proces mišljenja. Za Z-skup cijelih brojeva vrijedi\r\n\r\n(a<b & b<c) =>a<c\r\n\r\nU logici je bitna forma, a ne sadržaj;\r\n\r\nČovjek je smrtan i Šekspir je smrtan znači Šekspir je čovjek\r\nAko je svaki kamen crven i ako je čovjek kamen znaći čovjek je crven.",
                  ImageUrl = null,
                  CreatedAt = new DateTime(2025, 10, 14, 9, 49, 05, DateTimeKind.Utc),
                  NotebookId = 11
              },
              new Page()
              {
                  Id = 12,
                  Title = "Abelova grupa",
                  Content = "Za Abelov teorem o algebarskim krivima, pogledajte članak Abel-Jacobijeva mapa\r\nZa Abelob teorem o nerješivosti jednačine petog stepena sa radikalima, pogledajte članak Abel-Ruffinijev teorem\r\nU matematici, Abelov teorem[1][2] za potencijalne redove dovodi u vezu graničnu vrijednost potencijalnog reda sa sumom njegovih koeficijenata. Teorem je dobio naziv po norveškom matematičaru Nielsu Henriku Abelu.", 
                  ImageUrl = null,
                  CreatedAt = new DateTime(2025, 10, 14, 2, 55, 04, DateTimeKind.Utc),
                  NotebookId = 12
              },
              new Page()
              {
                  Id = 13,
                  Title = "vEKTORI",
                  Content= "Vektorski ili linearni prostor je algebarski pojam u matematici koji nalazi primjenu u svim glavnim granama matematike, među kojima su linearna algebra, analiza i analitička geometrija. Definiše se na sljedeći način:\r\n\r\nNeka skup V ima strukturu Abelove grupe u odnosu na sabiranje. Elemente skupa V zovemo vektori. Neutralni element označujemo sa 0 i zovemo nulti vektor.\r\n\r\nNeka skup F ima strukturu polja. Elemente skupa F zovemo skalari, a neutralne elemente u odnosu na dvije binarne operacije označujemo sa 0 i 1.\r\n\r\nNa skupu F × V definirano je množenje vektora skalarom, tj. preslikavanje F × V → V, koje svakom skalaru \r\nα\r\n∈\r\nF\r\n{\\displaystyle \\alpha \\in F} i svakom vektoru \r\nx\r\n∈\r\nV\r\n{\\displaystyle x\\in V} pridružuje vektor \r\nα\r\nx\r\n∈\r\nV\r\n{\\displaystyle \\alpha x\\in V}, tako da vrijede sljedeći aksiomi:\r\n\r\n(I) \r\nα\r\n(\r\nβ\r\nx\r\n)\r\n=\r\n(\r\nα\r\nβ\r\n)\r\nx\r\n,\r\n∀\r\nα\r\n,\r\nβ\r\n∈\r\nF\r\n,\r\n∀\r\nx\r\n∈\r\nV\r\n{\\displaystyle \\alpha (\\beta x)=(\\alpha \\beta )x,\\forall \\alpha ,\\beta \\in F,\\forall x\\in V}\r\n(II) \r\nα\r\n(\r\nx\r\n+\r\ny\r\n)\r\n=\r\nα\r\nx\r\n+\r\nα\r\ny\r\n,\r\n∀\r\nα\r\n∈\r\nF\r\n,\r\n∀\r\nx\r\n,\r\ny\r\n∈\r\nV\r\n{\\displaystyle \\alpha (x+y)=\\alpha x+\\alpha y,\\forall \\alpha \\in F,\\forall x,y\\in V}\r\n(III) \r\n(\r\nα\r\n+\r\nβ\r\n)\r\nx\r\n=\r\nα\r\nx\r\n+\r\nβ\r\nx\r\n,\r\n∀\r\nα\r\n,\r\nβ\r\n∈\r\nF\r\n,\r\n∀\r\nx\r\n∈\r\nV\r\n{\\displaystyle (\\alpha +\\beta )x=\\alpha x+\\beta x,\\forall \\alpha ,\\beta \\in F,\\forall x\\in V}\r\n(IV) \r\n1\r\nx\r\n=\r\nx\r\n,\r\n∀\r\nx\r\n∈\r\nV\r\n{\\displaystyle 1x=x,\\forall x\\in V}\r\nOvako se definisano preslikavanje zove množenje vektora skalarom, dok se V naziva vektorski prostor nad poljem F i piše V(F).\r\n\r\nUobičajeno je da se vektorski prostori nad poljem realnih odnosno kompleksnih brojeva nazivaju realni, odnosno kompleksni vektorski prostori. Također, vektorski se prostor u kojem je definisan skalarni produkt naziva Euklidski vektorski prostor.",
                  ImageUrl = null,
                  CreatedAt = new DateTime(2025, 10, 14, 12, 46, 22, DateTimeKind.Utc),
                  NotebookId = 13
              },
              new Page()
              {
                  Id = 14,
                  Title = "Tin Ujevic",
                  Content = "Rođen je u Dizdara kuli u Vrgorcu. Njegovo puno ime bilo je Augustin Josip Ujević, po starom običaju župe imotskih Poljica, gdje su svoj pokrštenoj djeci davana dva imena. Njegov otac, Ivan Ujević, bio je učitelj rodom iz Krivodola u Imotskoj krajini, dok mu je majka Bračanka, iz mjesta Milne. Tin je rođen kao jedno od petero djece od kojih su dvoje umrli još u djetinjstvu.\r\n\r\nS očeve strane je mogao naslijediti određen književni talent, pošto je on kao učitelj bio sklon književnosti te i sam pisao. Tin je prva tri razreda osnovne škole polazio u Imotskom, kada seli u Makarsku gdje završava osnovnoškolsko obrazovanje. 1902. odlazi u Split gdje se upisuje u klasičnu gimnaziju i živi u nadbiskupijskom sjemeništu. U svojoj trinaestoj godini počinje pisati pjesme od kojih ništa nije sačuvano (po njemu je njegovo prvo djelo kratak tekst \"Voda\" koji je završio u košu za smeće nekog urednika). 1909. godine Tin maturira u Splitu s odličnim uspjehom, odriče se mogućnosti zaređenja te odlazi u Zagreb upisujući studij hrvatskog jezika i književnosti, klasične filologije, filozofije i estetike na Filozofskom fakultetu u Zagrebu. Te iste godine objavio je svoj prvi sonet \"Za novim vidicima\" u časopisu \"Mlada Hrvatska\".",
                  ImageUrl = null,
                  CreatedAt = new DateTime(2025, 10, 14, 1, 28, 04, DateTimeKind.Utc),
                  NotebookId = 14
              },
              new Page()
              {
                  Id = 15,
                  Title = "Fonologija",
                  Content = "Fonologija je nauka koja proučava kako jezik iskorištava razlikovnu funkciju fonema radi komunikacije (od grč. φωνή, phone = glas, λόγος, lógos = riječ, govor, nauka). Ovo je disciplina koja proučava jezičku funkciju i ponašanje govornih jedinica. Najmanje jedinice koje imaju razlikovnu funkciju jesu fonemi (centralni pojam u fonologiji). Fonologija proučava sistem govornih jedinica (glasova) u jeziku, dok se fonetika bavi proučavanjem artikulacijskih i akustičnih obilježja glasova i govora. Uobičajeno je označiti fonološku transkripciju kosim zagradama / /.",
                  ImageUrl = null,
                  CreatedAt = new DateTime(2025, 10, 14, 5, 46, 44, DateTimeKind.Utc),
                  NotebookId = 15
              }

                );

        }
    }
}
