using System.Collections.Generic;

namespace Application.Core.DTOs.Account
{
    public class CreateRolesRequest
    {
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
