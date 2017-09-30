using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Troschuetz.Random;

namespace DistinctBallsInDistinctBoxes
{
	class Program
	{
		private static ThreadLocal<IGenerator> rng = new ThreadLocal<IGenerator>(() => new Troschuetz.Random.Generators.XorShift128Generator(12));
		public static IGenerator Rand {
			get { return rng.Value; }
			//set { rng.Value = value; }
		}

		static double Simulate(uint boxes, uint objects, uint simulations = 100000000)
		{
			ConcurrentBag<bool> results = new ConcurrentBag<bool>();

			Parallel.For(0, (int)simulations, (int foobar) => {
				bool[] output = new bool[boxes];

				for (uint i = 0; i < objects; i++)
					output[Rand.Next((int)boxes)] = true;

				if(output.All((p) => p))
					results.Add(true);
			});

			return (results.Count / (double)simulations) * Math.Pow(boxes, objects);
		}

		public static void Main(string[] args)
		{
			Console.WriteLine(Simulate(5, 8));
		}
	}
}
