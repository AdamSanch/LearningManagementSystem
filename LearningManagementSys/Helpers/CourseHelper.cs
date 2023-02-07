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

        public void CreateCourseRecord(Course? updateCourse = null)
        {
			Console.WriteLine("Enter the course name:");
			var name = Console.ReadLine();
            Console.WriteLine("Enter the course code:");
            var code = Console.ReadLine();
            Console.WriteLine("What is the course description?:");
            var description = Console.ReadLine();

            bool isNew = false;
            if (updateCourse == null)
            {
                isNew = true;
                updateCourse = new Course();
            }

            updateCourse.Name = name ?? string.Empty;
            updateCourse.Code = code ?? string.Empty;
            updateCourse.Description = description ?? string.Empty;

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
            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code == code);


            if (selectedCourse != null)
            {
                CreateCourseRecord(selectedCourse);
            }
        }

        public void SearchCourses()
        {
            Console.WriteLine("Enter either the name or description of the course");
            var query = Console.ReadLine() ?? string.Empty;

            courseService.SearchCourses(query).ToList().ForEach(Console.WriteLine);
        }

        public void ListCourses()
        {
            courseService.Courses.ForEach(Console.WriteLine);
        }
        public CourseHelper()
		{
		}
	}
}

