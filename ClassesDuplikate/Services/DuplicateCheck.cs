using ClassFiles.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassFiles.Services
{
    public class DuplicateCheck
    {
        /// <summary>
        /// This method checks if there is a duplicate line
        /// </summary>
        /// <param name="files">A list of files</param>
        /// <param name="output">The output</param>
        public static void DoubleCheck(List<FilesRead> files, out Dictionary<string, Output> output)
        {
            // Test1: null
           // Test2: null Eintrag
           // Test3: Eintrag null
           // Test4: Eintrag
           // Test5: Eintrag Eintrag

            List<FileInfo> file = new List<FileInfo>();
            List<int> linenumber = new List<int>();
            output = new Dictionary<string, Output>();
            for (int i = 0; i < files.Count - 1; i++)
            {
                for (int y = 0; y < files[i].FileText.Count; y++)
                {
                    Text firstFileText = files[i].FileText[y];
                    FilesRead firstfile = files[i];
                    for (int x = 0; x < files.Count - 1; x++)
                    {
                        FilesRead secendFile = files[x + 1];
                        OutputCheck(output, firstFileText, firstfile, secendFile);
                    }
                }
            }
        }

        public static void OutputAdd(Dictionary<string, Output> output, Text textRead, FilesRead filesRead)
        {
            // Test1: Console.WriteLine(); 10 File esrtellen
            // Test2: textRead = null Fileerstellen
            // Test3: TextRead = Console.WriteLine(); 50 filesread = null;
            // Test4: beide null
            output[textRead.FileText].GetUPDuplicate();
            output[textRead.FileText].FileName.Add(filesRead.FileInfo);
            output[textRead.FileText].LineNumber.Add(textRead.LineNumber);
        }


        public static void OutputCheck(Dictionary<string, Output> output, Text firstFileText, FilesRead firstFile, FilesRead secendFile)
        {

            // Test1: null null null
            // Test2: cw null null
            // Test3: null, file, null
            // Test4: null, null file
            // Test5: cw, file, null
            // Test6: null file, file
            // Test7: cw, null, file
            // Test8: cw, file, file

            if (!output.ContainsKey(firstFileText.FileText))
            {
                for (int k = 0; k < secendFile.FileText.Count; k++)
                {
                    if (firstFileText.FileText != secendFile.FileText[k].FileText) continue;
                    if (!output.ContainsKey(firstFileText.FileText))
                    {
                        output.Add(firstFileText.FileText, new Output(new List<FileInfo>() { firstFile.FileInfo }, 1, new List<int>() { firstFileText.LineNumber }));
                    }
                    else
                    {
                        if (!output[firstFileText.FileText].FileName.Contains(firstFile.FileInfo))
                        {
                            OutputAdd(output, firstFileText, firstFile);
                        }

                    }

                }
            }
            else
            {
                if (!output[firstFileText.FileText].FileName.Contains(firstFile.FileInfo))
                {
                   OutputAdd(output, firstFileText, firstFile);
                }
                
            }
        }
    }
}
