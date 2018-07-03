using System;

namespace lesson_di.Services
{
    public class RandomTransient
    {
        private int _value;
        public int Value => _value;

        public RandomTransient()
        {
            var rnd = new Random();
            _value = rnd.Next(1, 10000);
        }
    }
}