﻿using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaxscriptManager.Control
{
    public class MvvmTextEditor : TextEditor
    {

        /// <summary>
        /// 
        /// </summary>
        public TextDocument TextDocument
        {
            get => (TextDocument)GetValue(TextDocumentProperty);
            set => SetValue(TextDocumentProperty, value);
        }

        // DependencyProperty as the backing store for TextDocument
        public static readonly DependencyProperty TextDocumentProperty = DependencyProperty.Register(
            "TextDocument",
            typeof(TextDocument),
            typeof(MvvmTextEditor),
            new FrameworkPropertyMetadata(default(TextDocument), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TextDocumentCallback)
        );



        //public string DocumentText
        //{
        //    get => (string)GetValue(DocumentTextProperty);
        //    set => SetValue(DocumentTextProperty, value);
        //}
        //public static readonly DependencyProperty DocumentTextProperty = DependencyProperty.Register(
        //    "DocumentText",
        //    typeof(string),
        //    typeof(MvvmTextEditor),
        //    new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TextChangedCallback)
        //);


        public MvvmTextEditor()
        {
            TextDocument = new TextDocument();
            //DocumentChanged += MvvmTextEditor_DocumentChanged;
            //TextChanged += MvvmTextEditor_TextChanged;
        }

        protected override void OnDocumentChanged(EventArgs e)
        {
            TextDocument = Document;
            base.OnDocumentChanged(e);
            
        }

        private void MvvmTextEditor_DocumentChanged(object sender, EventArgs e)
        {
            if (sender is MvvmTextEditor textEditor && textEditor.Document != null)
            {
                TextDocument = textEditor.Document;
            }
        }

        //private void MvvmTextEditor_TextChanged(object sender, EventArgs e)
        //{
        //    if (sender is TextEditor textEditor && textEditor.Document != null)
        //    {
        //        DocumentText = textEditor.Document.Text;
        //    }
        //}



        private static void TextDocumentCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            if (s is MvvmTextEditor editor)
            {
                TextDocument value = e.NewValue as TextDocument;
                editor.Document = value;
            }
        }

        //private static void TextChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        //{
        //    if (s is TextEditor editor && editor.Document != null)
        //    {
        //        int caretOffset = editor.CaretOffset;
        //        editor.Document.Text = e.NewValue.ToString();
        //        editor.CaretOffset = editor.Document.Text.Length >= caretOffset ? caretOffset : 0;

        //    }
        //}
    }
}
