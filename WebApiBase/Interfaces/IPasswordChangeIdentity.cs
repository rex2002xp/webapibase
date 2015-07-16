using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiBase.Interfaces
{
    public interface IPasswordChangeIdentity : IPasswordIdentity
    {
        /// <summary>
        /// Password Actual
        /// </summary>
        string OldPassword { get; set; }
    }
}
