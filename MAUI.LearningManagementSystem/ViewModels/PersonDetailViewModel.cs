using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;

namespace MAUI.LearningManagementSystem.ViewModels
{
	public class PersonDetailViewModel
	{
        public string Name { get; set; }

        public string ClassificationString { get; set; }

        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PersonDetailViewModel(int id = 0)
        {
            if (id > 0)
            {
                LoadById(id);
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
            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(ClassificationString));

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

