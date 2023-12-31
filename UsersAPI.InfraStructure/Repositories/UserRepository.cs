﻿using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Domain.Models;
using UsersAPI.Infra.Data.Contexts;

namespace UsersAPI.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        private DataContext? dataContext;

        public UserRepository(DataContext? dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
    }
}
