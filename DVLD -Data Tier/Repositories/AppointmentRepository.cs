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
    public class AppointmentRepository
    {
        private string _connectionString = DataBaseSettings.DataBaseConnectionString; 
        public async Task<int> AddNewTestAppoitmentAsync(TestAppointment appointment) 
        {
            int newAppointmentID = -1;
            string query = @"INSERT INTO TestAppointments
           (
            TestType_ID
           ,LocalDrivingLicenseApplication_ID
           ,AppointmentDate
           ,PaidFees
           ,CreatedByUser_ID
           ,isLocked
           ,RetakeTestApplication_ID)
            VALUES
           (
           @TestType_ID,
           @LocalDrivingLicenseApplication_ID,
           @AppointmentDate,
           @PaidFees,
           @CreatedByUser_ID,
           @isLocked,
           @RetakeTestApplication_ID);
           SELECT SCOPE_IDENTITY();";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@TestType_ID", appointment.TestType_ID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplication_ID", appointment.LocalDrivingLicenseApplication_ID);
                command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", appointment.PaidFees);
                command.Parameters.AddWithValue("@CreatedByUser_ID", appointment.CreatedByUser_ID);
                command.Parameters.AddWithValue("@isLocked", appointment.isLocked);
                if (appointment.RetakeTestApplication_ID != null)
                {
                    command.Parameters.AddWithValue("@RetakeTestApplication_ID", appointment.RetakeTestApplication_ID);
                }
                else
                {
                    command.Parameters.AddWithValue("@RetakeTestApplication_ID", DBNull.Value);
                }


                try
                {
                    await conn.OpenAsync();
                    object applicationReturnedID = await command.ExecuteScalarAsync();
                    int.TryParse(applicationReturnedID.ToString(), out newAppointmentID);

                }
                catch (Exception)
                {

                    throw;
                }
                
            }
            return newAppointmentID;
        }

        public async Task<int> DoesApplicationHasActiveAppointmentAsync(TestAppointment appointment)
        {
            int foundedAppointmentID = -1;
            string query = @"select TestAppointments.TestAppointmentID from TestAppointments
                             where TestAppointments.LocalDrivingLicenseApplication_ID = @LDLAppID AND isLocked = 0 and TestType_ID = @testTypeID;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LDLAppID", appointment.LocalDrivingLicenseApplication_ID);
                command.Parameters.AddWithValue("@testTypeID", appointment.TestType_ID);
                try
                {
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            foundedAppointmentID = reader.GetInt32(0);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return foundedAppointmentID;
        }

        public async Task<List<clsAppointmentsView>> GetAllAppointmentsAsync(int LDLApp , int testTypeID)
        {
            List<clsAppointmentsView> appsList = new List<clsAppointmentsView>();
            string query = "SELECT TestAppointmentID , AppointmentDate ,PaidFees,isLocked" +
                " FROM AppointmentsView " +
                "where LocalDrivingLicenseApplication_ID= @lDLAppID  AND TestType_ID = @testType ORDER BY TestAppointmentID DESC";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@lDLAppID",LDLApp);
                command.Parameters.AddWithValue("@testType", testTypeID);
                try
                {
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            appsList.Add(new clsAppointmentsView
                            {
                                TestAppointmentID = (int)reader["TestAppointmentID"],
                                AppointmentDate = (DateTime)reader["AppointmentDate"],
                                PaidFees = (decimal)reader["PaidFees"],
                                isLocked = Convert.ToBoolean(reader["isLocked"])
                                
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
    }
}
