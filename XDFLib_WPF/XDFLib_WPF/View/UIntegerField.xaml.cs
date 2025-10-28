using System;
using System.Windows;
using System.Windows.Input;
using XDFLib.Extensions;
using XDFLib_WPF.Common;

namespace XDFLib_WPF.View
{
    /// <summary>
    /// UIntegerField.xaml 的交互逻辑
    /// </summary>
    public partial class UIntegerField : FieldBase
    {
        public uint Value
        {
            get { return (uint)GetValue(ValueProperty); }
            set { if (Value != value) { SetValue(ValueProperty, value); } }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(uint), typeof(UIntegerField),
                new FrameworkPropertyMetadata(
                    0u,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValuePropertyChanged));
        static void OnValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = sender as UIntegerField;
            s.InvokeOnValueChanged(e.OldValue, e.NewValue);
        }

        public UIntegerField()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(this, OnPaste);
        }

        protected override void TryAssignInput()
        {
            if (uint.TryParse(InputBox.Text, out uint v))
            {
                Value = v;
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
            var isUInt = uint.TryParse(newStr, out _);
            e.Handled = !isUInt;
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (uint.TryParse(text, out _))
                {
                    return;
                }
                else
                {
                    var d = text.ToDouble();
                    if (d.HasValue)
                    {
                        var newInt = (uint)Math.Round(d.Value);
                        Value = newInt;
                        return;
                    }
                }
            }
            e.CancelCommand();
        }

    }
}
