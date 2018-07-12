﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyLibraryApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookDetail : ContentPage
	{
        Book book;
        public BookDetail (Book book)
		{
			InitializeComponent ();
            BindingContext = this.book = book;

		}

        private async void Btn_delete_Clicked(object sender, EventArgs e)
        {
            App._booklist.Remove(book);
            //string data = XStorage.SerializeXML<ObservableCollection<Book>>(App._booklist);
            //var res = await XStorage.WriteStorageFile<ObservableCollection<Book>>(data, "books.xml");
            await Navigation.PopAsync();
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            Lbl_avail.Text = book.Availability;
            if (book.Availability == "Yes")
            {
                Btn_action.Text = "Borrow";
            }
            else
            {
                Btn_action.Text = "Return";
            }

            try
            {
                Stream stream = await GetStreamAsync(book.Image);
                Img_book.Source = ImageSource.FromStream(() => stream);
            }
            catch
            {
                return;
            }
        }

        async Task<Stream> GetStreamAsync(string uri)
        {
            TaskFactory factory = new TaskFactory();
            WebRequest request = WebRequest.Create(uri);
            WebResponse response = await factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
            return response.GetResponseStream();
        }

        private async void Btn_action_Clicked(object sender, EventArgs e)
        {
            if(book.Availability == "Yes")
            {
                await Navigation.PushAsync(new BorrowBook(book));
            }
            else
            {
                book.Availability = Lbl_avail.Text = "Yes";
                Btn_action.Text = "Borrow";
            }
        }
    }
}