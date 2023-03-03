using System;
namespace Lib.LearningManagementSys.Item
{
	public class AssignmentItem : ContentItem
	{
		public AssignmentItem()
		{
			Assignment = new Assignment();
			
		}

		public Assignment Assignment { get; set; }
		

        public override string ToString()
        {
            return $"AI - {Name} - {Description}";
        }
    }
}

