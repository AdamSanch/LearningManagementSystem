using System;
namespace Lib.LearningManagementSys.People
{
	public class Student : Person
	{
		//public double GPA { get; set; }

        public PersonClassification Classification { get; set; }

        public Dictionary<string, double> Grades { get; set; }

        public Student()
        {
            Grades = new Dictionary<string, double>();
        }

        public override string ToString()
        {
            string s = $"({Id})-{Name}: Student, {Classification}";
            foreach(var g in Grades)
            {
                s = s + $"\n{g.Key}-{g.Value}";
            }
                return s;
        }
    }

    public enum PersonClassification
    {
        Freshman, Sophmore, Junior, Senior
    }
}

