using ICSharpCode.AvalonEdit.Document;
using MaxscriptManager.Model;
using SugzTools.Src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MaxscriptManager.Control
{
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(SgzTextEditorItem))]
    [TemplatePart(Name = "PART_ScrollViewer", Type = typeof(ScrollViewer))]
    public class SgzTextEditorsControl : TabControl
    {

        #region Fields


        ScrollViewer _ScrollViewer;


        #endregion Fields





        #region Dependency Properties


        /// <summary>
        /// 
        /// </summary>
        [Description(""), Category("Brush")]
        public Brush HeaderBackground
        {
            get => (Brush)GetValue(HeaderBackgroundProperty);
            set => SetValue(HeaderBackgroundProperty, value);
        }

        // DependencyProperty as the backing store for HeaderBackground
        public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.Register(
            "HeaderBackground",
            typeof(Brush),
            typeof(SgzTextEditorsControl),
            new PropertyMetadata(Resource<SolidColorBrush>.GetColor("MaxButtonMouseOver"))
        );


        /// <summary>
        /// 
        /// </summary>
        [Description(""), Category("Layout")]
        public double HeaderHeight
        {
            get => (double)GetValue(HeaderHeightProperty);
            set => SetValue(HeaderHeightProperty, value);
        }

        // DependencyProperty as the backing store for HeaderHeight
        public static readonly DependencyProperty HeaderHeightProperty = DependencyProperty.Register(
            "HeaderHeight",
            typeof(double),
            typeof(SgzTextEditorsControl),
            new PropertyMetadata(30d)
        ); 


        #endregion Dependency Properties



        #region Commands

        public static readonly DependencyProperty AddTabCommandProperty = DependencyProperty.Register(
            "AddTabCommand",
            typeof(ICommand),
            typeof(SgzTextEditorsControl));

        public ICommand AddTabCommand
        {
            get { return (ICommand)GetValue(AddTabCommandProperty); }
            set { SetValue(AddTabCommandProperty, value); }
        }



        public static readonly DependencyProperty CloseTabCommandProperty = DependencyProperty.Register(
            "CloseTabCommand",
            typeof(ICommand),
            typeof(SgzTextEditorsControl));

        public ICommand CloseTabCommand
        {
            get { return (ICommand)GetValue(CloseTabCommandProperty); }
            set { SetValue(CloseTabCommandProperty, value); }
        }


        #endregion Commands



        #region Constructor


        static SgzTextEditorsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SgzTextEditorsControl), new FrameworkPropertyMetadata(typeof(SgzTextEditorsControl)));
        }
        public SgzTextEditorsControl()
        {
            MouseDoubleClick += (s, e) => AddTab();
        }


        #endregion Constructor



        #region Overrides

        /// <summary>
        /// Get the ScrollViewer and handle the mouse wheel event
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild("PART_ScrollViewer") is ScrollViewer scv)
            {
                _ScrollViewer = scv;
                _ScrollViewer.PreviewMouseWheel += ScrollViewer_PreviewMouseWheel;
            }
        }

        /// <summary>
        /// Bring SelectedItem into view
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (ItemContainerGenerator.ContainerFromItem(SelectedItem) is SgzTextEditorItem item)
                item.BringIntoView();
        }

        /// <summary>
        /// Force the container to only accept SgzTextEditorItem
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride() => new SgzTextEditorItem();
        protected override bool IsItemItsOwnContainerOverride(object item) => item is SgzTextEditorItem;


        #endregion Overrides



        #region Methods


        internal void AddTab()
        {
            if (AddTabCommand != null)
                AddTabCommand.Execute(null);
        }

        internal void RemoveTab(SgzTextEditorItem tab)
        {
            if (CloseTabCommand != null)
                CloseTabCommand.Execute(tab.DataContext);
        }


        #endregion Methods



        #region Events Handlers


        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _ScrollViewer.ScrollToHorizontalOffset(_ScrollViewer.HorizontalOffset - e.Delta);
            e.Handled = true;
        }


        #endregion Events Handlers

    }
}
