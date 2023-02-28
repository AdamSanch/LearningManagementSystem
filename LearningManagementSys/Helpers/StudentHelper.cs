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

        public void CreateStudentRecord(Student? updateStudent = null)
		{
            bool isNew = false;
            if (updateStudent == null)
            {
                isNew = true;
                updateStudent = new Student();
            }

            Console.WriteLine("Enter student name:");
            var name = Console.ReadLine();

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

            updateStudent.Name = name ?? string.Empty;
            updateStudent.Classification = classEnum;

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

