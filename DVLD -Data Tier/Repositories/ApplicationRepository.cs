using System;
using System.Collections.Generic;
using System.Data.Common;
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
        private string _connectionString = DataBaseSettings.DataBaseConnectionString;
        // ==========================================
        // 1. CREATE (Insert)
        // ==========================================

        public async Task<int> AddNewLocalDrivingLicesneApplicationAsync(Application newApplication, int licenseClassID)
        {            
            int newLocalAppID = -1;
            int newBaseAppID = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                
                await connection.OpenAsync();

                // 2. Begin the transaction
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                       
                        newBaseAppID = Convert.ToInt32(await InsertApplicationTransactional(connection, transaction, newApplication));
                        newLocalAppID = Convert.ToInt32(await _insertLDLAppliaction(newBaseAppID, licenseClassID, connection, transaction));
                    
                        transaction.Commit();
                    }
                    catch (Exception)
                    {                        
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return newBaseAppID;
        }
        public async Task<int> InsertApplicationTransactional(SqlConnection connection, SqlTransaction transaction, Application newApplication)
        {
            int newLocalAppID = -1;
            string query = @"INSERT INTO Applications 
                                     (CreatedByUser_ID, ApplicationType_ID, Person_ID, ApplicationDate, PaidFees, LastStatusDate, ApplicationStatus)
                                     VALUES 
                                     (@CreatedByUser_ID, @ApplicationType_ID, @Person_ID, @ApplicationDate, @PaidFees, @LastStatusDate, @ApplicationStatus);
                                     SELECT SCOPE_IDENTITY();";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
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

                object applicationReturnedID = await command.ExecuteScalarAsync();
                int.TryParse(applicationReturnedID.ToString(), out newLocalAppID);
            }
            return 
                newLocalAppID;
        }
        private async Task<int> _insertLDLAppliaction(int insertedApplicationID,int LicenseClassTypeID, SqlConnection connection, SqlTransaction transaction)
        {
            int newLocalAppID = -1;
            string query = @"INSERT INTO LocalDrivingLicenseApplications
                                         (Application_ID, LicenseClass_ID)
                                         VALUES 
                                         (@ApplicationID, @LicenseClassID);
                                         SELECT SCOPE_IDENTITY();";
            using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@ApplicationID", insertedApplicationID);
                cmd.Parameters.AddWithValue("@LicenseClassID",LicenseClassTypeID);

                object applicationReturnedID = await cmd.ExecuteScalarAsync();
                int.TryParse(applicationReturnedID.ToString(), out newLocalAppID);
            }
            return newLocalAppID;
        }
        public async Task<int> AddNewApplication(DVLD__Core.Models.Application newApplication)
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
                    await connection.OpenAsync();                    
                    object result = await command.ExecuteScalarAsync();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        newApplicationID = insertedID;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return newApplicationID;
        }

        // ==========================================
        // 2. READ (Get By ID)
        // ==========================================
        public async Task<DVLD__Core.Models.Application> GetApplicationByID(int appID)
        {
            throw new NotImplementedException();
        }
        public async Task<DVLD__Core.Models.Application> GetApplicationByLDL_ID(int localDrivingLicenseApplicationID)
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
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
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
                finally
                {
                    connection.Close();
                }
            }
            return application;
        }
        
        public async Task<DVLD__Core.Models.LocalDrivingLicenseApplication> GetLocalDrivingLicenseApplicationByIDAsync(int localDrivingLicenseApplicationID)
        {
            LocalDrivingLicenseApplication LDLApplication = null;
            string query = "SELECT LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, LocalDrivingLicenseApplications.Application_ID, LocalDrivingLicenseApplications.LicenseClass_ID" +
                " FROM LocalDrivingLicenseApplications " +
                "where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLApplicationId;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LDLApplicationId", localDrivingLicenseApplicationID);

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            LDLApplication = new LocalDrivingLicenseApplication
                            {
                                LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"],
                                Application_ID = (int)reader["ApplicationID"],
                                LicenseClass_ID = (int)reader["LicenseClass_ID"]
                            };
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                } 
            }
            return LDLApplication;
        }
        public async Task<int> doesHasAnNewOrCompletedLocalDrivingLicenseApplication(int personID,int licenseClassID)
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
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            foundApplicationID = reader.GetInt32(0);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return foundApplicationID;
        }
        
        // ==========================================
        // 3. READ ALL (Get All as List)
        // ==========================================
        public async Task<List<Application>> GetAllApplications()
        {
            List<Application> appsList = new List<Application>();
            string query = "SELECT * FROM Applications ORDER BY ApplicationDate DESC"; // Newest first!

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return appsList;
        }
        public  async Task<List<clsLocalDrivingLicesnseApplicationView>> GetAll_L_D_L_Applications()
        {        
            List<clsLocalDrivingLicesnseApplicationView> appsList = new List<clsLocalDrivingLicesnseApplicationView>();
          
            string query = @"SELECT 
                            Main.LocalDrivingLicenseApplicationID, 
                            Main.ClassName, 
                            Main.NationalNO, 
                            Main.FullName, 
                            Main.ApplicationDate, 
                            Main.ApplicationStatus,
                            ISNULL(TestCounts.PassedTests, 0) AS PassedTestsCount
                            FROM 
                            LocalDrivingLicenseApplicationsView AS Main
                            LEFT JOIN 
                            (
                            SELECT TestAppointments.LocalDrivingLicenseApplication_ID, COUNT(Tests.TestID) AS PassedTests
                            FROM Tests
                            INNER JOIN TestAppointments ON Tests.TestAppointment_ID = TestAppointments.TestAppointmentID
                            WHERE Tests.TestResult = 1
                            GROUP BY TestAppointments.LocalDrivingLicenseApplication_ID
                            ) AS TestCounts 
                            ON Main.LocalDrivingLicenseApplicationID = TestCounts.LocalDrivingLicenseApplication_ID
                            order by Main.LocalDrivingLicenseApplicationID DESC;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            appsList.Add(new clsLocalDrivingLicesnseApplicationView
                            {
                                LDLApplicationID = (int)reader["LocalDrivingLicenseApplicationID"],
                                DrivingClassTitle = reader["ClassName"].ToString(),
                                NationalNO = reader["NationalNO"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                ApplicationDate = (DateTime)reader["ApplicationDate"],
                                Status = reader["ApplicationStatus"].ToString(),
                                PassedTests = (int)reader["PassedTestsCount"]                              
                            });                            
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return appsList;
        }

        // ==========================================
        // 4. UPDATE
        // ==========================================
        public async Task<bool> UpdateApplication(Application app)
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
                    await connection.OpenAsync();
                    rowsAffected = await command.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return (rowsAffected > 0);
        }

        public async Task<bool> UpdateApplicationStatusTransactionalAsync(SqlConnection connection , SqlTransaction transaction,string Status , int appID)
        {
            int rowsAffected = 0;

            string query = @"UPDATE Applications
                         SET 
                             LastStatusDate = @LastStatusDate,
                             ApplicationStatus = @ApplicationStatus
                         WHERE ApplicationID = @ApplicationID";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@ApplicationID", appID);
                command.Parameters.AddWithValue("@ApplicationStatus", Status);
                command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);         
                try
                {
                    rowsAffected = await command.ExecuteNonQueryAsync();
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
        public async Task<bool> DeleteApplication(int applicationID)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", applicationID);

                try
                {
                    await connection.OpenAsync();

                    rowsAffected = await command.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {                    
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return (rowsAffected > 0);
        }

        public async Task<bool> DeleteLDLApplication(int lDLApplicationID)
        {
            bool isBaseApplicationDeleted = false;
            bool isLDLApplicationDeleted = false;
            int ApplicationId = -1;
            Application ApplicationBeforeDelete = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                await connection.OpenAsync();

                // 2. Begin the transaction
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        ApplicationBeforeDelete = await GetApplicationByLDL_ID(lDLApplicationID);
                        if (ApplicationBeforeDelete == null)
                        {
                            throw new ArgumentNullException("Returnd LDLApplication is Null");
                        }

                        ApplicationId = ApplicationBeforeDelete.ApplicationID;

                        isLDLApplicationDeleted = await DeleteLDLApplicationFromLDLTable(lDLApplicationID);
                        if (!isLDLApplicationDeleted)
                        {
                            throw new Exception("LDLApplication is not Deleted");
                        }

                        isBaseApplicationDeleted = await DeleteApplication(ApplicationId);
                        if (!isLDLApplicationDeleted)
                        {
                            throw new Exception("Base Application is not Deleted");
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return true;
        }
        private async Task<bool> DeleteLDLApplicationFromLDLTable(int lDLApplicationID)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", lDLApplicationID);

                try
                {
                    await connection.OpenAsync();

                    rowsAffected = await command.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return (rowsAffected > 0);
        }

    }
}
