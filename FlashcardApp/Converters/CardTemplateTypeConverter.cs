using FlashcardApp.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FlashcardApp.WPF.Converters;

public class CardTemplateTypeConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CardTemplateType templateType)
        {
            return GetString(templateType);
        }

        return null;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string s)
        {
            return Enum.Parse(typeof(CardTemplateType), s);
        }

        return null;
    }

    public string[] Strings => GetStrings();

    public static string GetString(CardTemplateType templateType) => templateType.ToString();
    public static string[] GetStrings()
    {
        List<string> list = new List<string>();

        foreach (CardTemplateType templateType in Enum.GetValues(typeof(CardTemplateType)))
        {
            list.Add(GetString(templateType));
        }

        return list.ToArray();
    }
}
