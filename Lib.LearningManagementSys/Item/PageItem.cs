﻿using System;
namespace Lib.LearningManagementSys.Item
{
	public class PageItem : ContentItem
	{
		public PageItem()
		{
		}

        public string? HTMLBody { get; set; }

        public override string ToString()
        {
            return $"PageItem - {Name} - {Description}";
        }
    }
}

