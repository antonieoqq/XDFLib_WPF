using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace XDFLib_WPF.Common
{
    /// <summary>
    /// 这个类会把对应enum类型的所有值处理成字典
    /// 其中 Key 为 enum 值，Value 为 每个 enum 值的 System.ComponentModel.DescriptionAttribute 的 Description
    /// 在绑定 ComboBox 或 XComboBox 的时候，把 SelectedValue 和 ItemsSource 绑定给同名属性即可
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumBindHelper<T> where T : Enum
    {
        T _selectedValue;
        public T SelectedValue
        {
            get => _selectedValue;
            set
            {
                if (!_selectedValue.Equals(value))
                {
                    _selectedValue = value;
                    OnSelectedItemChanged?.Invoke(_selectedValue);
                }
            }
        }

        Dictionary<T, string> _itemsSource;
        public Dictionary<T, string> ItemsSource
        {
            get
            {
                if (_itemsSource == null) { _itemsSource = BuildEnumComboBoxDictionary(); }
                return _itemsSource;
            }
        }

        public event Action<T> OnSelectedItemChanged;

        public EnumBindHelper() { }

        public EnumBindHelper(T selectedItem) { _selectedValue = selectedItem; }

        public static Dictionary<T, string> BuildEnumComboBoxDictionary()
        {
            var dict = new Dictionary<T, string>();
            foreach (var v in Enum.GetValues(typeof(T)))
            {
                var e = (T)v;
                var descAttr = GetAttr(e);
                var desc = descAttr?.Description ?? e.ToString();
                dict.Add(e, desc);
            }
            return dict;
        }

        private static DescriptionAttribute GetAttr(T e)
        {
            return Attribute.GetCustomAttribute(ForValue(e), typeof(DescriptionAttribute)) as DescriptionAttribute;
        }

        private static MemberInfo ForValue(T e)
        {
            return typeof(T).GetField(Enum.GetName(typeof(T), e));
        }
    }
}
