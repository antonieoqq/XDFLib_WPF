using System.Windows;

namespace XDFLib_WPF.View
{
    public partial class StringField : FieldBase
    {
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { if (Value != value) { SetValue(ValueProperty, value); } }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(StringField),
                new FrameworkPropertyMetadata(
                    "",
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValuePropertyChanged)
                );
        static void OnValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = sender as StringField;
            s.InvokeOnValueChanged(e.OldValue, e.NewValue);
        }

        public bool AcceptsReturn
        {
            get { return (bool)GetValue(AcceptsReturnProperty); }
            set { SetValue(AcceptsReturnProperty, value); }
        }
        public static readonly DependencyProperty AcceptsReturnProperty =
            DependencyProperty.Register("AcceptsReturn", typeof(bool), typeof(StringField),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public TextWrapping Wraping
        {
            get { return (TextWrapping)GetValue(WrapingProperty); }
            set { SetValue(WrapingProperty, value); }
        }
        public static readonly DependencyProperty WrapingProperty =
            DependencyProperty.Register("Wraping", typeof(TextWrapping), typeof(StringField),
                new FrameworkPropertyMetadata(TextWrapping.Wrap, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(StringField),
                new FrameworkPropertyMetadata(TextAlignment.Justify, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public StringField()
        {
            InitializeComponent();
        }

        protected override void TryAssignInput()
        {
            Value = InputBox.Text;
        }
    }
}
