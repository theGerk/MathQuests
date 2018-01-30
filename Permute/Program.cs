using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permute
{
	class Program
	{
		public static List<T[]>Permute<T>(ISet<T> Available) where T : IEquatable<T>
		{
			List<T[]> output = new List<T[]>();
			Stack<T> buildStack = new Stack<T>();
			Action<T[]> recurse = null;
			recurse = (T[] A) => {
				if (A.Count() >= 1) {
					foreach (T v in A) {
						buildStack.Push(v);
						recurse(A.Where(p => !p.Equals(v)).ToArray());
						buildStack.Pop();
					}
				} else {
					output.Add(buildStack.ToArray().ToArray());
				}
			};
			recurse(Available.ToArray());
			return output;
		}

		/*

		public static IEnumerable<T[]> Permutations<T>(ISet<T> available) where T : IEquatable<T>
		{
			Stack<T> buildStack = new Stack<T>();
			Stack<Hash
		}
			*/
		static void Main(string[] args)
		{
			var a = Permute(new HashSet<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }));
			foreach (var item in a) {
				StringBuilder s = new StringBuilder();
				foreach (var i in item) {
					s.Append(i);
				}
				Console.WriteLine(s.ToString());
			}
			Console.WriteLine(a.Count);
		}
	}
}
