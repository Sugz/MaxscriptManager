using GalaSoft.MvvmLight;
using MaxscriptManager.Src;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MaxscriptManager.Model
{
    public enum MDataType
    {
        Container,
        Folder,
        Script,
        Struct,
        Rollout,
        Function,
    }


    public interface IMPathItem
    {
        /// <summary>
        /// Get if the given path exist
        /// </summary>
        bool IsValidPath { get; }

        /// <summary>
        /// The path of the folder or the file
        /// </summary>
        string Path { get; }

        /// <summary>
        /// The relative path either from 3ds max install or enu folders
        /// </summary>
        string RelativePath { get; }
    }


    public abstract class MDataItem : ViewModelBase
    {

        #region Fields

        protected string _Text;
        protected ObservableCollection<MDataItem> _Children;
        protected bool _IsSelected = false;
        protected bool _IsExpanded;


        #endregion Fields


        #region Properties

        /// <summary>
        /// Get the type of DataItem
        /// </summary>
        public virtual MDataType DataType { get; set; }

        /// <summary>
        /// The treeviewitem parent node
        /// </summary>
        public virtual object Parent { get; protected set; }

        /// <summary>
        /// Get the text to display in the treeview
        /// </summary>
        public virtual string Text
        {
            get => _Text;
            set => Set(ref _Text, value);
        }

        /// <summary>
        /// Get the treeviewitem children
        /// </summary>
        public virtual ObservableCollection<MDataItem> Children
        {
            get => _Children ?? (_Children = GetChildren());
            set => Set(ref _Children, value);
        }

        /// <summary>
        /// Get or set the treeviewitem selected state 
        /// </summary>
        public virtual bool IsSelected
        {
            get => _IsSelected;
            set
            {
                Set(ref _IsSelected, value);
                if (value)
                    MessengerInstance.Send(new MSelectedItemMessage(this));
            }
        }

        /// <summary>
        /// Get or set the treeviewitem expanded state 
        /// </summary>
        public virtual bool IsExpanded
        {
            get => _IsExpanded;
            set => Set(ref _IsExpanded, value);
        }


        #endregion Properties



        protected abstract ObservableCollection<MDataItem> GetChildren();



    }
}
