using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primes;
using System.IO;

namespace PrimeTester
{
	class Program
	{
		static void Main(string[] args)
		{
			StringBuilder sb = new StringBuilder("[");
			foreach(var p in Primes.Generator.Primes.Take(10000)) {
				sb.Append(p);
				sb.Append(',');
			}
			sb.Length--;
			sb.Append(']');
			File.WriteAllText("..\\..\\output.txt", sb.ToString());
		}
	}
}
