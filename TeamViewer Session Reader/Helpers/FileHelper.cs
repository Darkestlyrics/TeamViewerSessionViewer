using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TeamViewer_Session_Reader.Helpers {
    static class FileHelper {
        /// <summary>
        /// Opens the file when the file is not locked and reads all lines
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>String array of all lines</returns>
        public static string[] ReadWhenPossible(string path) {
            return readWhenPossible(path);
        }
        private static string[] readWhenPossible(string p) {
            bool f = false;
            string[] s = null;
            while (!f)
                try {
                    s = File.ReadAllLines(p);
                    f = true;
                } catch (IOException) {
                    //file is locked
                }
            return s;
        }
        /// <summary>
        /// Search for all files based on passed criteria
        /// </summary>
        /// <param name="filter">The search string to match against the names of files in path. Supports Wildcards </param>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <returns></returns>
        public static List<string> SearchFiles(string filter = "*.txt", string path = @"c:\") {
            return Directory.EnumerateFiles(path, filter, SearchOption.AllDirectories).ToList();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchtext">Text to search for in matched files </param>
        /// <param name="filter">The search string to match against the names of files in path. Supports Wildcards </param>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <returns></returns>
        public static List<string> SearchFileByText(string searchtext, string filter = "*.txt", string path = @"c:\") {
            var files = from file in Directory.EnumerateFiles(path, filter, SearchOption.AllDirectories)
                        from line in File.ReadLines(file)
                        where line.Contains(searchtext)
                        select file;
           return files.ToList();
        }
    }
}
    

