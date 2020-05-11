using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NoteTracker.Data.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace NoteTracker.Data.Repositories
{
    public class TermRepository
    {
        private static readonly string DatabasePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "notes.db");

        public void AddOrUpdate(Term term)
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                if (term.Id != 0)
                {
                    var existing = db.Table<Term>().FirstOrDefault(x => x.Id == term.Id);

                    if (existing != null)
                    {
                        db.Update(term);
                        return;
                    }
                }

                db.Insert(term);
            }
        }

        public List<Term> RetrieveAll()
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                return db.GetAllWithChildren<Term>().ToList();
            }
        }

        public Term Retrieve(int id)
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                return db.GetWithChildren<Term>(id);
            }
        }

        public void Delete(Term term)
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                foreach (var course in term.Courses)
                {
                    var courseWithChildren = db.GetWithChildren<Course>(course.Id);
                    courseWithChildren.Assessments.ForEach(a => db.Delete(a));
                    courseWithChildren.Notes.ForEach(n => db.Delete(n));
                    db.Delete(courseWithChildren);
                }
                
                db.Delete(term);
            }
        }
    }
}
