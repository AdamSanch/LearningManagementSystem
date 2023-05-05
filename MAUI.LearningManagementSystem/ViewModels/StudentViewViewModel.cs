using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;
//using static Android.App.DownloadManager;

namespace MAUI.LearningManagementSystem.ViewModels
{
	public class StudentViewViewModel : INotifyPropertyChanged
	{
        public Person SelectedStudent { get; set; }

        public ObservableCollection<Person> People
        {
            get
            {
                var filteredList = StudentService.Current.Students.Where
                    (s => s.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty));
                return new ObservableCollection<Person>(filteredList);
            }
        }

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

        public void ViewStudent(Shell s)
		{
            if(SelectedStudent == null) { return; }
            //var navigationParameter = new Dictionary<string, object>
            //{
            //    { "stu", SelectedPerson }
            //};

            s.GoToAsync($"//StudentInternal?personId={SelectedStudent.Id}");
		}

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(People));
        }

        public StudentViewViewModel()
		{
           
		}
	}
}

