using System;
namespace RusianRoulette
{
	public class Person
	{
		// Character values
		private string name;
		private int hasHead;

		/*
		 * A constructor that takes a player name
		 */
		public Person(string name)
		{
			this.name = name;
			hasHead = 1;
		}

		/*
		 * Removes the character head
		 */
		public void Kill()
		{
			hasHead = 0;
		}

		/*
		 * Reports if a player has survived 
		 */
		public string Status()
		{
			string condition = null;
			if (hasHead == 0)
				condition = "dead.";

			return condition;
		}

		/*
		 * Returns the player name
		 */
		public string GetName()
		{
			return this.name;
		}
	}
}
