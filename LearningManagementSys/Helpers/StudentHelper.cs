using System;
using System.Xml.Linq;
using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;

namespace LearningManagementSys.Helpers
{
	public class StudentHelper
	{
        private StudentService studentService;
        private CourseService courseService;
        private ListNavigator<Person> studentNavigator;

        public StudentHelper()
        {
            studentService = StudentService.Current;
            courseService = CourseService.Current;
            studentNavigator = new ListNavigator<Person>(studentService.Students, 5);
        }

        public void CreateStudentRecord(Person? updateStudent = null)
		{
            bool isNew = false;

            if (updateStudent == null)
            {
                isNew = true;

                Console.WriteLine("Is this person a");
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
                else { return; }
            }

            Console.WriteLine("Enter their name:");
            var name = Console.ReadLine() ?? string.Empty;
            //Console.WriteLine("Enter their ID:");
            //var id = Console.ReadLine() ?? string.Empty;

            if (updateStudent is Student)
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
            studentService.Students.ForEach(Console.WriteLine);
            var name = Console.ReadLine() ?? string.Empty;

            var selectedStudent = studentService.Students.FirstOrDefault(s => s.Name == name);
            if (selectedStudent != null)
            {
                CreateStudentRecord(selectedStudent);
            }
        }

        private void NavigateStudents()
        {
            bool cont = true;
            ListNavigator<Person> currentNavigator = studentNavigator;
            while (cont)
            {
                Console.WriteLine("---------------------------------");
                foreach (var pair in currentNavigator.GetCurrentPage())
                {
                    Console.WriteLine($"{pair.Key}. {pair.Value}");
                }
                Console.WriteLine("---------------------------------");

                //Console.WriteLine("Choose from the following options");
                if (currentNavigator.HasPreviousPage)
                {
                    Console.WriteLine("* (A)-previous page");
                }
                if (currentNavigator.HasNextPage)
                {
                    Console.WriteLine("* (D)-next page");
                }
                Console.WriteLine("* (Q)-quit\n* ID to print student info");
                var input = Console.ReadLine() ?? string.Empty;

                if (input.Equals("A", StringComparison.InvariantCultureIgnoreCase))
                {
                    currentNavigator.GoBackward();
                }
                else if(input.Equals("D", StringComparison.InvariantCultureIgnoreCase))
                {
                    currentNavigator.GoForward();
                }
                else if(input.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    cont = false;
                }
                else
                {
                    var inputID = int.Parse(input ?? "0");
                    var selStudent = currentNavigator.GetCurrentPage().FirstOrDefault(n => n.Key == inputID).Value ?? null;
                    if (selStudent != null)
                    {
                        Console.WriteLine(selStudent);
                        if (selStudent is Student && (selStudent as Student).Grades.Any())
                        {
                            var str = $"GPA - {String.Format("{0:0.00}", studentService.CalculateGPA(selStudent))}";
                            str = str + "\nCourses:";
                            foreach (var g in (selStudent as Student).Grades)
                            {
                                str = str + $"\n{g.Key} - {g.Value.letterGrade}";
                            }
                            Console.WriteLine(str);
                        }
                        cont = false;
                    }
                }

            }
        }

        public void ListStudents()
        {
            NavigateStudents();
        }

        //public void PrintStudents()
        //{
        //    Console.WriteLine("Enter student id:");
        //    ListStudents();
        //    var id = Console.ReadLine() ?? string.Empty;

        //    var person = studentService.GetPerson(int.Parse(id ?? "0"));

        //    Console.WriteLine(person);
        //    if(person is Student)
        //    {
        //        var str = "Courses:";
        //        foreach (var g in (person as Student).Grades)
        //        {
        //            str = str + $"\n{g.Key} - {g.Value}";
        //        }
        //        Console.WriteLine(str);
        //    }
        //    //Console.WriteLine("Student's Courses");
        //    //courseService.Courses.Where(s => s.Roster.Any(s2 => s2.Name.ToUpper() == name.ToUpper())).ToList().ForEach(Console.WriteLine);
        //}

        //Temp function
        public void AddPerson (Person p)
        {
            studentService.Add(p);
        }
	}
}

