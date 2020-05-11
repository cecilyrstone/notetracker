using System;
using NoteTracker.Data.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace NoteTracker.Data.Models
{
    [Table("Assessments")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [ForeignKey(typeof(Course)), Column("CourseId")]
        public int CourseId { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("StartDateTime")]
        public DateTime StartDateTime { get; set; }
        [Column("EndDateTime")]
        public DateTime EndDateTime { get; set; }
        [Column("AssessmentTypeId")]
        public int AssessmentTypeId { get; set; }
        [Column("DisplayNotification")]
        public bool DisplayNotification { get; set; }
        public Assessment() {}
    }
}
