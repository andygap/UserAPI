using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;

namespace UsersAPI.Application.Interfaces.Application
{
    public interface IUserAppService:IDisposable
    {
        UserResponseDto Add(UserAddRequestDto dto);
        UserResponseDto Update(UserUpdateRequest dto, Guid id);
        UserResponseDto Delete(Guid id);
        UserResponseDto Get(Guid id);
    }
}
