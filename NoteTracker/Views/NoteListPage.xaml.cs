using System;
using System.Linq;
using NoteTracker.Data.Models;
using NoteTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteListPage : ContentPage
    {
        private readonly NoteListViewModel _viewModel;
        public NoteListPage(Course course)
        {
            InitializeComponent();

            BindingContext = _viewModel = new NoteListViewModel(course);
        }

        async void AddNote_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditNotePage(_viewModel.Course.Id));
        }


        public async void EditNote_onClick(object sender, EventArgs e)
        {
            var selectedNote = _viewModel.Notes.FirstOrDefault(t =>
            {
                var commandParameter = (sender as Button)?.CommandParameter;
                return commandParameter != null && t.Note.Id == (int) commandParameter;
            });

            if (selectedNote == null)
                return;

            await Navigation.PushAsync(new EditNotePage(selectedNote));

            NoteListView.SelectedItem = null;
        }

        public void DeleteNote_onClick(object sender, EventArgs e)
        {
            var selectedNoteViewModel = _viewModel.Notes.FirstOrDefault(t =>
            {
                var commandParameter = (sender as Button)?.CommandParameter;
                return commandParameter != null && t.Note.Id == (int) commandParameter;
            });

            if (selectedNoteViewModel == null)
                return;

            _viewModel.DeleteNote(selectedNoteViewModel.Note);
            _viewModel.GetNotes();
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            var note = itemTappedEventArgs.Item as NoteViewModel;
            note.Expanded = !note.Expanded;
        }

        protected override void OnAppearing()
        {
            _viewModel.GetNotes();
        }
    }
}