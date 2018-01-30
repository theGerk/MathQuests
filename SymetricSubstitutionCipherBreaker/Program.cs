using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learn;




namespace SymetricSubstitutionCipherBreaker
{
	namespace NotMyCode
	{
		// NOT MY CODE (translated from https://www.nayuki.io/res/automatic-caesar-cipher-breaker-javascript/automatic-caesar-cipher-breaker.js)
		public static class notMyCode
		{
			//frequency of each english letter on average
			public static readonly double[] ENGLISH_FREQS = new double[]{
		0.08167, 0.01492, 0.02782, 0.04253, 0.12702, 0.02228, 0.02015, 0.06094, 0.06966, 0.00153, 0.00772, 0.04025, 0.02406, 0.06749, 0.07507, 0.01929, 0.00095, 0.05987, 0.06327, 0.09056, 0.02758, 0.00978, 0.02360, 0.00150, 0.01974, 0.00074
	};

			// Returns the cross-entropy of the given string with respect to the English unigram frequencies, which is a positive floating-point number.
			public static double getEntropy(string str)
			{
				double sum = 0;
				for (var i = 0; i < str.Length; i++) {
					char c = str[i];
					sum += System.Math.Log(ENGLISH_FREQS[SymetricCipherMap.charToNumber(c)]);
				}
				return -sum / System.Math.Log(2) / (str.Length);
			}

			public static double getEntropy(IEnumerable<int> str)
			{
				double sum = 0;
				uint count = 0;
				foreach (var item in str) {
					sum += System.Math.Log(ENGLISH_FREQS[item]);
					count++;
				}
				return -sum / System.Math.Log(2) / (count);
			}
		}
	}
	//END OF NOT MY CODE BIT


	public class Program
	{
		public static Random rand = new Random();
		public const int LANGUAGE_SIZE = 26;

