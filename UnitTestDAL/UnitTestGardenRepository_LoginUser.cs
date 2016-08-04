using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Garden.DAL;
using System.Collections.Generic;
using System.Linq;
using Garden.ViewModel;

namespace UnitTestDAL
{
    [TestClass]
    public class UnitTestGardenRepository_LoginUser
    {
        [TestMethod]
        public void TestGetLoginUser()
        {
            GardenRepository_LoginUser reposition = new GardenRepository_LoginUser();
            List<LoginUserViewModel> UserList = reposition.GetLoginUser().ToList();
            Assert.AreEqual(0, UserList.Count);
        }
    }
}
