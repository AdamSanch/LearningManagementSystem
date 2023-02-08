using System;
using Lib.LearningManagementSys.Item;

namespace Lib.LearningManagementSys.Services
{
	public class CourseService
	{
		private List<Course> CourseList;

		private static CourseService? instance;

        private CourseService()
        {
			CourseList = new List<Course>();
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
			CourseList.Add(course);
		}

		public List<Course> Courses
		{
			get
			{
				return CourseList;
			}
		}

		public IEnumerable<Course> SearchCourses(string query)
		{
			return Courses.Where(s => s.Name.ToUpper().Contains(query.ToUpper())
				|| s.Description.ToUpper().Contains(query.ToUpper()));
        }
	}
}

