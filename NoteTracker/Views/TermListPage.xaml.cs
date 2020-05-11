using System;
using System.Linq;
using NoteTracker.Data.Repositories;
using Xamarin.Forms;
using NoteTracker.ViewModels;
using Plugin.LocalNotifications;

namespace NoteTracker.Views
{
    public partial class TermListPage
    {
        private readonly TermListViewModel _viewModel;

        public TermListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new TermListViewModel();
            SetNotifications();
        }

        async void AddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditTermPage());
        }

        public async void TermCourses_onClick(object sender, EventArgs e)
        {
            var selectedTermViewModel = _viewModel.Terms.FirstOrDefault(t =>
            {
                var commandParameter = (sender as Button)?.CommandParameter;
                return commandParameter != null && t.Term.Id == (int) commandParameter;
            });

            if (selectedTermViewModel == null)
                return;

            await Navigation.PushAsync(new CourseListPage(selectedTermViewModel.Term));

            ItemsListView.SelectedItem = null;
        }

        public async void EditTerm_onClick(object sender, EventArgs e)
        {
            var selectedTermViewModel = _viewModel.Terms.FirstOrDefault(t =>
            {
                var commandParameter = (sender as Button)?.CommandParameter;
                return commandParameter != null && t.Term.Id == (int) commandParameter;
            });

            if (selectedTermViewModel == null)
                return;

            await Navigation.PushAsync(new EditTermPage(selectedTermViewModel));

            ItemsListView.SelectedItem = null;
        }

        public void DeleteTerm_onClick(object sender, EventArgs e)
        {
            var selectedTermViewModel = _viewModel.Terms.FirstOrDefault(t =>
            {
                var commandParameter = (sender as Button)?.CommandParameter;
                return commandParameter != null && t.Term.Id == (int) commandParameter;
            });

            if (selectedTermViewModel == null)
                return;

            _viewModel.DeleteTerm(selectedTermViewModel.Term);
            _viewModel.GetTerms();
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            if (itemTappedEventArgs.Item is TermViewModel term) term.Expanded = !term.Expanded;
        }

        protected override void OnAppearing()
        {
            _viewModel.GetTerms();
        }

        protected void SetNotifications()
        {
            var notificationId = 1;

            foreach (var course in from term in _viewModel.Terms where term.Term.Courses != null from course in term.Term.Courses where course.DisplayNotifications select course)
            {
                CrossLocalNotifications.Current.Show(course.Name, $"{course.Name} is starting.", notificationId, course.StartDate);
                notificationId++;
                CrossLocalNotifications.Current.Show(course.Name, $"{course.Name} is ending.", notificationId, course.EndDate);
                notificationId++;
                notificationId = SetAssessmentNotifications(course.Id, notificationId);
            }
        }

        protected int SetAssessmentNotifications(int courseId, int notificationId)
        {
            var assessmentRepo = new AssessmentRepository();
            var assessments = assessmentRepo.RetrieveAll(courseId);

            if (assessments == null) return notificationId;
            foreach (var assessment in assessments.Where(assessment => assessment.DisplayNotification))
            {
                CrossLocalNotifications.Current.Show(assessment.Name, $"{assessment.Name} is starting.", notificationId, assessment.StartDateTime);
                notificationId++;
            }

            return notificationId;
        }
    }
}