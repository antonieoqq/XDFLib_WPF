using System;
using System.Windows;
using System.Windows.Controls;

namespace XDFLib_WPF.View
{
    public partial class EnumerateField : FieldBase
    {
        public Enum Value
        {
            get { return (Enum)GetValue(ValueProperty); }
            set
            {
                if (value != null && Value != value)
                {
                    //var needRefresh = Value == null || Value.GetType() != value.GetType();
                    SetValue(ValueProperty, value);
                    Combo.SelectedItem = Value;
                }
            }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Enum), typeof(EnumerateField),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValuePropertyChanged)
                );
        static void OnValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = sender as EnumerateField;
            s.InvokeOnValueChanged(e.OldValue, e.NewValue);
            s.RefreshComboBox(e.NewValue as Enum);
        }

        public EnumerateField()
        {
            InitializeComponent();
            Combo.SelectionChanged += OnEnumSelect;
        }

        public EnumerateField(Enum value)
        {
            InitializeComponent();
            Combo.SelectionChanged += OnEnumSelect;
            RefreshComboBox(Value);

            Value = value;
        }

        private void OnEnumSelect(object sender, SelectionChangedEventArgs e)
        {
            Value = Combo.SelectedItem as Enum;
        }

        public void RefreshComboBox(Enum v)
        {
            if (v != null)
            {
                Combo.ItemsSource = Enum.GetValues(v.GetType());
                Combo.SelectedItem = v;
            }
            else
            {
                Combo.ItemsSource = null;
                Combo.SelectedItem = null;
            }
        }
    }
}
