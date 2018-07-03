using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace lesson_webapi.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int CVV { get; set; }
    }
}