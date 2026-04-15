using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;

namespace DVLD__Data_Tier.Repositories
{
    public
    class DetainedLicenseRepository
    {
        private string _connectionString;
        public DetainedLicenseRepository()
        {
            _connectionString = DataBaseSettings.DataBaseConnectionString;
        }

        public async Task<bool> IsLicenseDetained(int licenseID)
        {
            bool isLicenseDetained = false;
            string query = @"
                                select 1 as Founded from DetainedLicenses
                                where DetainedLicenses.license_ID = @licenseID;";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@licenseID", licenseID);
                try
                {
                    await conn.OpenAsync();
                    var result = await cmd.ExecuteScalarAsync();
                    if (result != null)
                    {
                        isLicenseDetained = true;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return isLicenseDetained;
            }
        }

        public async Task<int> AddNewDetainedLicenseAsync(DetainedLicense detainedLicense)
        {
            int insertedDetainID = -1;

            string query = @"
                INSERT INTO [dbo].[DetainedLicenses]
                           ([license_ID]
                           ,[DetainDate]
                           ,[FineFees]
                           ,[CreatedByUser_ID]
                           ,[isReleased]
                           ,[ReleaseDate]
                           ,[ReleaseByUser_ID]
                           ,[ReleaseApplication_ID])
                     VALUES
                           (@LicenseID, 
                            @DetainDate, 
                            @FineFees, 
                            @CreatedByUserID, 
                            @IsReleased, 
                            @ReleaseDate, 
                            @ReleaseByUserID, 
                            @ReleaseApplicationID);
               
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@LicenseID", detainedLicense.License_ID);
                command.Parameters.AddWithValue("@DetainDate", detainedLicense.DetainDate);
                command.Parameters.AddWithValue("@FineFees", detainedLicense.FineFees);
                command.Parameters.AddWithValue("@CreatedByUserID", detainedLicense.CreatedByUser_ID);
                command.Parameters.AddWithValue("@IsReleased", detainedLicense.isReleased);

                if (detainedLicense.ReleaseDate.HasValue)
                    command.Parameters.AddWithValue("@ReleaseDate", detainedLicense.ReleaseDate.Value);
                else
                    command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

                if (detainedLicense.ReleaseByUser_ID.HasValue)
                    command.Parameters.AddWithValue("@ReleaseByUserID", detainedLicense.ReleaseByUser_ID.Value);
                else
                    command.Parameters.AddWithValue("@ReleaseByUserID", DBNull.Value);

                if (detainedLicense.ReleaseApplication_ID.HasValue)
                    command.Parameters.AddWithValue("@ReleaseApplicationID", detainedLicense.ReleaseApplication_ID.Value);
                else
                    command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);

                try
                {
                    await connection.OpenAsync();

                    object result = await command.ExecuteScalarAsync();

                    if (result != null && int.TryParse(result.ToString(), out int generatedID))
                    {
                        insertedDetainID = generatedID;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return insertedDetainID;
        }

        public async Task<List<DetainedLicense>> GetAllDetainedLicensesAsync()
        {
            List<DetainedLicense> detainedLicenses = new List<DetainedLicense>();
            string query = @"
                SELECT DetainID, license_ID, DetainDate, FineFees, CreatedByUser_ID, isReleased, ReleaseDate, ReleaseByUser_ID, ReleaseApplication_ID
                FROM DetainedLicenses
                ORDER BY DetainDate DESC;";
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
                            DetainedLicense detainedLicense = new DetainedLicense
                            {
                                DetainID = reader.GetInt32(reader.GetOrdinal("DetainID")),
                                License_ID = reader.GetInt32(reader.GetOrdinal("license_ID")),
                                DetainDate = reader.GetDateTime(reader.GetOrdinal("DetainDate")),
                                FineFees = reader.GetDecimal(reader.GetOrdinal("FineFees")),
                                CreatedByUser_ID = reader.GetInt32(reader.GetOrdinal("CreatedByUser_ID")),
                                isReleased = reader.GetBoolean(reader.GetOrdinal("isReleased")),
                                ReleaseDate = reader.IsDBNull(reader.GetOrdinal("ReleaseDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ReleaseDate")),
                                ReleaseByUser_ID = reader.IsDBNull(reader.GetOrdinal("ReleaseByUser_ID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ReleaseByUser_ID")),
                                ReleaseApplication_ID = reader.IsDBNull(reader.GetOrdinal("ReleaseApplication_ID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ReleaseApplication_ID"))
                            };
                            detainedLicenses.Add(detainedLicense);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return detainedLicenses;
        }
    }
}
