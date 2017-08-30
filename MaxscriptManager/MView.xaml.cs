using System.Windows;
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
            DispatcherTimer foldingUpdateTimer = new DispatcherTimer();
            foldingUpdateTimer.Interval = TimeSpan.FromSeconds(2);
            foldingUpdateTimer.Tick += delegate { UpdateFoldings(); };
            foldingUpdateTimer.Start();
        }

        private void UpdateFoldings()
        {
            foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
        }
    }
}