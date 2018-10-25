using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primes;
using System.IO;
using System.Numerics;

namespace PrimeTester
{
	class Program
	{
		static void Main(string[] args)
		{
			foreach (BigInteger p in Generator.Primes)
				if (p > 20000000)
					break;
				else
					Console.WriteLine(p);
		}
	}
}
