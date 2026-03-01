using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;

namespace DVLD__Data_Tier.Repositories
{
    public class LicenseClassRepository
    {
        public static List<LicenseClass> GetAllLicenseClasses()
        {         
            List<LicenseClass> classesList = new List<LicenseClass>();
          
            string query = "SELECT * FROM LicenseClasses ORDER BY LicenseClassID ASC";

            using (SqlConnection connection = new SqlConnection(DataBaseSettings.DataBaseConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            classesList.Add(new LicenseClass
                            {
                                LicenseClassID = (int)reader["LicenseClassID"],
                                ClassName = (string)reader["ClassName"],
                                ClassDescription = (string)reader["ClassDescription"],
                                MinimumAllowedAge = (int)reader["MinimumAllowedAge"],
                                DefaultValidityLength = (int)reader["DefaultValidityLength"],
                                ClassFees = (decimal)reader["ClassFees"]
                               
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }           
            return classesList;
        }
    }
}
