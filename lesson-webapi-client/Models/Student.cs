using System;

namespace lesson_webapi_client.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? isDel { get; set; }

        public override string ToString()
        {
            return $"{Surname} {Firstname} {Middlename}".Trim();
        }
    }
}