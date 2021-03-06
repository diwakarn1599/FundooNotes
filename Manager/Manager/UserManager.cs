// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooNotes.Managers.Manager
{
    using System;
    using FundooNotes.Managers.Interface;
    using FundooNotes.Models;
    using FundooNotes.Repository.Interface;
    using global::Models.Models;

    /// <summary>
    /// User manager class
    /// </summary>
    public class UserManager : IUserManager
    {
        /// <summary>
        /// reference for user repository project
        /// </summary>
        private readonly IUserRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager" /> class
        /// </summary>
        /// <param name="repository">initializes object</param>
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// method to invoke login from repository
        /// </summary>
        /// <param name="userData">user data object for login</param>
        /// <returns>successfully login or not</returns>
        public string Login(CredentialModel userData)
        {
            try
            {
                return this.repository.Login(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method to invoke register from repository
        /// </summary>
        /// <param name="userData"> user data object for register</param>
        /// <returns> returns successfully registered or not</returns>
        public string Register(RegisterModel userData)
        {
            try
            {
                return this.repository.Register(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method to invoke the forgot password
        /// </summary>
        /// <param name="email">email of the user</param>
        /// <returns>returns email sent or not</returns>
        public bool ForgotPassword(string email)
        {
            try
            {
                return this.repository.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>
        /// boolean value whether reset or not
        /// </returns>
        public bool ResetPassword(CredentialModel userData)
        {
            try
            {
                return this.repository.ResetPassword(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// string of token
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public string GenrateJwtToken(string email)
        {
            try
            {
                return this.repository.GenrateJwtToken(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
