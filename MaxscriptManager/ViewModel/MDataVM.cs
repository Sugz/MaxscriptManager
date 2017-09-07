using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaxscriptManager.Model;
using MaxscriptManager.Src;
using SugzTools;
using SugzTools.Extensions;
using SugzTools.Src;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        private MCodeItem _LastCodeItem;
        private MDataItem _SelectedItem;
        

        private Visibility _DataPathFieldVisibility = Visibility.Collapsed;


        private RelayCommand _AddFolderCommand;
        private RelayCommand _AddFileCommand;
        private RelayCommand _CreateFileCommand;
        private RelayCommand<MCodeItem> _RemoveOpenFile;

        #endregion Fields



        #region Properties


        /// <summary>
        /// Get or set the currently open files
        /// </summary>
        public ObservableCollection<MCodeItem> CurrentFiles { get; set; } = new ObservableCollection<MCodeItem>();

        /// <summary>
        /// Get all 3ds max folders
        /// </summary>
        public ObservableCollection<MFolder> MaxFolders { get; set; } = new ObservableCollection<MFolder>();

        /// <summary>
        /// Get the collection of DataItem that will be displayed in the treeview
        /// </summary>
        public ObservableCollection<MFolder> Folders { get; set; } = new ObservableCollection<MFolder>();





        /// <summary>
        /// Get the current treeview selected item
        /// </summary>
        public MDataItem SelectedItem
        {
            get => _SelectedItem;
            private set
            {
                if (_SelectedItem is MCodeItem oldItem)
                    _LastCodeItem = oldItem;

                _SelectedItem = value;

                if (value is MCodeItem item && !CurrentFiles.Contains(item))
                {
                    if (CurrentFiles.Count == 0)
                        CurrentFiles.Add(item);
                    else
                    {
                        int curIndex = CurrentFiles.IndexOf(_LastCodeItem);
                        CurrentFiles.RemoveAt(curIndex);
                        CurrentFiles.ForEach(x => x.IsActive = false);
                        CurrentFiles.Insert(curIndex, item);
                    }
                }

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


        /// <summary>
        /// Get the command to add a file the the data list
        /// </summary>
        public RelayCommand CreateFileCommand => _CreateFileCommand ?? (_CreateFileCommand = new RelayCommand(CreateFile));

        


        /// <summary>
        /// Get the command to remove an item in the OpenFiles list
        /// </summary>
        public RelayCommand<MCodeItem> RemoveOpenFileCommand => _RemoveOpenFile ?? (_RemoveOpenFile = new RelayCommand<MCodeItem>(RemoveFile));


        #endregion Properties


        public MDataVM()
        {
            // Get selected treeview item
            MessengerInstance.Register<MSelectedItemMessage>(this, x => SelectedItem = x.NewItem);

            SugzTools.Src.MaxFolders.Get().ForEach(x =>
            {
                MaxFolders.Add(new MFolder(x.Key) { Text = $"3ds Max {x.Value.Substring(x.Value.Length - 4)} ENU" });
                MaxFolders.Add(new MFolder(x.Value));
            });
        }



        #region Methods


        /// <summary>
        /// Let the user choose a folder to add to the folders list if it doesn't exist already
        /// </summary>
        private void AddFolder()
        {
            if (_Browser.GetFolder() is string selectedFolder && !Folders.Any(x => x.Path.Equals(selectedFolder)))
                Folders.Add(new MFolder(selectedFolder));
        }

        /// <summary>
        /// Let the user choose a file to add to the folders list if it doesn't exist already
        /// </summary>
        private void AddFile()
        {
            if (_Browser.GetFile() is string selectedFile)
            {
                bool isInCollection = false;
                for (int i = 0; i < CurrentFiles.Count; i++)
                {
                    if (CurrentFiles[i] is MScript script && script.Path.Equals(selectedFile))
                    {
                        isInCollection = true;
                        CurrentFiles[i].IsSelected = true;
                        break;
                    }
                }
                if (!isInCollection)
                {
                    CurrentFiles.All(x => x.IsSelected = false);
                    CurrentFiles.Add(new MScript(null, selectedFile) { IsSelected = true });
                }

            }
        }


        public void CreateFile()
        {
            CurrentFiles.Add(new MScript(null, null, "Untitled.ms") { Code = string.Empty });
            CurrentFiles.Last().IsActive = true;
        }

        /// <summary>
        /// Remove a file from the OpenFiles collection
        /// </summary>
        /// <param name="item"></param>
        private void RemoveFile(MCodeItem item)
        {
            int index = CurrentFiles.IndexOf(item);
            item.IsSelected = false;
            CurrentFiles.Remove(item);
            if (CurrentFiles.Count >= index + 1)
                CurrentFiles[index].IsSelected = true;
            else if (CurrentFiles.Count != 0)
                CurrentFiles[index - 1].IsSelected = true;
            else
                SelectedItem = null;
        }
    }


    #endregion Methods
}
