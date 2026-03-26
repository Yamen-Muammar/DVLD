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
        private readonly string _connectionString = DataBaseSettings.DataBaseConnectionString; 
        public  async Task<List<LicenseClass>> GetAllLicenseClasses()
        {         
            List<LicenseClass> classesList = new List<LicenseClass>();
          
            string query = "SELECT * FROM LicenseClasses ORDER BY LicenseClassID ASC";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await connection.OpenAsync();

                    using (SqlDataReader reader =await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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
                catch (Exception)
                {
                    throw;
                }
            }           
            return classesList;
        }         

        public async Task<LicenseClass> GetLicenseClassByLDLApp_IdAsync(int ldlAppid)
        {
            LicenseClass licenseClass = null;
            string query = @"select LicenseClasses.LicenseClassID ,LicenseClasses.ClassName,LicenseClasses.ClassDescription,
                            LicenseClasses.MinimumAllowedAge,LicenseClasses.DefaultValidityLength,
                            LicenseClasses.ClassFees
                            from LocalDrivingLicenseApplications 
                            inner join LicenseClasses on LocalDrivingLicenseApplications.LicenseClass_ID = LicenseClasses.LicenseClassID
                            where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @ldlAppID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ldlAppID", ldlAppid);

                try
                {
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            licenseClass = new LicenseClass
                            {
                                LicenseClassID = (int)reader["LicenseClassID"],
                                ClassName = reader["ClassName"].ToString(),
                                ClassDescription = reader["ClassDescription"].ToString(),
                                MinimumAllowedAge = (int)reader["MinimumAllowedAge"],
                                DefaultValidityLength = (int)reader["DefaultValidityLength"],
                                ClassFees = (decimal)reader["ClassFees"],
                            };
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                return licenseClass;
            }
        }
    }
}
