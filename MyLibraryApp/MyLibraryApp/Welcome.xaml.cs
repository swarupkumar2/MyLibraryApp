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
        public int AvailableBookCount { get; set; } = 0;

        public Welcome()
        {
            InitializeComponent();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            foreach (Book book in App._booklist)
            {
                if (book.Availability == "Yes")
                    AvailableBookCount++;
            }

            Lbl_avlbkcnt.Text = AvailableBookCount.ToString();
            Lbl_ttlbkcnt.Text = App._booklist.Count.ToString();
            Lbl_ttlfncnt.Text = App._friendlist.Count.ToString();
        }

    }
}