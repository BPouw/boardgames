using System;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BoardgamesContext: DbContext
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<GameNight> GameNight { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<GameNightGame> GameNight_Game { get; set; }
        public DbSet<GameNightPlayer> GameNight_Player { get; set; }

        public BoardgamesContext(DbContextOptions<BoardgamesContext> contextOptions): base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>().HasData(
             new Address() { Id = 1, StreetName = "Tramsingel", City = "Breda", HouseNumber = 97, PostalCode = "4814AE" });

            modelBuilder.Entity<Person>().HasData(
               new Person() { Id = 1, Name = "Boris Pouw", DateOfBirth = new DateTime(1998, 07, 08), Email = "boris@email.com", Gender = Gender.M, Shows = 0, NoShows = 0, AddressId = 1, });

            modelBuilder.Entity<Address>().HasData(
             new Address() { Id = 2, StreetName = "Pleinweg", City = "Rotterdam", HouseNumber = 199, PostalCode = "5317MJ" });

            modelBuilder.Entity<Person>().HasData(
               new Person() { Id = 2, Name = "Stefi Nicoara", DateOfBirth = new DateTime(1999, 01, 22), Email = "ntstefi@email.com", Gender = Gender.V, Shows = 0, NoShows = 0, AddressId = 2, });

            modelBuilder.Entity<Address>().HasData(
             new Address() { Id = 3, StreetName = "Teststraat", City = "Devtown", HouseNumber = 52, PostalCode = "4452SG" });

            modelBuilder.Entity<Person>().HasData(
               new Person() { Id = 3, Name = "Piet Test", DateOfBirth = new DateTime(2000, 03, 20), Email = "piet@email.com", Gender = Gender.M, Shows = 0, NoShows = 0, AddressId = 3, });

            modelBuilder.Entity<Game>().HasIndex(o => o.Name).IsUnique();

            modelBuilder.Entity<Game>().HasData(
               new Game() { Id = 1, Name = "MarioKart", AdultsOnly = false, category = Category.Computergame, Description = "It's me Mario", genre = Genre.Racing, });

            modelBuilder.Entity<Game>().HasData(
                 new Game() { Id = 2, Name = "Monopoly", AdultsOnly = false, category = Category.Boardgame, Description = "Can you buy all the houses?", genre = Genre.Family, });

            modelBuilder.Entity<Game>().HasData(
                 new Game() { Id = 3, Name = "Scrabble", AdultsOnly = false, category = Category.Boardgame, Description = "Scribble, Scrubble, Scrabble", genre = Genre.Mind, });

            modelBuilder.Entity<Game>().HasData(
                 new Game() { Id = 4, Name = "Cards against humanity", AdultsOnly = true, category = Category.Cardgame, Description = "How offensive can you get?", genre = Genre.Fantasy, });

            modelBuilder.Entity<Game>().HasData(
                  new Game() { Id = 5, Name = "Poker", AdultsOnly = true, category = Category.Cardgame, Description = "Nice hand bro", genre = Genre.Mind, });

            modelBuilder.Entity<Game>().HasData(
                 new Game() { Id = 6, Name = "Blackjack", AdultsOnly = true, category = Category.Cardgame, Description = "Dont hit at 21", genre = Genre.Mind, });


            modelBuilder.Entity<GameNight>().HasData(
             new GameNight() { Id = 1, Name = "MarioKart session", MaxPlayers = 6, DateTime = DateTime.Now, AddressId = 1, OrganiserId = 1, AdultsOnly = false });

            modelBuilder.Entity<GameNight>().HasData(
             new GameNight() { Id = 2, Name = "Poker night", MaxPlayers = 6, DateTime = DateTime.Now, AddressId = 3, OrganiserId = 2, AdultsOnly = true });

            modelBuilder.Entity<Person>()
                .HasMany(x => x.GameNights)
                .WithMany(x => x.Players)
                .UsingEntity<GameNightPlayer>(
                     x => x.HasOne(x => x.GameNight)
                     .WithMany().HasForeignKey(x => x.GameNightId),
                     x => x.HasOne(x => x.Person)
                     .WithMany().HasForeignKey(x => x.PersonId)
                     .OnDelete(DeleteBehavior.NoAction)
                    );


            modelBuilder.Entity<Game>()
               .HasMany(x => x.GameNights)
               .WithMany(x => x.Games)
               .UsingEntity<GameNightGame>(
                   x => x.HasOne(x => x.GameNight)
                   .WithMany().HasForeignKey(x => x.GameNightId),
                   x => x.HasOne(x => x.Game)
                  .WithMany().HasForeignKey(x => x.GameId));


            modelBuilder.Entity<Person>()
              .HasOne(p => p.Address)
              .WithMany(b => b.Persons)
              .HasForeignKey(p => p.AddressId);

            modelBuilder.Entity<GameNight>()
                .HasOne(p => p.Address)
                .WithMany(b => b.GameNights)
                .HasForeignKey(p => p.AddressId);

            modelBuilder.Entity<GameNight>()
                .HasOne(p => p.Organiser)
                .WithMany(b => b.OrganisedGameNights)
                .HasForeignKey(p => p.OrganiserId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<GameNightPlayer>().HasData(
             new GameNightPlayer() { GameNightId = 1, PersonId = 1 });
            modelBuilder.Entity<GameNightPlayer>().HasData(
           new GameNightPlayer() { GameNightId = 1, PersonId = 2 });
            modelBuilder.Entity<GameNightPlayer>().HasData(
           new GameNightPlayer() { GameNightId = 1, PersonId = 3 });

            modelBuilder.Entity<GameNightGame>().HasData(
            new GameNightGame() { GameNightId = 1, GameId = 1 });



        }
    }
}

