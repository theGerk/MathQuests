#include <iostream>
#include <string>
#include <numeric>
#include <vector>
#include <algorithm>

const char LANGUAGE_SIZE = 26;

//all these work only on a language of 26 lower case english letters
std::string ceaserCipher(const std::string& plainText, int offset);

std::string alphineCipher(const std::string& plainText, int multiplier, int offset);

std::string arbitraryBijectionCipher(const std::string& plainText, const unsigned char map[]);

//takes a character in either 'a' to 'z' or 'A' to 'Z' and converts it into a number 0 to 25
constexpr char charToNum(char c);

//takes a number from 0 to 25 and maps it back to a letter 'a' through 'z'
constexpr char numToChar(char n);

//returns number k such that 0 <= k < modulo and input is congruent to k (mod modulo)
constexpr int normalizeInMod(int input, unsigned int modulo);

// NOT MY CODE (translated from https://www.nayuki.io/res/automatic-caesar-cipher-breaker-javascript/automatic-caesar-cipher-breaker.js)
namespace notMyCode 
{
	//frequency of each english letter on average
	const double ENGLISH_FREQS[]{
		0.08167, 0.01492, 0.02782, 0.04253, 0.12702, 0.02228, 0.02015, 0.06094, 0.06966, 0.00153, 0.00772, 0.04025, 0.02406,
			0.06749, 0.07507, 0.01929, 0.00095, 0.05987, 0.06327, 0.09056, 0.02758, 0.00978, 0.02360, 0.00150, 0.01974, 0.00074,
	};

	// Returns the cross-entropy of the given string with respect to the English unigram frequencies, which is a positive floating-point number.
	double getEntropy(const std::string& str) {
		double sum = 0;
		for (size_t i = 0; i < str.size(); i++) {
			char c = str[i];
			sum += std::log(ENGLISH_FREQS[charToNum(c)]);
		}
		return -sum / std::log(2) / (str.size());
	}
}
//END OF NOT MY CODE BIT

//stores information about a alphine cipher guess with entropy
struct AlphineEntropyGuess {
	std::string plaintText;
	int multiplier;
	int offset;
	double entropy;

	AlphineEntropyGuess(const std::string& plainText, int multiplier, int offset, double entropy)
		: plaintText(plaintText)
		, multiplier(multiplier)
		, offset(offset)
		, entropy(entropy) {}

	AlphineEntropyGuess() {}

	//allows for sorting by entorpy
	inline bool operator<(AlphineEntropyGuess other)
	{
		return entropy < other.entropy;
	}
};


