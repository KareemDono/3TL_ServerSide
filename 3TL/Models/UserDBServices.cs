using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace _3TL.Models
{
    public static class UserDBServices
    {
        static string conStr = @"Data Source=KAREEMDONO\SQLEXPRESS;Initial Catalog=Buildr;Integrated Security=True";
        public static Users[] GetAllUsers()
        {
            return GetUsersByQuery(
                " SELECT * " +
                " FROM USERS");
        }

        private static Users[] GetUsersByQuery(string query)
        {
            List<Users> users = new List<Users>();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new Users()
                    {
                        Id = (int)reader["Id"],
                        firstName = (string)reader["FirstName"],
                        lastName = (string)reader["LastName"],
                        username = (string)reader["Username"],
                        email = (string)reader["Email"],
                        phoneNumber = (string)reader["PhoneNumber"],
                        birthDate = (string)reader["BirthDate"],
                        city = (string)reader["City"],
                        password = (string)reader["Password"],
                        userType = (string)reader["UserType"]
                    });
                }
                reader.Close();
            }

            return users.ToArray();
        }

        public static Users GetUserById(int id)
        {
            return GetUsersByQuery(
                " SELECT * " +
                " FROM Users " +
                " WHERE Id=" + id)[0];
        }

        public static int CreateUser(Users user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Users (firstName, lastName, username, email, phoneNumber, birthDate, city, password, userType)" +
                        "VALUES (@firstName, @lastName, @username, @email, @phoneNumber, @birthDate, @city, @password, @userType)",
                        con);

                    cmd.Parameters.AddWithValue("@firstName", user.firstName);
                    cmd.Parameters.AddWithValue("@lastName", user.lastName);
                    cmd.Parameters.AddWithValue("@username", user.username);
                    cmd.Parameters.AddWithValue("@email", user.email);
                    cmd.Parameters.AddWithValue("@phoneNumber", user.phoneNumber);
                    cmd.Parameters.AddWithValue("@birthDate", user.birthDate);
                    cmd.Parameters.AddWithValue("@city", user.city);
                    cmd.Parameters.AddWithValue("@password", user.password);
                    cmd.Parameters.AddWithValue("@userType", user.userType);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT SCOPE_IDENTITY()", con);
                    int id = Convert.ToInt32(cmd.ExecuteScalar());

                    return id;
                }
            }
            catch (Exception ex)
            {
                // Log the exception here.
                Console.WriteLine(ex.ToString());

                // Return an appropriate response to the caller.
                throw new Exception("Error creating user.", ex);
            }
        }


        public static void UpdateUser(Users user)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Users SET firstName=@firstName, lastName=@lastName, username=@username, email=@email, " +
                    "phoneNumber=@phoneNumber, birthDate=@birthDate, city=@city, password=@password, userType=@userType " +
                    "WHERE Id=@id",
                    con);

                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.Parameters.AddWithValue("@firstName", user.firstName);
                cmd.Parameters.AddWithValue("@lastName", user.lastName);
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@phoneNumber", user.phoneNumber);
                cmd.Parameters.AddWithValue("@birthDate", user.birthDate);
                cmd.Parameters.AddWithValue("@city", user.city);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@userType", user.userType);
                cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteUser(int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Users WHERE Id=@id",
                    con);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}

