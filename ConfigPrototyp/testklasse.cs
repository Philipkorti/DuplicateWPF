using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConfigPrototyp
{
    class testklasse
    {
        #region ----- Fields ------

        private int duplicatenumber;
        private List<int> lineNumber;
        private List<FileInfo> fileName;

        #endregion

        #region ------ Properties ------

        public int Duplicatenumber
        {
            get { return duplicatenumber; }
            set { duplicatenumber = value; }
        }

        public List<int> LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        public List<FileInfo> FileName
        {
            get { return fileName; }

            set { fileName = value; }
        }


        #endregion

        #region ------ Constructor ------

        public testklasse(List<FileInfo> fileName, int duplicatenumber, List<int> lineNumbers)
        {
            this.LineNumber = lineNumbers;
            this.FileName = fileName;
            this.Duplicatenumber = duplicatenumber;
        }

        public void GetUPDuplicate()
        {
            Duplicatenumber++;
        }

        #endregion
    }
}
