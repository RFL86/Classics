using Classics.Data.UnitOfWork;
using ClassicsApp.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.Services
{
    public class LoginService : ILoginService
    {
        private readonly IBaseUnitOfWork _unitOfWork;


        public LoginService(IBaseUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Login(UserLogin userLoginViewModel)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.Email == userLoginViewModel.Email).FirstOrDefault();

            //if (user != null && user.Password1.Length != 64 && user.Password1 == Crypt.GetMd5Hash(model.Password.ToUpper()))
            //{
            //    user.Password1 = Crypt.Sha256(model.Password);
            //    _unitOfWork.RepositoryBase.Edit<User>(user);
            //    _unitOfWork.Commit();
            //}

            //if (user == null || user.Password1 != Crypt.Sha256(model.Password) || user.Status == (int)Enums.User.Status.Inativo)
            //    return false;

            //return true;


            return true;
        }

        public void Loggof()
        {
       
        }

        //public User GetById(Guid userId)
        //{
        //    var user = _unitOfWork.UserRepository.Get(u => u.UserId == userId).First();

        //    var userViewModel = new User
        //    {
        //        UserId = user.UserId,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Email = user.Email,
        //        MobilePhone = user.MobilePhone
        //    };

        //    return userViewModel;
        //}
    }
}
