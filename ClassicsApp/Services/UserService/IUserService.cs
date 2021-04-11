using ClassicsApp.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.Services
{
    public interface IUserService
    {
        UserProfile GetById(Guid userId);
        void Update(EditUserProfile editUser);
        void Create(NewUser newUser);
        LoggedUser GetLoggedUser(string email, string password);
        List<UsersList> GetAll();
    }
}
