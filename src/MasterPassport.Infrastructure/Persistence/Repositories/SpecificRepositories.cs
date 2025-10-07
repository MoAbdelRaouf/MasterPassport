using Microsoft.EntityFrameworkCore;
using MasterPassport.Domain.Entities;
using MasterPassport.Domain.Interfaces;
using MasterPassport.Infrastructure.Persistence.Contexts;

namespace MasterPassport.Infrastructure.Persistence.Repositories;

public class TempUserRepository : GenericRepository<TempUser>, ITempUserRepository
{
    public TempUserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<TempUser?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<TempUser?> GetByIdWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(u => u.Nationality)
            .Include(u => u.Title)
            .Include(u => u.Gender)
            .Include(u => u.City).ThenInclude(c => c!.Governorate).ThenInclude(g => g!.Country)
            .Include(u => u.TempWorkPlaces).ThenInclude(w => w.WorkPlaceType)
            .Include(u => u.TempWorkPlaces).ThenInclude(w => w.City)
            .Include(u => u.TempWorkPlaces).ThenInclude(w => w.University)
            .Include(u => u.TempWorkPlaces).ThenInclude(w => w.College)
            .Include(u => u.TempScientificDegrees).ThenInclude(s => s.QualificationType)
            .Include(u => u.TempScientificDegrees).ThenInclude(s => s.University)
            .Include(u => u.TempScientificDegrees).ThenInclude(s => s.College)
            .Include(u => u.TempScientificDegrees).ThenInclude(s => s.QualificationMajor)
            .Include(u => u.TempScientificDegrees).ThenInclude(s => s.QualificationMinor)
            .Include(u => u.TempUserExperienceFields).ThenInclude(e => e.ExperienceField)
            .Include(u => u.TempUserLanguages).ThenInclude(l => l.Language)
            .Include(u => u.TempUserLanguages).ThenInclude(l => l.ProficiencyLevel)
            .Include(u => u.TempUserAttachments).ThenInclude(a => a.AttachmentType)
            .Include(u => u.TempUserCredential)
            .FirstOrDefaultAsync(u => u.TempUserID == id);
    }

    public async Task<IEnumerable<TempUser>> GetPendingApprovalAsync()
    {
        return await _dbSet
            .Where(u => !u.IsApproved)
            .OrderBy(u => u.RegistrationDate)
            .ToListAsync();
    }
}

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(u => u.Nationality)
            .Include(u => u.Title)
            .Include(u => u.Gender)
            .Include(u => u.City).ThenInclude(c => c!.Governorate).ThenInclude(g => g!.Country)
            .Include(u => u.WorkPlaces).ThenInclude(w => w.WorkPlaceType)
            .Include(u => u.WorkPlaces).ThenInclude(w => w.City)
            .Include(u => u.WorkPlaces).ThenInclude(w => w.University)
            .Include(u => u.WorkPlaces).ThenInclude(w => w.College)
            .Include(u => u.ScientificDegrees).ThenInclude(s => s.QualificationType)
            .Include(u => u.ScientificDegrees).ThenInclude(s => s.University)
            .Include(u => u.ScientificDegrees).ThenInclude(s => s.College)
            .Include(u => u.ScientificDegrees).ThenInclude(s => s.QualificationMajor)
            .Include(u => u.ScientificDegrees).ThenInclude(s => s.QualificationMinor)
            .Include(u => u.UserExperienceFields).ThenInclude(e => e.ExperienceField)
            .Include(u => u.UserLanguages).ThenInclude(l => l.Language)
            .Include(u => u.UserLanguages).ThenInclude(l => l.ProficiencyLevel)
            .Include(u => u.UserAttachments).ThenInclude(a => a.AttachmentType)
            .Include(u => u.UserCredential)
            .Include(u => u.UserRoles).ThenInclude(r => r.Role)
            .FirstOrDefaultAsync(u => u.UserID == id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbSet
            .Include(u => u.UserCredential)
            .FirstOrDefaultAsync(u => u.UserCredential!.Username == username);
    }
}

