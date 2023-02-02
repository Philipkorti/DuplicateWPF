using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ClassFiles.Classes;

namespace ClassFiles
{
    public class FilesRead
    {
        #region ------ Fields ------

        private FileInfo fileInfo;

        private List<Text> fileText;

        #endregion

        #region ------ Properties-----

        public List<Text> FileText
        {
            get { return fileText; }
            set { fileText = value; }
        }
        public FileInfo FileInfo
        {
            get { return fileInfo; }
            set { fileInfo = value; }
        }

        #endregion
        
        public FilesRead(FileInfo fileInfo ,List<Text> filetext)
        {
            
            this.FileText = filetext;
            this.FileInfo = fileInfo;
        }
    }
}
