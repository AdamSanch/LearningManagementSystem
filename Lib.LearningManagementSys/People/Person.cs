using System;
namespace Lib.LearningManagementSys.People
{
	public class Person
	{
        private static int id = 1;
        public string Name { get; set; }

        public int Id { get; }

        public Person()
		{
			Name = String.Empty;
            Id = id++;
		}

        public override string ToString()
        {
            return $"({Id})-{Name}";
        }
    }
}

