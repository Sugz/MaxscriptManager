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
    public class MvvmTextEditor : TextEditor//, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion INotifyPropertyChanged


        /// <summary>
        /// A bindable Text property
        /// </summary>
        public new string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// The bindable text property dependency property
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", 
            typeof(string), 
            typeof(MvvmTextEditor),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PropertyChangedCallback)
        );


        protected override void OnTextChanged(EventArgs e)
        {
            Text = Document.Text;
            base.OnTextChanged(e);
        }


        private static void PropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            MvvmTextEditor editor = s as MvvmTextEditor;
            if (editor.Document != null)
            {
                int caretOffset = editor.CaretOffset;
                editor.Document.Text = e.NewValue.ToString();
                editor.CaretOffset = caretOffset;
            }
        }

    }
}
