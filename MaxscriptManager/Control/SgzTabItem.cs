using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace MaxscriptManager.Control
{
    public class SgzTabItem : ContentControl
    {

        internal SgzTabControl ParentSelector => ItemsControl.ItemsControlFromItemContainer(this) as SgzTabControl;


        /// <summary>
        ///     Indicates whether this TabItem is selected.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = Selector.IsSelectedProperty.AddOwner(
            typeof(SgzTabItem),
            new FrameworkPropertyMetadata(false, 
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | 
            FrameworkPropertyMetadataOptions.AffectsParentMeasure | 
            FrameworkPropertyMetadataOptions.Journal,
            OnIsSelectedChanged
        ));

        /// <summary>
        /// Indicates whether this TabItem is selected.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        private static readonly RoutedUICommand closeTabCommand = new RoutedUICommand(
            "Close tab", 
            "CloseTab", 
            typeof(SgzTabItem));

        public static RoutedUICommand CloseTabCommand => closeTabCommand;



        /// <summary>
        /// 
        /// </summary>
        public bool HasChanged
        {
            get { return (bool)GetValue(HasChangedProperty); }
            set { SetValue(HasChangedProperty, value); }
        }

        // DependencyProperty as the backing store for HasChanged
        public static readonly DependencyProperty HasChangedProperty = DependencyProperty.Register(
            "HasChanged",
            typeof(bool),
            typeof(SgzTabItem),
            new PropertyMetadata(false)
        );




        static SgzTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SgzTabItem), new FrameworkPropertyMetadata(typeof(SgzTabItem)));
            CommandManager.RegisterClassCommandBinding(typeof(SgzTabItem), new CommandBinding(closeTabCommand, HandleCloseTabCommand));
        }
        public SgzTabItem()
        {
            MouseUp += (s, e) => IsSelected = true;
        }



        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SgzTabItem tabItem = d as SgzTabItem;
            if ((bool)e.NewValue)
            {
                tabItem.RaiseEvent(new RoutedEventArgs(Selector.SelectedEvent, tabItem));
            }
            else
            {
                tabItem.RaiseEvent(new RoutedEventArgs(Selector.UnselectedEvent, tabItem));
            }
        }
        


        private static void HandleCloseTabCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is SgzTabItem item)
                item.ParentSelector.RemoveTab(item);
        }

    }
}