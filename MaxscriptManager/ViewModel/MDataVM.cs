using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaxscriptManager.Model;
using SugzTools.Src;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaxscriptManager.ViewModel
{
    public class MDataVM : ViewModelBase
    {

        #region Fields


        private Browser _Browser = new Browser();

        private MDataItem _SelectedItem;


        private Visibility _DataPathFieldVisibility = Visibility.Collapsed;

        private RelayCommand _AddFolderCommand;
        private RelayCommand _AddFileCommand;

        #endregion Fields



        #region Properties


        /// <summary>
        /// Get the collection of DataItem that will be displayed in the treeview
        /// </summary>
        public ObservableCollection<IMMPathItem> Datas { get; set; } = new ObservableCollection<IMMPathItem>();


        /// <summary>
        /// Get the collection of open files
        /// </summary>
        public ObservableCollection<MCodeItem> OpenFiles { get; set; } = new ObservableCollection<MCodeItem>();


        /// <summary>
        /// Get the current treeview selected item
        /// </summary>
        public MDataItem SelectedItem
        {
            get => _SelectedItem;
            private set
            {
                _SelectedItem = value;
                //SetStatusPanel();
            }
        }


        /// <summary>
        /// Get the data path field visibility
        /// </summary>
        public Visibility DataPathFieldVisibility
        {
            get => _DataPathFieldVisibility;
            private set => Set(ref _DataPathFieldVisibility, value);
        }



        /// <summary>
        /// Get the command to add a folder the the data list
        /// </summary>
        public RelayCommand AddFolderCommand => _AddFolderCommand ?? (_AddFolderCommand = new RelayCommand(AddFolder));

        /// <summary>
        /// Get the command to add a file the the data list
        /// </summary>
        public RelayCommand AddFileCommand => _AddFileCommand ?? (_AddFileCommand = new RelayCommand(AddFile));


        #endregion Properties



        #region Methods


        /// <summary>
        /// Let the user choose a folder to add to the folders list if it doesn't exist already
        /// </summary>
        private void AddFolder()
        {
            if (_Browser.GetFolder() is string selectedFolder && !Datas.Any(x => x.Path.Equals(selectedFolder)))
                Datas.Add(new MFolder(selectedFolder) { IsSelected = true });
        }

        /// <summary>
        /// Let the user choose a file to add to the folders list if it doesn't exist already
        /// </summary>
        private void AddFile()
        {
            if (_Browser.GetFile() is string selectedFile && !Datas.Any(x => x.Path.Equals(selectedFile)))
                Datas.Add(new MScript(null, selectedFile) { IsSelected = true });
        }
    } 


    #endregion Methods
}
