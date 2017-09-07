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
        private bool _IsActive;

        #endregion Fields


        #region Properties

        public StringCollection Description { get; protected set; }
        public string Code { get; set; }

        
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive
        {
            get => _IsActive;
            set
            {
                Set(ref _IsActive, value);
                if (value && !_IsSelected)
                    IsSelected = true;
                else if (!value && _IsSelected)
                    IsSelected = false;
            }
        }

        public override bool IsSelected
        {
            get => base.IsSelected;
            set
            {
                base.IsSelected = value;
                if (value)
                    IsActive = true;
            }
        }

        #endregion Properties


        #region Constructors


        public MCodeItem()
        {

        }
        public MCodeItem(object parent)
        {
            Parent = parent;
        }
        public MCodeItem(object parent, MDataType type )
        {
            Parent = parent;
            DataType = type;
        }
        public MCodeItem(object parent, string text, MDataType type)
        {
            Parent = parent;
            Text = text;
            DataType = type;
        }
        public MCodeItem(object parent, string text, MDataType type, string code)
        {
            Parent = parent;
            Text = text;
            DataType = type;
            Code = code;
        }


        #endregion Constructors


        #region Methods




        protected override ObservableCollection<MDataItem> GetChildren() => null;


        #endregion Methods
    }
}
