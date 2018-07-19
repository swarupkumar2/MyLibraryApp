using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MyLibraryApp
{
    public class IsbnToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<string> titleList = new ObservableCollection<string>();

            foreach (string element in (ObservableCollection<string>)value)
            {
                Book book = (from b in App._booklist where b.Isbn.Contains(element) select b).FirstOrDefault<Book>();
                if(book != null)
                {
                    titleList.Add(book.Title);
                }
            }

            return titleList;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
