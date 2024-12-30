using AccessControlTests;
using Project_Artjom_Kuzmenko;

namespace AccessControlAcceptanceTests
{
    [TestClass]
    public sealed class Test1
    {
        private AccessControl accessControl;

        // Setup voor acceptatietests
        [TestInitialize]
        public void Setup()
        {
            // Gebruik echte implementaties
            var accessInfoProvider = new AccessInfoProvider();
            var timeProvider = new DateTimeProvider(); // Echte implementatie
            accessControl = new AccessControl(accessInfoProvider, timeProvider);
        }

        [TestMethod]
        public void GrantAccess_ActiveUserWithinWorkingHours_ReturnsTrue()
        {
            // Arrange
            // Gebruiker 12345 is actief en de tijd is 10:00 (binnen werktijd)
            string accessCardId = "12345";

            // Act
            var result = accessControl.GrantAccess(accessCardId);

            // Assert
            Assert.IsTrue(result, "De toegang moet worden verleend aan een actieve gebruiker binnen werktijd.");
        }
        [TestMethod]
        public void GrantAccess_InactiveUser_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            // Gebruiker 67890 is inactief
            string accessCardId = "67890";

            // Act & Assert
            var ex = Assert.ThrowsException<UnauthorizedAccessException>(() => accessControl.GrantAccess(accessCardId));
            Assert.AreEqual("User is not active.", ex.Message, "De toegang moet worden geweigerd voor een inactieve gebruiker.");
        }
        [TestMethod]
        public void GrantAccess_AccessOutsideWorkingHours_ThrowsUnauthorizedAccessException()
        {
            string accessCardId = "12345";
            var mockTimeProvider = new DateTimeProvider();
            mockTimeProvider.SetTime(new DateTime(2024, 1, 1, 20, 0, 0)); // Buiten werktijd

            // Maak een nieuwe AccessControl met de tijdprovider ingesteld op 20:00 uur
            var accessControl = new AccessControl(new AccessInfoProvider(), mockTimeProvider);

            // Act & Assert
            var ex = Assert.ThrowsException<UnauthorizedAccessException>(() => accessControl.GrantAccess(accessCardId));
            Assert.AreEqual("Access denied outside working hours.", ex.Message, "De toegang moet worden geweigerd buiten werktijden.");

        }
        [TestMethod]
        public void GrantAccess_UnknownAccessCard_ThrowsArgumentException()
        {
            // Arrange
            // Onbekende toegangspas
            string accessCardId = "99999";

            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => accessControl.GrantAccess(accessCardId));
            Assert.AreEqual("Invalid access card.", ex.Message, "De toegangspas moet onbekend zijn en een fout moeten genereren.");
        }
    }
}
