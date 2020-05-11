using System;
using NoteTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCoursePage : ContentPage
    {
        public CourseViewModel _viewModel { get; set; }

        public EditCoursePage(CourseViewModel courseViewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = courseViewModel;
        }

        public EditCoursePage(int termId)
        {
            InitializeComponent();

            BindingContext = _viewModel = new CourseViewModel();
            _viewModel.TermId = termId;
        }

        public EditCoursePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CourseViewModel();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            _viewModel = (CourseViewModel)BindingContext;
            var successful = _viewModel.Save();
            if (successful)
                await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}