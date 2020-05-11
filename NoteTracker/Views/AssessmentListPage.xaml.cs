using System;
using System.Linq;
using NoteTracker.Data.Models;
using NoteTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentListPage : ContentPage
    {
        private readonly AssessmentListViewModel _viewModel;

        public AssessmentListPage(Course course)
        {
            InitializeComponent();

            BindingContext = _viewModel = new AssessmentListViewModel(course);
        }

        async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditAssessmentPage(_viewModel.Course.Id));
        }


        public async void EditAssessment_onClick(object sender, EventArgs e)
        {
            var selectedAssessmentViewModel = _viewModel.Assessments.FirstOrDefault(t =>
            {
                var commandParameter = (sender as Button)?.CommandParameter;
                return commandParameter != null && t.Assessment.Id == (int) commandParameter;
            });
            if (selectedAssessmentViewModel == null)
                return;

            await Navigation.PushAsync(new EditAssessmentPage(selectedAssessmentViewModel));

            AssessmentListView.SelectedItem = null;
        }

        public void DeleteAssessment_onClick(object sender, EventArgs e)
        {
            var selectedAssessmentViewModel = _viewModel.Assessments.FirstOrDefault(t =>
            {
                var commandParameter = (sender as Button)?.CommandParameter;
                return commandParameter != null && t.Assessment.Id == (int) commandParameter;
            });

            if (selectedAssessmentViewModel == null)
                return;

            _viewModel.DeleteAssessments(selectedAssessmentViewModel.Assessment);
            _viewModel.GetAssessments();
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            var assessment = itemTappedEventArgs.Item as AssessmentViewModel;
            assessment.Expanded = !assessment.Expanded;
        }

        protected override void OnAppearing()
        {
            _viewModel.GetAssessments();
        }
    }
}