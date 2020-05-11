using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NoteTracker.Data.Models;
using NoteTracker.Data.Repositories;
using CourseStatus = NoteTracker.Enums.CourseStatus;

namespace NoteTracker.ViewModels
{
    public class CourseListViewModel : INotifyPropertyChanged
    {
        public List<CourseViewModel> Courses { get; set; }
        public Term Term { get; set; }
        private readonly CourseRepository _courseRepository;
        public CourseListViewModel(){}

        public CourseListViewModel(Term term)
        {
            _courseRepository = new CourseRepository();
            Term = term;
            GetCourses();
        }

        public void GetCourses()
        {
            var viewModels = new List<CourseViewModel>();
            var courses = _courseRepository.RetrieveAll(Term.Id);

            foreach (var course in courses)
            {
                var viewModel = new CourseViewModel
                {
                    Course = course,
                    Name = course.Name,
                    StartDate = course.StartDate,
                    EndDate = course.EndDate,
                    CourseStatus = (CourseStatus)course.CourseStatusId,
                    InstructorName = course.InstructorName,
                    InstructorPhone = course.InstructorPhone,
                    InstructorEmail = course.InstructorEmail,
                    TermId = course.TermId,
                    DisplayNotifications = course.DisplayNotifications
                };
                viewModels.Add(viewModel);
            }

            Courses = viewModels;
            OnPropertyChanged(nameof(Courses));
        }

        public void DeleteCourse(Course course)
        {
            _courseRepository.Delete(course);
            OnPropertyChanged(nameof(Courses));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
