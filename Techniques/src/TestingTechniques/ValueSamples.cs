using System;
using System.Collections.Generic;
using System.Text;

namespace TestingTechniques
{
    public class ValueSamples
    {
        public string FullName = "Okan Akın";
        public int Age = 23;
        public DateOnly DateOfBirth = new DateOnly(2002, 11, 17);

        public User AppUser = new()
        {
            FullName = "Okan Akın",
            Age = 23,
            DateOfBirth = new(2002, 11, 17)
        };

        public IEnumerable<User> Users = new[]
        {
            new User
            {
                FullName = "Okan Akın",
                Age = 23,
                DateOfBirth = new(2002, 11, 17)
            },
            new User
            {
                FullName = "Ali Akın",
                Age = 30,
                DateOfBirth = new(1993, 5, 10)
            },
            new User
            {
                FullName = "Mehmet Akın",
                Age = 43,
                DateOfBirth = new(1995, 8, 25)
            }
        };

        public IEnumerable<int> Numbers = new[] { 5, 10, 25, 50};
        public float Divide (int a, int b)
        {
            EnsureThatDivisorIsNotZero(a);
            EnsureThatDivisorIsNotZero(b);

            return a / b;
        }
        private void EnsureThatDivisorIsNotZero(int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }
        }
        public event EventHandler ExampleEvent;
        public virtual void RaiseExampleEvent()
        {
            ExampleEvent(this, EventArgs.Empty);
        }
        internal int InternalSecretNumber = 42;
    }
}
