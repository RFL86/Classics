using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassicsApp.ObjectValue;
using ClassicsApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin;
using ClassicsApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ClassicsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        //[Authorize(Roles = "Manager")]
        public ActionResult GetUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }


        [HttpGet("GetById")]
        [Authorize]
        public ActionResult GetById()
        {
            var user = User.Claims.Where(u => u.Type == ClaimTypes.UserData).FirstOrDefault().Value;
            var users = _userService.GetById(new Guid(user));
            return Ok(users);
        }

        [HttpGet("GetAddress")]
        [AllowAnonymous]
        public ActionResult GetAddress(string postalCode)
        {
            var fullAddress = Helpers.Sigep.GetAddressByCEP(postalCode);
            if (fullAddress != null)
            {
                var addressViewModel = new AddressViewModel
                {
                    PostalCode = fullAddress.cep,
                    StateCode = fullAddress.uf,
                    City = fullAddress.cidade
                };
                return Ok(addressViewModel);
            }

            return Ok(null);
        }


        [HttpPost("AddUser")]
        [AllowAnonymous]
        public IActionResult AddUser([FromForm] NewUser user)
        {
            _userService.Create(user);
            return Ok();
        }

        //[HttpPost]
        //[Route("GoogleAuthenticate")]
        //public async Task<ActionResult<dynamic>> GoogleAuthenticate([FromForm] string token)
        //{
        //    var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;

        //    try
        //    {
        //        var response = await auth.VerifyIdTokenAsync(token);
        //        //add claims como no authenticate              
        //        //var teste = auth

        //        if (response != null)
        //        {

        //            return Accepted();
        //        }
        //    }
        //    catch (FirebaseException ex)
        //    {
        //        return BadRequest();
        //    }

        //    return BadRequest();
        //}


        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate([FromForm] UserLogin model)
        {
            var password = Helpers.Crypt.Sha256(model.Password);
            var user = _userService.GetLoggedUser(model.Email, password);        

            if (!string.IsNullOrEmpty(user.LoginMessage))
                return Ok(null);

            var token = Helpers.TokenService.GenerateToken(user);
            model.Password = string.Empty;

            user.Token = token;
            return Ok(new
            {
               user.Token, user.Name, user.ProfileTypeValue
            });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken(string token)
        {
            var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;

            try
            {
                var response = await auth.VerifyIdTokenAsync(token);
                if (response != null)
                    return Accepted();
            }
            catch (FirebaseException ex)
            {
                return BadRequest();
            }

            return BadRequest();
        }

        [HttpPost("EditUser")]
        [Authorize(Roles = "Manager")]
        public IActionResult EditUser([FromForm] EditUserProfile user)
        {
            _userService.Update(user);

            return Ok(null);
        }
    }
}
