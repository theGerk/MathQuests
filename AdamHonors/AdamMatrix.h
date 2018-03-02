#ifndef __ADAM_MATRIX__
#define __ADAM_MATRIX__
#include <bitset>
#include <string>
#include <set>
#include <ostream>
#include <vector>

namespace adam
{
	//in progress
	//contains data used by adam for his n * 2n bit matrix
	template <size_t Height>
	class matrix
	{
		size_t data[Height];

	public:
		//constructors
		//complete
		///<summary>
		///each size_t is thought of as a bit matrix, should be Height long
		///</sumary>
		matrix(const size_t noise[])
		{
			std::copy(noise, noise + Height, data);
			std::reverse(data, data + Height);
		}

		//copy constructor
		matrix(const matrix<Height>& other)
		{
			std::copy(other.data, other.data + Height, data);
		}


		//complete
		//assignment overload
		inline const matrix<Height> operator=(const matrix<Height> other)
		{
			std::copy(other.data, other.data + Height, data);
			return *this;
		}


		//complete
		//prints to ostream
		std::ostream& print(std::ostream& os) const
		{
			for (size_t i = 0; i < Height; i++)
				os << std::bitset<Height * 2>(data[i]) << std::endl;
			return os;
		}

		bool confirmWord(size_t word)
		{
			size_t id = word >> Height;
			size_t value = 0;
			for (size_t counter = 0; counter < Height; counter++)
			{
				if ((id >> counter) & 1)
				{
					value ^= data[counter];
				}
			}
			std::cout << std::bitset<Height * 2>(value) << std::endl;
			return value == word;
		}

		std::vector<size_t> getRowSpace() const
		{
			std::vector<size_t> output;
			for (size_t i = 0; i < (1 << Height); i++)
			{
				size_t value = 0;
				for(size_t counter = 0; counter < Height; counter++) 
				{
					if ((i >> counter) & 1)
					{
						value ^= data[counter];
					}
				}
				output.push_back(value);
			}
			return output;
		}

		std::vector<size_t> getSubset(const size_t flags) const
		{
			std::vector<size_t> output;
			for (size_t i = 0; i < (1 << Height); i++)
			{
				size_t value = 0;
				for (size_t counter = 0; counter < Height; counter++)
				{
					if ((i >> counter) & 1)
					{
						value ^= data[counter];
					}
				}
				if ((flags & value) == flags)
					output.push_back(value);
			}
			return output;
		}

	};

	template<size_t Height>
	std::ostream& operator<<(std::ostream& os, const matrix<Height>& mat)
	{
		return mat.print(os);
	}
}

#endif