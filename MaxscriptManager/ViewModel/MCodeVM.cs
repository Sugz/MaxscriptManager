using GalaSoft.MvvmLight;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using MaxscriptManager.Model;
using MaxscriptManager.Src;
using SugzTools.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xml;

namespace MaxscriptManager.ViewModel
{
    public class MCodeVM : ViewModelBase
    {

        #region Fields


        private bool _ShowCode = true;
        private MDataItem _SelectedItem;                                                                   // Treeview selected item
        //private FlowDocument _Document;                                                                     // The flowdocument 
        private TextDocument _Document = new TextDocument();

        FoldingManager foldingManager;
        MaxscriptFoldingStrategy foldingStrategy = new MaxscriptFoldingStrategy();

        #endregion Fields


        #region Properties


        /// <summary>
        /// 
        /// </summary>
        public bool ShowCode
        {
            get => _ShowCode;
            set
            {
                Set(ref _ShowCode, value);
                GetDocument();
            }
        }



        /// <summary>
        /// Treeview selected item. Set the flowdocument when updated
        /// </summary>
        public MDataItem SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                GetDocument();
                SelectedItem.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "IsValidPath")
                        GetDocument();
                };
            }
        }

        /// <summary>
        /// The flowdocument used to binding  
        /// </summary>
        public TextDocument Document
        {
            get => _Document;
            set => Set(ref _Document, value);
        }


        public IHighlightingDefinition SyntaxHighlighting { get; private set; }


        #endregion Properties


        #region Constructor


        public MCodeVM()
        {
            // Get selected treeview item
            MessengerInstance.Register<MSelectedItemMessage>(this, x => SelectedItem = x.NewItem);
            InitializeDocument();

        }


        #endregion Constructor



        /// <summary>
        /// Create the flowdocument and apply the styles
        /// </summary>
        private void InitializeDocument()
        {
            using (Stream s = typeof(MCodeVM).Assembly.GetManifestResourceStream("MaxscriptManager.Resource.Maxscript.xshd"))
            {
                if (s == null)
                    throw new InvalidOperationException("Could not find embedded resource");
                using (XmlReader reader = new XmlTextReader(s))
                    SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            }

            HighlightingManager.Instance.RegisterHighlighting("Custom Highlighting", new string[] { ".cool" }, SyntaxHighlighting);
        }


        /// <summary>
        /// Set the flowdocument from selected item
        /// </summary>
        private void GetDocument()
        {
            if ((SelectedItem as MCodeItem) is MCodeItem item && item.IsSelected)
            {
                if (ShowCode)
                {
                    string code = string.Empty;
                    item.Code.ForEach(x => code += $"{x}\n");
                    Document.Text = code;
                }
            }
            
        }
    }
}
