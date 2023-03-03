using System;
namespace Lib.LearningManagementSys.Item
{
	public class AssignmentItem : ContentItem
	{
		public AssignmentItem()
		{
			Assignment = new Assignment();
			Submissions = new List<Submission>();
		}

		public Assignment Assignment { get; set; }
		public List<Submission> Submissions { get; set; }

        public override string ToString()
        {
            return $"AI - {Name} - {Description}";
        }
    }
}

