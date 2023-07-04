using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAPI.Domain.Interfaces.Services;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;

namespace UsersAPI.Application.Services
{
    public class AuthAppService: IAuthAppService
    {
        private readonly IUserDomainService userDomainService;
        private readonly IMapper mapper;

        public AuthAppService(IUserDomainService userDomainService, IMapper mapper)
        {
            this.userDomainService = userDomainService;
            this.mapper = mapper;
        }

        public LoginResponseDto Login(LoginRequestDto dto)
        {
            var user = userDomainService?.Get(dto.Email, dto.Password);
            return new LoginResponseDto
            {
                AccesToken = string.Empty,
                Expiration = DateTime.Now,
                User = mapper.Map<UserResponseDto>(user)
            };
        }

        public UserResponseDto ForgotPassword(ForgotPasswordRequestDto dto)
        {
            var user = userDomainService?.Get(dto.Email);
            return mapper.Map<UserResponseDto>(user);
        }

        public UserResponseDto ResetPassword(ResetPasswordRequestDto dto, Guid id)
        {
            var user = userDomainService?.Get(id);
            return mapper.Map<UserResponseDto>(user);
        }

        public void Dispose() => userDomainService.Dispose();
    }
}
