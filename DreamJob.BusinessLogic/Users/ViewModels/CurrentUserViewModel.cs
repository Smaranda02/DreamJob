using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Users.ViewModels
{
    public class CurrentUserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; }

        public string Username { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
