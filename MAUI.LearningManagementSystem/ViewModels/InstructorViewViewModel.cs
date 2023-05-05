using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lib.LearningManagementSys.Item;
using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;

namespace MAUI.LearningManagementSystem.ViewModels
{
	public class InstructorViewViewModel: INotifyPropertyChanged
	{
        public InstructorViewViewModel()
        {
            IsEnrollmentsVisible = true;
            IsCoursesVisible = false;
            RefreshView();
        }

        public bool IsEnrollmentsVisible{ get; set; }
        public bool IsCoursesVisible { get; set; }

        public Person SelectedPerson { get; set; }
        public Course SelectedCourse { get; set; }

        public ObservableCollection<Person> People
        {
            get
            {
                var filteredList = StudentService.Current.Students.Where
                    (s => s.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty));
                return new ObservableCollection<Person>(filteredList);
            }
        }

        public ObservableCollection<Course> Courses
        {
            get
            {
                return new ObservableCollection<Course>(CourseService.Current.Courses);
            }
        }

        public void ShowEnrollments()
        {
            IsEnrollmentsVisible = true;
            IsCoursesVisible = false;
            NotifyPropertyChanged("IsEnrollmentsVisible");
            NotifyPropertyChanged("IsCoursesVisible");
        }

        public void ShowCourses()
        {
            IsEnrollmentsVisible = false;
            IsCoursesVisible = true;
            NotifyPropertyChanged("IsEnrollmentsVisible");
            NotifyPropertyChanged("IsCoursesVisible");
        }



        public string Title { get => "Instructor / Administrator Menu"; }

        private string query;
        public string Query
        {
            get => query;
            set
            {
                query = value;
                NotifyPropertyChanged(nameof(People));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshView()
        {
            SelectedPerson = null;
            NotifyPropertyChanged(nameof(People));
            NotifyPropertyChanged(nameof(Courses));
            NotifyPropertyChanged(nameof(SelectedPerson));
            NotifyPropertyChanged(nameof(SelectedCourse));
        }

        public void AddEnrollmentClick(Shell s)
        {
            var idParam = SelectedPerson?.Id ?? 0;
            RefreshView();
            s.GoToAsync($"//PersonDetail?personId={idParam}");
        }

        public void AddCourseClick(Shell s)
        {
            var Bar = new Dictionary<string, object>
              {
                  { "Cor", SelectedCourse }
              };

            s.GoToAsync("//CourseDetail", Bar);
            //s.GoToAsync($"//CourseDetail");
        }

        //public void RemoveCourseClick()
        //{
        //    if (SelectedCourse == null) { return; }

        //    CourseService.Current.Re
        //}

        public void RemoveEnrollmentClick()
        {
            if(SelectedPerson == null) { return; }

            StudentService.Current.Remove(SelectedPerson);
            RefreshView();
        }
	}
}

