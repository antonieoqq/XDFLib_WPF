using System;
using System.Windows.Controls;
using System.Windows.Input;
using XDFLib_WPF.Common;

namespace XDFLib_WPF.View
{
    public class FieldBase : UserControl
    {
        public event Action<object, object> OnValueChanged;
        public void InvokeOnValueChanged(object oldValue, object newValue)
        {
            OnValueChanged?.Invoke(oldValue, newValue);
        }

        protected virtual void TryAssignInput() { }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            TryAssignInput();
        }

        protected override sealed void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            var isCtrlPressed = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
            var moveFocus = e.Key == Key.Escape || (isCtrlPressed && e.Key == Key.Enter);
            if (moveFocus)
            {
                Utilities.MoveFocusToFocusableParent(this);
                e.Handled = true;
            }
            else
            {
                ExtraPreviewKeyDown(e);
            }
        }
        protected virtual void ExtraPreviewKeyDown(KeyEventArgs e) { }

    }
}
