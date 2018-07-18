using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyLibraryApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailMaster : ContentPage
    {
        public ListView ListView;

        public MasterDetailMaster()
        {
            InitializeComponent();

            BindingContext = new MasterDetailMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailMenuItem> MenuItems { get; set; }
            
            public MasterDetailMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailMenuItem>(new[]
                {
                    new MasterDetailMenuItem { Id = 0, Title = "Home", TargetType = typeof(Welcome) },
                    new MasterDetailMenuItem { Id = 1, Title = "Books", TargetType = typeof(BooksListPage) },
                    new MasterDetailMenuItem { Id = 2, Title = "Friends", TargetType = typeof(FriendsListPage) },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}