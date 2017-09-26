using System;
using System.Collections.Generic;
using System.Threading;

namespace RusianRoulette
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			while (true)
			{

				Console.Clear();
				
				// A list to hold our players
				List<Person> personList = new List<Person>();

				// Our needed objects 
				Coin coin;
				Random rand;
				Revovler gun;

				// A nice little welcome message
				Console.WriteLine("Hello and welcome to a good ol' round of Russian Roulette!");
				Console.WriteLine("Type \"done\" to continue");
				Console.WriteLine();

				// Register players
				int br = 0;
				int eRR = 0;
				while (true)
				{
					br++;
					string name;

					// Give me your personal information!
					Console.WriteLine();
					Console.WriteLine("Player " + br + ":");
					Console.WriteLine("Enter player name: ");
					name = Console.ReadLine();

					// Continue when ready!
					if (name.ToLower().Equals("done") || name.Length == 0)
					{
						if (personList.Count >= 1)
						{
							break;
						}
						else
						{
							Console.WriteLine("Add at least 1 person to the game!");
							Console.WriteLine();
							eRR = 1;
							br--;
						}
					}

					// Create our player
					if (eRR != 1)
						personList.Add(new Person(name));
					eRR = 0;

				}


				// Singleplayer! How many rounds does the player have to survive?
				int rounds = 0;
				if (personList.Count == 1)
				{
					Console.WriteLine();
					Console.WriteLine("How many rounds does our hero have to survive? ");
					while (true)
					{
						string bulletCount = Console.ReadLine();
						if (int.TryParse(bulletCount, out rounds))
							break;

						Console.WriteLine("Use an integer value!");
					}
				}


				// How many bullets to load in the gun
				int bullets;
				Console.WriteLine();
				Console.WriteLine("How many bullets?");
				Console.Write("Our revovler can take a max of 6 bullets: ");
				while (true)
				{
					string bulletCount = Console.ReadLine();
					if (int.TryParse(bulletCount, out bullets) && bullets > 0 && bullets < 7)
						break;

					Console.WriteLine("Use an integer value! (1-6)");
				}

				// Load the gun
				gun = new Revovler(bullets);

				int playerTurn = 0;
				Console.WriteLine();

				// If more than 2 players, pick one to start at random
				if (personList.Count >= 3)
				{
					rand = new Random();
					Console.WriteLine("Alright, I'll pick one to start by random..");

					// Suspense!
					Thread.Sleep(450);
					Console.Write("Eeny");

					Thread.Sleep(450);
					Console.Write(", Meeny");

					Thread.Sleep(450);
					Console.Write(", Miny");

					Thread.Sleep(450);
					Console.Write(", Moe!");

					// Pick a player to start
					playerTurn = rand.Next(0, personList.Count);
					Console.WriteLine();
					Console.WriteLine(personList[playerTurn].GetName() + " starts!");
				}


				// If only 2 players, flip a coin
				if (personList.Count == 2)
				{

					// Our needed variables (and object)
					coin = new Coin();
					int coinPick;
					int settleConvert;
					string settledCoin;

					// Player 1, select Head or Tails, please!
					Console.WriteLine();
					Console.WriteLine(personList[0].GetName() + ", Head or Tails?");
					Console.Write("Head = 1,  Tals = 2: ");
					while (true)
					{
						string selectCoin = Console.ReadLine();
						if (int.TryParse(selectCoin, out coinPick) && coinPick == 1 || coinPick == 2)
							break;

						Console.WriteLine("Pick! 1 or 2!");
					}

					// Excellent choice *flip coin*
					Console.WriteLine();
					Console.WriteLine("*Flips the coin*");
					Thread.Sleep(1200);
					settledCoin = coin.Flip();
					Console.WriteLine();
					Console.WriteLine(settledCoin);
					Console.WriteLine();

					// Convert the land side to a value we can work with
					if (settledCoin.ToLower().Equals("heads"))
						settleConvert = 1;
					else
						settleConvert = 2;

					// Winner winner chicken dinner!
					if (coinPick == settleConvert)
					{
						Console.WriteLine(personList[0].GetName() + " is the lucky winner and will be starting!");
						playerTurn = 0;
					}
					else
					{
						Console.WriteLine(personList[1].GetName() + " got " + settledCoin + " and will be starting!");
						playerTurn = 1;
					}
				}


				// Click to continue
				Console.WriteLine("Push any key to continue..");
				Console.ReadKey();

				// Spin the chamber
				Console.WriteLine();
				Console.WriteLine("*Spin the Chamber*");
				Thread.Sleep(2000);
				gun.SpinChamber();

				// Start the battle
				int bodyCount = 0;
				int round = 0;
				while (true)
				{

					Console.Clear();
					round++;

					int playersLeft = personList.Count - bodyCount;
					Console.Write("Players: " + personList.Count);
					Console.Write("(" + playersLeft + "), ");
					Console.Write("Turn: " + personList[playerTurn].GetName());
					Console.Write(", Bullets: " + gun.loadedBullets);

					Console.WriteLine();
					Console.WriteLine();

					Thread.Sleep(1000);
					Console.WriteLine(personList[playerTurn].GetName() + " picks up the gun..");

					// Suspense!
					Thread.Sleep(600);
					Console.Write(".. points it at his head ");
					Thread.Sleep(800);
					Console.Write("..");
					Thread.Sleep(1200);
					Console.Write("...");

					// Fire the revovler!
					string shoot = gun.PullTrigger();
					Console.Write(" " + shoot);
					Console.WriteLine();

					// Did the player dieded?
					if (shoot.ToLower().Equals("bum!"))
					{
						personList[playerTurn].Kill();
						bodyCount++;
					}

					// Can we quit yet?
					if (gun.loadedBullets == 0 || bodyCount == personList.Count - 1)
					{
						if (personList.Count != 1)
						{
							Console.WriteLine();
							Console.WriteLine("The game has ended! ..Finally");
							if (gun.loadedBullets == 0)
								Console.WriteLine("We've run out of bullets..");
							else
								Console.WriteLine("We got a winner!");

							Console.WriteLine();
							Console.WriteLine("Press any key to continue..");
							Console.ReadKey();
							break;
						}
						else
						{
							if (personList[0].Status() != null)
							{
								Console.WriteLine("Oh.. What a mess.");
								Console.WriteLine();
								Console.WriteLine("Press any key to continue..");
								Console.ReadKey();
								break;
							}
							else if (round >= rounds)
							{
								Console.WriteLine("Oh man. He actually made it out alive. Congratulations!");
								Console.WriteLine();
								Console.WriteLine("Press any key to continue..");
								Console.ReadKey();
								break;
							}
						}
					}

					// Next in line
					playerTurn++;
					if (playerTurn >= personList.Count)
						playerTurn = 0;

					Console.WriteLine();
					Console.WriteLine("Click to continue..");
					Console.ReadKey();
				}

				Console.Clear();

				// How many survivors we got?
				List<Person> survivors = new List<Person>();
				foreach (Person player in personList)
				{
					if (player.Status() == null)
						survivors.Add(player);
				}

				// Congratulate the lucky players
				if (survivors.Count >= 1)
				{
					Console.WriteLine("Congratulation to our lucky contestants");
					foreach (Person player in survivors)
					{
						Console.WriteLine(player.GetName());
					}
				}
				else
				{
					if(survivors.Count != 0)
						Console.WriteLine("Congratulation, " + survivors[0].GetName() + ", that sure was a mind-blowing experience!");
					else
						Console.WriteLine("Unfortunately we have no survivors this round.");
				}

				// Play again?
				Console.WriteLine();
				Console.WriteLine("Play again?");
				Console.WriteLine("Yes/No");

				while (true)
				{
					string answer = Console.ReadLine();
					if (answer.ToLower().Equals("no") || answer.ToLower().Equals("n"))
						Environment.Exit(0);
					else if (answer.ToLower().Equals("yes") || answer.ToLower().Equals("y") || answer.Length == 0)
						break;
					else
						Console.WriteLine("I'm sorry, I didn't understand that?"); // Hue hue
				}
			}
		}
	}
}
