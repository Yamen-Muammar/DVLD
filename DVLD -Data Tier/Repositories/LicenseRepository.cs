using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core;
using DVLD__Core.Models;
using DVLD__Core.View_Models;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD__Data_Tier.Repositories
{
    public class LicenseRepository
    {
        private readonly string _connectionString = DataBaseSettings.DataBaseConnectionString;
        DriverRepository _driverRepository;
        ApplicationRepository _applicationRepository;
        public LicenseRepository()
        {
            _driverRepository = new DriverRepository();
            _applicationRepository = new ApplicationRepository();
        }


        // Add
        public async Task<int> InsertNewLicenseForFirstTimeAsync(DVLD__Core.Models.Application application, DVLD__Core.Models.License newLicense)
        {
            int insertedLicenseID = -1;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // ==========================================
                        // STEP 1: INSERT DRIVER
                        // ==========================================
                        Driver newDriver = new Driver(application.Person_ID, newLicense.CreateByUser_ID, newLicense.IssueDate);

                        newLicense.Driver_ID = await _insertNewDriverForTransactionalAsync(newDriver, transaction, connection);
                        if (newLicense.Driver_ID <= 0)
                        {
                            throw new Exception("Failed to insert new Driver.");
                        }

                        // ==========================================
                        // STEP 2: INSERT LICENSE
                        // ==========================================
                        insertedLicenseID = await _insertNewLicenseForTransactionalAsync(newLicense, transaction, connection);

                        if (insertedLicenseID <= 0)
                        {
                            throw new Exception("Failed to insert new License.");
                        }
                        // ==========================================
                        // STEP 3: UPDATE APPLICATION STATUS TO 'COMPLETED'
                        // ==========================================

                        if (! await _updateApplicationStatusToCompletedForTransactionalAsync(application.ApplicationID,transaction,connection))
                        {
                            throw new Exception("Failed to update Application status to Completed.");
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return insertedLicenseID;
        }

        public async Task<int> InsertNewLicenseOnlyAsync(DVLD__Core.Models.Application application, DVLD__Core.Models.License newLicense)
        {
            int insertedLicenseID = -1;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // ==========================================
                        // STEP 1: INSERT LICENSE
                        // ==========================================
                        insertedLicenseID = await _insertNewLicenseForTransactionalAsync(newLicense, transaction, connection);

                        if (insertedLicenseID <= 0)
                        {
                            throw new Exception("Failed to insert new License.");
                        }
                        // ==========================================
                        // STEP 2: UPDATE APPLICATION STATUS TO 'COMPLETED'
                        // ==========================================

                        if (!await _updateApplicationStatusToCompletedForTransactionalAsync(application.ApplicationID, transaction, connection))
                        {
                            throw new Exception("Failed to update Application status to Completed.");
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return insertedLicenseID;
        }

        public async Task<int> InsertNewInternationalLicense(DVLD__Core.Models.Application application , InternationalLicense internationalLicense)
        {

            int InsertedLicenseID = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // insert  application
                        application.ApplicationID =await _insertNewApplicationAsync(application,transaction,connection);
                        if (application.ApplicationID <= 0)
                        {
                            throw new ArgumentException("Error While Inserting Application");
                        }

                        internationalLicense.Application_ID = application.ApplicationID;

                        // insert license 

                        InsertedLicenseID = await _insertNewInternationalLicenseAsync(internationalLicense,transaction,connection);
                        if (InsertedLicenseID <= 0)
                        {
                            throw new ArgumentException("Error While Inserting International License");
                        }


                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                }
            }
            return InsertedLicenseID;
        }

        public async Task<int> RenewLocalLicense(int previouslicenseID, DVLD__Core.Models.Application application, DVLD__Core.Models.License newLicense)
        {
            int InsertedLicenseID = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // insert application
                        application.ApplicationID = await _insertNewApplicationAsync(application, transaction, connection);
                        if (application.ApplicationID <= 0)
                        {
                            throw new ArgumentException("Error While Inserting Application");
                        }

                        newLicense.Application_ID = application.ApplicationID;

                        // insert license 

                        InsertedLicenseID =await _insertRenewLicenseForTransactionalAsync(newLicense, transaction, connection);
                        if (InsertedLicenseID <= 0)
                        {
                            throw new ArgumentException("Error While Inserting International License");
                        }

                        // update previous License to inactive
                        bool isPreLicenseInActive = await _deactivateLicenseForTransactionalAsync(previouslicenseID, transaction, connection);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                }
            }
            return InsertedLicenseID;
        }

        public async Task<int> ReplaceLocalLicense(int previouslicenseID, DVLD__Core.Models.Application application, DVLD__Core.Models.License newLicense)
        {
            int InsertedLicenseID = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // insert application
                        application.ApplicationID = await _insertNewApplicationAsync(application, transaction, connection);
                        if (application.ApplicationID <= 0)
                        {
                            throw new ArgumentException("Error While Inserting Application");
                        }

                        newLicense.Application_ID = application.ApplicationID;

                        // insert license 

                        InsertedLicenseID = await _insertRenewLicenseForTransactionalAsync(newLicense, transaction, connection);
                        if (InsertedLicenseID <= 0)
                        {
                            throw new ArgumentException("Error While Inserting International License");
                        }

                        // update previous License to inactive
                        bool isPreLicenseInActive = await _deactivateLicenseForTransactionalAsync(previouslicenseID, transaction, connection);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                }
            }
            return InsertedLicenseID;
        }


        // GET
        public async Task<DVLD__Core.Models.License> GetLicenseByLocalDrivingLicenseAppIDAsync(int localDrivingLicenseAppID)
        {
            DVLD__Core.Models.License foundLicense = null;

            string query = @"
                SELECT LicenseID, Driver_ID, IssueDate, IssueReasen, 
                Note, isActive, ExpirationDate, CreateByUser_ID, LocalDrivingLicenseApplication_ID , Application_ID, LicenseClass_ID
                FROM Licenses 
                WHERE LocalDrivingLicenseApplication_ID = @LDLAppID;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LDLAppID", localDrivingLicenseAppID);

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            foundLicense = new DVLD__Core.Models.License
                            {
                                LicenseID = (int)reader["LicenseID"],
                                Driver_ID = (int)reader["Driver_ID"],
                                IssueDate = (DateTime)reader["IssueDate"],
                                IssueReasen = (string)reader["IssueReasen"],

                                // Protect against DBNull for the Note column!
                                Note = reader["Note"] != DBNull.Value ? (string)reader["Note"] : string.Empty,

                                isActive = (bool)reader["isActive"],
                                ExpirationDate = (DateTime)reader["ExpirationDate"],
                                CreateByUser_ID = (int)reader["CreateByUser_ID"],
                                LocalDrivingLicenseApplication_ID = (int)reader["LocalDrivingLicenseApplication_ID"],
                                Application_ID = reader["Application_ID"] != DBNull.Value ? (int?)reader["Application_ID"] : null,
                                LicenseClass_ID  = reader["LicenseClass_ID"] != DBNull.Value ? (int?)reader["LicenseClass_ID"] : null

                            };
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return foundLicense;
        }

        public async Task<DVLD__Core.Models.InternationalLicense> GetInternationalLicenseByLocalLicenseIDAsync(int localLicenseID)
        {
            DVLD__Core.Models.InternationalLicense foundLicense = null;

            string query = @"
                SELECT InternationalLicenseID, IssueDate, IsActive, ExpirationDate, CreatedBy_ID, Application_ID,LocalLicense_ID 
                FROM InternationalLicenses 
                WHERE LocalLicense_ID = @LicID and IsActive = 1 ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicID", localLicenseID);

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            foundLicense = new DVLD__Core.Models.InternationalLicense
                            {
                                InternationalLicenseID = (int)reader["InternationalLicenseID"],
                                LocalLicense_ID = (int)reader["LocalLicense_ID"],
                                Application_ID = (int)reader["Application_ID"],
                                IssueDate = (DateTime)reader["IssueDate"],
                                IsActive = (bool)reader["IsActive"],
                                ExpirationDate = (DateTime)reader["ExpirationDate"],
                                CreatedBy_ID = (int)reader["CreatedBy_ID"],
                            };
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return foundLicense;
        }

        public async Task<DVLD__Core.Models.License> GetLicenseByIDAsync(int id)
        {
            DVLD__Core.Models.License foundLicense = null;

            string query = @"
                SELECT LicenseID, Driver_ID, IssueDate, IssueReasen, 
                Note, isActive, ExpirationDate, CreateByUser_ID, LocalDrivingLicenseApplication_ID ,Application_ID,LicenseClass_ID
                FROM Licenses 
                WHERE LicenseID  = @licID;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@licID", id);

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            foundLicense = new DVLD__Core.Models.License
                            {
                                LicenseID = (int)reader["LicenseID"],
                                Driver_ID = (int)reader["Driver_ID"],
                                IssueDate = (DateTime)reader["IssueDate"],
                                IssueReasen = (string)reader["IssueReasen"],

                                // Protect against DBNull for the Note column!
                                Note = reader["Note"] != DBNull.Value ? (string)reader["Note"] : string.Empty,

                                isActive = (bool)reader["isActive"],
                                ExpirationDate = (DateTime)reader["ExpirationDate"],
                                CreateByUser_ID = (int)reader["CreateByUser_ID"],
                                LocalDrivingLicenseApplication_ID = (reader["LocalDrivingLicenseApplication_ID"] != DBNull.Value )? (int ?)reader["LocalDrivingLicenseApplication_ID"] : null,
                                Application_ID = (int) reader["Application_ID"],
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

            return foundLicense;
        }

        public async Task<List<clsLicenseHistoryView>> GetAllLocalLicensesForPersonAsync(int personID)
        {
            List<clsLicenseHistoryView> licenseHistoryList = new List<clsLicenseHistoryView>();
            string query = @"
               select Licenses.LicenseID,Licenses.Application_ID,LicenseClasses.ClassName,Licenses.IssueDate,Licenses.ExpirationDate,Licenses.isActive
               from Licenses
               inner join LicenseClasses on Licenses.LicenseClass_ID = LicenseClasses.LicenseClassID
               inner join Applications on Licenses.Application_ID = Applications.ApplicationID
               WHERE Applications.Person_ID = @personID
               Order by Application_ID Desc;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@personID", personID);

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                             licenseHistoryList.Add(new clsLicenseHistoryView {
                                 LicenseID = (int)reader["LicenseID"],
                                 Application_ID = (int)reader["Application_ID"],
                                 ClassName = (string)reader["ClassName"],
                                 IssueDate = (DateTime)reader["IssueDate"],
                                 ExpirationDate = (DateTime)reader["ExpirationDate"],
                                 isActive = (bool)reader["isActive"]
                             });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return licenseHistoryList;
        }

        public async Task<List<clsInternationalLicenseHistory>> GetAllInternationalLicensesForPersonAsync(int personID)
        {
            List<clsInternationalLicenseHistory> licenseHistoryList = new List<clsInternationalLicenseHistory>();
            string query = @"
                        SELECT
                         [InternationalLicenseID]
                        ,[LocalLicense_ID]
                        ,[ExpirationDate]
                        ,[Application_ID]
                        ,[IssueDate]
  	                    ,[IsActive]
                        FROM [dbo].[InternationalLicenses]
                        INNER JOIN Applications ON InternationalLicenses.Application_ID = Applications.ApplicationID
                        WHERE Applications.Person_ID =  @personID;";
                                                      

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@personID", personID);

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            licenseHistoryList.Add(new clsInternationalLicenseHistory
                            {
                                InternationalLicenseID = (int)reader["InternationalLicenseID"],
                                Application_ID = (int)reader["Application_ID"],
                                LocalLicense_ID = (int)reader["LocalLicense_ID"],
                                IssueDate = (DateTime)reader["IssueDate"],
                                ExpirationDate = (DateTime)reader["ExpirationDate"],
                                isActive = (bool)reader["isActive"]
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return licenseHistoryList;
        }

        public async Task<List<clsInternationalLicenseHistory>> GetAllInternationalLicensesAsync()
        {
            List<clsInternationalLicenseHistory> licenseHistoryList = new List<clsInternationalLicenseHistory>();
            string query = @"
                        SELECT
                         [InternationalLicenseID]
                        ,[LocalLicense_ID]
                        ,[ExpirationDate]
                        ,[Application_ID]
                        ,[IssueDate]
  	                    ,[IsActive]
                        FROM [dbo].[InternationalLicenses];";


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
                            licenseHistoryList.Add(new clsInternationalLicenseHistory
                            {
                                InternationalLicenseID = (int)reader["InternationalLicenseID"],
                                Application_ID = (int)reader["Application_ID"],
                                LocalLicense_ID = (int)reader["LocalLicense_ID"],
                                IssueDate = (DateTime)reader["IssueDate"],
                                ExpirationDate = (DateTime)reader["ExpirationDate"],
                                isActive = (bool)reader["isActive"]
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return licenseHistoryList;
        }

        public async Task<int> ActiveLicense(int driverID , int LicenseClassID)
        {
            int ActiveID = -1;

            string query = @"
                SELECT LicenseID 
                FROM Licenses 
                WHERE Driver_ID  = @driverID and LicenseClass_ID = @licClassID and ExpirationDate > GETDATE();";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@driverID", driverID);
                command.Parameters.AddWithValue("@licClassID", LicenseClassID);

                try
                {
                    await connection.OpenAsync();

                    object result = await command.ExecuteScalarAsync();
                    if (result != null && int.TryParse(result.ToString(), out int ID))
                    {
                        ActiveID = ID;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return ActiveID;
        }
        

        // helper methods for transactional operations

        // local License
        private async Task<int> _insertNewDriverForTransactionalAsync(DVLD__Core.Models.Driver newDriver, SqlTransaction transaction, SqlConnection connection)
        {
            _driverRepository = new DriverRepository();

            return
                 await _driverRepository.InsertNewDriverForTransactionalAsync(newDriver, transaction, connection);
        }

        private async Task<int> _insertNewLicenseForTransactionalAsync(DVLD__Core.Models.License newLicense, SqlTransaction transaction, SqlConnection connection)
        {
            int insertedLicenseID = -1;
            string insertLicenseQuery = @"
                            INSERT INTO Licenses 
                            (Driver_ID, IssueDate, IssueReasen, Note, isActive, ExpirationDate, CreateByUser_ID, LocalDrivingLicenseApplication_ID)
                            VALUES 
                            (@Driver_ID, @IssueDate, @IssueReasen, @Note, @isActive, @ExpirationDate, @LicenseCreatedBy, @LDLApp_ID);
                            SELECT SCOPE_IDENTITY();";
            using (SqlCommand cmdLicense = new SqlCommand(insertLicenseQuery, connection, transaction))
            {
                cmdLicense.Parameters.AddWithValue("@Driver_ID", newLicense.Driver_ID);
                cmdLicense.Parameters.AddWithValue("@IssueDate", newLicense.IssueDate);
                cmdLicense.Parameters.AddWithValue("@IssueReasen", newLicense.IssueReasen);
                cmdLicense.Parameters.AddWithValue("@isActive", newLicense.isActive);
                cmdLicense.Parameters.AddWithValue("@ExpirationDate", newLicense.ExpirationDate);
                cmdLicense.Parameters.AddWithValue("@LicenseCreatedBy", newLicense.CreateByUser_ID);
                cmdLicense.Parameters.AddWithValue("@LDLApp_ID", newLicense.LocalDrivingLicenseApplication_ID);

                if (string.IsNullOrWhiteSpace(newLicense.Note))
                    cmdLicense.Parameters.AddWithValue("@Note", DBNull.Value);
                else
                    cmdLicense.Parameters.AddWithValue("@Note", newLicense.Note);

                object licenseResult = await cmdLicense.ExecuteScalarAsync();
                if (licenseResult != null && int.TryParse(licenseResult.ToString(), out int generatedLicenseID))
                {
                    insertedLicenseID = generatedLicenseID;
                }
            }
            return insertedLicenseID;
        }

        private async Task<bool> _updateApplicationStatusToCompletedForTransactionalAsync(int applicationID, SqlTransaction transaction, SqlConnection connection)
        {
            if (await _applicationRepository.UpdateApplicationStatusTransactionalAsync(connection, transaction, "Completed", applicationID))
            {
                return true;
            }
            return false;
        }

        private async Task<int> _insertNewApplicationAsync(DVLD__Core.Models.Application application, SqlTransaction transaction, SqlConnection connection)
        {
            return await _applicationRepository.InsertApplicationTransactional(connection, transaction, application);
        }

        // Helper needed for renewal
        private async Task<int> _insertRenewLicenseForTransactionalAsync(DVLD__Core.Models.License newLicense, SqlTransaction transaction, SqlConnection connection)
        {
            int insertedLicenseID = -1;
            string insertLicenseQuery = @"
                            INSERT INTO Licenses 
                            (Driver_ID, IssueDate, IssueReasen, Note, isActive, ExpirationDate, CreateByUser_ID, Application_ID,LicenseClass_ID)
                            VALUES 
                            (@Driver_ID, @IssueDate, @IssueReasen, @Note, @isActive, @ExpirationDate, @LicenseCreatedBy, @App_ID,@LicClassID);
                            SELECT SCOPE_IDENTITY();";
            using (SqlCommand cmdLicense = new SqlCommand(insertLicenseQuery, connection, transaction))
            {
                cmdLicense.Parameters.AddWithValue("@Driver_ID", newLicense.Driver_ID);
                cmdLicense.Parameters.AddWithValue("@IssueDate", newLicense.IssueDate);
                cmdLicense.Parameters.AddWithValue("@IssueReasen", newLicense.IssueReasen);
                cmdLicense.Parameters.AddWithValue("@isActive", newLicense.isActive);
                cmdLicense.Parameters.AddWithValue("@ExpirationDate", newLicense.ExpirationDate);
                cmdLicense.Parameters.AddWithValue("@LicenseCreatedBy", newLicense.CreateByUser_ID);
                cmdLicense.Parameters.AddWithValue("@App_ID", newLicense.Application_ID);
                cmdLicense.Parameters.AddWithValue("@LicClassID", newLicense.LicenseClass_ID);

                if (string.IsNullOrWhiteSpace(newLicense.Note))
                    cmdLicense.Parameters.AddWithValue("@Note", DBNull.Value);
                else
                    cmdLicense.Parameters.AddWithValue("@Note", newLicense.Note);

                object licenseResult = await cmdLicense.ExecuteScalarAsync();
                if (licenseResult != null && int.TryParse(licenseResult.ToString(), out int generatedLicenseID))
                {
                    insertedLicenseID = generatedLicenseID;
                }
            }
            return insertedLicenseID;
        }
        private async Task<bool> _deactivateLicenseForTransactionalAsync(int prelicenseID, SqlTransaction transaction, SqlConnection connection)
        {
            string query = @"UPDATE Licenses 
                     SET isActive = 0 
                     WHERE LicenseID = @LicenseID";

            using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@LicenseID", prelicenseID);
                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }

        // international license
        private async Task<int> _insertNewInternationalLicenseAsync(InternationalLicense newLicense, SqlTransaction transaction, SqlConnection connection)
        {
            int insertedLicenseID = -1;
            string insertLicenseQuery = @"
                            INSERT INTO InternationalLicenses 
                            (IssueDate, IsActive, ExpirationDate, CreatedBy_ID, Application_ID,LocalLicense_ID)
                            VALUES 
                            ( @IssueDate, @isActive, @ExpirationDate, @LicenseCreatedBy, @appID,@LLicID);
                            SELECT SCOPE_IDENTITY();";
            using (SqlCommand cmdLicense = new SqlCommand(insertLicenseQuery, connection, transaction))
            {
                cmdLicense.Parameters.AddWithValue("@IssueDate", newLicense.IssueDate);
                cmdLicense.Parameters.AddWithValue("@isActive", newLicense.IsActive);
                cmdLicense.Parameters.AddWithValue("@ExpirationDate", newLicense.ExpirationDate);
                cmdLicense.Parameters.AddWithValue("@LicenseCreatedBy", newLicense.CreatedBy_ID);
                cmdLicense.Parameters.AddWithValue("@appID", newLicense.Application_ID);
                cmdLicense.Parameters.AddWithValue("@LLicID", newLicense.LocalLicense_ID);

                object licenseResult = await cmdLicense.ExecuteScalarAsync();
                if (licenseResult != null && int.TryParse(licenseResult.ToString(), out int generatedLicenseID))
                {
                    insertedLicenseID = generatedLicenseID;
                }
            }
            return insertedLicenseID;
        }
    }
}
