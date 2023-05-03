using System;
using Lib.LearningManagementSys.Item;
using Lib.LearningManagementSys.People;

namespace Lib.LearningManagementSys.Database
{
	public static class FakeDatabase
	{
        private static List<Person> people = new List<Person>();
        private static List<Course> courses = new List<Course>();
        public static List<Person> People
        {
            get
            {
                return people;
            }
        }

        public static List<Course> Courses
        {
            get
            {
                return courses;
            }
        }
    }
}

