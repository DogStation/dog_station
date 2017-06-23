// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the DOGSTATIONWIN32_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// DOGSTATIONWIN32_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef DOGSTATIONWIN32_EXPORTS
#define DOGSTATIONWIN32_API extern "C" __declspec(dllexport)
#else
#define DOGSTATIONWIN32_API extern "C" __declspec(dllimport)
#endif


DOGSTATIONWIN32_API bool checkAccountInfo(const char * username, const char * password);

