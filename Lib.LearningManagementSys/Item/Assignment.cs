using System;
namespace Lib.LearningManagementSys.Item
{
	public class Assignment
	{
        public string Name { get; set; }

        public string Description { get; set; }

		public int TotalAvailablePoints { get; set; }

		public DateTime DueDate { get; set; }

        public List<Submission> Submissions { get; set; }

        public Assignment()
		{
			Name = string.Empty;
			Description = string.Empty;
			TotalAvailablePoints = 0;
			DueDate = DateTime.Today;
            Submissions = new List<Submission>();
        }

        public override string ToString()
        {
            return $"{Name} (-/{TotalAvailablePoints}) - Due:{DueDate}";
        }
    }
}

