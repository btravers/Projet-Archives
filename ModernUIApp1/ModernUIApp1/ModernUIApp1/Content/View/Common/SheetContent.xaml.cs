using ModernUIApp1.Pages.Popups;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModernUIApp1.Resources;
using Data.Data.Registre;
using ModernUIApp1.Handlers.Utils;
using Handlers.Utils;
using Handlers.Handlers;
using System.Threading;
using Data.Data.Registre.Annotation;
using System.Windows.Media.Effects;
using ModernUIApp1.Content.View.Registre;
using Data.Data;

namespace ModernUIApp1.Content.View.Common
{


    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    ///
    // Test page to test a code provided by : WPF simple zoom and drag support in a ScrollViewer By Kevin Stumpf, 6 Nov 2013
    // http://www.codeproject.com/Articles/97871/WPF-simple-zoom-and-drag-support-in-a-ScrollViewer

    public partial class SheetContent : UserControl
    {
        public static SheetContent window { get; private set; }

        Point? lastCenterPositionOnTarget;
        Point? lastMousePositionOnTarget;
        Point? lastDragPoint;
        AddAnnotation addAnnotationUserControl;
        DisplayAnnotation displayAnnotationUserControl;
        Boolean mouseMove;

        Point mouseStartDrag;

        private SheetHandler sheetHandler;

        public SheetContent()
        {
            InitializeComponent();

            sheetHandler = new SheetHandler();

            SheetContent.window = this;
            Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Arrange(new Rect(0, 0, window.DesiredSize.Width, window.DesiredSize.Height));

            scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            //scrollViewer.MouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseWheel += OnPreviewMouseWheel;

            scrollViewer.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;

            scrollViewer.MouseMove += OnMouseMove;            

            rmmImage.MouseLeftButtonUp += OnMouseLeftButtonUpImage;

            slider.ValueChanged += OnSliderValueChanged;
            slider.Value = 2;

            reload();
        }

        public void reload()
        {        
            Sheet sheet = ViewManager.instance.sheet;
            if (sheet != null)
            {
                FileCache.instance.downloadFile(Connection.ROOT_URL + "/" + ModernUIApp1.Resources.LinkResources.LinkPrintFile.Replace(ModernUIApp1.Resources.LinkResources.Path, sheet.url.Replace("/", "-")), sheet.url,
                    () =>
                    {
                        if (File.Exists(sheet.url))
                        {
                            rmmImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/" + sheet.url, UriKind.Absolute));
                        }
                    }
                );
            }

            onImageChange();
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

            if (scrollViewer.ScrollableWidth == 0)
            {
                mouseStartDrag = e.GetPosition(scrollViewer);
            }
        }

        void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            lastMousePositionOnTarget = Mouse.GetPosition(grid);

            if (e.Delta > 0)
                slider.Value += 1;
            else if (e.Delta < 0)
                slider.Value -= 1;

            e.Handled = true;
        }

        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.Cursor = Cursors.Arrow;
            scrollViewer.ReleaseMouseCapture();
            lastDragPoint = null;

            if (scrollViewer.ScrollableWidth == 0)
            {
                Point mouseEndDrag = e.GetPosition(scrollViewer);

                if (mouseEndDrag.X < mouseStartDrag.X && (mouseStartDrag.X - mouseEndDrag.X) > 100)
                {
                    Storyboard anim = (Storyboard)this.Resources["leftAnimation"];
                    anim.Completed -= animNext_Completed;
                    anim.Completed += animNext_Completed;
                    anim.Begin();

                }
                else if (mouseEndDrag.X > mouseStartDrag.X && (mouseEndDrag.X - mouseStartDrag.X) > 100)
                {
                    Storyboard anim = (Storyboard)this.Resources["rightAnimation"];
                    anim.Completed -= animPrevious_Completed;
                    anim.Completed += animPrevious_Completed;
                    anim.Begin();
                }
            }
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
                    addAnnotationUserControl.close_dialog();
                }

                addAnnotationUserControl = new AddAnnotation(position);
                // addAnnotationUserControl.window.Title = "Ajouter une annotation";

                Double left;

                if (mouse.X < SystemParameters.FullPrimaryScreenWidth / 2)
                {
                    left = mouse.X + SystemParameters.FullPrimaryScreenWidth / 8;
                }
                else
                {
                    left = mouse.X - SystemParameters.FullPrimaryScreenWidth / 4;
                }

                addAnnotationUserControl.setParameters(left, mouse.Y);
                addAnnotationUserControl.Show();
