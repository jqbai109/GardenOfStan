using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Garden.ViewModel;

namespace Garden.IDAL
{
    public interface IGardenRepository_LoginUser
    {        
        IEnumerable<LoginUserViewModel> GetLoginUser();
    }
}
