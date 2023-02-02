using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassFiles.Classes
{
    public class Text
    {
        #region ----- Fields -----

        private string fileText;

        private int lineNumber;

        #endregion

        #region ------ Properties ------

        public string FileText
        {
            get { return fileText; }
            set { fileText = value; }
        }

        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        #endregion

        #region ------ Constructor ------

        public Text(string fileText, int lineNumber)
        {
            this.FileText = fileText;
            this.LineNumber = lineNumber;
        }

        #endregion
    }
}
