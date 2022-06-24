using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GDCursorLock;

public static class Native {
    private static void ApiException(string method) => throw new Exception($"API call failed for method '{method}'!");

    [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
    private static extern bool _GetCursorPos(out POINT point);
    public static void GetCursorPos(out POINT point) {
        if (!_GetCursorPos(out point)) ApiException(Helpers.GetCallerName());
    }

    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    private static extern bool _SetCursorPos(int X, int Y);
    public static void SetCursorPos(in POINT point) => SetCursorPos(point.x, point.y);
    public static void SetCursorPos(int x, int y) {
        if (!_SetCursorPos(x, y)) ApiException(Helpers.GetCallerName());
    }

    [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
    private static extern bool _GetWindowRect(IntPtr hWnd, out RECT lpRect);
    public static void GetWindowRect(IntPtr hwnd, out RECT lpRect) {
        if (!_GetWindowRect(hwnd, out lpRect)) ApiException(Helpers.GetCallerName());
    }

    [DllImport("user32.dll", EntryPoint = "ClipCursor")]
    private static extern bool _ClipCursor(in RECT lpRect);
    public static void ClipCursor(in RECT lpRect) {
        if (!_ClipCursor(in lpRect)) ApiException(Helpers.GetCallerName());
    }

    [DllImport("user32.dll", EntryPoint = "GetClipCursor")]
    private static extern bool _GetClipCursor(out RECT lpRect);
    public static void GetClipCursor(out RECT lpRect) {
        if (!_GetClipCursor(out lpRect)) ApiException(Helpers.GetCallerName());
    }

    [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
    [SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible", Justification = "Method is safe to use.")]
    public static extern IntPtr GetForegroundWindow();

}

public static class Helpers {
    public static string GetCallerName([CallerMemberName] string caller = default!)
        => caller ?? throw new InvalidProgramException("CallerMemberName was null!");
}