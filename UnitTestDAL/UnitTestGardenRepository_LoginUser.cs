using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Garden.DAL;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestDAL
{
    [TestClass]
    public class UnitTestGardenRepository_LoginUser
    {
        [TestMethod]
        public void TestGetLoginUser()
        {
            GardenRepository reposition = new GardenRepository();
            List<LoginUser> UserList = reposition.GetLoginUser().ToList();
            Assert.AreEqual(0, UserList.Count);
        }
    }
}
