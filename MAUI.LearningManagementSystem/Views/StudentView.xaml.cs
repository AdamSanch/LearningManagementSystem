using MAUI.LearningManagementSystem.ViewModels;

namespace MAUI.LearningManagementSystem.Views;

public partial class StudentView : ContentPage
{
	public StudentView()
	{
		InitializeComponent();
		BindingContext = new StudentViewViewModel();
	}

	private void ViewEditStudentClick(object sender, EventArgs e)
	{
		(BindingContext as StudentViewViewModel).ViewStudent(Shell.Current);
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as StudentViewViewModel).RefreshView();
    }
}
