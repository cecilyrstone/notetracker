using System;
using System.Threading.Tasks;
using NoteTracker.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditNotePage : ContentPage
    {
        public NoteViewModel _viewModel { get; set; }

        public EditNotePage(NoteViewModel noteViewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = noteViewModel;
        }

        public EditNotePage(int courseId)
        {
            InitializeComponent();

            BindingContext = _viewModel = new NoteViewModel();
            _viewModel.CourseId = courseId;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            _viewModel = (NoteViewModel)BindingContext;
            _viewModel.Save();
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void ShareNote_Clicked(object sender, EventArgs e)
        {
            await _viewModel.ShareNote();
        }
    }
}