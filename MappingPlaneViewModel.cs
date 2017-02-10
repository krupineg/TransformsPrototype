using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private ObservableCollection<IMappingPlanePointViewModel> _itemsCollection;

        public MappingPlaneViewModel(ILogger logger, IMappingPlanePointViewModel leftTop, IMappingPlanePointViewModel rightTop, IMappingPlanePointViewModel rightBottom, IMappingPlanePointViewModel leftBottom) : base(logger)
        {
            _itemsCollection = new ObservableCollection<IMappingPlanePointViewModel>
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

        public ObservableCollection<IMappingPlanePointViewModel> ItemsCollection
        {
            get
            {
                return _itemsCollection;
            }

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