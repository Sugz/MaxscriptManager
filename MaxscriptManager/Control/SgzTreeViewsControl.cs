using MaxscriptManager.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MaxscriptManager.Control
{
    public class SgzTreeViewsControl : ItemsControl
    {
        protected override DependencyObject GetContainerForItemOverride() => new TreeView();

        protected override bool IsItemItsOwnContainerOverride(object item) => item is TreeView;


        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {

            foreach(var item in newValue)
            {
                if (oldValue.Cast<ObservableCollection<MDataItem>>().Contains(item))
                    return;

                TreeView tv = new TreeView();
                tv.ItemsSource = new ObservableCollection<MDataItem>() { item as MDataItem };
                AddChild(tv);
            }
            base.OnItemsSourceChanged(oldValue, newValue);
        }
    }
}
