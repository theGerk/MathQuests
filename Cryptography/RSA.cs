using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography
{
	public class RSAPrivateKey
	{
		public BigInteger primeA;
		public BigInteger primeB;
		public BigInteger pulicKey { get => primeA * primeB; }
		public BigInteger EncryptionExponent;

		public RSAPrivateKey(BigInteger primeA, BigInteger primeB, BigInteger exp)
		{
			this.primeA = primeA;
			this.primeB = primeB;
			EncryptionExponent = exp;
		}

		public BigInteger decrypt(BigInteger cipher)
		{
			//(n-p1)(n-p2) = phi(n)
			BigInteger phi = (primeA - 1)*(primeB - 1);
			BigInteger exponent = modInverse(EncryptionExponent, phi);
			return BigInteger.ModPow(cipher, exponent, pulicKey);
		}

		private static BigInteger modInverse(BigInteger value, BigInteger modulus)
		{
			BigInteger i = modulus, v = 0, d = 1;
			while (value > 0) {
				BigInteger t = i / value, x = value;
				value = i % x;
				i = x;
				x = d;
				d = v - t * x;
				v = x;
			}
			v %= modulus;
			if (v < 0) v = (v + modulus) % modulus;
			return v;
		}
	}
}
