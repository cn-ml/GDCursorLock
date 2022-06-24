using System.Diagnostics;
using System.Reflection;
using GDCursorLock;

RECT rect;
POINT point;

var assembly = Assembly.GetExecutingAssembly();
var assemblyName = assembly.GetName();
Console.WriteLine($"{assemblyName.Name} by cn-ml version {assemblyName.Version}");
Console.WriteLine("Your Cursor will now be fixed to Geometry Dash if the application is focused.");
Console.WriteLine("You can always Alt+Tab out of Geometry Dash");
Console.WriteLine("Press Ctrl+C to quit");

CancellationTokenSource cts = new CancellationTokenSource();
void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e) {
    cts.Cancel();
    e.Cancel = true;
}
Console.CancelKeyPress += Console_CancelKeyPress;

while (true) {
    // without delay this loops ~100/s
    try { await Task.Delay(60, cts.Token); } catch (TaskCanceledException) { }
    if (cts.IsCancellationRequested) break;
    if (Process.GetProcessesByName("GeometryDash").SingleOrDefault() is not Process process) {
        Log("Process not running!");
        continue;
    }
    if (process.MainWindowHandle == IntPtr.Zero) {
        Log("Process has no main window!");
        continue;
    }
    if (process.MainWindowHandle != Native.GetForegroundWindow()) {
        Log("Process window is not focused!");
        continue;
    }
    Native.GetCursorPos(out point);
    Native.GetWindowRect(process.MainWindowHandle, out rect);
    if (IsInside(in point, in rect)) {
        Log("Cursor is in window");
        continue;
    }
    Log("Cursor is outside window and will be reset!");
    SetToCenter(in rect, out point);
    Native.SetCursorPos(in point);
}

Console.WriteLine("Goodbye");

[Conditional("DEBUG")]
static void Log(string message) => Console.WriteLine(message);

static void SetToCenter(in RECT rect, out POINT point) {
    point.x = (rect.right - rect.left) / 2;
    point.y = (rect.bottom - rect.top) / 2;
}

static bool IsInside(in POINT point, in RECT rect)
    => point.x >= rect.left && point.x < rect.right
    && point.y >= rect.top && point.y < rect.bottom;
