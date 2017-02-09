using System;
using System.Collections.Generic;
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
            Loaded+= OnLoaded;

        }

        bool captured = false;
        double mouseOffsetX, currentMouseX, mouseOffsetY, currentMouseY;
        UIElement source = null;

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Point position = Mouse.GetPosition(Rectangle2);
            var result = VisualTreeHelper.HitTest(Rectangle2, position);
            if (result != null)
            {
                PointHitTestResult hitResult = (PointHitTestResult)result;
                source = (UIElement)Rectangle2;
                Mouse.Capture(source);
                captured = true;
                currentMouseX = e.GetPosition(this).X;
                currentMouseY = e.GetPosition(this).Y;
                //currentMouseX = hitResult.PointHit.X;
                //currentMouseY = hitResult.PointHit.Y;
                Debug.WriteLine("");
                Debug.WriteLine("Inside Mouse down");
                Debug.WriteLine("currentMouseX = " + currentMouseX);
                Debug.WriteLine("currentMouseY = " + currentMouseY);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (captured)
            {
                double newMouseX = e.GetPosition(this).X;
                double newMouseY = e.GetPosition(this).Y;

                Debug.WriteLine("");
                Debug.WriteLine("currentMouseX = " + currentMouseX);
                Debug.WriteLine("currentMouseY = " + currentMouseY);
                Debug.WriteLine("newMosueX = " + newMouseX);
                Debug.WriteLine("newMosueY = " + newMouseY);

                mouseOffsetX = newMouseX - currentMouseX;
                mouseOffsetY = newMouseY - currentMouseY;
                currentMouseX = newMouseX;
                currentMouseY = newMouseY;

                Debug.WriteLine("After transform");
                Debug.WriteLine("currentMouseX = " + currentMouseX);
                Debug.WriteLine("currentMouseY = " + currentMouseY);
                var transform = Rectangle2.RenderTransform as TranslateTransform;
                transform.X += mouseOffsetX;
                transform.Y += mouseOffsetY;
            }
        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            Mouse.Capture(null);
            captured = false;
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