/*
                addAnnotationUserControl.window.Top = mouse.Y;
                addAnnotationUserControl.window.Width = addAnnotationUserControl.Width + 25;
                addAnnotationUserControl.window.Height = addAnnotationUserControl.Height + 35;
                addAnnotationUserControl.window.ResizeMode = ResizeMode.NoResize;
                addAnnotationUserControl.window.WindowStyle = System.Windows.WindowStyle.None;
                addAnnotationUserControl.window.AllowsTransparency = true;
                addAnnotationUserControl.window.Background = Brushes.Transparent;
                addAnnotationUserControl.window.Content = addAnnotationUserControl;
                addAnnotationUserControl.window.Show();
 */
            }
            else
            {
                mouseMove = false;
            }
        }

        void animNext_Completed(object sender, EventArgs e)
        {

            if (ViewManager.instance.nextSheet != null)
            {
                try
                {
                    rmmImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/" + ViewManager.instance.nextSheet.url, UriKind.Absolute));

                    ViewManager.instance.sheet = ViewManager.instance.nextSheet;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                } 
            }

            onImageChange();

            Storyboard anim = (Storyboard)this.Resources["backNextAnimation"];
            anim.Begin();
        }

        void animPrevious_Completed(object sender, EventArgs e)
        {            
            if (ViewManager.instance.previousSheet != null)
            {
                try
                {
                    rmmImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/" + ViewManager.instance.previousSheet.url, UriKind.Absolute));

                    ViewManager.instance.sheet = ViewManager.instance.previousSheet;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                } 
            }            

            onImageChange();

            Storyboard anim = (Storyboard)this.Resources["backPreviousAnimation"];
            anim.Begin();
        }

        void onImageChange()
        {
            slider.Value = 2;
            sliderContrast.Value = 0;
            sliderBrightness.Value = 0;

            Sheet sheet = ViewManager.instance.sheet;
            if (sheet != null && Authenticator.AUTHENTICATOR.user != null)
            {
                User user = Authenticator.AUTHENTICATOR.user;
                if (user == null)
                    user = new User(0, "xx");

                sheetHandler.preloadSheets(sheet.id_sheet);

                // TODO download annotations
                AnnotationHandler annotHandler = new AnnotationHandler(user);
                List<AnnotationSheet> annotations = annotHandler.getAnnotationSheetBySheetId(sheet.id_sheet);
                displayAnnotations(annotations);
            }

            if (IdentitySheet.IDENTITYSHEET != null)
                IdentitySheet.IDENTITYSHEET.reload();
        }

        void displayAnnotations(List<AnnotationSheet> annotations)
        {
            if (annotations == null)
                return;

            overlay.Children.Clear();

            foreach (AnnotationSheet annotation in annotations)
                displayAnnotationCircle(annotation);
        }

        void displayAnnotationCircle(AnnotationSheet annotation)
        {
            Ellipse e = new Ellipse();
            e.Width = 8;
            e.Height = 8;
            e.Fill = new SolidColorBrush(Colors.CornflowerBlue);
            e.Tag = annotation;
            double x = (double)annotation.x / ((BitmapSource)rmmImage.Source).PixelWidth * rmmImage.ActualWidth;
            double y = (double)annotation.y / ((BitmapSource)rmmImage.Source).PixelHeight * rmmImage.ActualHeight;
            Canvas.SetLeft(e, x);
            Canvas.SetTop(e, y);
            e.MouseLeftButtonUp += OnMouseLeftButtonUpAnnotation;
            overlay.Children.Add(e);

            Console.WriteLine(annotation.id_annotations_sheet + ", x=" + annotation.x + ", y=" + annotation.y);
        }

        void OnMouseLeftButtonUpAnnotation(object sender, MouseButtonEventArgs e)
        {
            AnnotationSheet annotation = (AnnotationSheet) ((Ellipse)sender).Tag;
            Console.WriteLine("id:" + annotation.id_annotations_sheet + ", ty:" + annotation.type + ", tx:" + annotation.text);

            if (displayAnnotationUserControl != null)
            {
                displayAnnotationUserControl.close_dialog();
            }

            displayAnnotationUserControl = new DisplayAnnotation();

            Double left;

            if (annotation.x < SystemParameters.FullPrimaryScreenWidth / 2)
                left = annotation.x + SystemParameters.FullPrimaryScreenWidth / 8;
            else
                left = annotation.x - SystemParameters.FullPrimaryScreenWidth / 4;

            displayAnnotationUserControl.setPosition(left, annotation.y);
            displayAnnotationUserControl.setParameters(annotation.text, annotation.type);
            displayAnnotationUserControl.Show();
        }
    }
}