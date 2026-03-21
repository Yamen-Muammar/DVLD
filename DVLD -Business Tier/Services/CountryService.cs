using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class CountryService
    {
        private CountryRepository _repo;

        public CountryService()
        {
            _repo = new CountryRepository();
        }
        public async Task<List<Country>> GetAllCountries()
        {           
            return await _repo.GetCountries();
        }
        public  async Task<Country> GetCountry(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Just Valid IDs");
            }
            return await _repo.GetCountryByID(id);
        }
        public async Task<Country> GetCountry(string countryName)
        {
            if (string.IsNullOrEmpty(countryName))
            {
                throw new ArgumentException();
            }
            return await _repo.GetCountryByName(countryName);        
        }

    }
}
