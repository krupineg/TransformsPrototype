using System.Collections.ObjectModel;
using System.Security.RightsManagement;

namespace TransformsPrototype
{
    public class MappingPlainConfigurationPointViewModel:ViewModelBase
    {
        private double _x;
        private double _y;

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

        public MappingPlainConfigurationPointViewModel(ILogger logger) : base(logger)
        {
        }

        public override string ToString()
        {
            return X + " : " + Y;
        }
    }
}