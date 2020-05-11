using System;
using NoteTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAssessmentPage : ContentPage
    {
        public AssessmentViewModel _viewModel { get; set; }

        public EditAssessmentPage(AssessmentViewModel assessmentViewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = assessmentViewModel;
        }

        public EditAssessmentPage(int courseId)
        {
            InitializeComponent();

            BindingContext = _viewModel = new AssessmentViewModel();
            _viewModel.CourseId = courseId;
        }

        public EditAssessmentPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new AssessmentViewModel();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            _viewModel = (AssessmentViewModel)BindingContext;
            _viewModel.Save();
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}