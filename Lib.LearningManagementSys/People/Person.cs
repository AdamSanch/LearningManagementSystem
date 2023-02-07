using System;
namespace Lib.LearningManagementSys.People
{
	public class Person
	{
        public string Name { get; set; }

        public PersonClassification Classification { get; set; }

        public string Grades { get; set; }

        public Person()
		{
			Name = String.Empty;

			Grades = String.Empty;

		}

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

