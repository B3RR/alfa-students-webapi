using System;

namespace lesson_di.Services
{
    public class RandomSingleton
    {
        private int _value;
        public int Value => _value;

        public RandomSingleton()
        {
            var rnd = new Random();
            _value = rnd.Next(1, 10000);
        }
    }
}