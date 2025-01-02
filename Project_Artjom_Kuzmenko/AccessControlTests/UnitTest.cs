using Moq;
using Project_Artjom_Kuzmenko;

namespace AccessControlTests
{
    [TestClass]
    public sealed class UnitTest
    {
        [TestMethod]
        public void GrantAccess_ActiveUserWithinWorkingHours_ReturnsTrue()
        {
            var mockInfoProvider = new Mock<IAccessInfoProvider>();
            mockInfoProvider.Setup(p => p.GetUserInfo("12345")).Returns(new UserInfo
            {
                AccessCardId = "12345",
                Name = "John Doe",
                IsActive = true
            });

            var mockTime = new Mock<IDateTimeProvider>();
            mockTime.Setup(p => p.Now).Returns(new DateTime(2024, 1, 1, 10, 0, 0));

            var accessControl = new AccessControl(mockInfoProvider.Object, mockTime.Object);

            var result = accessControl.GrantAccess("12345");

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void GrantAccess_UnknownAccessCard_ThrowsArgumentException()
        {
            var mockInfoProvider = new Mock<IAccessInfoProvider>();
            mockInfoProvider.Setup(p => p.GetUserInfo("12345")).Returns((UserInfo)null);

            var mockTime = new Mock<IDateTimeProvider>();
            mockTime.Setup(p => p.Now).Returns(new DateTime(2024, 1, 1, 10, 0, 0));

            var accessControl = new AccessControl(mockInfoProvider.Object, mockTime.Object);

            Assert.ThrowsException<ArgumentException>(() => accessControl.GrantAccess("12345"));
        }

        [TestMethod]
        public void GrantAccess_InactiveUser_ThrowsUnauthorizedAccessException()
        {
            var mockInfoProvider = new Mock<IAccessInfoProvider>();
            mockInfoProvider.Setup(p => p.GetUserInfo("12345")).Returns(new UserInfo
            {
                AccessCardId = "12345",
                Name = "Jane Doe",
                IsActive = false
            });

            var mockTime = new Mock<IDateTimeProvider>();
            mockTime.Setup(p => p.Now).Returns(new DateTime(2024, 1, 1, 10, 0, 0));

            var accessControl = new AccessControl(mockInfoProvider.Object, mockTime.Object);

            Assert.ThrowsException<UnauthorizedAccessException>(() => accessControl.GrantAccess("12345"));
        }

        [TestMethod]
        public void GrantAccess_AccessOutsideWorkingHours_ThrowsUnauthorizedAccessException()
        {

            var mockInfoProvider = new Mock<IAccessInfoProvider>();
            mockInfoProvider.Setup(p => p.GetUserInfo("12345")).Returns(new UserInfo
            {
                AccessCardId = "12345",
                Name = "John Doe",
                IsActive = true
            });

            var mockTime = new Mock<IDateTimeProvider>();
            mockTime.Setup(p => p.Now).Returns(new DateTime(2024, 1, 1, 20, 0, 0));

            var accessControl = new AccessControl(mockInfoProvider.Object, mockTime.Object);

            Assert.ThrowsException<UnauthorizedAccessException>(() => accessControl.GrantAccess("12345"));
        }
    }
}
