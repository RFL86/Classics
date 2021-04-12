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
    public class AlertService : IAlertService
    {
        private readonly IBaseUnitOfWork _unitOfWork;


        public AlertService(IBaseUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ViewModels.AlertViewModel> GetAll()
        {
            var alerts = _unitOfWork.AlertRepository.Get( a=> !a.Message.Contains("moderação")).Select(a => new ViewModels.AlertViewModel
            {
                AlertId = a.AlertId,
                Subject = a.Subject,
                Message = a.Message,
                ShortMessage = a.Message.Substring(0, Math.Min(a.Message.Length, 60)) +
                (Math.Min(a.Message.Length, 60) == 60 ? "(...)" : ""),
                StatusText = a.Status == Enums.Alert.AlertStatus.Available ? "Disponível" : "Removido",
                StatusValue = a.Status.GetHashCode(),
                CreatedBy = a.Creator.Name,
                CreatedOn = a.CreatedOn,
                Receiver = a.Receiver == Enums.Alert.Receiver.All ? "Todos" : a.Receiver == Enums.Alert.Receiver.Client ? "Cliente" : "Customizado"
            }).OrderByDescending(a => a.CreatedOn).ToList();

            return alerts;
        }

        public List<ViewModels.UserAlert> GetUserAlerts(Guid userId)
        {
            var alerts = _unitOfWork.UserAlertRepository.Get(a => a.UserId == userId && a.Alert.Status == Enums.Alert.AlertStatus.Available).Select(a => new ViewModels.UserAlert
            {
                UserAlertId = a.UserAlertId,
                Subject = a.Alert.Subject,
                Message = a.Alert.Message,
                ShortMessage = a.Alert.Message.Substring(0, Math.Min(a.Alert.Message.Length, 60)) +
                (Math.Min(a.Alert.Message.Length, 60) == 60 ? "(...)" : ""),
                Status = Helpers.EnumHelper.GetDescription(a.ReadingStatus)
            }).OrderByDescending(u => u.CreatedOn).ToList();

            return alerts;
        }

        public void Create(ViewModels.NewAlert alert)
        {
            var receiver = (Enums.Alert.Receiver)alert.Receiver;
            var newAlert = new Alert()
            {
                AlertId = Guid.NewGuid(),
                Subject = alert.Subject,
                Message = alert.Message,
                CreatedBy = alert.CreatedBy,
                CreatedOn = DateTime.Now,
                Receiver = receiver,
                Status = Enums.Alert.AlertStatus.Available
            };

            var userIds = GetReceiverIds(receiver);
            var userAlerts = new List<UserAlert>();
            foreach (var item in userIds)
            {
                userAlerts.Add(new UserAlert
                {
                    UserAlertId = Guid.NewGuid(),
                    UserId = item,
                    AlertId = newAlert.AlertId,
                    ReadingStatus = Enums.UserAlert.ReadingStatus.NotRead
                });
            }

            _unitOfWork.AlertRepository.Add(newAlert);
            _unitOfWork.UserAlertRepository.AddAll(userAlerts);
            _unitOfWork.Commit();
        }

        public List<Guid> GetReceiverIds(Enums.Alert.Receiver receiver)
        {
            if (receiver == Enums.Alert.Receiver.Client)
            {
                return _unitOfWork.UserRepository.Get(u => u.Profile.Type == Enums.Profile.ProfileType.Client
                && u.Status == Enums.User.UserStatus.Enabled)
                    .Select(u => u.UserId).ToList();
            }
            else if (receiver == Enums.Alert.Receiver.All)
            {
                return _unitOfWork.UserRepository.Get(u => u.Profile.Type == Enums.Profile.ProfileType.Client
                && u.Status == Enums.User.UserStatus.Enabled)
                 .Select(u => u.UserId).ToList();
            }
            else
                return new List<Guid>();

        }

        public void ChangeAlertStatus(ViewModels.AlertStatus alert)
        {
            var alertToEdit = _unitOfWork.AlertRepository.FirstOrDefault(a => a.AlertId == alert.AlertId);

            alertToEdit.Status = alert.Status;

            _unitOfWork.AlertRepository.Edit(alertToEdit);
            _unitOfWork.Commit();
        }


        public void ChangeUserAlertStatus(Guid userAlertId)
        {
            var alertToEdit = _unitOfWork.UserAlertRepository.FirstOrDefault(a => a.UserAlertId == userAlertId);

            alertToEdit.ReadingStatus = Enums.UserAlert.ReadingStatus.Read;

            _unitOfWork.UserAlertRepository.Edit(alertToEdit);
            _unitOfWork.Commit();
        }

    }
}
