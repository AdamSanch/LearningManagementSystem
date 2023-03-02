using System;
using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;

namespace LearningManagementSys.Helpers
{
	public class StudentHelper
	{
        private StudentService studentService;
        private CourseService courseService;

        public StudentHelper()
        {
            studentService = StudentService.Current;
            courseService = CourseService.Current;
        }

        public void CreateStudentRecord(Person? updateStudent = null)
		{
            bool isNew = false;
            if (updateStudent == null)
            {
                isNew = true;

                Console.WriteLine("Is this person a:");
                Console.WriteLine("(S)tudent?");
                Console.WriteLine("(T)A?");
                Console.WriteLine("(I)nstructor?");
                var personType = Console.ReadLine() ?? String.Empty;
                if (personType.Equals("S", StringComparison.InvariantCultureIgnoreCase))
                {
                    updateStudent = new Student();
                }
                else if (personType.Equals("T", StringComparison.InvariantCultureIgnoreCase))
                {
                    updateStudent = new TeachingAssistant();
                }
                else if (personType.Equals("I", StringComparison.InvariantCultureIgnoreCase))
                {
                    updateStudent = new Instructor();
                }
                else
                {
                    return;
                }

            }

            Console.WriteLine("Enter their name:");
            var name = Console.ReadLine() ?? string.Empty;
            //Console.WriteLine("Enter their ID:");
            //var id = Console.ReadLine() ?? string.Empty;
            if  (updateStudent is Student)
            {
                Console.WriteLine("Enter student year ( (F)reshman, s(O)phmore, (J)unior, (S)enior ):");
                var classification = Console.ReadLine() ?? string.Empty;
                PersonClassification classEnum = PersonClassification.Freshman;
                if (classification.Equals("O", StringComparison.InvariantCultureIgnoreCase))
                {
                    classEnum = PersonClassification.Sophmore;
                }
                else if (classification.Equals("J", StringComparison.InvariantCultureIgnoreCase))
                {
                    classEnum = PersonClassification.Junior;
                }
                else if (classification.Equals("S", StringComparison.InvariantCultureIgnoreCase))
                {
                    classEnum = PersonClassification.Senior;
                }
                (updateStudent as Student).Classification = classEnum;
            }
            

            updateStudent.Name = name;
            //updateStudent.Id = int.Parse(id ?? "0");

            if (isNew)
            {
                studentService.Add(updateStudent);
            }     
        }

        public void UpdateStudent()
        {
            Console.WriteLine("Enter the name of the student to update:");
            ListStudents();
            var name = Console.ReadLine() ?? string.Empty;

            var selectedStudent = studentService.Students.FirstOrDefault(s => s.Name == name);
            if (selectedStudent != null)
            {
                CreateStudentRecord(selectedStudent);
            }
        }

        public void ListStudents()
        {
            studentService.Students.ForEach(Console.WriteLine);
        }

        public void SearchStudents()
        {
            Console.WriteLine("Enter student name:");
            var name = Console.ReadLine() ?? string.Empty;

            studentService.SearchStudents(name).ToList().ForEach(Console.WriteLine);
            Console.WriteLine("Student's Courses:");
            courseService.Courses.Where(s => s.Roster.Any(s2 => s2.Name.ToUpper() == name.ToUpper())).ToList().ForEach(Console.WriteLine);
        }
	}
}

