using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primes
{
	//make thread safe? possibly make thread safe alternitive version
    public static class Generator
    {
		public static IEnumerable<uint> Primes {
			get
			{
				for(int i = 0; ; i++) {
					if (generatedPrimes.Count > i)
						yield return generatedPrimes[i];
					else
						yield return generateNextPrime();
				}
			}
		}
		private static List<uint> generatedPrimes = new List<uint>{ 2, 3, 5, 7, 11 };

		/// <summary>
		/// Gets n'th prime, considers 2 to be the 0th prime
		/// </summary>
		/// <param name="n">which prime to get</param>
		/// <returns>The nth prime number</returns>
		public static uint Prime(uint n)
		{
			while (generatedPrimes.Count <= n) {
				generateNextPrime();
			}
			return generatedPrimes[(int)n];
		}

		/// <summary>
		/// Adds 1 number to the generated Primes list
		/// </summary>
		private static uint generateNextPrime()
		{
			var guess = generatedPrimes[generatedPrimes.Count - 1] + 2;
			for (; !isPrime(guess); guess += 2) ;
			generatedPrimes.Add(guess);
			return guess;
		}

		/// <summary>
		/// Only works for numbers within reasonable range of generated primes list
		/// </summary>
		/// <param name="number">number, must be no greater then the last generated prime squared</param>
		/// <returns>if numb is prime</returns>
		private static bool isPrime(uint number)
		{
			for(int i = 0; i * i < number && i < generatedPrimes.Count; i++) {
				if (number % generatedPrimes[i] == 0)
					return false;
			}
			return true;
		}
	}
}
