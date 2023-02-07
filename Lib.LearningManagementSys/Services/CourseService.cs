using System;
using Lib.LearningManagementSys.Item;

namespace Lib.LearningManagementSys.Services
{
	public class CourseService
	{
		private List<Course> CourseList = new List<Course>();

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
		public CourseService()
		{
		}
	}
}

