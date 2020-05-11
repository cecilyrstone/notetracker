using SQLite;
using SQLiteNetExtensions.Attributes;

namespace NoteTracker.Data.Models
{
    [Table("Notes")]
    public class Note
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [ForeignKey(typeof(Course)), Column("CourseId")]
        public int CourseId { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Body")]
        public string Body { get; set; }
        [Column("IsOptional")]
        public bool IsOptional { get; set; }
        [Column("SharedWith")]
        public string SharedWith { get; set; }
        public Note() {}
    }
}