		static void Main(string[] args)
		{
			string cipherText;
			//set ciphertext
			{
				cipherText = "csoqeytonshgtqrhwjdnshonssmshjhyjeekasrwtqoryrjheoonsepgcjpsrkrojshosonsajxswqkthrorfcscsoqeytonatqynlsaorjhnrcbwsesaosweoassoeonsvqoosajhyasoasroetbaseocseehjynoejhthshjynolnsrkntoscerhwerdwqeoaseorqarhoedjontgeosaenscceeoassoeonrobtcctdcjpsroswjtqerayqvshotbjhejwjtqejhoshootcsrwgtqotrhtmsadnscvjhyuqseojthtnwthtorepdnrojejocsoqeytrhwvrpstqamjejojhonsattvonsdtvshltvsrhwytorcpjhytbvjlnscrhysctonsgscctdbtyonroaqfejoefrlpqkthonsdjhwtdkrhseonsgscctdevtpsonroaqfejoevqxxcsthonsdjhwtdkrhsecjlpswjoeothyqsjhotonsltahsaetbonssmshjhycjhysaswqkthonskttceonroeorhwjhwarjhecsobrccqkthjoefrlponsettoonrobrccebatvlnjvhsgeecjkkswfgonsosaarlsvrwsreqwwshcsrkrhwessjhyonrojodreretbotlotfsahjynolqacswthlsrftqoonsntqesrhwbsccrecsskrhwjhwsswonsasdjccfsojvsbtaonsgscctdevtpsonroecjwsercthyonseoassoaqffjhyjoefrlpqkthonsdjhwtdkrhseonsasdjccfsojvsonsasdjccfsojvsotkaskrasrbrlsotvssoonsbrlseonrogtqvssoonsasdjccfsojvsotvqawsarhwlasrosrhwojvsbtarcconsdtaperhwwrgetbnrhweonrocjborhwwatkruqseojththgtqakcrosojvsbtagtqrhwojvsbtavsrhwojvsgsobtarnqhwaswjhwsljejtherhwbtarnqhwaswmjejtherhwasmjejthefsbtasonsorpjhytbrotreorhwosrjhonsattvonsdtvshltvsrhwytorcpjhytbvjlnscrhysctrhwjhwsswonsasdjccfsojvsotdthwsawtjwrasrhwwtjwrasojvsotoqahfrlprhwwselshwonseorjadjonrfrcwektojhonsvjwwcstbvgnrjaonsgdjccergntdnjenrjajeyatdjhyonjhvgvtahjhyltrovgltccravtqhojhybjavcgotonslnjhvghslpojsajlnrhwvtwseofqoreesaoswfgrejvkcskjhonsgdjccergfqontdnjeraverhwcsyerasonjhwtjwraswjeoqafonsqhjmsaesjhrvjhqosonsasjeojvsbtawsljejtherhwasmjejthednjlnrvjhqosdjccasmsaesbtajnrmsphtdhonsvrccrcasrwgphtdhonsvrccnrmsphtdhonssmshjhyevtahjhyerbosahtthejnrmsvsreqaswtqovgcjbsdjonltbbssektthejphtdonsmtjlsewgjhydjonrwgjhybrccfshsrononsvqejlbatvrbraonsaattvetntdentqcwjkaseqvsrhwjnrmsphtdhonssgsercasrwgphtdhonsvrcconssgseonrobjzgtqjhrbtavqcroswknaresrhwdnshjrvbtavqcroswekardcjhythrkjhdnshjrvkjhhswrhwdajyycjhythonsdrcconshntdentqcwjfsyjhotekjotqorcconsfqooshwetbvgwrgerhwdrgerhwntdentqcwjkaseqvsrhwjnrmsphtdhonsravercasrwgphtdhonsvrccraveonrorasfarlscsoswrhwdnjosrhwfrasfqojhonscrvkcjynowtdhswdjoncjynofatdhnrjajejoksabqvsbatvrwaseeonrovrpsevsetwjyaseeraveonrocjsrcthyrorfcstadarkrftqorenrdcrhwentqcwjonshkaseqvsrhwntdentqcwjfsyjhenrccjergjnrmsythsrowqeponatqynhraatdeoassoerhwdrolnswonsevtpsonroajesebatvonskjksetbcthscgvshjhenjaoecssmsecsrhjhytqotbdjhwtdejentqcwnrmsfsshrkrjatbaryyswlcrdeelqoocjhyrlateeonsbcttaetbejcshoesrerhwonsrbosahtthonssmshjhyecsskeetksrlsbqccgevttonswfgcthybjhysaerecsskojaswtajovrcjhysaeeoasolnswthonsbcttansasfsejwsgtqrhwvsentqcwjrbosaosrrhwlrpserhwjlsenrmsonseoashyonotbtalsonsvtvshootjoelajejefqoontqynjnrmsdskorhwbreoswdskorhwkargswontqynjnrmsesshvgnsrwyatdhecjynocgfrcwfatqynojhqkthrkcroosajrvhtkatknsorhwnsasehtyasrovroosajnrmsesshonsvtvshotbvgyasrohseebcjlpsarhwjnrmsesshonssosahrcbttovrhntcwvgltrorhwehjlpsarhwjhentaojdrerbarjwrhwdtqcwjonrmsfsshdtaonjorbosarccrbosaonslqkeonsvravrcrwsonsosrrvthyonsktalscrjhrvthyetvsorcptbgtqrhwvsdtqcwjonrmsfsshdtaondnjcsotnrmsfjooshtbbonsvroosadjonrevjcsotnrmseuqssxswonsqhjmsaesjhotrfrccotatccjootdrawetvstmsadnscvjhyuqseojthotergjrvcrxraqeltvsbatvonswsrwltvsfrlpotosccgtqrccjenrccosccgtqrccjbthsesoocjhyrkjcctdfgnsansrwentqcwergonrojehtodnrojvsrhororcconrojehtojororccrhwdtqcwjonrmsfsshdtaonjorbosarccdtqcwjonrmsfsshdtaondnjcsrbosaonseqhesoerhwonswttagrawerhwonsekajhpcsweoassoerbosaonshtmscerbosaonsosrlqkerbosaonsepjaoeonrooarjcrcthyonsbcttarhwonjerhwetvqlnvtasjojejvkteejfcsotergiqeodnrojvsrhfqorejbrvryjlcrhosahonasdonshsamsejhkroosahethrelasshdtqcwjonrmsfsshdtaondnjcsjbthsesoocjhyrkjcctdtaonatdjhytbbrenrdcrhwoqahjhyotdrawonsdjhwtdentqcwergonrojehtojororcconrojehtodnrojvsrhororcchtjrvhtokajhlsnrvcsohtadrevsrhootfsrvrhrooshwrhoctawthsonrodjccwtotedsccrkatyaseeeoraorelshstaodtrwmjesonskajhlshtwtqforhsregottcwsbsashojrcycrwotfstbqesktcjojllrqojtqerhwvsojlqctqebqcctbnjyneshoshlsfqorfjotfoqesroojvsejhwsswrcvteoajwjlqctqercvteoroojvseonsbttcjyatdtcwjyatdtcwjenrccdsraonsftootvetbvgoatqesaeatccswenrccjkraovgnrjafsnjhwwtjwrasotsrorksrlnjenrccdsradnjosbcrhhscoatqesaerhwdrcpqkthonsfsrlnjnrmsnsrawonsvsavrjweejhyjhysrlnotsrlnjwthtoonjhponroonsgdjccejhyotvsjnrmsesshonsvajwjhyesrdrawthonsdrmseltvfjhyonsdnjosnrjatbonsdrmsefctdhfrlpdnshonsdjhwfctdeonsdrosadnjosrhwfcrlpdsnrmscjhysaswjhonslnrvfsaetbonsesrfgesryjacedasronswdjonesrdsswaswrhwfatdhojccnqvrhmtjlsedrpsqerhwdswatdh";
			}

			SymetricCipherMap m = new SymetricCipherMap();
			for (int i = 0; i < LANGUAGE_SIZE; i += 2)
				m[i] = i + 1;

			Dycryptor d = new Dycryptor(m, cipherText);
			Dycryptor dPrime = d;

			for(int i = 0; i < 1000; i++)
				d = (Dycryptor)d.RunGeneration(rand, .4, 300);

			Console.WriteLine(d.internalMap.encyrptMessage(cipherText));
		}
	}


