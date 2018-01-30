using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParalelPolymer
{
	public class Class1
	{
		public static Random rand = new Random();

		public static void Main(String[] args)
		{
			Console.WriteLine("How many simulations?");
			uint simulations = uint.Parse(Console.ReadLine());
			Console.WriteLine("How many monomers per polymer");
			uint polymerLength = uint.Parse(Console.ReadLine());
			double sum = 0;
			for (uint i = 0; i < simulations; i++) {
				double x = 0, y = 0;
				for (uint j = 0; j < polymerLength; j++) {
					double angle = rand.NextDouble() * 2 * Math.PI;
					x += Math.Cos(angle);
					y += Math.Sin(angle);
				}
				sum += Math.Sqrt(x * x + y * y);
			}
			Console.WriteLine(sum / simulations);
		}
	}
}
