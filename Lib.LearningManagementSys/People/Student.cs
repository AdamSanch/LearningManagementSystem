using System;
namespace Lib.LearningManagementSys.People
{
	public class Student : Person
	{
		public Student()
		{
            Grades = String.Empty;
        }

        public PersonClassification Classification { get; set; }

        public string Grades { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Classification}";
        }
    }

    public enum PersonClassification
    {
        Freshman, Sophmore, Junior, Senior
    }
}

