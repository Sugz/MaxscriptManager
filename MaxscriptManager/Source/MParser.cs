using MaxscriptManager.Model;
using SugzTools.Extensions;
using SugzTools.Src;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MaxscriptManager.Src
{
    public static class MParser
    {

        private static TextBlock FormatStandardCode(string s)
        {
            TextBlock run = new TextBlock() { Text = s, TextWrapping = TextWrapping.NoWrap };
            if (MConstants.ClassDef.Concat(MConstants.BlueWords).Contains(s.ToLower()))
                run.Foreground = new SolidColorBrush(Colors.CornflowerBlue);
            else if (s.StartsWith("\"") && s.EndsWith("\""))
                run.Foreground = new SolidColorBrush(Colors.Pink);
            return run;
        }

        private static TextBlock FormatCommentary(string s)
        {
            TextBlock run = new TextBlock() { Text = s, TextWrapping = TextWrapping.NoWrap };
            run.Foreground = Resource<SolidColorBrush>.GetColor("MaxGreen");
            run.FontStyle = FontStyles.Italic;
            return run;
        }

        public static void FormatCode(StringCollection code, ref FlowDocument document)
        {
            bool isCommentary = false;
            for (int i = 0; i < code.Count; i++)
            {
                Paragraph paragraph = new Paragraph();
                string line = code[i];

                string[] splitters = { "\t" };
                string[] lineSplit = line.SplitAndKeep(MConstants.ComDelimiters.Concat(splitters).ToArray()).ToArray();
                if (lineSplit.Length == 1)
                {
                    if (isCommentary)
                        paragraph.Inlines.Add(FormatCommentary(line));
                    else
                        line.SplitAndKeep(' ').ForEach(x => paragraph.Inlines.Add(FormatStandardCode(x)));
                        //paragraph.Inlines.Add(FormatStandardCode(line));
                }
                    

                else if (lineSplit.Length > 1)
                {
                    bool singleLine = false;
                    foreach (string linePart in lineSplit)
                    {
                        // Start or end of commentary
                        if (Array.FindIndex(MConstants.ComDelimiters, x => linePart == x) is int comIndex && comIndex != -1)
                        {
                            isCommentary = (comIndex != 2);
                            singleLine = (comIndex == 0);
                            paragraph.Inlines.Add(FormatCommentary(linePart));
                        }
                        // Currently in commentary
                        else if (isCommentary)
                            paragraph.Inlines.Add(FormatCommentary(linePart));
                        // standard code
                        else
                            linePart.SplitAndKeep(' ').ForEach(x => paragraph.Inlines.Add(FormatStandardCode(x)));
                            //paragraph.Inlines.Add(new Run(linePart));
                    }
                    // Stop commentary formating if the commentary started with --
                    if (singleLine)
                        isCommentary = false;
                }

                document.Blocks.Add(paragraph);
            }
        }

    }
}
