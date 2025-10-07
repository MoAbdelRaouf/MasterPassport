using Microsoft.EntityFrameworkCore.Storage;
using MasterPassport.Domain.Interfaces;
using MasterPassport.Infrastructure.Persistence.Contexts;

namespace MasterPassport.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        
        TempUsers = new TempUserRepository(_context);
        Users = new UserRepository(_context);
        TempUserCredentials = new TempUserCredentialRepository(_context);
        UserCredentials = new UserCredentialRepository(_context);
        Countries = new CountryRepository(_context);
        Governorates = new GovernorateRepository(_context);
        Cities = new CityRepository(_context);
        Universities = new UniversityRepository(_context);
        Colleges = new CollegeRepository(_context);
        ExperienceCategories = new ExperienceCategoryRepository(_context);
        ExperienceFields = new ExperienceFieldRepository(_context);
        QualificationMajors = new QualificationMajorRepository(_context);
        QualificationMinors = new QualificationMinorRepository(_context);
    }

    public ITempUserRepository TempUsers { get; }
    public IUserRepository Users { get; }
    public ITempUserCredentialRepository TempUserCredentials { get; }
    public IUserCredentialRepository UserCredentials { get; }
    public ICountryRepository Countries { get; }
    public IGovernorateRepository Governorates { get; }
    public ICityRepository Cities { get; }
    public IUniversityRepository Universities { get; }
    public ICollegeRepository Colleges { get; }
    public IExperienceCategoryRepository ExperienceCategories { get; }
    public IExperienceFieldRepository ExperienceFields { get; }
    public IQualificationMajorRepository QualificationMajors { get; }
    public IQualificationMinorRepository QualificationMinors { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
