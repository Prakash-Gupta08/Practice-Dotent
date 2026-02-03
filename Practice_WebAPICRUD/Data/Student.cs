using System.ComponentModel.DataAnnotations;

namespace Practice_WebAPICRUD.Data
{
    public class Student
    {
        [Key]
        public int student_id { get; set; }
        public string student_name { get; set; }
        public int student_std { get; set; }
        public int student_marks { get; set; }
        public string student_grade { get; set; }
    }
}
