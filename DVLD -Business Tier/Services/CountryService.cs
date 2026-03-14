using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public static class CountryService
    {
        public static List<Country> GetAllCountries()
        {           
            return CountryRepository.GetCountries();
        }

        public static Country GetCountry(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Just Valid IDs");
            }
            return CountryRepository.GetCountryByID(id);
        }

        public static Country GetCountry(string countryName)
        {
            if (string.IsNullOrEmpty(countryName))
            {
                throw new ArgumentException();
            }
            return CountryRepository.GetCountryByName(countryName);        
        }

    }
}
