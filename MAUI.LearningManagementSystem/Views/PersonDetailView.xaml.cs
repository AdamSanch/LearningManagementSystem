using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;
using MAUI.LearningManagementSystem.ViewModels;

namespace MAUI.LearningManagementSystem.Views;

[QueryProperty(nameof(PersonId), "personId")]

public partial class PersonDetailView : ContentPage
{
	public PersonDetailView()
	{
		InitializeComponent();
	}

    public int PersonId{ set; get; }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new PersonDetailViewModel(PersonId);
    }

    private void AddCourseClick(object sender, EventArgs e)
    {
        (BindingContext as PersonDetailViewModel).AddCourse();
    }

    private void OkClick(object sender, EventArgs e)
	{
        (BindingContext as PersonDetailViewModel).AddPerson();
	}
}
