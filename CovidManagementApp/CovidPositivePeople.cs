using System;
using System.Collections.Generic;
using System.Text;

namespace CovidManagementApp
{
    class CovidPositivePeople : People
    {
        public CovidPositivePeople(string name, int age, string gender) : base(name, age, gender) { }

        public override void Display()
        {
            Console.WriteLine("COVID-19 positive people:");
            base.Display();
        }
    }
}
