using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var leftTop = new MappingPlanePointViewModel(null, 100, 100);
            var rightTop = new MappingPlanePointViewModel(null, 300, 100);
            var rightBottom = new MappingPlanePointViewModel(null, 300, 300);
            var leftBottom = new MappingPlanePointViewModel(null, 100, 300);
            DataContext = new MappingPlaneViewModel(null, leftTop, rightTop, rightBottom, leftBottom);
        }
    }
}
