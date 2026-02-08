using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;

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
                string query = @"INSERT INTO People 
                            (FirstName, MiddelName, LastName, NationalNO, Gender, Email, Phone, Country_ID, Address, ImagePath)
                            VALUES 
                            (@FirstName, @MiddelName, @LastName, @NationalNO, @Gender, @Email, @Phone, @Country_ID, @Address, @ImagePath);
                            SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                    cmd.Parameters.AddWithValue("@MiddelName", person.MiddelName); 
                    cmd.Parameters.AddWithValue("@LastName", person.LastName);
                    cmd.Parameters.AddWithValue("@NationalNO", person.NationalNO);
                    cmd.Parameters.AddWithValue("@Gender", person.Gender);
                    cmd.Parameters.AddWithValue("@Email", person.Email);
                    cmd.Parameters.AddWithValue("@Phone", person.Phone);
                    cmd.Parameters.AddWithValue("@Country_ID", person.Country_ID);
                    cmd.Parameters.AddWithValue("@Address", person.Address);

                    if (person.ImageName == string.Empty || person.ImageName == "")
                    {
                        cmd.Parameters.AddWithValue("@ImagePath", "");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ImagePath", person.ImageName);
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
                        Debug.WriteLine("** Error IN ADD Person :"+ex.ToString()+" ***");
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
                string query = "SELECT * FROM People WHERE PersonID = @PersonID";

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
                                    MiddelName = (reader["MiddelName"] != DBNull.Value) ? (string)reader["MiddelName"] : "",
                                    LastName = (string)reader["LastName"],
                                    NationalNO = (string)reader["NationalNO"],
                                    Gender = (string)reader["Gender"],
                                    Email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : "",
                                    Phone = (string)reader["Phone"],
                                    Country_ID = (int)reader["Country_ID"],
                                    Address = (string)reader["Address"],
                                    ImageName = (reader["ImagePath"] != DBNull.Value) ? (string)reader["ImagePath"] : ""
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
        public static List<Person> GetAllPeople()
        {
            List < Person > people = new List < Person >();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM People";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Person person = new Person
                                {
                                    PersonID = (int)reader["PersonID"],
                                    FirstName = (string)reader["FirstName"],
                                    MiddelName = (reader["MiddelName"] != DBNull.Value) ? (string)reader["MiddelName"] : "",
                                    LastName = (string)reader["LastName"],
                                    NationalNO = (string)reader["NationalNO"],
                                    Gender = (string)reader["Gender"],
                                    Email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : "",
                                    Phone = (string)reader["Phone"],
                                    Country_ID = (int)reader["Country_ID"],
                                    Address = (string)reader["Address"],
                                    ImageName = (reader["ImagePath"] != DBNull.Value) ? (string)reader["ImagePath"] : ""

                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN GetAllPeople :" + ex.ToString() + " ***");
                    }
                }
            }
            return people;
        }

        // ---------------------------------------------------------
        // 4. UPDATE (Update Existing Person)
        // ---------------------------------------------------------
        public static bool UpdatePerson(Person person)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE People 
                            SET FirstName = @FirstName,
                                MiddelName = @MiddelName,
                                LastName = @LastName,
                                NationalNO = @NationalNO,
                                Gender = @Gender,
                                Email = @Email,
                                Phone = @Phone,
                                Country_ID = @Country_ID,
                                Address = @Address,
                                ImagePath = @ImagePath
                            WHERE PersonID = @PersonID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", person.PersonID);
                    cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                    cmd.Parameters.AddWithValue("@MiddelName", person.MiddelName);
                    cmd.Parameters.AddWithValue("@LastName", person.LastName);
                    cmd.Parameters.AddWithValue("@NationalNO", person.NationalNO);
                    cmd.Parameters.AddWithValue("@Gender", person.Gender);
                    cmd.Parameters.AddWithValue("@Email", person.Email);
                    cmd.Parameters.AddWithValue("@Phone", person.Phone);
                    cmd.Parameters.AddWithValue("@Country_ID", person.Country_ID);
                    cmd.Parameters.AddWithValue("@Address", person.Address);
                    cmd.Parameters.AddWithValue("@ImagePath",person.ImageName);

                    try
                    {
                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN UpdatePerson :" + ex.ToString() + " ***");   
                        return false;
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
                string query = "DELETE FROM People WHERE PersonID = @PersonID";

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
                string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

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
                string query = "SELECT Found=1 FROM People WHERE NationalNO = @nationalNO";

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
