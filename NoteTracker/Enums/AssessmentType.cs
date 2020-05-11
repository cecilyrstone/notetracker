using System.ComponentModel.DataAnnotations;

namespace NoteTracker.Enums
{
    public enum AssessmentType
    {
        [Display(Description = "Objective")]
        Objective,
        [Display(Description = "Performance")]
        Performance
    }
}
