using System;
namespace Lib.LearningManagementSys.Item
{
	public class AssignmentGroup
	{
		public string Name { get; set; }
		public int Weight { get; set; }
		public List<Assignment> Assignments { get; set; }

        public AssignmentGroup()
		{
			Name = String.Empty;
			Weight = 0;
			Assignments = new List<Assignment>();
		}

        public override string ToString()
        {
            return $"-- {Name} --";
        }
    }
}

