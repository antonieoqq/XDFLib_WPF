using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace XDFLib_WPF.Common
{
    public static class Utilities
    {
        public static void ShowInExplorer(string path)
        {
            path = path.Replace('/', '\\');
            string argument = $"/select, \"{path}\"";
            Process.Start("explorer.exe", argument);
        }

        public static void RenderVisualToPng(Visual visual, string saveToPath, int width, int height, int dpiX = 96, int dpiY = 96)
        {
            RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, dpiX, dpiY, PixelFormats.Pbgra32);
            bmp.Render(visual);
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            using (Stream stm = File.Create(saveToPath))
                encoder.Save(stm);
        }

        public static void InitBitmapImage(BitmapImage src, Uri uri, int? decodeWidth)
        {
            src.BeginInit();
            if (decodeWidth.HasValue)
            {
                src.DecodePixelWidth = decodeWidth.Value;
            }
            src.UriSource = uri;
            src.EndInit();
        }

        public static BitmapImage CreateBitmapImage(Uri uri, int? decodeWidth)
        {
            BitmapImage img = new();
            InitBitmapImage(img, uri, decodeWidth);
            return img;
        }

        public static void MoveFocusToFocusableParent(FrameworkElement element)
        {
            FrameworkElement parent = (FrameworkElement)element.Parent;
            while (parent != null && parent is IInputElement && !((IInputElement)parent).Focusable)
            {
                parent = (FrameworkElement)parent.Parent;
            }

            DependencyObject scope = FocusManager.GetFocusScope(element);
            FocusManager.SetFocusedElement(scope, parent);
        }
    }
}
