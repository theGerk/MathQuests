#pragma once
class BitInt
{
private:
	unsigned long long bitArray;

public:
	BitInt();
	~BitInt();

	inline unsigned long long asInt() {
		return bitArray;
	}

	inline bool operator[](size_t index) {
		const size_t size = sizeof(bitArray) * 8;
		const unsigned long long offset = bitArray << (size - index - 1);
		return offset >> (size - 1);
	}
};

