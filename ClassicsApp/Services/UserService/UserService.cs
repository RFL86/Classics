using Classics.Data.UnitOfWork;
using Classics.Data.Models;
using ClassicsApp.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClassicsApp.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseUnitOfWork _unitOfWork;


        public UserService(IBaseUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(NewUser newUser)
        {
            var profile = _unitOfWork.ProfileRepository.FirstOrDefault(p => p.Type == Enums.Profile.ProfileType.Client);

            var address = new Address
            {
                AddressId = Guid.NewGuid(),
                StateCode = newUser.StateCode,
                City = newUser.City,
                ZipCode = newUser.PostalCode
            };

            var user = new Classics.Data.Models.User
            {
                UserId = Guid.NewGuid(),
                Name = newUser.Name,
                Email = newUser.Email,
                Password1 = Helpers.Crypt.Sha256(newUser.Password),
                Status = Enums.User.UserStatus.Enabled,
                ProfileId = profile.ProfileId,
                CreatedOn = DateTime.Now
            };

            _unitOfWork.AddressRepository.Add(address);
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Commit();
        }

        public void Update(EditUserProfile editUser)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.UserId == editUser.UserId).First();
            var profile = _unitOfWork.ProfileRepository.Get(p => p.Type == editUser.ProfileType).First();

            user.Status = editUser.Status;
            user.ProfileId = profile.ProfileId;

            _unitOfWork.UserRepository.Edit(user);
            _unitOfWork.Commit();
        }

        public UserProfile GetById(Guid userId)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.UserId == userId).First();

            var userViewModel = new UserProfile
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                MobilePhone = user.MobilePhone,
                Address = user.Address != null ? string.Concat(user.Address.City, "-", user.Address.StateCode) : "Não informado", 
                PostalCode = user.Address != null ? user.Address.ZipCode : string.Empty
            };

            return userViewModel;
        }

        public LoggedUser GetLoggedUser(string email, string password)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.Email == email && u.Password1.Equals(password)).FirstOrDefault();

            if (user == null)
                return new LoggedUser { LoginMessage = "Usuário ou senha inválidos." };

            var userLogin = new LoggedUser
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                LoginMessage = string.Empty,
                ProfileType = Helpers.EnumHelper.GetDescription(user.Profile.Type),
                ProfileTypeValue = user.Profile.Type.GetHashCode(),
                PostalCode = user.Address != null ? user.Address.ZipCode : "Não informado"
            };

            return userLogin;
        }

        public List<UsersList> GetAll()
        {
            var users = _unitOfWork.UserRepository.Get().Select(u => new UsersList
            {
                UserId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                PhoneNumber = u.MobilePhone,
                StatusDescription = u.Status == Enums.User.UserStatus.Enabled ? "Ativo" : "Inativo",
                ProfileDescription = u.Profile.Name,
                StatusValue = u.Status.GetHashCode(),
                ProfileValue = u.Profile.Type.GetHashCode()
            }).ToList();

            return users;
        }

    }
}
