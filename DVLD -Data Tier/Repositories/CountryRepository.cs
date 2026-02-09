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
        public static Country GetCountryByID(int CountryId)
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
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foundCountry = new Country
                                {
                                    CountryID = Convert.ToInt32(reader["CountryID"]),
                                    CountryName = reader["CountryName"].ToString()
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN GetCountryByID :" + ex.ToString() + " ***");
                    }
                }
            }
            return foundCountry;
        }
        public static List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Countries;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                countries.Add(new Country
                                {
                                    CountryID = Convert.ToInt32(reader["CountryID"]),
                                    CountryName = reader["CountryName"].ToString()
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN GetCountries :" + ex.ToString() + " ***");
                    }
                }
            }
            return countries;
        }

        public static Country GetCountryByName(string countryName)
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
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foundCountry = new Country
                                {
                                    CountryID = Convert.ToInt32(reader["CountryID"]),
                                    CountryName = reader["CountryName"].ToString()
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN GetCountryByID :" + ex.ToString() + " ***");
                    }
                }
            }
            return foundCountry;
        }
    }
}

