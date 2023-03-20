using System;
namespace Lib.LearningManagementSys.Item
{
	public class FileItem : ContentItem
	{
		public FileItem()
		{
		}

        public string? FilePath{ get; set; }

        public override string ToString()
        {
            return $"FileItem - {Name} - {Description}";
        }
    }
}

