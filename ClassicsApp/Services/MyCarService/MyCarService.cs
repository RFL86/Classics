using Classics.Data.UnitOfWork;
using Classics.Data.Models;
using ClassicsApp.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicsApp.ViewModels;

namespace ClassicsApp.Services
{
    public class MyCarService : IMyCarService
    {
        private readonly IBaseUnitOfWork _unitOfWork;


        public MyCarService(IBaseUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<MyCars> GetMyCars(Guid ownerId)
        {
            var myCars = _unitOfWork.MyCarRepository.Get(c => c.OwnerId == ownerId).Select(c => new MyCars
            {
                MyCarId = c.MyCarId,
                NickName = c.NickName,
                BrandName = c.Serie.CarModel.Brand.Name,
                ModelName = c.Serie.CarModel.Name,
                SerieName = c.Serie.Name
            }).OrderBy(c => c.NickName).ToList();
            return myCars;
        }

        public string AddMyCar(NewCar newCar, Guid ownerId)
        {
            var result = CheckCarBeforeSave(newCar.Name, ownerId);
            if (!string.IsNullOrEmpty(result))
                return result;

            var myCar = new MyCar
            {
                MyCarId = Guid.NewGuid(),
                NickName = newCar.Name,
                SerieId = newCar.SerieId,
                OwnerId = ownerId,
                CreatedOn = DateTime.Now,
                Status = Enums.MyCar.MyCarStatus.Enabled
            };

            _unitOfWork.MyCarRepository.Add(myCar);
            _unitOfWork.Commit();

            return string.Empty;
        }



        public string EditMyCar(EditMyCar myCar)
        {
            if (string.IsNullOrEmpty(myCar.NickName))
                return "Tamanho do nome inválido.";
            if (myCar.NickName.Length < 3)
                return "Tamanho do nome inválido.";

            var car = _unitOfWork.MyCarRepository.Get(c => c.MyCarId == myCar.MyCarId).FirstOrDefault();
            car.NickName = myCar.NickName;

            _unitOfWork.MyCarRepository.Edit(car);
            _unitOfWork.Commit();

            return string.Empty;
        }


        public void RemoveMyCar(Guid myCarId)
        {
            var myCar = _unitOfWork.MyCarRepository.Get(c => c.MyCarId == myCarId).First();
    
            _unitOfWork.MyCarRepository.Delete(myCar);
            _unitOfWork.Commit();
        }


        public string CheckCarBeforeSave(string name, Guid ownerId)
        {
            var existsCar = _unitOfWork.MyCarRepository.GetIQueryable(c => c.NickName.ToLower().Equals(name.ToLower().Trim()) && c.OwnerId == ownerId).Any();
            if (existsCar)
                return "Carro já cadastrado.";

            if (string.IsNullOrEmpty(name))
                return "Tamanho do nome inválido.";
            if (name.Length < 3)
                return "Tamanho do nome inválido.";

            return string.Empty;
        }

    }
}
