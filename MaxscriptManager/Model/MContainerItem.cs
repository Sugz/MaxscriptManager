using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxscriptManager.Model
{
    public class MContainerItem : MDataItem
    {
        public override MDataType DataType => MDataType.Container;

        protected override ObservableCollection<MDataItem> GetChildren() => new ObservableCollection<MDataItem>();
    }
}
