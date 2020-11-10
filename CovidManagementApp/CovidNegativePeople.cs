using System;
using System.Collections.Generic;
using System.Text;

namespace CovidManagementApp
{
    class CovidNegativePeople : People
    {
        public CovidNegativePeople(string name, int age, string gender) : base(name, age, gender){}

        public override void Display()
        {
            Console.WriteLine("COVID-19 negative or inconclusive people:");
            base.Display();
        }
    }
}
