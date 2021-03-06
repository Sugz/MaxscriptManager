﻿using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Rendering;
using SugzTools.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace MaxscriptManager.Src
{
    public sealed class AvalonEditBehaviour : Behavior<TextEditor>
    {

        private FoldingManager foldingManager;
        private UndoStack _UndoStack;

        #region Dependency Properties


        public int CaretOffset
        {
            get => (int)GetValue(CaretOffsetProperty);
            set => SetValue(CaretOffsetProperty, value);
        }
        public static readonly DependencyProperty CaretOffsetProperty = DependencyProperty.Register(
            "CaretOffset",
            typeof(int),
            typeof(AvalonEditBehaviour),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, CaretOffsetCallback)
        );


        public Vector ScrollOffset
        {
            get => (Vector)GetValue(ScrollOffsetProperty);
            set => SetValue(ScrollOffsetProperty, value);
        }
        // DependencyProperty as the backing store for ScrollOffset
        public static readonly DependencyProperty ScrollOffsetProperty = DependencyProperty.Register(
            "ScrollOffset",
            typeof(Vector),
            typeof(AvalonEditBehaviour),
            new FrameworkPropertyMetadata(default(Vector), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ScrollOffsetCallback)
        );


        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<FoldingSection> FoldingSections
        {
            get => (IEnumerable<FoldingSection>)GetValue(FoldingSectionsProperty);
            set => SetValue(FoldingSectionsProperty, value);
        }

        // DependencyProperty as the backing store for FoldingSection
        public static readonly DependencyProperty FoldingSectionsProperty = DependencyProperty.Register(
            "FoldingSections",
            typeof(IEnumerable<FoldingSection>),
            typeof(AvalonEditBehaviour),
            new FrameworkPropertyMetadata(default(IEnumerable<FoldingSection>), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, FoldingSectionsCallback)
        );






        public string DocumentText
        {
            get => (string)GetValue(DocumentTextProperty);
            set => SetValue(DocumentTextProperty, value);
        }
        public static readonly DependencyProperty DocumentTextProperty = DependencyProperty.Register(
            "DocumentText",
            typeof(string),
            typeof(AvalonEditBehaviour),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TextChangedCallback)
        ); 


        #endregion Dependency Properties


        #region Attach / Detach


        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
            {
                AssociatedObject.TextArea.Caret.PositionChanged += OnCaretPositionChanged;
                AssociatedObject.TextArea.TextView.ScrollOffsetChanged += OnScrollOffsetChanged;
                AssociatedObject.TextChanged += OnTextChanged;
            }

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
            {
                AssociatedObject.TextArea.Caret.PositionChanged -= OnCaretPositionChanged;
                AssociatedObject.TextArea.TextView.ScrollOffsetChanged -= OnScrollOffsetChanged;
                AssociatedObject.TextChanged -= OnTextChanged;
            }

        } 


        #endregion Attach / Detach


        #region Event Handlers


        private void OnCaretPositionChanged(object sender, EventArgs eventArgs)
        {
            if (sender is Caret caret)
            {
                CaretOffset = caret.Offset;
            }
        }

        private void OnScrollOffsetChanged(object sender, EventArgs e)
        {
            if (sender is TextView textView)
            {
                ScrollOffset = textView.ScrollOffset;
            }
        }

        private void OnTextChanged(object sender, EventArgs eventArgs)
        {
            if (sender is TextEditor textEditor)
            {
                if (textEditor.Document != null)
                {
                    DocumentText = textEditor.Document.Text;
                }
            }
        } 


        #endregion Event Handlers


        #region Callbacks


        private static void CaretOffsetCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            AvalonEditBehaviour behavior = s as AvalonEditBehaviour;
            if (behavior.AssociatedObject != null)
            {
                TextEditor editor = behavior.AssociatedObject as TextEditor;
                editor.CaretOffset = (int)e.NewValue;
            }
        }


        private static void ScrollOffsetCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            AvalonEditBehaviour behavior = s as AvalonEditBehaviour;
            if (behavior.AssociatedObject != null)
            {
                TextEditor editor = behavior.AssociatedObject as TextEditor;
                Vector offset = (Vector)e.NewValue;
                editor.ScrollToVerticalOffset(offset.Y);
            }
        }


        private static void FoldingSectionsCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var behavior = s as AvalonEditBehaviour;
            if (behavior.AssociatedObject != null)
            {
                var editor = behavior.AssociatedObject as TextEditor;
                
                behavior.FoldingSections = behavior.foldingManager.AllFoldings;
                if (editor.Document != null && behavior.foldingManager != null)
                {
                    //behavior.foldingManager = (editor.TextArea.LeftMargins[1] as FoldingMargin).FoldingManager;
                    List<NewFolding> foldingSections = new List<NewFolding>();
                    foreach(FoldingSection section in e.NewValue as IEnumerable<FoldingSection>)
                    {
                        NewFolding folding = new NewFolding(section.StartOffset, section.EndOffset);
                        folding.DefaultClosed = section.IsFolded;
                        foldingSections.Add(folding);
                    }
                    behavior.foldingManager.UpdateFoldings(foldingSections, -1);
                }
            }
        }

        private static void TextChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var behavior = s as AvalonEditBehaviour;
            if (behavior.AssociatedObject != null)
            {
                var editor = behavior.AssociatedObject as TextEditor;
                if (editor.Document != null)
                {
                    behavior.foldingManager = (editor.TextArea.LeftMargins[1] as FoldingMargin).FoldingManager;
                    if (behavior.foldingManager != null)
                        behavior.FoldingSections = behavior.foldingManager.AllFoldings;

                    int caretOffset = editor.CaretOffset;
                    editor.Document.Text = e.NewValue.ToString();
                    editor.CaretOffset = editor.Document.Text.Length >= caretOffset ? caretOffset : 0;
                    
                }
            }
        } 


        #endregion Callbacks
    }
}
