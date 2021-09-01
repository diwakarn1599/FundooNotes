// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------
using Models.Models;

namespace FundooNotes.Managers.Interface
{
    using FundooNotes.Models;

    /// <summary>
    /// Interface for user manager
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Registers the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>returns boolean value</returns>
        string Register(RegisterModel userData);

        /// <summary>
        /// Login the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>returns boolean value</returns>
        string Login(CredentialModel userData);

        /// <summary>
        /// Forgot password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>returns boolean value</returns>
        bool ForgotPassword(string email);

        bool ResetPassword(CredentialModel userData);
    }
}
