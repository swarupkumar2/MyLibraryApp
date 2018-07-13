using Goodreads;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace MyLibraryApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewBook : ContentPage
	{
        const string apiKey = "aDD53NT7GVzS6uNLoRdVmA";
        const string apiSecret = "hILFNA5yplWNGQf7fCfLp1I2qnmH1I7LTWIK8NEKbg";

        Book book;
		public NewBook ()
		{
			InitializeComponent ();
            BindingContext = book = new Book();
		}

        private async void Btn_save_Clicked(object sender, EventArgs e)
        {
            book.Isbn = Ent_isbn.Text;
            book.Title = Ent_title.Text;
            book.Author = Ent_author.Text;
            book.Description = Ent_desc.Text;
            book.Image = Ent_img.Text;
            book.Availability = "Yes";

            App._booklist.Add(book);
            //string data = XStorage.SerializeXML<ObservableCollection<Book>>(App._booklist);
            //var res = await XStorage.WriteStorageFile<ObservableCollection<Book>>(data, "books.xml");
            await Navigation.PopAsync();
        }

        private async void Btn_scan_Clicked(object sender, EventArgs e)
        {
            var scanpage = new ZXingScannerPage();
            await Navigation.PushAsync(scanpage);
            scanpage.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    Ent_isbn.Text = result.Text;
                });
            };
        }

        private async Task Btn_populate_Clicked(object sender, EventArgs e)
        {
            if(Ent_isbn.Text == string.Empty | Ent_isbn.Text == null)
            {
                await DisplayAlert("Warning", "Please enter book ISBN", "OK");
                return;
            }

            try
            {
                var client = GoodreadsClient.Create(apiKey, apiSecret);
                var localbook = await client.Books.GetByIsbn(Ent_isbn.Text);
                Ent_title.Text = localbook.Title;

                //---------Convert HTML to plain Text for Description-------------
                var pageContent = localbook.Description;
                var pageDoc = new HtmlDocument();
                pageDoc.LoadHtml(pageContent);
                Ent_desc.Text = pageDoc.DocumentNode.InnerText;
                //----------------------

                //Ent_desc.Text = localbook.Description;
                Ent_img.Text = localbook.ImageUrl;

                string tempAuth = "";
                foreach(var author in localbook.Authors)
                {
                    tempAuth = tempAuth + author.Name + ", ";
                }
                Ent_author.Text = tempAuth.Trim().Trim(',');
            }
            catch
            {
                await DisplayAlert("Error", "Error in fetching data, Enter manually.", "OK");
            }           
        }
    }
}