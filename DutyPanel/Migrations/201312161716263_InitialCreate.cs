namespace DutyPanel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(),
                        PassportNumber = c.Int(nullable: false),
                        WhoGivePassport = c.String(nullable: false),
                        DateGivePassport = c.DateTime(nullable: false),
                        SubdivisionPasport = c.Int(nullable: false),
                        RegistrationAddress = c.String(nullable: false),
                        ContactPhone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Merits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateOfAssignment = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        Promotion = c.String(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeUsers", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.Statements",
                c => new
                    {
                        NumberStatement = c.Int(nullable: false, identity: true),
                        DeclarantLastName = c.String(nullable: false),
                        DeclarantFirstName = c.String(nullable: false),
                        DeclarantSecondName = c.String(),
                        DateStatment = c.DateTime(nullable: false),
                        DaetIncident = c.DateTime(nullable: false),
                        Adress = c.String(nullable: false),
                        District = c.String(nullable: false),
                        Harm = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        Duty_Id = c.Int(),
                    })
                .PrimaryKey(t => t.NumberStatement)
                .ForeignKey("dbo.Dutys", t => t.Duty_Id)
                .Index(t => t.Duty_Id);
            
            CreateTable(
                "dbo.LeavingGroups",
                c => new
                    {
                        IdLeaving = c.Int(nullable: false),
                        TimeLeaving = c.DateTime(nullable: false),
                        Group_IdGroup = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdLeaving)
                .ForeignKey("dbo.Statements", t => t.IdLeaving)
                .ForeignKey("dbo.OperationalGroups", t => t.Group_IdGroup, cascadeDelete: true)
                .Index(t => t.IdLeaving)
                .Index(t => t.Group_IdGroup);
            
            CreateTable(
                "dbo.OperationalGroups",
                c => new
                    {
                        IdGroup = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.IdGroup);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        IdCar = c.Int(nullable: false, identity: true),
                        NumberCar = c.String(nullable: false),
                        IsPlaceFoDetainees = c.Boolean(nullable: false),
                        BrandCar = c.String(nullable: false),
                        ModelCar = c.String(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Color = c.String(nullable: false),
                        FuelType = c.String(nullable: false),
                        DateLastRefueling = c.DateTime(nullable: false),
                        SeatsCount = c.Int(nullable: false),
                        IsSpecSignal = c.Boolean(nullable: false),
                        DesiredCategoryDriving = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdCar);
            
            CreateTable(
                "dbo.WarDogs",
                c => new
                    {
                        IdDog = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Breed = c.String(nullable: false),
                        DateOfBirthday = c.DateTime(nullable: false),
                        DateCommencementOfService = c.DateTime(nullable: false),
                        DateLastInspection = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdDog)
                .ForeignKey("dbo.OperativeWorkers", t => t.IdDog)
                .Index(t => t.IdDog);
            
            CreateTable(
                "dbo.Protocols",
                c => new
                    {
                        NumberProtocol = c.Int(nullable: false),
                        DateOfPreparation = c.DateTime(nullable: false),
                        PlaceOfPreparation = c.String(nullable: false),
                        IsHaveDelayed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NumberProtocol)
                .ForeignKey("dbo.LeavingGroups", t => t.NumberProtocol)
                .Index(t => t.NumberProtocol);
            
            CreateTable(
                "dbo.Detentions",
                c => new
                    {
                        NumberDetention = c.Int(nullable: false, identity: true),
                        DetentionLastName = c.String(nullable: false),
                        DetentionFirstName = c.String(nullable: false),
                        DetentionSecondName = c.String(),
                        DateOfDetention = c.DateTime(nullable: false),
                        PleaceDetention = c.String(nullable: false),
                        BaseDetention = c.String(nullable: false),
                        Things = c.String(nullable: false),
                        IsNotifiRelatives = c.Boolean(nullable: false),
                        Protocol_NumberProtocol = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NumberDetention)
                .ForeignKey("dbo.Protocols", t => t.Protocol_NumberProtocol, cascadeDelete: true)
                .Index(t => t.Protocol_NumberProtocol);
            
            CreateTable(
                "dbo.EmployeeUsers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Rank_Id = c.Int(nullable: false),
                        EmployeeNumber = c.Int(nullable: false),
                        DateRegistr = c.DateTime(nullable: false),
                        DateLastEditedRank = c.DateTime(nullable: false),
                        WorkingPhone = c.String(nullable: false),
                        PenaltiesCount = c.Int(nullable: false),
                        NumberServiceCertificate = c.Int(nullable: false),
                        DateIssueServiceCertificate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.Ranks", t => t.Rank_Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.Rank_Id);
            
            CreateTable(
                "dbo.AdminUsers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DateOfLastEntry = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Dutys",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TypeDuty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.OperativeWorkers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Group_IdGroup = c.Int(),
                        IsHeadOfGroup = c.Boolean(nullable: false),
                        NumberServiceWeapon = c.Int(nullable: false),
                        AccessionNumberHandCuffs = c.Int(nullable: false),
                        IsHaveDog = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeUsers", t => t.Id)
                .ForeignKey("dbo.OperationalGroups", t => t.Group_IdGroup)
                .Index(t => t.Id)
                .Index(t => t.Group_IdGroup);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        WorkingCar_IdCar = c.Int(),
                        Group_IdGroup = c.Int(),
                        LicenseNumber = c.Int(nullable: false),
                        DateReceiptLicense = c.DateTime(nullable: false),
                        CategoryLicense = c.String(nullable: false),
                        NumberMedicalCertificates = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeUsers", t => t.Id)
                .ForeignKey("dbo.Cars", t => t.WorkingCar_IdCar)
                .ForeignKey("dbo.OperationalGroups", t => t.Group_IdGroup)
                .Index(t => t.Id)
                .Index(t => t.WorkingCar_IdCar)
                .Index(t => t.Group_IdGroup);
            
            CreateTable(
                "dbo.InternetStatements",
                c => new
                    {
                        NumberStatement = c.Int(nullable: false),
                        IpAdress = c.String(nullable: false),
                        InfoBrowser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NumberStatement)
                .ForeignKey("dbo.Statements", t => t.NumberStatement)
                .Index(t => t.NumberStatement);
            
            CreateTable(
                "dbo.OralStatements",
                c => new
                    {
                        NumberStatement = c.Int(nullable: false),
                        PhoneCaller = c.String(nullable: false),
                        AdressCaller = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NumberStatement)
                .ForeignKey("dbo.Statements", t => t.NumberStatement)
                .Index(t => t.NumberStatement);
            
            CreateTable(
                "dbo.WrittenStatements",
                c => new
                    {
                        NumberStatement = c.Int(nullable: false),
                        DeclarantNumberPasport = c.Int(nullable: false),
                        DeclarantWhoGivePassport = c.String(nullable: false),
                        DeclarantSubdivisionPasport = c.Int(nullable: false),
                        DeclarantDateGivePassport = c.DateTime(nullable: false),
                        DeclarantRegistrationAddress = c.String(nullable: false),
                        DeclarantContactPhone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NumberStatement)
                .ForeignKey("dbo.Statements", t => t.NumberStatement)
                .Index(t => t.NumberStatement);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.WrittenStatements", new[] { "NumberStatement" });
            DropIndex("dbo.OralStatements", new[] { "NumberStatement" });
            DropIndex("dbo.InternetStatements", new[] { "NumberStatement" });
            DropIndex("dbo.Drivers", new[] { "Group_IdGroup" });
            DropIndex("dbo.Drivers", new[] { "WorkingCar_IdCar" });
            DropIndex("dbo.Drivers", new[] { "Id" });
            DropIndex("dbo.OperativeWorkers", new[] { "Group_IdGroup" });
            DropIndex("dbo.OperativeWorkers", new[] { "Id" });
            DropIndex("dbo.Dutys", new[] { "Id" });
            DropIndex("dbo.AdminUsers", new[] { "Id" });
            DropIndex("dbo.EmployeeUsers", new[] { "Rank_Id" });
            DropIndex("dbo.EmployeeUsers", new[] { "Id" });
            DropIndex("dbo.Detentions", new[] { "Protocol_NumberProtocol" });
            DropIndex("dbo.Protocols", new[] { "NumberProtocol" });
            DropIndex("dbo.WarDogs", new[] { "IdDog" });
            DropIndex("dbo.LeavingGroups", new[] { "Group_IdGroup" });
            DropIndex("dbo.LeavingGroups", new[] { "IdLeaving" });
            DropIndex("dbo.Statements", new[] { "Duty_Id" });
            DropIndex("dbo.Merits", new[] { "Employee_Id" });
            DropForeignKey("dbo.WrittenStatements", "NumberStatement", "dbo.Statements");
            DropForeignKey("dbo.OralStatements", "NumberStatement", "dbo.Statements");
            DropForeignKey("dbo.InternetStatements", "NumberStatement", "dbo.Statements");
            DropForeignKey("dbo.Drivers", "Group_IdGroup", "dbo.OperationalGroups");
            DropForeignKey("dbo.Drivers", "WorkingCar_IdCar", "dbo.Cars");
            DropForeignKey("dbo.Drivers", "Id", "dbo.EmployeeUsers");
            DropForeignKey("dbo.OperativeWorkers", "Group_IdGroup", "dbo.OperationalGroups");
            DropForeignKey("dbo.OperativeWorkers", "Id", "dbo.EmployeeUsers");
            DropForeignKey("dbo.Dutys", "Id", "dbo.EmployeeUsers");
            DropForeignKey("dbo.AdminUsers", "Id", "dbo.Users");
            DropForeignKey("dbo.EmployeeUsers", "Rank_Id", "dbo.Ranks");
            DropForeignKey("dbo.EmployeeUsers", "Id", "dbo.Users");
            DropForeignKey("dbo.Detentions", "Protocol_NumberProtocol", "dbo.Protocols");
            DropForeignKey("dbo.Protocols", "NumberProtocol", "dbo.LeavingGroups");
            DropForeignKey("dbo.WarDogs", "IdDog", "dbo.OperativeWorkers");
            DropForeignKey("dbo.LeavingGroups", "Group_IdGroup", "dbo.OperationalGroups");
            DropForeignKey("dbo.LeavingGroups", "IdLeaving", "dbo.Statements");
            DropForeignKey("dbo.Statements", "Duty_Id", "dbo.Dutys");
            DropForeignKey("dbo.Merits", "Employee_Id", "dbo.EmployeeUsers");
            DropTable("dbo.WrittenStatements");
            DropTable("dbo.OralStatements");
            DropTable("dbo.InternetStatements");
            DropTable("dbo.Drivers");
            DropTable("dbo.OperativeWorkers");
            DropTable("dbo.Dutys");
            DropTable("dbo.AdminUsers");
            DropTable("dbo.EmployeeUsers");
            DropTable("dbo.Detentions");
            DropTable("dbo.Protocols");
            DropTable("dbo.WarDogs");
            DropTable("dbo.Cars");
            DropTable("dbo.OperationalGroups");
            DropTable("dbo.LeavingGroups");
            DropTable("dbo.Statements");
            DropTable("dbo.Merits");
            DropTable("dbo.Ranks");
            DropTable("dbo.Users");
        }
    }
}
