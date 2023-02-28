using System;
namespace Lib.LearningManagementSys.People
{
	public class Person
	{
        public string Name { get; set; }

        public Person()
		{
			Name = String.Empty;
		}

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}

