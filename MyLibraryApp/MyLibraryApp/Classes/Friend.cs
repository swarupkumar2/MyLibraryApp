using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyLibraryApp
{
    public class Friend
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public ObservableCollection<string> BookList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> History { get; set; } = new ObservableCollection<string>();
    }
}
