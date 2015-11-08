// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"

HMODULE g_hinstDLL;

BOOL APIENTRY DllMain(HMODULE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
	)
{
	g_hinstDLL = hModule;

	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}


#include <Windows.h>
#include<cstdio>
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

std::vector<HWND> windows;

// get top level windows
struct EnumWindowsCallbackArgs
{
	EnumWindowsCallbackArgs(DWORD p) : pid(p) { }
	const DWORD pid;
	std::vector<HWND> handles;
};
static BOOL CALLBACK EnumWindowsCallback(HWND hnd, LPARAM lParam)
{
	EnumWindowsCallbackArgs *args = (EnumWindowsCallbackArgs *)lParam;

	DWORD windowPID;
	(void)::GetWindowThreadProcessId(hnd, &windowPID);
	if (windowPID == args->pid)
	{
		args->handles.push_back(hnd);
	}

	return TRUE;
}
std::vector<HWND> getToplevelWindows()
{
	EnumWindowsCallbackArgs args(::GetCurrentProcessId());
	if (::EnumWindows(&EnumWindowsCallback, (LPARAM)&args) == FALSE)
	{
		// XXX Log error here
		return std::vector<HWND>();
	}
	return args.handles;
}

LRESULT CALLBACK CallWndProc(int nCode, WPARAM wParam, LPARAM lParam)
{
	return 1;

	if (nCode == 9)
	{
		return 1;
	}
	else if (nCode == 5)
	{
		// 5 => Focus got here
		HWND toBe = (HWND)wParam;
		LPCBTACTIVATESTRUCT data = (LPCBTACTIVATESTRUCT)wParam;
		//return NULL;
		if (windows.empty())
		{
			windows = getToplevelWindows();
		}
		for (int i = 0; i < windows.size(); i++)
		{
			HWND win = windows[i];
			if (toBe == win)
			{
				return 0;
			}
		}
		return 1;
	}
	return 0;

	/* If nCode is greater than or equal to HC_ACTION,
	* we should process the message. */
	//if (nCode >= HC_ACTION)
	//{
	//	/* Retrieve a pointer to the structure that contains details about
	//	* the message, and see if it is one that we want to handle. */
	//	const LPCWPSTRUCT lpcwprs = (LPCWPSTRUCT)lParam;

	//	switch (lpcwprs->message)
	//	{
	//		/* ...SNIP: process the messages we're interested in ... */
	//		case WM_KILLFOCUS:
	//			return 1;
	//		break;
	//		case WM_SETFOCUS:
	//			return 1;
	//		break;
	//		default:
	//			return CallNextHookEx(g_hhkCallWndProcRet, nCode, wParam, lParam);
	//		break;
	//	}
	//}

	/* At this point, we are either not processing the message
	* (because nCode is less than HC_ACTION),
	* or we've already finished processing it.
	* Either way, pass the message on. */
}



extern "C" __declspec(dllexport) BOOL __stdcall InstallHook(DWORD threadId)
{
	/* Try to install the WH_CALLWNDPROCRET hook,
	* if it is not already installed. */
	if (!g_hhkCallWndProcRet)
	{
		/*MessageBox(NULL,
			(LPCWSTR)L"Initialized",
			(LPCWSTR)L"Account Details",
			MB_ICONWARNING | MB_CANCELTRYCONTINUE | MB_DEFBUTTON2
			);*/

		/*g_hhkCallWndProcRet = SetWindowsHookEx(WH_CALLWNDPROC,
			CallWndProc,
			g_hinstDLL,
			threadId);*/
		g_hhkCallWndProcRet = SetWindowsHookEx(WH_CBT,
			CallWndProc,
			g_hinstDLL,
			threadId); 
		if (!g_hhkCallWndProcRet)
		{
			/* ...SNIP: handle failure condition ... */
			return FALSE;
		}
	}

	return TRUE;  /* return success */
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
