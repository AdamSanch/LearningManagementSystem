using System;
namespace Lib.LearningManagementSys.People
{
	public class TeachingAssistant : Person
	{
		public TeachingAssistant()
		{
		}

        public override string ToString()
        {
            return $"({Id})-{Name}: Teaching Assistant";
        }
    }
}

