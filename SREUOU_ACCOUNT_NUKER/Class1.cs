using System;
using System.IO;

internal static class DnSpy
{
    internal static bool ValueType()
    {
        if (!File.Exists(Environment.ExpandEnvironmentVariables("%appdata%") + "\\dnSpy\\dnSpy.xml"))
        {
            return false;
        }
        return true;
    }
}