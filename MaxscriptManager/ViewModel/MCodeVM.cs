using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using MaxscriptManager.Control;
using MaxscriptManager.Model;
using MaxscriptManager.Source.Message;
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

        private MCodeItem _ActiveItem;
        private TextDocument _Document = new TextDocument();
        private bool _IsModified;

        #endregion Fields


        #region Properties



        /// <summary>
        /// The Active item in the tabs
        /// </summary>
        public MCodeItem ActiveItem
        {
            get => _ActiveItem;
            set
            {
                Set(ref _ActiveItem, value);
                Document = value.Document;
                IsModified = value.IsModified;
            }
        }

        
        /// <summary>
        /// get or set the document of the texteditor
        /// </summary>
        public TextDocument Document
        {
            get => _Document;
            set
            {
                Set(ref _Document, value);
                ActiveItem.Document = value;
            }
        }


        /// <summary>
        /// Get or set the the current document text differs from the active item code
        /// </summary>
        public bool IsModified
        {
            get => _IsModified;
            set
            {
                Set(ref _IsModified, value);
                ActiveItem.IsModified = value;
            }
        }




        #endregion Properties


        #region Constructor


        public MCodeVM()
        {
            // Get active item
            MessengerInstance.Register<MActiveItemMessage>(this, x => ActiveItem = x.NewItem);
        }


        #endregion Constructor


    }
}
