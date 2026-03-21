using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;

namespace DVLD__Data_Tier.Repositories
{
    public class ApplicationsTypesRepository
    {
        public async Task<bool> UpdateApplicationType(ApplicationType applicationType)
        {
            int rowsAffected = 0;

            string query = @"UPDATE ApplicationTypes  
                         SET
                             ApplicationTypeFees = @Fees
                         WHERE ApplicationTypeID = @ID";

            using (SqlConnection connection = new SqlConnection(DataBaseSettings.DataBaseConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@ID", applicationType.ApplicationTypeID);
                command.Parameters.AddWithValue("@Title", applicationType.ApplicationTypeTitle);
                command.Parameters.AddWithValue("@Fees", applicationType.ApplicationTypeFees);

                try
                {
                    await connection.OpenAsync();

                    rowsAffected =await command.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return (rowsAffected > 0);
        }

        public async Task<List<ApplicationType>> GetAllApplicationTypes()
        {

            List<ApplicationType> types = new List<ApplicationType>();
            string query = "SELECT * FROM ApplicationTypes ORDER BY ApplicationTypeID ASC";

            // 2. Setup the Connection and Command
            using (SqlConnection connection = new SqlConnection(DataBaseSettings.DataBaseConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await connection.OpenAsync();
                  
                    using (SqlDataReader reader =await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            types.Add(new ApplicationType
                            {
                                ApplicationTypeID = (int)reader["ApplicationTypeID"],
                                ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString(),
                                ApplicationTypeFees = (decimal)reader["ApplicationTypeFees"]
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return types;
        }

        public async Task<ApplicationType> GetApplicationTypeByID(int applicationTypeID)
        {
            ApplicationType applicationType = null;
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ID";
            using (SqlConnection connection = new SqlConnection(DataBaseSettings.DataBaseConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID", applicationTypeID);
                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader =await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            applicationType = new ApplicationType
                            {
                                ApplicationTypeID = (int)reader["ApplicationTypeID"],
                                ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString(),
                                ApplicationTypeFees = (decimal)reader["ApplicationTypeFees"]
                            };
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return applicationType;
        }
    }
}
