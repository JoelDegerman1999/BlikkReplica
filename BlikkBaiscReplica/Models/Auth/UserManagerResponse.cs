using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BlikkBaiscReplica.Models.Auth
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public string UserId { get; set; }
        public string Token{ get; set; }
        public bool Success { get; set; }
    }
}
