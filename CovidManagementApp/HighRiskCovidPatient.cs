using System;
using System.Collections.Generic;
using System.Text;

namespace CovidManagementApp
{
    class HighRiskCovidPatient : People
    {
        public HighRiskCovidPatient(string name, int age, string gender) : base(name, age, gender) { }
    }
}
