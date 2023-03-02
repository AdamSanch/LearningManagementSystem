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
            var studentHelper = new StudentHelper();
            var courseHelper = new CourseHelper();
            bool cont = true;

            while (cont)
            {
                Console.WriteLine("\nEnter a choice below");
                Console.WriteLine("1. People Menu:");
                Console.WriteLine("2. Course Menu:");
                Console.WriteLine("3. Exit");
               
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
                    else if (result == 3)
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

