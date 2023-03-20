using ClassFiles.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesDuplikate.Services
{
    public class IgnorePaths
    {
        /// <summary>
        /// This method creates the ignore file and writes some Paths into it if it does not exist
        /// </summary>
        /// <param name="currentDirectory">This is the current directory of the project</param>
        /// <returns>
        /// Returns the ignore file string
        /// </returns>
        public static string CreateIgnorePathFile(string currentDirectory)
        {
            string ignorePathsFile = Path.Combine(currentDirectory + "/ignore", @"ignorePaths.txt");
            string ignorePathsFilePath = Path.GetFullPath(ignorePathsFile);

            if (!File.Exists(ignorePathsFile))
            {
                using (StreamWriter sw = File.CreateText(ignorePathsFilePath))
                {
                    WriteIntoignorePathsFile(sw);
                }
                Log.log.Info("ignorePathsFile Created!");
            }

            return ignorePathsFile;
        }

        /// <summary>
        /// This method writes these standard Paths into the ignore file.
        /// </summary>
        /// <param name="sw">This is the stream writer.</param>
        public static void WriteIntoignorePathsFile(StreamWriter sw)
        {
            sw.WriteLine("");
            sw.WriteLine("");
        }


        /// <summary>
        /// This method reads the Paths from the cs file and compares them to the Paths in the ignore file
        /// </summary>
        /// <param name="file">the path of the code file</param>
        /// <param name="ignorePathsFile">the path of the ignore file</param>
        /// <param name="filelinecount">a list of the line numbers</param>
        /// <returns>
        /// Returns a list with not ignored Paths of code
        /// </returns>
        public static List<string> IgnoreFilePaths(string file, string ignorePathsFile, out List<int> filelinecount)
        {
            List<string> Paths = new List<string>();
            List<string> ignorePaths = new List<string>();
            ignorePaths.AddRange(File.ReadAllLines(ignorePathsFile));
            ignorePaths.Remove(string.Empty);
            int count = 0;
            filelinecount = new List<int>();

            string fileline;
            bool goodline = true;
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    count++;
                    fileline = reader.ReadLine().TrimStart().TrimEnd().ToLower();

                    for (int i = 0; i < ignorePaths.Count(); i++)
                    {
                        ignorePaths[i] = ignorePaths[i].ToLower();

                        if (ignorePaths[i].StartsWith("*"))
                        {
                            if (fileline == ignorePaths[i].TrimStart().TrimEnd().Remove(0, 1))
                            {
                                goodline = false;
                                break;
                            }
                        }
                        else
                        {
                            if (fileline.Contains(ignorePaths[i]))
                            {
                                goodline = false;
                                break;
                            }
                        }
                    }

                    if (goodline)
                    {
                        if (fileline.Length != 0)
                        {
                            Paths.Add(fileline);
                            filelinecount.Add(count);
                        }
                    }

                    goodline = true;
                }
            }

            return Paths;
        }
    }
}
