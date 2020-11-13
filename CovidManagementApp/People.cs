using System;
using System.Collections.Generic;
using System.Text;

namespace CovidManagementApp
{
    class People
    {
        private string name;
        private int age;
        private string gender;

        public People(string name, int age, string gender)
        {
            this.name = name;
            this.age = age;
            this.gender = gender;
        }

        public virtual void Display()
        {
            Console.WriteLine("(Name: {0}) - (Gender: {1}) - (Age: {2})", GetName(), GetGender(), GetAge());
            Console.WriteLine(new String('+', 60));
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public int GetAge()
        {
            return age;
        }

        public void SetAge(int age)
        {
            this.age = age;
        }

        public string GetGender()
        {
            return gender;
        }

        public void SetGender(string gender)
        {
            this.gender = gender;
        }
    }
}
