#pragma once
#define DECLARE_ENUM_BEGIN( CLASSNAME ) \
class CLASSNAME {                       \
public:                                 \
	enum Enum {


#define DECLARE_ENUM_END( CLASSNAME )                 \
};                                                    \
public:                                               \
	CLASSNAME( void ){}                               \
	CLASSNAME( Enum value ) : value_( value ) {}      \
	CLASSNAME( int value ) : value_( (Enum)value ) {} \
	operator Enum ( void ) const { return value_; }   \
private:                                              \
	Enum value_;                                      \
};

DECLARE_ENUM_BEGIN(ReflectionType) 
	String, 
	Int,
	Double,
	Object, 
	Unknown
DECLARE_ENUM_END(ReflectionType)

class Type
{
public:
	explicit Type(ReflectionType typeId);

	ReflectionType getType() const;

	static void init();

private:
	ReflectionType typeId;
};