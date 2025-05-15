using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class IntialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    GeneroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.GeneroId);
                });

            migrationBuilder.CreateTable(
                name: "Productora",
                columns: table => new
                {
                    ProductoraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productora", x => x.ProductoraId);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Portada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkYT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductoraId = table.Column<int>(type: "int", nullable: true),
                    GeneroPrimarioId = table.Column<int>(type: "int", nullable: true),
                    GeneroSecundarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Generos_GeneroPrimarioId",
                        column: x => x.GeneroPrimarioId,
                        principalTable: "Generos",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Series_Generos_GeneroSecundarioId",
                        column: x => x.GeneroSecundarioId,
                        principalTable: "Generos",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Series_Productora_ProductoraId",
                        column: x => x.ProductoraId,
                        principalTable: "Productora",
                        principalColumn: "ProductoraId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SerieGeneros",
                columns: table => new
                {
                    SerieId = table.Column<int>(type: "int", nullable: false),
                    GeneroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieGeneros", x => new { x.SerieId, x.GeneroId });
                    table.ForeignKey(
                        name: "FK_SerieGeneros_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SerieGeneros_Series_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "GeneroId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Comedia" },
                    { 2, "Drama" },
                    { 3, "Acción" },
                    { 4, "Suspenso" },
                    { 5, "Thriller" },
                    { 6, "Fantasía" },
                    { 7, "Animación" },
                    { 8, "Terror" },
                    { 9, "Aventura" }
                });

            migrationBuilder.InsertData(
                table: "Productora",
                columns: new[] { "ProductoraId", "Nombre" },
                values: new object[,]
                {
                    { 1, "NBC" },
                    { 2, "HBO" },
                    { 3, "Netflix" },
                    { 4, "Fox Television Studio" },
                    { 5, "Gracie Films" },
                    { 6, "Nickelodeon" },
                    { 7, "Incendo Films" },
                    { 8, "Nova Veranda" },
                    { 9, "LP Entertainment" },
                    { 10, "Cartoon Network" },
                    { 11, "Disney Channel" }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "GeneroPrimarioId", "GeneroSecundarioId", "LinkYT", "Nombre", "Portada", "ProductoraId" },
                values: new object[,]
                {
                    { 1, 1, 2, "https://www.youtube.com/embed/OZL2nwJalQk?si=OS9r2X-RAV3NJQLn&amp;controls=0", "The Office", "/Imagenes/theoffice.jpg", 1 },
                    { 2, 3, 6, "https://www.youtube.com/embed/beG07Q917rU?si=T1cmfEp-yqdna5SB", "Game of Thrones", "/Imagenes/got.jpg", 2 },
                    { 3, 5, 4, "https://www.youtube.com/embed/yjmDBKyemUw?si=R-3nbfvcVZt9ACWH", "Stranger Things", "/Imagenes/strangerthings.jpg", 3 },
                    { 4, 1, null, "https://www.youtube.com/embed/l19tQk23noI?si=h5Vin_NgfD_HNPeH", "Malcolm In The Middle", "/Imagenes/malcolm.jpg", 4 },
                    { 5, 1, 7, "https://www.youtube.com/embed/F-dNggtr8E0?si=NUxjZp6dKW4u5jil", "The Simpsons", "/Imagenes/simpsons.jpg", 5 },
                    { 6, 2, 3, "https://www.youtube.com/embed/kQHo8DU-_Sk?si=fbPPDe6mzC_eJTxh", "El Chapo", "/Imagenes/chapo.jpg", 3 },
                    { 7, 7, 6, "https://www.youtube.com/embed/dDhB4_mt2f4?si=5pod83hpdV0bYadx", "Winx Club", "/Imagenes/winx.jpg", 6 },
                    { 8, 8, 5, "https://www.youtube.com/embed/_g8E35l0L0w?si=qceFMM7FbR8llu65", "The Haunting Hour", "/Imagenes/hauntinghour.jpg", 7 },
                    { 9, 2, null, "https://www.youtube.com/embed/6oqiksG962M?si=fhYMXnHbkVHuyU6u", "Merlí", "/Imagenes/merli.jpg", 8 },
                    { 10, 4, 7, "https://www.youtube.com/embed/kOKk1wMmZBY?si=OAtXr_WMcJ5G7MzT", "Death Note", "/Imagenes/deathnote.jpg", 9 },
                    { 11, 1, 2, "https://www.youtube.com/embed/v4IaSXl-JPw?si=MXZSmrQlF_4adDfl", "Scream Queens", "/Imagenes/screamqueens.jpg", 4 },
                    { 12, 9, 7, "https://www.youtube.com/embed/n9Z5dpGuThM?si=4gz0q_1VBhSZ32-T", "The Backyardigans", "/Imagenes/backyardigans.jpg", 6 },
                    { 13, 1, 9, "https://www.youtube.com/embed/aOBqbiBDntw?si=x12iU2dMI_kVOPZA", "Regular Show", "/Imagenes/regularshow.jpg", 10 },
                    { 14, 1, 9, "https://www.youtube.com/embed/P6EWLTBs-dQ?si=pDYG32N8QvawuOXN", "Adventure Time", "/Imagenes/adventuretime.jpg", 10 },
                    { 15, 2, null, "https://www.youtube.com/embed/AnAAy91YDy4?si=y9wDI1TCXUfY1WLG", "Violetta", "/Imagenes/violetta.jpg", 11 },
                    { 16, 6, 3, "https://www.youtube.com/embed/JAg1XgKEG8U?si=_kwggta2ZGeJP32a", "American Dragon Jake", "/Imagenes/americandragon.jpg", 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SerieGeneros_GeneroId",
                table: "SerieGeneros",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_GeneroPrimarioId",
                table: "Series",
                column: "GeneroPrimarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_GeneroSecundarioId",
                table: "Series",
                column: "GeneroSecundarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_ProductoraId",
                table: "Series",
                column: "ProductoraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SerieGeneros");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Productora");
        }
    }
}
