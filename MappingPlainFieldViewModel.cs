using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace TransformsPrototype
{
    public class MappingPlainFieldViewModel : ViewModelBase
    {
        bool captured = false;
        double mouseOffsetX, currentMouseX, mouseOffsetY, currentMouseY;
        private MappingPlainConfigurationPointViewModel _selectedPoint;
        private MappingPlainConfigurationPointViewModel _leftTop;
        private MappingPlainConfigurationPointViewModel _rightTop;
        private MappingPlainConfigurationPointViewModel _rightDown;
        private MappingPlainConfigurationPointViewModel _leftDown;

        public MappingPlainFieldViewModel(ILogger logger) : base(logger)
        {
            LeftTop = new MappingPlainConfigurationPointViewModel(logger);
            LeftDown = new MappingPlainConfigurationPointViewModel(logger);
            RightDown = new MappingPlainConfigurationPointViewModel(logger);
            RightTop = new MappingPlainConfigurationPointViewModel(logger);
            LeftTop.Next = RightTop;
            LeftTop.Previous = LeftDown;
            RightTop.Next = RightDown;
            RightTop.Previous = LeftTop;
            RightDown.Next = LeftDown;
            RightDown.Previous = RightTop;
            LeftDown.Next = LeftTop;
            LeftDown.Previous = RightDown;
            LeftTop.X = 100;
            LeftTop.Y = 100;
            RightTop.X = 300;
            RightTop.Y = 100;
            RightDown.X = 300;
            RightDown.Y = 300;
            LeftDown.X = 100;
            LeftDown.Y = 300;
            
        }

        public MappingPlainConfigurationPointViewModel LeftTop
        {
            get { return _leftTop; }
            set { SetProperty(ref _leftTop, value); }
        }

        public MappingPlainConfigurationPointViewModel RightTop
        {
            get { return _rightTop; }
            set { SetProperty(ref _rightTop, value); }
        }

        public MappingPlainConfigurationPointViewModel RightDown
        {
            get { return _rightDown; }
            set { SetProperty(ref _rightDown, value); }
        }

        public MappingPlainConfigurationPointViewModel LeftDown
        {
            get { return _leftDown; }
            set { SetProperty(ref _leftDown, value); }
        }

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

        public ICommand LockCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (Convex(new[] { LeftTop, RightTop, RightDown, LeftDown }) > 0)
                    {
                        MessageBox.Show("error");
                    }
                }, ()=>true);
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
                RaisePropertyChanged("LeftTop");
                RaisePropertyChanged("RightTop");
                RaisePropertyChanged("RightDown");
                RaisePropertyChanged("LeftDown");
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
}