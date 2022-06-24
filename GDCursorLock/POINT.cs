using System.Runtime.InteropServices;

namespace GDCursorLock;

/// <summary>
/// The POINT structure defines the x- and y- coordinates of a point.
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct POINT {
    /// <summary>
    /// The x-coordinate of the point.
    /// </summary>
    public int x;
    /// <summary>
    /// The x-coordinate of the point.
    /// </summary>
    public int y;
}
