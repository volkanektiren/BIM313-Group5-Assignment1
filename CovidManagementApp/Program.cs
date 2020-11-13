using System;
using System.Collections.Generic;
using System.Linq;

namespace CovidManagementApp
{
    class Program
    {
        //Generate patient list
        static List<People> GeneratePatientList(int females, int males)
        {
            #region Generate People list
            List<People> patientPeopleList = new List<People>();
            string name;
            int age, nameIndex, surnameIndex;
            string[] maleNameArr = { "James", "John", "Robert", "Michael", "William", "David", "Richard", "Charles", "Joseph", "Thomas" };
            string[] femaleNameArr = { "Mary", "Patricia", "Linda", "Barbara", "Elizabeth", "Jennifer", "Susan", "Margaret", "Lisa", "Nancy" };

            string[] surnameArr = { "Adams", "Allen", "Anderson", "Atkins", "Baker", "Barnes", "Bell", "Bennet", "Cooper", "Forester", "Foster", "Fox", "Gardener", "Hamilton", "Harris", "Marshall", "Murphy", "Parker", "Richardson", "Simpson"};
            //Generate Females
            age = 10;
            for (int i = 0; i < females; i++)
            {
                nameIndex = i % femaleNameArr.Count();
                surnameIndex = i % surnameArr.Count();
                name = femaleNameArr[nameIndex] + " " + surnameArr[surnameIndex];
                age++;
                if (age > 80)
                {
                    age = 10;
                }
                People newPeople = new People(name, age, "female");
                patientPeopleList.Add(newPeople);
            }
            //Generate Males
            age = 80;
            for (int i = 0; i < males; i++)
            {
                nameIndex = i % maleNameArr.Count();
                surnameIndex = i % surnameArr.Count();
                name = maleNameArr[nameIndex] + " " + surnameArr[surnameIndex];
                age--;
                if (age < 10)
                {
                    age = 80;
                }
                People newPeople = new People(name, age, "male");
                patientPeopleList.Add(newPeople);
            }
            //Order people by age
            patientPeopleList = patientPeopleList.OrderBy(p => p.GetAge())
            .ToList();
            return patientPeopleList;
            #endregion
        }

