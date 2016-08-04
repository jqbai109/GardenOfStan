using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Garden.ViewModel;
using Garden.IDAL;
namespace Garden.DAL
{
    public class GardenRepository_LoginUser : IGardenRepository_LoginUser
    {
        public IEnumerable<LoginUserViewModel> GetLoginUser()
        {
            GardenEntities context = new GardenEntities();

            return context.LoginUser.Where(t => t.status == "A").Select(a => new LoginUserViewModel {FullName=a.firstName+a.lastName,LoginUserID=a.loginID,LoginUserName=a.loginName,Mail=a.email });        
        }
    
    }
}
