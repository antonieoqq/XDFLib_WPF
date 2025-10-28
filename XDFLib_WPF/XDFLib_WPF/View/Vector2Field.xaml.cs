using System.Numerics;
using System.Windows;
using System.Windows.Media;

namespace XDFLib_WPF.View
{
    public partial class Vector2Field : FieldBase
    {
        public Vector2 Value
        {
            get { return (Vector2)GetValue(ValueProperty); }
            set { if (Value != value) { SetValue(ValueProperty, value); } }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Vector2), typeof(Vector2Field),
                new FrameworkPropertyMetadata(
                    Vector2.Zero,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValuePropertyChanged)
                );

        static void OnValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = sender as Vector2Field;
            var currV = s.Value;
            if (s.InputX.Value != currV.X) { s.InputX.Value = currV.X; }
            if (s.InputY.Value != currV.Y) { s.InputY.Value = currV.Y; }
            s.InvokeOnValueChanged(e.OldValue, e.NewValue);
        }

        public float X
        {
            get => Value.X;
            set { if (Value.X != value) { Value = new Vector2(value, Value.Y); } }
        }

        public float Y
        {
            get => Value.Y;
            set { if (Value.Y != value) { Value = new Vector2(Value.X, value); } }
        }

        public Brush FieldForeground
        {
            get { return (Brush)GetValue(FieldForegroundProperty); }
            set { SetValue(FieldForegroundProperty, value); }
        }
        public static readonly DependencyProperty FieldForegroundProperty =
            DependencyProperty.Register("FieldForeground", typeof(Brush), typeof(Vector2Field),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush FieldBackground
        {
            get { return (Brush)GetValue(FieldBackgroundProperty); }
            set { SetValue(FieldBackgroundProperty, value); }
        }
        public static readonly DependencyProperty FieldBackgroundProperty =
            DependencyProperty.Register("FieldBackground", typeof(Brush), typeof(Vector2Field),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Transparent), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush FieldBorderBrush
        {
            get { return (Brush)GetValue(FieldBorderBrushProperty); }
            set { SetValue(FieldBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty FieldBorderBrushProperty =
            DependencyProperty.Register("FieldBorderBrush", typeof(Brush), typeof(Vector2Field),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.DarkGray), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Thickness FieldBorderThickness
        {
            get { return (Thickness)GetValue(FieldBorderThicknessProperty); }
            set { SetValue(FieldBorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty FieldBorderThicknessProperty =
            DependencyProperty.Register("FieldBorderThickness", typeof(Thickness), typeof(Vector2Field),
                new FrameworkPropertyMetadata(new Thickness(1), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public Brush LabelForeground
        {
            get { return (Brush)GetValue(LabelForegroundProperty); }
            set { SetValue(LabelForegroundProperty, value); }
        }
        public static readonly DependencyProperty LabelForegroundProperty =
            DependencyProperty.Register("LabelForeground", typeof(Brush), typeof(Vector2Field),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.DimGray), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush LabelBackground
        {
            get { return (Brush)GetValue(LabelBackgroundProperty); }
            set { SetValue(LabelBackgroundProperty, value); }
        }
        public static readonly DependencyProperty LabelBackgroundProperty =
            DependencyProperty.Register("LabelBackground", typeof(Brush), typeof(Vector2Field),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Transparent), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public Vector2Field()
        {
            InitializeComponent();
        }
    }
}
