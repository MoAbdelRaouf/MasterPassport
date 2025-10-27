using MasterPassport.Application.DTOs;

namespace MasterPassport.Application.Interfaces;

public interface ILookupService
{
    Task<List<LookupDto>> GetCountriesAsync();
    Task<List<LookupDto>> GetGovernoratesByCountryAsync(int countryId);
    Task<List<LookupDto>> GetCitiesByGovernorateAsync(int governorateId);
    Task<List<LookupDto>> GetGendersAsync();
    Task<List<LookupDto>> GetTitlesAsync();
    Task<List<LookupDto>> GetUniversitiesAsync();
    Task<List<LookupDto>> GetCollegesByUniversityAsync(int universityId);
    Task<List<LookupDto>> GetWorkPlaceTypesAsync();
    Task<List<LookupDto>> GetQualificationTypesAsync();
    Task<List<LookupDto>> GetQualificationMajorsAsync();
    Task<List<LookupDto>> GetQualificationMinorsByMajorAsync(int majorId);
    Task<List<LookupDto>> GetExperienceCategoriesAsync(int? parentId = null);
    Task<List<LookupDto>> GetExperienceFieldsByCategoryAsync(int categoryId);
    Task<List<LookupDto>> GetLanguagesAsync();
    Task<List<LookupDto>> GetProficiencyLevelsAsync();
    Task<List<LookupDto>> GetAttachmentTypesAsync();
}
