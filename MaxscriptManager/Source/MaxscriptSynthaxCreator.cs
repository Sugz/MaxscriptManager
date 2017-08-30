using SugzTools.Src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MaxscriptManager.Src
{
    public static class MaxscriptSynthaxCreator
    {

        public static void Create()
        {
            using (StreamWriter file = new StreamWriter(@"D:\MaxscriptSynthax.txt"))
            {
                foreach (KeyValuePair<string, string[]> pair in MConstants.ParserColors)
                {
                    file.WriteLine($"<Keywords color={pair.Key}>");
                    foreach(string s in pair.Value)
                    {
                        file.WriteLine($"<Word>{s}</Word>");
                    }
                    file.WriteLine("</Keywords>");
                    file.WriteLine("\n");
                }
            }
        }
        
    }
}
