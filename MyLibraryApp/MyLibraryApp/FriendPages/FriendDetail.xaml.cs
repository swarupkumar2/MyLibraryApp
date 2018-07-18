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
            Sly_editable.IsEnabled = false;
            Btn_action.Text = "Edit";
            Sly_label.IsVisible = true;
            Sly_entry.IsVisible = false;

            this.Title = friend.FirstName + " " + friend.LastName;
        }

        private async void Btn_delete_Clicked(object sender, EventArgs e)
        {
            App._friendlist.Remove(friend);
            //string data = XStorage.SerializeXML<ObservableCollection<Friend>>(App._friendlist);
            //var res = await XStorage.WriteStorageFile<ObservableCollection<Friend>>(data, "friends.xml");
            await Navigation.PopAsync();
        }

        private void Btn_action_Clicked(object sender, EventArgs e)
        {
            if(Sly_editable.IsEnabled == false)
            {
                Sly_editable.IsEnabled = true;
                Btn_action.Text = "Save";
                Sly_label.IsVisible = false;
                Sly_entry.IsVisible = true;
                Ent_phone.Text = friend.Phone;
                Ent_email.Text = friend.Email;
            }
            else
            {
                Lbl_phone.Text = friend.Phone = Ent_phone.Text;
                Lbl_email.Text = friend.Email = Ent_email.Text;
                Sly_editable.IsEnabled = false;
                Btn_action.Text = "Edit";
                Sly_label.IsVisible = true;
                Sly_entry.IsVisible = false;
            }
            
            //await Navigation.PushAsync(new EditFriend(friend));
        }
        
    }
}