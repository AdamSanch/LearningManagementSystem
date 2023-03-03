using System;
using Lib.LearningManagementSys.People;

namespace Lib.LearningManagementSys.Item
{
	public class Submission
	{
		//public Assignment Assignment { get; set; }

		public double Grade { get; set; }

		public Student Student { get; set; }

        public Submission()
		{
			Grade = 0;
			//Assignment = new Assignment();
			Student = new Student();
		}
	}
}

