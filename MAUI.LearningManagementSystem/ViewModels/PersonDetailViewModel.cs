using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lib.LearningManagementSys.Item;
using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;
//using static Android.Provider.Contacts;

namespace MAUI.LearningManagementSystem.ViewModels
{
	public class PersonDetailViewModel: INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string ClassificationString { get; set; }
        public int Id { get; set; }

        public bool EditPerson { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PersonDetailViewModel(int id = 0)
        {
            EditPerson = false;
            if (id > 0)
            {
                LoadById(id);
            }
        }

        public Course SelectedCourse { get; set; }

        public ObservableCollection<Course> Courses
        {
            get
            {
                //var student = StudentService.Current.GetPerson(Id) as Student;
                var filteredList = CourseService.Current.Courses.Where(c => !c.Roster.Contains(StudentService.Current.GetPerson(Id)));
                return new ObservableCollection<Course>(filteredList);
                //return new ObservableCollection<Course>(CourseService.Current.Courses.Where(c => !student.Grades.Keys.Any(c2 => c2 == c)));
            }
        }

        public void AddPerson()
		{
            if (Id <= 0)
            {
                StudentService.Current.Add(new Student { Name = Name, Classification = StringToClass(ClassificationString) });
            }
            else
            {
                var refToUpdate = StudentService.Current.GetPerson(Id) as Student;
                refToUpdate.Name = Name;
                refToUpdate.Classification = StringToClass(ClassificationString);
            }
            Shell.Current.GoToAsync("//Instructor");
        }

        public void LoadById(int id)
        {
            if (id == 0) { return; }
            var person = StudentService.Current.GetPerson(id) as Student;
            if (person != null)
            {
                Name = person.Name;
                Id = person.Id;
                ClassificationString = ClassToString(person.Classification);

                EditPerson = true;

            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(ClassificationString));

        }

        public void AddCourse()
        {
            if (SelectedCourse == null || Id <= 0) { return; }

            var student = StudentService.Current.GetPerson(Id) as Student;
            student.Grades.Add(SelectedCourse,new GpaNumStruct());
            SelectedCourse.Roster.Add(student);

            RefreshView();
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(Courses));
        }

        private string ClassToString(PersonClassification pc)
        {
            var classificationString = string.Empty;
            switch (pc)
            {
                case PersonClassification.Senior:
                    classificationString = "S";
                    break;
                case PersonClassification.Junior:
                    classificationString = "J";
                    break;
                case PersonClassification.Sophmore:
                    classificationString = "O";
                    break;
                case PersonClassification.Freshman:
                default:
                    classificationString = "F";
                    break;
            }
            return classificationString;
        }

        private PersonClassification StringToClass(string s)
        {
            PersonClassification classification;
            switch (s)
            {
                case "S":
                    classification = PersonClassification.Senior;
                    break;
                case "J":
                    classification = PersonClassification.Junior;
                    break;
                case "O":
                    classification = PersonClassification.Sophmore;
                    break;
                case "F":
                default:
                    classification = PersonClassification.Freshman;
                    break;
            }

            return classification;
        }
    }
}

