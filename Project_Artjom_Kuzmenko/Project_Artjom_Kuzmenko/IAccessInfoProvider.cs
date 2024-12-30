namespace Project_Artjom_Kuzmenko
{
    public interface IAccessInfoProvider
    {
        UserInfo GetUserInfo(string accessCardId);
    }
}
