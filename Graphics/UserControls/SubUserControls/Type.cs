using System.Collections.Generic;
using System.Windows.Media;
namespace FESScript2.Graphics.UserControls.SubUserControls
{

    /// <summary>
    /// Input output enum.
    /// </summary>

    public enum IO : byte
    { 
        Input = 0,
        Output = 1,
        Error = byte.MaxValue
    }

    /// <summary>
    /// Type enum.
    /// </summary>

    public enum Type : byte
    {
        Action = 0,
        Numerical = 1,
        Textual = 2,
        Console = 3,
        Boolean = 4,
        SubAction = 5,
        Error = byte.MaxValue
    }

    /// <summary>
    /// Binding Types to Colors.
    /// </summary>

    public static class ColorsBrushes
    {
        public static Dictionary<Type, Brush> TypeToBrush = new Dictionary<Type, Brush>();

        public static Dictionary<Type, Color> TypeToColor = new Dictionary<Type, Color>();
        static ColorsBrushes() 
        {
            TypeToBrush.Add(Type.Action,    CustomBrushes.Action);
            TypeToBrush.Add(Type.Textual,   CustomBrushes.Textual);
            TypeToBrush.Add(Type.Numerical, CustomBrushes.Numerical);
            TypeToBrush.Add(Type.Console,   CustomBrushes.Console);
            TypeToBrush.Add(Type.Boolean,   CustomBrushes.Boolean);
            TypeToBrush.Add(Type.SubAction, CustomBrushes.SubAction);

            TypeToColor.Add(Type.Action,    CustomColors.Action);
            TypeToColor.Add(Type.Textual,   CustomColors.Textual);
            TypeToColor.Add(Type.Numerical, CustomColors.Numerical);
            TypeToColor.Add(Type.Console,   CustomColors.Console);
            TypeToColor.Add(Type.Boolean,   CustomColors.Boolean);
            TypeToColor.Add(Type.SubAction, CustomColors.SubAction);
        }
    }
}
