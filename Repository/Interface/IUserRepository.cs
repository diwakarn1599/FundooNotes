﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------
using Models.Models;

namespace FundooNotes.Repository.Interface
{
    using FundooNotes.Models;

    /// <summary>
    /// Interface for user repository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Registers the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>returns the boolean value</returns>
        string Register(RegisterModel userData);

        /// <summary>
        /// Logins the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>returns the boolean value</returns>
        string Login(CredentialModel userData);

        /// <summary>
        /// Forgot password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>returns the boolean value</returns>
        bool ForgotPassword(string email);

        bool ResetPassword(CredentialModel userData);
    }
}
