using RetroRealm.Models;

namespace RetroRealm.Data.Services
{
    public class CountriesService : ICountriesService
    {
        private ApplicationDbContext _context;
        public CountriesService(ApplicationDbContext context)
        {

            _context = context;

        }
        public IQueryable<CountryModel> GetCountries()
        {
            IQueryable<CountryModel> countries = _context.Countries.OrderBy(c => c.Country);
            return countries;
        }
    }
}
