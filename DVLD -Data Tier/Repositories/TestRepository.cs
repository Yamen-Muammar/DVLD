using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD__Data_Tier.Repositories
{
    public class TestRepository
    {
        private string _connectionString = DataBaseSettings.DataBaseConnectionString;
        public async Task<int> GetPassedTestsAsync(int ldlAppID,string nationalNo,string className)
        {
            int passedTestsCount = -1;

            string query = @"
                            select count(TestID)
                            from Tests
                            inner join TestAppointments
                            on Tests.TestAppointment_ID = TestAppointments.TestAppointmentID
                            inner join LocalDrivingLicenseApplications
                            on TestAppointments.LocalDrivingLicenseApplication_ID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            INNER JOIN Applications
                            on LocalDrivingLicenseApplications.Application_ID  = Applications.ApplicationID
                            inner join Persons on Persons.PersonID = Applications.Person_ID
                            inner join LicenseClasses
                            on LocalDrivingLicenseApplications.LicenseClass_ID = LicenseClasses.LicenseClassID

                            where Tests.TestResult = 1 and Persons.NationalNO = @nationalNo and LicenseClasses.ClassName = @className and LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @ldlAppID;
";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nationalNo", nationalNo);
                cmd.Parameters.AddWithValue("@className", className);
                cmd.Parameters.AddWithValue("@ldlAppID", ldlAppID);
                try
                {
                    await conn.OpenAsync();

                    object result = await cmd.ExecuteScalarAsync();

                    if (result != null && int.TryParse(result.ToString(), out int count))
                    {
                        passedTestsCount = count;
                    }
                }
                catch (Exception)
                {
                    throw;
                }    
            }
            return passedTestsCount;
        }
        // ==========================================
        // READ (checks By TestAppointment ID)
        // ==========================================
        public async Task<bool> isAppointmentTakedTestAsync(int appointmentID)
        {
            bool isPass = false;
            string query = @"select yes = 1 from Tests t
                             inner join TestAppointments ta on t.TestAppointment_ID = ta.TestAppointmentID
                             where ta.TestAppointmentID = @appID;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@appID", appointmentID);
                try
                {
                    await connection.OpenAsync();
                    object result = await command.ExecuteScalarAsync();
                     if (result != null && int.TryParse(result.ToString(),out int resultValue))
                     {
                        isPass = (resultValue == 1);
                     }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return isPass;
        }
        public async Task<bool> isAppointmentHasFailTestResultAsync(int appointmentID)
        {
            bool isFail = false;
            string query = @"select faild = 1 from Tests t
                             inner join TestAppointments ta on t.TestAppointment_ID = ta.TestAppointmentID
                             where t.TestResult = 0 and ta.TestAppointmentID = @appID;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@appID", appointmentID);
                try
                {
                    await connection.OpenAsync();

                    object result = await command.ExecuteScalarAsync();
                     if (result != null && int.TryParse(result.ToString(),out int resultValue))
                     {
                        isFail = (resultValue == 1);
                     }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return isFail;
        }
        // ==========================================
        // READ (Get All Test For TestAppointment ID)
        // ==========================================
        public async Task<List<Test>> GetTestsByAppointmentIDAsync(int appointmentID)
        {
            List<Test> tests = new List<Test>();
            string query = @"SELECT TestID, TestAppointment_ID, TestResult, Notes, CreatedByUser_ID 
                             FROM Tests 
                             WHERE TestAppointment_ID = @appointmentID;";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@appointmentID", appointmentID);
                try
                {
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Test test = new Test
                            (
                                (int)reader["TestID"],
                                 (int)reader["TestAppointment_ID"],
                                 (bool)reader["TestResult"],
                                reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : string.Empty,
                                 (int)reader["CreatedByUser_ID"]
                            );
                            tests.Add(test);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return tests;
        }
        // ==========================================
        // CREATE
        // ==========================================
        public async Task<int> AddNewTestAsync(Test newTest)
        {
            int insertedID = -1;

            // SCOPE_IDENTITY() grabs the brand new ID that SQL auto-generates
            string query = @"INSERT INTO Tests (TestAppointment_ID, TestResult, Notes, CreatedByUser_ID)
                         VALUES (@TestAppointment_ID, @TestResult, @Notes, @CreatedByUser_ID);
                         SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestAppointment_ID", newTest.TestAppointment_ID);
                cmd.Parameters.AddWithValue("@TestResult", newTest.TestResult);
                cmd.Parameters.AddWithValue("@CreatedByUser_ID", newTest.CreatedByUser_ID);
    
                if (string.IsNullOrWhiteSpace(newTest.Notes))
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Notes", newTest.Notes);

                try
                {
                    await conn.OpenAsync();
                    object result = await cmd.ExecuteScalarAsync();

                    if (result != null && int.TryParse(result.ToString(), out int generatedID))
                    {
                        insertedID = generatedID;
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
            return insertedID;
        }
        // ==========================================
        // READ (Get By ID)
        // ==========================================
        public async Task<Test> GetTestByIDAsync(int testID)
        {
            Test test = null;

            string query = @"SELECT TestID,TestAppointment_ID, TestResult, Notes, CreatedByUser_ID FROM Tests WHERE TestID = @TestID;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestID", testID);

                try
                {
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            test = new Test
                            (
                                (int)reader["TestID"],
                                 (int)reader["TestAppointment_ID"],
                                 (bool)reader["TestResult"],
                                reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : string.Empty,
                                 (int)reader["CreatedByUser_ID"]
                            );
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return test;
        }

        // ==========================================
        // UPDATE
        // ==========================================
        public async Task<bool> UpdateTestAsync(Test test)
        {
            int rowsAffected = -1;

            string query = @"UPDATE Tests 
                         SET TestAppointment_ID = @TestAppointment_ID, 
                             TestResult = @TestResult, 
                             Notes = @Notes, 
                             CreatedByUser_ID = @CreatedByUser_ID
                         WHERE TestID = @TestID;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestID", test.TestID);
                cmd.Parameters.AddWithValue("@TestAppointment_ID", test.TestAppointment_ID);
                cmd.Parameters.AddWithValue("@TestResult", test.TestResult);
                cmd.Parameters.AddWithValue("@CreatedByUser_ID", test.CreatedByUser_ID);

                if (string.IsNullOrWhiteSpace(test.Notes))
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Notes", test.Notes);

                try
                {
                    await conn.OpenAsync();
                    rowsAffected = await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (rowsAffected > 0);
        }

        // ==========================================
        // D - DELETE
        // ==========================================
        public async Task<bool> DeleteTestAsync(int testID)
        {
            int rowsAffected = -1;

            string query = @"DELETE FROM Tests WHERE TestID = @TestID;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestID", testID);

                try
                {
                    await conn.OpenAsync();
                    rowsAffected = await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (rowsAffected > 0);
        }
    }
}
