using CeilUfas.models;

namespace CeilUfas
{
public static class Globals
{public const string Admin = "Admin";
        public const string Student = "Student"; 
        public const string Teacher = "Teacher";

        public static readonly string[] RoleNames = [Admin, Student, Teacher];
        public static AppSettings AppSettings { get; set; }
   
}
}