using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace TransformsPrototype
{
    public class MappingPlainFieldViewModel : ViewModelBase
    {
        bool captured = false;
        double mouseOffsetX, currentMouseX, mouseOffsetY, currentMouseY;
        private MappingPlainConfigurationPointViewModel _selectedPoint;

        public MappingPlainFieldViewModel(ILogger logger) : base(logger)
        {
            LeftTop = new MappingPlainConfigurationPointViewModel(logger);
            LeftTop.X = 100;
            LeftTop.Y = 100;
            RightTop = new MappingPlainConfigurationPointViewModel(logger);
            RightTop.X = 300;
            RightTop.Y = 100;
            RightDown = new MappingPlainConfigurationPointViewModel(logger);
            RightDown.X = 300;
            RightDown.Y = 300;
            LeftDown = new MappingPlainConfigurationPointViewModel(logger);
            LeftDown.X = 100;
            LeftDown.Y = 300;
        }

        public MappingPlainConfigurationPointViewModel LeftTop { get; set; }
        public MappingPlainConfigurationPointViewModel RightTop { get; set; }
        public MappingPlainConfigurationPointViewModel RightDown { get; set; }
        public MappingPlainConfigurationPointViewModel LeftDown { get; set; }

        public MappingPlainConfigurationPointViewModel SelectedPoint
        {
            get { return _selectedPoint; }
            set { SetProperty(ref _selectedPoint, value); }
        }

        public ICommand MouseDownCommand
        {
            get
            {
                return new DelegateCommand(MouseDownExecute, o => true);
            }
        }

        public ICommand MouseMoveCommand
        {
            get
            {
                return new DelegateCommand(MouseMoveExecute, o => captured);
            }
        }

        public ICommand MouseUpCommand
        {
            get
            {
                return new DelegateCommand(MouseUpExecute, o => captured);
            }
        }
        private void MouseMoveExecute(object obj)
        {
            if (captured)
            {
                var point = (Point)obj;
                Debug.WriteLine("");
                Debug.WriteLine("currentMouseX = " + currentMouseX);
                Debug.WriteLine("currentMouseY = " + currentMouseY);
                Debug.WriteLine("newMosueX = " + point.X);
                Debug.WriteLine("newMosueY = " + point.Y);

                mouseOffsetX = point.X - currentMouseX;
                mouseOffsetY = point.Y - currentMouseY;
                currentMouseX = point.X;
                currentMouseY = point.Y;

                Debug.WriteLine("After transform");
                Debug.WriteLine("currentMouseX = " + currentMouseX);
                Debug.WriteLine("currentMouseY = " + currentMouseY);
                SelectedPoint.X += mouseOffsetX;
                SelectedPoint.Y += mouseOffsetY;
            }
           
        }


        private void MouseDownExecute(object obj)
        {
            var point = (Point?)obj;
            if (point.HasValue)
            {
                captured = true;
                currentMouseX = point.Value.X;
                currentMouseY = point.Value.Y;
            }
        }

        private void MouseUpExecute(object obj)
        {
            captured = false;
        }
    }

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
}