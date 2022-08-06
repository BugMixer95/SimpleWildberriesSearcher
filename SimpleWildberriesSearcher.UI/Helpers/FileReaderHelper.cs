using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWildberriesSearcher.UI.Helpers
{
    internal static class FileReaderHelper
    {
        /// <summary>
        /// Returns a collection of strings which are read from a file.
        /// </summary>
        /// <param name="filePath">Path to a file.</param>
        public static IEnumerable<string> ReadFile(string filePath)
        {
            List<string> result = new();

            using (var reader = new StreamReader(filePath))
            {
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }
}
