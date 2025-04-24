using DreamscapeGrove.Core;
using System;

public static class CoreExtensions
{
    internal static void DisposeIfNeeded(this IFocusSource src)
    {
        if (src is IDisposable d) d.Dispose();
    }
}
