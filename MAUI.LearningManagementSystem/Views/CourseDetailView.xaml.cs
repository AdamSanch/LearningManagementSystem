using Lib.LearningManagementSys.Item;
using Lib.LearningManagementSys.People;
using MAUI.LearningManagementSystem.ViewModels;

namespace MAUI.LearningManagementSystem.Views;

[QueryProperty(nameof(selCourse), "Cor")]

public partial class CourseDetailView : ContentPage
{
    public CourseDetailView()
    {
        InitializeComponent();
    }

    public Course selCourse { get; set; }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CourseDetailViewModel(selCourse);
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddCourse(Shell.Current);
    }
}
