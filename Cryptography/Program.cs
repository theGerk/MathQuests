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
