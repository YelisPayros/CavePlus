using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DB.Contexts
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Series> Series { get; set; }
        public DbSet<Productora> Productoras { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<SerieGenero> SerieGeneros { get; set; }

        //haciendo conversiones manual
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TABLES

            //modificar nombre
            modelBuilder.Entity<Productora>().ToTable("Productora");

            //primary keys
            modelBuilder.Entity<Series>().HasKey(serie => serie.Id);
            modelBuilder.Entity<Productora>().HasKey(productora => productora.ProductoraId);
            modelBuilder.Entity<Genero>().HasKey(genero => genero.GeneroId);

            //relaciones
            //series-productora
            modelBuilder.Entity<Series>()
                .HasOne(s => s.Productora)  // Una serie tiene una productora
                .WithMany(p => p.Series)    // Una productora tiene muchas series
                .HasForeignKey(s => s.ProductoraId)
                .OnDelete(DeleteBehavior.SetNull);

            //series-generoprimario
            modelBuilder.Entity<Series>()
               .HasOne(s => s.GeneroPrimario)
               .WithMany()
               .HasForeignKey(s => s.GeneroPrimarioId)
               .OnDelete(DeleteBehavior.Restrict);

            //series-generosecundario
            modelBuilder.Entity<Series>()
               .HasOne(s => s.GeneroSecundario)
               .WithMany()
               .HasForeignKey(s => s.GeneroSecundarioId)
               .OnDelete(DeleteBehavior.Restrict);

            //relacion muchos a muchos genero-series
            modelBuilder.Entity<SerieGenero>()
            .HasKey(sg => new { sg.SerieId, sg.GeneroId });  // Clave compuesta

            modelBuilder.Entity<SerieGenero>()
                .HasOne(sg => sg.Series)
                .WithMany(s => s.SerieGeneros)
                .HasForeignKey(sg => sg.SerieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SerieGenero>()
                .HasOne(sg => sg.Genero)
                .WithMany(g => g.SerieGeneros)
                .HasForeignKey(sg => sg.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);


            //propiedades
            modelBuilder.Entity<Series>()
             .Property(s => s.Nombre)
             .HasMaxLength(100);

            modelBuilder.Entity<Productora>()
             .Property(p => p.Nombre)
             .HasMaxLength(100);

            modelBuilder.Entity<Genero>()
             .Property(g => g.Nombre)
             .HasMaxLength(50);



            //Datos Productora
            var productoras = new List<Productora>
        {
                new Productora { ProductoraId = 1, Nombre = "NBC" },
                new Productora { ProductoraId = 2, Nombre = "HBO" },
                new Productora { ProductoraId = 3, Nombre = "Netflix" },
                new Productora { ProductoraId = 4, Nombre = "Fox Television Studio" },
                new Productora { ProductoraId = 5, Nombre = "Gracie Films" },
                new Productora { ProductoraId = 6, Nombre = "Nickelodeon" },
                new Productora { ProductoraId = 7, Nombre = "Incendo Films" },
                new Productora { ProductoraId = 8, Nombre = "Nova Veranda" },
                new Productora { ProductoraId = 9, Nombre = "LP Entertainment" },
                new Productora { ProductoraId = 10, Nombre = "Cartoon Network" },
                new Productora { ProductoraId = 11, Nombre = "Disney Channel" },

        };

            // Datos de Géneros
            var generos = new List<Genero>
        {
               new Genero { GeneroId = 1, Nombre = "Comedia" },
               new Genero { GeneroId = 2, Nombre = "Drama" },
               new Genero { GeneroId = 3, Nombre = "Acción" },
               new Genero { GeneroId = 4, Nombre = "Suspenso" },
               new Genero { GeneroId = 5, Nombre = "Thriller" },
               new Genero { GeneroId = 6, Nombre = "Fantasía" },
               new Genero { GeneroId = 7, Nombre = "Animación" },
               new Genero { GeneroId = 8, Nombre = "Terror" },
               new Genero { GeneroId = 9, Nombre = "Aventura" }
        };  

            // Datos de Series
            var series = new List<Series>
        {
                new Series { Id = 1, Nombre = "The Office", Portada = "/Imagenes/theoffice.jpg", LinkYT = "https://www.youtube.com/embed/OZL2nwJalQk?si=OS9r2X-RAV3NJQLn&amp;controls=0", ProductoraId = 1, GeneroPrimarioId = 1, GeneroSecundarioId= 2 },
                new Series { Id = 2, Nombre = "Game of Thrones", Portada = "/Imagenes/got.jpg", LinkYT = "https://www.youtube.com/embed/beG07Q917rU?si=T1cmfEp-yqdna5SB", ProductoraId = 2, GeneroPrimarioId = 3, GeneroSecundarioId= 6},
                new Series { Id = 3, Nombre = "Stranger Things", Portada = "/Imagenes/strangerthings.jpg", LinkYT = "https://www.youtube.com/embed/yjmDBKyemUw?si=R-3nbfvcVZt9ACWH", ProductoraId = 3, GeneroPrimarioId = 5, GeneroSecundarioId= 4 },
                new Series { Id = 4, Nombre = "Malcolm In The Middle", Portada = "/Imagenes/malcolm.jpg", LinkYT = "https://www.youtube.com/embed/l19tQk23noI?si=h5Vin_NgfD_HNPeH", ProductoraId = 4, GeneroPrimarioId = 1, GeneroSecundarioId= null },
                new Series { Id = 5, Nombre = "The Simpsons", Portada = "/Imagenes/simpsons.jpg", LinkYT = "https://www.youtube.com/embed/F-dNggtr8E0?si=NUxjZp6dKW4u5jil", ProductoraId = 5, GeneroPrimarioId = 1, GeneroSecundarioId= 7 },
                new Series { Id = 6, Nombre = "El Chapo", Portada = "/Imagenes/chapo.jpg", LinkYT = "https://www.youtube.com/embed/kQHo8DU-_Sk?si=fbPPDe6mzC_eJTxh", ProductoraId = 3, GeneroPrimarioId = 2, GeneroSecundarioId= 3 },
                new Series { Id = 7, Nombre = "Winx Club", Portada = "/Imagenes/winx.jpg", LinkYT = "https://www.youtube.com/embed/dDhB4_mt2f4?si=5pod83hpdV0bYadx", ProductoraId = 6, GeneroPrimarioId = 7, GeneroSecundarioId= 6 },
                new Series { Id = 8, Nombre = "The Haunting Hour", Portada = "/Imagenes/hauntinghour.jpg", LinkYT = "https://www.youtube.com/embed/_g8E35l0L0w?si=qceFMM7FbR8llu65", ProductoraId = 7, GeneroPrimarioId = 8, GeneroSecundarioId= 5 },
                new Series { Id = 9, Nombre = "Merlí", Portada = "/Imagenes/merli.jpg", LinkYT = "https://www.youtube.com/embed/6oqiksG962M?si=fhYMXnHbkVHuyU6u", ProductoraId = 8, GeneroPrimarioId = 2, GeneroSecundarioId= null },
                new Series { Id = 10, Nombre = "Death Note", Portada = "/Imagenes/deathnote.jpg", LinkYT = "https://www.youtube.com/embed/kOKk1wMmZBY?si=OAtXr_WMcJ5G7MzT", ProductoraId = 9, GeneroPrimarioId = 4, GeneroSecundarioId= 7 },
                new Series { Id = 11, Nombre = "Scream Queens", Portada = "/Imagenes/screamqueens.jpg", LinkYT = "https://www.youtube.com/embed/v4IaSXl-JPw?si=MXZSmrQlF_4adDfl", ProductoraId = 4, GeneroPrimarioId = 1, GeneroSecundarioId= 2 },
                new Series { Id = 12, Nombre = "The Backyardigans", Portada = "/Imagenes/backyardigans.jpg", LinkYT = "https://www.youtube.com/embed/n9Z5dpGuThM?si=4gz0q_1VBhSZ32-T", ProductoraId = 6, GeneroPrimarioId = 9, GeneroSecundarioId= 7 },
                new Series { Id = 13, Nombre = "Regular Show", Portada = "/Imagenes/regularshow.jpg", LinkYT = "https://www.youtube.com/embed/aOBqbiBDntw?si=x12iU2dMI_kVOPZA", ProductoraId = 10, GeneroPrimarioId = 1, GeneroSecundarioId= 9 },
                new Series { Id = 14, Nombre = "Adventure Time", Portada = "/Imagenes/adventuretime.jpg", LinkYT = "https://www.youtube.com/embed/P6EWLTBs-dQ?si=pDYG32N8QvawuOXN", ProductoraId = 10, GeneroPrimarioId = 1, GeneroSecundarioId= 9 },
                new Series { Id = 15, Nombre = "Violetta", Portada = "/Imagenes/violetta.jpg", LinkYT = "https://www.youtube.com/embed/AnAAy91YDy4?si=y9wDI1TCXUfY1WLG", ProductoraId = 11, GeneroPrimarioId = 2, GeneroSecundarioId= null },
                new Series { Id = 16, Nombre = "American Dragon Jake", Portada = "/Imagenes/americandragon.jpg", LinkYT = "https://www.youtube.com/embed/JAg1XgKEG8U?si=_kwggta2ZGeJP32a", ProductoraId = 11, GeneroPrimarioId = 6, GeneroSecundarioId= 3 }



        };

            // Agregar los datos usando HasData
            modelBuilder.Entity<Productora>().HasData(productoras);
            modelBuilder.Entity<Genero>().HasData(generos);
            modelBuilder.Entity<Series>().HasData(series);

            base.OnModelCreating(modelBuilder);
        }

    }
}
