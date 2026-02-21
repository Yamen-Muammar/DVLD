using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Core.View_Models;

namespace DVLD__Data_Tier.Repositories
{    
    public static class UserRepository
    {
        private static string connectionString = DataBaseSettings.DataBaseConnectionString;

        // ---------------------------------------------------------
        // 1. CREATE (Insert New User)
        // ---------------------------------------------------------
        public static int AddNewUser(User user)
        {
            int newUserID = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Users 
                            (Username, HashedPassword,isActive, Person_ID)
                            VALUES 
                            (@username, @hashedPassword,@isActive, @personID);
                            SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@hashedPassword", user.HashedPassword);
                    cmd.Parameters.AddWithValue("@isActive", user.isActive);
                    cmd.Parameters.AddWithValue("@personID", user.Person_ID);                    
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            newUserID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN AddNewUser :" + ex.ToString() + " ***");
                        throw new Exception("Error in Database while inserting new User");
                    }
                }
            }
            return newUserID;
        }

        // ---------------------------------------------------------
        // 2. READ (Get User)
        // ---------------------------------------------------------
        public static User GetUserByID(int userID)
        {
            User foundUser = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE UserID = @userID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foundUser = new User
                                {
                                    UserID = (int)reader["UserID"],
                                    Username = reader["Username"].ToString(),
                                    HashedPassword = reader["HashedPassword"].ToString(),
                                    isActive = (bool)reader["isActive"],
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN GetPersonByID :" + ex.ToString() + " ***");
                        throw new Exception("Error While Get The User Data");
                    }
                }
            }
            return foundUser;
        }
        public static User GetUserByUsername(string username)
        {
            User foundUser = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE Username = @username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foundUser = new User
                                {
                                    UserID = (int)reader["UserID"],
                                    Username = reader["Username"].ToString(),
                                    HashedPassword = reader["HashedPassword"].ToString(),
                                    isActive = (bool)reader["isActive"],
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN GetUserByUsername :" + ex.ToString() + " ***");
                        throw new Exception("Error While Get The User Data");
                    }
                }
            }
            return foundUser;
        }

        // ---------------------------------------------------------
        // 3. READ ALL (Get List of Users)
        // ---------------------------------------------------------                        
        public static List<clsUserView> GetAllUser()
        {
            List<clsUserView> usersList = new List<clsUserView>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "select * from UsersView";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usersList.Add(new clsUserView
                                {
                                    UserID = (int)reader["UserID"],
                                    UserName = reader["UserName"].ToString(),
                                    FullName = reader["FullName"].ToString(),
                                    isActive = (bool)reader["isActive"]

                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN GetAllPeople :" + ex.ToString() + " ***");
                        throw new Exception("Error While Geting the User Data");
                    }
                }
            }

            return usersList;
        }
        // ---------------------------------------------------------
        // 4. UPDATE (Update Existing User)
        // ---------------------------------------------------------
        public static bool UpdateUser(User user)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Users 
                            SET Username = @username,
                                HashedPassword = @hashedPassword,
                                isActive = @isActive,
                            WHERE UserID = @userID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@hashedPassword", user.HashedPassword);
                    cmd.Parameters.AddWithValue("@isActive", user.isActive);
                    cmd.Parameters.AddWithValue("@userID", user.UserID);

                    try
                    {
                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN UpdateUser :" + ex.ToString() + " ***");
                        throw new Exception("Error in Database while updating User");
                    }
                }
            }
            return (rowsAffected > 0);
        }

        // ---------------------------------------------------------
        // 5. DELETE (Delete User)
        // ---------------------------------------------------------
        public static bool DeleteUser(int userID)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE UserID = @userID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);

                    try
                    {
                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("** Error IN DeleteUser :" + ex.ToString() + " ***");
                        throw new Exception("Error While Delete User");
                    }
                }
            }
            return (rowsAffected > 0);
        }

        // ---------------------------------------------------------
        // 6. CHECK EXISTENCE (Is User Exists)
        // ---------------------------------------------------------
        public static bool IsUserExist(int personID)
        {
            bool isFound = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Found=1 FROM Users WHERE Person_ID = @PersonID";

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
                        Debug.WriteLine("** Error IN IsUserExist :" + ex.ToString() + " ***");
                        throw new Exception("Error While Check if User Exists");
                    }
                }
            }
            return isFound;
        }

        
    }

}

