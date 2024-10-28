using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManoTourism.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "admin", NormalizedName = "admin".ToUpper() });
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "employee", NormalizedName = "employee".ToUpper() });
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "lead", NormalizedName = "lead".ToUpper() });
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "accountant", NormalizedName = "accountant".ToUpper() });
            modelBuilder.Entity<ApplicationUser>()
               .Property(e => e.Pic);
            modelBuilder.Entity<ApplicationUser>()
             .Property(e => e.EntityId);
            modelBuilder.Entity<ApplicationUser>()
             .Property(e => e.FullName);

            this.SeedRoles(modelBuilder);
            this.SeedUsers(modelBuilder);
            this.SeedUserRoles(modelBuilder);
        }
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "897f7c7a-cce6-4503-a3f9-aee92acc8d33", Name = "admin", ConcurrencyStamp = "897f7c7a-cce6-4503-a3f9-aee92acc8d33", NormalizedName = "admin" },
                new IdentityRole() { Id = "6f826707-3414-46e3-b80c-0bc28f310dbb", Name = "employee", ConcurrencyStamp = "6f826707-3414-46e3-b80c-0bc28f310dbb", NormalizedName = "employee" },
                new IdentityRole() { Id = "6407b04c-cf88-4cb0-847e-d5d88340da0c", Name = "lead", ConcurrencyStamp = "6407b04c-cf88-4cb0-847e-d5d88340da0c", NormalizedName = "lead" },
                new IdentityRole() { Id = "b5a68ed4-507a-48f6-8347-ba7c2c840518", Name = "accountant", ConcurrencyStamp = "b5a68ed4-507a-48f6-8347-ba7c2c840518", NormalizedName = "accountant" }
                );
        }
        private void SeedUsers(ModelBuilder builder)
        {
            //Roles
            //6407b04c-cf88-4cb0-847e-d5d88340da0c      lead
            //6f826707-3414-46e3-b80c-0bc28f310dbb      emp
            //897f7c7a-cce6-4503-a3f9-aee92acc8d33      adm
            //b5a68ed4-507a-48f6-8347-ba7c2c840518      acc
            ApplicationUser user = new ApplicationUser()
            {
                Id = "a0325d4b-2a04-4d33-8e01-6f6f3afb3d8f",
                UserName = "sitedesign@gmail.com",
                Email = "sitedesign@gmail.com",
                NormalizedEmail = "SITEDESIGN@GMAIL.COM",
                NormalizedUserName = "SITEDESIGN@GMAIL.COM",
                FullName = "Shenouda && Mary",
                Pic = "Pic1.png",
                LockoutEnabled = false,
                PhoneNumber = "9080706050"
            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            var hashed=passwordHasher.HashPassword(user, "P@ssw0rd@123");
            user.PasswordHash = hashed;
            builder.Entity<ApplicationUser>().HasData(user);
            ApplicationUser admin = new ApplicationUser()
            {
                Id = "acd2498f-9519-400d-b43d-5a8ea7308cd5",
                UserName = "manoadmin@info.com",
                Email = "manoadmin@info.com",
                NormalizedEmail = "MANOADMIN@INFO.COM",
                NormalizedUserName = "MANOADMIN@INFO.COM",
                FullName = "admin",
                Pic = "Pic1.png",
                LockoutEnabled = false,
                PhoneNumber = "9080706050"
            };
            var adminHashed = passwordHasher.HashPassword(admin, "P@ssw0rd");
            admin.PasswordHash = adminHashed;
            builder.Entity<ApplicationUser>().HasData(admin);
            ApplicationUser Accountant = new ApplicationUser()
            {
                Id = "5e479daa-e034-4990-acf0-c3245efc4584",
                UserName = "accountant@info.com",
                Email = "accountant@info.com",
                NormalizedEmail = "ACCOUNTANT@INFO.COM",
                NormalizedUserName = "ACCOUNTANT@INFO.COM",
                FullName = "Accountant",
                Pic = "Pic1.png",
                LockoutEnabled = false,
                PhoneNumber = "6040302010"
            };
            var AccountantHashed = passwordHasher.HashPassword(Accountant, "Accountant@123");
            Accountant.PasswordHash = AccountantHashed;
            builder.Entity<ApplicationUser>().HasData(Accountant);
        }
        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "6407b04c-cf88-4cb0-847e-d5d88340da0c", UserId = "a0325d4b-2a04-4d33-8e01-6f6f3afb3d8f" }
                );
            builder.Entity<IdentityUserRole<string>>().HasData(
               new IdentityUserRole<string>() { RoleId = "897f7c7a-cce6-4503-a3f9-aee92acc8d33", UserId = "acd2498f-9519-400d-b43d-5a8ea7308cd5" }
               );
            builder.Entity<IdentityUserRole<string>>().HasData(
              new IdentityUserRole<string>() { RoleId = "b5a68ed4-507a-48f6-8347-ba7c2c840518", UserId = "5e479daa-e034-4990-acf0-c3245efc4584" }
              );
        }
    }
}