using System;
using System.Collections.Generic;
using System.IO;
using NoteTracker.Data.Models;
using SQLite;

namespace NoteTracker.Data.Repositories
{
    public class AssessmentRepository
    {
        private static readonly string DatabasePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "notes.db");

        public void AddOrUpdate(Assessment assessment)
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                if (assessment.Id != 0)
                {
                    var existing = db.Table<Assessment>().FirstOrDefault(x => x.Id == assessment.Id);

                    if (existing != null)
                    {
                        db.Update(assessment);
                        return;
                    }
                }

                db.Insert(assessment);
            }
        }

        public List<Assessment> RetrieveAll(int courseId)
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                return db.Table<Assessment>().Where(x => x.CourseId == courseId).ToList();
            }
        }

        public void Delete(Assessment assessment)
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                db.Delete(assessment);
            }
        }
    }
}