public class TempUserCredentialRepository : GenericRepository<TempUserCredential>, ITempUserCredentialRepository
{
    public TempUserCredentialRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<TempUserCredential?> GetByUsernameAsync(string username)
    {
        return await _dbSet
            .Include(c => c.TempUser)
            .FirstOrDefaultAsync(c => c.Username == username);
    }
}

public class UserCredentialRepository : GenericRepository<UserCredential>, IUserCredentialRepository
{
    public UserCredentialRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<UserCredential?> GetByUsernameAsync(string username)
    {
        return await _dbSet
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Username == username);
    }

    public async Task<UserCredential?> GetByUserIdAsync(int userId)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.UserID == userId);
    }
}

public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Country>> GetActiveCountriesAsync()
    {
        return await _dbSet.Where(c => c.IsActive).OrderBy(c => c.CountryNameAr).ToListAsync();
    }
}

public class GovernorateRepository : GenericRepository<Governorate>, IGovernorateRepository
{
    public GovernorateRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Governorate>> GetByCountryIdAsync(int countryId)
    {
        return await _dbSet.Where(g => g.CountryID == countryId).OrderBy(g => g.GovernorateNameAr).ToListAsync();
    }
}

public class CityRepository : GenericRepository<City>, ICityRepository
{
    public CityRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<City>> GetByGovernorateIdAsync(int governorateId)
    {
        return await _dbSet.Where(c => c.GovernorateID == governorateId && c.IsActive).OrderBy(c => c.CityNameAr).ToListAsync();
    }

    public async Task<IEnumerable<City>> GetActiveCitiesAsync()
    {
        return await _dbSet.Where(c => c.IsActive).OrderBy(c => c.CityNameAr).ToListAsync();
    }
}

public class UniversityRepository : GenericRepository<University>, IUniversityRepository
{
    public UniversityRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<University>> GetActiveUniversitiesAsync()
    {
        return await _dbSet.Where(u => u.IsActive).OrderBy(u => u.UniversityName).ToListAsync();
    }
}

public class CollegeRepository : GenericRepository<College>, ICollegeRepository
{
    public CollegeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<College>> GetByUniversityIdAsync(int universityId)
    {
        return await _dbSet.Where(c => c.UniversityID == universityId && c.IsActive).OrderBy(c => c.CollegeName).ToListAsync();
    }

    public async Task<IEnumerable<College>> GetActiveCollegesAsync()
    {
        return await _dbSet.Where(c => c.IsActive).OrderBy(c => c.CollegeName).ToListAsync();
    }
}

public class ExperienceCategoryRepository : GenericRepository<ExperienceCategory>, IExperienceCategoryRepository
{
    public ExperienceCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ExperienceCategory>> GetRootCategoriesAsync()
    {
        return await _dbSet.Where(c => c.ParentID == null && c.IsActive).OrderBy(c => c.ExperienceCatName).ToListAsync();
    }

    public async Task<IEnumerable<ExperienceCategory>> GetChildCategoriesAsync(int parentId)
    {
        return await _dbSet.Where(c => c.ParentID == parentId && c.IsActive).OrderBy(c => c.ExperienceCatName).ToListAsync();
    }
}

public class ExperienceFieldRepository : GenericRepository<ExperienceField>, IExperienceFieldRepository
{
    public ExperienceFieldRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ExperienceField>> GetByCategoryIdAsync(int categoryId)
    {
        return await _dbSet.Where(f => f.ExperienceCatID == categoryId && f.IsActive).OrderBy(f => f.ExperienceFieldName).ToListAsync();
    }
}

public class QualificationMajorRepository : GenericRepository<QualificationMajor>, IQualificationMajorRepository
{
    public QualificationMajorRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<QualificationMajor>> GetActiveQualificationMajorsAsync()
    {
        return await _dbSet.Where(m => m.IsActive).OrderBy(m => m.MajorName).ToListAsync();
    }
}

public class QualificationMinorRepository : GenericRepository<QualificationMinor>, IQualificationMinorRepository
{
    public QualificationMinorRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<QualificationMinor>> GetByMajorIdAsync(int majorId)
    {
        return await _dbSet.Where(m => m.QualificationMajorID == majorId && m.IsActive).OrderBy(m => m.MinorName).ToListAsync();
    }
}
