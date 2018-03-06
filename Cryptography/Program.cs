using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
	static class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 1 || !uint.TryParse(args[0], out uint b))
				Console.WriteLine("Need to pass in a positive integer followed by as many textual messages as you like");
			else {
				Console.WriteLine($"Base: {args[0]}");
				for (int i = 1; i < args.Length; i++)
					Console.WriteLine($"{args[i]} -> {encrypt(args[i], b)}");
			}
		}

		static BigInteger encrypt(string str, uint b)
		{
			BigInteger msg = 1;
			foreach(char c in str) {
				msg *= 100;
				if (c >= 'a' && c <= 'z')
					msg += c - 'a';
				else if (c >= 'A' && c <= 'Z')
					msg += c - 'A';
			}
			msg *= 10;
			msg += 1;
			return msg.flip(b);
		}

		static BigInteger flip(this BigInteger number, BigInteger b)
		{
			BigInteger output = 0;
			while(number != 0) {
				output *= b;
				output += number % b;
				number /= b;
			}
			return output;
		}

		static string EnglishString(this BigInteger self)
		{
			StringBuilder sb = new StringBuilder();
			while (self > 0) {
				sb.Append((char)((int)(self % 100) + 'a'));
				self /= 100;
			}
			return sb.ToString().Reverse();
		}

		public static string Reverse(this string s)
		{
			char[] charArray = s.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}
	}
}
