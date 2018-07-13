using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyLibraryApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewFriend : ContentPage
	{
        Friend friend;
		public NewFriend ()
		{
			InitializeComponent ();
            BindingContext = friend = new Friend();
		}

        private async void Btn_save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Ent_phone.Text) | string.IsNullOrEmpty(Ent_fname.Text) | string.IsNullOrWhiteSpace(Ent_phone.Text) | string.IsNullOrWhiteSpace(Ent_fname.Text))
            {
                await DisplayAlert("Alert", "Phone and/or First Name are missing.", "OK");
                return;
            }

            friend.Phone = Ent_phone.Text;
            friend.FirstName = Ent_fname.Text;
            friend.LastName = Ent_lname.Text;
            friend.Email = Ent_email.Text;

            App._friendlist.Add(friend);
            //string data = XStorage.SerializeXML<ObservableCollection<Friend>>(App._friendlist);
            //var res = await XStorage.WriteStorageFile<ObservableCollection<Friend>>(data, "friends.xml");
            await Navigation.PopAsync();
        }

        private void Btn_contact_Clicked(object sender, EventArgs e)
        {
            
        }

        private void Btn_populate_Clicked(object sender, EventArgs e)
        {

        }

        private void Btn_clear_Clicked(object sender, EventArgs e)
        {
            Ent_phone.Text = string.Empty;
            Ent_fname.Text = string.Empty;
            Ent_lname.Text = string.Empty;
            Ent_email.Text = string.Empty;
            //Ent_img.Text = string.Empty;
        }
    }
}