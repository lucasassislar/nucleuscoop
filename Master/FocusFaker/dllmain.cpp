// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"

HMODULE g_hinstDLL;

BOOL APIENTRY DllMain(HMODULE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
	)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		g_hinstDLL = hModule;
		break;
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}


#include <Windows.h>
#include <cstdio>
#include <iostream>
#include <fstream>
#include <vector>

/* The handle to the hook is stored as a shared global variable and is the
* same for all hooked processes. We achieve that by placing it in the
* shared data segment of the DLL.
*
* Note that shared global variables must be explicitly initialized.
*
* And also note that this is really not the ideal way of doing this; it's just
* an easy way to get going. The better solution is to use a memory-mapped file.
* See Also: http://msdn.microsoft.com/en-us/library/h90dkhs0.aspx
*/
#pragma comment(linker, "/section:.SHARED,rws")
#pragma data_seg(".SHARED") /* begin the shared data segment */
HHOOK g_hhkCallWndProcRet = NULL;
#pragma data_seg()          /* end the shared data segment and default back to normal behavior */

LRESULT CALLBACK CallWndProc(int nCode, WPARAM wParam, LPARAM lParam)
{
	if (nCode == 0x7 || nCode == 0x8)
	{
		// 5 => Focus got here
		/*HWND toBe = (HWND)wParam;*/
		return 1;
	}
	return 0;
}

extern "C" __declspec(dllexport) BOOL __stdcall InstallHook(void)
{
	if (!g_hhkCallWndProcRet)
	{
		//g_hhkCallWndProcRet = SetWindowsHookEx(WH_CBT, CallWndProc, g_hinstDLL, threadId); 
		g_hhkCallWndProcRet = SetWindowsHookEx(WH_CALLWNDPROC, CallWndProc, g_hinstDLL, 0);
		if (!g_hhkCallWndProcRet)
		{
			return FALSE;
		}
	}
	return TRUE;
}

extern "C" __declspec(dllexport) BOOL __stdcall RemoveHook(void)
{
	/* Try to remove the WH_CALLWNDPROCRET hook, if it is installed. */
	if (g_hhkCallWndProcRet)
	{
		if (!UnhookWindowsHookEx(g_hhkCallWndProcRet))
		{
			/* ...SNIP: handle failure condition ... */
			return FALSE;
		}
		g_hhkCallWndProcRet = NULL;
	}

	return TRUE;  /* return success */
}
