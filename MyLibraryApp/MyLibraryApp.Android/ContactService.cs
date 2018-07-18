using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using MyLibraryApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(ContactService))]

namespace MyLibraryApp.Droid
{
    public class ContactService :  IContactService
    {
        public List<Phonebook> GetAllContacts()
        {
            var phoneContacts = new List<Phonebook>();

            try
            {
                using (var phones = Android.App.Application.Context.ContentResolver.Query(ContactsContract.CommonDataKinds.Phone.ContentUri, null, null, null, null))
                {
                    if (phones != null)
                    {
                        while (phones.MoveToNext())
                        {
                            try
                            {
                                string name = phones.GetString(phones.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.DisplayName));
                                string phoneNumber = phones.GetString(phones.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number));

                                string[] words = name.Split(' ');
                                var contact = new Phonebook();
                                contact.FirstName = words[0];
                                if (words.Length > 1)
                                    contact.LastName = words[1];
                                else
                                    contact.LastName = ""; //no last name
                                contact.Phone = phoneNumber;
                                contact.DisplayName = name;
                                phoneContacts.Add(contact);
                            }
                            catch
                            {
                                return phoneContacts;
                            }
                        }
                        phones.Close();
                    }
                }
            }
            catch
            {
                return phoneContacts;
            }

            return phoneContacts;
        }

    }
}