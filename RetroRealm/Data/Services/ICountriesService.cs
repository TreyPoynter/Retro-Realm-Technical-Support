using RetroRealm.Models;

namespace RetroRealm.Data.Services
{
    public interface ICountriesService
    {
        IQueryable<CountryModel> GetCountries();
    }
}
