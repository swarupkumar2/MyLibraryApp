using RestSharp;
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
    public partial class Welcome : ContentPage
    {
        public int BorrowedBookCount { get; set; } = 0;

        public Welcome()
        {
            InitializeComponent();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            Lbl_welcomeTxt.Text = "Welcome to My Library.\n\nAn application where you can organise your books and keep a record of who has borrowed it from you and when they are due for returning.";
            foreach (Book book in App._booklist)
            {
                if (book.Availability == "No")
                    BorrowedBookCount++;
            }

            Lbl_avlbkcnt.Text = BorrowedBookCount.ToString();
            Lbl_ttlbkcnt.Text = App._booklist.Count.ToString();
            Lbl_ttlfncnt.Text = App._friendlist.Count.ToString();
        }
        
    }
}