using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using MaxscriptManager.Control;
using MaxscriptManager.Model;
using MaxscriptManager.Src;
using SugzTools.Extensions;
using SugzTools.Src;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    //TODO: switch selected item to MCodeItem
    public class MCodeVM : ViewModelBase
    {

        #region Fields

        private Browser _Browser = new Browser();

        
        private bool _ShowCode = true;
        private MDataItem _SelectedItem;                                                                    // Treeview selected item
        //private FlowDocument _Document;                                                                   // The flowdocument 
        //private TextDocument _Document = new TextDocument();
        private string _Code;
        private int _CaretOffset;
        private Vector _ScrollOffset;

        private Visibility _EditorPanelVisibility = Visibility.Collapsed;

        #endregion Fields


        #region Properties

        public IHighlightingDefinition SyntaxHighlighting { get; private set; }


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
        /// Get or set selected item code
        /// </summary>
        public string Code
        {
            get => _Code;
            set
            {
                Set(ref _Code, value);
                if ((SelectedItem as MCodeItem) is MCodeItem item && item.Code != value)
                {
                    item.CodeChanged = true;
                    //item.Code = value;
                }
            }
        }





        /// <summary>
        /// 
        /// </summary>
        public Visibility EditorPanelVisibility
        {
            get { return _EditorPanelVisibility; }
            set { Set(ref _EditorPanelVisibility, value); }
        }


        #endregion Properties


        #region Constructor


        public MCodeVM()
        {
            // Get selected treeview item
            MessengerInstance.Register<MSelectedItemMessage>(this, x => SelectedItem = x.NewItem);
            InitializeDocument();
        }


        #endregion Constructor



        
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
            if ((SelectedItem as MCodeItem) is MCodeItem item)
            {
                EditorPanelVisibility = Visibility.Visible;
                if (ShowCode)
                {
                    Code = item.Code;
                    //item.CodeLoaded = true;
                }
            }
        }

    }
}
