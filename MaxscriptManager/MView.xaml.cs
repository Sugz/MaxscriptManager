using System.Windows;
using MaxscriptManager.ViewModel;
using ICSharpCode.AvalonEdit.Folding;
using MaxscriptManager.Model;
using System.Windows.Threading;
using System;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Document;
using System.Diagnostics;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using SugzTools.Src;
using System.Windows.Input;
using System.IO;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit;
using System.Windows.Media;

namespace MaxscriptManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MView : Window
    {

        double _OldTvWidth = 300;


        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MView()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            showTvBtn.Click += (s, e) => SetLayout();
            SetLayout();
        }



        /// <summary>
        /// Show or hide the treeviews
        /// </summary>
        private void SetLayout()
        {
            if (TvsPanel.ActualWidth == 0)
            {
                gridSplitter.Visibility = Visibility.Visible;
                LayoutRoot.ColumnDefinitions[1].Width = new GridLength(_OldTvWidth);
            }
            else
            {
                gridSplitter.Visibility = Visibility.Collapsed;
                LayoutRoot.ColumnDefinitions[1].Width = new GridLength(0);
            }
                
        }


        /// <summary>
        /// Store the treeview ActualWidth
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Treeview_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (TvsPanel.ActualWidth > 0)
                _OldTvWidth = TvsPanel.ActualWidth;
        }



        private void TreeView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Helpers.FindAnchestor<TreeViewItem>(e.OriginalSource as DependencyObject) is TreeViewItem treeViewItem)
            {
                if (treeViewItem.DataContext is MCodeItem dataContext)
                {
                    if (e.ChangedButton == MouseButton.Middle)
                        (Application.Current.Resources["Locator"] as ViewModelLocator).Data.CreateFile();
                    //dataContext.IsSelected = true;
                    dataContext.IsActive = true;
                }
                else if (treeViewItem.DataContext is MFolder)
                    treeViewItem.IsExpanded = !treeViewItem.IsExpanded;
            }
        }


    }
    
}