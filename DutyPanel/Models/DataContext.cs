using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DutyPanel.Models
{
    public class DataContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EmployeeUser> EmployeeUsers { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Merit> Merits { get; set; }
        public DbSet<Duty> Dutys { get; set; }
        public DbSet<OperativeWorker> OperativeWorkers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<InternetStatement> InternetStatements { get; set; }
        public DbSet<OralStatement> OralStatements { get; set; }
        public DbSet<WrittenStatement> WrittenStatements { get; set; }
        public DbSet<WarDog> WarDogs { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<OperationalGroup> OperationalGroups { get; set; }
        public DbSet<LeavingGroup> LeavingsGroups { get; set; }
        public DbSet<Protocol> Protocols { get; set; }
        public DbSet<Detention> Detentions { get; set; }

        public DataContext() : base("DefaultConnection") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<AdminUser>().ToTable("AdminUsers");
            modelBuilder.Entity<EmployeeUser>().ToTable("EmployeeUsers");
            modelBuilder.Entity<Duty>().ToTable("Dutys");
            modelBuilder.Entity<OperativeWorker>().ToTable("OperativeWorkers");
            modelBuilder.Entity<Driver>().ToTable("Drivers");
            modelBuilder.Entity<Statement>().ToTable("Statements");
            modelBuilder.Entity<InternetStatement>().ToTable("InternetStatements");
            modelBuilder.Entity<OralStatement>().ToTable("OralStatements");
            modelBuilder.Entity<WrittenStatement>().ToTable("WrittenStatements");
            modelBuilder.Entity<LeavingGroup>()
            .HasOptional(f => f.Protocol)
            .WithRequired(s => s.Leaving);
            modelBuilder.Entity<Driver>()
            .HasOptional(f => f.Group)
            .WithOptionalDependent(s => s.Driver);
        }

    }
}