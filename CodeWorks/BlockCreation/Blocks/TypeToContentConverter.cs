using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using FESScript.CodeWorks.BlockCreation.Blocks.Templates;

namespace FESScript.CodeWorks.BlockCreation.Blocks
{
    public class TypeToContentConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            ContentTemplate contentData = (ContentTemplate)value;
            if (contentData.Type != null) return (UserControl)Activator.CreateInstance(contentData.Type, new object[] { contentData });
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
