using System;
using System.Xml.Linq;
using Lib.LearningManagementSys.Item;
using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;


namespace LearningManagementSys.Helpers
{
	public class CourseHelper
	{
		private CourseService courseService;
        private StudentService studentService;

        public CourseHelper()
        {
            courseService = CourseService.Current;
            studentService = StudentService.Current;
        }

        public void CreateUpdateCourseRecord(Course? updateCourse = null)
        {
            bool isNew = false;
            if (updateCourse == null)
            {
                isNew = true;
                updateCourse = new Course();
            }

            Console.WriteLine("Enter the course name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter the course code:");
            var code = Console.ReadLine();
            Console.WriteLine("What is the course description?:");
            var description = Console.ReadLine();

            updateCourse.Name = name ?? string.Empty;
            updateCourse.Code = code ?? string.Empty;
            updateCourse.Description = description ?? string.Empty;

            if (isNew)
            {
                AddToRoster(updateCourse);
                courseService.Add(updateCourse);
            }
        }

        public void UpdateCourse()
        {
            Console.WriteLine("Enter the code of the course to update:");
            ListCourses();
            var code = Console.ReadLine() ?? string.Empty;
            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.ToUpper() == code.ToUpper());

            if (selectedCourse != null)
            {
                Console.WriteLine("Enter a choice below");
                Console.WriteLine("1. Update Course Info");
                Console.WriteLine("2. Add an Assignment");
                Console.WriteLine("3. Add students to the roster");
                Console.WriteLine("4. Remove students from the roster");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if(result == 1)
                    {
                        CreateUpdateCourseRecord(selectedCourse);
                    }
                    else if(result == 2)
                    {
                        CreateAssignment(selectedCourse);
                    }
                    else if (result == 3)
                    {
                        AddToRoster(selectedCourse);
                    }
                    else if (result == 4)
                    {
                        RemoveFromRoster(selectedCourse);
                    }
                }
            }
        }

        private void AddToRoster(Course updateCourse)
        {
            Console.WriteLine("Give the name of a student you would like to add (Q to quit)");
            var cont = true;
            while (cont)
            {
                studentService.Students.Where(s => !updateCourse.Roster.Any(s2 => s2.Name == s.Name)).ToList().ForEach(Console.WriteLine);
                var input = "Q";
                if (studentService.Students.Any(s => !updateCourse.Roster.Any(s2 => s2.Name == s.Name)))
                {
                    input = Console.ReadLine() ?? string.Empty;
                }

                if (input.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                {
                    cont = false;
                }
                else
                {
                    var selectedStudent = studentService.Students.Where
                        (s => !updateCourse.Roster.Any(s2 => s2.Name == s.Name)).ToList().
                        FirstOrDefault(s => s.Name.ToUpper() == input.ToUpper());

                    if (selectedStudent != null)
                    {
                        updateCourse.Roster.Add(selectedStudent);
                    }
                }
            }
        }

        private void RemoveFromRoster(Course updateCourse)
        {
            Console.WriteLine("Give the name of a student you would like to remove (Q to quit)");
            var cont = true;
            while (cont)
            {
                updateCourse.Roster.ForEach(Console.WriteLine);
                var input = "Q";
                if (updateCourse.Roster.Count > 0)
                {
                    input = Console.ReadLine() ?? string.Empty;
                }

                if (input.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                {
                    cont = false;
                }
                else
                {
                    var selectedStudent = updateCourse.Roster.FirstOrDefault(s => s.Name.ToUpper() == input.ToUpper());
                        

                    if (selectedStudent != null)
                    {
                        updateCourse.Roster.Remove(selectedStudent);
                    }
                }
            }
        }

        private void CreateAssignment(Course updateCourse)
        {
            var assignment = new Assignment();
            Console.WriteLine("What is the assignment's name?");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the assignment description?");
            var description = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("How many points is the assignment worth?:");
            var points = Console.ReadLine();

            assignment.Name = name;
            assignment.Description = description;
            assignment.TotalAvailablePoints = int.Parse(points ?? "0");

            updateCourse.Assignments.Add(assignment);
        }

        public void SearchCourses()
        {
            Console.WriteLine("Enter the name/description of the course your looking for:");
            ListCourses();
            var query = Console.ReadLine() ?? string.Empty;

            courseService.SearchCourses(query).ToList().ForEach(s => Console.WriteLine($"{s.Name}({s.Code}) - {s.Description}\nRoster:"));
            courseService.SearchCourses(query).ToList().ForEach(s => s.Roster.ForEach(Console.WriteLine));
            Console.WriteLine("Current Assignemnts:");
            courseService.SearchCourses(query).ToList().ForEach(s => s.Assignments.ForEach(Console.WriteLine));
        }

        public void ListCourses()
        {
            courseService.Courses.ForEach(Console.WriteLine);
        }
	}
}

