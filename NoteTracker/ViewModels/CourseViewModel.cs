
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NoteTracker.Annotations;
using NoteTracker.Data.Models;
using NoteTracker.Data.Repositories;
using Xamarin.Forms;
using CourseStatus = NoteTracker.Enums.CourseStatus;

namespace NoteTracker.ViewModels
{
    public class CourseViewModel: INotifyPropertyChanged
    {
        public Course Course { get; set; }
        private bool _expanded { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CourseStatus CourseStatus { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string ErrorText { get; set; }
        public bool DisplayNotifications { get; set; }
        public int TermId { get; set; }
        private readonly CourseRepository _courseRepository = new CourseRepository();

        public CourseViewModel()
        {
            if (StartDate.Equals(DateTime.MinValue))
                StartDate = DateTime.Now;
            if (EndDate.Equals(DateTime.MinValue))
                EndDate = DateTime.Now;
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

            var course = new Course
            {
                Name = Name,
                StartDate = StartDate,
                EndDate = EndDate,
                CourseStatusId = (int) CourseStatus,
                InstructorName = InstructorName,
                InstructorPhone = InstructorPhone,
                InstructorEmail = InstructorEmail,
                TermId = TermId,
                DisplayNotifications = DisplayNotifications
            };

            if (Course != null)
            {
                course.Id = Course.Id;
            }

            _courseRepository.AddOrUpdate(course);
            OnPropertyChanged(nameof(Course));
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Validate()
        {
            var hasErrors = false;

            if (string.IsNullOrWhiteSpace(Name))
            {
                hasErrors = true;
                ErrorText = "Name can't be empty";
            }

            if (string.IsNullOrWhiteSpace(InstructorName))
            {
                hasErrors = true;
                ErrorText = "Instructor Name can't be empty";
            }

            if (string.IsNullOrWhiteSpace(InstructorPhone))
            {
                hasErrors = true;
                ErrorText = "Instructor phone can't be empty";
            }

            if (string.IsNullOrWhiteSpace(InstructorEmail))
            {
                hasErrors = true;
                ErrorText = "Instructor email can't be empty";
            }

            if (StartDate > EndDate)
            {
                hasErrors = true;
                ErrorText = "Start date must be before end date";
            }

            OnPropertyChanged(nameof(ErrorText));
            return hasErrors;
        }
    }
}
