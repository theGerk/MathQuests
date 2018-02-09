using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
	class Program
	{
		static void Main(string[] args)
		{
			BigInteger a = new BigInteger();
			a = 10;
			Console.WriteLine(a.GetType().GetProperty("IsZero").GetValue(a));
			a = 0;
			Console.WriteLine(typeof(BigInteger).GetProperty("IsZero").GetValue(a));
		}
	}
}
