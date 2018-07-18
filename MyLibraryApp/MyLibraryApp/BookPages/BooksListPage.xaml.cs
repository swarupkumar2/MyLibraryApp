using System;
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
            Sbr_books.Text = null;
            LV_bklst.ItemsSource = App._booklist;
            Btn_brwdlst.Text = "Borrowed books";
            Btn_duelst.IsVisible = true;
            Sbr_books.IsVisible = true;
            Lbl_comment.IsVisible = false;
        }

        private async void LV_bklst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(LV_bklst.SelectedItem != null)
            {
                await Navigation.PushAsync(new BookDetail(LV_bklst.SelectedItem as Book));
            }
        }

        private void Sbr_books_TextChanged(object sender, TextChangedEventArgs e)
        {
            LV_bklst.BeginRefresh();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                LV_bklst.ItemsSource = App._booklist;
            }
            else
            {
                LV_bklst.ItemsSource = App._booklist.Where(b => (b.Title.ToLower().Contains(e.NewTextValue.ToLower())) | (b.Author.ToLower().Contains(e.NewTextValue.ToLower())));
            }
            LV_bklst.EndRefresh();
        }

        private void Btn_brwdlst_Clicked(object sender, EventArgs e)
        {
            LV_bklst.BeginRefresh();
            if (Btn_brwdlst.Text == "Borrowed books")
            {
                LV_bklst.ItemsSource = App._booklist.Where(b => (b.Availability.ToLower().Contains("no")));
                Btn_brwdlst.Text = "Show all books";
                Btn_duelst.IsVisible = false;
                Sbr_books.IsVisible = false;

                Lbl_comment.IsVisible = true;
                Lbl_comment.Text = "List of borrowed books:";
            }
            else
            {
                LV_bklst.ItemsSource = App._booklist;
                Btn_brwdlst.Text = "Borrowed books";
                Btn_duelst.IsVisible = true;
                Sbr_books.IsVisible = true;
                Lbl_comment.IsVisible = false;
            }
            LV_bklst.EndRefresh();
        }

        private void Btn_duelst_Clicked(object sender, EventArgs e)
        {
            Btn_brwdlst.Text = "Show all books";
            Btn_duelst.IsVisible = false;
            Sbr_books.IsVisible = false;

            Lbl_comment.IsVisible = true;
            Lbl_comment.Text = "List of overdue books:";

            DateTime now = DateTime.Parse(DateTime.Now.ToLongDateString());
            LV_bklst.ItemsSource = App._booklist.Where(b => (b.Availability.ToLower().Contains("no"))).Where(b => (DateTime.Parse(b.ReturnDate).CompareTo(now) == -1));
            
        }
    }
}