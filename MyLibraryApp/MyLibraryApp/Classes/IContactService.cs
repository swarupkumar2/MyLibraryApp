using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibraryApp
{
    public interface IContactService
    {
        List<Friend> GetAllContacts();
    }
}
