using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Point = System.Windows.Point;

namespace TransformsPrototype
{
    /// <summary>
    /// Interaction logic for MappingPlainField.xaml
    /// </summary>
    public partial class MappingPlaneView : UserControl
    {
        public MappingPlaneView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem", typeof (object), typeof (MappingPlaneView), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty MouseDownCommandProperty = DependencyProperty.Register(
            "MouseDownCommand", typeof (ICommand), typeof (MappingPlaneView), new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty MouseMoveCommandProperty = DependencyProperty.Register(
            "MouseMoveCommand", typeof (ICommand), typeof (MappingPlaneView), new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty MouseUpCommandProperty = DependencyProperty.Register(
            "MouseUpCommand", typeof (ICommand), typeof (MappingPlaneView), new PropertyMetadata(default(ICommand)));

        public ICommand MouseUpCommand
        {
            get { return (ICommand) GetValue(MouseUpCommandProperty); }
            set { SetValue(MouseUpCommandProperty, value); }
        }
        public ICommand MouseMoveCommand
        {
            get { return (ICommand) GetValue(MouseMoveCommandProperty); }
            set { SetValue(MouseMoveCommandProperty, value); }
        }
        public ICommand MouseDownCommand
        {
            get { return (ICommand) GetValue(MouseDownCommandProperty); }
            set { SetValue(MouseDownCommandProperty, value); }
        }
        
        public object SelectedItem
        {
            get { return (object) GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private FrameworkElement _source;

        public FrameworkElement Source
        {
            get { return _source; }
            set
            {
                _source = value;
            }
        }
        private T FindVisualParent<T>(DependencyObject child)
           where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            return FindVisualParent<T>(parentObject);
        }
        
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Source = FindVisualParent<MappingPlanePointView>(e.OriginalSource as DependencyObject);
            if (Source != null)
            {
                SelectedItem = Source.DataContext;
                Point position = Mouse.GetPosition(Source);
                var result = VisualTreeHelper.HitTest(Source, position);
                if (result != null)
                {
                    Mouse.Capture(Source);
                    MouseDownCommand.Execute(e.GetPosition(this));
                }
            }
            else
            {
                Debug.Write("hueta");
                SelectedItem = null;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            MouseMoveCommand.Execute(e.GetPosition(this));
         
        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            Mouse.Capture(null);
            SelectedItem = null;
            MouseUpCommand.Execute(null);
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
}
