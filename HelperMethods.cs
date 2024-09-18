using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileRenamer
{
    public class HelperMethods
    {
        public static string ShortenPathToLowestDirectories(string path, int numberOfDirectories = 2)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            // Split the path into its components
            string[] pathComponents = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            // If the path has fewer components than the number of directories to show, return the full path
            if (pathComponents.Length <= numberOfDirectories)
            {
                return path;
            }

            // Get the last 'numberOfDirectories' components
            string[] lastComponents = pathComponents[^numberOfDirectories..];

            // Combine the components into a shortened path
            string shortenedPath = Path.Combine(lastComponents);

            // Add ellipsis to indicate that the path is shortened
            return "..." + Path.DirectorySeparatorChar + shortenedPath;
        }
    }
}
