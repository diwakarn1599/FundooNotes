// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CredentialModel.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace Models.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Credential model class has email and password properties
    /// </summary>
    public class CredentialModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }
    }
}
