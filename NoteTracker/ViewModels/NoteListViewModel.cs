using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NoteTracker.Data.Models;
using NoteTracker.Data.Repositories;

namespace NoteTracker.ViewModels
{
    public class NoteListViewModel : INotifyPropertyChanged
    {
        public Course Course { get; set; }
        public List<NoteViewModel> Notes { get; set; }
        private readonly NoteRepository _noteRepository;
        public NoteListViewModel(){}

        public NoteListViewModel(Course course)
        {
            _noteRepository = new NoteRepository();
            Course = course;
            GetNotes();
        }

        public void GetNotes()
        {
            var viewModels = new List<NoteViewModel>();
            var notes = _noteRepository.RetrieveAll(Course.Id);

            foreach (var note in notes)
            {
                var viewModel = new NoteViewModel
                {
                    Note = note,
                    Id = note.Id,
                    Title = note.Title,
                    Body = note.Body,
                    CourseId = note.CourseId,
                    IsOptional = note.IsOptional,
                    SharedWith = note.SharedWith
                };
                viewModels.Add(viewModel);
            }

            Notes = viewModels;
            OnPropertyChanged(nameof(Notes));
        }

        public void DeleteNote(Note note)
        {
            _noteRepository.Delete(note);
            OnPropertyChanged(nameof(Notes));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
