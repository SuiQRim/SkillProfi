using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System;
using System.Diagnostics;

namespace SkillProfiWPF.ViewModels.Converters
{
    public class Base64ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = value as string;

            if (s == null)
                return null;

            BitmapImage bi = new();

            bi.BeginInit();
            bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(s));
            bi.EndInit();

            return bi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}