using Auth.Service.Models;

namespace Auth.Service.Infrastructure.Data.EntityFramework;
public interface IAuthStore
{
  Task<User?> VerifyUserLogin(string username, string password);
}
