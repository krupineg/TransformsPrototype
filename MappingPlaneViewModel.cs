using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TransformsPrototype
{
    public class MappingPlaneViewModel : ViewModelBase
    {
        bool _captured = false;
        double _mouseOffsetX, _currentMouseX, _mouseOffsetY, _currentMouseY;
        private IMappingPlanePointViewModel _selectedPoint;
        //private readonly IMappingPlanePointViewModel _leftTop;
        //private readonly IMappingPlanePointViewModel _rightTop;
        //private readonly IMappingPlanePointViewModel _rightBottom;
        //private readonly IMappingPlanePointViewModel _leftBottom;
        private IMappingPlanePointViewModel[] _itemsCollection;
        private IConvexityCalculator _convexityCalculator;
        private bool _isValid;

        public MappingPlaneViewModel(ILogger logger, IConvexityCalculator convexityCalculator, IMappingPlanePointViewModel leftTop, IMappingPlanePointViewModel rightTop, IMappingPlanePointViewModel rightBottom, IMappingPlanePointViewModel leftBottom) : base(logger)
        {
            _convexityCalculator = convexityCalculator;
            _itemsCollection = new []
            {
                leftTop,
                rightTop,
                rightBottom,
                leftBottom
            };
            leftTop.Next = rightTop;
            rightTop.Next = rightBottom;
            rightBottom.Next = leftBottom;
            leftBottom.Next = leftTop;
            IsValid = _convexityCalculator.IsConvex(_itemsCollection);
        }

        public IMappingPlanePointViewModel SelectedPoint
        {
            get { return _selectedPoint; }
            set
            {
                SetProperty(ref _selectedPoint, value);
            }
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
                return new DelegateCommand(MouseMoveExecute, o => _captured);
            }
        }

        public ICommand MouseUpCommand
        {
            get
            {
                return new DelegateCommand(MouseUpExecute, o => _captured);
            }
        }

        public IReadOnlyCollection<IMappingPlanePointViewModel> ItemsCollection
        {
            get
            {
                return _itemsCollection;
            }

        }

        public bool IsValid
        {
            get { return _isValid; }
            set { SetProperty(ref _isValid, value); }
        }

        private void MouseMoveExecute(object obj)
        {
            if (_captured)
            {
                var point = (Point)obj;
                Debug.WriteLine("");
                Debug.WriteLine("currentMouseX = " + _currentMouseX);
                Debug.WriteLine("currentMouseY = " + _currentMouseY);
                Debug.WriteLine("newMosueX = " + point.X);
                Debug.WriteLine("newMosueY = " + point.Y);

                _mouseOffsetX = point.X - _currentMouseX;
                _mouseOffsetY = point.Y - _currentMouseY;
                _currentMouseX = point.X;
                _currentMouseY = point.Y;

                Debug.WriteLine("After transform");
                Debug.WriteLine("currentMouseX = " + _currentMouseX);
                Debug.WriteLine("currentMouseY = " + _currentMouseY);
                SelectedPoint.X += _mouseOffsetX;
                SelectedPoint.Y += _mouseOffsetY;
                RaisePropertyChanged("ItemsCollection");
                IsValid = _convexityCalculator.IsConvex(_itemsCollection);
            }
           
        }


        private void MouseDownExecute(object obj)
        {
            var point = (Point?)obj;
            if (point.HasValue)
            {
                _captured = true;
                _currentMouseX = point.Value.X;
                _currentMouseY = point.Value.Y;
            }
        }

        private void MouseUpExecute(object obj)
        {
            _captured = false;
        }
    }
}