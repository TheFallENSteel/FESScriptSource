using FESScript.Graphics;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;
using FESScript.CodeWorks.BlockCreation.Blocks.Placements;
using System.Windows;
using System;

namespace FESScript
{
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
            return t == type || compatibleTypes.Contains((t, type)) || compatibleTypes.Contains((type, t));
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
        private static readonly List<(Type, Type)> compatibleTypes = new List<(Type, Type)>() 
        { 
            (Type.Action, Type.SubAction)
        };
        private static readonly Dictionary<Type, string> values = new Dictionary<Type, string>()
        {
            { Type.Action, null },
            { Type.Boolean, "false" },
            { Type.Console, null },
            { Type.Error, null },
            { Type.Numerical, "0" },
            { Type.Textual, @"""""" },
            { Type.SubAction, null },

        };
    }

    public static class ColorsBrushes
    {
        public static Dictionary<Type, Brush> TypeToBrush = new Dictionary<Type, Brush>();

        public static Dictionary<Type, Color> TypeToColor = new Dictionary<Type, Color>();
        static ColorsBrushes()
        {
            AddToDictionaries(Type.Error, CustomBrushes.Error, CustomColors.Error);
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
    public class TypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return ColorsBrushes.TypeToBrush[(Type)value];
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
            //return ColorsBrushes.TypeToBrush.First((x) => x.Value == (Brush)value);
        }
    }
    public class IOFilter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return ((List<DotPlacement>)value).FindAll(dot => dot.DotTemplate.IO == (IO)parameter).Select(dotPlacement => dotPlacement.Dot);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
