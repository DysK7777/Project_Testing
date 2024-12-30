using AccessControlTests;
using Moq;
using Project_Artjom_Kuzmenko;

namespace AccessControlIntegrationTests
{
    [TestClass]
    public sealed class Test1
    {
        private AccessInfoProvider accessInfoProvider;
        private Mock<IDateTimeProvider> mockTimeProvider;
        private AccessControl accessControl;

        // Setup code die wordt uitgevoerd voor elke test
        [TestInitialize]
        public void TestInitialize()
        {
            // Initialiseer de mock objecten en de AccessControl instantie
            accessInfoProvider = new AccessInfoProvider();
            mockTimeProvider = new Mock<IDateTimeProvider>();
        }

        [TestMethod]
        public void GrantAccess_ActiveUserWithinWorkingHours_ReturnsTrue()
        {
            // Arrange: Stel de tijd in en maak de AccessControl instantie
            mockTimeProvider.Setup(p => p.Now).Returns(new DateTime(2024, 1, 1, 10, 0, 0)); // Binnen werktijd
            accessControl = new AccessControl(accessInfoProvider, mockTimeProvider.Object);

            // Act
            var result = accessControl.GrantAccess("12345");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GrantAccess_InactiveUser_ThrowsUnauthorizedAccessException()
        {
            // Arrange: Stel de tijd in en maak de AccessControl instantie
            mockTimeProvider.Setup(p => p.Now).Returns(new DateTime(2024, 1, 1, 10, 0, 0)); // Binnen werktijd
            accessControl = new AccessControl(accessInfoProvider, mockTimeProvider.Object);

            // Act & Assert
            Assert.ThrowsException<UnauthorizedAccessException>(() => accessControl.GrantAccess("67890")); // Inactieve gebruiker
        }

        [TestMethod]
        public void GrantAccess_UnknownAccessCard_ThrowsArgumentException()
        {
            // Arrange: Stel de tijd in en maak de AccessControl instantie
            mockTimeProvider.Setup(p => p.Now).Returns(new DateTime(2024, 1, 1, 10, 0, 0)); // Binnen werktijd
            accessControl = new AccessControl(accessInfoProvider, mockTimeProvider.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => accessControl.GrantAccess("99999")); // Onbekende toegangspas
        }

        [TestMethod]
        public void GrantAccess_AccessOutsideWorkingHours_ThrowsUnauthorizedAccessException()
        {
            // Arrange: Stel de tijd in en maak de AccessControl instantie
            mockTimeProvider.Setup(p => p.Now).Returns(new DateTime(2024, 1, 1, 20, 0, 0)); // Buiten werktijd
            accessControl = new AccessControl(accessInfoProvider, mockTimeProvider.Object);

            // Act & Assert
            Assert.ThrowsException<UnauthorizedAccessException>(() => accessControl.GrantAccess("12345")); // Actieve gebruiker, maar buiten werktijden
        }
    }
}
