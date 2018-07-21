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
        List<Phonebook> contacts = new List<Phonebook>();

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

        private void Btn_clear_Clicked(object sender, EventArgs e)
        {
            Ent_phone.Text = string.Empty;
            Ent_fname.Text = string.Empty;
            Ent_lname.Text = string.Empty;
            Ent_email.Text = string.Empty;
            Sbr_phonebook.Text = string.Empty;
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            contacts = DependencyService.Get<IContactService>().GetAllContacts();
            Slt_contacts.IsVisible = false;
        }

        private void Sbr_phonebook_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                Slt_contacts.IsVisible = false;
                return;
            }
            Slt_contacts.IsVisible = true;
            Lvw_contacts.ItemsSource = contacts.Where(p => (p.DisplayName.ToLower().Contains(e.NewTextValue.ToLower())));
            var list = contacts.Where(p => (p.DisplayName.ToLower().Contains(e.NewTextValue.ToLower()))).FirstOrDefault();
            if (list == null)
            {
                Slt_contacts.IsVisible = false;
            }
        }

        private void Lvw_contacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var localfriend = Lvw_contacts.SelectedItem as Phonebook;
            Ent_phone.Text = localfriend.Phone;
            Ent_fname.Text = localfriend.FirstName;
            Ent_lname.Text = localfriend.LastName;

            Slt_contacts.IsVisible = false;
        }

    }
}