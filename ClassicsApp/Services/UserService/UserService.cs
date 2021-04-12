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
            var profileId = _unitOfWork.ProfileRepository.FirstOrDefault(p => p.Type == Enums.Profile.ProfileType.Client).ProfileId;

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
                Email = newUser.Email.Trim().ToLower(),
                Password1 = Helpers.Crypt.Sha256(newUser.Password),
                Status = Enums.User.UserStatus.Enabled,
                ProfileId = profileId,
                CreatedOn = DateTime.Now
            };

            _unitOfWork.AddressRepository.Add(address);
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Commit();
        }

        public bool CheckIfExists(string email) 
        { 
            return _unitOfWork.UserRepository.GetIQueryable(p => p.Email.Equals(email.Trim().ToLower())).Any();
        }

        public void Update(EditUserProfile editUser)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.UserId == editUser.UserId).First();
            var profile = _unitOfWork.ProfileRepository.Get(p => p.Type == (Enums.Profile.ProfileType)editUser.ProfileType).First();

            user.Status = (Enums.User.UserStatus)editUser.Status;
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

            var logged = new LoggedUser
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                LoginMessage = string.Empty,
                ProfileType = Helpers.EnumHelper.GetDescription(user.Profile.Type),
                ProfileTypeValue = user.Profile.Type.GetHashCode(),
                PostalCode = user.Address != null ? user.Address.ZipCode : string.Empty
            };

            return logged;
        }

        public LoggedUser GetLoggedUserFromGoogle(string email, string name)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.Email.Equals(email.Trim().ToLower())).FirstOrDefault();

            if (user == null)
                CreateFromGoogle(email, name);

            var logged = GetByEmail(email);

            return logged;
        }

        public void CreateFromGoogle(string email, string name)
        {
            var profileId = _unitOfWork.ProfileRepository.FirstOrDefault(p => p.Type == Enums.Profile.ProfileType.Client).ProfileId;

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = name,
                Email = email.Trim().ToLower(),
                Status = Enums.User.UserStatus.Enabled,
                ProfileId = profileId,
                CreatedOn = DateTime.Now
            };

            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Commit();
        }

        public LoggedUser GetByEmail(string email)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.Email.Equals(email)).FirstOrDefault();

            var logged = new LoggedUser
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                LoginMessage = string.Empty,
                ProfileType = Helpers.EnumHelper.GetDescription(user.Profile.Type),
                ProfileTypeValue = user.Profile.Type.GetHashCode(),
                PostalCode = user.Address != null ? user.Address.ZipCode : string.Empty
            };

            return logged;
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
