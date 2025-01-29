using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileRenamer
{
    public partial class Utility
    {
        public enum RenameOptions
        {
            Prefix,
            Suffix
        }

        public static string ShortenPath(string input)
        {
            string root = Path.GetPathRoot(input);
            if (string.IsNullOrEmpty(root))
            {
                return input;
            }

            string pathWithoutRoot = input.Substring(root.Length);

            string[] parts = pathWithoutRoot.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length <= 2)
            {
                return input;
            }

            string lastTwoFolders = Path.Combine(parts[^2], parts[^1]);
            string shortenedFolders = string.Join
                (Path.DirectorySeparatorChar.ToString(), parts.Take(parts.Length - 2).Select(f => f.Substring(0, 2)));
            return Path.Combine(root, shortenedFolders, lastTwoFolders);
        }

        /// <summary>
        /// Renames a list of files by adding a prefix or suffix to their names.
        /// </summary>
        /// <param name="files">The list of files to rename.</param>
        /// <param name="name">The name to add as a prefix or suffix.</param>
        /// <param name="option">Specifies whether to add the name as a prefix or suffix.</param>
        /// <param name="counter">The starting counter value for the renaming. Default is 1.</param>
        /// <param name="increment">The increment value for the counter. Default is 1.</param>
        public void RenameFiles(List<FileItem> files, string name, Utility.RenameOptions option, int counter = 1, int increment = 1)
        {
            foreach (FileItem file in files)
            {
                string newName = file.Name;
                switch (option)
                {
                    case RenameOptions.Prefix:
                        newName = $"{counter}_{name}";
                        break;
                    case RenameOptions.Suffix:
                        newName = $"{name}_{counter}";
                        break;
                }

                string newFilePath = Path.Combine(file.FolderPath, newName + file.Extension);
                File.Move(Path.Combine(file.FolderPath, file.Name + file.Extension), newFilePath);
                file.Name = newName;
                counter += increment;
            }
        }

    }
}
