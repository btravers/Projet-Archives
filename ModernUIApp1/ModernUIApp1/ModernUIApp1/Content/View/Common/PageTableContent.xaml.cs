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
using ModernUIApp1.Handlers.Utils;
using Data.Data.Registre;
using Handlers.Utils;
using Data.Data.Registre.Annotation;
using Handlers.Handlers;
using ModernUIApp1.Pages;


namespace ModernUIApp1.Content.View.Common
{


    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    ///
    // Test page to test a code provided by : WPF simple zoom and drag support in a ScrollViewer By Kevin Stumpf, 6 Nov 2013
    // http://www.codeproject.com/Articles/97871/WPF-simple-zoom-and-drag-support-in-a-ScrollViewer

    public partial class PageTableContent : UserControl
    {
        public static PageTableContent window { get; private set; }

        Point? lastCenterPositionOnTarget;
        Point? lastMousePositionOnTarget;
        Point? lastDragPoint;
        AddAnnotationTable addAnnotationUserControl;
        Boolean mouseMove;

        Point mouseStartDrag;

        Point positionTopLeft;

        private TableHandler tableHandler;

        public PageTableContent()
        {
            InitializeComponent();

            tableHandler = new TableHandler();

            PageTableContent.window = this;
            Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Arrange(new Rect(0, 0, window.DesiredSize.Width, window.DesiredSize.Height));

            scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            //scrollViewer.MouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseWheel += OnPreviewMouseWheel;

            scrollViewer.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;

            scrollViewer.MouseMove += OnMouseMove;            

            pageImage.MouseLeftButtonUp += OnMouseLeftButtonUpImage;

            slider.ValueChanged += OnSliderValueChanged;

            reload();
        }

        public void reload()
        {
            slider.Value = 2;

            noImageError.Visibility = Visibility.Hidden;
            
            if (ViewManager.instance.pageTables != null)
            {
                PageTable pageTable = ViewManager.instance.pageTables[ViewManager.instance.indexPageTables];
                FileCache.instance.downloadFile(Connection.ROOT_URL + "/" + ModernUIApp1.Resources.LinkResources.LinkPrintFile.Replace(ModernUIApp1.Resources.LinkResources.Path, pageTable.url.Replace("/", "-")), pageTable.url,
                    () =>
                    {
                        if (File.Exists(pageTable.url))
                        {
                            pageImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/" + pageTable.url, UriKind.Absolute));
                        }
                    },
                    () =>
                    {

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
                Point mouse = Mouse.GetPosition(this);

                if (Authenticator.AUTHENTICATOR.connected)
                {
                    if ((positionTopLeft.X == -1 || positionTopLeft.Y == -1) && (addAnnotationUserControl == null || !addAnnotationUserControl.IsVisible))
                    {
                        positionTopLeft = e.MouseDevice.GetPosition(overlay);

                        Ellipse ell = new Ellipse();
                        ell.Width = 2;
                        ell.Height = 2;
                        ell.StrokeThickness = 0.2;
                        ell.Stroke = new SolidColorBrush(Colors.Blue);
                        ell.Fill = new SolidColorBrush(Color.FromArgb(100, 100, 149, 237));
                        double x = positionTopLeft.X - ell.Width / 2;
                        double y = positionTopLeft.Y - ell.Height / 2;
                        Canvas.SetLeft(ell, x);
                        Canvas.SetTop(ell, y);
                        overlay.Children.Add(ell);

                        positionTopLeft.X = (int)(positionTopLeft.X * ((BitmapSource)pageImage.Source).PixelWidth / pageImage.ActualWidth);
                        positionTopLeft.Y = (int)(positionTopLeft.Y * ((BitmapSource)pageImage.Source).PixelHeight / pageImage.ActualHeight);
                    }
                    else
                    {
                        Point positionBottomRight = e.MouseDevice.GetPosition(overlay);
                        positionBottomRight.X = (int)(positionBottomRight.X * ((BitmapSource)pageImage.Source).PixelWidth / pageImage.ActualWidth);
                        positionBottomRight.Y = (int)(positionBottomRight.Y * ((BitmapSource)pageImage.Source).PixelHeight / pageImage.ActualHeight);

                        if (addAnnotationUserControl != null)
                        {
                            addAnnotationUserControl.close_dialog();
                        }

                        if ((int)positionTopLeft.X < (int)positionBottomRight.X && (int)positionTopLeft.Y < (int)positionBottomRight.Y)
                        {
                            addAnnotationUserControl = new AddAnnotationTable(positionTopLeft, positionBottomRight);

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
                        }
                        else
                        {
                            MessageBox.Show("Le second point doit être situé en bas à droite du premier.", "Opération impossible");
                            reload();
                        }

                        positionTopLeft = new Point(-1, -1);
                    }
                }

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
                positionTopLeft = new Point(-1, -1);
            }
        }

        void animNext_Completed(object sender, EventArgs e)
        {
            overlay.Children.Clear();
            
            if (ViewManager.instance.pageTables != null && ViewManager.instance.indexPageTables + 1 < ViewManager.instance.pageTables.Count)
            {
                ViewManager.instance.indexPageTables++;
                PageTable pageTable = ViewManager.instance.pageTables[ViewManager.instance.indexPageTables];
                try
                {
                    noImageError.Visibility = Visibility.Hidden;
                    pageImage.Visibility = System.Windows.Visibility.Visible;
                    pageImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/" + pageTable.url, UriKind.Absolute));

                    ViewManager.instance.pageTable = pageTable;

                    onImageChange();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);

                    pageImage.Visibility = System.Windows.Visibility.Hidden;
                    noImageError.Visibility = Visibility.Visible;
                }
            }            
            
            Storyboard anim = (Storyboard)this.Resources["backNextAnimation"];
            anim.Begin();
        }

        void animPrevious_Completed(object sender, EventArgs e)
        {
            overlay.Children.Clear();

            if (ViewManager.instance.pageTables != null && ViewManager.instance.indexPageTables - 1 >= 0)
            {
                ViewManager.instance.indexPageTables--;
                PageTable pageTable = ViewManager.instance.pageTables[ViewManager.instance.indexPageTables];
                try
                {
                    noImageError.Visibility = Visibility.Hidden;
                    pageImage.Visibility = System.Windows.Visibility.Visible;
                    pageImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/" + pageTable.url, UriKind.Absolute));

                    ViewManager.instance.pageTable = pageTable;

                    onImageChange();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);

                    pageImage.Visibility = System.Windows.Visibility.Hidden;
                    noImageError.Visibility = Visibility.Visible;
                }                
            }

            Storyboard anim = (Storyboard)this.Resources["backPreviousAnimation"];
            anim.Begin();
        }

