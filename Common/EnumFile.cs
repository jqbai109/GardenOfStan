using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromolinkUS.Common
{
    public enum SendBy
    {
        WebSite = 1,
        Job = 2
    }
    public enum ApproveFlowNode
    {

        IR = 3,  //user type
        Ops = 2,
        Admin = 1
    }
    public enum ApproveStatus
    {
        N = 0,
        Y = 1
    }
}
