using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassFiles.Services
{
    public class IgnoreFile
    {
        /// <summary>
        /// This method creates the ignore file and writes some lines into it if it does not exist
        /// </summary>
        /// <param name="currentDirectory">This is the current directory of the project</param>
        /// <returns>
        /// Returns the ignore file string
        /// </returns>
        public static string CreateIgnoreFile(string currentDirectory)
        {
            string ignorefile = Path.Combine(currentDirectory, @"ignore.txt");
            string ignorefilePath = Path.GetFullPath(ignorefile);

            if (!File.Exists(ignorefile))
            {
                using (StreamWriter sw = File.CreateText(ignorefilePath))
                {
                    WriteIntoIgnoreFile(sw);
                }
                Log.log.Info("IgnoreFile Created!");
            }

            return ignorefile;
        }

        /// <summary>
        /// This method writes these standard lines into the ignore file.
        /// </summary>
        /// <param name="sw">This is the stream writer.</param>
        public static void WriteIntoIgnoreFile(StreamWriter sw)
        {
            sw.WriteLine("// * = removes line if this symbol is the only thing in the line");
            sw.WriteLine("*{");
            sw.WriteLine("*}");

            sw.WriteLine("// removes the line if the line contains this");
            sw.WriteLine("using system");
            sw.WriteLine("using file");
        }



        /// <summary>
        /// This method reads the lines from the cs file and compares them to the lines in the ignore file
        /// </summary>
        /// <param name="file">the path of the code file</param>
        /// <param name="ignorefile">the path of the ignore file</param>
        /// <param name="filelinecount">a list of the line numbers</param>
        /// <returns>
        /// Returns a list with not ignored lines of code
        /// </returns>
        public static List<string> IgnoreLines(string file, string ignorefile, out List<int> filelinecount)
        {
            List<string> lines = new List<string>();
            List<string> ignorelines = new List<string>();
            ignorelines.AddRange(File.ReadAllLines(ignorefile));
            ignorelines.Remove(string.Empty);
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

                    for (int i = 0; i < ignorelines.Count(); i++)
                    {
                        ignorelines[i] = ignorelines[i].ToLower();

                        if (ignorelines[i].StartsWith("*"))
                        {
                            if (fileline == ignorelines[i].TrimStart().TrimEnd().Remove(0, 1))
                            {
                                goodline = false;
                                break;
                            }
                        }
                        else
                        {
                            if (fileline.Contains(ignorelines[i]))
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
                            lines.Add(fileline);
                            filelinecount.Add(count);
                        }
                    }

                    goodline = true;
                }
            }

            return lines;
        }
    }
}