int main()
{
	std::string ceaser;
	std::string alphine;
	std::string substitution;

	std::string name1;
	std::string name2;
	std::string name3;
	
	//set strings
	{
		ceaser = "xbpztpeprilxiwndjlpailwxibpcxwpktstithitsndjadcvtcdjvwxrdbtidndjphpvgdlcrwxaslwdwphwpspexvwtpstsupiwtgxpbdastcdjvwcdlidbpztugxtcshxilphndjiwpiqgdztiwtctllddscdlxhpixbtudgrpgkxcvltwpktdcthpepcsdctgddiatiiwtgtqtrdbbtgrtqtilttcjh";
		alphine = "osjjtrkxqjjkxqvdcgocgsxitrkueioejsxqfksfzdczrcuarcbcfyctorkfoebioqfoebciokftoexzijkttrkakforkizsazjkcfieorzxkiisitrkwsxkeikztqaksxsfzjkttrkdqwidxcfgvjqakxicfjsituqftrifkaibsbkxijktdkdkvcfsjkqvikkutrkqfjwkubkxqxcitrkkubkxqxqvcokoxksutsykvxqutrkzxkiikxqvzksjjsoycfgtrktrxkkgjsiiyfqditrstirkktqfarcorirkkudxqczkxkzvsftscjiqfoksfzibxkszctiqsitqoqpkxrkxvsokcvrkxrqxfwvkktbxqtxezktrkwoquktqirqarqaoqjzirkcisfzzeudjkttrkjsubsvvclctidksutrkqfjwkubkxqxcitrkkubkxqxqvcokoxksu";
		substitution = "csoqeytonshgtqrhwjdnshonssmshjhyjeekasrwtqoryrjheoonsepgcjpsrkrojshosonsajxswqkthrorfcscsoqeytonatqynlsaorjhnrcbwsesaosweoassoeonsvqoosajhyasoasroetbaseocseehjynoejhthshjynolnsrkntoscerhwerdwqeoaseorqarhoedjontgeosaenscceeoassoeonrobtcctdcjpsroswjtqerayqvshotbjhejwjtqejhoshootcsrwgtqotrhtmsadnscvjhyuqseojthtnwthtorepdnrojejocsoqeytrhwvrpstqamjejojhonsattvonsdtvshltvsrhwytorcpjhytbvjlnscrhysctonsgscctdbtyonroaqfejoefrlpqkthonsdjhwtdkrhseonsgscctdevtpsonroaqfejoevqxxcsthonsdjhwtdkrhsecjlpswjoeothyqsjhotonsltahsaetbonssmshjhycjhysaswqkthonskttceonroeorhwjhwarjhecsobrccqkthjoefrlponsettoonrobrccebatvlnjvhsgeecjkkswfgonsosaarlsvrwsreqwwshcsrkrhwessjhyonrojodreretbotlotfsahjynolqacswthlsrftqoonsntqesrhwbsccrecsskrhwjhwsswonsasdjccfsojvsbtaonsgscctdevtpsonroecjwsercthyonseoassoaqffjhyjoefrlpqkthonsdjhwtdkrhseonsasdjccfsojvsonsasdjccfsojvsotkaskrasrbrlsotvssoonsbrlseonrogtqvssoonsasdjccfsojvsotvqawsarhwlasrosrhwojvsbtarcconsdtaperhwwrgetbnrhweonrocjborhwwatkruqseojththgtqakcrosojvsbtagtqrhwojvsbtavsrhwojvsgsobtarnqhwaswjhwsljejtherhwbtarnqhwaswmjejtherhwasmjejthefsbtasonsorpjhytbrotreorhwosrjhonsattvonsdtvshltvsrhwytorcpjhytbvjlnscrhysctrhwjhwsswonsasdjccfsojvsotdthwsawtjwrasrhwwtjwrasojvsotoqahfrlprhwwselshwonseorjadjonrfrcwektojhonsvjwwcstbvgnrjaonsgdjccergntdnjenrjajeyatdjhyonjhvgvtahjhyltrovgltccravtqhojhybjavcgotonslnjhvghslpojsajlnrhwvtwseofqoreesaoswfgrejvkcskjhonsgdjccergfqontdnjeraverhwcsyerasonjhwtjwraswjeoqafonsqhjmsaesjhrvjhqosonsasjeojvsbtawsljejtherhwasmjejthednjlnrvjhqosdjccasmsaesbtajnrmsphtdhonsvrccrcasrwgphtdhonsvrccnrmsphtdhonssmshjhyevtahjhyerbosahtthejnrmsvsreqaswtqovgcjbsdjonltbbssektthejphtdonsmtjlsewgjhydjonrwgjhybrccfshsrononsvqejlbatvrbraonsaattvetntdentqcwjkaseqvsrhwjnrmsphtdhonssgsercasrwgphtdhonsvrcconssgseonrobjzgtqjhrbtavqcroswknaresrhwdnshjrvbtavqcroswekardcjhythrkjhdnshjrvkjhhswrhwdajyycjhythonsdrcconshntdentqcwjfsyjhotekjotqorcconsfqooshwetbvgwrgerhwdrgerhwntdentqcwjkaseqvsrhwjnrmsphtdhonsravercasrwgphtdhonsvrccraveonrorasfarlscsoswrhwdnjosrhwfrasfqojhonscrvkcjynowtdhswdjoncjynofatdhnrjajejoksabqvsbatvrwaseeonrovrpsevsetwjyaseeraveonrocjsrcthyrorfcstadarkrftqorenrdcrhwentqcwjonshkaseqvsrhwntdentqcwjfsyjhenrccjergjnrmsythsrowqeponatqynhraatdeoassoerhwdrolnswonsevtpsonroajesebatvonskjksetbcthscgvshjhenjaoecssmsecsrhjhytqotbdjhwtdejentqcwnrmsfsshrkrjatbaryyswlcrdeelqoocjhyrlateeonsbcttaetbejcshoesrerhwonsrbosahtthonssmshjhyecsskeetksrlsbqccgevttonswfgcthybjhysaerecsskojaswtajovrcjhysaeeoasolnswthonsbcttansasfsejwsgtqrhwvsentqcwjrbosaosrrhwlrpserhwjlsenrmsonseoashyonotbtalsonsvtvshootjoelajejefqoontqynjnrmsdskorhwbreoswdskorhwkargswontqynjnrmsesshvgnsrwyatdhecjynocgfrcwfatqynojhqkthrkcroosajrvhtkatknsorhwnsasehtyasrovroosajnrmsesshonsvtvshotbvgyasrohseebcjlpsarhwjnrmsesshonssosahrcbttovrhntcwvgltrorhwehjlpsarhwjhentaojdrerbarjwrhwdtqcwjonrmsfsshdtaonjorbosarccrbosaonslqkeonsvravrcrwsonsosrrvthyonsktalscrjhrvthyetvsorcptbgtqrhwvsdtqcwjonrmsfsshdtaondnjcsotnrmsfjooshtbbonsvroosadjonrevjcsotnrmseuqssxswonsqhjmsaesjhotrfrccotatccjootdrawetvstmsadnscvjhyuqseojthotergjrvcrxraqeltvsbatvonswsrwltvsfrlpotosccgtqrccjenrccosccgtqrccjbthsesoocjhyrkjcctdfgnsansrwentqcwergonrojehtodnrojvsrhororcconrojehtojororccrhwdtqcwjonrmsfsshdtaonjorbosarccdtqcwjonrmsfsshdtaondnjcsrbosaonseqhesoerhwonswttagrawerhwonsekajhpcsweoassoerbosaonshtmscerbosaonsosrlqkerbosaonsepjaoeonrooarjcrcthyonsbcttarhwonjerhwetvqlnvtasjojejvkteejfcsotergiqeodnrojvsrhfqorejbrvryjlcrhosahonasdonshsamsejhkroosahethrelasshdtqcwjonrmsfsshdtaondnjcsjbthsesoocjhyrkjcctdtaonatdjhytbbrenrdcrhwoqahjhyotdrawonsdjhwtdentqcwergonrojehtojororcconrojehtodnrojvsrhororcchtjrvhtokajhlsnrvcsohtadrevsrhootfsrvrhrooshwrhoctawthsonrodjccwtotedsccrkatyaseeeoraorelshstaodtrwmjesonskajhlshtwtqforhsregottcwsbsashojrcycrwotfstbqesktcjojllrqojtqerhwvsojlqctqebqcctbnjyneshoshlsfqorfjotfoqesroojvsejhwsswrcvteoajwjlqctqercvteoroojvseonsbttcjyatdtcwjyatdtcwjenrccdsraonsftootvetbvgoatqesaeatccswenrccjkraovgnrjafsnjhwwtjwrasotsrorksrlnjenrccdsradnjosbcrhhscoatqesaerhwdrcpqkthonsfsrlnjnrmsnsrawonsvsavrjweejhyjhysrlnotsrlnjwthtoonjhponroonsgdjccejhyotvsjnrmsesshonsvajwjhyesrdrawthonsdrmseltvfjhyonsdnjosnrjatbonsdrmsefctdhfrlpdnshonsdjhwfctdeonsdrosadnjosrhwfcrlpdsnrmscjhysaswjhonslnrvfsaetbonsesrfgesryjacedasronswdjonesrdsswaswrhwfatdhojccnqvrhmtjlsedrpsqerhwdswatdh";
	}

	//attempt to crack ceaser 1 (brute force) Sucess (Ezra Pound)
	/*
	std::cout << "Do you know who wrote this?\nIf yes then type name, otherwise just hit enter.\n";
	for (size_t guess = 0; guess < LANGUAGE_SIZE; guess++)
	{
		std::string plainTextGuess = ceaserCipher(ceaser, guess);

		std::cout << plainTextGuess << std::endl;

		std::getline(std::cin, name1);
		if (name1 != "")
		{
			std::cout << name1 << " wrote the first cipher. Decryption offset was at " << guess << std::endl;
			break;
		}
	}
	*/


	//attempt to crack alphine 2 (use frequency of each letter found on the internet, and match decrypted text's letters frequences to message) Sucess (Wallace Stevens)
	/*
	{
		std::vector<AlphineEntropyGuess> guesses;
		for (size_t multiplier = 0; multiplier < LANGUAGE_SIZE; multiplier++)
			if (std::gcd(multiplier, LANGUAGE_SIZE) == 1)
				for (size_t offset = 0; offset < LANGUAGE_SIZE; offset++)
				{
					AlphineEntropyGuess g;
					g.plaintText = alphineCipher(alphine, multiplier, offset);
					g.entropy = notMyCode::getEntropy(g.plaintText);
					g.multiplier = multiplier;
					g.offset = offset;

					//adds g to list of guesses
					guesses.push_back(g);
				}

		//sorts vector by using < operator, which then uses entropy
		std::sort(guesses.begin(), guesses.end());

		std::cout << "Do you know who wrote this?\nIf yes then type name, otherwise just hit enter.\n";
		for (size_t i = 0; i < guesses.size(); i++)
		{
			std::cout << guesses[i].plaintText << std::endl;
			std::getline(std::cin, name2);

			if (name2 != "")
			{
				std::cout << name2 << " wrote the second cipher. Alphine multiplier was " << guesses[i].multiplier << " and offset was " << guesses[i].multiplier << "." << std::endl;
				std::cout << "The plaintext is: " << guesses[i].plaintText << "\nand has entropy of " << guesses[i].entropy << ".\n";

				break;
			}
		}

	}
	*/


	//Substitution
	//Methodology:
	//	First we try easiest cipher

	size_t countInString[LANGUAGE_SIZE] {};


	//count all characters in cipherText
	for (size_t i = 0; i < substitution.size(); i++)
	{
		char number = charToNum(substitution[i]);
		countInString[number]++;
	}

	//make array from 0 to LANGUAGE_SIZE
	char mostCommonEnglishLetters[LANGUAGE_SIZE];
	for (size_t i = 0; i < LANGUAGE_SIZE; i++)
		mostCommonEnglishLetters[i] = i;

	//sort array from 0 to LANGUAGE_SIZE based on contents of notMyCode::ENGLISH_FREQS array
	std::sort(mostCommonEnglishLetters, mostCommonEnglishLetters + LANGUAGE_SIZE, [](char a, char b) {
		return notMyCode::ENGLISH_FREQS[a] > notMyCode::ENGLISH_FREQS[b]; 
	});


	//do same thing with letters in my text
	char mostCommonCipherLetters[LANGUAGE_SIZE];
	for (size_t i = 0; i < LANGUAGE_SIZE; i++)
		mostCommonCipherLetters[i] = i;

	std::sort(mostCommonCipherLetters, mostCommonCipherLetters + LANGUAGE_SIZE, [countInString](char a, char b) {
		return countInString[a] > countInString[b];
	});

	unsigned char cipherGuess[LANGUAGE_SIZE];

	const unsigned char UNASSIGNED = LANGUAGE_SIZE;

	for (size_t i = 0; i < LANGUAGE_SIZE; i++)
		cipherGuess[i] = UNASSIGNED;

	size_t englishItr = 0;
	size_t cipherItr = 0;
	while(true)
	{
		while (cipherGuess[mostCommonEnglishLetters[englishItr]] != UNASSIGNED)
			englishItr++;

		if (englishItr >= LANGUAGE_SIZE)
			break;

		while (cipherGuess[mostCommonCipherLetters[cipherItr]] != UNASSIGNED && mostCommonCipherLetters[cipherItr] != mostCommonEnglishLetters[englishItr])
			cipherItr = (cipherItr + 1) % 26;


		unsigned char a = mostCommonEnglishLetters[englishItr];
		unsigned char b = mostCommonCipherLetters[cipherItr];

		cipherGuess[a] = b;
		cipherGuess[b] = a;
	}

	std::cout << arbitraryBijectionCipher(substitution, cipherGuess) << std::endl;

	return 0;
}

