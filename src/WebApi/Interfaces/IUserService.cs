using WebApi.Models;

namespace WebApi.Interface;
public interface IUserService
{  
    public IEnumerable<User> GetUsers();

}