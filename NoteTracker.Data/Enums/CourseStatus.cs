using System.ComponentModel.DataAnnotations;

namespace NoteTracker.Data.Enums
{
    public enum CourseStatus
    {
        [Display(Description = "In Progress")]
        InProgress,
        [Display(Description = "Deleted")]
        Deleted,
        [Display(Description = "Completed")]
        Completed,
        [Display(Description = "Plan to Take")]
        PlanToTake
    }
}
