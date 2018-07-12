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
            Sbr_friends.Text = null;
            LV_fnlst.ItemsSource = App._friendlist;
        }

        private async void LV_fnlst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (LV_fnlst.SelectedItem != null)
            {
                await Navigation.PushAsync(new FriendDetail(LV_fnlst.SelectedItem as Friend));
            }
        }

        private void Sbr_friends_TextChanged(object sender, TextChangedEventArgs e)
        {
            LV_fnlst.BeginRefresh();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                LV_fnlst.ItemsSource = App._friendlist;
            }
            else
            {
                LV_fnlst.ItemsSource = App._friendlist.Where(f => (f.FirstName.ToLower().Contains(e.NewTextValue.ToLower())) | (f.LastName.ToLower().Contains(e.NewTextValue.ToLower())));
            }
            LV_fnlst.EndRefresh();
        }
    }
}