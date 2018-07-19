using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MyLibraryApp
{
    public class PhoneToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name;

            Friend friend = App._friendlist.Where(f => (f.Phone.Contains(value.ToString()))).FirstOrDefault<Friend>();
            name = friend.FirstName + " " + friend.LastName;

            return name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
