using SugzTools.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaxscriptManager.Model
{
    public class MCodeItem : MDataItem
    {

        #region Fields

        private string _Code;
        private bool _CodeChanged = false;
        private int _CaretOffset = 0;
        private Vector _ScrollOffset;
        private bool _IsActive;

        #endregion Fields


        #region Properties


        public StringCollection Description { get; protected set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            get => _Code;
            set => Set(ref _Code, value);
        }

        
        /// <summary>
        /// Get or set if the code has changed
        /// </summary>
        public bool CodeChanged
        {
            get => _CodeChanged;
            set => Set(ref _CodeChanged, value);
        }

        
        /// <summary>
        /// Get or set the editor caret position
        /// </summary>
        public int CaretOffset
        {
            get => _CaretOffset;
            set => Set(ref _CaretOffset, value);
        }


        /// <summary>
        /// Get or set the editor scroll offset
        /// </summary>
        public Vector ScrollOffset
        {
            get => _ScrollOffset;
            set => Set(ref _ScrollOffset, value);
        }


        /// <summary>
        /// Get or set if this is the active item in the editor, the tabs and the current treeview
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

        /// <summary>
        /// Get or set if this is the selected item in the treeviews except the current
        /// </summary>
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
