using System;
using System.Xml.Linq;
using Lib.LearningManagementSys.Item;
using Lib.LearningManagementSys.People;
using Lib.LearningManagementSys.Services;




namespace LearningManagementSys.Helpers
{
	public class CourseHelper
	{
		private CourseService courseService;
        private StudentService studentService;

        public CourseHelper()
        {
            courseService = CourseService.Current;
            studentService = StudentService.Current;
        }

        public void CreateUpdateCourseRecord(Course? updateCourse = null)
        {
            bool isNew = false;
            if (updateCourse == null)
            {
                isNew = true;
                updateCourse = new Course();
            }

            Console.WriteLine("Enter the course name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter the course code:");
            var code = Console.ReadLine();
            while (courseService.Courses.Any(c => c.Code == code)){
                Console.WriteLine("Code already exist, enter course code:");
                code = Console.ReadLine();
            }
            Console.WriteLine("What is the course description?:");
            var description = Console.ReadLine();
            Console.WriteLine("How many Credit Hours?(5 max):");
            var creditHours = Console.ReadLine();
            int.TryParse(creditHours, out int result);

            updateCourse.Name = name ?? string.Empty;
            updateCourse.Code = code ?? string.Empty;
            updateCourse.Description = description ?? string.Empty;
            updateCourse.CreditHours = result;

            if (isNew)
            {
                AddToRoster(updateCourse);
                courseService.Add(updateCourse);
            }
        }

        public void UpdateCourse()
        {
            Console.WriteLine("Enter the code of the course to update:");
            ListCourses();
            var code = Console.ReadLine() ?? string.Empty;
            var selectedCourse = courseService.FindCourse(code);

            if (selectedCourse != null)
            {
                Console.WriteLine("Enter a choice below");
                Console.WriteLine("1. Update Course Info");
                Console.WriteLine("2. Add a Module");
                Console.WriteLine("3. Update a Module");
                Console.WriteLine("4. Enter Submisions");
                Console.WriteLine("5. Add students to the roster");
                Console.WriteLine("6. Remove students from the roster");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if(result == 1)
                    {
                        CreateUpdateCourseRecord(selectedCourse);
                    }
                    else if(result == 2)
                    {
                        AddModule(selectedCourse);
                    }
                    else if (result == 3)
                    {
                        UpdateModule(selectedCourse);
                    }
                    else if (result == 4)
                    {
                        EnterSubmissions(selectedCourse);
                    }
                    else if (result == 5)
                    {
                        AddToRoster(selectedCourse);
                    }
                    else if (result == 6)
                    {
                        RemoveFromRoster(selectedCourse);
                    }
                }
            }
        }

        private void AddToRoster(Course updateCourse)
        {
            Console.WriteLine("Give the code of a student you would like to add (Q to quit)");
            var cont = true;
            while (cont)
            {
                studentService.Students.Where(s => !updateCourse.Roster.Any(s2 => s2.Name == s.Name)).ToList().ForEach(Console.WriteLine);
                var input = "Q";
                if (studentService.Students.Any(s => !updateCourse.Roster.Any(s2 => s2.Name == s.Name)))
                {
                    input = Console.ReadLine() ?? string.Empty;
                }

                if (input.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    cont = false;
                }
                else
                {
                    var selectedPerson = studentService.GetPerson(int.Parse(input ?? "0"));
                    //var selectedStudent = studentService.Students.Where
                    //    (s => !updateCourse.Roster.Any(s2 => s2.Name == s.Name)).ToList().
                    //    FirstOrDefault(s => s.Name.ToUpper() == input.ToUpper());

                    if (selectedPerson != null)
                    {
                        updateCourse.Roster.Add(selectedPerson);
                        if (selectedPerson is Student)
                        {
                            studentService.UpdateAddGrade((selectedPerson as Student), updateCourse);
                        }
                    }
                }
            }
        }

