using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ClassicsApp.ObjectValue
{
    public class LoggedUser
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string LoginMessage { get; set; }
        public string ProfileType { get; set; }
        public int ProfileTypeValue { get; set; }
        public string Token { get; set; }
        public string PostalCode { get; set; }

    }

    public class NewUser
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }


    public class EditUserProfile
    {
        public Guid UserId { get; set; }    
        public int Status { get; set; }
        public int ProfileType { get; set; }

    }

    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UsersList
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StatusDescription { get; set; }
        public string ProfileDescription { get; set; }
        public int StatusValue{ get; set; }
        public int ProfileValue { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        public IEnumerable<SelectListItem> ProfileType { get; set; }
    }

    public class UserProfile
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string PostalCode { get; set; }

    }
}
