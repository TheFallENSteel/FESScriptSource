using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    public static class TypeFunctions 
    {
        public static bool IsCompatible(this Type t, Type type) 
        {
            return ((byte)t % 64 == (byte)type % 64);
        }

        public static string DefaultValues(this Type type)
        {
            try
            {
                return values[type];
            }
            catch
            {
                return null;
            }
        }
        private static Dictionary<Graphics.UserControls.SubUserControls.Type, string> values = new Dictionary<Graphics.UserControls.SubUserControls.Type, string>();
        static TypeFunctions()
        {
            values.Add(Graphics.UserControls.SubUserControls.Type.Action, null);
            values.Add(Graphics.UserControls.SubUserControls.Type.Boolean, "false");
            values.Add(Graphics.UserControls.SubUserControls.Type.Console, null);
            values.Add(Graphics.UserControls.SubUserControls.Type.Error, null);
            values.Add(Graphics.UserControls.SubUserControls.Type.Numerical, "0");
            values.Add(Graphics.UserControls.SubUserControls.Type.Textual, @"""""");
            values.Add(Graphics.UserControls.SubUserControls.Type.SubAction, null);
        }
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
        SubAction = 64,
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
            AddToDictionaries(Type.Action, CustomBrushes.Action, CustomColors.Action);
            AddToDictionaries(Type.Textual, CustomBrushes.Textual, CustomColors.Textual);
            AddToDictionaries(Type.Numerical, CustomBrushes.Numerical, CustomColors.Numerical);
            AddToDictionaries(Type.Console, CustomBrushes.Console, CustomColors.Console);
            AddToDictionaries(Type.Boolean, CustomBrushes.Boolean, CustomColors.Boolean);
            AddToDictionaries(Type.SubAction, CustomBrushes.SubAction, CustomColors.SubAction);
        }
        private static void AddToDictionaries(Type type, Brush brush, Color color) 
        {
            TypeToBrush.Add(type, brush);
            TypeToColor.Add(type, color);
        }
    }
}
