using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryApp
{

    public class MasterDetailMenuItem
    {
        public MasterDetailMenuItem()
        {
            //TargetType = typeof(Welcome);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}