using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NoteTracker.Annotations;
using NoteTracker.Data.Models;
using NoteTracker.Data.Repositories;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NoteTracker.ViewModels
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        public Note Note { get; set; }
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsOptional { get; set; }
        public string SharedWith { get; set; }
        private bool _expanded { get; set; }
        private readonly NoteRepository _noteRepository = new NoteRepository();

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

        public void Save()
        {
            var note = new Note
            {
                Body = Body,
                CourseId = CourseId,
                IsOptional = IsOptional,
                Title = Title,
                SharedWith = SharedWith
            };

            if (Note != null) note.Id = Note.Id;

            _noteRepository.AddOrUpdate(note);
            OnPropertyChanged(nameof(Note));
        }

        public async Task ShareNote()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = Body,
                Title = Title
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
