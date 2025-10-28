using System.Windows.Media;

namespace XDFLib_WPF.Common
{
    public class ColorHandler
    {
        public Color CurrColor;

        public ColorHandler(string colorCode)
        {
            CurrColor = FromString(colorCode);
        }

        public static Color FromString(string colorCode)
        {
            var colorObj = ColorConverter.ConvertFromString(colorCode);
            if (colorObj != null)
            {
                return (Color)colorObj;
            }
            else
            {
                return Color.FromArgb(0, 0, 0, 0);
            }
        }

        public static implicit operator Color(ColorHandler ch)
        {
            return ch.CurrColor;
        }
    }
}
