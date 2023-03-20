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
        private ListNavigator<Course> courseNavigator;

        public CourseHelper()
        {
            courseService = CourseService.Current;
            studentService = StudentService.Current;
            courseNavigator = new ListNavigator<Course>(courseService.Courses, 2);
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
            bool cont = true;
            Console.WriteLine("Enter the code of the course to update:");
            courseService.Courses.ForEach(Console.WriteLine);
            var code = Console.ReadLine() ?? string.Empty;
            var selectedCourse = courseService.FindCourse(code);
            if (selectedCourse != null)
            {
                while (cont)
                {
                    Console.WriteLine("Enter a choice below or (Q)uit");
                    Console.WriteLine("1. Update Course Info");
                    Console.WriteLine("2. Add a Module");
                    Console.WriteLine("3. Update a Module");
                    Console.WriteLine("4. Enter Submisions");
                    Console.WriteLine("5. Announcements");
                    Console.WriteLine("6. Add students to the roster");
                    Console.WriteLine("7. Remove students from the roster");
                    var input = Console.ReadLine() ?? string.Empty;

                    if (input.Equals("q", StringComparison.CurrentCultureIgnoreCase)) { cont = false; }

                    else if (int.TryParse(input, out int result))
                    {
                        if (result == 1)
                        {
                            CreateUpdateCourseRecord(selectedCourse);
                        }
                        else if (result == 2)
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
                            AnnouncementMenu(selectedCourse);
                        }
                        else if (result == 6)
                        {
                            AddToRoster(selectedCourse);
                        }
                        else if (result == 7)
                        {
                            RemoveFromRoster(selectedCourse);
                        }
                    }
                }
            }
        }

        private void AnnouncementMenu(Course selectedCourse)
        {
            bool cont = true;
            while (cont)
            {
                Console.WriteLine("Enter a choice below or (Q)uit");
                Console.WriteLine("1. Add Announcement");
                Console.WriteLine("2. Update Announcement");
                Console.WriteLine("3. Delete Announcement");
                var input = Console.ReadLine() ?? string.Empty;
                if (input.Equals("q", StringComparison.CurrentCultureIgnoreCase)) { cont = false; }

                else if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        AddUpdateAnnouncement(selectedCourse);
                    }
                    else if ((result == 2 || result == 3) && selectedCourse.Announcements.Any())
                    {
                        Console.WriteLine("Enter announcement's name");
                        selectedCourse.Announcements.ForEach(s => Console.WriteLine($"{s.Name}"));
                        var name = Console.ReadLine() ?? string.Empty;
                        var announcement = selectedCourse.Announcements.FirstOrDefault(s => s.Name.Contains(name));
                        if (announcement != null && result == 2)
                        {
                            AddUpdateAnnouncement(selectedCourse, announcement);
                        }
                        else if (announcement != null && result == 3)
                        {
                            selectedCourse.Announcements.Remove(announcement);
                        }
                    }
                }
            }
        }

        private void AddUpdateAnnouncement(Course selectedCourse, Announcement? announcement = null)
        {
            bool isNew = false;
            if (announcement == null)
            {
                announcement = new Announcement();
                isNew = true;
            }
            Console.WriteLine("What is the announcement's name?");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the announcement?");
            var content = Console.ReadLine() ?? string.Empty;
            announcement.Name = name;
            announcement.Content = content;
            if (isNew)
            {
                selectedCourse.Announcements.Add(announcement);
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
                            (studentService.Students.FirstOrDefault(s => s.Id == selectedPerson.Id) as Student).Grades.Remove(updateCourse);
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
                    Console.WriteLine("2. Add Content Item to module:");
                    Console.WriteLine("3. Update Content Item in module:");
                    Console.WriteLine("4. Remove a Content Item from the module:");
                    Console.WriteLine("5. Remove module:");
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
                        else if ((result == 3 || result == 4) && module.Content.Any())
                        {
                            Console.WriteLine("Enter the Content Item name");
                            module.Content.ForEach(Console.WriteLine);
                            var ciName = Console.ReadLine() ?? string.Empty;
                            var item = module.Content.FirstOrDefault(s => s.Name.ToUpper() == ciName.ToUpper());
                            if (item != null && result == 3)
                            {
                                UpdateContentItem(item);
                                foreach (var p in updateCourse.Roster)
                                {
                                    if (p is Student)
                                    {
                                        studentService.UpdateAddGrade((p as Student), updateCourse);
                                    }
                                }
                            }
                            if (item != null && result == 4)
                            {
                                module.Content.Remove(item);
                                if (item is AssignmentItem)
                                {
                                    foreach (var a in updateCourse.AssignmentGroups)
                                    {
                                        if (a.Assignments.Any(s => s.Name == item.Name))
                                        {
                                            a.Assignments.Remove((item as AssignmentItem).Assignment);
                                        }
                                    }
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
                        else if (result == 5)
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
                if (selectedGroup == null){ return;}
                else
                {
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
                        TotalAvailablePoints = int.Parse(points ?? "100")
                    };
                    selectedGroup.Assignments.Add((contentItem as AssignmentItem).Assignment);

                    contentItem.Name = (contentItem as AssignmentItem).Assignment.Name;
                    contentItem.Description = (contentItem as AssignmentItem).Assignment.Description;
                    Assign = true;
                }
                //if (selectedGroup != null)
                //{

                //foreach (var p in updateCourse.Roster)
                //{
                //    if(p is Student)
                //    {
                //        studentService.UpdateAddGrade((p as Student), updateCourse);
                //    }
                //}
                //}
            }
            else{return;}

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

        private void UpdateContentItem(ContentItem item)
        {
            Console.WriteLine("What is the Content Item's name?");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the Content Items's description?");
            var description = Console.ReadLine() ?? string.Empty;
            item.Name = name;
            item.Description = description;
            if (item is AssignmentItem)
            {
                Console.WriteLine("How many points is the assignment worth?:");
                var points = Console.ReadLine() ?? string.Empty;
                ((AssignmentItem)item).Assignment.TotalAvailablePoints = int.Parse(points ?? "100");
                ((AssignmentItem)item).Assignment.Name = name;
                ((AssignmentItem)item).Assignment.Description = description;
            }
            else if (item is PageItem)
            {
                Console.WriteLine("Enter the HTML body for the page item:");
                var body = Console.ReadLine() ?? string.Empty;
                ((PageItem)item).HTMLBody = body;
            }
            else if (item is FileItem)
            {
                Console.WriteLine("Enter the path for the file item:");
                var path = Console.ReadLine() ?? string.Empty;
                ((FileItem)item).FilePath = path;
            }
        }

        private void EnterSubmissions(Course updateCourse)
        {
            if (updateCourse.AssignmentGroups.Exists(s => s.Assignments.Any()))
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
                                if (result <= assignment.TotalAvailablePoints && result >= 0)
                                {
                                    assignment.Submissions.Add(new Submission { Grade = result, Student = (student as Student) });
                                    studentService.UpdateAddGrade((student as Student), updateCourse);
                                    //Console.WriteLine(student);
                                }
                                else { Console.WriteLine("Error in submission entry"); }
                            }
                        }     
                    }
                }
            }
        }

        private void NavigateCourses()
        {
            bool cont = true;
            ListNavigator<Course> currentNavigator = courseNavigator;
            while (cont)
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Choose from the following options");

                foreach (var pair in currentNavigator.GetCurrentPage())
                {
                    Console.WriteLine($"{pair.Key}. {pair.Value}");
                }

                if (currentNavigator.HasPreviousPage)
                {
                    Console.WriteLine("(A)-previous page");
                }
                if (currentNavigator.HasNextPage)
                {
                    Console.WriteLine("(D)-next page");
                }
                Console.WriteLine("(Q)-quit\nID to print course info");
                var input = Console.ReadLine() ?? string.Empty;

                if (input.Equals("A", StringComparison.InvariantCultureIgnoreCase))
                {
                    currentNavigator.GoBackward();
                }
                else if (input.Equals("D", StringComparison.InvariantCultureIgnoreCase))
                {
                    currentNavigator.GoForward();
                }
                else if (input.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    cont = false;
                }
                else
                {
                    var inputID = int.Parse(input ?? "0");
                    var selCourse = currentNavigator.GetCurrentPage().FirstOrDefault(n => n.Key == inputID).Value ?? null;
                    if (selCourse != null)
                    {
                        Console.WriteLine("\n**********************");
                        Console.WriteLine($"{selCourse.Name}({selCourse.Code}) - {selCourse.Description}");
                        Console.WriteLine("-------Roster-------");
                        //courseService.FindCourse(query).Console.WriteLine($"{Name}({s.Code}) - {s.Description}\nRoster:"));
                        selCourse.Roster.ForEach(Console.WriteLine);
                        Console.WriteLine("-------Current Assignemnts-------");
                        foreach (var a in selCourse.AssignmentGroups)
                        {
                            Console.WriteLine(a);
                            a.Assignments.ForEach(Console.WriteLine);
                        }
                        Console.WriteLine("-------Current Modules-------");
                        foreach (var m in selCourse.Modules)
                        {
                            Console.WriteLine(m);
                            m.Content.ForEach(Console.WriteLine);
                        }
                        Console.WriteLine("-------Current Announcements-------");
                        selCourse.Announcements.ForEach(Console.WriteLine);

                        cont = false;
                    }
                }

            }
        }

        //public void SearchCourses()
        //{
        //    Console.WriteLine("Enter the code of the course your looking for:");
        //    ListCourses();
        //    var query = Console.ReadLine() ?? string.Empty;

        //    var selectedCourse = courseService.FindCourse(query);

        //    if (selectedCourse != null) {
        //        Console.WriteLine($"{selectedCourse.Name}({selectedCourse.Code}) - {selectedCourse.Description}");
        //        Console.WriteLine("-------Roster-------");
        //        //courseService.FindCourse(query).Console.WriteLine($"{Name}({s.Code}) - {s.Description}\nRoster:"));
        //        selectedCourse.Roster.ForEach(Console.WriteLine);
        //        Console.WriteLine("-------Current Assignemnts-------");
        //        foreach (var a in selectedCourse.AssignmentGroups)
        //        {
        //            Console.WriteLine(a);
        //            a.Assignments.ForEach(Console.WriteLine);
        //        }
        //        Console.WriteLine("-------Current Modules-------");
        //        foreach (var m in selectedCourse.Modules)
        //        {
        //            Console.WriteLine(m);
        //            m.Content.ForEach(Console.WriteLine);
        //        }
        //        //courseService.SearchCourses(query).ToList().ForEach(s => s.AssignmentGroups.ForEach(s => Console.WriteLine($"{s.Name} \n {s.Assignments.ForEach(Console.WriteLine)}")));
        //    }
        //}

        public void ListCourses()
        {
            NavigateCourses();
        }
	}
}

