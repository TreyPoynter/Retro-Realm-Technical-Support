using Microsoft.EntityFrameworkCore;
using RetroRealm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RetroRealm.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<GameModel> Games { get; set; }
        public DbSet<TechnicianModel> Technicians { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<IncidentModel> Incidents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CountryModel>().HasData(
                new CountryModel() { CountryModelId = 1, Country = "United States" },
                new CountryModel() { CountryModelId = 2, Country = "Canada" },
                new CountryModel() { CountryModelId = 3, Country = "United Kingdom" },
                new CountryModel() { CountryModelId = 4, Country = "Germany" },
                new CountryModel() { CountryModelId = 5, Country = "France" },
                new CountryModel() { CountryModelId = 6, Country = "Australia" },
                new CountryModel() { CountryModelId = 7, Country = "Japan" },
                new CountryModel() { CountryModelId = 8, Country = "Brazil" },
                new CountryModel() { CountryModelId = 9, Country = "India" },
                new CountryModel() { CountryModelId = 10, Country = "South Africa" }
            );

            modelBuilder.Entity<GameModel>().HasData(
                new GameModel
                {
                    GameModelId = 1,
                    Code = "CNTPD91",
                    Title = "Centipede",
                    Price = 10m,
                    ReleaseDate = new DateOnly(1981, 6, 15)
                },
                new GameModel
                {
                    GameModelId = 2,
                    Code = "ASTRD79",
                    Title = "Asteroids",
                    Price = 20m,
                    ReleaseDate = new DateOnly(1979, 11, 17)
                },
                new GameModel
                {
                    GameModelId = 3,
                    Code = "ADVTR80",
                    Title = "Adventure",
                    Price = 20m,
                    ReleaseDate = new DateOnly(1980, 10, 12)
                }
                );
            modelBuilder.Entity<TechnicianModel>().HasData(
                new TechnicianModel()
                {
                    TechnicianModelId = -1,
                    Name = "Not Assigned",
                    Email = "",
                    Phone = ""
                },
                new TechnicianModel()
                {
                    TechnicianModelId = 1,
                    Name = "Alison Diaz",
                    Email = "alison@retrorealmsoftware.com",
                    Phone = "800-555-0449"
                },
                new TechnicianModel()
                {
                    TechnicianModelId = 2,
                    Name = "Tony Chef",
                    Email = "tonyc@retrorealmsoftware.com",
                    Phone = "314-123-4567"
                },
                new TechnicianModel()
                {
                    TechnicianModelId = 3,
                    Name = "Trey Poynter",
                    Email = "poynter@retrorealmsoftware.com",
                    Phone = "573-789-1234"
                },
                new TechnicianModel()
                {
                    TechnicianModelId = 4,
                    Name = "John Doe",
                    Email = "johnd@retrorealmsoftware.com",
                    Phone = "111-222-3333"
                });
            modelBuilder.Entity<CustomerModel>().HasData(
                new CustomerModel()
                {
                    CustomerModelId = 1,
                    Firstname = "Ania",
                    Lastname = "Irvin",
                    Address = "989 Beach Way",
                    City = "San Fransisco",
                    State = "CA",
                    PostalCode = "94110",
                    Email = "ania@yahoo.com",
                    Phone = "314-890-7889",
                    CountryModelId = 1
                });

            modelBuilder.Entity<CustomerModel>().HasMany(c => c.GameModels)
                .WithMany(g => g.CustomerModels)
                .UsingEntity<Dictionary<string, object>>(
                    "CustomerGames",  // what the linking table is called
                    ta => ta.HasOne<GameModel>()  // set activity foreign key column
                        .WithMany().OnDelete(DeleteBehavior.Restrict)
                        .HasForeignKey("GameModelId"),
                    ta => ta.HasOne<CustomerModel>()  // set trip foreign key column
                        .WithMany()
                        .HasForeignKey("CustomerModelId"),
                    ta => ta.HasData(
                        new { GameModelId = 1, CustomerModelId = 1 },
                        new { GameModelId = 3, CustomerModelId = 1 })
                );

            modelBuilder.Entity<IncidentModel>().HasData(
                new IncidentModel()
                {
                    IncidentModelId = 1,
                    CustomerModelId = 1,
                    GameModelId = 2,
                    Title = "Error launching game",
                    Description = "When I insert cartridge the game flashes and crashes",
                    TechnicianModelId = 3,
                    DateOpened = new DateTime(2024, 1, 20)
                });
        }
    }
}
