using ICSharpCode.AvalonEdit;
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

        



        public string DocumentText
        {
            get => (string)GetValue(DocumentTextProperty);
            set => SetValue(DocumentTextProperty, value);
        }
        public static readonly DependencyProperty DocumentTextProperty = DependencyProperty.Register(
            "DocumentText",
            typeof(string),
            typeof(MvvmTextEditor),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TextChangedCallback)
        );


        public MvvmTextEditor()
        {
            TextChanged += MvvmTextEditor_TextChanged;
        }


        private void MvvmTextEditor_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextEditor textEditor && textEditor.Document != null)
            {
                DocumentText = textEditor.Document.Text;
            }
        }




        private static void TextChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            if (s is TextEditor editor && editor.Document != null)
            {
                int caretOffset = editor.CaretOffset;
                editor.Document.Text = e.NewValue.ToString();
                editor.CaretOffset = editor.Document.Text.Length >= caretOffset ? caretOffset : 0;

            }
        }
    }
}
