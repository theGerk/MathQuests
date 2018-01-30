#include <iostream>
#include <vector>


class priorityQueue
{
	struct node {
		path value;
		node* next;

		~node()
		{
			delete next;
		}
	};

	node* head;
	node* back;//?

public:

	//default constructor
	priorityQueue(){}

	//desctructor
	~priorityQueue()
	{
		delete head;
	}

	//adds path to correct spot in queue
	void add(path);

	//get next node and remove it from queue
	path pop();
};


struct path
{
	std::vector<unsigned int> vec;
	unsigned int cost;


	unsigned int getEndNode() const
	{
		return vec.back();
	}

	bool hasPath() const
	{
		return cost >= 0;
	}

	path()
	{
		cost = -1;
	}

	path(std::vector<unsigned int> p, unsigned int c)
	{
		vec = p;
		cost = c;
	}
};

unsigned int getPos(unsigned int from, unsigned int to, unsigned int n)
{
	return from * (n - 1) + to - (to > from);
}

std::vector<unsigned int> fsp(int* graph, unsigned int n)
{
	path* p = new path[n];
	p[0].cost = 0;



	delete[] p;
}

int main()
{
	std::cout << getPos(4, 3, 5) << std::endl;
	return 0;
}