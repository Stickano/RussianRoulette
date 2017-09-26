using System;
namespace RusianRoulette
{
	public class Coin
	{
		// Heads and tals
		private const int HEADS = 1;
		private const int TAILS = 0;

		Random rand;

		public Coin()
		{
			rand = new Random();
		}

		/*
		 * Who will be pulling the trigger first?^
		 */
		public string Flip()
		{
			string condition;
			if (rand.Next(0, 2) == 1)
				condition = "Heads";
			else
				condition = "Tails";

			return condition;
		}
	}
}
