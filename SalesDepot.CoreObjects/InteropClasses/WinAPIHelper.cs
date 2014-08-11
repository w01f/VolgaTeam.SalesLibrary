using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace SalesDepot.CoreObjects.InteropClasses
{
	public enum WM
	{
		WM_KEYUP = 0x0101,
		WM_KEYDOWN = 0x0100,
		WM_LBUTTONDOWN = 0x0201,
		WM_LBUTTONUP = 0x0202
	}

	/// <summary>
	///     Enumeration of the different ways of showing a window using
	///     ShowWindow
	/// </summary>
	public enum WindowShowStyle : uint
	{
		/// <summary>Hides the window and activates another window.</summary>
		/// <remarks>See SW_HIDE</remarks>
		Hide = 0,

		/// <summary>
		///     Activates and displays a window. If the window is minimized
		///     or maximized, the system restores it to its original size and
		///     position. An application should specify this flag when displaying
		///     the window for the first time.
		/// </summary>
		/// <remarks>See SW_SHOWNORMAL</remarks>
		ShowNormal = 1,

		/// <summary>Activates the window and displays it as a minimized window.</summary>
		/// <remarks>See SW_SHOWMINIMIZED</remarks>
		ShowMinimized = 2,

		/// <summary>Activates the window and displays it as a maximized window.</summary>
		/// <remarks>See SW_SHOWMAXIMIZED</remarks>
		ShowMaximized = 3,

		/// <summary>Maximizes the specified window.</summary>
		/// <remarks>See SW_MAXIMIZE</remarks>
		Maximize = 3,

		/// <summary>
		///     Displays a window in its most recent size and position.
		///     This value is similar to "ShowNormal", except the window is not
		///     actived.
		/// </summary>
		/// <remarks>See SW_SHOWNOACTIVATE</remarks>
		ShowNormalNoActivate = 4,

		/// <summary>
		///     Activates the window and displays it in its current size
		///     and position.
		/// </summary>
		/// <remarks>See SW_SHOW</remarks>
		Show = 5,

		/// <summary>
		///     Minimizes the specified window and activates the next
		///     top-level window in the Z order.
		/// </summary>
		/// <remarks>See SW_MINIMIZE</remarks>
		Minimize = 6,

		/// <summary>
		///     Displays the window as a minimized window. This value is
		///     similar to "ShowMinimized", except the window is not activated.
		/// </summary>
		/// <remarks>See SW_SHOWMINNOACTIVE</remarks>
		ShowMinNoActivate = 7,

		/// <summary>
		///     Displays the window in its current size and position. This
		///     value is similar to "Show", except the window is not activated.
		/// </summary>
		/// <remarks>See SW_SHOWNA</remarks>
		ShowNoActivate = 8,

		/// <summary>
		///     Activates and displays the window. If the window is
		///     minimized or maximized, the system restores it to its original size
		///     and position. An application should specify this flag when restoring
		///     a minimized window.
		/// </summary>
		/// <remarks>See SW_RESTORE</remarks>
		Restore = 9,

		/// <summary>
		///     Sets the show state based on the SW_ value specified in the
		///     STARTUPINFO structure passed to the CreateProcess function by the
		///     program that started the application.
		/// </summary>
		/// <remarks>See SW_SHOWDEFAULT</remarks>
		ShowDefault = 10,

		/// <summary>
		///     Windows 2000/XP: Minimizes a window, even if the thread
		///     that owns the window is hung. This flag should only be used when
		///     minimizing windows from a different thread.
		/// </summary>
		/// <remarks>See SW_FORCEMINIMIZE</remarks>
		ForceMinimized = 11
	}


	public class WinAPIHelper
	{
		#region Public constants
		public const Byte BSF_IGNORECURRENTTASK = 2; //this ignores the current app Hex 2
		public const Byte BSF_POSTMESSAGE = 16; //This posts the message Hex 10
		public const Byte BSM_APPLICATIONS = 8; //This tells the windows message to just go to applications Hex 8


		private const UInt32 SWP_NOSIZE = 0x0001;
		private const UInt32 SWP_NOMOVE = 0x0002;

		public const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

		public const int DWMWA_TRANSITIONS_FORCEDISABLED = 3; /* Don't send WM_WINDOWPOSCHANGING */

		public const int MAX_PATH = 260;
		private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
		private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
		private static readonly IntPtr HWND_TOP = new IntPtr(0);
		private static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

		public static readonly UInt32 WM_NCLBUTTONDOWN = 0xA1;
		public static readonly IntPtr HTCAPTION = new IntPtr(0x2);
		#endregion

		#region API imports
		[DllImport("USER32.DLL", EntryPoint = "BroadcastSystemMessageA", SetLastError = true,
			CharSet = CharSet.Unicode, ExactSpelling = true,
			CallingConvention = CallingConvention.StdCall)]
		public static extern int BroadcastSystemMessage(Int32 dwFlags, ref Int32 pdwRecipients, int uiMessage, int wParam, int lParam);


		[DllImport("USER32.DLL", EntryPoint = "RegisterWindowMessageA", SetLastError = true,
			CharSet = CharSet.Unicode, ExactSpelling = true,
			CallingConvention = CallingConvention.StdCall)]
		public static extern int RegisterWindowMessage(String pString);

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string className, string windowName);

		[DllImport("user32.dll")]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
			int Y, int cx, int cy, uint uFlags);

		[DllImport("user32.dll")]
		public static extern int SetForegroundWindow(IntPtr wnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetActiveWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		[DllImport("user32.dll")]
		public static extern IntPtr SetFocus(IntPtr hWnd);

		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(IntPtr hWnd);

		[DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);

		[DllImport("User32.dll", EntryPoint = "ShowWindowAsync")]
		public static extern bool ShowWindowAsync(IntPtr hWnd, WindowShowStyle cmdShow);

		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

		[DllImport("kernel32.dll")]
		public static extern uint GetCurrentThreadId();

		[DllImport("uxtheme", ExactSpelling = true)]
		public static extern Int32 DrawThemeParentBackground(IntPtr hWnd, IntPtr hdc, ref Rectangle pRect);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int StrCmpLogicalW(String x, String y);

		[DllImport("dwmapi.dll")]
		public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("User32.dll")]
		public static extern bool ReleaseCapture();

		[DllImport("user32.dll")]
		public static extern int SetParent(IntPtr wndChild, IntPtr wndNewParent);
		#endregion

		public static void MakeTopMost(IntPtr handle)
		{
			SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
		}

		public static void MakeNormal(IntPtr handle)
		{
			SetWindowPos(handle, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
		}

		public static void MakeTop(IntPtr handle)
		{
			SetWindowPos(handle, HWND_TOP, 0, 0, 0, 0, TOPMOST_FLAGS);
		}

		public static void MakeBottom(IntPtr handle)
		{
			SetWindowPos(handle, HWND_BOTTOM, 0, 0, 0, 0, TOPMOST_FLAGS);
		}
	}
}