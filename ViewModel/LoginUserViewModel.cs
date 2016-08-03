using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGarden.ViewModel
{
    public class LoginUserViewModel
    {
        public Nullable<int> LoginUserID { set; get; }
        public string LoginUserName { set; get; }
        public string Mail { set; get; }
        public string FullName { set; get; }
    }
}

