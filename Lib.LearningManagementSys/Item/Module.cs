using System;
namespace Lib.LearningManagementSys.Item
{
	public class Module
	{
        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<ContentItem>? Content { get; set; }

        public Module()
		{
		}
	}
}

