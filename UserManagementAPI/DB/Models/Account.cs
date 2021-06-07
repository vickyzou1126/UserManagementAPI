using Newtonsoft.Json;

namespace UserManagementAPI.DB.Models
{
    public class Account : BaseClass
    {
        public int userId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
