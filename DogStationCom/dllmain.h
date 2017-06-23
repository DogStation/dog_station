// dllmain.h : Declaration of module class.

class CDogStationComModule : public ATL::CAtlDllModuleT< CDogStationComModule >
{
public :
	DECLARE_LIBID(LIBID_DogStationComLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_DOGSTATIONCOM, "{484DE91E-629F-4B12-B7BC-FA59C7BA513A}")
};

extern class CDogStationComModule _AtlModule;
