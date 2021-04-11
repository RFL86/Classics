using ClassicsApp.ObjectValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.Services
{
    public interface IAlertService
    {
        void Create(ViewModels.NewAlert alert);
        List<ViewModels.AlertViewModel> GetAll();
        void ChangeAlertStatus(ViewModels.AlertStatus alert);
        List<ViewModels.UserAlert> GetUserAlerts(Guid userId);
        void ChangeUserAlertStatus(Guid userAlertId);
    }
}
