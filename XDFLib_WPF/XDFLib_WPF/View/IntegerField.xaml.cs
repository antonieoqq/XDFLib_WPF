using System;
using System.Windows;
using System.Windows.Input;
using XDFLib.Extensions;
using XDFLib_WPF.Common;

namespace XDFLib_WPF.View
{
    public partial class IntegerField : FieldBase
    {
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { if (Value != value) { SetValue(ValueProperty, value); } }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(IntegerField),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValuePropertyChanged)
                );
        static void OnValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = sender as IntegerField;
            s.InvokeOnValueChanged(e.OldValue, e.NewValue);
        }

        public IntegerField()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(this, OnPaste);
        }

        protected override void TryAssignInput()
        {
            var iv = InputBox.Text.ToInteger();
            if (iv.HasValue)
            {
                Value = iv.Value;
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
                Utilities.MoveFocusToFocusableParent(this);
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
        //        Utilities.MoveFocusToFocusableParent(this);
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
            var isInt = newStr.IsInteger();
            var isSign = newStr == "-";
            e.Handled = !(isInt || isSign);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (text.IsInteger())
                {
                    return;
                }
                else
                {
                    var d = text.ToDouble();
                    if (d.HasValue)
                    {
                        var newInt = (int)Math.Round(d.Value);
                        Value = newInt;
                        return;
                    }
                }
            }
            e.CancelCommand();
        }
    }
}
