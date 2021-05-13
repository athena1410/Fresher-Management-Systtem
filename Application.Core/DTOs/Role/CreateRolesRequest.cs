using System.Collections.Generic;

namespace Application.Core.DTOs.Role
{
    public class CreateRolesRequest
    {
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
