using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TransformsPrototype
{
    public class ShapeCenterToOriginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue)
            {
                return 0;
            }
            var x = (double)values[0];
            var size = (double)values[1];
            return x - size / 2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
}