﻿using UsersAPI.Domain.Models;

namespace UsersAPI.Domain.Interfaces.Services
{
    public interface IUserDomainService : IDisposable
    {
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        User? Get(Guid id);
        User? Get(string email);
        User? Get(string email, string password);
    }
}