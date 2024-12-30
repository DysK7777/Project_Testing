namespace Project_Artjom_Kuzmenko
{
    public class AccessInfoProvider : IAccessInfoProvider
    {
        private readonly List<UserInfo> _users = new List<UserInfo>
    {
        new UserInfo { AccessCardId = "12345", Name = "John Doe", IsActive = true },
        new UserInfo { AccessCardId = "67890", Name = "Jane Doe", IsActive = false }
    };
        public string Url { get; set; }
        public UserInfo GetUserInfo(string accessCardId)
        {
            return _users.FirstOrDefault(u => u.AccessCardId == accessCardId);
        }
    }
}
