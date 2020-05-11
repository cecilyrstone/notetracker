using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NoteTracker.Annotations;
using NoteTracker.Data.Models;
using NoteTracker.Data.Repositories;
using Xamarin.Forms;

namespace NoteTracker.ViewModels
{
    public class TermViewModel : INotifyPropertyChanged
    {
        public Term Term { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ErrorText { get; set; }
        private bool _expanded { get; set; }
        private readonly TermRepository _termRepository = new TermRepository();

        public TermViewModel()
        {
            if (StartDate.Equals(DateTime.MinValue))
                StartDate = DateTime.Now;
            if (EndDate.Equals(DateTime.MinValue))
                EndDate = DateTime.Now;
        }

        public string StartText
        {
            get => $"{StartDate.ToShortDateString()} to";
        }

        public string EndText
        {
            get => $"{EndDate.ToShortDateString()}";
        }

        public bool Expanded
        {
            get => _expanded;
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged(nameof(Expanded));
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }

        public Color BackgroundColor
        {
            get
            {
                if (Expanded)
                    return Color.LightSlateGray;
                return Color.White;
            }
        }

        public bool Save()
        {
            var hasErrors = Validate();

            if (hasErrors)
                return false;

            var term = new Term
            {
                Name = Name,
                StartDate = StartDate,
                EndDate = EndDate
            };

            _termRepository.AddOrUpdate(term);
            OnPropertyChanged(nameof(Term));
            return true;
        }

        public bool Validate()
        {
            var hasErrors = false;

            if (StartDate > EndDate)
            {
                hasErrors = true;
                ErrorText = "Start date must be before end date";
            }

            OnPropertyChanged(nameof(ErrorText));
            return hasErrors;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
