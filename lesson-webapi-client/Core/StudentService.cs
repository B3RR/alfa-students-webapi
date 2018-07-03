using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using lesson_webapi_client.Models;
using System.Text;

namespace lesson_webapi_client.Core
{
    public class StudentService

    {
        private HttpClient _client { get; }
        public StudentService(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync(string path)
        {
            var list = new List<Student>();
            var response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<List<Student>>();
            }
            return list;
        }

        public async Task<Student> GetStudentAsync(string path,int id)
        {
            
            var response = await _client.GetAsync(path+"/"+id);
            if (response.IsSuccessStatusCode)
            {
                return  await response.Content.ReadAsAsync<Student>();
            }
            else
            {
                return null;
            }
            
        }

        public async Task<Student> PutStudentsAsync(string path, int id, Student student)
        {
            try
            {
                var response = await _client.PutAsJsonAsync<Student>(path + "/" + id, student);
                if (response.IsSuccessStatusCode)
                {
                    return await this.GetStudentAsync(path,id);
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    throw new Exception("Problems with PUT");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

    }

}