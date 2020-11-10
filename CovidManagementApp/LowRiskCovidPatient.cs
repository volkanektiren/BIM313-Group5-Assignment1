using System;
using System.Collections.Generic;
using System.Text;

namespace CovidManagementApp
{
    class LowRiskCovidPatient : People
    {
        public LowRiskCovidPatient(string name, int age, string gender) : base(name, age, gender) { }
    }
}
