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

        private static Run FormatStandardCode(string s)
        {
            Run run = new Run(s);
            if (MConstants.ClassDef.Concat(MConstants.BlueWords).Contains(s.ToLower()))
                run.Foreground = Resource<SolidColorBrush>.GetColor("BlueText");
            else if (MConstants.LightBlueWords.Contains(s.ToLower()))
                run.Foreground = Resource<SolidColorBrush>.GetColor("LightBlueText");
            else if (float.TryParse(s, out float result))
                run.Foreground = Resource<SolidColorBrush>.GetColor("OrangeText");
            else if (s.StartsWith("#"))
                run.Foreground = Resource<SolidColorBrush>.GetColor("PinkText");
            return run;
        }


        private static Run FormatString(string s)
        {
            Run run = new Run(s);
            run.Foreground = Resource<SolidColorBrush>.GetColor("StringColor");
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
            bool isString = false;
            foreach ( string line in code)
            {
                Paragraph paragraph = new Paragraph();

                // Line contain strings or commentary
                string[] delims = MConstants.ComDelimiters.Concat(new string[] { "\"" }).ToArray();
                if (delims.Any(x => line.Contains(x)) || isString || isCommentary)
                {
                    bool singleLine = false;
                    foreach (string str in line.SplitAndKeep(delims))
                    {
                        // Begining or end of commentary
                        if (Array.FindIndex(MConstants.ComDelimiters, x => str == x) is int comIndex && comIndex != -1)
                        {
                            isCommentary = (comIndex != 2);
                            singleLine = (comIndex == 0);
                            paragraph.Inlines.Add(FormatCommentary(str));
                        }
                        // Currently in commentary
                        else if (isCommentary)
                            paragraph.Inlines.Add(FormatCommentary(str));
                        // Begining or end of string
                        else if (str == "\"")
                        {
                            isString = !isString;
                            paragraph.Inlines.Add(FormatString(str));
                        }
                        // Currently in string
                        else if (isString)
                            paragraph.Inlines.Add(FormatString(str));
                        // Standard code
                        else
                            str.SplitAndKeep("\t ".ToCharArray()).ForEach(x => paragraph.Inlines.Add(FormatStandardCode(x)));
                    }
                    if (singleLine)
                        isCommentary = false;
                }
                // Standard code
                else
                    line.SplitAndKeep("\t ".ToCharArray()).ForEach(x => paragraph.Inlines.Add(FormatStandardCode(x)));

                document.Blocks.Add(paragraph);
            }
        }

    }
}
