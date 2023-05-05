using System;
using System.Collections.ObjectModel;
using Lib.LearningManagementSys.Item;
using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;
//using static Android.App.DownloadManager;

namespace MAUI.LearningManagementSystem.ViewModels
{
	public class CourseDetailViewModel
	{
        private Course course;
        public int Id { get; set; }
        public string ClassificationString { get; set; }

        private bool EditCourse { get; set; }

        public ObservableCollection<Person> Roster
        {
            get
            {
                return new ObservableCollection<Person>(course.Roster);
            }
        }

        public CourseDetailViewModel(Course c = null)
        {
            if(c == null)
            {
                course = new Course();
                EditCourse = false;
            }
            else
            {
                course = c;
                EditCourse = true;
            }
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

        public string Room
        {
            get => course?.Room ?? string.Empty;
            set { if (course != null) course.Room = value; }
        }

        private SemesterClassification StringToClass(string s)
        {
            SemesterClassification classification;
            switch (s)
            {
                case "S":
                    classification = SemesterClassification.Spring;
                    break;
                case "U":
                    classification = SemesterClassification.Summer;
                    break;
                case "F":
                default:
                    classification = SemesterClassification.Fall;
                    break;
            }

            return classification;
        }

        //public string CourseCode
        //{
        //    get => course?.Code ?? string.Empty;
        //}

        public void AddCourse(Shell s)
        {
            course.Classification = StringToClass(ClassificationString);

            if (!EditCourse)
            {
                CourseService.Current.Add(course);
            }
            s.GoToAsync("//Instructor");
        }
    }
}

