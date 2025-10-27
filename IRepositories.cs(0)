using MasterPassport.Domain.Entities;

namespace MasterPassport.Domain.Interfaces;

public interface ITempUserRepository : IGenericRepository<TempUser>
{
    Task<TempUser?> GetByEmailAsync(string email);
    Task<TempUser?> GetByIdWithDetailsAsync(int id);
    Task<IEnumerable<TempUser>> GetPendingApprovalAsync();
}

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdWithDetailsAsync(int id);
    Task<User?> GetByUsernameAsync(string username);
}

public interface ITempUserCredentialRepository : IGenericRepository<TempUserCredential>
{
    Task<TempUserCredential?> GetByUsernameAsync(string username);
}

public interface IUserCredentialRepository : IGenericRepository<UserCredential>
{
    Task<UserCredential?> GetByUsernameAsync(string username);
    Task<UserCredential?> GetByUserIdAsync(int userId);
}

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<IEnumerable<Country>> GetActiveCountriesAsync();
}

public interface IGovernorateRepository : IGenericRepository<Governorate>
{
    Task<IEnumerable<Governorate>> GetByCountryIdAsync(int countryId);
}

public interface ICityRepository : IGenericRepository<City>
{
    Task<IEnumerable<City>> GetByGovernorateIdAsync(int governorateId);
    Task<IEnumerable<City>> GetActiveCitiesAsync();
}

public interface IUniversityRepository : IGenericRepository<University>
{
    Task<IEnumerable<University>> GetActiveUniversitiesAsync();
}

public interface ICollegeRepository : IGenericRepository<College>
{
    Task<IEnumerable<College>> GetByUniversityIdAsync(int universityId);
    Task<IEnumerable<College>> GetActiveCollegesAsync();
}

public interface IExperienceCategoryRepository : IGenericRepository<ExperienceCategory>
{
    Task<IEnumerable<ExperienceCategory>> GetRootCategoriesAsync();
    Task<IEnumerable<ExperienceCategory>> GetChildCategoriesAsync(int parentId);
}

public interface IExperienceFieldRepository : IGenericRepository<ExperienceField>
{
    Task<IEnumerable<ExperienceField>> GetByCategoryIdAsync(int categoryId);
}

public interface IQualificationMajorRepository : IGenericRepository<QualificationMajor>
{
    Task<IEnumerable<QualificationMajor>> GetActiveQualificationMajorsAsync();
}

public interface IQualificationMinorRepository : IGenericRepository<QualificationMinor>
{
    Task<IEnumerable<QualificationMinor>> GetByMajorIdAsync(int majorId);
}

public interface IUnitOfWork : IDisposable
{
    ITempUserRepository TempUsers { get; }
    IUserRepository Users { get; }
    ITempUserCredentialRepository TempUserCredentials { get; }
    IUserCredentialRepository UserCredentials { get; }
    ICountryRepository Countries { get; }
    IGovernorateRepository Governorates { get; }
    ICityRepository Cities { get; }
    IUniversityRepository Universities { get; }
    ICollegeRepository Colleges { get; }
    IExperienceCategoryRepository ExperienceCategories { get; }
    IExperienceFieldRepository ExperienceFields { get; }
    IQualificationMajorRepository QualificationMajors { get; }
    IQualificationMinorRepository QualificationMinors { get; }
    
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
