using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassFiles.Classes;

namespace ClassFiles.Services
{
    public class FilesAdd
    {
        /// <summary>
        /// The lines from files get put into the files list with the linenumber and fileInfo.
        /// </summary>
        /// <param name="fileList"></param>
        /// <param name="ignorefile"></param>
        /// <param name="files"></param>
        public static void Files(List<string> fileList, string ignorefile, out List<FilesRead> files)
        {
            files = new List<FilesRead>();
            for (int i = 0; i < fileList.Count; i++)
            {
                // The class fileInfo
                FileInfo fileInfo = new FileInfo(fileList[i]);
                // writes all the lines that are not ignored into the text list
                List<string> text = IgnoreFile.IgnoreLines(fileList[i], ignorefile, out List<int> filelinecount);
                LinesAdd.Lines(filelinecount, text, out List<Text> lines);
                files.Add(new FilesRead(fileInfo, lines.ToList()));
                lines.Clear();
            }
        }
    }
}
