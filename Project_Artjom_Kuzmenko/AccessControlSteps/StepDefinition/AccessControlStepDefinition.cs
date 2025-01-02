using AccessControlTests;
using Project_Artjom_Kuzmenko;
using Xunit.Gherkin.Quick;

namespace AcceptanceTesting.StepDefinition
{
    [FeatureFile("./Features/AccessControl.feature")]
    public sealed class AccessControlStepDefinition : Feature
    {
        private const string mockoonUsers = "http://localhost:3000/data/acces";

        private readonly IAccessInfoProvider _infoProvider;
        private readonly IDateTimeProvider _dateTimeProvider;
        private AccessControl accessControl;
        private bool _accessGranted;
        private int? _accessCardId;
        private string _name;
        private bool _active;
        public bool _exist;

        public AccessControlStepDefinition()
        {
            _dateTimeProvider = new DateTimeProvider();
            _infoProvider = new AccessInfoProvider();
            accessControl = new AccessControl(_infoProvider, _dateTimeProvider);
            _name = string.Empty;
        }

        [Given(@"the user (.*) is active")]
        public void GivenUserIsActive(string accessCardId)
        {
            _accessCardId = int.Parse(accessCardId);
            _name = "test";
            _active = true;
            _exist = true;
            _infoProvider.Url = $"{mockoonUsers}/?id={_accessCardId}&name=%22{_name}%22&active={_active}&exist={_exist}";
        }

        [And(@"the current time is (.*)")]
        public void AndTheCurrentTimeIs(string time)
        {
            var currentTime = DateTime.Parse(time);
            _dateTimeProvider.Now = currentTime;
        }

        [Given(@"the user (.*) is inactive")]
        public void GivenUserIsInactive(string accessCardId)
        {
            _accessCardId = int.Parse(accessCardId);
            _name = "test";
            _active = false;
            _exist = true;
            _infoProvider.Url = $"{mockoonUsers}/?id={_accessCardId}&name=%22{_name}%22&active={_active}&exist={_exist}";
        }

        [And(@"the current time is (.*)")]
        public void AndTheCurrentTimeIsAgain(string time)
        {
            var currentTime = DateTime.Parse(time);
            _dateTimeProvider.Now = currentTime;
        }

        [Given(@"the user (.*) is unknown")]
        public void GivenUserIsUnknown(string accessCardId)
        {
            _accessCardId = null;
            _name = "test";
            _active = true;
            _exist = true;
            _infoProvider.Url = $"{mockoonUsers}/?id={_accessCardId}&name=%22{_name}%22&active={_active}&exist={_exist}";
        }

        [And(@"the current time is (.*)")]
        public void AndTheCurrentTimeIsForUnknown(string time)
        {
            var currentTime = DateTime.Parse(time);
            _dateTimeProvider.Now = currentTime;
        }

        [When(@"the user attempts to access the building")]
        public void WhenUserAttemptsToAccessBuilding()
        {
            try
            {
                _accessGranted = accessControl.GrantAccess((_accessCardId ?? 0).ToString());
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

        [Then(@"access should be granted")]
        public void ThenAccessShouldBeGranted()
        {
            Assert.True(_accessGranted);
        }

        [Then(@"access should be denied")]
        public void ThenAccessShouldBeDenied()
        {
            Assert.False(_accessGranted);
        }
    }
}
