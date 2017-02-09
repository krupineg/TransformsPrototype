using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TransformsPrototype
{
    public class TranslateOffsetConverter : IMultiValueConverter
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

    public class MappingPlainPointToPointConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vm = (MappingPlainConfigurationPointViewModel) value;
            if (vm == null)
            {
                return default(Point);
            }
            else
            {
                return new Point(vm.X, vm.Y);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}