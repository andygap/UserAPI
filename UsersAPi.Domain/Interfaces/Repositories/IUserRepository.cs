using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAPi.Domain.Models;

namespace UsersAPi.Domain.Interfaces.Repositories
{
    public interface IUserRepository:IBaseRepository<User, Guid>
    {
    }
}
