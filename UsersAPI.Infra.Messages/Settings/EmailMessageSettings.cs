using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersAPI.Infra.Messages.Settings
{
    public class EmailMessageSettings
    {
        public string? BaseUrl { get; set; }
        public string? User { get; set; }
        public string? Pass { get; set; }
    }
}
