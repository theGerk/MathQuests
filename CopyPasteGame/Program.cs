using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public class CopyPasteGame : ICloneable, IEquatable<CopyPasteGame>
	{
		public const uint CopyCost = 4;

		public CopyPasteGame(uint printed = 1, uint buffer = 1)
		{
			Count = printed;
			Buffer = buffer;
			//Time = time;
		}

		public CopyPasteGame(CopyPasteGame copyPasteGame)
		{
			Count = copyPasteGame.Count;
			Buffer = copyPasteGame.Buffer;
			//Time = copyPasteGame.Time;
			history = new StringBuilder(copyPasteGame.history.ToString());
		}

		public BigInteger Count { get; private set; }
		public BigInteger Buffer { get; private set; }
		//public uint Time { get; private set; }
		public String History { get { return history.ToString(); } }
		private StringBuilder history = new StringBuilder();

		public CopyPasteGame Print()
		{
			history.Append('p');
			Count += Buffer;
			//Time++;
			return this;
		}
		public CopyPasteGame Copy()
		{
			history.Append('c');
			Buffer = Count;
			Count += Buffer;
			//Time += CopyCost;
			return this;
		}

		public object Clone()
		{
			return new CopyPasteGame(this);
		}

		public bool Equals(CopyPasteGame other)
		{
			if (other == null)
				return false;
			else
				return history == other.history;
		}

		public string toString()
		{
			return $"{history.ToString()} --- {Count.ToString()}";
		}
	}


	class Program
	{
		static void Main(string[] args)
		{

			//setup
			Queue<CopyPasteGame> printQueue = new Queue<CopyPasteGame>();
			Queue<CopyPasteGame> copyQueue = new Queue<CopyPasteGame>();

			{
				CopyPasteGame cpg = new CopyPasteGame();
				for(int i = 0; i < CopyPasteGame.CopyCost; i++)
					copyQueue.Enqueue(new CopyPasteGame(cpg.Print()));
				printQueue.Enqueue(cpg);
			}


			const int print = 50000;
			for (uint i = 0; i < print; i++) {
				Queue<CopyPasteGame> newPrintQueue = new Queue<CopyPasteGame>();
				CopyPasteGame winner = copyQueue.Dequeue().Copy();
				newPrintQueue.Enqueue(winner);
				while (printQueue.Count > 0) {
					CopyPasteGame tmp = printQueue.Dequeue().Print();
					if (tmp.Count > winner.Count) {
						winner = tmp;
						newPrintQueue.Enqueue(winner);
					}
				}
				copyQueue.Enqueue(new CopyPasteGame(winner));
				printQueue = newPrintQueue;
				if(i > print - 100) {
					uint p = 0;
					uint c = 0;
					foreach(char d in winner.History) {
						switch (d) {
							case 'p':
								p++;
								break;
							case 'c':
								c++;
								break;
							default:
								break;
						}
					}

					Console.WriteLine((double)p / (double)c);
				}
			}
		}
	}
}
