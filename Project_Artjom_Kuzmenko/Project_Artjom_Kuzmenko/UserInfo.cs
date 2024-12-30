using System.Text.Json.Serialization;

namespace Project_Artjom_Kuzmenko
{
    public class UserInfo
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string AccessCardId { get; set; }
        public Boolean Exists { get; set; }
    }
}