	public class SymetricCipherMap : ICloneable
	{
		public static int charToNumber(char c)
		{
			return c - 'a';
		}

		public static char numberToChar(int n)
		{
			return (char)(n + 'a');
		}

		public static int[] strToNumberArray(string s)
		{
			return s.Select(charToNumber).ToArray();
		}

		public static string numberArrayToString(IEnumerable<int> msg)
		{
			return new String(msg.Select(numberToChar).ToArray());
		}

		public const int UNUSED = Program.LANGUAGE_SIZE;
		public int[] map = new int[Program.LANGUAGE_SIZE];
		public int this[int c] 
		{
			get
			{
				return map[c];
			}

			set
			{
				int oldC = map[c];
				int oldValue = map[value];
				map[c] = value;
				map[value] = c;

				if (oldC != UNUSED && oldValue != UNUSED) {
					map[oldC] = oldValue;
					map[oldValue] = oldC;
				} else {
					if (oldC != UNUSED)
						map[oldC] = UNUSED;
					if (oldValue != UNUSED)
						map[oldValue] = UNUSED;
				}
			}
		}

		public SymetricCipherMap()
		{
			for (int i = 0; i < map.Length; i++)
				map[i] = UNUSED;
		}
		public SymetricCipherMap(SymetricCipherMap other)
		{
			map = (int[])other.map.Clone();
		}

		bool isSet(int c)
		{
			return map[c] != UNUSED;
		}

		public IEnumerable<int> encyrptMessage(IEnumerable<int> plainText)
		{
			if (map.Contains(UNUSED))
				throw new Exception("Cipher not set up");

			return plainText.Select((int c) => map[c]);
		}

		public IEnumerable<char> encyrptMessage(IEnumerable<char> plainText)
		{
			return encyrptMessage(plainText.Select(charToNumber)).Select(numberToChar);
		}

		public string encyrptMessage(string plainText)
		{
			return new string(encyrptMessage((IEnumerable<char>)plainText).ToArray());
		}

		public object Clone()
		{
			return new SymetricCipherMap(this);
		}
	}


	public class Dycryptor : Learn.IEvolve
	{
		public readonly int[] plainText;
		public Dycryptor(SymetricCipherMap map, string plainTextMsg)
		{
			internalMap = map;
			plainText = SymetricCipherMap.strToNumberArray(plainTextMsg);
		}
		public Dycryptor(SymetricCipherMap map, int[] msg)
		{
			internalMap = map;
			plainText = msg;
		}
		public SymetricCipherMap internalMap { get; }
		public object Clone()
		{
			return new Dycryptor((SymetricCipherMap)internalMap.Clone(), plainText);
		}

		public double GetFitness()
		{
			return -NotMyCode.notMyCode.getEntropy(plainText);
		}

		public void Mutate()
		{
			int set, to;
			do {
				set = Program.rand.Next(Program.LANGUAGE_SIZE);
				to = Program.rand.Next(Program.LANGUAGE_SIZE);
			} while (set == to);
			internalMap[set] = to;
		}
	}
}


namespace Learn
{
	public interface IEvolve : ICloneable
	{
		double GetFitness();
		void Mutate();
	}

	public static class Weasel
	{
		public static IEvolve RunGeneration(this IEvolve parent, Random rand, double noMutationProbability = .5, uint numberOfChildren = 100)
		{
			IEvolve bestChild = parent;
			double bestFitness = bestChild.GetFitness();

			for (int i = 0; i < numberOfChildren; i++) {
				var current = (IEvolve)parent.Clone();
				while (rand.NextDouble() >= noMutationProbability)
					current.Mutate();

				double currentFitness = current.GetFitness();
				if (currentFitness > bestFitness) {
					bestChild = current;
					bestFitness = currentFitness;
				}
			}

			return bestChild;
		}
	}
}