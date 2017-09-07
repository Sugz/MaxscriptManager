using SugzTools.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxscriptManager.Model
{
    public class MFolder : MDataItem, IMPathItem
    {

        private class DummyChild : MDataItem
        {
            protected override ObservableCollection<MDataItem> GetChildren() => null;
        }

        #region Fields


        //private string _Text;
        private bool _IsValidPath;
        private string _Path;


        #endregion Fields


        #region Properties


        public override MDataType DataType => MDataType.Folder;
        public override object Parent => null;
        public override string Text
        {
            get => _Text ?? (_Text = System.IO.Path.GetFileName(Path));
            set => Set(ref _Text, value);
        }
        public bool IsValidPath
        {
            get => _IsValidPath;
            set => Set(ref _IsValidPath, value);
        }
        public string Path
        {
            get => _Path;
            set
            {
                Set(ref _Path, value);
                IsValidPath = Directory.Exists(value);
                if (IsValidPath && Children is null)
                    Children = GetChildren();
            }
        }
        public string RelativePath => throw new NotImplementedException();
        //public override bool IsSelected
        //{
        //    get => _IsSelected;
        //    set => Set(ref _IsSelected, value);
        //}
        public override bool IsExpanded
        {
            get => _IsExpanded;
            set
            {
                Set(ref _IsExpanded, value);
                if (value && _Children.Count == 1 && _Children[0] is DummyChild)
                {
                    Children.Clear();
                    foreach (string dir in Directory.GetDirectories(Path))
                        Children.Add(new MFolder(dir));
                    foreach (string file in Directory.GetFiles(Path))
                        Children.Add(new MScript(this, file));
                }
            }
        }
        public override ObservableCollection<MDataItem> Children
        {
            get => _Children;
            set => _Children = value;
        }

        #endregion Properties


        public MFolder(string path)
        {
            Path = path;
            if (HasSubItem())
                Children = new ObservableCollection<MDataItem>() { new DummyChild() };
        }


        private bool HasSubItem()
        {
            DirectoryInfo directory = new DirectoryInfo(Path);
            if (directory.GetDirectories().Length != 0 || directory.GetFiles().Length != 0)
                return true;
            return false;
        }


        protected override ObservableCollection<MDataItem> GetChildren() => null;
    }



}
 