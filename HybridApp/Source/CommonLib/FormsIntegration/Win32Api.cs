using System;
using System.Runtime.InteropServices;

namespace CommonLib.FormsIntegration;
internal class Win32Api
{
    public static readonly int GWL_STYLE = -16;

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    public static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);

    [DllImport("user32.dll")]
    public static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    public static void SetWindowStyle(IntPtr hWnd, WindowStyles windowStyle)
    {
        var newStyle = windowStyle;
        SetWindowLong(hWnd, GWL_STYLE, (uint)newStyle);
    }
    public static WindowStyles GetWindowStyle(IntPtr hWnd)
    {
        return (WindowStyles)GetWindowLong(hWnd, GWL_STYLE);
    }
}
