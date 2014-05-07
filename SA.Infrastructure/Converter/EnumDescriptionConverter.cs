using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace SA.Infrastructure.Converter
{

    public class EnumDescriptionConverter : IValueConverter
    {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException();
            
            if (!(value is Enum))
                throw new ArgumentException("Tipo de parâmetro inválido para conveter um 'Enum' numa descrição!");

            return ((Enum)value).GetDescription();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
