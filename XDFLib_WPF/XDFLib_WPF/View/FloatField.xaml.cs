using System.Windows;
using System.Windows.Input;
using XDFLib.Extensions;

namespace XDFLib_WPF.View
{
    public partial class FloatField : FieldBase
    {
        public float Value
        {
            get { return (float)GetValue(ValueProperty); }
            set { if (Value != value) { SetValue(ValueProperty, value); } }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(float), typeof(FloatField),
                new FrameworkPropertyMetadata(
                    0f,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValuePropertyChanged)
                );
        static void OnValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = sender as FloatField;
            s.InvokeOnValueChanged(e.OldValue, e.NewValue);
        }

        public FloatField()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(this, OnPaste);
        }

        protected override void TryAssignInput()
        {
            var fv = InputBox.Text.ToFloat();
            if (fv.HasValue)
            {
                Value = fv.Value;
            }
            else
            {
                InputBox.Text = Value.ToString();
            }
        }

        protected override void ExtraPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                TryAssignInput();
                e.Handled = true;
            }
        }
        //protected override void OnPreviewKeyDown(KeyEventArgs e)
        //{
        //    base.OnPreviewKeyDown(e);
        //    if (e.Key == Key.Space)
        //    {
        //        e.Handled = true;
        //    }
        //    else if (e.Key == Key.Enter)
        //    {
        //        TryAssignInput();
        //        e.Handled = true;
        //    }
        //}

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            string newStr;

            if (InputBox.SelectionLength > 0)
            {
                newStr = InputBox.Text.Replace(InputBox.SelectedText, e.Text);
            }
            else
            {
                newStr = InputBox.Text.Insert(InputBox.CaretIndex, e.Text);
            }
            var isFloat = newStr.IsFloat();
            var isSign = newStr == "." || newStr == "-" || newStr == "-.";
            e.Handled = !(isFloat || isSign);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!text.IsFloat())
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        //void TryAssignInput()
        //{
        //    var fv = InputBox.Text.ToFloat();
        //    if (fv.HasValue)
        //    {
        //        Value = fv.Value;
        //    }
        //    else
        //    {
        //        InputBox.Text = Value.ToString();
        //    }
        //}
    }
}
