using System;
using System.Collections.Generic;
using System.Text;

namespace CovidManagementApp
{
    class CovidSelfIzolatePatient : People
    {
        public CovidSelfIzolatePatient(string name, int age, string gender) : base(name, age, gender) { }
    }
}
