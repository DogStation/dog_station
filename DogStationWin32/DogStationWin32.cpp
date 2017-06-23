// DogStationWin32.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "DogStationWin32.h"
#include <cctype>
#include <string>

using namespace std;


bool checkValidUsername(string str)
{
	int i = 0;
	while (i < str.length() && str[i] != '(' && str[i] != ')')
	{
		++i;
	}

	return i == str.length();
}

bool checkValidPasswd(string str)
{
	int i = 0;
	while (i < str.length() &&
		(isalnum(str[i]) || str[i] == '_'))
	{
		++i;
	}
	return i == str.length();
}

DOGSTATIONWIN32_API bool checkAccountInfo(const char * name, const char * passwd)
{
	bool result = true;
	string username = name;
	string password = passwd;

	if (name == NULL || passwd == NULL ||
		username.length() == 0 || password.length() == 0 ||
		username.length() > 20 || password.length() > 30)
		result = false;
	else {
		result = checkValidUsername(username) && checkValidPasswd(password);
	}
	return result;
}