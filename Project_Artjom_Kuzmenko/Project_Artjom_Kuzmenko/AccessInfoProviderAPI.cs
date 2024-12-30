using Newtonsoft.Json;

namespace Project_Artjom_Kuzmenko
{
    public class AccessInfoProviderAPI : IAccessInfoProvider
    {
        private string url = "http://localhost:3000/data/acces";

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public UserInfo GetUserInfo(string AccesCardId)
        {
            using (var httpClient = new HttpClient())
            {
                var httpRespone = httpClient.GetAsync(url).GetAwaiter().GetResult();
                var response = httpRespone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (!JsonConvert.DeserializeObject<UserInfo>(response).Exists) return null;
                return JsonConvert.DeserializeObject<UserInfo>(response);
            }
        }
    }
}
