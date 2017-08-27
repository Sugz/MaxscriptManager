using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxscriptManager.Model
{
    public class MMCodeItem : MMDataItem
    {

        #region Fields

        protected readonly string[] _ClassDef = { "struct", "rollout", "fn", "function" };
        //protected StringCollection _Description;
        //protected StringCollection _Code;


        #endregion Fields


        #region Properties

        public virtual StringCollection Description { get; protected set; }
        //{
        //    get => _Description ?? (Children = GetChildren());
        //    set => _Description = value;
        //}
        public virtual StringCollection Code { get; set; }
        //{
        //    get => _Code;
        //    set => _Code = value;
        //} 


        #endregion Properties


        #region Constructors


        public MMCodeItem(object parent)
        {
            Parent = parent;
        }
        public MMCodeItem(object parent, MMDataType type )
        {
            Parent = parent;
            DataType = type;
        }
        public MMCodeItem(object parent, string text, MMDataType type)
        {
            Parent = parent;
            Text = text;
            DataType = type;
        }
        public MMCodeItem(object parent, string text, MMDataType type, StringCollection code)
        {
            Parent = parent;
            Text = text;
            DataType = type;
            Code = code;
        }


        #endregion Constructors


        #region Methods


        protected override ObservableCollection<MMDataItem> GetChildren()
        {
            ObservableCollection<MMDataItem> children = new ObservableCollection<MMDataItem>();
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
