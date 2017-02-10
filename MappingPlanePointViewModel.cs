using System.Collections.ObjectModel;
using System.Security.RightsManagement;

namespace TransformsPrototype
{
    public class MappingPlanePointViewModel : ViewModelBase, IMappingPlanePointViewModel
    {
        private double _x;
        private double _y;
        private IMappingPlanePointViewModel _next;

        public double X
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
        }

        public double Y
        {
            get { return _y; }
            set { SetProperty(ref _y, value); }
        }

        public IMappingPlanePointViewModel Next
        {
            get
            {
                return _next;
            }
            set
            {
                SetProperty(ref _next, value);
            }
        }

        public MappingPlanePointViewModel(ILogger logger, double x, double y) : base(logger)
        {
            _x = x;
            _y = y;
        }
    }
}