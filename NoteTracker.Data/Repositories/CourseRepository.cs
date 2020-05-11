using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NoteTracker.Data.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace NoteTracker.Data.Repositories
{
    public class CourseRepository
    {
        private static readonly string DatabasePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "notes.db");

        public void AddOrUpdate(Course course)
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                if (course.Id != 0)
                {
                    var existing = db.Table<Course>().FirstOrDefault(x => x.Id == course.Id);

                    if (existing != null)
                    {
                        db.Update(course);
                        return;
                    }
                }

                db.Insert(course);
            }
        }

        public List<Course> RetrieveAll(int termId)
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                return db.GetAllWithChildren<Course>().Where(x => x.TermId == termId).ToList();
            }
        }

        public void Delete(Course course)
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                course.Assessments.ForEach(a => db.Delete(a));
                course.Notes.ForEach(n => db.Delete(n));
                db.Delete(course);
            }
        }
    }
}
