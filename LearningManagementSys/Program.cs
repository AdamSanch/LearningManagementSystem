using System;
using LearningManagementSys.Helpers;
using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;
using Lib.LearningManagementSys.Item;

namespace MyApp 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> Registry = new List<Person>
            {
                new Student{ Name = "Sylvester", Classification=PersonClassification.Freshman},
                new TeachingAssistant{ Name = "Whiskers"},
                new Instructor{ Name = "Sasha"},
                new Student{ Name = "Adam", Classification=PersonClassification.Senior},
                new Student{ Name = "Jim", Classification=PersonClassification.Junior},
                new Student{ Name = "Steve", Classification=PersonClassification.Sophmore},
                new Student{ Name = "Susie", Classification=PersonClassification.Freshman},
                new Student{ Name = "John", Classification=PersonClassification.Junior},
                new Student{ Name = "Sarah", Classification=PersonClassification.Senior},
                new Student{ Name = "Austin", Classification=PersonClassification.Freshman},
                new Student{ Name = "Joe", Classification=PersonClassification.Sophmore},
                new Student{ Name = "Ethin", Classification=PersonClassification.Senior},
                new Student{ Name = "Lydia", Classification=PersonClassification.Junior},
                new Student{ Name = "Andee", Classification=PersonClassification.Sophmore},
                new Student{ Name = "Sam", Classification=PersonClassification.Freshman},
                new Student{ Name = "Evan", Classification=PersonClassification.Junior},
                new Student{ Name = "Josh", Classification=PersonClassification.Senior},
                new Student{ Name = "Ozzy", Classification=PersonClassification.Freshman},
                new Student{ Name = "Cleo", Classification=PersonClassification.Sophmore},
                new TeachingAssistant{ Name = "Oscar"},
                new Instructor{ Name = "Andi"},
                new TeachingAssistant{ Name = "Martin"},
                new Instructor{ Name = "Will"},
                new TeachingAssistant{ Name = "Susan"},
                new Instructor{ Name = "Henry"},
                new TeachingAssistant{ Name = "Nick"},
                new Instructor{ Name = "Tom"},
                new TeachingAssistant{ Name = "Connor"},
                new Instructor{ Name = "Sage"},

            };

            var studentHelper = new StudentHelper();
            var courseHelper = new CourseHelper();
            bool cont = true;

            foreach (var p in Registry)
            {
                studentHelper.AddPerson(p);
            }

            while (cont)
            {
                Console.WriteLine("\nEnter a choice below");
                Console.WriteLine("1. People Menu:");
                Console.WriteLine("2. Course Menu:");
                Console.WriteLine("9. Exit");
               
                var input = Console.ReadLine();
                Console.WriteLine();

                if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        PersonMenu(studentHelper);
                    }
                    else if (result == 2)
                    {
                        CourseMenu(courseHelper);
                    }
                    else if (result == 9)
                    {
                        cont = false;
                    }
                }
            }
            
        }

        static void PersonMenu(StudentHelper studentHelper)
        {
            Console.WriteLine("1. Add a student to the list");
            Console.WriteLine("2. Update a student on the list");
            Console.WriteLine("3. Print student list");
            Console.WriteLine("4. Search for a student");
            var input = Console.ReadLine();
            Console.WriteLine();

            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    studentHelper.CreateStudentRecord();
                }
                else if (result == 2)
                {
                    studentHelper.UpdateStudent();
                }
                else if (result == 3)
                {
                    studentHelper.ListStudents();
                }
                else if (result == 4)
                {
                    studentHelper.SearchStudents();
                }
            }
        }

        static void CourseMenu(CourseHelper courseHelper)
        {
            Console.WriteLine("1. Add a course to the list");
            Console.WriteLine("2. Update a course on the list");
            Console.WriteLine("3. Print course list");
            Console.WriteLine("4. Search for a course");
            var input = Console.ReadLine();
            Console.WriteLine();

            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    courseHelper.CreateUpdateCourseRecord();
                }
                else if (result == 2)
                {
                    courseHelper.UpdateCourse();
                }
                else if (result == 3)
                {
                    courseHelper.ListCourses();
                }
                else if (result == 4)
                {
                    courseHelper.SearchCourses();
                }
            }
        }
    }
}

