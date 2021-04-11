using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassicsApp.ViewModels
{
    public class AlertViewModel
    {
        public Guid AlertId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string ShortMessage { get; set; }
        public string StatusText { get; set; }
        public int StatusValue { get; set; }
        public string CreatedBy { get; set; }
        public string Receiver { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class NewAlert
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public Guid CreatedBy { get; set; }
        public int Receiver { get; set; }
    }

    public class AlertStatus
    {        
        public Guid AlertId { get; set; }
        public Enums.Alert.AlertStatus Status { get; set; }
    }

    public class UserAlert
    {
        public Guid UserAlertId { get; set; }
        public string Subject { get; set; }
        public string ShortMessage { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
