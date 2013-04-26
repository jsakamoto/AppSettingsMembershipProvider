using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Security;

namespace AppSettingsMembershipProvider.Test
{
    [TestClass]
    public class AppSettingsMembershipProviderTest
    {
        [TestMethod]
        public void MissingAlgorithmValidateTest()
        {
            Membership.ValidateUser("Peter", "London").Is(true);
        }

        [TestMethod]
        public void Md5WithoutSaltValidateTest()
        {
            Membership.ValidateUser("Mike", "NewDelhi").Is(true);
        }

        [TestMethod]
        public void Md5WithSaltValidateTest()
        {
            Membership.ValidateUser("Steve", "Tokyo").Is(true);
        }

        [TestMethod]
        public void InvalidPasswordValidateTest()
        {
            Membership.ValidateUser("Peter", "NewYork").Is(false);
        }

        [TestMethod]
        public void NotExistUserValidateTest()
        {
            Membership.ValidateUser("Anna", "Brussels").Is(false);
        }

        [TestMethod]
        public void GetUserTest()
        {
            var user = Membership.GetUser("Peter");
            user.UserName.Is("Peter");
            user.IsApproved.Is(true);
            user.IsLockedOut.Is(false);
        }

        [TestMethod]
        public void GetNoExistUserTest()
        {
            Membership.GetUser("Anna").IsNull();
        }
    }
}
