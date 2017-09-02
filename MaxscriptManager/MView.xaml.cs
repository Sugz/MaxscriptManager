﻿using System.Windows;
using MaxscriptManager.ViewModel;
using ICSharpCode.AvalonEdit.Folding;
using MaxscriptManager.Model;
using System.Windows.Threading;
using System;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Document;

namespace MaxscriptManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MView : Window
    {
        FoldingManager foldingManager;
        MaxscriptFoldingStrategy foldingStrategy = new MaxscriptFoldingStrategy();
        double _OldTvWidth = 300;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MView()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();

            TextDocument document = (Application.Current.Resources["Locator"] as ViewModelLocator).Description.Document;
            foldingManager = new FoldingManager(document);
            foldingManager = FoldingManager.Install(textEditor.TextArea);
            DispatcherTimer foldingUpdateTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(2) };
            foldingUpdateTimer.Tick += (s, e) => foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
            foldingUpdateTimer.Start();
        }

        private void ShowTvBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowTvBtn.IsChecked == true)
                LayoutRoot.ColumnDefinitions[1].Width = new GridLength(_OldTvWidth);
            else
                LayoutRoot.ColumnDefinitions[1].Width = new GridLength(0);
        }

        private void Treeview_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ShowTvBtn.IsChecked == true)
                _OldTvWidth = Treeview.ActualWidth;
        }
    }
}