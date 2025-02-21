using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace FileRenamer
{
    public partial class Utility
    {
        public enum CounterOptions
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
            string shortenedFolders = string.Join(
                Path.DirectorySeparatorChar.ToString(),
                parts.Take(parts.Length - 2).Select(f => f.Length >= 2 ? f.Substring(0, 2) : f));
            return Path.Combine(root, shortenedFolders, lastTwoFolders);
        }

        /// <summary>
        /// Renames the items in a list of files and adds a counter as either prefix or suffix.
        /// </summary>
        /// <param name="files">The list of files to rename.</param>
        /// <param name="name">The name to add as a prefix or suffix.</param>
        /// <param name="option">Specifies whether to add the name as a prefix or suffix.</param>
        /// <param name="initialNumber">The starting counter value for the renaming.</param>
        /// <param name="increment">The increment value for the counter.</param>
        public static void RenameFiles(List<FileItem> files, string name, Utility.CounterOptions option, int initialNumber, int increment)
        {
            if(name == null)
            { 
                MessageBox.Show("Bitte gib einen Dateinamen an");
                return;
            }

            int fileListLength = files.Count.ToString().Length;

            foreach (FileItem file in files)
            {
                string newName = file.Name;
                switch (option)
                {
                    case CounterOptions.Prefix:
                        newName = $"{initialNumber.ToString().PadLeft(fileListLength, '0')}{name}";
                        break;
                    case CounterOptions.Suffix:
                        newName = $"{name}{initialNumber}";
                        break;
                }

                string newFilePath = Path.Combine(file.FolderPath, newName + file.Extension);
                File.Move(Path.Combine(file.FolderPath, file.Name + file.Extension), newFilePath);
                file.Name = newName;
                initialNumber += increment;
            }
        }


        public static void RestartApplication()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = Process.GetCurrentProcess().MainModule.FileName,
                UseShellExecute = true
            };
            Process.Start(startInfo);
            Application.Current.Shutdown();
        }

    }
}
