using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyLibraryApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FriendsListPage : ContentPage
	{
		public FriendsListPage ()
		{
			InitializeComponent ();
		}

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewFriend());
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            LV_fnlst.SelectedItem = null;
            LV_fnlst.ItemsSource = App._friendlist;
        }

        private async void LV_fnlst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (LV_fnlst.SelectedItem != null)
            {
                await Navigation.PushAsync(new FriendDetail(LV_fnlst.SelectedItem as Friend));
            }
        }
    }
}