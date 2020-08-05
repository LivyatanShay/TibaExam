using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TibaExam.Models;

namespace TibaExam.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}
