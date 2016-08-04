using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Garden.Biz;
using Garden.IDAL;
using Garden.ViewModel;

namespace UnitTestBiz
{
    [TestClass]
    public class UnitTestLoginUserBiz
    {
        [TestMethod]
        public void GetAllLoginUserViewModel()
        {
            LoginUserBiz biz = new LoginUserBiz();

            Assert.AreEqual(null, biz.GetAllLoginUserViewModel());

        }
    }
}
