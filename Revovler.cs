using System;
namespace RusianRoulette
{
	public class Revovler
	{
		// The revovler
		private const string MANUFACTOR = "Colt";
		private const string NAME = "Buntline";
		private const double CALIBER = .45;
		private const int CAPACITY = 6;

		// Some needed information for our game
		private int[] chamber = new int[CAPACITY];
		private int hammerPosition;
		public int loadedBullets
		{
			get;
			set;
		}

		// Other useful goodies
		private Random rand;


		/*
		 * The constructor loads the revovler with the given amount of bullets
		 */
		public Revovler(int bullets)
		{
			rand = new Random();
			loadedBullets = bullets;
			OrderBullets();
		}


		/*
		 * Arrange the bullets randomly in the chamber
		 */
		private void OrderBullets()
		{
			int val;
			for (int i = 0; i < loadedBullets;)
			{
				// TODO: Fails with nullreference
				val = rand.Next(0, 6);
				if (chamber[val] == 0)
				{
					chamber[val] = 1;
					i++;
				}
			}
		}


		/*
		 * Spin the chamber, leaving the hammer outsite a random chamber position
		 */
		public void SpinChamber()
		{
			hammerPosition = rand.Next(0, 6);
		}


		/*
		 * Pull the trigger and hope for the best
		 */
		public string PullTrigger()
		{
			string condition;
			if (chamber[hammerPosition] != 0)
			{
				condition = "BUM!";
				loadedBullets--;
			}
			else
			{
				condition = "Click.";
			}

			hammerPosition++;
			if (hammerPosition >= 6)
				hammerPosition = 0;
			
			return condition;
		}
	}
}
