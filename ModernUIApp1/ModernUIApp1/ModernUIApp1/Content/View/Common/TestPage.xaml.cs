using ModernUIApp1.Pages.Popups;
using System;
using System.Collections.Generic;
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

namespace ModernUIApp1.Content.View.Common
{


    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    ///
    // Test page to test a code provided by : WPF simple zoom and drag support in a ScrollViewer By Kevin Stumpf, 6 Nov 2013
    // http://www.codeproject.com/Articles/97871/WPF-simple-zoom-and-drag-support-in-a-ScrollViewer

    public partial class TestPage : UserControl
    {

        Point? lastCenterPositionOnTarget;
        Point? lastMousePositionOnTarget;
        Point? lastDragPoint;
        AddAnnotation addAnnotationUserControl;
        Boolean mouseMove;

        public TestPage()
        {
            InitializeComponent();

            scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            scrollViewer.MouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseWheel += OnPreviewMouseWheel;

            scrollViewer.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            scrollViewer.MouseMove += OnMouseMove;

            rmmImage.MouseLeftButtonUp += OnMouseLeftButtonUpImage;

            slider.ValueChanged += OnSliderValueChanged;
            slider.Value = 2;

            rmmImage.ManipulationDelta += OnManipulationDelta;

            Ellipse e = new Ellipse();
            e.Width = 8;
            e.Height = 8;
            e.Fill = new SolidColorBrush(Colors.CornflowerBlue);
            Canvas.SetLeft(e, 20);
            Canvas.SetTop(e, 20);
            e.MouseLeftButtonUp += OnMouseLeftButtonUpAnnotation;
            overlay.Children.Add(e);
        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (lastDragPoint.HasValue)
            {
                Point posNow = e.GetPosition(scrollViewer);

                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;

                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);

                if (dX != 0 || dY != 0)
                {
                    mouseMove = true;

                    scrollViewer.Cursor = Cursors.SizeAll;
                }

                
            }
        }

        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(scrollViewer);
            if (mousePos.X <= scrollViewer.ViewportWidth && mousePos.Y < scrollViewer.ViewportHeight) //make sure we still can use the scrollbars
            {
                lastDragPoint = mousePos;
                Mouse.Capture(scrollViewer);
            }
        }

        void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            lastMousePositionOnTarget = Mouse.GetPosition(grid);

            if (e.Delta > 0)
            {
                slider.Value += 1;
            }
            if (e.Delta < 0)
            {
                slider.Value -= 1;
            }

            e.Handled = true;
        }

        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.Cursor = Cursors.Arrow;
            scrollViewer.ReleaseMouseCapture();
            lastDragPoint = null;
        }

        void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            scaleTransform.ScaleX = e.NewValue;
            scaleTransform.ScaleY = e.NewValue;

            var centerOfViewport = new Point(scrollViewer.ViewportWidth / 2, scrollViewer.ViewportHeight / 2);
            lastCenterPositionOnTarget = scrollViewer.TranslatePoint(centerOfViewport, grid);
        }

        void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? targetBefore = null;
                Point? targetNow = null;

                if (!lastMousePositionOnTarget.HasValue)
                {
                    if (lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new Point(scrollViewer.ViewportWidth / 2, scrollViewer.ViewportHeight / 2);
                        Point centerOfTargetNow = scrollViewer.TranslatePoint(centerOfViewport, grid);

                        targetBefore = lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(grid);

                    lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    double dXInTargetPixels = targetNow.Value.X - targetBefore.Value.X;
                    double dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    double multiplicatorX = e.ExtentWidth / grid.Width;
                    double multiplicatorY = e.ExtentHeight / grid.Height;

                    double newOffsetX = scrollViewer.HorizontalOffset - dXInTargetPixels * multiplicatorX;
                    double newOffsetY = scrollViewer.VerticalOffset - dYInTargetPixels * multiplicatorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }

                    scrollViewer.ScrollToHorizontalOffset(newOffsetX);
                    scrollViewer.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }

        void OnMouseLeftButtonUpImage(object sender, MouseButtonEventArgs e)
        {
            if (!mouseMove)
            {
                Point position = e.MouseDevice.GetPosition(rmmImage);

                Point mouse = Mouse.GetPosition(this);

                if (addAnnotationUserControl != null)
                {
                    addAnnotationUserControl.window.Close();
                }

                addAnnotationUserControl = new AddAnnotation(position);
                addAnnotationUserControl.window.Title = "Ajouter une annotation";
                if (mouse.X < SystemParameters.FullPrimaryScreenWidth / 2)
                {
                    addAnnotationUserControl.window.Left = mouse.X + SystemParameters.FullPrimaryScreenWidth / 8;
                }
                else
                {
                    addAnnotationUserControl.window.Left = mouse.X - SystemParameters.FullPrimaryScreenWidth / 4;
                }
                addAnnotationUserControl.window.Top = mouse.Y;
                addAnnotationUserControl.window.Width = addAnnotationUserControl.Width + 25;
                addAnnotationUserControl.window.Height = addAnnotationUserControl.Height + 35;
                addAnnotationUserControl.window.ResizeMode = ResizeMode.NoResize;
                addAnnotationUserControl.window.WindowStyle = System.Windows.WindowStyle.ToolWindow;
                addAnnotationUserControl.window.Content = addAnnotationUserControl;
                addAnnotationUserControl.window.Show();
            }
            else
            {
                mouseMove = false;
            }
        }

        void OnManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            Console.WriteLine("aaa");

            UIElement element = sender as UIElement;

            ScaleTransform transform = element.RenderTransform as ScaleTransform;
            if (transform != null)
            {
                transform.ScaleX *= e.DeltaManipulation.Scale.X;
                transform.ScaleY *= e.DeltaManipulation.Scale.Y;
            }
            
            //CompositeTransform transform = element.RenderTransform as CompositeTransform;
            /*Transform transform = element.RenderTransform;
            if (transform != null)
            {
                transform.ScaleX *= e.DeltaManipulation.Scale;
                transform.ScaleY *= e.DeltaManipulation.Scale;
                transform.Rotation += e.DeltaManipulation.Rotation * 180 / Math.PI;
                transform.TranslateX += e.DeltaManipulation.Translation.X;
                transform.TranslateY += e.DeltaManipulation.Translation.Y;
            }*/
        }

        void OnMouseLeftButtonUpAnnotation(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("click annotation");
        }
    }
}