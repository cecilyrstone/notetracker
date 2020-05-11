using System;
using System.Collections.Generic;
using System.IO;
using NoteTracker.Data.Models;
using SQLite;

namespace NoteTracker.Data.Repositories
{
    public class NoteRepository
    {
        private static readonly string DatabasePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "notes.db");

        public void AddOrUpdate(Note note)
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                if (note.Id != 0)
                {
                    var existing = db.Table<Note>().FirstOrDefault(x => x.Id == note.Id);

                    if (existing != null)
                    {
                        db.Update(note);
                        return;
                    }
                }

                db.Insert(note);
            }
        }

        public List<Note> RetrieveAll(int courseId)
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                return db.Table<Note>().Where(x => x.CourseId == courseId).ToList();
            }
        }

        public void Delete(Note note)
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                db.Delete(note);
            }
        }
    }
}
