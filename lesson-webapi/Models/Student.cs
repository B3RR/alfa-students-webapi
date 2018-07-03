using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace lesson_webapi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? isDel { get; set; }
        [JsonIgnore]
        public string IpAddress { get; set; }
    }
}