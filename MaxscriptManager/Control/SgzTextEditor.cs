using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using MaxscriptManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MaxscriptManager.Control
{
    public class SgzTextEditor : TabItem
    {
        TextEditor textEditor;
        FoldingManager foldingManager;
        MaxscriptFoldingStrategy foldingStrategy = new MaxscriptFoldingStrategy();


        /// <summary>
        /// 
        /// </summary>
        public string DocumentText
        {
            get => (string)GetValue(DocumentTextProperty);
            set => SetValue(DocumentTextProperty, value);
        }

        // DependencyProperty as the backing store for DocumentText
        public static readonly DependencyProperty DocumentTextProperty = DependencyProperty.Register(
            "DocumentText",
            typeof(string),
            typeof(SgzTextEditor),
            new PropertyMetadata(default(string))
        );


        static SgzTextEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SgzTextEditor), new FrameworkPropertyMetadata(typeof(SgzTextEditor)));
        }
        public SgzTextEditor()
        {
            textEditor = Content as TextEditor;
            textEditor.MouseMove += TextEditor_MouseMove;
            foldingManager = new FoldingManager(new TextDocument());
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
