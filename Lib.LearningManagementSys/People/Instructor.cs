using System;
namespace Lib.LearningManagementSys.People
{
	public class Instructor : Person 
	{
		public Instructor()
		{
		}
        public override string ToString()
        {
            return $"({Id})-{Name}: Instructor";
        }
    }
}

