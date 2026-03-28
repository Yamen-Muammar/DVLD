using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Data_Tier.Repositories
{
    public class TestRepository
    {
        private string _connectionString = DataBaseSettings.DataBaseConnectionString;
        public async Task<int> GetPassedTestsAsync(string nationalNo)
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
                            where Tests.TestResult = 1 and Persons.NationalNO = @nationalNo;
";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nationalNo", nationalNo);
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
    }
}
