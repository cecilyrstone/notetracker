using System;
using Xamarin.Forms;
using NoteTracker.ViewModels;

namespace NoteTracker.Views
{
    public partial class EditTermPage : ContentPage
    {
        private TermViewModel _viewModel { get; set; }

        public EditTermPage(TermViewModel viewModel)
        {
            InitializeComponent();

            BindingContext =_viewModel = viewModel;
        }

        public EditTermPage()
        {
            InitializeComponent();

            BindingContext =_viewModel = new TermViewModel();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            _viewModel = (TermViewModel)BindingContext;
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