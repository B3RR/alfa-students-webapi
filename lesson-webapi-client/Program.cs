using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using lesson_webapi_client.Core;
using lesson_webapi_client.Models;

namespace lesson_webapi_client
{
    class Program
    {
        private const string address = "http://localhost:8500";
        private const string apiStudents = "api/students";
        private const string apiCard= "api/card";
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(address);
            var studentService = new StudentService(client);
            var students = await studentService.GetStudentsAsync(apiStudents);
            foreach (var stud in students)
            {
                Console.WriteLine($"ID: {stud.Id} ФИО: {stud.ToString()}");
            }
            var student = students.Where(x => x.Id == 2).FirstOrDefault();
            if (student != null)
            {
                student.Surname = "Молчанов";
                student.Firstname = "Николай";
                student.Middlename = "Валерьевич";
                var updateStudent = await studentService.PutStudentsAsync(apiStudents, student.Id, student);
                if (updateStudent != null)
                {
                    Console.WriteLine($"ID: {updateStudent.Id} ФИО: {updateStudent.ToString()}");
                }
            }
            var cardService=new CardService(client);
             var card="1234567819910126";
             for (var i=0;i<10;i++)
             {
                 await cardService.PutCardAsync(apiCard,i,card);
             }
            var card2="1234567819910127";
            for (var i=0;i<35;i++)
            {
                await cardService.PutCardAsync(apiCard,i,card2);
            }
            
        }
    }
}
