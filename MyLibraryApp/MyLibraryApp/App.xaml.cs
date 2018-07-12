using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MyLibraryApp
{
	public partial class App : Application
	{
        public static ObservableCollection<Book> _booklist = new ObservableCollection<Book>();
        public static ObservableCollection<Friend> _friendlist = new ObservableCollection<Friend>();

        public App ()
		{
			InitializeComponent();

			MainPage = new MasterDetail();
		}

		protected async override void OnStart ()
		{
            string bookdata = await XStorage.ReadStorageFile("books.xml");
            if (XStorage.DeserializeXML<ObservableCollection<Book>>(bookdata) != null)
            {
                _booklist = await XStorage.DeserializeXML<ObservableCollection<Book>>(bookdata);
            }

            string frienddata = await XStorage.ReadStorageFile("friends.xml");
            if (XStorage.DeserializeXML<ObservableCollection<Friend>>(frienddata) != null)
            {                
                _friendlist = await XStorage.DeserializeXML<ObservableCollection<Friend>>(frienddata);
            }
            // Handle when your app starts
        }

        protected async override void OnSleep ()
		{
            string bdata = XStorage.SerializeXML<ObservableCollection<Book>>(App._booklist);
            await XStorage.WriteStorageFile<ObservableCollection<Book>>(bdata, "books.xml");

            string fdata = XStorage.SerializeXML<ObservableCollection<Friend>>(App._friendlist);
            await XStorage.WriteStorageFile<ObservableCollection<Friend>>(fdata, "friends.xml");
            // Handle when your app sleeps
        }

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

    }
}
