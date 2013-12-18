using System;
using System.Collections.Generic;
using System.Data.Common;
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
           
            modelBuilder.Entity<User>().ToTable("Users", "visitor");
            modelBuilder.Entity<AdminUser>().ToTable("AdminUsers");
            modelBuilder.Entity<EmployeeUser>().ToTable("EmployeeUsers");
            modelBuilder.Entity<Duty>().ToTable("Dutys");
            modelBuilder.Entity<OperativeWorker>().ToTable("OperativeWorkers");
            modelBuilder.Entity<Driver>().ToTable("Drivers");
            modelBuilder.Entity<Statement>().ToTable("Statements", "visitor");
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
    //Класс для хранимых процедур , триггеров и функций
    public class ForDB : IDatabaseInitializer<DataContext>
    {
        public void InitializeDatabase(DataContext bases)
        {
            if (!bases.Database.CompatibleWithModel(false) || !bases.Database.Exists())
            {
                bases.Database.Delete();
                bases.Database.Create();

                bases.Database.Connection.Open();
                DbCommand command = null;

                // Триггер для изменения поля в оперативном работнике ипри задании ему собаки 
                command = bases.Database.Connection.CreateCommand();
                command.CommandText = @"
                    CREATE TRIGGER CreatDog on WarDogs
                    AFTER INSERT
                    AS
                    BEGIN
                            SET NOCOUNT ON;
		                    DECLARE @IdOwner int;
		                    SELECT @IdOwner = Id FROM OperativeWorkers WHERE
							                    Id = (SELECT IdDog FROM inserted);
		                    UPDATE
			                    OperativeWorkers
		                    SET
			                    OperativeWorkers.IsHaveDog = 1
		                    WHERE OperativeWorkers.Id = @IdOwner
                    END;
                    ";
                command.ExecuteNonQuery();
                // Триггер для изменения полья в оперативном работнике при удалении собаки
                command = bases.Database.Connection.CreateCommand();
                command.CommandText = @"
                    CREATE TRIGGER CreatDog on WarDogs
                    AFTER INSERT
                    AS
                    BEGIN
                            SET NOCOUNT ON;
		                    DECLARE @IdOwner int;
		                    SELECT @IdOwner = Id FROM OperativeWorkers WHERE
							                    Id = (SELECT IdDog FROM inserted);
		                    UPDATE
			                    OperativeWorkers
		                    SET
			                    OperativeWorkers.IsHaveDog = 1
		                    WHERE OperativeWorkers.Id = @IdOwner
                    END;
                    ";
                command.ExecuteNonQuery();
                // Процедура для получения ФИО начальника группы, которая произвела задержание
                command = bases.Database.Connection.CreateCommand();
                command.CommandText = @"
                    CREATE PROCEDURE [dbo].[GetDetention]
	                    @Lastname nvarchar(50),
	                    @Firstname nvarchar(50),
	                    @Secondname nvarchar(50)
                    AS
	                    DECLARE @numberProtocol int
	                    SELECT @numberProtocol = Protocol_NumberProtocol
	                    FROM [Detentions]
	                    WHERE
		                    DetentionLastName = @Lastname AND
		                    DetentionFirstName = @Firstname AND
		                    DetentionSecondName = @Secondname

	                    DECLARE @idGroup int
	                    SELECT @idGroup = Group_IdGroup
	                    FROM [LeavingGroups]
	                    WHERE
		                     IdLeaving = @numberProtocol

	                    DECLARE @idOW int
	                    SELECT @idOW = Id
	                    FROM [OperativeWorkers]
	                    WHERE
		                     Group_IdGroup = @idGroup AND
		                     IsHeadOfGroup = 1
	
	                    SELECT LastName, FirstName, SecondName
	                    FROM [visitor].Users
	                    WHERE
		                    Id = @idOW
                    RETURN 0;
                    ";
                command.ExecuteNonQuery();
                //Функция для получения ФИО главноего водителя по номеру авто
                command = bases.Database.Connection.CreateCommand();
                command.CommandText = @"
                    CREATE FUNCTION [dbo].[GetCarFIO]
                    (
	                    @Numbercar nvarchar(50)
                    )
                    RETURNS @returntable TABLE
                    (
	                    LastName nvarchar(50),
	                    FirstName nvarchar(50),
	                    SecondName nvarchar(50)
                    )
                    AS
                    BEGIN
	                    DECLARE @IdCar int
	                    SELECT @IdCar = IdCar
	                    FROM [Cars]
	                    WHERE NumberCar = @Numbercar

	                    DECLARE @IdDriver int
	                    SELECT @IdDriver = Id
	                    FROM [Drivers]
	                    WHERE WorkingCar_IdCar = @IdCar
	
	                    DECLARE @LastName nvarchar(50),
			                    @FirstName nvarchar(50),
			                    @SecondName nvarchar(50)
	                    SELECT @LastName = LastName FROM [visitor].Users WHERE Id = @IdDriver
	                    SELECT @FirstName = FirstName FROM [visitor].Users WHERE Id = @IdDriver
	                    SELECT @SecondName = SecondName FROM [visitor].Users WHERE Id = @IdDriver

	                    INSERT @returntable
	                    SELECT @LastName, @FirstName, @SecondName
	                    RETURN
                    END;
                    ";
                command.ExecuteNonQuery();
                // Функция для получения ФИО дежурного, который принял заявление
                command = bases.Database.Connection.CreateCommand();
                command.CommandText = @"
                    CREATE FUNCTION [dbo].[GetStatementFIO]
                    (
	                    @Idstatement int
                    )
                    RETURNS @returntable TABLE
                    (
	                    LastName nvarchar(50),
	                    FirstName nvarchar(50),
	                    SecondName nvarchar(50)
                    )
                    AS
                    BEGIN
	                    DECLARE @IdUser int
	                    SELECT @IdUser = Duty_Id
	                    FROM [visitor].Statements
	                    WHERE NumberStatement = @Idstatement
	
	                    DECLARE @LastName nvarchar(50),
			                    @FirstName nvarchar(50),
			                    @SecondName nvarchar(50)
	                    SELECT @LastName = LastName FROM [visitor].Users WHERE Id = @IdUser
	                    SELECT @FirstName = FirstName FROM [visitor].Users WHERE Id = @IdUser
	                    SELECT @SecondName = SecondName FROM [visitor].Users WHERE Id = @IdUser

	                    INSERT @returntable
	                    SELECT @LastName, @FirstName, @SecondName
	                    RETURN
                    END;
                    ";
                command.ExecuteNonQuery();
                bases.SaveChanges();
            }
        }
    }
}