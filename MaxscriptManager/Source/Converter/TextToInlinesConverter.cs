﻿using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaxscriptManager.Src
{
    public class TextToInlinesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values[0] is TextBlock textBlock && values[1] is string text)
            {
                //textBlock.Text = null;
                //textBlock.Inlines.AddRange(CDParser.FormatFunctionName(text));
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
