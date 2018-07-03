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
    public class CardService

    {
        private HttpClient _client { get; }
        public CardService(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        }


        public async Task<string> PutCardAsync(string path, int id, string number)
        {
            var response = await _client.PutAsJsonAsync<string>(path + "/" + id, number);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Number {number} CVV {id}");
            }
            return await response.Content.ReadAsStringAsync();
        }

    }

}