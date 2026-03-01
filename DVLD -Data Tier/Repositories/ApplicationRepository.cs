using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;

namespace DVLD__Data_Tier.Repositories
{
    public class ApplicationRepository
    {
        private static string _connectionString = DataBaseSettings.DataBaseConnectionString;
        // ==========================================
        // 1. CREATE (Insert)
        // ==========================================
        public static int AddNewApplication(Application newApplication)
        {
            // We return an int so we can send the newly created ApplicationID back to the UI
            int newApplicationID = -1;

            // 💡 Mentor Tip: Notice 'SCOPE_IDENTITY()'. This tells SQL to immediately 
            // give us back the new ID it just created for this row!
            string query = @"INSERT INTO Applications 
                         (CreatedByUser_ID, ApplicationType_ID, Person_ID, ApplicationDate, PaidFees, LastStatusDate, ApplicationStatus)
                         VALUES 
                         (@CreatedByUser_ID, @ApplicationType_ID, @Person_ID, @ApplicationDate, @PaidFees, @LastStatusDate, @ApplicationStatus);
                         SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CreatedByUser_ID", newApplication.CreatedByUser_ID);
                command.Parameters.AddWithValue("@ApplicationType_ID", newApplication.ApplicationType_ID);
                command.Parameters.AddWithValue("@Person_ID", newApplication.Person_ID);
                command.Parameters.AddWithValue("@ApplicationDate", newApplication.ApplicationDate);
                command.Parameters.AddWithValue("@PaidFees", newApplication.PaidFees);
                if (newApplication.LastStatusDate == null)
                {
                    command.Parameters.AddWithValue("@LastStatusDate", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@LastStatusDate", newApplication.LastStatusDate);
                }
                
                command.Parameters.AddWithValue("@ApplicationStatus", newApplication.ApplicationStatus);

                try
                {
                    connection.Open();                    
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        newApplicationID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return newApplicationID;
        }

        // ==========================================
        // 2. READ (Get By ID)
        // ==========================================
        public static Application GetApplicationByID(int applicationID)
        {
            Application application = null;
            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", applicationID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            application = new Application
                            {
                                ApplicationID = (int)reader["ApplicationID"],
                                CreatedByUser_ID = (int)reader["CreatedByUser_ID"],
                                ApplicationType_ID = (int)reader["ApplicationType_ID"],
                                Person_ID = (int)reader["Person_ID"],
                                ApplicationDate = (DateTime)reader["ApplicationDate"],
                                PaidFees = (decimal)reader["PaidFees"],
                                ApplicationStatus = (string)reader["ApplicationStatus"],
                                LastStatusDate = (DateTime)reader["LastStatusDate"]

                            };                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return application;
        }

        // ==========================================
        // 3. READ ALL (Get All as List)
        // ==========================================
        public static List<Application> GetAllApplications()
        {
            List<Application> appsList = new List<Application>();
            string query = "SELECT * FROM Applications ORDER BY ApplicationDate DESC"; // Newest first!

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appsList.Add(new Application
                            {
                                ApplicationID = (int)reader["ApplicationID"],
                                CreatedByUser_ID = (int)reader["CreatedByUser_ID"],
                                ApplicationType_ID = (int)reader["ApplicationType_ID"],
                                Person_ID = (int)reader["Person_ID"],
                                ApplicationDate = (DateTime)reader["ApplicationDate"],
                                PaidFees = (decimal)reader["PaidFees"],
                                LastStatusDate = (DateTime)reader["LastStatusDate"],
                                ApplicationStatus = (string)(reader["ApplicationStatus"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return appsList;
        }

        // ==========================================
        // 4. UPDATE
        // ==========================================
        public static bool UpdateApplication(Application app)
        {
            int rowsAffected = 0;

            // Notice we do NOT update the ApplicationID, we just use it in the WHERE clause
            string query = @"UPDATE Applications  
                         SET CreatedByUser_ID = @CreatedByUser_ID, 
                             ApplicationType_ID = @ApplicationType_ID,
                             Person_ID = @Person_ID,
                             ApplicationDate = @ApplicationDate,
                             PaidFees = @PaidFees,
                             LastStatusDate = @LastStatusDate,
                             ApplicationStatus = @ApplicationStatus
                         WHERE ApplicationID = @ApplicationID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // We pass the entire object in, so we just read its properties here
                command.Parameters.AddWithValue("@ApplicationID", app.ApplicationID);
                command.Parameters.AddWithValue("@CreatedByUser_ID", app.CreatedByUser_ID);
                command.Parameters.AddWithValue("@ApplicationType_ID", app.ApplicationType_ID);
                command.Parameters.AddWithValue("@Person_ID", app.Person_ID);
                command.Parameters.AddWithValue("@ApplicationDate", app.ApplicationDate);
                command.Parameters.AddWithValue("@PaidFees", app.PaidFees);
                command.Parameters.AddWithValue("@LastStatusDate", app.LastStatusDate);
                command.Parameters.AddWithValue("@ApplicationStatus", app.ApplicationStatus);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
            return (rowsAffected > 0);
        }

        // ==========================================
        // 5. DELETE
        // ==========================================
        public static bool DeleteApplication(int applicationID)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", applicationID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return (rowsAffected > 0);
        }
    }
}