std::string ceaserCipher(const std::string& plainText, int offset)
{
	std::string output;	//will contain output

	offset = normalizeInMod(offset, LANGUAGE_SIZE);

	//iterate across sring
	for (size_t i = 0; i < plainText.size(); i++)
	{
		unsigned int numb = charToNum(plainText[i]);	//convert to useful forme
		numb += offset;									//offset for cipher
		numb %= LANGUAGE_SIZE;							//modulo
		output += numToChar(numb);						//append to output
	}

	return output;
}

std::string alphineCipher(const std::string& plainText, int multipler, int offset)
{
	std::string output;
	
	multipler = normalizeInMod(multipler, LANGUAGE_SIZE);
	offset = normalizeInMod(offset, LANGUAGE_SIZE);

	for (size_t i = 0; i < plainText.size(); i++)
	{
		unsigned int numb = charToNum(plainText[i]);
		numb *= multipler;
		numb += offset;
		numb %= LANGUAGE_SIZE;
		output += numToChar(numb);
	}

	return output;
}

std::string arbitraryBijectionCipher(const std::string& plainText, const unsigned char map[])
{
	std::string output;

	for (size_t i = 0; i < plainText.size(); i++)
	{
		char numb = charToNum(plainText[i]);
		numb = map[numb];
		output += numToChar(numb);
	}

	return output;
}

constexpr char charToNum(char c)
{
	if (c >= 'a' && c <= 'z')
		return c - 'a';
	else if (c >= 'A' && c <= 'Z')
		return c = 'A';
	else
		throw "Invalid character to convert to numb: " + c;
}

constexpr char numToChar(char n) 
{
	return n + 'a';
}

constexpr int normalizeInMod(int input, unsigned int modulo)
{
	input %= modulo;
	if (input < 0)
		input += modulo;
	return input;
}
