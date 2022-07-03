using NannyModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NannyData.Interfaces
{
    public interface IUserDAO
    {
        public ApplicationUser GetUserByID(int id);
        public ApplicationUser GetUserForLogin(string userInput);
    }
}
