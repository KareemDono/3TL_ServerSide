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
        static SqlConnection con = new SqlConnection(conStr);

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


        public static List<Users> GetUsersGreaterThenId(int id)
        {
            List<Users> userList = new List<Users>();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    " SELECT * " +
                    " FROM Users " +
                    " WHERE id > " + id, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    userList.Add(new Users()
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

            return userList;
        }


        public static Users GetUserById(int id)
        {
            return GetUsersByQuery(
                " SELECT * " +
                " FROM Users " +
                " WHERE Id=" + id)[0];
        }
    }
}