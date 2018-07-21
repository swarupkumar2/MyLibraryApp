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
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using MyLibraryApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(ContactService))]

namespace MyLibraryApp.Droid
{
    public class ContactService : MainActivity,  IContactService
    {
        static readonly int REQUEST_CONTACTS = 1;
        static string[] PERMISSIONS_CONTACT = {
            Manifest.Permission.ReadContacts    
        };

        List<Phonebook> phoneContacts = new List<Phonebook>();

        public List<Phonebook> GetAllContacts()
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                GetPhoneContacts();
            }
            else
            {
                CheckPhonebookPermission();
            }

            return phoneContacts;
        }

        private void CheckPhonebookPermission()
        {
            if (ActivityCompat.CheckSelfPermission(Android.App.Application.Context, PERMISSIONS_CONTACT[0]) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions((Activity)Forms.Context, PERMISSIONS_CONTACT, REQUEST_CONTACTS);
            }
            else
            {
                GetPhoneContacts();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == REQUEST_CONTACTS)
            {
                if (grantResults.Length == 1 && grantResults[0] == Permission.Granted)
                {
                    GetPhoneContacts();
                }
                else
                {
                    return;
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }


        private void GetPhoneContacts()
        {
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
                                    contact.LastName = "";
                                contact.Phone = phoneNumber;
                                contact.DisplayName = name;
                                phoneContacts.Add(contact);
                            }
                            catch
                            {
                                return;
                            }
                        }
                        phones.Close();
                    }
                }
            }
            catch
            {
                return;
            }

            return;
        }

    }
}