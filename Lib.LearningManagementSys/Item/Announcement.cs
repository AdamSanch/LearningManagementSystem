using System;
namespace Lib.LearningManagementSys.Item
{
	public class Announcement
	{
		public string Name { get; set; }
		public string Content { get; set; }
		public Announcement()
		{
			Name = string.Empty;
			Content = string.Empty;
        }

        public override string ToString()
        {
            return $"----\n*{Name}*\n{Content}\n----";
        }
    }
}