        void onImageChange()
        {
            slider.Value = 2;
            sliderContrast.Value = 0;
            sliderBrightness.Value = 0;

            // TODO Appel à displayAnnotations
            PageTable page = ViewManager.instance.pageTable;
            if (page != null)
            {
                AnnotationHandler annotHandler = new AnnotationHandler(Authenticator.AUTHENTICATOR.user);
                List<AnnotationPageTable> annotations = annotHandler.getAnnotationPageTableByPageTableId(page.id_page_table);

                displayAnnotations(annotations);
            }
        }

        public void displayAnnotations(List<AnnotationPageTable> annotations)
        {
            overlay.Children.Clear();

            if (annotations == null)
                return;

            foreach (AnnotationPageTable annotation in annotations)
                displayAnnotationRectangle(annotation);
        }

        void displayAnnotationRectangle(AnnotationPageTable annotation)
        {            
            double padding = 0.8;

            Rectangle r = new Rectangle();
            r.Width = ((double)annotation.width / ((BitmapSource)pageImage.Source).PixelWidth * pageImage.ActualWidth) - 2 * padding;
            r.Height = ((double)annotation.height / ((BitmapSource)pageImage.Source).PixelHeight * pageImage.ActualHeight) - 2 * padding;
            r.StrokeThickness = 0.2;
            r.Stroke = new SolidColorBrush(Colors.Blue);
            r.Fill = new SolidColorBrush(Color.FromArgb(80, 101, 156, 239));
            r.Tag = annotation;
            double x = ((double)annotation.x / ((BitmapSource)pageImage.Source).PixelWidth * pageImage.ActualWidth) + padding;
            double y = ((double)annotation.y / ((BitmapSource)pageImage.Source).PixelHeight * pageImage.ActualHeight) + padding;
            Canvas.SetLeft(r, x);
            Canvas.SetTop(r, y);
            r.MouseLeftButtonUp += OnMouseLeftButtonUpAnnotation;
            overlay.Children.Add(r);
        }

        void OnMouseLeftButtonUpAnnotation(object sender, MouseButtonEventArgs e)
        {
            try
            {
                AnnotationPageTable annotation = (AnnotationPageTable)((Rectangle)sender).Tag;
                Console.WriteLine("id:" + annotation.id_annotation_page_table + ", num:" + annotation.id_number + ", x:" + annotation.x + ", y:" + annotation.y);

                SheetHandler sheetHandler = new SheetHandler();

                if (annotation.id_sheet != -1)
                {
                    ViewManager.instance.sheet = sheetHandler.getById(annotation.id_sheet);

                    if (ViewManager.instance.sheet != null)
                    {
                        if (ViewRegister.window != null)
                        {
                            ViewRegister.window.reload();
                        }

                        MainWindow.window.ContentSource = new Uri("/Pages/ViewRegister.xaml", UriKind.Relative);      
                    }
                    else
                    {
                        Console.WriteLine("Image introuvable...");
                    }
                }
            }
            catch (System.InvalidCastException ice)
            {
                Console.WriteLine(ice.Message);
            }
        }

    }
}