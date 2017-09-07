using SugzTools.Src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaxscriptManager.Control
{
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(SgzTextEditorItem))]
    public class SgzTextEditorsControl : TabControl
    {

        /// <summary>
        /// 
        /// </summary>
        [Description(""), Category("Brush")]
        public Brush HeaderBackground
        {
            get => (Brush)GetValue(HeaderBackgroundProperty);
            set => SetValue(HeaderBackgroundProperty, value);
        }

        // DependencyProperty as the backing store for HeaderBackground
        public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.Register(
            "HeaderBackground",
            typeof(Brush),
            typeof(SgzTextEditorsControl),
            new PropertyMetadata(Resource<SolidColorBrush>.GetColor("MaxButtonMouseOver"))
        );


        /// <summary>
        /// 
        /// </summary>
        [Description(""), Category("Layout")]
        public double HeaderHeight
        {
            get => (double)GetValue(HeaderHeightProperty);
            set => SetValue(HeaderHeightProperty, value);
        }

        // DependencyProperty as the backing store for HeaderHeight
        public static readonly DependencyProperty HeaderHeightProperty = DependencyProperty.Register(
            "HeaderHeight",
            typeof(double),
            typeof(SgzTextEditorsControl),
            new PropertyMetadata(30d)
        );




        static SgzTextEditorsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SgzTextEditorsControl), new FrameworkPropertyMetadata(typeof(SgzTextEditorsControl)));
        }


        #region Overrides

        /// <summary>
        /// Force the container to only accept SgzTabItem
        /// </summary>
        protected override DependencyObject GetContainerForItemOverride() => new SgzTextEditorItem();
        protected override bool IsItemItsOwnContainerOverride(object item) => item is SgzTextEditorItem;


        #endregion Overrides

    }
}
