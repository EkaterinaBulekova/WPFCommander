using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfCommander
{
    [ValueConversion(typeof(EntryType), typeof(BitmapImage))]
    class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var image = "/Images/file.png";
            switch ((EntryType)value)
            {
                case EntryType.Drive:
                    image = "/Images/drive.png";
                    break;
                case EntryType.Dir:
                    image = "/Images/folder.png";
                    break;
            }

            return new BitmapImage(new Uri($"pack://application:,,,{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
