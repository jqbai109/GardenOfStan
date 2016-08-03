using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden.DAL
{
    public partial class GardenRepository
    {
        public IEnumerable<LoginUser> GetLoginUser()
        {
            GardenEntities context = new GardenEntities();

            return context.LoginUser.Where(t => t.status == "A");        
        }
    
    }
}
