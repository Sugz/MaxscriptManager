using SugzTools.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxscriptManager.Model
{
    public class MCodeItem : MDataItem
    {

        #region Fields

        protected readonly string[] _ClassDef = { "struct", "rollout", "fn", "function", "attributes", "macroscript", "parameters" };
        protected readonly string[] _ComDelimiters = { "/*", "*/", "--" };

        #endregion Fields


        #region Properties

        public StringCollection Description { get; protected set; }
        public StringCollection Code { get; protected set; }


        #endregion Properties


        #region Constructors


        public MCodeItem(object parent)
        {
            Parent = parent;
        }
        public MCodeItem(object parent, MMDataType type )
        {
            Parent = parent;
            DataType = type;
        }
        public MCodeItem(object parent, string text, MMDataType type)
        {
            Parent = parent;
            Text = text;
            DataType = type;
        }
        public MCodeItem(object parent, string text, MMDataType type, StringCollection code)
        {
            Parent = parent;
            Text = text;
            DataType = type;
            Code = code;
        }


        #endregion Constructors


        #region Methods




        protected override ObservableCollection<MDataItem> GetChildren()
        {
            ObservableCollection<MDataItem> children = new ObservableCollection<MDataItem>();
            //for (int i = 0; i < Code.Count; i++)
            //{
            //    string line = Code[i].TrimStart('\t');

            //    //Read line and check if it has comment => check what before the comments
            //    //Escape until the comment end
            //    string[] lineParts = line.SplitAndKeep(_ComDelimiters).ToArray();
            //    if (_ComDelimiters.Any(x => lineParts[0].Contains(x)))
            //    {

            //    }

            //    if (Array.FindIndex(_ClassDef, x => line.Contains(x)) is int index && index != -1)
            //    {
            //        // struct
            //        if (index == 0)
            //        {
                        
            //        }
            //        // rollout
            //        else if (index == 1)
            //        {

            //        }
            //        // function
            //        else
            //        {

            //        }
            //    }
            //}


            //for (int i = 1; i < Code.Count; i++)
            //{ 
            //    string line = Code[i];
            //    line = line.TrimStart().ToLower();
            //    if (Array.FindIndex(_ClassDef, x => line.Contains(x)) is int index && index != -1)
            //    {
            //        MMDataType type = MMDataType.Struct;
            //        if (index == 1) type = MMDataType.Rollout;
            //        if (index == 2 || index == 3) type = MMDataType.Function;



            //        StringCollection childCode = new StringCollection();
            //        int openCount = 0, closeCount = 0;

            //        for (int j = i; j < Code.Count; j++)
            //        {
            //            line = Code[j];
            //            childCode.Add(line);
            //            openCount += line.Count(x => x == '(');
            //            closeCount += line.Count(x => x == ')');
            //            if (openCount != 0 && closeCount == openCount)
            //                break;
            //        }
            //        children.Add(new MMCodeItem(this, line.Trim("\t".ToCharArray()), type, childCode));
            //    }
            //}
            return children;
        }


        #endregion Methods
    }
}
