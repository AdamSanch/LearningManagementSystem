using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;
using MAUI.LearningManagementSystem.ViewModels;

namespace MAUI.LearningManagementSystem.Views;

public partial class PersonDetailView : ContentPage
{
	public PersonDetailView()
	{
		InitializeComponent();

		BindingContext = new PersonDetailViewModel();
	}

	private void OkClick(object sender, EventArgs e)
	{
		var contex = BindingContext as PersonDetailViewModel;
		PersonClassification classification;
		switch (contex.ClassificationString)
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
		StudentService.Current.Add(new Student { Name = contex.Name, Classification = classification });
		Shell.Current.GoToAsync("//Instructor");
	}
}
