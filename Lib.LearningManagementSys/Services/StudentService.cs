using System;
using Lib.LearningManagementSys.People;

namespace Lib.LearningManagementSys.Services
{
	public class StudentService
	{
        private List<Person> studentList;

        private static StudentService? instance;

        private StudentService()
        {
            studentList = new List<Person>();

        }

        public static StudentService Current
        {
            get
            {
                if(instance == null)
                {
                    instance = new StudentService();
                }

                return instance;
            }
        }

        public void Add(Person student)
		{
			studentList.Add(student);
		}

        public List<Person> Students
        {
            get
            {
                return studentList;
            }
        }

        public IEnumerable<Person> SearchStudents(string name)
        {
            return studentList.Where(s => s.Name.ToUpper().Contains(name.ToUpper()));
        }
	}
}

