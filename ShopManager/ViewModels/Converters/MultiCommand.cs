using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ShopManager.ViewModels.Common;

namespace ShopManager.ViewModels.Converters
{
    public class MultiCommand : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            List<RelayCommand> list = new List<RelayCommand>();
            foreach (var value in values)
            {
                list.Add((RelayCommand)value);
            }

            MultiRelayCommand multi = new MultiRelayCommand(list.ToArray());

            return multi;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
