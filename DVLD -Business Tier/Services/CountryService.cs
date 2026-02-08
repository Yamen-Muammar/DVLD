using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Business_Tier.Services
{
    public static class CountryService
    {
        public static List<string> GetAllCountries()
        {
            // # This method should return a list of all countries. You can use a static list, read from a file, or fetch from an API.
            // For simplicity, let's return a static list of countries.
            return new List<string>
            {
                "United States",
                "Canada",
                "United Kingdom",
                "Australia",
                "Germany",
                "France",
                "India",
                "China",
                "Japan",
                "Brazil"
            };
        }

    }
}
