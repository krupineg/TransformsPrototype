using System;
using System.Collections.Generic;
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
            Loaded+= OnLoaded;

        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Point position = Mouse.GetPosition(Rectangle2);
            VisualTreeHelper.HitTest(Rectangle2, null, new HitTestResultCallback(HitTestResultHandler),
                    new PointHitTestParameters(position)
            );
        }

        private HitTestResultBehavior HitTestResultHandler(HitTestResult result)
        {
            PointHitTestResult hitResult = (PointHitTestResult)result;
            Console.WriteLine(((FrameworkElement)hitResult.VisualHit).Name);
            Console.WriteLine(hitResult.PointHit.ToString());
            return HitTestResultBehavior.Continue;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            
            /*SkewTransform.CenterX = Rectangle.RenderedGeometry.Bounds.X + Rectangle.RenderedGeometry.Bounds.Width/2;
            SkewTransform.CenterY = Rectangle.RenderedGeometry.Bounds.Y + Rectangle.RenderedGeometry.Bounds.Height / 2;
            Task.Factory.StartNew(async () =>
            {
               
                double inc = 0.1;
                for (int i = 0; i < 2000; i++)
                {
                   
                    await Task.Delay(1);
                    Dispatcher.Invoke(new Action(() =>
                    {
                        if (SkewTransform.AngleX > 45 || SkewTransform.AngleX < -45)
                        {
                            inc = -1*inc;
                        }
                        SkewTransform.AngleX += inc;
                        SkewTransform.AngleY += inc;
                    }));
                }
            });*/
        }
    }

    public class PointViewModel
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
