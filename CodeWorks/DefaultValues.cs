using System;
using System.Collections.Generic;
using System.Text;
using FESScript2.Graphics.UserControls.SubUserControls;

namespace FESScript2.CodeWorks
{
    public static class DefaultValues
    {
        public static string Values(Graphics.UserControls.SubUserControls.Type type) 
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
        static DefaultValues() 
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
}
