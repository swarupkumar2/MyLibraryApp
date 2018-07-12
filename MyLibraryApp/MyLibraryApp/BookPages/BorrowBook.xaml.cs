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
	public partial class BorrowBook : ContentPage
	{
        Book book;
		public BorrowBook (Book book)
		{
			InitializeComponent ();
            BindingContext = this.book = book;
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            book.Availability = "No";
            await Navigation.PopAsync();
        }
    }
}