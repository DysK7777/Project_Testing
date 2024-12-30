using AccessControlTests;

namespace Project_Artjom_Kuzmenko
{
    public class AccessControl
    {
        private readonly IAccessInfoProvider _infoProvider;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AccessControl(IAccessInfoProvider infoProvider, IDateTimeProvider dateTimeProvider)
        {
            _infoProvider = infoProvider;
            _dateTimeProvider = dateTimeProvider;
        }

        public bool GrantAccess(string accessCardId)
        {
            var userInfo = _infoProvider.GetUserInfo(accessCardId);
            if (userInfo == null)
                throw new ArgumentException("Invalid access card.");

            if (!userInfo.IsActive)
                throw new UnauthorizedAccessException("User is not active.");

            if (_dateTimeProvider.Now.Hour < 8 || _dateTimeProvider.Now.Hour > 18)
                throw new UnauthorizedAccessException("Access denied outside working hours.");

            return true;
        }
    }
}
