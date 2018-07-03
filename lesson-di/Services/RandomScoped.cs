using System;

namespace lesson_di.Services
{
    public class RandomScoped 
    {
        private int _value;
        public int Value => _value;

        public RandomScoped()
        {
            var rnd = new Random();
            _value = rnd.Next(1, 10000);
        }
    }
}