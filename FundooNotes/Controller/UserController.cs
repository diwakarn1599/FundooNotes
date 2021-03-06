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
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;
    using global::Models.Models;
    using StackExchange.Redis;

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
        /// The logger
        /// </summary>
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="logger">The logger.</param>
        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
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
                string sessionFirstName = string.Empty;
                string sessionEmail = string.Empty;
                this.logger.LogInformation($"{userData.FirstName} registering");
                string result = this.manager.Register(userData);
                if (result.Equals("Registration Successfull"))
                {
                    HttpContext.Session.SetString(sessionFirstName, userData.FirstName);
                    HttpContext.Session.SetString(sessionEmail, userData.Email);

                    this.logger.LogInformation($"{userData.FirstName} registered successfully");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    this.logger.LogInformation($"{userData.FirstName} registeration unsuccessfull");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{userData.FirstName} Exception Occured => {ex.Message}");
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
                    this.logger.LogInformation($"{userData.Email} logged in");
                    string jwtToken = this.manager.GenrateJwtToken(userData.Email);

                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string firstName = database.StringGet("FirstName");
                    string lastName = database.StringGet("LastName");
                    int userId = Convert.ToInt32(database.StringGet("UserID"));

                    RegisterModel usersData = new RegisterModel
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        UserId = userId,
                        Email = userData.Email
                    };
                    return this.Ok(new { Status = true, Message = result, userData.Email, jwtToken, userData = usersData });
                }
                else
                {
                    this.logger.LogInformation($"{userData.Email} login failed");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{userData.Email} Exception Occured => {ex.Message}");
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
                this.logger.LogInformation($"{email} is clicked forgot password");
                bool result = this.manager.ForgotPassword(email);
                if (result)
                {
                    this.logger.LogInformation($"Mail sent to {email} for reseting password");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Please Check the mail" });
                }
                else
                {
                    this.logger.LogInformation($"{email} is not present in database");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Error!!!Email id incorrect" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{email} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>password reset or not</returns>
        [HttpPut]
        [Route("api/resetPassword")]
        public IActionResult ResetPassword([FromBody] CredentialModel userData)
        {
            try
            {
                this.logger.LogInformation($"{userData.Email} reseting password");
                bool result = this.manager.ResetPassword(userData);
                if (result)
                {
                    this.logger.LogInformation($"{userData.Email} Password reseted");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Password has been reseted" });
                }
                else
                {
                    this.logger.LogInformation($"{userData.Email} Password not reseted");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Error!!!Try after some time" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{userData.Email} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        // public IActionResult Index()
        // {
        //    return View();
        //  }
    }
}
