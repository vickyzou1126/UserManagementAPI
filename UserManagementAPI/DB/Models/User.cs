using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using UserManagementAPI.Extensions;

namespace UserManagementAPI.DB.Models
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
            var valid = true;
            if (name.Length == 0)
            {
                this.message.addStringIfNotExist("name is empty");
                valid = false;
            }
                

            if (email.Length == 0)
            {
                this.message.addStringIfNotExist("email is empty");
                valid = false;
            }    
            else if (!email.IsValidEmail())
            {
                this.message.addStringIfNotExist("email address is not valid");
                valid = false;
            }
              
            if (salary <= 0)
            {
                this.message.addStringIfNotExist("salary must be greater than 0");
                
            }             

            if (expense <= 0)
            {
                this.message.addStringIfNotExist("expense must be greater than 0");
                valid = false;
            }

            return valid;
        }
        private void TrimValues()
        {
            name = name != null ? name.Trim() : "";
            email = email != null ? email.Trim() : "";
        }
    }
}
