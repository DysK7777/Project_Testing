using AccessControlTests;
using Moq;
using Project_Artjom_Kuzmenko;
using Xunit.Gherkin.Quick;

namespace AccessControlSteps
{
    [FeatureFile("./Features/AccessControl.feature")]
    public class UnitTest1 : Feature
    {
        private Mock<IAccessInfoProvider> _mockAccessInfoProvider;
        private Mock<IDateTimeProvider> _mockTimeProvider;
        private AccessControl _accessControl;
        private bool _accessGranted;
        private string _accessCardId;

        public UnitTest1()
        {
            // Arrange mock dependencies
            _mockAccessInfoProvider = new Mock<IAccessInfoProvider>();
            _mockTimeProvider = new Mock<IDateTimeProvider>();
            _accessControl = new AccessControl(_mockAccessInfoProvider.Object, _mockTimeProvider.Object);
        }

        [Given("the user \"(.*)\" is active")]
        public void GivenUserIsActive(string accessCardId)
        {
            _accessCardId = accessCardId;
            _mockAccessInfoProvider.Setup(p => p.GetUserInfo(accessCardId)).Returns(new UserInfo
            {
                AccessCardId = accessCardId,
                IsActive = true
            });
        }

        [Given("the user \"(.*)\" is inactive")]
        public void GivenUserIsInactive(string accessCardId)
        {
            _accessCardId = accessCardId;
            _mockAccessInfoProvider.Setup(p => p.GetUserInfo(accessCardId)).Returns(new UserInfo
            {
                AccessCardId = accessCardId,
                IsActive = false
            });
        }

        [Given("the user \"(.*)\" is unknown")]
        public void GivenUserIsUnknown(string accessCardId)
        {
            _accessCardId = accessCardId;
            _mockAccessInfoProvider.Setup(p => p.GetUserInfo(accessCardId)).Throws(new ArgumentException("Access card is not valid."));
        }

        [Given("the current time is (.*)")]
        public void GivenTheCurrentTimeIs(string time)
        {
            var currentTime = DateTime.Parse(time);
            _mockTimeProvider.Setup(p => p.Now).Returns(currentTime);
        }

        [When("the user attempts to access the building")]
        public void WhenUserAttemptsToAccessBuilding()
        {
            try
            {
                _accessGranted = _accessControl.GrantAccess(_accessCardId);
            }
            catch (UnauthorizedAccessException)
            {
                _accessGranted = false;
            }
            catch (ArgumentException)
            {
                _accessGranted = false;
            }
        }

        [Then("access should be granted")]
        public void ThenAccessShouldBeGranted()
        {
            Assert.True(_accessGranted);
        }

        [Then("access should be denied")]
        public void ThenAccessShouldBeDenied()
        {
            Assert.False(_accessGranted);
        }
    }
    }