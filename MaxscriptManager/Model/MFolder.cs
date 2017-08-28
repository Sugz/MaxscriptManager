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
    public class MFolder : MDataItem, IMMPathItem
    {

        #region Fields


        //private string _Text;
        private bool _IsValidPath;
        private string _Path;


        #endregion Fields


        #region Properties


        public override MMDataType DataType => MMDataType.Folder;
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
        //public override ObservableCollection<MMDataItem> Children
        //{
        //    get => base.Children ?? (base.Children = GetChildren());
        //    set => base.Children = value;
        //}


        #endregion Properties


        public MFolder(string path, ObservableCollection<MDataItem> children = null)
        {
            Path = path;
            if (children != null)
                Children = children;
        }




        protected override ObservableCollection<MDataItem> GetChildren()
        {
            if (!IsValidPath)
                return null;

            ObservableCollection<MDataItem> children = new ObservableCollection<MDataItem>();
            //Directory.GetFiles(Path).ForEach(x => children.Add(new MMScript(this, x)));
            foreach(string file in Directory.GetFiles(Path))
                children.Add(new MScript(this, file));
            return children;

        }
    }
}
