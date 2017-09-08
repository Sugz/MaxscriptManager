using ICSharpCode.AvalonEdit.Document;
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
    public class MScript : MCodeItem, IMPathItem
    {

        #region Fields

        private bool _IsValidPath;
        private string _Path;


        #endregion Fields


        #region Properties


        public override MDataType DataType => MDataType.Script;
        public override string Text
        {
            get => _Text ?? (_Text = System.IO.Path.GetFileName(Path));
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
                if (IsValidPath && Children is null)
                    Children = GetChildren();
            }
        }
        public string RelativePath => throw new NotImplementedException();


        #endregion Properties


        #region Constructors


        public MScript(string text, string code)
        {
            Text = text;
            Code = Code;
            Parent = null;
            Path = null;
        }
        public MScript(object parent, string path) : base(parent)
        {
            Path = path;
        }
        public MScript(object parent, string path, string text) : base(parent)
        {
            Path = path;
            Text = text;
        }


        #endregion Constructors


        /// <summary>
        /// Get all the script code, then get the desciprtion and children
        /// </summary>
        /// <returns></returns>
        protected override ObservableCollection<MDataItem> GetChildren()
        {
            if (!IsValidPath)
                return null;

            StringBuilder code = new StringBuilder();
            StreamReader streamReader = new StreamReader(Path, Encoding.GetEncoding("iso-8859-1"));
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                code.AppendLine(line);
            }

            Code = code.ToString();
            streamReader.Close();
            return new ObservableCollection<MDataItem>();
        }
    }
}
