using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NoteTracker.Data.Models;
using NoteTracker.Data.Repositories;
using AssessmentType = NoteTracker.Enums.AssessmentType;

namespace NoteTracker.ViewModels
{
    public class AssessmentListViewModel : INotifyPropertyChanged
    {
        public Course Course { get; set; }
        public List<AssessmentViewModel> Assessments { get; set; }
        private readonly AssessmentRepository _assessmentRepository;

        public AssessmentListViewModel(){}

        public AssessmentListViewModel(Course course)
        {
            _assessmentRepository = new AssessmentRepository();
            Course = course;
            GetAssessments();
            
        }

        public bool CanAddAssessments { get; set; }

        public void GetAssessments()
        {
            var viewModels = new List<AssessmentViewModel>();
            var assessments = _assessmentRepository.RetrieveAll(Course.Id);

            foreach (var assessment in assessments)
            {
                var viewModel = new AssessmentViewModel
                {
                    Assessment = assessment,
                    Name = assessment.Name,
                    CourseId = assessment.CourseId,
                    StartDateTime = assessment.StartDateTime,
                    EndDateTime = assessment.EndDateTime,
                    AssessmentType = (AssessmentType)assessment.AssessmentTypeId,
                    DisplayNotification = assessment.DisplayNotification
                };

                viewModels.Add(viewModel);
            }

            Assessments = viewModels;
            OnPropertyChanged(nameof(Assessments));

            if (Assessments.Count < 2)
            {
                CanAddAssessments = true;
            }
            else
            {
                CanAddAssessments = false;
            }
                

            OnPropertyChanged(nameof(CanAddAssessments));
        }

        public void DeleteAssessments(Assessment assessment)
        {
            _assessmentRepository.Delete(assessment);
            OnPropertyChanged(nameof(Assessments));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
