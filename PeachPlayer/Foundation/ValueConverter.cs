using Avalonia.Data.Converters;
using System;
using System.Globalization;


namespace PeachPlayer.Foundation
{
    /// <summary>
    /// 数字转bool(默认：1等于true，需要其他转换通过参数传递，值等于参数值返回true)
    /// </summary>
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                int gender = value == null ? 0 : (int)value;
                return gender == 1;
            }
            else
            {
                int.TryParse(parameter?.ToString(), out int par);
                int gender = value == null ? 0 : (int)value;
                return gender == par;
            }
        }
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0;
            }
            return ((bool)value) ? 1 : 0;
        }
    }
}
