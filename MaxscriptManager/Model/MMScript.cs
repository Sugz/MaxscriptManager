using MaxscriptManager.Src;
using SugzTools.Src;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxscriptManager.Model
{
    public class MMScript : MMCodeItem, IMMPathItem
    {
        #region Fields

        public const string UseModify = "*Use / Modify this script at your own risk !*";
        public const string descriptionStart = "/*##############################################################################";
        public const string descriptionEnd = "###############################################################################*/";

        //private string _Text;
        private bool _IsValidPath;
        private string _Path;


        #endregion Fields

        #region Properties


        public override MMDataType DataType => MMDataType.Script;
        public override string Text
        {
            get => _Text ?? (_Text = System.IO.Path.GetFileNameWithoutExtension(Path));
            set => Set(ref _Text, value);
        }
        public bool IsValidPath
        {
            get => _IsValidPath;
            set => Set(ref _IsValidPath, value);
        }
        public string Path
        {
            get => _Path;
            set
            {
                Set(ref _Path, value);
                IsValidPath = File.Exists(Path);
                if (IsValidPath && Code is null)
                    Code = GetCode();
            }
        }
        public string RelativePath => throw new NotImplementedException();
        public override StringCollection Code
        {
            get => base.Code ?? (base.Code = GetCode());
            set => base.Code = value;
        }


        #endregion Properties


        #region Constructors


        public MMScript(object parent, string path) : base(parent)
        {
            Path = path;
        }


        #endregion Constructors



        private StringCollection GetCode()
        {
            if (!IsValidPath)
                return null;

            StreamReader streamReader = new StreamReader(Path, Encoding.GetEncoding("iso-8859-1"));
            PeekableStreamReaderAdapter peekStreamReader = new PeekableStreamReaderAdapter(streamReader);
            StringCollection code = new StringCollection();
            while (!streamReader.EndOfStream)
            {
                string line = peekStreamReader.PeekLine();
                line = line.TrimStart().ToLower();

                if (line == descriptionStart)
                {
                    StringCollection description = new StringCollection();
                    while (line != descriptionEnd)
                    {
                        line = peekStreamReader.ReadLine();
                        description.Add(line);
                        code.Add(line);
                    }
                    Description = description;
                }

                else if (Array.FindIndex(_ClassDef, x => line.Contains(x)) is int index && index != -1)
                {
                    MMDataType type = MMDataType.Struct;
                    if (index == 1) type = MMDataType.Rollout;
                    if (index == 2 || index == 3) type = MMDataType.Function;

                    if (Children is null)
                        Children = new ObservableCollection<MMDataItem>();
                    string text = line.Trim("\t".ToCharArray());

                    StringCollection childCode = new StringCollection();
                    int openCount = 0, closeCount = 0;
                    while (!streamReader.EndOfStream)
                    {
                        line = peekStreamReader.ReadLine();
                        childCode.Add(line);
                        code.Add(line);
                        openCount += line.Count(x => x == '(');
                        closeCount += line.Count(x => x == ')');
                        if (openCount != 0 && closeCount == openCount)
                            break;
                    }
                    //child.Code = childCode;
                    Children.Add(new MMCodeItem(this, text, type, childCode));
                }

                else
                    code.Add(peekStreamReader.ReadLine());
            }

            streamReader.Close();
            return code;
        }
    }
}
