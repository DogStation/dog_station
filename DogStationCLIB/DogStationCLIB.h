// DogStationCLIB.h

#pragma once

using namespace System;
typedef System::String^ string;

namespace DogStationCLIB {

	public ref class CppTokenHolder
	{
	public:
		static string GenToken(long p1, string p2, string p3);
		static long ExtractId(string token);
		static string ExtractName(string token);
	private:
		static string extract(string token);
	};
}
