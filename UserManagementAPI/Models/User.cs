using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using UserManagementAPI.Services;

namespace UserManagementAPI.Models
{
    public class User : BaseClass
    {
        public string name { get; set; }
        public string email { get; set; }
        public double salary { get; set; }
        public double expense { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [NotMapped]
        public bool hasCredit
        {
            get
            {
                return UserIsValid() && (salary - expense) >= 1000;
            }
        }

        public bool UserIsValid()
        {
            TrimValues();

            if (name.Length == 0)
                this.message.addStringIfNotExist("name is empty");

            if (email.Length == 0)
                this.message.addStringIfNotExist("email is empty");
            else if (!email.IsValidEmail())
                this.message.addStringIfNotExist("email address is not valid");

            if (salary <= 0)
                this.message.addStringIfNotExist("salary must be greater than 0");

            if (expense <= 0)
                this.message.addStringIfNotExist("expense must be greater than 0");

            return this.message.Count == 0;
        }
        private void TrimValues()
        {
            name = name != null ? name.Trim() : "";
            email = email != null ? email.Trim() : "";
        }
    }
}
