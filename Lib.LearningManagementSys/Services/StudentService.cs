using System;
using Lib.LearningManagementSys.People;

namespace Lib.LearningManagementSys.Services
{
	public class StudentService
	{
        private List<Student> studentList;

        private static StudentService? instance;

        private StudentService()
        {
            studentList = new List<Student>();

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

        public void Add(Student student)
		{
			studentList.Add(student);
		}

        public List<Student> Students
        {
            get
            {
                return studentList;
            }
        }

        public IEnumerable<Student> SearchStudents(string name)
        {
            return studentList.Where(s => s.Name.ToUpper().Contains(name.ToUpper()));
        }
	}
}

