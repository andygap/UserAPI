using UsersAPI.Domain.Exceptions;
using UsersAPI.Domain.Interfaces.Messages;
using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Domain.Interfaces.Security;
using UsersAPI.Domain.Interfaces.Services;
using UsersAPI.Domain.Models;
using UsersAPI.Domain.ValueObjects;

namespace UsersAPI.Domain.Services
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUnitOfWork? unitOfWork;
        private readonly IUserMessageProducer? messageProducer;
        private readonly ITokenService tokenService;
        public UserDomainService(IUnitOfWork? unitOfWork, IUserMessageProducer? messageProducer, ITokenService tokenService)
        {
            this.unitOfWork = unitOfWork;
            this.messageProducer = messageProducer;
            this.tokenService = tokenService;
        }

        public void Add(User user)
        {
            if (Get(user.Email) != null)
                throw new EmailAlreadyExistsException(user.Email);

            unitOfWork?.UserRepository.Add(user);
            unitOfWork?.SaveChanges();

            messageProducer?.Send(new UserMessageVO
            {
                Id = user.Id,
                SendedAt = DateTime.Now,
                To = user.Email,
                Subject = "Parabens, sua conta de usuario foi criada com sucesso",
                Body = @$"Olá {user.Name}, seu cadastro foi realizado com sucesso."
            }); ;
        }

        public void Update(User user)
        {
            unitOfWork?.UserRepository.Update(user);
            unitOfWork?.SaveChanges();
        }

        public void Delete(User user)
        {
            unitOfWork?.UserRepository.Delete(user);
            unitOfWork?.SaveChanges();
        }

        public User? Get(Guid id)
        {
            return unitOfWork?.UserRepository.GetById(id);
        }

        public User? Get(string email)
        {
            return unitOfWork?.UserRepository.Get(u => u.Email == email);
        }

        public User? Get(string email, string password)
        {
            return unitOfWork?.UserRepository.Get(u => u.Email == email && u.Password == password);
        }

        public string Authenticate(string email, string password)
        {
            var user = Get(email, password);

            if (user == null)
                throw new AccessDeniedExeption();

            var userAuth = new UserAuthVO
            {
                Id = user.Id,
                Nome = user.Name,
                Email = user.Email,
                Role = "USER_ROLE",  //perfil do usuario
                SignedAt = DateTime.Now,
            };

            return tokenService?.CreateToke(userAuth);
        }

        public void Dispose() => unitOfWork?.Dispose();


    }
}
