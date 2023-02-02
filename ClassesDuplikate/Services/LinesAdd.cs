using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassFiles.Classes;

namespace ClassFiles.Services
{
    public class LinesAdd
    {
        public static void Lines( List<int> filelinecount,List<string> text, out List<Text> lines)
        {
            lines = new List<Text>();
            for (int j = 0; j < text.Count(); j++)
            {
                try
                {
                    lines.Add(new Text(text[j], filelinecount[j]));
                }
                catch (Exception ex)
                {
                    Log.log.Error(ex.Message);
                }
            }
        }
    }
}
