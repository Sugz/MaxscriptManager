using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using MaxscriptManager.Model;
using MaxscriptManager.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace MaxscriptManager.Control
{
    public class SgzTextEditorItem : TabItem
    {
        private SgzTextEditor textEditor = new SgzTextEditor();
        private SgzTextEditorsControl ParentTabControl => ItemsControl.ItemsControlFromItemContainer(this) as SgzTextEditorsControl;


        public static RoutedUICommand CloseTabCommand => _CloseTabCommand;
        private static readonly RoutedUICommand _CloseTabCommand = new RoutedUICommand(
            "Close tab",
            "CloseTab",
            typeof(SgzTextEditorItem));



        /// <summary>
        /// 
        /// </summary>
        public bool IsModified
        {
            get => (bool)GetValue(IsModifiedProperty);
            set => SetValue(IsModifiedProperty, value);
        }

        // DependencyProperty as the backing store for IsModified
        public static readonly DependencyProperty IsModifiedProperty = DependencyProperty.Register(
            "IsModified",
            typeof(bool),
            typeof(SgzTextEditorItem),
            new PropertyMetadata(false)
        );






        static SgzTextEditorItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SgzTextEditorItem), new FrameworkPropertyMetadata(typeof(SgzTextEditorItem)));
            CommandManager.RegisterClassCommandBinding(typeof(SgzTextEditorItem), new CommandBinding(_CloseTabCommand, CloseTab));
        }
        public SgzTextEditorItem()
        {
            Content = textEditor;
            textEditor.Style = Application.Current.Resources["TextEditorStyle"] as Style;
        }



        private static void CloseTab(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is SgzTextEditorItem item)
                item.ParentTabControl.RemoveTab(item);
        }
    }
}
