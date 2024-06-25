using System.Windows.Media;

namespace FESScript2.Graphics
{
    public static class CustomBrushes
    {
        public static SolidColorBrush OrangeMenu1   => new SolidColorBrush(CustomColors.OrangeMenu1);
        public static SolidColorBrush OrangeMenu2   => new SolidColorBrush(CustomColors.OrangeMenu2);
        public static SolidColorBrush StrokesBlock1 => new SolidColorBrush(CustomColors.StrokesBlock1);
        public static SolidColorBrush Action        => new SolidColorBrush(CustomColors.Action);
        public static SolidColorBrush Textual       => new SolidColorBrush(CustomColors.Textual);
        public static SolidColorBrush Numerical     => new SolidColorBrush(CustomColors.Numerical);
        public static SolidColorBrush Console       => new SolidColorBrush(CustomColors.Console);
        public static SolidColorBrush Boolean       => new SolidColorBrush(CustomColors.Boolean);
        public static SolidColorBrush TopBar        => new SolidColorBrush(CustomColors.TopBar);
        public static SolidColorBrush LeftBar       => new SolidColorBrush(CustomColors.LeftBar);
        public static SolidColorBrush SubAction     => new SolidColorBrush(CustomColors.SubAction);
    }
    public static class CustomColors
    {
        public static Color OrangeMenu1     => new Color() { R = 255, G = 178, B = 38, A = 255 };
        public static Color OrangeMenu2     => new Color() { R = 255, G = 183, B = 51, A = 255 };
        public static Color StrokesBlock1   => Colors.Gray ;
        public static Color Action          => Colors.Peru;
        public static Color Textual         => Colors.OrangeRed;
        public static Color Numerical       => Colors.LimeGreen;
        public static Color Console         => Colors.DarkGreen;
        public static Color Boolean         => Colors.Blue;
        public static Color TopBar          => Colors.DarkOrange;
        public static Color LeftBar         => Colors.Orange;
        public static Color SubAction       => Colors.Peru;
    }
}
