using System.Threading.Tasks;
using BlikkBasicReplica.API.Models.Auth;

namespace BlikkBasicReplica.API.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> Register(RegisterModel model);
        Task<UserManagerResponse> Login(LoginModel model);
    }
}
