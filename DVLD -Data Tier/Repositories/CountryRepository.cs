using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;

namespace DVLD__Data_Tier.Repositories
{
    public class CountryRepository
    {
        private static string connectionString = DataBaseSettings.DataBaseConnectionString;
        public async Task<Country> GetCountryByID(int CountryId)
        {
            Country foundCountry = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Countries WHERE CountryID = @countryID;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@countryID", CountryId);

                    try
                    {
                        await conn.OpenAsync();
                        using (SqlDataReader reader =await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                foundCountry = new Country
                                {
                                    CountryID = Convert.ToInt32(reader["CountryID"]),
                                    CountryName = reader["CountryName"].ToString()
                                };
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return foundCountry;
        }
        public async Task<List<Country>> GetCountries()
        {
            List<Country> countries = new List<Country>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Countries;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        await conn.OpenAsync();
                        using (SqlDataReader reader =await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                countries.Add(new Country
                                {
                                    CountryID = Convert.ToInt32(reader["CountryID"]),
                                    CountryName = reader["CountryName"].ToString()
                                });
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return countries;
        }
        public async Task<Country> GetCountryByName(string countryName)
        {
            Country foundCountry = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Countries WHERE CountryName = @countryName;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@countryName", countryName);

                    try
                    {
                        await conn.OpenAsync();
                        using ( SqlDataReader reader =await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                foundCountry = new Country
                                {
                                    CountryID = Convert.ToInt32(reader["CountryID"]),
                                    CountryName = reader["CountryName"].ToString()
                                };
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return foundCountry;
        }
    }
}

