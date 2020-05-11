using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NoteTracker.Data.Enums;
using NoteTracker.Data.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace NoteTracker.Data.Repositories
{
    public class SeedDataRepository
    {
        private static readonly string DatabasePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "notes.db");

        public SeedDataRepository()
        {
            //ClearData();

            if (!SchemaExists())
                SeedData();
        }

        public bool SchemaExists()
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                var info = db.GetTableInfo("Terms");
                if (!info.Any())
                    return false;
            }

            return true;
        }

        public void ClearData()
        {
            using (var db = new SQLiteConnection(DatabasePath))
            {
                db.DropTable<Note>();
                db.DropTable<Assessment>();
                db.DropTable<Course>();
                db.DropTable<Term>();
            }
        }

        public void SeedData()
        {
            try
            {
                using (var db = new SQLiteConnection(DatabasePath))
                {
                    db.CreateTable<Note>();
                    db.CreateTable<Assessment>();
                    db.CreateTable<Course>();
                    db.CreateTable<Term>();

                    foreach (var term in SeedTerms)
                    {
                        db.Insert(term);

                        foreach (var course in term.Courses)
                        {
                            db.Insert(course);
                            foreach (var assessment in course.Assessments)
                            {
                                db.Insert(assessment);
                            }
                            course.Notes.ForEach(x => db.Insert(x));
                            db.UpdateWithChildren(course);
                        }

                        db.UpdateWithChildren(term);
                    }
                }
            }
            catch (Exception e)
            {
                var exception = e;
            }
        }

        public List<Term> SeedTerms = new List<Term>
        {
            new Term
            {
                EndDate = new DateTime(2020, 6, 1),
                Name = "First Term",
                StartDate = new DateTime(2020, 1, 1),
                Courses = new List<Course>
                {
                    new Course
                    {
                        CourseStatusId = 0,
                        InstructorEmail = "cnewm24@wgu.edu",
                        InstructorName = "Cecily Dantam",
                        InstructorPhone = "(206) 683-5482",
                        Name = "Course 101",
                        StartDate = new DateTime(2020, 2, 1),
                        EndDate = new DateTime(2020, 2, 28),
                        Assessments = new List<Assessment>
                        {
                            new Assessment
                            {
                                AssessmentTypeId = 1,
                                Name = "Do stuff",
                                StartDateTime = new DateTime(2020, 2, 15, 12, 0, 0),
                                EndDateTime = new DateTime(2020, 2, 15, 14, 0, 0),
                            },
                            new Assessment
                            {
                                AssessmentTypeId = 0,
                                Name = "Fill in bubbles",
                                StartDateTime = new DateTime(2020, 2, 16, 12, 0, 0),
                                EndDateTime = new DateTime(2020, 2, 16, 14, 0, 0),
                            }
                        },
                        Notes = new List<Note>
                        {
                            new Note
                            {
                                Title = "Note 1",
                                Body =
                                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                IsOptional = false,
                            },
                            new Note
                            {
                                Title = "Note 2",
                                Body =
                                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                IsOptional = false,
                            },
                            new Note
                            {
                                Title = "Note 3",
                                Body =
                                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                IsOptional = false,
                            }
                        }
                    }
                }
            }
        };
    }
}