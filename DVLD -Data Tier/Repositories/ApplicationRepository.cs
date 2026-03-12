using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Core.View_Models;


namespace DVLD__Data_Tier.Repositories
{
    public class ApplicationRepository
    {
        private static string _connectionString = DataBaseSettings.DataBaseConnectionString;
        // ==========================================
        // 1. CREATE (Insert)
        // ==========================================

        public static int AddNewLocalDrivingLicesneApplication(Application newApplication, int licenseClassID)
        {            
            int newLocalAppID = -1;
            int newBaseAppID = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                
                connection.Open();

                // 2. Begin the transaction
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {                        
                        string query1 = @"INSERT INTO Applications 
                                     (CreatedByUser_ID, ApplicationType_ID, Person_ID, ApplicationDate, PaidFees, LastStatusDate, ApplicationStatus)
                                     VALUES 
                                     (@CreatedByUser_ID, @ApplicationType_ID, @Person_ID, @ApplicationDate, @PaidFees, @LastStatusDate, @ApplicationStatus);
                                     SELECT SCOPE_IDENTITY();";
                     
                        using (SqlCommand command = new SqlCommand(query1, connection, transaction))
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
                            
                            object applicationReturnedID = command.ExecuteScalar();
                            newBaseAppID = Convert.ToInt32(applicationReturnedID);


                            string query2 = @"INSERT INTO LocalDrivingLicenseApplications 
                                         (Application_ID, LicenseClass_ID)
                                         VALUES 
                                         (@ApplicationID, @LicenseClassID);
                                         SELECT SCOPE_IDENTITY();";

                            
                            command.CommandText = query2;
                            command.Parameters.Clear();

                            

                            command.Parameters.AddWithValue("@ApplicationID", newBaseAppID);
                            command.Parameters.AddWithValue("@LicenseClassID", licenseClassID);
                            
                            object LDLApplicationReturnedID = command.ExecuteScalar();

                            
                            newLocalAppID = Convert.ToInt32(LDLApplicationReturnedID);
                        }
                      
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {                        
                        transaction.Rollback();                        
                        return -1; 
                    }
                }
            }

            return newBaseAppID;
        }
        public static int AddNewApplication(DVLD__Core.Models.Application newApplication)
        {
            
            int newApplicationID = -1;

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
                }
            }
            return newApplicationID;
        }
               
        // ==========================================
        // 2. READ (Get By ID)
        // ==========================================
        public static DVLD__Core.Models.Application GetApplicationByID(int appID)
        {
            throw new NotImplementedException();
        }
        public static DVLD__Core.Models.Application GetApplicationByLDL_ID(int localDrivingLicenseApplicationID)
        {
            Application application = null;
            string query = "SELECT ApplicationID,CreatedByUser_ID,ApplicationType_ID,Person_ID,ApplicationDate,PaidFees,LastStatusDate,ApplicationStatus" +
                " FROM Applications " +
                "inner join LocalDrivingLicenseApplications" +
                " on Applications.ApplicationID = LocalDrivingLicenseApplications.Application_ID " +
                "where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLApplicationId;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LDLApplicationId", localDrivingLicenseApplicationID);

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
                                
                            };
                            if (reader["LastStatusDate"] == DBNull.Value)
                            {
                                application.LastStatusDate =  null;
                            }
                            else
                            {
                                application.LastStatusDate = (DateTime)reader["LastStatusDate"];
                            }
                            
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return application;
        }
        
        public static int doesHasAnActiveLocalDrivingLicenseApplication(int personID,int licenseClassID)
        {
            int foundApplicationID = -1;
            string query = "select Applications.ApplicationID from Applications " +
                "inner join LocalDrivingLicenseApplications" +
                " on Applications.ApplicationID = LocalDrivingLicenseApplications.Application_ID " +
                "where ApplicationStatus in('New','Completed') and Applications.Person_ID = @person_ID and LocalDrivingLicenseApplications.LicenseClass_ID = @licenseClassID;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@person_ID", personID);
                command.Parameters.AddWithValue("@licenseClassID", licenseClassID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            foundApplicationID = reader.GetInt32(0);
                        }
                    }
                }
                catch (Exception ex)
                {                      
                }
            }
            return foundApplicationID;
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
                    throw;
                }
            }
            return appsList;
        }
        public static List<clsLocalDrivingLicesnseApplicationView> GetAll_L_D_L_Applications()
        {
            List<clsLocalDrivingLicesnseApplicationView> appsList = new List<clsLocalDrivingLicesnseApplicationView>();
            string query = "SELECT LocalDrivingLicenseApplicationID,ClassName,NationalNO,FullName,ApplicationDate,ApplicationStatus" +
                " FROM LocalDrivingLicenseApplicationsView ORDER BY ApplicationDate DESC";

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
                            appsList.Add(new clsLocalDrivingLicesnseApplicationView
                            {
                                LDLApplicationID = (int)reader["LocalDrivingLicenseApplicationID"],
                                DrivingClassTitle = reader["ClassName"].ToString(),
                                NationalNO = reader["NationalNO"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                ApplicationDate = (DateTime)reader["ApplicationDate"],
                                Status = reader["ApplicationStatus"].ToString()
                            });                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
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
                
                command.Parameters.AddWithValue("@ApplicationID", app.ApplicationID);
                command.Parameters.AddWithValue("@CreatedByUser_ID", app.CreatedByUser_ID);
                command.Parameters.AddWithValue("@ApplicationType_ID", app.ApplicationType_ID);
                command.Parameters.AddWithValue("@Person_ID", app.Person_ID);
                command.Parameters.AddWithValue("@ApplicationDate", app.ApplicationDate);
                command.Parameters.AddWithValue("@PaidFees", app.PaidFees);
                if (app.LastStatusDate == null)
                {
                    command.Parameters.AddWithValue("@LastStatusDate",DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@LastStatusDate", app.LastStatusDate);
                }
              
                command.Parameters.AddWithValue("@ApplicationStatus", app.ApplicationStatus);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
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
                    throw;
                }
            }
            return (rowsAffected > 0);
        }
    }
}
