using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProBook.Services.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    PasswordSalt = table.Column<string>(type: "text", nullable: true),
                    IsStudent = table.Column<bool>(type: "boolean", nullable: true),
                    TelephoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TwoFactorCode = table.Column<string>(type: "text", nullable: true),
                    TwoFactorCodeExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collections_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notebooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notebooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notebooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotebookCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotebookId = table.Column<int>(type: "integer", nullable: false),
                    CollectionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotebookCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotebookCollections_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotebookCollections_Notebooks_NotebookId",
                        column: x => x.NotebookId,
                        principalTable: "Notebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NotebookId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Notebooks_NotebookId",
                        column: x => x.NotebookId,
                        principalTable: "Notebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharedNotebooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SharedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsForEdit = table.Column<bool>(type: "boolean", nullable: true),
                    NotebookId = table.Column<int>(type: "integer", nullable: false),
                    FromUserId = table.Column<int>(type: "integer", nullable: false),
                    ToUserId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedNotebooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedNotebooks_Notebooks_NotebookId",
                        column: x => x.NotebookId,
                        principalTable: "Notebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharedNotebooks_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharedNotebooks_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharedNotebooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PageId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SharedNotebookId = table.Column<int>(type: "integer", nullable: false),
                    Viewed = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_SharedNotebooks_SharedNotebookId",
                        column: x => x.SharedNotebookId,
                        principalTable: "SharedNotebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Gender", "IsStudent", "Name", "PasswordHash", "PasswordSalt", "RegisteredDate", "Surname", "TelephoneNumber", "TwoFactorCode", "TwoFactorCodeExpiresAt", "Username" },
                values: new object[,]
                {
                    { 1, "user1@gmail.com", "Male", true, "User1", "UbzzxOGag4pPmBhguTkyKnpEZw4=", "qYk4OxryQgplthbzFlS0yQ==", new DateTime(2025, 10, 13, 19, 49, 5, 0, DateTimeKind.Utc), "User1", "061-234-444", null, null, "user1" },
                    { 2, "user2@gmail.com", "Male", true, "User2", "UbzzxOGag4pPmBhguTkyKnpEZw4=", "qYk4OxryQgplthbzFlS0yQ==", new DateTime(2025, 10, 13, 19, 49, 5, 0, DateTimeKind.Utc), "User2", "063-234-444", null, null, "user2" },
                    { 3, "user3@gmail.com", "Female", true, "User3", "UbzzxOGag4pPmBhguTkyKnpEZw4=", "qYk4OxryQgplthbzFlS0yQ==", new DateTime(2025, 10, 13, 19, 49, 5, 0, DateTimeKind.Utc), "User3", "065-234-444", null, null, "user3" }
                });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 14, 22, 49, 5, 0, DateTimeKind.Utc), "-", "Math-Collection", 1 },
                    { 2, new DateTime(2025, 10, 14, 21, 49, 5, 0, DateTimeKind.Utc), "-", "Bosnian-Collection", 1 },
                    { 3, new DateTime(2025, 10, 14, 11, 49, 5, 0, DateTimeKind.Utc), "-", "Math-Collection", 2 },
                    { 4, new DateTime(2025, 10, 14, 13, 49, 5, 0, DateTimeKind.Utc), "-", "Bosnian-Collection", 2 },
                    { 5, new DateTime(2025, 10, 13, 11, 49, 5, 0, DateTimeKind.Utc), "-", "Math-Collection", 3 },
                    { 6, new DateTime(2025, 10, 13, 22, 49, 5, 0, DateTimeKind.Utc), "-", "Bosnian-Collection", 3 }
                });

            migrationBuilder.InsertData(
                table: "Notebooks",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 13, 19, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-1", 1 },
                    { 2, new DateTime(2025, 10, 13, 20, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-2", 1 },
                    { 3, new DateTime(2025, 10, 13, 19, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-3", 1 },
                    { 4, new DateTime(2025, 10, 13, 21, 49, 5, 0, DateTimeKind.Utc), "-", null, "Bosnian-1", 1 },
                    { 5, new DateTime(2025, 10, 13, 21, 0, 5, 0, DateTimeKind.Utc), "-", null, "Bosnian-2", 1 },
                    { 6, new DateTime(2025, 10, 14, 19, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-1", 2 },
                    { 7, new DateTime(2025, 10, 14, 19, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-2", 2 },
                    { 8, new DateTime(2025, 10, 14, 19, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-3", 2 },
                    { 9, new DateTime(2025, 10, 14, 11, 49, 5, 0, DateTimeKind.Utc), "-", null, "Bosnian-1", 2 },
                    { 10, new DateTime(2025, 10, 14, 23, 49, 5, 0, DateTimeKind.Utc), "-", null, "Bosnian-2", 2 },
                    { 11, new DateTime(2025, 10, 14, 22, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-1", 3 },
                    { 12, new DateTime(2025, 10, 14, 22, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-2", 3 },
                    { 13, new DateTime(2025, 9, 13, 19, 11, 5, 0, DateTimeKind.Utc), "-", null, "Math-3", 3 },
                    { 14, new DateTime(2025, 10, 14, 19, 55, 5, 0, DateTimeKind.Utc), "-", null, "Bosnian-1", 3 },
                    { 15, new DateTime(2025, 10, 14, 19, 24, 5, 0, DateTimeKind.Utc), "-", null, "Bosnian-2", 3 }
                });

            migrationBuilder.InsertData(
                table: "NotebookCollections",
                columns: new[] { "Id", "CollectionId", "CreatedAt", "NotebookId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 10, 11, 19, 49, 5, 0, DateTimeKind.Utc), 1 },
                    { 2, 1, new DateTime(2025, 10, 11, 11, 49, 5, 0, DateTimeKind.Utc), 2 },
                    { 3, 1, new DateTime(2025, 10, 11, 10, 49, 5, 0, DateTimeKind.Utc), 3 },
                    { 4, 2, new DateTime(2025, 10, 10, 19, 49, 5, 0, DateTimeKind.Utc), 4 },
                    { 5, 2, new DateTime(2025, 10, 9, 19, 49, 5, 0, DateTimeKind.Utc), 5 },
                    { 6, 3, new DateTime(2025, 10, 9, 12, 49, 5, 0, DateTimeKind.Utc), 6 },
                    { 7, 3, new DateTime(2025, 10, 6, 19, 49, 5, 0, DateTimeKind.Utc), 7 },
                    { 8, 3, new DateTime(2025, 10, 5, 19, 49, 5, 0, DateTimeKind.Utc), 8 },
                    { 9, 4, new DateTime(2025, 10, 11, 19, 49, 5, 0, DateTimeKind.Utc), 9 },
                    { 10, 4, new DateTime(2025, 10, 1, 19, 49, 5, 0, DateTimeKind.Utc), 10 },
                    { 11, 5, new DateTime(2025, 10, 2, 19, 49, 5, 0, DateTimeKind.Utc), 11 },
                    { 12, 5, new DateTime(2025, 10, 4, 22, 49, 5, 0, DateTimeKind.Utc), 12 },
                    { 13, 5, new DateTime(2025, 6, 11, 19, 49, 5, 0, DateTimeKind.Utc), 13 },
                    { 14, 6, new DateTime(2025, 9, 11, 19, 49, 5, 0, DateTimeKind.Utc), 14 },
                    { 15, 6, new DateTime(2025, 10, 6, 19, 49, 5, 0, DateTimeKind.Utc), 15 }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "Content", "CreatedAt", "ImageUrl", "NotebookId", "Title" },
                values: new object[,]
                {
                    { 1, "Matematička ili moderna logika je grana matematike i logike koja se bavi prikazom tradicionalne logike simbolima (pa se još naziva i simboličkom logikom), pri čemu je sve potpuno definirano te nema mogućnosti različitog shvaćanja kao što je to često u tradicionalnoj logici. Matematička je logika osnova modernih računala - na njoj se temelji cijeli logički dio procesora (CPU).\r\n\r\nPovijest\r\nRazvitku teorije matematičke logike su pridonijeli su George Boole (Booleova algebra), koji je otkrio zakonitosti ovog područja (začetke je postavio u jednom svom djelu iz 1847.), te hrvatski matematičar Vatroslav Bertić (čije je djelo doduše mnogo manje poznato, ali osnove ovome je postavio u svom djelu iz 1846., bez poznavanja Booleovog djela).\r\n\r\nIskazna logika\r\nIskazna je logika dio matematičke logike koji se bavi isključivo iskazima (jednostavnim i složenim), a ne jednostavnijim oblicima (prirocima). Osnova iskazne logike je iskaz. On se može usporediti s izjavom a dijeli se na jednostavne i složene iskaze", new DateTime(2025, 10, 14, 5, 49, 5, 0, DateTimeKind.Utc), null, 1, "Matematicka logika" },
                    { 2, "U matematici, skup se može shvatiti kao bilo koja kolekcija različitih apstraktnih objekata smatranim cjelinom. Iako se ovo čini jednostavnom idejom, skupovi su jedan od najvažnijih fundamentalnih koncepata u modernoj matematici.[1]:21 Matematička disciplina koja proučava moguće skupove, teorija skupova, je sadržajno bogata i aktivna.[2][3][4][5]\r\n\r\nTeorija skupova, stvorena tek krajem 19. stoljeća, je danas sveprisutni dio matematičkog obrazovanja, te se stoga u većini zemalja uvodi već u osnovnoj školi. Teorija skupova se može shvatiti kao osnova nad kojom može biti izgrađena gotovo cijela matematika, te kao ishodište iz kojeg gotovo cijela matematika može biti izvedena. Ovaj članak predstavlja kratak i osnovni uvod u ono što matematičari zovu \"intuitivna\" ili \"naivna\" teorija - za više detalja pogledati naivna teorija skupova. Za rigorozniji i moderniji aksiomatski pristup skupovima, razvijena je aksiomatska teorija skupova.", new DateTime(2025, 10, 14, 6, 46, 4, 0, DateTimeKind.Utc), null, 2, "Skupovi" },
                    { 3, "U matematici je determinanta skalar svojstven kvadratnoj matrici koji ima mnoga korisna svojstva u linearnoj algebri. Označava se obično s \r\ndet\r\nA\r\n{\\displaystyle \\det A} ili \r\n|\r\nA\r\n|\r\n{\\displaystyle |A|}. Determinanta matrice koja je zadana svojim elementima može se označiti tako da se umjesto uglatih zagrada matrice napišu ravne zagrade determinante.", new DateTime(2025, 10, 14, 11, 46, 4, 0, DateTimeKind.Utc), null, 3, "Determinante" },
                    { 4, "Bosanski jezik jest normativna varijanta srpskohrvatskog jezika[11][12][13] koji koriste uglavnom Bošnjaci, ali i značajan broj ostalih osoba bosanskohercegovačkog porijekla.\r\n\r\nBosanski je službeni jezik u Bosni i Hercegovini,[14] uz srpski i hrvatski, u Crnoj Gori jedan od službenih,[9][15] a regionalni, odnosno priznati manjinski jezik u Srbiji (Sandžak),[16] Sjevernoj Makedoniji i na Kosovu.[17]\r\n\r\nPrema popisu iz 2013, 1.866.585 stanovnika u Bosni i Hercegovini govori bosanskim jezikom, što je 52,86% stanovništva Bosne i Hercegovine.[1] Osim toga, u Srbiji se ovim jezikom služi 138.871 stanovnik[2], Crnoj Gori 33.077,[3] Hrvatskoj 16.856,[4] na Kosovu 28.898[6] i zapadnoj Evropi i Sjevernoj Americi 150.000 stanovnika te neutvrđen broj iseljenika u Turskoj (neki izvori pretpostavljaju 100.000 do 200.000 govornika; na popisu stanovništva u Turskoj 1965. godine 17.627 osoba navelo je bosanski kao maternji jezik, 2.345 osoba navelo je bosanski kao jedini jezik kojim govore, a 34.892 osobe navele su bosanski kao drugi jezik kojim se najbolje služe).[18]\r\n\r\nPisma bosanskog jezika su latinica i ćirilica, mada se ćirilica sve slabije koristi. Historijska pisma su bosančica i arebica.\r\n\r\nNauka koja se bavi bosanskim jezikom naziva se bosnistika.", new DateTime(2025, 10, 14, 9, 46, 4, 0, DateTimeKind.Utc), null, 4, "Historija jezika" },
                    { 5, "Fonetika je lingvistička disciplina koja proučava fizičke karakteristike govora, uključujući njegovu proizvodnju (artikulaciju), prenošenje (akustiku) i percipiranje (slušanje). Dok se fonetika bavi svim glasovima bez obzira na njihovu funkciju u jeziku, fonologija se fokusira na glasove kao jedinice koje imaju razliku značenja (foneme) unutar određenog jezika. ", new DateTime(2025, 10, 14, 4, 46, 4, 0, DateTimeKind.Utc), null, 5, "Fonetika" },
                    { 6, "Matematička ili moderna logika je grana matematike i logike koja se bavi prikazom tradicionalne logike simbolima (pa se još naziva i simboličkom logikom), pri čemu je sve potpuno definirano te nema mogućnosti različitog shvaćanja kao što je to često u tradicionalnoj logici. Matematička je logika osnova modernih računala - na njoj se temelji cijeli logički dio procesora (CPU).\r\n\r\nPovijest\r\nRazvitku teorije matematičke logike su pridonijeli su George Boole (Booleova algebra), koji je otkrio zakonitosti ovog područja (začetke je postavio u jednom svom djelu iz 1847.), te hrvatski matematičar Vatroslav Bertić (čije je djelo doduše mnogo manje poznato, ali osnove ovome je postavio u svom djelu iz 1846., bez poznavanja Booleovog djela).\r\n\r\nIskazna logika\r\nIskazna je logika dio matematičke logike koji se bavi isključivo iskazima (jednostavnim i složenim), a ne jednostavnijim oblicima (prirocima). Osnova iskazne logike je iskaz. On se može usporediti s izjavom a dijeli se na jednostavne i složene iskaze", new DateTime(2025, 10, 14, 8, 49, 5, 0, DateTimeKind.Utc), null, 6, "Matematicka logika" },
                    { 7, "Skup (množina) u matematici je osnovni pojam moderne matematike.\r\n\r\nNeformalno, pod skupom se podrazumijeva \"svaka vrste kolekcije različitih predmeta\" (Georg Cantor). Na pojmu skupa stoji današnja matematika, jer upravo taj pojam se uzima, zajedno s logikom prvog reda, za gradnju matematike na aksiomima.\r\n\r\nSkup možemo zadati njegovim elementima (članovima) konačnim ili beskonačnim:\r\n\r\nS\r\n=\r\n{\r\n1\r\n,\r\n2\r\n,\r\n3\r\n,\r\n4\r\n,\r\n5\r\n,\r\n6\r\n}\r\n{\\displaystyle S=\\lbrace 1,2,3,4,5,6\\rbrace },\r\n\r\nT\r\n=\r\n{\r\na\r\n1\r\n,\r\na\r\n2\r\n,\r\na\r\n3\r\n,\r\na\r\n4\r\n,\r\na\r\n5\r\n,\r\na\r\n6\r\n,\r\n.\r\n.\r\n.\r\n}\r\n{\\displaystyle T=\\lbrace a_{1},a_{2},a_{3},a_{4},a_{5},a_{6},...\\rbrace }.\r\n\r\nČesto skup zadajemo i pomoću nekog pravila:\r\n\r\nS\r\n=\r\n{\r\nn\r\n∈\r\nN\r\n:\r\nn\r\n<\r\n7\r\n}\r\n{\\displaystyle S=\\lbrace n\\in \\mathbf {N} :n<7\\rbrace }.\r\n\r\nNeke skupove označavamo uvijek istim slovima\r\n\r\nN prirodni brojevi\r\nR realni brojevi\r\nQ racionalni brojevi...\r\nSkup koji nema ni jedan element naziva se prazni skup. Jednačina x2+1=0 u R nema rješenja.", new DateTime(2025, 10, 14, 2, 46, 4, 0, DateTimeKind.Utc), null, 7, "Skupovi" },
                    { 8, "U matematici, matrica je pravougaona tabela brojeva, ili općenito, tabela koja se sastoji od apstraktnih objekata koji se mogu sabirati i množiti.\r\n\r\nMatrice se koriste za opisivanje linearnih jednačina, za praćenje koeficijenata linearnih transformacija, kao i za čuvanje podataka koji ovise od dva parametra. Matrice se mogu sabirati, množiti i razlagati na razne načine, što ih čini ključnim konceptom u linearnoj algebri i teoriji matrica.", new DateTime(2025, 10, 14, 12, 46, 4, 0, DateTimeKind.Utc), null, 8, "Matrice" },
                    { 9, "Rođen je u Dizdara kuli u Vrgorcu. Njegovo puno ime bilo je Augustin Josip Ujević, po starom običaju župe imotskih Poljica, gdje su svoj pokrštenoj djeci davana dva imena. Njegov otac, Ivan Ujević, bio je učitelj rodom iz Krivodola u Imotskoj krajini, dok mu je majka Bračanka, iz mjesta Milne. Tin je rođen kao jedno od petero djece od kojih su dvoje umrli još u djetinjstvu.\r\n\r\nS očeve strane je mogao naslijediti određen književni talent, pošto je on kao učitelj bio sklon književnosti te i sam pisao. Tin je prva tri razreda osnovne škole polazio u Imotskom, kada seli u Makarsku gdje završava osnovnoškolsko obrazovanje. 1902. odlazi u Split gdje se upisuje u klasičnu gimnaziju i živi u nadbiskupijskom sjemeništu. U svojoj trinaestoj godini počinje pisati pjesme od kojih ništa nije sačuvano (po njemu je njegovo prvo djelo kratak tekst \"Voda\" koji je završio u košu za smeće nekog urednika). 1909. godine Tin maturira u Splitu s odličnim uspjehom, odriče se mogućnosti zaređenja te odlazi u Zagreb upisujući studij hrvatskog jezika i književnosti, klasične filologije, filozofije i estetike na Filozofskom fakultetu u Zagrebu. Te iste godine objavio je svoj prvi sonet \"Za novim vidicima\" u časopisu \"Mlada Hrvatska\".", new DateTime(2025, 10, 14, 12, 28, 4, 0, DateTimeKind.Utc), null, 9, "Tin Ujevic" },
                    { 10, "Fonologija je nauka koja proučava kako jezik iskorištava razlikovnu funkciju fonema radi komunikacije (od grč. φωνή, phone = glas, λόγος, lógos = riječ, govor, nauka). Ovo je disciplina koja proučava jezičku funkciju i ponašanje govornih jedinica. Najmanje jedinice koje imaju razlikovnu funkciju jesu fonemi (centralni pojam u fonologiji). Fonologija proučava sistem govornih jedinica (glasova) u jeziku, dok se fonetika bavi proučavanjem artikulacijskih i akustičnih obilježja glasova i govora. Uobičajeno je označiti fonološku transkripciju kosim zagradama / /.", new DateTime(2025, 10, 14, 6, 46, 4, 0, DateTimeKind.Utc), null, 10, "Fonologija" },
                    { 11, "Matematička logika zasniva se na na zakonima matematičkog aparata i koristi se matematičkim metodama. Predmet matematičke logike je dokaz.\r\n\r\nPosmatrajmo rečenice\r\n\r\n4 je paran broj (istinita)\r\nTriglav je viši od Monblana (neistinita)\r\nZa ove rečenice možemo reći da li su istinite ili neistinite Za rečenice kao što su\r\n\r\nx<4\r\nDonesi vode\r\nne možemo utvrditi istinitost\r\n\r\nNauka koja se bavi proučavanjem oblika mišljenja i vezama između tih oblika je logika. Logičko mišljenje je sam proces mišljenja. Za Z-skup cijelih brojeva vrijedi\r\n\r\n(a<b & b<c) =>a<c\r\n\r\nU logici je bitna forma, a ne sadržaj;\r\n\r\nČovjek je smrtan i Šekspir je smrtan znači Šekspir je čovjek\r\nAko je svaki kamen crven i ako je čovjek kamen znaći čovjek je crven.", new DateTime(2025, 10, 14, 9, 49, 5, 0, DateTimeKind.Utc), null, 11, "Matematicka logika" },
                    { 12, "Za Abelov teorem o algebarskim krivima, pogledajte članak Abel-Jacobijeva mapa\r\nZa Abelob teorem o nerješivosti jednačine petog stepena sa radikalima, pogledajte članak Abel-Ruffinijev teorem\r\nU matematici, Abelov teorem[1][2] za potencijalne redove dovodi u vezu graničnu vrijednost potencijalnog reda sa sumom njegovih koeficijenata. Teorem je dobio naziv po norveškom matematičaru Nielsu Henriku Abelu.", new DateTime(2025, 10, 14, 2, 55, 4, 0, DateTimeKind.Utc), null, 12, "Abelova grupa" },
                    { 13, "Vektorski ili linearni prostor je algebarski pojam u matematici koji nalazi primjenu u svim glavnim granama matematike, među kojima su linearna algebra, analiza i analitička geometrija. Definiše se na sljedeći način:\r\n\r\nNeka skup V ima strukturu Abelove grupe u odnosu na sabiranje. Elemente skupa V zovemo vektori. Neutralni element označujemo sa 0 i zovemo nulti vektor.\r\n\r\nNeka skup F ima strukturu polja. Elemente skupa F zovemo skalari, a neutralne elemente u odnosu na dvije binarne operacije označujemo sa 0 i 1.\r\n\r\nNa skupu F × V definirano je množenje vektora skalarom, tj. preslikavanje F × V → V, koje svakom skalaru \r\nα\r\n∈\r\nF\r\n{\\displaystyle \\alpha \\in F} i svakom vektoru \r\nx\r\n∈\r\nV\r\n{\\displaystyle x\\in V} pridružuje vektor \r\nα\r\nx\r\n∈\r\nV\r\n{\\displaystyle \\alpha x\\in V}, tako da vrijede sljedeći aksiomi:\r\n\r\n(I) \r\nα\r\n(\r\nβ\r\nx\r\n)\r\n=\r\n(\r\nα\r\nβ\r\n)\r\nx\r\n,\r\n∀\r\nα\r\n,\r\nβ\r\n∈\r\nF\r\n,\r\n∀\r\nx\r\n∈\r\nV\r\n{\\displaystyle \\alpha (\\beta x)=(\\alpha \\beta )x,\\forall \\alpha ,\\beta \\in F,\\forall x\\in V}\r\n(II) \r\nα\r\n(\r\nx\r\n+\r\ny\r\n)\r\n=\r\nα\r\nx\r\n+\r\nα\r\ny\r\n,\r\n∀\r\nα\r\n∈\r\nF\r\n,\r\n∀\r\nx\r\n,\r\ny\r\n∈\r\nV\r\n{\\displaystyle \\alpha (x+y)=\\alpha x+\\alpha y,\\forall \\alpha \\in F,\\forall x,y\\in V}\r\n(III) \r\n(\r\nα\r\n+\r\nβ\r\n)\r\nx\r\n=\r\nα\r\nx\r\n+\r\nβ\r\nx\r\n,\r\n∀\r\nα\r\n,\r\nβ\r\n∈\r\nF\r\n,\r\n∀\r\nx\r\n∈\r\nV\r\n{\\displaystyle (\\alpha +\\beta )x=\\alpha x+\\beta x,\\forall \\alpha ,\\beta \\in F,\\forall x\\in V}\r\n(IV) \r\n1\r\nx\r\n=\r\nx\r\n,\r\n∀\r\nx\r\n∈\r\nV\r\n{\\displaystyle 1x=x,\\forall x\\in V}\r\nOvako se definisano preslikavanje zove množenje vektora skalarom, dok se V naziva vektorski prostor nad poljem F i piše V(F).\r\n\r\nUobičajeno je da se vektorski prostori nad poljem realnih odnosno kompleksnih brojeva nazivaju realni, odnosno kompleksni vektorski prostori. Također, vektorski se prostor u kojem je definisan skalarni produkt naziva Euklidski vektorski prostor.", new DateTime(2025, 10, 14, 12, 46, 22, 0, DateTimeKind.Utc), null, 13, "vEKTORI" },
                    { 14, "Rođen je u Dizdara kuli u Vrgorcu. Njegovo puno ime bilo je Augustin Josip Ujević, po starom običaju župe imotskih Poljica, gdje su svoj pokrštenoj djeci davana dva imena. Njegov otac, Ivan Ujević, bio je učitelj rodom iz Krivodola u Imotskoj krajini, dok mu je majka Bračanka, iz mjesta Milne. Tin je rođen kao jedno od petero djece od kojih su dvoje umrli još u djetinjstvu.\r\n\r\nS očeve strane je mogao naslijediti određen književni talent, pošto je on kao učitelj bio sklon književnosti te i sam pisao. Tin je prva tri razreda osnovne škole polazio u Imotskom, kada seli u Makarsku gdje završava osnovnoškolsko obrazovanje. 1902. odlazi u Split gdje se upisuje u klasičnu gimnaziju i živi u nadbiskupijskom sjemeništu. U svojoj trinaestoj godini počinje pisati pjesme od kojih ništa nije sačuvano (po njemu je njegovo prvo djelo kratak tekst \"Voda\" koji je završio u košu za smeće nekog urednika). 1909. godine Tin maturira u Splitu s odličnim uspjehom, odriče se mogućnosti zaređenja te odlazi u Zagreb upisujući studij hrvatskog jezika i književnosti, klasične filologije, filozofije i estetike na Filozofskom fakultetu u Zagrebu. Te iste godine objavio je svoj prvi sonet \"Za novim vidicima\" u časopisu \"Mlada Hrvatska\".", new DateTime(2025, 10, 14, 1, 28, 4, 0, DateTimeKind.Utc), null, 14, "Tin Ujevic" },
                    { 15, "Fonologija je nauka koja proučava kako jezik iskorištava razlikovnu funkciju fonema radi komunikacije (od grč. φωνή, phone = glas, λόγος, lógos = riječ, govor, nauka). Ovo je disciplina koja proučava jezičku funkciju i ponašanje govornih jedinica. Najmanje jedinice koje imaju razlikovnu funkciju jesu fonemi (centralni pojam u fonologiji). Fonologija proučava sistem govornih jedinica (glasova) u jeziku, dok se fonetika bavi proučavanjem artikulacijskih i akustičnih obilježja glasova i govora. Uobičajeno je označiti fonološku transkripciju kosim zagradama / /.", new DateTime(2025, 10, 14, 5, 46, 44, 0, DateTimeKind.Utc), null, 15, "Fonologija" }
                });

            migrationBuilder.InsertData(
                table: "SharedNotebooks",
                columns: new[] { "Id", "FromUserId", "IsForEdit", "NotebookId", "SharedDate", "ToUserId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, false, 1, new DateTime(2025, 10, 13, 19, 49, 5, 0, DateTimeKind.Utc), 2, null },
                    { 2, 3, true, 14, new DateTime(2025, 10, 13, 11, 49, 5, 0, DateTimeKind.Utc), 2, null },
                    { 3, 2, false, 7, new DateTime(2025, 10, 11, 19, 49, 5, 0, DateTimeKind.Utc), 3, null },
                    { 4, 1, true, 4, new DateTime(2025, 10, 10, 11, 49, 5, 0, DateTimeKind.Utc), 3, null },
                    { 5, 3, false, 13, new DateTime(2025, 10, 9, 10, 49, 5, 0, DateTimeKind.Utc), 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collections_UserId",
                table: "Collections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PageId",
                table: "Comments",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SharedNotebookId",
                table: "Comments",
                column: "SharedNotebookId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotebookCollections_CollectionId",
                table: "NotebookCollections",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_NotebookCollections_NotebookId",
                table: "NotebookCollections",
                column: "NotebookId");

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_UserId",
                table: "Notebooks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_NotebookId",
                table: "Pages",
                column: "NotebookId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedNotebooks_FromUserId",
                table: "SharedNotebooks",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedNotebooks_NotebookId",
                table: "SharedNotebooks",
                column: "NotebookId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedNotebooks_ToUserId",
                table: "SharedNotebooks",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedNotebooks_UserId",
                table: "SharedNotebooks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "NotebookCollections");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "SharedNotebooks");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "Notebooks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
