using System;
using Lib.LearningManagementSys.Item;
using Lib.LearningManagementSys.Services;

namespace MAUI.LearningManagementSystem.ViewModels
{
	public class CourseDetailViewModel
	{
        private Course course;
        public int Id { get; set; }

        public CourseDetailViewModel()
        {
            course = new Course();
        }

        public string Name
        {
            get => course?.Name ?? string.Empty;
            set { if (course != null) course.Name = value; }
        }
        public string Description
        {
            get => course?.Description ?? string.Empty;
            set { if (course != null) course.Description = value; }
        }
        public string Code
        {
            get => course?.Code ?? string.Empty;
            set { if (course != null) course.Code = value; }
        }

        //public string CourseCode
        //{
        //    get => course?.Code ?? string.Empty;
        //}

        public void AddCourse(Shell s)
        {
            CourseService.Current.Add(course);
            s.GoToAsync("//Instructor");
        }
    }
}

