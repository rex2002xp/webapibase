using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiBase.Interfaces
{
    public interface IUserIdentity
    {
        string Email { set; get; }
        string Username { set; get; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        byte Level { get; set; }
    }
}
