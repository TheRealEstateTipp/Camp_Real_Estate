using System;
using System.Collections.Generic;
using System.Text;
using CampRealEstate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CampRealEstate.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<LoanOfficer> LoanOfficers { get; set; }
        public DbSet<RealEstateAgent> RealEstateAgents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Client",
                    NormalizedName = "CLIENT"
                },
                new IdentityRole
                {
                    Name = "RealEstateAgent",
                    NormalizedName = "REAL ESTATE AGENT"
                },
                new IdentityRole
                {
                    Name = "LoanOfficer",
                    NormalizedName = "LOAN OFFICER"
                },

                new IdentityRole
                {
                    Name = "Contractor",
                    NormalizedName = "CONTRACTOR"
                }

            );
        }


    }
}
