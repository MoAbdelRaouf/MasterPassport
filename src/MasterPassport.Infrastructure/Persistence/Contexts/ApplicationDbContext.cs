using Microsoft.EntityFrameworkCore;
using MasterPassport.Domain.Entities;

namespace MasterPassport.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Temporary Tables
    public DbSet<TempUser> TempUsers { get; set; }
    public DbSet<TempWorkPlace> TempWorkPlaces { get; set; }
    public DbSet<TempScientificDegree> TempScientificDegrees { get; set; }
    public DbSet<TempUserExperienceField> TempUserExperienceFields { get; set; }
    public DbSet<TempUserLanguage> TempUserLanguages { get; set; }
    public DbSet<TempUserAttachment> TempUserAttachments { get; set; }
    public DbSet<TempUserCredential> TempUserCredentials { get; set; }

    // Permanent Tables
    public DbSet<User> Users { get; set; }
    public DbSet<WorkPlace> WorkPlaces { get; set; }
    public DbSet<ScientificDegree> ScientificDegrees { get; set; }
    public DbSet<UserExperienceField> UserExperienceFields { get; set; }
    public DbSet<UserLanguage> UserLanguages { get; set; }
    public DbSet<UserAttachment> UserAttachments { get; set; }
    public DbSet<UserCredential> UserCredentials { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<PasswordHistory> PasswordHistories { get; set; }
    public DbSet<LoginRequest> LoginRequests { get; set; }

    // Lookup Tables
    public DbSet<Country> Countries { get; set; }
    public DbSet<Governorate> Governorates { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<College> Colleges { get; set; }
    public DbSet<WorkPlaceType> WorkPlaceTypes { get; set; }
    public DbSet<QualificationType> QualificationTypes { get; set; }
    public DbSet<QualificationMajor> QualificationMajors { get; set; }
    public DbSet<QualificationMinor> QualificationMinors { get; set; }
    public DbSet<ExperienceCategory> ExperienceCategories { get; set; }
    public DbSet<ExperienceField> ExperienceFields { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<ProficiencyLevel> ProficiencyLevels { get; set; }
    public DbSet<AttachmentType> AttachmentTypes { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<InformationSystem> InformationSystems { get; set; }
    public DbSet<ModificationType> ModificationTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Configure TempUser
        modelBuilder.Entity<TempUser>(entity =>
        {
            entity.HasKey(e => e.TempUserID);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.FirstNameAr).IsRequired().HasMaxLength(50);
            entity.Property(e => e.SecondNameAr).IsRequired().HasMaxLength(50);
            entity.Property(e => e.ThirdNameAr).IsRequired().HasMaxLength(50);
            entity.Property(e => e.FourthNameAr).IsRequired().HasMaxLength(50);
            entity.Property(e => e.FullNameEn).IsRequired().HasMaxLength(200);
            entity.Property(e => e.MobileNumber).IsRequired().HasMaxLength(20);
            entity.Property(e => e.NationalID).HasMaxLength(14);
            entity.Property(e => e.PassportNumber).HasMaxLength(50);
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("GETDATE()");
            
            entity.HasOne(e => e.Nationality).WithMany().HasForeignKey(e => e.NationalityID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Title).WithMany().HasForeignKey(e => e.TitleID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Gender).WithMany().HasForeignKey(e => e.GenderID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.City).WithMany().HasForeignKey(e => e.CityID).OnDelete(DeleteBehavior.Restrict);
        });

        // Configure User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserID);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.FirstNameAr).IsRequired().HasMaxLength(50);
            entity.Property(e => e.SecondNameAr).IsRequired().HasMaxLength(50);
            entity.Property(e => e.ThirdNameAr).IsRequired().HasMaxLength(50);
            entity.Property(e => e.FourthNameAr).IsRequired().HasMaxLength(50);
            entity.Property(e => e.FullNameEn).IsRequired().HasMaxLength(200);
            entity.Property(e => e.MobileNumber).IsRequired().HasMaxLength(20);
            entity.Property(e => e.NationalID).HasMaxLength(14);
            entity.Property(e => e.PassportNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            
            entity.HasOne(e => e.Nationality).WithMany().HasForeignKey(e => e.NationalityID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Title).WithMany().HasForeignKey(e => e.TitleID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Gender).WithMany().HasForeignKey(e => e.GenderID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.City).WithMany().HasForeignKey(e => e.CityID).OnDelete(DeleteBehavior.Restrict);
        });

        // Configure TempWorkPlace
        modelBuilder.Entity<TempWorkPlace>(entity =>
        {
            entity.HasKey(e => e.TempWorkPlaceID);
            entity.HasOne(e => e.TempUser).WithMany(u => u.TempWorkPlaces).HasForeignKey(e => e.TempUserID).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.WorkPlaceType).WithMany().HasForeignKey(e => e.WorkPlaceTypeID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.City).WithMany().HasForeignKey(e => e.CityID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.University).WithMany().HasForeignKey(e => e.UniversityID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.College).WithMany().HasForeignKey(e => e.CollegeID).OnDelete(DeleteBehavior.Restrict);
        });

        // Configure WorkPlace
        modelBuilder.Entity<WorkPlace>(entity =>
        {
            entity.HasKey(e => e.WorkPlaceID);
            entity.HasOne(e => e.User).WithMany(u => u.WorkPlaces).HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.WorkPlaceType).WithMany().HasForeignKey(e => e.WorkPlaceTypeID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.City).WithMany().HasForeignKey(e => e.CityID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.University).WithMany().HasForeignKey(e => e.UniversityID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.College).WithMany().HasForeignKey(e => e.CollegeID).OnDelete(DeleteBehavior.Restrict);
        });

        // Configure TempScientificDegree
        modelBuilder.Entity<TempScientificDegree>(entity =>
        {
            entity.HasKey(e => e.TempScientificDegreeID);
            entity.HasOne(e => e.TempUser).WithMany(u => u.TempScientificDegrees).HasForeignKey(e => e.TempUserID).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.QualificationType).WithMany().HasForeignKey(e => e.QualificationTypeID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.University).WithMany().HasForeignKey(e => e.UniversityID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.College).WithMany().HasForeignKey(e => e.CollegeID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.QualificationMajor).WithMany().HasForeignKey(e => e.QualificationMajorID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.QualificationMinor).WithMany().HasForeignKey(e => e.QualificationMinorID).OnDelete(DeleteBehavior.Restrict);
        });

        // Configure ScientificDegree
        modelBuilder.Entity<ScientificDegree>(entity =>
        {
            entity.HasKey(e => e.ScientificDegreeID);
            entity.HasOne(e => e.User).WithMany(u => u.ScientificDegrees).HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.QualificationType).WithMany().HasForeignKey(e => e.QualificationTypeID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.University).WithMany().HasForeignKey(e => e.UniversityID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.College).WithMany().HasForeignKey(e => e.CollegeID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.QualificationMajor).WithMany().HasForeignKey(e => e.QualificationMajorID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.QualificationMinor).WithMany().HasForeignKey(e => e.QualificationMinorID).OnDelete(DeleteBehavior.Restrict);
        });

        // Configure TempUserExperienceField
        modelBuilder.Entity<TempUserExperienceField>(entity =>
        {
            entity.HasKey(e => e.TempUserExperienceFieldID);
            entity.HasOne(e => e.TempUser).WithMany(u => u.TempUserExperienceFields).HasForeignKey(e => e.TempUserID).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.ExperienceField).WithMany().HasForeignKey(e => e.ExperienceFieldID).OnDelete(DeleteBehavior.Restrict);
        });

        // Configure UserExperienceField
        modelBuilder.Entity<UserExperienceField>(entity =>
        {
            entity.HasKey(e => e.UserExperienceFieldID);
            entity.HasOne(e => e.User).WithMany(u => u.UserExperienceFields).HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.ExperienceField).WithMany().HasForeignKey(e => e.ExperienceFieldID).OnDelete(DeleteBehavior.Restrict);
        });

        // Configure TempUserLanguage
        modelBuilder.Entity<TempUserLanguage>(entity =>
        {
            entity.HasKey(e => e.TempUserLanguageID);
            entity.HasOne(e => e.TempUser).WithMany(u => u.TempUserLanguages).HasForeignKey(e => e.TempUserID).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Language).WithMany().HasForeignKey(e => e.LanguageID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.ProficiencyLevel).WithMany().HasForeignKey(e => e.ProficiencyLevelID).OnDelete(DeleteBehavior.Restrict);
        });

        // Configure UserLanguage
        modelBuilder.Entity<UserLanguage>(entity =>
        {
            entity.HasKey(e => e.UserLanguageID);
            entity.HasOne(e => e.User).WithMany(u => u.UserLanguages).HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Language).WithMany().HasForeignKey(e => e.LanguageID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.ProficiencyLevel).WithMany().HasForeignKey(e => e.ProficiencyLevelID).OnDelete(DeleteBehavior.Restrict);
        });

        // Configure TempUserAttachment
        modelBuilder.Entity<TempUserAttachment>(entity =>
        {
            entity.HasKey(e => e.TempUserAttachmentID);
            entity.HasOne(e => e.TempUser).WithMany(u => u.TempUserAttachments).HasForeignKey(e => e.TempUserID).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.AttachmentType).WithMany().HasForeignKey(e => e.AttachmentTypeID).OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.UploadedAt).HasDefaultValueSql("GETDATE()");
        });

        // Configure UserAttachment
        modelBuilder.Entity<UserAttachment>(entity =>
        {
            entity.HasKey(e => e.UserAttachmentID);
            entity.HasOne(e => e.User).WithMany(u => u.UserAttachments).HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.AttachmentType).WithMany().HasForeignKey(e => e.AttachmentTypeID).OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.UploadedAt).HasDefaultValueSql("GETDATE()");
        });

        // Configure TempUserCredential
        modelBuilder.Entity<TempUserCredential>(entity =>
        {
            entity.HasKey(e => e.TempUserCredentialID);
            entity.HasOne(e => e.TempUser).WithOne(u => u.TempUserCredential).HasForeignKey<TempUserCredential>(e => e.TempUserID).OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Username).IsUnique();
        });

        // Configure UserCredential
        modelBuilder.Entity<UserCredential>(entity =>
        {
            entity.HasKey(e => e.UserCredentialID);
            entity.HasOne(e => e.User).WithOne(u => u.UserCredential).HasForeignKey<UserCredential>(e => e.UserID).OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Username).IsUnique();
        });

        // Configure UserRole
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleID);
            entity.HasOne(e => e.User).WithMany(u => u.UserRoles).HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Role).WithMany(r => r.UserRoles).HasForeignKey(e => e.RoleID).OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.AssignedAt).HasDefaultValueSql("GETDATE()");
        });

        // Configure PasswordHistory
        modelBuilder.Entity<PasswordHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryID);
            entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.ChangedAt).HasDefaultValueSql("GETDATE()");
        });

        // Configure LoginRequest
        modelBuilder.Entity<LoginRequest>(entity =>
        {
            entity.HasKey(e => e.RequestID);
            entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.InformationSystem).WithMany().HasForeignKey(e => e.SystemID).OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.RequestDate).HasDefaultValueSql("GETDATE()");
        });

        // Configure Lookup Tables
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryID);
            entity.Property(e => e.CountryNameAr).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.CountryNameAr).IsUnique();
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<Governorate>(entity =>
        {
            entity.HasKey(e => e.GovernorateID);
            entity.Property(e => e.GovernorateNameAr).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Country).WithMany(c => c.Governorates).HasForeignKey(e => e.CountryID).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityID);
            entity.Property(e => e.CityNameAr).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Governorate).WithMany(g => g.Cities).HasForeignKey(e => e.GovernorateID).OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderID);
            entity.Property(e => e.GenderNameAr).IsRequired().HasMaxLength(10);
            entity.HasIndex(e => e.GenderNameAr).IsUnique();
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => e.TitleID);
            entity.Property(e => e.TitleNameAr).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.UniversityID);
            entity.Property(e => e.UniversityName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<College>(entity =>
        {
            entity.HasKey(e => e.CollegeID);
            entity.Property(e => e.CollegeName).IsRequired().HasMaxLength(100);
            entity.HasOne(e => e.University).WithMany(u => u.Colleges).HasForeignKey(e => e.UniversityID).OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<WorkPlaceType>(entity =>
        {
            entity.HasKey(e => e.WorkPlaceTypeID);
            entity.Property(e => e.TypeName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<QualificationType>(entity =>
        {
            entity.HasKey(e => e.QualificationTypeID);
            entity.Property(e => e.TypeName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<QualificationMajor>(entity =>
        {
            entity.HasKey(e => e.QualificationMajorID);
            entity.Property(e => e.MajorName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<QualificationMinor>(entity =>
        {
            entity.HasKey(e => e.QualificationMinorID);
            entity.Property(e => e.MinorName).IsRequired().HasMaxLength(100);
            entity.HasOne(e => e.QualificationMajor).WithMany(m => m.QualificationMinors).HasForeignKey(e => e.QualificationMajorID).OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<ExperienceCategory>(entity =>
        {
            entity.HasKey(e => e.ExperienceCatID);
            entity.Property(e => e.ExperienceCatName).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Parent).WithMany(c => c.Children).HasForeignKey(e => e.ParentID).OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<ExperienceField>(entity =>
        {
            entity.HasKey(e => e.ExperienceFieldID);
            entity.Property(e => e.ExperienceFieldName).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.ExperienceCategory).WithMany(c => c.ExperienceFields).HasForeignKey(e => e.ExperienceCatID).OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageID);
            entity.Property(e => e.LanguageName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<ProficiencyLevel>(entity =>
        {
            entity.HasKey(e => e.ProficiencyLevelID);
            entity.Property(e => e.ProficiencyLevelName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CDate).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<AttachmentType>(entity =>
        {
            entity.HasKey(e => e.AttachmentTypeID);
            entity.Property(e => e.TypeName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleID);
            entity.Property(e => e.RoleName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<InformationSystem>(entity =>
        {
            entity.HasKey(e => e.SystemID);
            entity.Property(e => e.SystemName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.SystemCode).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.SystemName).IsUnique();
            entity.HasIndex(e => e.SystemCode).IsUnique();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<ModificationType>(entity =>
        {
            entity.HasKey(e => e.ModificationTypeID);
            entity.Property(e => e.ModificationTypeName).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.ModificationTypeName).IsUnique();
        });
    }
}
