using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lib.LearningManagementSys.Item;
using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;

namespace MAUI.LearningManagementSystem.ViewModels
{
	public class StudentInternalViewViewModel : INotifyPropertyChanged
	{
        public Course SelectedCourse { get; set; }
        public Student SelectedStu { get; set; }

        public bool Fall { get; set; }
        public bool Spring { get; set; }
        public bool Summer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Course> Courses
        {
            get
            {
                if (Spring)
                {
                    return new ObservableCollection<Course>(CourseService.Current.Courses.Where(c => SelectedStu.Grades.Keys.Any(c2 => c2 == c && c2.Classification == SemesterClassification.Spring)));
                }
                else if (Summer)
                {
                    return new ObservableCollection<Course>(CourseService.Current.Courses.Where(c => SelectedStu.Grades.Keys.Any(c2 => c2 == c && c2.Classification == SemesterClassification.Summer)));
                }
                else
                {
                    return new ObservableCollection<Course>(CourseService.Current.Courses.Where(c => SelectedStu.Grades.Keys.Any(c2 => c2 == c && c2.Classification == SemesterClassification.Fall)));
                }
            }
        }

        public void AddCourseStudent(Shell s)
		{
            var idParam = SelectedStu?.Id ?? 0;
            if (idParam <= 0) { return; }
            s.GoToAsync($"//PersonDetail?personId={idParam}");
            //s.GoToAsync("//Student");
        }

        public void RemoveCourseStudent(Shell s)
        {
            if (SelectedCourse == null) { return; }

            SelectedStu.Grades.Remove(SelectedCourse);
            SelectedCourse.Roster.Remove(SelectedStu);

            NotifyPropertyChanged(nameof(Courses));
        }

        public void ShowFall()
        {
            Fall = true;
            Spring = false;
            Summer = false;
            NotifyPropertyChanged("Fall");
            NotifyPropertyChanged("Spring");
            NotifyPropertyChanged("Summer");
            NotifyPropertyChanged(nameof(Courses));
        }

        public void ShowSpring()
        {
            Fall = false;
            Spring = true;
            Summer = false;
            NotifyPropertyChanged("Fall");
            NotifyPropertyChanged("Spring");
            NotifyPropertyChanged("Summer");
            NotifyPropertyChanged(nameof(Courses));
        }

        public void ShowSummer()
        {
            Fall = false;
            Spring = false;
            Summer = true;
            NotifyPropertyChanged("Fall");
            NotifyPropertyChanged("Spring");
            NotifyPropertyChanged("Summer");
            NotifyPropertyChanged(nameof(Courses));
        }

        public StudentInternalViewViewModel(int id)
		{
            SelectedStu = StudentService.Current.GetPerson(id) as Student;
            ShowFall();
            NotifyPropertyChanged(nameof(Courses));
        }
    }
}

