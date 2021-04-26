using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DigitalEdge.Domain;
using DigitalEdge.Repository;
using DigitalEdge.Services;

namespace DigitalEdgeTest
{
    [TestClass]
    public class AccountServiceTest
    {

        private IOptions<AppSettings> OptionsValue()
        {
            AppSettings aSettings = new AppSettings() { Secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING" };
            IOptions<AppSettings> options = Options.Create(aSettings);
            return (options);
        }
        
        [TestMethod]
        public void TestLogin()
        {
            var mock = new Mock<IAccountRepository>();
            mock.Setup(x => x.GetLogin("sheffy@gmail.com", "sheffy"))
               .Returns(new Users
                    (34, "sheffy", false, "Sheffy", "Kalra", "sheffy@gmail.com", "983172808", 0, false, false));
                  
            var accountService = new AccountService(mock.Object, OptionsValue());
            var result = accountService.ValidateUser("sheffy@gmail.com", "sheffy");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLogin_Failed()
        {
            var mock = new Mock<IAccountRepository>();
            mock.Setup(x => x.GetLogin("sheffy@gmail.com", "sheffy123"))
               .Returns(new Users
                    (34, "sheffy", false, "Sheffy", "Kalra", "sheffy@gmail.com", "983172808", 0, false, false));

            var accountService = new AccountService(mock.Object, OptionsValue());
            var result = accountService.ValidateUser("sheffy@gmail.com", "sheffy");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestGetToken()
        {
            var mock = new Mock<IAccountRepository>();
            mock.Setup(x => x.GetRoleName(1))
               .Returns("Admin");
            var accountService = new AccountService(mock.Object, OptionsValue());
            var user = new UserModel
            {
                RoleId = 1,
                FirstName = "User"
            };
            var result = accountService.GetToken(user);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetTokenExpectedNull()
        {
            var mock = new Mock<IAccountRepository>();
            var accountService = new AccountService(mock.Object, OptionsValue());
            var user = new UserModel();
            var result = accountService.GetToken(user);
            Assert.IsNull(result);
        }
    }
}
