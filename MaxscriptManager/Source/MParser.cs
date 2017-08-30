using MaxscriptManager.Model;
using SugzTools.Extensions;
using SugzTools.Src;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MaxscriptManager.Src
{
    public static class MParser
    {

        public static IEnumerable<Run> FormatFunctionName(string name)
        {
            foreach (string str in Regex.Split(name, @"(?<=[ :])"))
            {
                Run run = new Run(str);
                if (str.EndsWith(":"))
                {
                    run.Foreground = new SolidColorBrush(Color.FromArgb(255, 185, 185, 185));
                    run.FontStyle = FontStyles.Italic;
                }
                yield return run;
            }
        }

        private static void FormatStandardCode(string str, ref Paragraph paragraph)
        {
            foreach(string split in str.SplitAndKeep("\t ".ToCharArray()))
            {
                foreach (string matches in Regex.Matches(split, "[0-9.]+|[^0-9.]+").Cast<Match>().Select(match => match.Value))
                {
                    foreach (string s in Regex.Split(matches, @"(?<=[ :])"))
                    {
                        Run run = new Run(s);
                        if (float.TryParse(s, out float result))
                            run.Foreground = Resource<SolidColorBrush>.GetColor("OrangeText");
                        else if (s.EndsWith(":"))
                        {
                            run.Foreground = new SolidColorBrush(Color.FromArgb(255, 185, 185, 185));
                            run.FontStyle = FontStyles.Italic;
                        }
                        else if (s.StartsWith("#"))
                            run.Foreground = Resource<SolidColorBrush>.GetColor("PinkText");
                        else if (MConstants.ParserColors["ControlColor"].Contains(s.ToLower()))
                            run.Foreground = Resource<SolidColorBrush>.GetColor("ControlColor");
                        else if (MConstants.ClassDef.Contains(s.ToLower()) || MConstants.ParserColors["BlueText"].Contains(s.ToLower()))
                            run.Foreground = Resource<SolidColorBrush>.GetColor("BlueText");
                        else if (MConstants.ParserColors["LightBlueText"].Contains(s.ToLower()))
                            run.Foreground = Resource<SolidColorBrush>.GetColor("LightBlueText");
                        else if (MConstants.ParserColors["DarkBlueText"].Contains(s.ToLower()))
                            run.Foreground = Resource<SolidColorBrush>.GetColor("DarkBlueText");
                        else if (MConstants.ParserColors["VioletText"].Contains(s.ToLower()))
                            run.Foreground = Resource<SolidColorBrush>.GetColor("VioletText");
                        else if (MConstants.ParserColors["LightRedText"].Contains(s.ToLower()))
                            run.Foreground = Resource<SolidColorBrush>.GetColor("LightRedText");
                        else if (MConstants.ParserColors["GoldText"].Contains(s.ToLower()))
                            run.Foreground = Resource<SolidColorBrush>.GetColor("GoldText");
                        else if (MConstants.ParserColors["YellowText"].Contains(s.ToLower()))
                            run.Foreground = Resource<SolidColorBrush>.GetColor("YellowText");
                        else if (MConstants.ParserColors["LightGreenText"].Contains(s.ToLower()))
                            run.Foreground = Resource<SolidColorBrush>.GetColor("LightGreenText");
                        else if (MConstants.ParserColors["DarkGreenText"].Contains(s.ToLower()))
                            run.Foreground = Resource<SolidColorBrush>.GetColor("DarkGreenText");

                        paragraph.Inlines.Add(run);
                    }
                }
            }
        }


        private static Run FormatString(string s)
        {
            Run run = new Run(s);
            run.Foreground = Resource<SolidColorBrush>.GetColor("StringColor");
            return run;
        }


        private static Run FormatCommentary(string s)
        {
            Run run = new Run(s);
            run.Foreground = Resource<SolidColorBrush>.GetColor("CommentaryColor");
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

                    // string has priorities over commentaries but not if a commentary has already begun
                    foreach (string str in line.SplitAndKeep(delims))
                    {
                        // Begining or end of string
                        if (str == "\"" && !isCommentary)
                        {
                            isString = !isString;
                            paragraph.Inlines.Add(FormatString(str));
                        }
                        // Currently in string
                        else if (isString && !isCommentary)
                            paragraph.Inlines.Add(FormatString(str));
                        // Begining or end of commentary
                        else if (Array.FindIndex(MConstants.ComDelimiters, x => str == x) is int comIndex && comIndex != -1)
                        {
                            isCommentary = (comIndex != 2);
                            singleLine = (comIndex == 0);
                            paragraph.Inlines.Add(FormatCommentary(str));
                        }
                        // Currently in commentary
                        else if (isCommentary)
                            paragraph.Inlines.Add(FormatCommentary(str));
                        // Standard code
                        else
                        {
                            FormatStandardCode(str, ref paragraph);
                            //str.SplitAndKeep("\t ".ToCharArray()).ForEach(x => paragraph.Inlines.Add(FormatStandardCode(x)));
                        }
                            
                    }
                    if (singleLine)
                        isCommentary = false;
                }
                // Standard code
                else
                    FormatStandardCode(line, ref paragraph);
                    //line.SplitAndKeep("\t ".ToCharArray()).ForEach(x => paragraph.Inlines.Add(FormatStandardCode(x)));

                document.Blocks.Add(paragraph);
            }
        }

    }
}
