using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Benji
{
	namespace Math
	{
		[Serializable, System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
		public struct Rational : IComparable<Rational>, IComparable<BigInteger>, IComparable<double>, IConvertible, IFormattable, IEquatable<Rational>
		{
			public BigInteger numerator { get; }
			public BigInteger denominator { get; }

			public Rational(BigInteger numerator, BigInteger denominator, bool safe = true)
			{
				if (denominator.IsZero)
					throw new ArgumentException("denominator can not be zero");
				BigInteger gcd = BigInteger.GreatestCommonDivisor(numerator, denominator) * denominator.Sign;
				this.numerator = numerator / gcd;
				this.denominator = denominator / denominator.Sign;
			}

			private Rational(BigInteger numerator, BigInteger denominator)
			{
				this.numerator = numerator;
				this.denominator = denominator;
			}

			public Rational(double other)
			{
				//TODO figure out how to do
				throw new NotImplementedException();
			}

			public Rational(decimal other)
			{
				throw new NotImplementedException();
			}

			public override string ToString()
			{
				return denominator == 1 ? numerator.ToString() : $"{numerator}/{denominator}";
			}

			public string ToString(string format, IFormatProvider formatProvider = null)
			{
				if (format == null)
					return ToString();
				switch (format.ToLowerInvariant()) {
					case "":
						return ToString();
					case "tex":
						return $"\frac{{{numerator}}}{{{denominator}}}";
					default:
						throw new FormatException($"Invalid format string: \"{format}\"");
				}
			}

			public TypeCode GetTypeCode()
			{
				return TypeCode.Object;
			}

			public BigInteger ToBigInteger()
			{
				return numerator / denominator;
			}

			public bool ToBoolean(IFormatProvider provider = null)
			{
				return numerator != 0;
			}

			public char ToChar(IFormatProvider provider = null)
			{
				return (char)ToBigInteger();
			}

			public sbyte ToSByte(IFormatProvider provider = null)
			{
				return (sbyte)ToBigInteger();
			}

			public byte ToByte(IFormatProvider provider = null)
			{
				return (byte)ToBigInteger();
			}

			public short ToInt16(IFormatProvider provider = null)
			{
				return (short)ToBigInteger();
			}

			public ushort ToUInt16(IFormatProvider provider = null)
			{
				return (ushort)ToBigInteger();
			}

			public int ToInt32(IFormatProvider provider = null)
			{
				return (int)ToBigInteger();
			}

			public uint ToUInt32(IFormatProvider provider = null)
			{
				return (uint)ToBigInteger();
			}

			public long ToInt64(IFormatProvider provider = null)
			{
				return (long)ToBigInteger();
			}

			public ulong ToUInt64(IFormatProvider provider = null)
			{
				return (uint)ToBigInteger();
			}

			public float ToSingle(IFormatProvider provider = null)
			{
				return (float)ToDouble();
			}

			public double ToDouble(IFormatProvider provider = null)
			{
				return (double)numerator / (double)denominator;
			}

			public decimal ToDecimal(IFormatProvider provider = null)
			{
				return (decimal)numerator / (decimal)denominator;
			}

			public DateTime ToDateTime(IFormatProvider provider = null)
			{
				return Convert.ToDateTime(ToInt64());
			}

			public string ToString(IFormatProvider provider)
			{
				return ToString();
			}

			public object ToType(Type conversionType, IFormatProvider provider = null)
			{
				return Convert.ChangeType(ToDouble(), conversionType);
			}

			public int CompareTo(double other)
			{
				return CompareTo(new Rational(other));
			}

			public int CompareTo(BigInteger other)
			{
				return numerator.CompareTo(other * denominator);
			}

			public int CompareTo(Rational other)
			{
				return (this.numerator * other.denominator).CompareTo(other.numerator * this.denominator);
			}

			public override bool Equals(object obj)
			{
				return obj is Rational && Equals((Rational)obj);
			}

			public bool Equals(Rational other)
			{
				return numerator.Equals(other.numerator) &&
					   denominator.Equals(other.denominator);
			}

			public override int GetHashCode()
			{
				var hashCode = -671859081;
				hashCode = hashCode * -1521134295 + base.GetHashCode();
				hashCode = hashCode * -1521134295 + EqualityComparer<BigInteger>.Default.GetHashCode(numerator);
				hashCode = hashCode * -1521134295 + EqualityComparer<BigInteger>.Default.GetHashCode(denominator);
				return hashCode;
			}

			public static Rational operator +(Rational operand)
			{
				return operand;
			}

			public static Rational operator -(Rational operand)
			{
				return new Rational(-operand.numerator, operand.denominator);
			}

			public static Rational operator ++(Rational operand)
			{
				return new Rational(operand.numerator + operand.denominator, operand.denominator);
			}

			public static Rational operator --(Rational operand)
			{
				return new Rational(operand.numerator - operand.denominator, operand.denominator);
			}

			public static Rational operator +(Rational left, Rational right)
			{
				BigInteger numerator = left.numerator * right.denominator + right.numerator * left.denominator;
				BigInteger denominator = left.denominator * right.denominator;
				BigInteger gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
				return new Rational(numerator / gcd, denominator / gcd);
			}

			public static Rational operator -(Rational left, Rational right)
			{
				BigInteger numerator = left.numerator * right.denominator - right.numerator * left.denominator;
				BigInteger denominator = left.denominator * right.denominator;
				BigInteger gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
				return new Rational(numerator / gcd, denominator / gcd);
			}

			public static Rational operator *(Rational left, Rational right)
			{
				BigInteger numerator = left.numerator * right.numerator;
				BigInteger denominator = left.denominator * right.denominator;
				BigInteger gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
				return new Rational(numerator / gcd, denominator / gcd);
			}

			public static Rational operator /(Rational left, Rational right)
			{
				BigInteger numerator = left.numerator * right.denominator;
				BigInteger denominator = left.denominator * right.numerator;
				BigInteger gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
				return new Rational(numerator / gcd, denominator / gcd);
			}

			public static bool operator ==(Rational left, Rational right)
			{
				return right.numerator == left.numerator && right.denominator == left.denominator;
			}

			public static bool operator !=(Rational left, Rational right)
			{
				return right.numerator != left.numerator || right.denominator != left.denominator;
			}

			public static bool operator >(Rational left, Rational right)
			{
				return left.numerator * right.denominator > right.numerator * left.denominator;
			}

			public static bool operator <(Rational left, Rational right)
			{
				return left.numerator * right.denominator < right.numerator * left.denominator;
			}

			public static bool operator >=(Rational left, Rational right)
			{
				return left.numerator * right.denominator >= right.numerator * left.denominator;
			}

			public static bool operator <=(Rational left, Rational right)
			{
				return left.numerator * right.denominator <= right.numerator * left.denominator;
			}

			//TODO define casting
			public static implicit operator Rational(BigInteger value)
			{
				return new Rational(value, 1);
			}

			public static implicit operator Rational(int value)
			{
				return new Rational(value, 1);
			}

			public static implicit operator Rational(uint value)
			{
				return new Rational(value, 1);
			}

			public static Rational Zero => new Rational(0, 1);
			public static Rational One => new Rational(1, 1);
			public static Rational Half => new Rational(1, 2);
		}
	}
}