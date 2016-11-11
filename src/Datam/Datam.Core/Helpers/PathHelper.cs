using System;
using System.IO;

namespace Datam.Core.Helpers
{
    public static class PathHelper
    {
        public static string GetRelativeLocation(string codebasePath, string relativePath)
        {
            UriBuilder uri = new UriBuilder(codebasePath);
            string unescapedPath = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));

            string path = Path.Combine(unescapedPath, relativePath);
            path = Path.GetFullPath(path);

            return path;
        }
    }
}
