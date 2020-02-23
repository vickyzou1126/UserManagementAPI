using Newtonsoft.Json;

namespace UserManagementAPI.Models
{
    public class Account : BaseClass
    {
        public int userId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
