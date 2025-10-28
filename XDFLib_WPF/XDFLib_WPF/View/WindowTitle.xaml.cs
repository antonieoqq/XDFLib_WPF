using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace XDFLib_WPF.View
{
    public partial class WindowTitle : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(WindowTitle),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }
        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register("TitleFontSize", typeof(double), typeof(WindowTitle),
                new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush TitleBackground
        {
            get { return (Brush)GetValue(TitleBackgroundProperty); }
            set { SetValue(TitleBackgroundProperty, value); }
        }
        public static readonly DependencyProperty TitleBackgroundProperty =
            DependencyProperty.Register("TitleBackground", typeof(Brush), typeof(WindowTitle),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush TitleForeground
        {
            get { return (Brush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }
        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(WindowTitle),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.LightGray), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public CornerRadius TitleCorner
        {
            get { return (CornerRadius)GetValue(TitleCornerProperty); }
            set { SetValue(TitleCornerProperty, value); }
        }
        public static readonly DependencyProperty TitleCornerProperty =
            DependencyProperty.Register("TitleCorner", typeof(CornerRadius), typeof(WindowTitle),
                new FrameworkPropertyMetadata(new CornerRadius(8, 8, 0, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush TitleBorderBrush
        {
            get { return (Brush)GetValue(TitleBorderBrushProperty); }
            set { SetValue(TitleBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty TitleBorderBrushProperty =
            DependencyProperty.Register("TitleBorderBrush", typeof(Brush), typeof(WindowTitle),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.DimGray), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Thickness TitleBorderThickness
        {
            get { return (Thickness)GetValue(TitleBorderThicknessProperty); }
            set { SetValue(TitleBorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty TitleBorderThicknessProperty =
            DependencyProperty.Register("TitleBorderThickness", typeof(Thickness), typeof(WindowTitle),
                new FrameworkPropertyMetadata(new Thickness(1), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(WindowTitle),
                new FrameworkPropertyMetadata(14.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Thickness IconGapMargin
        {
            get { return (Thickness)GetValue(IconGapMarginProperty); }
            set { SetValue(IconGapMarginProperty, value); }
        }
        public static readonly DependencyProperty IconGapMarginProperty =
            DependencyProperty.Register("IconGapMargin", typeof(Thickness), typeof(WindowTitle),
                new FrameworkPropertyMetadata(new Thickness(4, 0, 4, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush IconBorderBrush
        {
            get { return (Brush)GetValue(IconBorderBrushProperty); }
            set { SetValue(IconBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty IconBorderBrushProperty =
            DependencyProperty.Register("IconBorderBrush", typeof(Brush), typeof(WindowTitle),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Gray), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Thickness IconBorderThickness
        {
            get { return (Thickness)GetValue(IconBorderThicknessProperty); }
            set { SetValue(IconBorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty IconBorderThicknessProperty =
            DependencyProperty.Register("IconBorderThickness", typeof(Thickness), typeof(WindowTitle),
                new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public Brush MinimizeBrush
        {
            get { return (Brush)GetValue(MinimizeBrushProperty); }
            set { SetValue(MinimizeBrushProperty, value); }
        }
        public static readonly DependencyProperty MinimizeBrushProperty =
            DependencyProperty.Register("MinimizeBrush", typeof(Brush), typeof(WindowTitle),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Yellow), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush MaximizeBrush
        {
            get { return (Brush)GetValue(MaximizeBrushProperty); }
            set { SetValue(MaximizeBrushProperty, value); }
        }
        public static readonly DependencyProperty MaximizeBrushProperty =
            DependencyProperty.Register("MaximizeBrush", typeof(Brush), typeof(WindowTitle),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.LightGreen)));

        public Brush CloseBrush
        {
            get { return (Brush)GetValue(CloseBrushProperty); }
            set { SetValue(CloseBrushProperty, value); }
        }
        public static readonly DependencyProperty CloseBrushProperty =
            DependencyProperty.Register("CloseBrush", typeof(Brush), typeof(WindowTitle),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Red), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        Window _parentWindow;
        public Window ParentWindow
        {
            get
            {
                if (_parentWindow == null)
                {
                    _parentWindow = Window.GetWindow(this);
                }
                return _parentWindow;
            }
        }

        public WindowTitle()
        {
            InitializeComponent();
        }

        private void TitleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                ParentWindow.WindowState =
                    ParentWindow.WindowState == WindowState.Normal ?
                    WindowState.Maximized :
                    WindowState.Normal;
            }
        }

        private void WindowMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try { ParentWindow.DragMove(); }
                catch (Exception) { }
            }
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            ParentWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow(object sender, RoutedEventArgs e)
        {
            if (ParentWindow.WindowState == WindowState.Maximized)
            {
                ParentWindow.WindowState = WindowState.Normal;
            }
            else
            {
                ParentWindow.WindowState = WindowState.Maximized;
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            ParentWindow.Close();
        }
    }
}
