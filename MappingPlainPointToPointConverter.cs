using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TransformsPrototype
{
    public class MappingPlainPointToPointConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vm = (MappingPlanePointViewModel) value;
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