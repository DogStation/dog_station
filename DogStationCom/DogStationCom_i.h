

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0620 */
/* at Tue Jan 19 11:14:07 2038
 */
/* Compiler settings for DogStationCom.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.01.0620 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __DogStationCom_i_h__
#define __DogStationCom_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IComClass_FWD_DEFINED__
#define __IComClass_FWD_DEFINED__
typedef interface IComClass IComClass;

#endif 	/* __IComClass_FWD_DEFINED__ */


#ifndef __ComClass_FWD_DEFINED__
#define __ComClass_FWD_DEFINED__

#ifdef __cplusplus
typedef class ComClass ComClass;
#else
typedef struct ComClass ComClass;
#endif /* __cplusplus */

#endif 	/* __ComClass_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IComClass_INTERFACE_DEFINED__
#define __IComClass_INTERFACE_DEFINED__

/* interface IComClass */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IComClass;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("A0CB4ECC-0398-403D-A3A1-A150F652386E")
    IComClass : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE doSth( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE add( 
            LONG p1,
            LONG p2,
            LONG *result) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IComClassVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IComClass * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IComClass * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IComClass * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IComClass * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IComClass * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IComClass * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IComClass * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *doSth )( 
            IComClass * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *add )( 
            IComClass * This,
            LONG p1,
            LONG p2,
            LONG *result);
        
        END_INTERFACE
    } IComClassVtbl;

    interface IComClass
    {
        CONST_VTBL struct IComClassVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IComClass_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IComClass_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IComClass_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IComClass_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IComClass_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IComClass_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IComClass_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IComClass_doSth(This)	\
    ( (This)->lpVtbl -> doSth(This) ) 

#define IComClass_add(This,p1,p2,result)	\
    ( (This)->lpVtbl -> add(This,p1,p2,result) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IComClass_INTERFACE_DEFINED__ */



#ifndef __DogStationComLib_LIBRARY_DEFINED__
#define __DogStationComLib_LIBRARY_DEFINED__

/* library DogStationComLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_DogStationComLib;

EXTERN_C const CLSID CLSID_ComClass;

#ifdef __cplusplus

class DECLSPEC_UUID("9E523678-114C-4066-BD78-95C431AA6A07")
ComClass;
#endif
#endif /* __DogStationComLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


