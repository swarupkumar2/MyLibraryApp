using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyLibraryApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FriendDetail : ContentPage
	{
        Friend friend;
		public FriendDetail (Friend friend)
		{
			InitializeComponent ();
            BindingContext = this.friend = friend;
		}

        private async void Btn_delete_Clicked(object sender, EventArgs e)
        {
            App._friendlist.Remove(friend);
            //string data = XStorage.SerializeXML<ObservableCollection<Friend>>(App._friendlist);
            //var res = await XStorage.WriteStorageFile<ObservableCollection<Friend>>(data, "friends.xml");
            await Navigation.PopAsync();
        }
    }
}