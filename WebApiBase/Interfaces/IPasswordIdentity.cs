using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiBase.Interfaces
{
    public interface IPasswordIdentity
    {   
        /// <summary>
        /// Password
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// Confirmacion de Password
        /// </summary>
        string ConfirmPassword { get; set; }
    }
}