        //Checks given rate inputs if it is between 0 and 100 from user 
        static void RateInputControl(out int value)
        {
            while (true)
            {
                Console.Write("Enter a rate between 0 and 100: ");
                value = Convert.ToInt32(Console.ReadLine());
                if (value >= 0 && value <= 100) break;
                else
                {
                    Console.WriteLine("It should be between 1 and 100.");
                    Console.WriteLine("Please try again!\n");
                }
            }
        }
        static void Main(string[] args)
        {
            int females, males;

            Console.Write("Enter number of females: ");
            females = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter number of males: ");
            males = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("High risk exposure rate input");
            RateInputControl(out int high_risk_exposure_rate);

            Console.WriteLine("High risk exposure symptoms rate input");
            RateInputControl(out int high_risk_exposure_symptoms_rate);

            Console.WriteLine("Low risk exposure symptoms rate input");
            RateInputControl(out int low_risk_exposure_symptoms_rate);

            Console.WriteLine("Laboratory testing positive rate input");
            RateInputControl(out int laboratory_testing_positive_rate);

            List<People> patientPeopleList = GeneratePatientList(females, males); //Generate patients
            List<HighRiskCovidPatient> highRiskPatientPeopleList = new List<HighRiskCovidPatient>(); //High risk exposure patients
            List<LowRiskCovidPatient> lowRiskPatientPeopleList = new List<LowRiskCovidPatient>(); //Low risk exposure patients
            List<CovidSelfIzolatePatient> covidSelfIzolatePeopleList = new List<CovidSelfIzolatePatient>(); //COVID-19 self isolate people
            List<CovidNegativePeople> covidNegativePeopleList = new List<CovidNegativePeople>(); //COVID-19 negative or inconclusive people
            List<CovidPositivePeople> covidPossitivePeopleList = new List<CovidPositivePeople>(); //COVID-19 positive people

            Console.WriteLine("Number of patients is {0}", patientPeopleList.Count);
            foreach (People patient in patientPeopleList) { patient.Display(); } //Patients List

            Random rand = new Random();

            #region Generate highRiskPatientPeopleList with high_risk_exposure_rate, sort it by age and display its number and patients
            int numberOfPatients = patientPeopleList.Count;
            int numberOfHighRiskPatients = (int)Math.Round((double) high_risk_exposure_rate / 100 * numberOfPatients, MidpointRounding.AwayFromZero);
            for (int i = 0; i < numberOfHighRiskPatients; i++)
            {
                int index = rand.Next(patientPeopleList.Count);
                People person = patientPeopleList[index];
                highRiskPatientPeopleList.Add(new HighRiskCovidPatient(person.GetName(), person.GetAge(), person.GetGender()));
                patientPeopleList.Remove(person);
            }

            highRiskPatientPeopleList = highRiskPatientPeopleList.OrderBy(p => p.GetAge()).ToList();

            Console.WriteLine("Number of high risk covid patients is {0}", highRiskPatientPeopleList.Count);

            foreach (HighRiskCovidPatient patient in highRiskPatientPeopleList){ patient.Display(); }
            #endregion

            #region Generate lowRiskPatientPeopleList with high_risk_exposure_rate, sort it by age and display its number and patients
            foreach (People person in patientPeopleList)
            {
                lowRiskPatientPeopleList.Add(new LowRiskCovidPatient(person.GetName(), person.GetAge(), person.GetGender()));
            }

            lowRiskPatientPeopleList = lowRiskPatientPeopleList.OrderBy(p => p.GetAge()).ToList();

            Console.WriteLine("Number of low risk covid patients is {0}", lowRiskPatientPeopleList.Count);

            foreach (LowRiskCovidPatient patient in lowRiskPatientPeopleList) { patient.Display(); }
            #endregion

            #region Generate covidSelfIzolatePeopleList with symptoms rates. symptoms -> yes
            //Generate from highRiskPatientPeopleList
            int numberOfHighRiskWithSymptoms = (int)Math.Round((double) high_risk_exposure_symptoms_rate / 100 * numberOfHighRiskPatients, MidpointRounding.AwayFromZero);
            for (int i = 0; i < numberOfHighRiskWithSymptoms; i++)
            {
                int index = rand.Next(highRiskPatientPeopleList.Count);
                HighRiskCovidPatient person = highRiskPatientPeopleList[index];
                covidSelfIzolatePeopleList.Add(new CovidSelfIzolatePatient(person.GetName(), person.GetAge(), person.GetGender()));
                highRiskPatientPeopleList.Remove(person);
            }

            //Generate from lowRiskPatientPeopleList
            int numberOfLowRiskWithSymptoms = (int)Math.Round((double)low_risk_exposure_symptoms_rate / 100 * lowRiskPatientPeopleList.Count, MidpointRounding.AwayFromZero);
            for (int i = 0; i < numberOfLowRiskWithSymptoms; i++)
            {
                int index = rand.Next(lowRiskPatientPeopleList.Count);
                LowRiskCovidPatient person = lowRiskPatientPeopleList[index];
                covidSelfIzolatePeopleList.Add(new CovidSelfIzolatePatient(person.GetName(), person.GetAge(), person.GetGender()));
                lowRiskPatientPeopleList.Remove(person);
            }

            covidSelfIzolatePeopleList = covidSelfIzolatePeopleList.OrderBy(p => p.GetAge()).ToList();

            Console.WriteLine("Number of self izolated covid patients is {0}", covidSelfIzolatePeopleList.Count);

            foreach (CovidSelfIzolatePatient patient in covidSelfIzolatePeopleList) { patient.Display(); }
            #endregion

            #region Generate covid negative patients. symptoms -> no & laboratory test -> no
            //symptoms -> no
            foreach (HighRiskCovidPatient person in highRiskPatientPeopleList)
            {
                covidNegativePeopleList.Add(new CovidNegativePeople(person.GetName(), person.GetAge(), person.GetGender()));
            }

            foreach (LowRiskCovidPatient person in lowRiskPatientPeopleList)
            {
                covidNegativePeopleList.Add(new CovidNegativePeople(person.GetName(), person.GetAge(), person.GetGender()));
            }

            //laboratory test -> no
            int numberOfCovidNegativeWithSymptoms = (int)Math.Round((double)(100 - laboratory_testing_positive_rate) / 100 * covidSelfIzolatePeopleList.Count, MidpointRounding.AwayFromZero);
            for (int i = 0; i < numberOfCovidNegativeWithSymptoms; i++)
            {
                int index = rand.Next(covidSelfIzolatePeopleList.Count);
                CovidSelfIzolatePatient person = covidSelfIzolatePeopleList[index];
                covidNegativePeopleList.Add(new CovidNegativePeople(person.GetName(), person.GetAge(), person.GetGender()));
                covidSelfIzolatePeopleList.Remove(person);
            }

            covidNegativePeopleList = covidNegativePeopleList.OrderBy(p => p.GetAge()).ToList();

            Console.WriteLine("Number of covid negative patients is {0}", covidNegativePeopleList.Count);

            foreach (CovidNegativePeople patient in covidNegativePeopleList) { patient.Display(); }
            #endregion

            #region Generate covid positive patients. laboratory test -> yes
            foreach (CovidSelfIzolatePatient person in covidSelfIzolatePeopleList)
            {
                covidPossitivePeopleList.Add(new CovidPositivePeople(person.GetName(), person.GetAge(), person.GetGender()));
            }

            covidPossitivePeopleList = covidPossitivePeopleList.OrderBy(p => p.GetAge()).ToList();

            Console.WriteLine("Number of covid positive patients is {0}", covidPossitivePeopleList.Count);

            foreach (CovidPositivePeople patient in covidPossitivePeopleList) { patient.Display(); }
            #endregion
        }
    }
}
