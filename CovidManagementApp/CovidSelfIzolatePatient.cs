using System;
using System.Collections.Generic;
using System.Text;

namespace CovidManagementApp
{
    class CovidSelfIzolatePatient : People
    {
        public CovidSelfIzolatePatient(string name, int age, string gender) : base(name, age, gender) { }

        public override void Display()
        {
            Console.WriteLine("COVID-19 self-isolating patient:");
            base.Display();
        }
    }
}
