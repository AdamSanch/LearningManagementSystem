using System;
using System.Xml.Linq;
using Lib.LearningManagementSys.Item;
using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;


namespace LearningManagementSys.Helpers
{
	public class CourseHelper
	{
		private CourseService courseService = new CourseService();
        private StudentService studentService;

        public CourseHelper()
        {
            studentService = StudentService.Current;
        }

        public void CreateCourseRecord(Course? updateCourse = null)
        {
            bool isNew = false;
            bool newInfo = false;
            if (updateCourse == null)
            {
                isNew = true;
                updateCourse = new Course();
            }
            else
            {
                Console.WriteLine("Would you like to update the course information? (Y or N)");
                var YorN = Console.ReadLine() ?? string.Empty;
                if(YorN.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                {
                    newInfo = true;
                }
            }
            if (isNew || newInfo)
            {
                Console.WriteLine("Enter the course name:");
                var name = Console.ReadLine();
                Console.WriteLine("Enter the course code:");
                var code = Console.ReadLine();
                Console.WriteLine("What is the course description?:");
                var description = Console.ReadLine();

                updateCourse.Name = name ?? string.Empty;
                updateCourse.Code = code ?? string.Empty;
                updateCourse.Description = description ?? string.Empty;
            }


// Adding students to the Course

            Console.WriteLine("Give the name of a student you would like to add (Q to quit)");
            //var roster = updateCourse.Roster;
            var cont = true;
            while (cont)
            {
                studentService.Students.Where(s => !updateCourse.Roster.Any(s2 => s2.Name == s.Name)).ToList().ForEach(Console.WriteLine);
                var input = "Q";
                if(studentService.Students.Any(s => !updateCourse.Roster.Any(s2 => s2.Name == s.Name)))
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

                Console.WriteLine("-------------------");
                updateCourse.Roster.ForEach(Console.WriteLine);
                Console.WriteLine("-------------------");
            }
            

            if (isNew)
            {
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
                CreateCourseRecord(selectedCourse);
            }
        }

        public void SearchCourses()
        {
            Console.WriteLine("Enter the name/description of the course your looking for:");
            ListCourses();
            var query = Console.ReadLine() ?? string.Empty;

            courseService.SearchCourses(query).ToList().ForEach(s => s.PrintCourseFull());
        }

        public void ListCourses()
        {
            courseService.Courses.ForEach(Console.WriteLine);
        }
	}
}