        private void RemoveFromRoster(Course updateCourse)
        {
            Console.WriteLine("Give the name of a student you would like to remove (Q to quit)");
            var cont = true;
            while (cont)
            {
                updateCourse.Roster.ForEach(Console.WriteLine);
                var input = "Q";
                if (updateCourse.Roster.Count > 0)
                {
                    input = Console.ReadLine() ?? string.Empty;
                }

                if (input.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                {
                    cont = false;
                }
                else
                {
                    var selectedPerson = updateCourse.Roster.FirstOrDefault(s => s.Name.ToUpper() == input.ToUpper());
                    if (selectedPerson != null)
                    {
                        updateCourse.Roster.Remove(selectedPerson);
                        if (selectedPerson is Student)
                        {
                            (studentService.Students.FirstOrDefault(s => s.Id == selectedPerson.Id) as Student).Grades.Remove(updateCourse.Code);
                        }
                    }
                }
            }
        }

    //private Assignment CreateAssignment(Course updateCourse)
    //{
    //contentItem = new AssignmentItem();

    //Console.WriteLine("What is the assignment's name?");
    //        var name = Console.ReadLine() ?? string.Empty;
    //Console.WriteLine("What is the assignment description?");
    //        var description = Console.ReadLine() ?? string.Empty;
    //Console.WriteLine("How many points is the assignment worth?:");
    //        var points = Console.ReadLine() ?? string.Empty;
    //(contentItem as AssignmentItem).Assignment = new Assignment
    //        {
    //            Name = name,
    //            Description = description,
    //            TotalAvailablePoints = int.Parse(points ?? "0")
    //        };
    //    return assignment;

    //    //assignment.Name = name;
    //    //assignment.Description = description;
    //    //assignment.TotalAvailablePoints = int.Parse(points ?? "0");

    //    //if (selectedGroup != null)
    //    //{
    //    //    selectedGroup.Assignments.Add(assignment);

    //    //}
    //    //return assignment;
    //}

        private void AddModule(Course updateCourse)
        {
            var module = new Module();
            bool cont = true;

            Console.WriteLine("What is the module's name?");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the module's description?");
            var description = Console.ReadLine() ?? string.Empty;
            module.Name = name;
            module.Description = description;

            while (cont)
            {
                Console.WriteLine("Would you like to add a content item to this module?(Y)o(N)");
                var ans = Console.ReadLine() ?? String.Empty;
                if (ans.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    CreateContentItem(updateCourse, module);
                }
                else if (ans.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                {
                    cont = false;
                }

            }
            updateCourse.Modules.Add(module);

        }

        private void UpdateModule(Course updateCourse)
        {
            if (updateCourse.Modules.Any())
            {
                Console.WriteLine("Enter name of module?");
                updateCourse.Modules.ForEach(Console.WriteLine);
                var name = Console.ReadLine() ?? string.Empty;
                var module = updateCourse.Modules.FirstOrDefault(m => m.Name.ToUpper() == name.ToUpper());

                if (module != null)
                {
                    Console.WriteLine("Would you like to:");
                    Console.WriteLine("1. Update module information:");
                    Console.WriteLine("2. Add a Content Item to the module:");
                    Console.WriteLine("3. Remove a Content Item from the module:");
                    Console.WriteLine("4. Remove module:");
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out int result))
                    {
                        if (result == 1)
                        {
                            Console.WriteLine("What is the module's name?");
                            var newName = Console.ReadLine() ?? string.Empty;
                            Console.WriteLine("What is the module's description?");
                            var newDescription = Console.ReadLine() ?? string.Empty;
                            module.Name = newName;
                            module.Description = newDescription;
                        }
                        else if (result == 2)
                        {
                            CreateContentItem(updateCourse, module);
                        }
                        else if (result == 3)
                        {

                        }
                        else if (result == 4)
                        {

                            List<ContentItem> assignmentItems = module.Content.Where(s => s is AssignmentItem).ToList();

                            foreach (var a in updateCourse.AssignmentGroups)
                            {
                                foreach (var b in assignmentItems)
                                {
                                    if (a.Assignments.Any(s => s.Name == b.Name))
                                    {
                                        a.Assignments.Remove((b as AssignmentItem).Assignment);
                                    }
                                }
                            }
                            updateCourse.Modules.Remove(module);
                            foreach (var p in updateCourse.Roster)
                            {
                                if (p is Student)
                                {
                                    studentService.UpdateAddGrade((p as Student), updateCourse);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CreateContentItem(Course updateCourse, Module updateModule)
        {
            var contentItem = new ContentItem();
            bool Assign = false;

            Console.WriteLine("Is this Content Item a");
            Console.WriteLine("(F)ile?");
            Console.WriteLine("(A)ssignment?");
            Console.WriteLine("(P)age?");
            var personType = Console.ReadLine() ?? String.Empty;
            if (personType.Equals("F", StringComparison.InvariantCultureIgnoreCase))
            {
                contentItem = new FileItem();
            }
            else if (personType.Equals("P", StringComparison.InvariantCultureIgnoreCase))
            {
                contentItem = new PageItem();
            }
            else if (personType.Equals("A", StringComparison.InvariantCultureIgnoreCase))
            {
                contentItem = new AssignmentItem();

                Console.WriteLine("What is the assignment's name?");
                var name = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("What is the assignment description?");
                var description = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("How many points is the assignment worth?:");
                var points = Console.ReadLine() ?? string.Empty;
                (contentItem as AssignmentItem).Assignment = new Assignment
                {
                    Name = name,
                    Description = description,
                    TotalAvailablePoints = int.Parse(points ?? "0")
                };

                contentItem.Name = (contentItem as AssignmentItem).Assignment.Name;
                contentItem.Description = (contentItem as AssignmentItem).Assignment.Description;

                var groupName = string.Empty;
                if (updateCourse.AssignmentGroups.Any())
                {
                    Console.WriteLine("Enter name of the group for the assignment, or (C) to create new group");
                    updateCourse.AssignmentGroups.ForEach(Console.WriteLine);
                    groupName = Console.ReadLine() ?? string.Empty;
                }
                if (!updateCourse.AssignmentGroups.Any() || groupName.Equals("C", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Enter new assignment group's name:");
                    groupName = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Enter new assignment group's weight:");
                    var groupWeight = Console.ReadLine() ?? string.Empty;
                    updateCourse.AssignmentGroups.Add(new AssignmentGroup { Name = groupName, Weight = int.Parse(groupWeight ?? "100") });
                }
                var selectedGroup = updateCourse.FindAssignmentGroup(groupName);

                if (selectedGroup != null)
                {
                    selectedGroup.Assignments.Add((contentItem as AssignmentItem).Assignment);
                    //foreach (var p in updateCourse.Roster)
                    //{
                    //    if(p is Student)
                    //    {
                    //        studentService.UpdateAddGrade((p as Student), updateCourse);
                    //    }
                    //}
                }

                Assign = true;
            }
            else
            {
                return;
            }

            if (!Assign)
            {
                Console.WriteLine("What is the Content Item's name?");
                var name = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("What is the Content Items's description?");
                var description = Console.ReadLine() ?? string.Empty;
                contentItem.Name = name;
                contentItem.Description = description;
            }
        

            updateModule.Content.Add(contentItem);
        }

        private void EnterSubmissions(Course updateCourse)
        {
            if (updateCourse.AssignmentGroups.Any(s => s.Assignments.Any()))
            {
                Console.WriteLine("Enter the Assignment group");
                updateCourse.AssignmentGroups.ForEach(Console.WriteLine);
                var groupName = Console.ReadLine() ?? string.Empty;
                var selectedGroup = updateCourse.FindAssignmentGroup(groupName);
                if (selectedGroup == null)
                {
                    return;
                }
                Console.WriteLine("Enter the Assignment name");
                selectedGroup.Assignments.ForEach(Console.WriteLine);
                var name = Console.ReadLine() ?? string.Empty;
                var assignment = selectedGroup.Assignments.FirstOrDefault(s => s.Name == name);
                if (assignment == null)
                {
                    return;
                }
                Console.WriteLine("Give the code of the Student to add a submission for (Q to quit)");
                var cont = true;
                while (cont)
                {
                    updateCourse.Roster.Where(s0 => s0 is Student).ToList().Where(s => !assignment.Submissions.Any(s2 => s2.Student.Name == s.Name)).ToList().ForEach(Console.WriteLine);
                    var input = "Q";
                    if (updateCourse.Roster.Where(s0 => s0 is Student).ToList().Any(s => !assignment.Submissions.Any(s2 => s2.Student.Name == s.Name)))
                    {
                        input = Console.ReadLine() ?? string.Empty;
                    }
                    if (input.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                    {
                        cont = false;
                    }
                    else if (updateCourse.Roster.Where(s0 => s0 is Student).ToList().Where(s => !assignment.Submissions.Any(s2 => s2.Student.Name == s.Name)).ToList().Any(s3 => s3.Id == int.Parse(input ?? "0")))
                    {
                        var student = studentService.Students.FirstOrDefault(s => s.Id == int.Parse(input ?? "0"));   //GetPerson(int.Parse(input ?? "0"));
                        if (student != null)
                        {
                            Console.WriteLine($"What was this students score on {assignment.Name} out of {assignment.TotalAvailablePoints}");
                            var score = Console.ReadLine() ?? string.Empty;
                            if (double.TryParse(score, out double result))
                            {
                                //double grade = result;
                                //grade = (grade / assignment.TotalAvailablePoints) * 100;
                                if (result <= assignment.TotalAvailablePoints && result > 0)
                                {
                                    assignment.Submissions.Add(new Submission { Grade = result, Student = (student as Student) });
                                    studentService.UpdateAddGrade((student as Student), updateCourse);
                                    Console.WriteLine(student);
                                }
                                else { Console.WriteLine("Error in submission entry"); }
                            }
                        }     
                    }
                }
            }
        }

        public void SearchCourses()
        {
            Console.WriteLine("Enter the code of the course your looking for:");
            ListCourses();
            var query = Console.ReadLine() ?? string.Empty;

            var selectedCourse = courseService.FindCourse(query);

            if (selectedCourse != null) {
                Console.WriteLine($"{selectedCourse.Name}({selectedCourse.Code}) - {selectedCourse.Description}");
                Console.WriteLine("-------Roster-------");
                //courseService.FindCourse(query).Console.WriteLine($"{Name}({s.Code}) - {s.Description}\nRoster:"));
                selectedCourse.Roster.ForEach(Console.WriteLine);
                Console.WriteLine("-------Current Assignemnts-------");
                foreach (var a in selectedCourse.AssignmentGroups)
                {
                    Console.WriteLine(a);
                    a.Assignments.ForEach(Console.WriteLine);
                }
                Console.WriteLine("-------Current Modules-------");
                foreach (var m in selectedCourse.Modules)
                {
                    Console.WriteLine(m);
                    m.Content.ForEach(Console.WriteLine);
                }
                //courseService.SearchCourses(query).ToList().ForEach(s => s.AssignmentGroups.ForEach(s => Console.WriteLine($"{s.Name} \n {s.Assignments.ForEach(Console.WriteLine)}")));
            }
        }

        public void ListCourses()
        {
            courseService.Courses.ForEach(Console.WriteLine);
        }


	}
}

