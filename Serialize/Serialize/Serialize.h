#pragma once
#include <type_traits>
#include <iostream>

template<class Base, class Derived>
class IsBaseOfHelper
{
public:
	operator Base*() const;
	operator Derived*();
};

template<class Base, class Derived>
class IsBaseOf
{
private:
	template<class T>
	static char check(Derived*, T);
	static int  check(Base*, int);

public:
	static const bool value = sizeof(check(IsBaseOfHelper<Base, Derived>(), int())) == sizeof(char);
};

template<class Base, class Derived>
class IsBaseOf<Base, const Derived>
{
private:
	template<class T>
	static char check(Derived*, T);
	static int  check(Base*, int);

public:
	static const bool value = sizeof(check(IsBaseOfHelper<Base, Derived>(), int())) == sizeof(char);
};

template<class Base, class Derived>
class IsBaseOf<Base&, Derived&>
{
public:
	static const bool value = false;
};

template<class Type>
class IsBaseOf<Type, Type>
{
public:
	static const bool value = true;
};

template<class Type>
class IsBaseOf<Type, const Type>
{
public:
	static const bool value = true;
};

template<bool B, typename T = void>
struct enable {};

template<typename T>
struct enable<true, T> { typedef T Type; };

template<bool B, typename T>
struct disable : public enable<!B, T> { };


namespace Serialization
{
	class Archive;
	
	enum class eSerializeFormat
	{
		FORMAT_BINARY,
		FORMAT_TEXT,
		FORMAT_XML,
		FORMAT_JSON,
		FORMAT_UNKNOWN,
	};

	enum class eSerializeType
	{
			TYPE_SCALAR,
			TYPE_INT32 = TYPE_SCALAR,
			TYPE_INT16,
			TYPE_INT64,

			TYPE_UINT32,
			TYPE_UINT16,
			TYPE_UINT64,

			TYPE_FLOAT32,
			TYPE_FLOAT64,

			TYPE_CHAR,
			TYPE_UCHAR,

			TYPE_BOOL,

			TYPE_POINTER,
			TYPE_VECTOR = TYPE_POINTER,
			TYPE_STRING,
			TYPE_STRUCT,
			TYPE_CLASS,
			TYPE_UNION,

			TYPE_UNKNOWN,
	};

	inline bool IsScalar(eSerializeType Type)  { return Type >= eSerializeType::TYPE_SCALAR  && Type < eSerializeType::TYPE_POINTER; }
	inline bool IsPointer(eSerializeType Type) { return Type >= eSerializeType::TYPE_POINTER && Type < eSerializeType::TYPE_UNKNOWN; }

	class Struct;
	class Enum;

	struct Type
	{
		eSerializeType BaseType;
		eSerializeType ElementType;		// Vector時のみ
		Struct		   *StructType;		// Struct時のみ
		Enum		   *EnumType;		// Enum時のみ
	};

	struct Value
	{
		Type type;
		unsigned long long offset;
	};

	// このクラスを継承すると、シリアライズ可能になる
	class Serializable
	{
	public:
		virtual void serialize(Archive &Data, int Version = 0) = 0;
	};

	class Archive
	{
	public:

		Archive();
		Archive(const Archive& );
		virtual ~Archive();

	protected:

		template<typename T>
		inline Archive &serialize(T &object, typename enable<IsBaseOf<Serializable, T>::value, T>::Type* = 0)
		{
			return *this; 
		}

		template<typename T>
		inline Archive &serialize(T &object, typename disable<IsBaseOf<Serializable, T>::value, T>::Type* = 0) 
		{ 
			return *this; 
		}

		inline void ByteSwap(void* Ptr, int Length);
		inline const bool IsNeedByteSwap() const;
		inline const bool IsReading() const;
		inline const bool IsWriting() const;

	protected:
		void* Data;
		unsigned int DataSize;

	};
}