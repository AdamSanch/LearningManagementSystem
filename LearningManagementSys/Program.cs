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
                Console.WriteLine("1. Add a student to the list");
                Console.WriteLine("2. Update a student on the list");
                Console.WriteLine("3. Print student list");
                Console.WriteLine("4. Search for a student");
                Console.WriteLine("5. Add a course to the list");
                Console.WriteLine("6. Update a course on the list");
                Console.WriteLine("7. Print course list");
                Console.WriteLine("8. Search for a course");
                Console.WriteLine("9. Exit");
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
                    else if(result == 5)
                    {
                        courseHelper.CreateCourseRecord();
                    }
                    else if (result == 6)
                    {
                        courseHelper.UpdateCourse();
                    }
                    else if (result == 7)
                    {
                        courseHelper.ListCourses();
                    }
                    else if (result == 8)
                    {
                        courseHelper.SearchCourses();
                    }
                    else if (result == 9)
                    {
                        cont = false;
                    }
                }
            }
            
        }
    }
}

