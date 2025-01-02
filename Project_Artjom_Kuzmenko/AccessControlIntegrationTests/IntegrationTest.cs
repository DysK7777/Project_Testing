using AccessControlTests;
using Project_Artjom_Kuzmenko;

namespace AccessControlIntegrationTests
{
    [TestClass]
    public sealed class IntegrationTest
    {
        private IAccessInfoProvider accessInfoProvider;
        private IDateTimeProvider dateTimeProvider;
        private AccessControl accessControl;

        [TestInitialize]
        public void TestInitialize()
        {
            accessInfoProvider = new AccessInfoProviderAPI();
            dateTimeProvider = new DateTimeProvider();
            accessControl = new AccessControl(accessInfoProvider, dateTimeProvider);
        }

        [TestMethod]
        public void GrantAccess_ActiveUserWithinWorkingHours_ReturnsTrue()
        {

            accessInfoProvider.Url = "http://localhost:3000/data/acces?id=12345&name=%22test%22&active=true&exist=true";
            dateTimeProvider.Now = new DateTime(2025, 1, 1, 17, 0, 0);
            var acces = accessControl.GrantAccess("12345");
            Assert.IsTrue(acces);
        }

        [TestMethod]
        public void GrantAccess_InactiveUser_ThrowsUnauthorizedAccessException()
        {
            accessInfoProvider.Url = "http://localhost:3000/data/acces?id=12345&name=%22test%22&active=false&exist=true";
            dateTimeProvider.Now = new DateTime(2025, 1, 1, 17, 0, 0);
            Assert.ThrowsException<UnauthorizedAccessException>(() =>
            {
                accessControl.GrantAccess("12345");
            });
        }

        [TestMethod]
        public void GrantAccess_UnknownAccessCard_ThrowsArgumentException()
        {
            accessInfoProvider.Url = "http://localhost:3000/data/acces?id=12345&name=%22test%22&active=false&exist=false";
            dateTimeProvider.Now = new DateTime(2025, 1, 1, 17, 0, 0);
            Assert.ThrowsException<ArgumentException>(() =>
            {
                accessControl.GrantAccess("12345");
            });
        }

        [TestMethod]
        public void GrantAccess_AccessOutsideWorkingHours_ThrowsUnauthorizedAccessException()
        {
            accessInfoProvider.Url = "http://localhost:3000/data/acces?id=12345&name=%22test%22&active=false&exist=true";
            dateTimeProvider.Now = new DateTime(2025, 1, 1, 20, 0, 0);
            Assert.ThrowsException<UnauthorizedAccessException>(() =>
            {
                accessControl.GrantAccess("12345");
            });
        }
    }
}
