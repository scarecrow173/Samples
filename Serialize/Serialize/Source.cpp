#include <stdio.h>
#include <fstream>
#include <vector>
#include <string>
#include "Serialize.h"
#include "Reflection.h"

class TEST_BASE : public Serialization::Serializable
{
	int num1;
	unsigned int num2;
	short num3; // シリアライズしない
public:
	// シリアライズメンバ関数(+を使ってデータを結合)
	void serializa(Serialization::Archive &Data, int Version = 0) { /*Data + num1 + num2;*/ }
};
class TEST_SUB : public TEST_BASE
{
	char moji;
public:
	void serializa(Serialization::Archive &Data, int Version = 0) { /*__super::serializa(Data); Data + moji;*/ }

};
class TEST_SUB_SUB : public TEST_SUB
{
	void serializa(Serialization::Archive &Data, int Version = 0) { /*__super::serializa(Data);*/ }

};
class TEST_NOT_SUB
{
};
class Something : public Serialization::Serializable
{
private:


public:
	Something() /*: num1(0), num2(100), num3(5), c('H')*/
	{}

	// シリアライズメンバ関数(+を使ってデータを結合)
	virtual void serialize(Serialization::Archive &Data, int Version = 0) override { /*Data + num1 + num2;*/ }

	int num1;
	unsigned int num2;
	short num3; // シリアライズしない
	char c;
};

void main()
{
	//std::ofstream ofs("test.bin", std::ios_base::binary);
	//Serialization::Archive writer(ofs);

	//Something sw, tw;
	//writer + sw + tw;
	//ofs.close();

	//std::ifstream ifs("test.bin", std::ios_base::binary);
	//Serialization::Archive reader(ifs);

	//Something sr, tr;
	//reader + sr + tr;

	printf("int is %s Scalar!\n",           std::is_scalar<int>::value ? "" : "Not");
	printf("unsigned int is %s Scalar!\n",  std::is_scalar<unsigned int>::value ? "" : "Not");
	printf("long is %s Scalar!\n",          std::is_scalar<long>::value ? "" : "Not");
	printf("long long is %s Scalar!\n",     std::is_scalar<long long>::value ? "" : "Not");
	printf("char is %s Scalar!\n",          std::is_scalar<char>::value ? "" : "Not");
	printf("unsigned char is %s Scalar!\n", std::is_scalar<unsigned char>::value ? "" : "Not");
	printf("float is %s Scalar!\n",         std::is_scalar<float>::value ? "" : "Not");
	printf("double is %s Scalar!\n",        std::is_scalar<double>::value ? "" : "Not");
	printf("bool is %s Scalar!\n\n",          std::is_scalar<bool>::value ? "" : "Not");

	printf("Pointer is %s Scalar!\n", std::is_scalar<void*>::value ? "" : "Not");
	printf("Vector is %s Scalar!\n", std::is_scalar<std::vector<bool>>::value ? "" : "Not");
	printf("String is %s Scalar!\n", std::is_scalar<std::string>::value ? "" : "Not");
	printf("Class is %s Scalar!\n", std::is_scalar<class SampleClass>::value ? "" : "Not");
	printf("Struct is %s Scalar!\n", std::is_scalar<struct SampleStruct>::value ? "" : "Not");
	printf("Union is %s Scalar!\n", std::is_scalar<union SampleUnion>::value ? "" : "Not");
	printf("Enum is %s Scalar!\n", std::is_scalar<enum SampleEnum>::value ? "" : "Not");

	getchar();
}