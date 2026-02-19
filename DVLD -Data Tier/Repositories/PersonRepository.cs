using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Core.View_Models;

namespace DVLD__Data_Tier.Repositories
{
    public class PersonRepository
    {
        private static string connectionString = DataBaseSettings.DataBaseConnectionString;

        // ---------------------------------------------------------
        // 1. CREATE (Insert New Person)
        // ---------------------------------------------------------
        public static int AddNewPerson(Person person)
        {
            int newPersonID = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Persons 
                            (FirstName, SecondName,ThirdName, LastName, NationalNO, Gender, Email, Phone, Country_ID, Address, ImageName,DateOfBirth)
                            VALUES 
                            (@FirstName, @SecondName,@thirdName, @LastName, @NationalNO, @Gender, @Email, @Phone, @Country_ID, @Address, @ImageName,@dateOfBirth);
                            SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                    cmd.Parameters.AddWithValue("@SecondName", person.MiddelName);
                    cmd.Parameters.AddWithValue("@thirdName", person.ThirdName);
                    cmd.Parameters.AddWithValue("@LastName", person.LastName);
                    cmd.Parameters.AddWithValue("@NationalNO", person.NationalNO);
                    cmd.Parameters.AddWithValue("@Gender", person.Gender);
                    cmd.Parameters.AddWithValue("@Email", person.Email);
                    cmd.Parameters.AddWithValue("@Phone", person.Phone);
                    cmd.Parameters.AddWithValue("@Country_ID", person.Country_ID);
                    cmd.Parameters.AddWithValue("@Address", person.Address);
                    cmd.Parameters.AddWithValue("@dateOfBirth", person.DateOfBirth);
                    if (person.ImageName == string.Empty || person.ImageName == "")
                    {
                        cmd.Parameters.AddWithValue("@ImageName", "");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ImageName", person.ImageName);
                    }
                        

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            newPersonID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN AddNewPerson :" + ex.ToString() + " ***");
                        throw new Exception("Error in Database while inserting new Person");
                    }
                }
            }
            return newPersonID;
        }

        // ---------------------------------------------------------
        // 2. READ (Get Person by ID)
        // ---------------------------------------------------------
        public static Person GetPersonByID(int personID)
        {
            Person foundPerson = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Persons WHERE PersonID = @PersonID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personID);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foundPerson = new Person
                                {
                                    PersonID = (int)reader["PersonID"],
                                    FirstName = (string)reader["FirstName"],
                                    MiddelName = (reader["SecondName"] != DBNull.Value) ? (string)reader["SecondName"] : "",
                                    ThirdName = (reader["ThirdName"] != DBNull.Value) ? (string)reader["ThirdName"] : "",
                                    LastName = (string)reader["LastName"],
                                    NationalNO = (string)reader["NationalNO"],
                                    Gender = (string)reader["Gender"],
                                    Email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : "",
                                    Phone = (string)reader["Phone"],
                                    Country_ID = (int)reader["Country_ID"],
                                    Address = (string)reader["Address"],
                                    ImageName = (reader["ImageName"] != DBNull.Value) ? (string)reader["ImageName"] : "",
                                    DateOfBirth = (reader["DateOfBirth"] != DBNull.Value) ? (DateTime)reader["DateOfBirth"] : DateTime.MinValue
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN GetPersonByID :" + ex.ToString() + " ***");
                    }
                }
            }
            return foundPerson;
        }

        // ---------------------------------------------------------
        // 3. READ ALL (Get List of People)
        // ---------------------------------------------------------
        public static List<clsPersonView> GetAllPeople()
        {
             List<clsPersonView> peopleList = new List<clsPersonView>();

             using (SqlConnection conn = new SqlConnection(connectionString))
             {
                string query = "select * from PeopleView";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                peopleList.Add(new clsPersonView
                                {
                                    PersonID = (int)reader["PersonID"],
                                    FirstName = (string)reader["FirstName"],
                                    MiddelName = (reader["SecondName"] != DBNull.Value) ? (string)reader["SecondName"] : "",
                                    ThirdName = (reader["ThirdName"] != DBNull.Value) ? (string)reader["ThirdName"] : "",
                                    LastName = (string)reader["LastName"],
                                    NationalNO = (string)reader["NationalNO"],
                                    Gender = (string)reader["Gender"],
                                    Email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : "",
                                    Phone = (string)reader["Phone"],
                                    CountryName = reader["CountryName"].ToString(),
                                    Address = (string)reader["Address"],                                    
                                    DateOfBirth = (reader["DateOfBirth"] != DBNull.Value) ? (DateTime)reader["DateOfBirth"] : DateTime.MinValue
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN GetAllPeople :" + ex.ToString() + " ***");
                    }
                }
             }
             
            return peopleList;
        }

        // ---------------------------------------------------------
        // 4. UPDATE (Update Existing Person)
        // ---------------------------------------------------------
        public static bool UpdatePerson(Person person)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Persons 
                            SET FirstName = @firstName,
                                SecondName = @secondName,
                                ThirdName = @thirdName,
                                LastName = @lastName,
                                NationalNO = @nationalNO,
                                Gender = @gender,
                                Email = @email,
                                Phone = @phone,
                                Country_ID = @country_ID,
                                Address = @address,
                                ImageName = @imageName,
                                DateOfBirth = @dateOfBirth
                            WHERE PersonID = @personID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@personID", person.PersonID);
                    cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                    cmd.Parameters.AddWithValue("@secondName", person.MiddelName);
                    cmd.Parameters.AddWithValue("@thirdName", person.ThirdName);
                    cmd.Parameters.AddWithValue("@lastName", person.LastName);
                    cmd.Parameters.AddWithValue("@nationalNO", person.NationalNO);
                    cmd.Parameters.AddWithValue("@gender", person.Gender);
                    cmd.Parameters.AddWithValue("@email", person.Email);
                    cmd.Parameters.AddWithValue("@phone", person.Phone);
                    cmd.Parameters.AddWithValue("@country_ID", person.Country_ID);
                    cmd.Parameters.AddWithValue("@address", person.Address);
                    cmd.Parameters.AddWithValue("@imageName", person.ImageName);
                    cmd.Parameters.AddWithValue("@dateOfBirth", person.DateOfBirth);

                    try
                    {
                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN UpdatePerson :" + ex.ToString() + " ***");   
                        throw new Exception("Error in Database while updating Person");                        
                    }
                }
            }
            return (rowsAffected > 0);
        }

        // ---------------------------------------------------------
        // 5. DELETE (Delete Person)
        // ---------------------------------------------------------
        public static bool DeletePerson(int personID)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Persons WHERE PersonID = @PersonID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personID);

                    try
                    {
                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN DeletePerson :" + ex.ToString() + " ***");
                    }
                }
            }
            return (rowsAffected > 0);
        }

        // ---------------------------------------------------------
        // 6. CHECK EXISTENCE (Is Person Exists)
        // ---------------------------------------------------------
        public static bool IsPersonExist(int personID)
        {
            bool isFound = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Found=1 FROM Persons WHERE PersonID = @PersonID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personID);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            isFound = reader.HasRows;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN IsPersonExist :" + ex.ToString() + " ***");
                        isFound = false;
                    }
                }
            }
            return isFound;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Found=1 FROM Persons WHERE NationalNO = @nationalNO";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nationalNO", NationalNo);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            isFound = reader.HasRows;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN IsPersonExist :" + ex.ToString() + " ***");
                        isFound = false;
                    }
                }
            }
            return isFound;
        }
    }
}
