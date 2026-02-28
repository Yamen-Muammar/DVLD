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
        public static bool UpdateApplicationType(ApplicationType applicationType)
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
                    connection.Open();

                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"***Error UpdateApplicationType  :{ex} ***");
                    return false;
                }
            }
            return (rowsAffected > 0);
        }

        public static List<ApplicationType> GetAllApplicationTypes()
        {

            List<ApplicationType> types = new List<ApplicationType>();
            string query = "SELECT * FROM ApplicationTypes ORDER BY ApplicationTypeID ASC";

            // 2. Setup the Connection and Command
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
                            types.Add(new ApplicationType
                            {
                                ApplicationTypeID = (int)reader["ApplicationTypeID"],
                                ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString(),
                                ApplicationTypeFees = (decimal)reader["ApplicationTypeFees"]
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"***Error GetAllApplicationTypes  :{ex} ***");
                }
            }
            return types;
        }

        public static ApplicationType GetApplicationTypeByID(int applicationTypeID)
        {
            ApplicationType applicationType = null;
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ID";
            using (SqlConnection connection = new SqlConnection(DataBaseSettings.DataBaseConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID", applicationTypeID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
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
                catch (Exception ex)
                {
                    Debug.WriteLine($"***Error GetApplicationTypeByID  :{ex} ***");
                }
            }
            return applicationType;
        }
    }
}
