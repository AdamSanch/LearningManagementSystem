using System;
using Lib.LearningManagementSys.Item;
using Lib.LearningManagementSys.People;

namespace Lib.LearningManagementSys.Services
{

    public class StudentService
    {
        private List<Person> studentList;

        private List<GpaNumStruct> gradeRanges = new List<GpaNumStruct>
        {
            {  new GpaNumStruct { letterGrade = "A", minNumForLetter = 94, gpaNumScale = 4.0 } },
            {  new GpaNumStruct { letterGrade = "A-", minNumForLetter = 90, gpaNumScale = 3.7 } },
            {  new GpaNumStruct { letterGrade = "B+", minNumForLetter = 87, gpaNumScale = 3.3 } },
            {  new GpaNumStruct { letterGrade = "B", minNumForLetter = 84, gpaNumScale = 3.0 } },
            {  new GpaNumStruct { letterGrade = "B-", minNumForLetter = 80, gpaNumScale = 2.7 } },
            {  new GpaNumStruct { letterGrade = "C+", minNumForLetter = 77, gpaNumScale = 2.3 } },
            {  new GpaNumStruct { letterGrade = "C",  minNumForLetter = 74, gpaNumScale = 2.0 } },
            {  new GpaNumStruct { letterGrade = "C-", minNumForLetter = 70, gpaNumScale = 1.7 } },
            {  new GpaNumStruct { letterGrade = "D+", minNumForLetter = 67, gpaNumScale = 1.3 } },
            {  new GpaNumStruct { letterGrade = "D", minNumForLetter = 64, gpaNumScale = 1.0 } },
            {  new GpaNumStruct { letterGrade = "D-", minNumForLetter = 60, gpaNumScale = 0.7 } },
            {  new GpaNumStruct { letterGrade = "F", minNumForLetter = 0, gpaNumScale = 0.0 } }
        };

        private static StudentService? instance;

        private StudentService()
        {
            studentList = new List<Person>();
        }

        public static StudentService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new StudentService();
                }

                return instance;
            }
        }

        public void Add(Person person)
        {
            studentList.Add(person);
        }

        public List<Person> Students
        {
            get
            {
                return studentList;
            }
        }

        public IEnumerable<Person> SearchStudents(string name)
        {
            return studentList.Where(s => s.Name.ToUpper().Contains(name.ToUpper()));
        }
        public Person? GetPerson(int id)
        {
            return studentList.FirstOrDefault(p => p.Id == id);
        }
        public void UpdateAddGrade(Student s, Course c)
        {
            double finalGrade = 0;
            int AssignGroupLeftover = 100;
            foreach (var AG in c.AssignmentGroups)
            {
                double assiGroupGrade = 0;
                int numOfSubmissions = 0;
                foreach (var A in AG.Assignments)
                {
                    var submission = A.Submissions.FirstOrDefault(x => x.Student.Id == s.Id);
                    if (submission != null)
                    {
                        if (numOfSubmissions == 0) { AssignGroupLeftover = AssignGroupLeftover - AG.Weight; }
                        numOfSubmissions++;
                        assiGroupGrade = assiGroupGrade + (submission.Grade / A.TotalAvailablePoints);
                    }
                }
                if (numOfSubmissions != 0)
                {
                    finalGrade = finalGrade + ((assiGroupGrade / numOfSubmissions) * AG.Weight);
                }
                finalGrade = finalGrade + AssignGroupLeftover;
                if (finalGrade > 100) { finalGrade = 100; }
                int i = 0;
                while (finalGrade < gradeRanges.ElementAt(i).minNumForLetter) { i++; }
                var studentGradeStruct = gradeRanges.ElementAt(i);
                studentGradeStruct.numberGrade = finalGrade;
                if (!s.Grades.Any(s => s.Key == c))
                {
                    s.Grades.Add(c, studentGradeStruct);
                }
                else
                {
                    s.Grades[c] = studentGradeStruct;
                }
            }
        }

        public double CalculateGPA(Person stu)
        {
            var s = (stu as Student);
            double GPA = 4.0;
            int totalHours = 0;
            double totalPointsEarned = 0;
            foreach(var grade in s.Grades)
            {
                totalPointsEarned += (grade.Key.CreditHours * grade.Value.gpaNumScale);
                totalHours += grade.Key.CreditHours;
            }
            if (totalHours > 0)
            {
                GPA = (totalPointsEarned / totalHours);
            }
            return GPA;
        }
        }
}

    


