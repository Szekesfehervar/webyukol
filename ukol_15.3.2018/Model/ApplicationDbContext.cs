using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ukol_15_3_2018.Model
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "ADMIN", Name = "Administrátor", NormalizedName = "ADMINISTRÁTOR" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "USER", Name = "Uživatel", NormalizedName = "UŽIVATEL" });
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "ADMINUSER",
                FirstName = "Hlavní",
                LastName = "Administrator",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "michal.stehlik@pslib.cz",
                NormalizedEmail = "MICHAL.STEHLIK@PSLIB.CZ",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = string.Empty,
                PasswordHash = hasher.HashPassword(null, "Admin.1234")
            });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = "ADMIN", UserId = "ADMINUSER" });
            builder.Entity<Note>().HasData(new Note { Id = 1, OwnerId = "Frajer Pepa", Title = "pepa je fakt fraja" });
        }


    }
}
