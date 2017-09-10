using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using MaxscriptManager.Source.Message;
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

        private TextDocument _Document = new TextDocument();
        private bool _IsModified;
        private string _Code;
        
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
        /// 
        /// </summary>
        public TextDocument Document
        {
            get => _Document;
            set => Set(ref _Document, value);
        }


        /// <summary>
        /// 
        /// </summary>
        public bool IsModified
        {
            get => _IsModified;
            set => Set(ref _IsModified, value);
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
                if (value)
                    MessengerInstance.Send(new MActiveItemMessage(this));
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


        public MCodeItem() : this(null, null, MDataType.Script, null) { }
        public MCodeItem(object parent) : this(parent, null, MDataType.Script, null) { }
        public MCodeItem(object parent, MDataType type) : this(parent, null, type, null) { }
        public MCodeItem(object parent, string text, MDataType type) : this(parent, text, type, null) { }
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
