using System.ComponentModel.DataAnnotations;

namespace NoteTracker.Data.Enums
{
    public enum AssessmentType
    {
        [Display(Description = "Objective")]
        Objective,
        [Display(Description = "Performance")]
        Performance
    }
}
