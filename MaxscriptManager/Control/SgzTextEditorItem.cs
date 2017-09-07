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
using System.Windows.Interactivity;

namespace MaxscriptManager.Control
{
    public class SgzTextEditorItem : TabItem
    {
        MvvmTextEditor textEditor = new MvvmTextEditor();
        FoldingManager foldingManager;
        MaxscriptFoldingStrategy foldingStrategy = new MaxscriptFoldingStrategy();
        TextDocument textDocument = new TextDocument();


        static SgzTextEditorItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SgzTextEditorItem), new FrameworkPropertyMetadata(typeof(SgzTextEditorItem)));
        }
        public SgzTextEditorItem()
        {
            Content = textEditor;
            textEditor.Style = Application.Current.Resources["TextEditorStyle"] as Style;
            textEditor.MouseMove += TextEditor_MouseMove;
            foldingManager = new FoldingManager(textDocument);
            foldingManager = FoldingManager.Install(textEditor.TextArea);
        }


        /// <summary>
        /// Show the block foldings when the mouse enter the area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextEditor_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.OriginalSource is FoldingMargin foldingMargin)
                foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
        }

    }
}
