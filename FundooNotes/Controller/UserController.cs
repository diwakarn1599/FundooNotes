// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

using Models.Models;

namespace FundooNotes.Controller
{
    using System;
    using FundooNotes.Managers.Interface;
    using FundooNotes.Models;
    using Microsoft.AspNetCore.Mvc;
    
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
                bool result = this.manager.Register(userData);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Registration Successfull" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Registration Unsuccessfull" });
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
        public IActionResult Login([FromBody] LoginModel userData)
        {
            try
            {
                string result = this.manager.Login(userData);
                if (result.Equals("Login Success"))
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
        /// Method to forget password
        /// </summary>
        /// <param name="email">email parameter to send reset email</param>
        /// <returns>status of the calls a response model object</returns>
        [HttpGet]
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

        // public IActionResult Index()
        // {
        //    return View();
        //  }
    }
}
