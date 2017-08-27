using GalaSoft.MvvmLight;
using MaxscriptManager.Model;
using MaxscriptManager.Src;
using SugzTools.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace MaxscriptManager.ViewModel
{
    public class MMDescriptionVM : ViewModelBase
    {

        #region Fields


        private bool _ShowCode = false;
        private MMDataItem _SelectedItem;                                                                   // Treeview selected item
        private FlowDocument _Document;                                                                     // The flowdocument 


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
        public MMDataItem SelectedItem
        {
            get { return _SelectedItem; }
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
        public FlowDocument Document
        {
            get { return _Document; }
            set { Set(ref _Document, value); }
        }


        #endregion Properties


        #region Constructor


        public MMDescriptionVM()
        {
            // Get selected treeview item
            MessengerInstance.Register<MMSelectedItemMessage>(this, x => SelectedItem = x.NewItem);
        }


        #endregion Constructor



        /// <summary>
        /// Create the flowdocument and apply the styles
        /// </summary>
        private void InitializeDocument()
        {
            Document = new FlowDocument();
            Document.Resources.Add(typeof(FlowDocument), (Style)Application.Current.Resources["FlowDocumentStyle"]);
            Document.Resources.Add(typeof(Paragraph), (Style)Application.Current.Resources["ParagraphStyle"]);
            Document.Resources.Add(typeof(List), (Style)Application.Current.Resources["ListStyle"]);
        }


        /// <summary>
        /// Set the flowdocument from selected item
        /// </summary>
        private void GetDocument()
        {
            // Initialize the flowdocument
            if (Document is null)
                InitializeDocument();
            Document.Blocks.Clear();

            if ((SelectedItem as MMCodeItem) is MMCodeItem item)
            {
                StringCollection strs = ShowCode ? item.Code : item.Description;
                foreach (string s in strs)
                    Document.Blocks.Add(new Paragraph(new Run(s)));
            }

        }
    }
}
