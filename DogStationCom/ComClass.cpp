// ComClass.cpp : Implementation of CComClass

#include "stdafx.h"
#include "ComClass.h"


// CComClass



STDMETHODIMP CComClass::doSth()
{
	// TODO: Add your implementation code here

	return S_OK;
}


STDMETHODIMP CComClass::add(LONG p1, LONG p2, LONG* result)
{
	// TODO: Add your implementation code here
	*result = p1 + p2;
	return S_OK;
}
