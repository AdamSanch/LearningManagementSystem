using System;
using Lib.LearningManagementSys.Item;
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

        public void Add(Person person)
		{
			studentList.Add(person);
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
        public Person? GetPerson(int id)
        {
            return studentList.FirstOrDefault(p => p.Id == id);
        }
        public void UpdateAddGrade(Student s, Course c)
        {
            double finalGrade = 0;
            int AssignGroupLeftover = 100;
            foreach (var AG in c.AssignmentGroups)
            {
                double assiGroupGrade = 0;
                int numOfSubmissions = 0;
                foreach (var A in AG.Assignments)
                {
                    var submission = A.Submissions.FirstOrDefault(x => x.Student.Id == s.Id);
                    if (submission != null)
                    {
                        if (numOfSubmissions == 0) { AssignGroupLeftover = AssignGroupLeftover - AG.Weight; }
                        numOfSubmissions++;
                        assiGroupGrade = assiGroupGrade + (submission.Grade / A.TotalAvailablePoints);
                    }
                }
                if (numOfSubmissions != 0) { finalGrade = finalGrade + ((assiGroupGrade / numOfSubmissions) * AG.Weight); }
                //if (numOfSubmissions == 0) { grade = grade + AG.Weight; }
                //else { grade = grade + ((grade / numOfSubmissions) * AG.Weight); }
            }
            finalGrade = finalGrade + AssignGroupLeftover;
            if (finalGrade > 100) { finalGrade = 100; }

            if (!s.Grades.Any(s => s.Key == c.Code))
            {
                s.Grades.Add(c.Code, finalGrade);
            }
            else
            {
                s.Grades[c.Code] = finalGrade;
            }
        }
	}
}

