﻿using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace CutCode.CrossPlatform.Converters
{
    public class StringToGeometryConverter : IValueConverter
    {
        public static StringToGeometryConverter Instance = new StringToGeometryConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is string rawString && targetType.IsAssignableFrom(typeof(Geometry)))
            {
                return Geometry.Parse(rawString);
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}