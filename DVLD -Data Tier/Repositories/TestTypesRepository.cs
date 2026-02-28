using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;

namespace DVLD__Data_Tier.Repositories
{
    public class TestTypesRepository
    {
        public static TestType GetTestTypeByID(int testTypeID)
        {
            TestType testType = null;

            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @ID";
            using (SqlConnection connection = new SqlConnection(DataBaseSettings.DataBaseConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID", testTypeID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            testType = new TestType
                            {
                                TestTypeID = (int)reader["TestTypeID"],
                                 TestTypeTitle = reader["TestTypeTitle"].ToString(),
                                TestTypeDescription = reader["TestTypeDescription"].ToString(),
                                TestTypeFees = (decimal)reader["TestTypeFees"]
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"***Error GetTestTypeByID  :{ex} ***");
                }
            }
            return testType;
        }

        public static List<TestType> GetAllTestTypes()
        {

            List<TestType> types = new List<TestType>();

            string query = "SELECT * FROM TestTypes ORDER BY TestTypeID ASC";
        
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
                            types.Add(new TestType
                            {
                                TestTypeID = (int)reader["TestTypeID"],
                                TestTypeTitle = reader["TestTypeTitle"].ToString(),
                                TestTypeDescription = reader["TestTypeDescription"].ToString(),
                                TestTypeFees = (decimal)reader["TestTypeFees"]
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"***Error GetAllTestTypes  :{ex} ***");
                }
            }
            return types;
        }

        public static bool UpdateTestType(TestType testType)
        {
            int rowsAffected = 0;

            string query = @"UPDATE ApplicationTypes  
                         SET
                            TestTypeTitle = @title,
                            TestTypeDescription = @description,
                            TestTypeFees = @Fees
                         WHERE TestTypeID = @ID";

            using (SqlConnection connection = new SqlConnection(DataBaseSettings.DataBaseConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@ID", testType.TestTypeID);
                command.Parameters.AddWithValue("@Title", testType.TestTypeTitle);
                command.Parameters.AddWithValue("@Fees", testType.TestTypeFees);
                command.Parameters.AddWithValue("@description", testType.TestTypeDescription);
                try
                {
                    connection.Open();

                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"***Error UpdateTestType  :{ex} ***");
                    return false;
                }
            }
            return (rowsAffected > 0);
        }
    }
}
