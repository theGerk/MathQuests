using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
	public struct ModClass
	{
		public override string ToString()
		{
			return $"{Value} (mod {Mod})";
		}

		static ModClass Default { get; } = new ModClass(0, 1);
		static ModClass Empty { get; } = new ModClass(0, 0);

		public ModClass(BigInteger value, BigInteger mod)
		{
			Mod = mod;
			Value = value;
		}

		public BigInteger Mod { get; set; }
		public BigInteger Value { get; set; }

		public static ModClass Intersection(ModClass a, ModClass b)
		{
			//optimize by checking gcd first
			for (var guess = a.Value; guess < a.Mod * b.Mod; guess += a.Mod)
				if (guess % b.Mod == b.Value)
					return new ModClass(guess, a.Mod * b.Mod);
			return Empty;
		}

		public static ModClass intersect(params ModClass[] a)
		{
			ModClass current = a[0];
			for (int i = 1; i < a.Length; i++) {
				current = Intersection(current, a[i]);
			}
			return current;
		}

	}
}
