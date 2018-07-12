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
	public partial class BooksListPage : ContentPage
	{
		public BooksListPage ()
		{
			InitializeComponent ();
		}

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewBook());
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            LV_bklst.SelectedItem = null;
            LV_bklst.ItemsSource = App._booklist;
        }

        private async void LV_bklst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(LV_bklst.SelectedItem != null)
            {
                await Navigation.PushAsync(new BookDetail(LV_bklst.SelectedItem as Book));
            }
        }
    }
}