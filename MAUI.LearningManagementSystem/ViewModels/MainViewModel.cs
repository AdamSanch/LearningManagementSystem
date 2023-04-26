using System;
using Lib.LearningManagementSys.People;

namespace MAUI.LearningManagementSystem.ViewModels
{
	public class MainViewModel
	{
		public MainViewModel()
		{
		}
        public List<Person> Students { get; set; } = new List<Person>();
    }
}

