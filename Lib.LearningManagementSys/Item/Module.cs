using System;
namespace Lib.LearningManagementSys.Item
{
	public class Module
	{
        public string Name { get; set; }

        public string Description { get; set; }

        public List<ContentItem> Content { get; set; }

        public Module()
		{
			Content = new List<ContentItem>();
			Name = string.Empty;
            Description = string.Empty;

        }

        public override string ToString()
        {
            return $"({Name})-{Description}";
        }
    }
}

