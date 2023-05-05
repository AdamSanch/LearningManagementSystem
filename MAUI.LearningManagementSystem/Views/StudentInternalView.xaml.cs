using Lib.LearningManagementSys.People;
using MAUI.LearningManagementSystem.ViewModels;

namespace MAUI.LearningManagementSystem.Views;

[QueryProperty(nameof(PId), "personId")]

public partial class StudentInternalView : ContentPage
{
	public StudentInternalView()
    {
        InitializeComponent();
	}

    public int PId { set; get; }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        if(PId > 0) {
            BindingContext = new StudentInternalViewViewModel(PId);
        }
        else
        {
            BindingContext = null;
            Shell.Current.GoToAsync("//Student");
        }
        

    }

    private void AddCourseClick(object sender, EventArgs e)
	{
		(BindingContext as StudentInternalViewViewModel).AddCourseStudent(Shell.Current);
	}

    private void RemoveCourseClick(object sender, EventArgs e)
    {
        (BindingContext as StudentInternalViewViewModel).RemoveCourseStudent(Shell.Current);
    }

    private void Toolbar_FallClicked(object sender, EventArgs e)
    {
        (BindingContext as StudentInternalViewViewModel).ShowFall();
    }

    private void Toolbar_SpringClicked(object sender, EventArgs e)
    {
        (BindingContext as StudentInternalViewViewModel).ShowSpring();
    }

    private void Toolbar_SummerClicked(object sender, EventArgs e)
    {
        (BindingContext as StudentInternalViewViewModel).ShowSummer();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Student");
    }

}
