 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3TL.Models
{
    public enum UserRole { ADMIN, USER, SUPERUSER }
    static public class UsersBLL
    {
        static public Users[] GetAllUsersFromDB(UserRole ur)
        {
            Users[] users = null;
            if (ur == UserRole.ADMIN)
            {
                users = (Users[])UserDBServices.GetAllUsers();
            }
            else
            {
                users = UserDBServices.GetAllUsers().Where(user => user.Id != 1).ToArray();

            }
            return users;
        }

        public static Users GetUserById(int id)
        {
            Users user = UserDBServices.GetUserById(id);
            return user;
        }

        public static List<Users> GetUsersGreaterThenId(int id)
        {
            return UserDBServices.GetUsersGreaterThenId(id);
        }
    }
}