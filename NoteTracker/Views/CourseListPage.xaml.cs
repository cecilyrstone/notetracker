using System;
using System.Linq;
using NoteTracker.Data.Models;
using NoteTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseListPage : ContentPage
    {
        readonly CourseListViewModel _viewModel;
        public CourseListPage(Term term)
        {
            InitializeComponent();

            BindingContext = _viewModel = new CourseListViewModel(term);
        }

        async void AddCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCoursePage(_viewModel.Term.Id));
        }

        public async void CourseNotes_onClick(object sender, EventArgs e)
        {
            var selectedCourseViewModel = _viewModel.Courses.FirstOrDefault(t =>
            {
                var commandParameter = (sender as Button)?.CommandParameter;
                return commandParameter != null && t.Course.Id == (int) commandParameter;
            });

            if (selectedCourseViewModel == null)
                return;

            await Navigation.PushAsync(new NoteListPage(selectedCourseViewModel.Course));
            CourseListView.SelectedItem = null;
        }

        public async void CourseAssessments_onClick(object sender, EventArgs e)
        {
            var selectedCourseViewModel = _viewModel.Courses.FirstOrDefault(t =>
            {
                var commandParameter = (sender as Button)?.CommandParameter;
                return commandParameter != null && t.Course.Id == (int) commandParameter;
            });

            if (selectedCourseViewModel == null)
                return;

            await Navigation.PushAsync(new AssessmentListPage(selectedCourseViewModel.Course));
            CourseListView.SelectedItem = null;
        }

        public async void EditCourse_onClick(object sender, EventArgs e)
        {
            var selectedCourseViewModel = _viewModel.Courses.FirstOrDefault(t =>
            {
                var commandParameter = (sender as Button)?.CommandParameter;
                return commandParameter != null && t.Course.Id == (int) commandParameter;
            });

            if (selectedCourseViewModel == null)
                return;

            await Navigation.PushAsync(new EditCoursePage(selectedCourseViewModel));
            CourseListView.SelectedItem = null;
        }

        public void DeleteCourse_onClick(object sender, EventArgs e)
        {
            var selectedCourseViewModel = _viewModel.Courses.FirstOrDefault(t =>
            {
                var commandParameter = (sender as Button)?.CommandParameter;
                return commandParameter != null && t.Course.Id == (int) commandParameter;
            });

            if (selectedCourseViewModel == null)
                return;

            _viewModel.DeleteCourse(selectedCourseViewModel.Course);
            _viewModel.GetCourses();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            var course = itemTappedEventArgs.Item as CourseViewModel;
            course.Expanded = !course.Expanded;
        }

        protected override void OnAppearing()
        {
            _viewModel.GetCourses();
        }
    }
}