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

namespace MaxscriptManager.ViewModel
{
    public class MMDataVM : ViewModelBase
    {

        #region Fields


        private Browser _Browser = new Browser();

        private MMDataItem _SelectedItem;

        private RelayCommand _AddFolderCommand;


        #endregion Fields




        #region Properties


        /// <summary>
        /// The collection of CDFolders that will be displayed in the treeview
        /// </summary>
        public ObservableCollection<IMMPathItem> Datas { get; set; } = new ObservableCollection<IMMPathItem>();

        /// <summary>
        /// 
        /// </summary>
        public MMDataItem SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                //SetStatusPanel();
            }
        }



        /// <summary>
        /// Get the command to add a folder the the data list
        /// </summary>
        public RelayCommand AddFolderCommand
        {
            get => _AddFolderCommand ?? (_AddFolderCommand = new RelayCommand(AddFolder));
        } 


        #endregion Properties



        /// <summary>
        /// Let the user choose a folder to add to the folders list if it doesn't exist already
        /// </summary>
        private void AddFolder()
        {
            if (_Browser.GetFolder() is string selectedFolder && !Datas.Any(x => x.Path.Equals(selectedFolder)))
                Datas.Add(new MMFolder(selectedFolder));
        }
    }
}
