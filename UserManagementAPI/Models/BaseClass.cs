using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementAPI.Models
{
    public class BaseClass
    {
        public BaseClass()
        {
            message = new List<string>();
        }
        public int id { get; set; }
        [NotMapped]
        public List<string> message { get; set; }
    }
}
