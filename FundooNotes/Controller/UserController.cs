// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controller
{
    using System;
    using FundooNotes.Managers.Interface;
    using FundooNotes.Models;
    using Microsoft.AspNetCore.Mvc;
    using global::Models.Models;

    /// <summary>
    /// User controller class
    /// </summary>
    public class UserController : ControllerBase
    {
        /// <summary>
        /// manager object
        /// </summary>
        private readonly IUserManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class
        /// </summary>
        /// <param name="manager">initializes object</param>
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// register data of the user
        /// </summary>
        /// <param name="userData">user data for register</param>
        /// <returns>status of the calls a response model object</returns>
        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromBody]RegisterModel userData)
        {
            try
            {
                string result = this.manager.Register(userData);
                if (result.Equals("Registration Successfull"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Login route
        /// </summary>
        /// <param name="userData">user data for login</param>
        /// <returns>status of the calls a response model object</returns>
        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] CredentialModel userData)
        {
            try
            {
                string result = this.manager.Login(userData);
                if (result.Equals("Login Success"))
                {
                    string jwtToken = this.manager.GenrateJwtToken(userData.Email);
                    return this.Ok(new { Status = true, Message = result ,Data = result,userData.Email, jwtToken });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Method to forget password
        /// </summary>
        /// <param name="email">email parameter to send reset email</param>
        /// <returns>status of the calls a response model object</returns>
        [HttpPost]
        [Route("api/forgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                bool result = this.manager.ForgotPassword(email);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Please Check the mail" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Error!!!Email id incorrect" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/resetPassword")]
        public IActionResult ResetPassword([FromBody] CredentialModel userData)
        {
            try
            {
                bool result = this.manager.ResetPassword(userData);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Password has been reseted" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Error!!!Try after some time" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        // public IActionResult Index()
        // {
        //    return View();
        //  }
    }
}
