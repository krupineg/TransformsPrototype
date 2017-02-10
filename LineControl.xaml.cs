using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TransformsPrototype
{
    /// <summary>
    /// Interaction logic for LineControl.xaml
    /// </summary>
    public partial class LineControl : UserControl
    {
        public static readonly DependencyProperty StartProperty = DependencyProperty.Register(
            "Start", typeof (MappingPlanePointViewModel), typeof (LineControl), new PropertyMetadata(default(MappingPlanePointViewModel)));

        public MappingPlanePointViewModel Start
        {
            get { return (MappingPlanePointViewModel) GetValue(StartProperty); }
            set
            {
                SetValue(StartProperty, value);
                Start.PropertyChanged += StartOnPropertyChanged;
            }
        }

        private void StartOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "X" || propertyChangedEventArgs.PropertyName == "Y")
            {
                LineGeometry.StartPoint = new Point(Start.X, Start.Y);
            }
        }

        public static readonly DependencyProperty EndProperty = DependencyProperty.Register(
            "End", typeof (MappingPlanePointViewModel), typeof (LineControl), new PropertyMetadata(default(MappingPlanePointViewModel)));

        public MappingPlanePointViewModel End
        {
            get { return (MappingPlanePointViewModel) GetValue(EndProperty); }
            set
            {
                SetValue(EndProperty, value);
                End.PropertyChanged += EndOnPropertyChanged;
            }
        }

        private void EndOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "X" || propertyChangedEventArgs.PropertyName == "Y")
            {
                LineGeometry.EndPoint = new Point(End.X, End.Y);
            }
        }

        public LineControl()
        {
            InitializeComponent();
        }
    }
}
