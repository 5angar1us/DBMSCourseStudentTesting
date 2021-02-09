using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMSCourseStudentTesting.CLasses
{
    public class User
    {
        public string Login { private set; get; }
        public UserRole Role { private set; get; }

        public User(string login, string role)
        {
            Login = login;
            Role = GetRole(role);
        }

        private UserRole GetRole(string role)
        {
            UserRole userRole = UserRole.None;
            if (role.Equals("student")) userRole =  UserRole.Student;
            if (role.Equals("teacher")) userRole = UserRole.Teacher;
            if (role.Equals("db_owner")) userRole = UserRole.Admin;
            return userRole;
        }
    }
}
