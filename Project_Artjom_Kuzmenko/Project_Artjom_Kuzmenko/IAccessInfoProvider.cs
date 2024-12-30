namespace Project_Artjom_Kuzmenko
{
    public interface IAccessInfoProvider
    {
        public string Url { get; set; }
        UserInfo GetUserInfo(string accessCardId);
    }
}
