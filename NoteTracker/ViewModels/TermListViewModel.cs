using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NoteTracker.Data.Models;
using NoteTracker.Data.Repositories;

namespace NoteTracker.ViewModels
{
    public class TermListViewModel : INotifyPropertyChanged
    {
        public List<TermViewModel> Terms { get; set; }
        private readonly TermRepository _termRepository;

        public TermListViewModel()
        {
            _termRepository = new TermRepository();
            GetTerms();
        }

        public void GetTerms()
        {
            var viewModels = new List<TermViewModel>();
            var terms = _termRepository.RetrieveAll();

            foreach (var term in terms)
            {
                var viewModel = new TermViewModel
                {
                    Term = term, Name = term.Name, StartDate = term.StartDate, EndDate = term.EndDate
                };
                viewModels.Add(viewModel);
            }

            Terms = viewModels;
            OnPropertyChanged(nameof(Terms));
        }

        public void DeleteTerm(Term term)
        {
            _termRepository.Delete(term);
            OnPropertyChanged(nameof(Terms));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
