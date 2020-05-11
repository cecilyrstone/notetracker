using System;
using System.Collections.Generic;
using NoteTracker.Data.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace NoteTracker.Data.Models
{
    [Table("Courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [ForeignKey(typeof(Term)), Column("TermId")]
        public int TermId { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("StartDate")]
        public DateTime StartDate { get; set; }
        [Column("EndDate")]
        public DateTime EndDate { get; set; }
        [Column("CourseStatus")]
        public int CourseStatusId { get; set; }
        [Column("InstructorName")]
        public string InstructorName { get; set; }
        [Column("InstructorPhone")]
        public string InstructorPhone { get; set; }
        [Column("InstructorEmail")]
        public string InstructorEmail { get; set; }
        [Column("DisplayNotifications")]
        public bool DisplayNotifications { get; set; }
        [OneToMany]
        public virtual List<Assessment> Assessments { get; set; }
        [OneToMany]
        public virtual List<Note> Notes { get; set; }
        public Course() {}
    }
}
