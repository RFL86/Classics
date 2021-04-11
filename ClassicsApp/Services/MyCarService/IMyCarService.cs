
using ClassicsApp.ViewModels;
using System;
using System.Collections.Generic;

namespace ClassicsApp.Services
{
    public interface IMyCarService
    {
        string AddMyCar(NewCar newCar, Guid ownerId);
        List<MyCars> GetMyCars(Guid ownerId);
        string EditMyCar(EditMyCar myCar);
        void RemoveMyCar(Guid myCarId);
    }
}
