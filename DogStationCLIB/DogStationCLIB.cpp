// This is the main DLL file.

#include "stdafx.h"

#include "DogStationCLIB.h"

using namespace System;
using namespace System::Web;
using namespace System::Web::Security;

namespace DogStationCLIB 
{
	string CppTokenHolder::GenToken(long p1, string p2, string p3)
	{
		FormsAuthenticationTicket^ ticket = gcnew FormsAuthenticationTicket(0, p2, DateTime::Now,
			DateTime::Now.AddDays(3), true, String::Format("{0}&{1}&{2}", p1, p2, p3),
			FormsAuthentication::FormsCookiePath);
		string token = FormsAuthentication::Encrypt(ticket);
		return token;
	}

	string CppTokenHolder::extract(string token)
	{
		FormsAuthenticationTicket^ ticket = FormsAuthentication::Decrypt(token);
		return ticket->UserData;
	}

	long CppTokenHolder::ExtractId(string token)
	{
		string decrypted = extract(token);
		return Convert::ToInt64(decrypted->Split('&')[0]);
	}
	string CppTokenHolder::ExtractName(string token)
	{
		string decrypted = extract(token);
		return decrypted->Split('&')[1];
	}
}

