using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NoteTracker.Annotations;
using NoteTracker.Data.Models;
using NoteTracker.Data.Repositories;
using Xamarin.Forms;
using AssessmentType = NoteTracker.Enums.AssessmentType;

namespace NoteTracker.ViewModels
{
    public class AssessmentViewModel : INotifyPropertyChanged
    {
        public Assessment Assessment { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public AssessmentType AssessmentType { get; set; }
        public bool DisplayNotification { get; set; }
        private bool _expanded { get; set; }
        private readonly AssessmentRepository _assessmentRepository = new AssessmentRepository();

        public AssessmentViewModel()
        {
            if (StartDateTime.Equals(DateTime.MinValue))
                StartDateTime = DateTime.Now;
            if (EndDateTime.Equals(DateTime.MinValue))
                EndDateTime = DateTime.Now;
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
        public void Save()
        {
            var assessment = new Assessment
            {
                Name = Name,
                StartDateTime = StartDateTime,
                EndDateTime = EndDateTime,
                AssessmentTypeId = (int) AssessmentType,
                CourseId = CourseId,
                DisplayNotification = DisplayNotification
            };

            if (Assessment != null) assessment.Id = Assessment.Id;

            _assessmentRepository.AddOrUpdate(assessment);
            OnPropertyChanged(nameof(Assessment));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
