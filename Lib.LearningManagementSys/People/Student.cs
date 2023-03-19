using System;
using Lib.LearningManagementSys.Item;

namespace Lib.LearningManagementSys.People
{
	public class Student : Person
	{
		//public double GPA { get; set; }

        public PersonClassification Classification { get; set; }

        public Dictionary<Course, GpaNumStruct> Grades { get; set; }

        public Student()
        {
            Grades = new Dictionary<Course,GpaNumStruct>();
        }

        public override string ToString()
        {
            string s = $"({Id})-{Name}: Student, {Classification}";
            foreach(var g in Grades)
            {
                s = s + $"\n({g.Key.Code}-{g.Value.numberGrade})";
            }
                return s;
        }
    }

    public enum PersonClassification
    {
        Freshman, Sophmore, Junior, Senior
    }
    public struct GpaNumStruct
    {
        public string letterGrade;
        public double numberGrade;
        public int minNumForLetter;
        public double gpaNumScale;
    }
}

