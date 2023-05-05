using Lib.LearningManagementSys.People;

namespace Lib.LearningManagementSys.Item
{
    public class Course
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public SemesterClassification Classification { get; set; }

        public string Description { get; set; }

        public string Room { get; set; }

        public int CreditHours { get; set; }

        public List<Person> Roster { get; set; }

        public List<AssignmentGroup> AssignmentGroups { get; set; }

        public List<Module> Modules { get; set; }

        public List<Announcement> Announcements { get; set; }

        public Course()
        {
            Code = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Room = string.Empty;
            CreditHours = 0;
            Roster = new List<Person>();
            AssignmentGroups = new List<AssignmentGroup>();
            Modules = new List<Module>();
            Announcements = new List<Announcement>();
        }

        public override string ToString()
        {
            string s = $"({Name})-{Code}: /Room:{Room}/ /Semester:{Classification}/ /Description: {Description}/";
            return s;
            //return $"{Name}({Code})";
        }

        public AssignmentGroup? FindAssignmentGroup(string name)
        {
            return AssignmentGroups.FirstOrDefault(g => g.Name == name);
        }


    }
    public enum SemesterClassification
    {
        Fall, Spring, Summer
    }
}

