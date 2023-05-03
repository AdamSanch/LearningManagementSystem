using System;
using Lib.LearningManagementSys.Database;
using Lib.LearningManagementSys.Item;

namespace Lib.LearningManagementSys.Services
{
	public class CourseService
	{
		//private List<Course> CourseList;

		private static CourseService? instance;

        private CourseService()
        {
			//CourseList = new List<Course>();
        }

		public static CourseService Current
		{
			get
			{
				if(instance == null)
				{
					instance = new CourseService();
				}

				return instance;
			}
		}


        public void Add(Course course)
		{
            FakeDatabase.Courses.Add(course);
		}

		public List<Course> Courses
		{
			get
			{
				return FakeDatabase.Courses;
			}
		}

		public IEnumerable<Course> SearchCourses(string query)
		{
			return Courses.Where(s => s.Name.ToUpper().Contains(query.ToUpper())
				|| s.Description.ToUpper().Contains(query.ToUpper()));
        }
		public Course? FindCourse(string codeQuery)
		{
			return FakeDatabase.Courses.FirstOrDefault(c => c.Code == codeQuery);
		}
	}
}

