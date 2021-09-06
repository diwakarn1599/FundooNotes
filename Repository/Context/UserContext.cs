// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserContext.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------
namespace Repository.Context
{
    using FundooNotes.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Models;

    /// <summary>
    /// User context class
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class UserContext:DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public UserContext(DbContextOptions options) :base(options)
        {

        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public DbSet<RegisterModel> Users { get; set; }

        public DbSet<NotesModel> Notes { get; set; }

        public DbSet<CollaboratorModel> Collaborators { get; set; }

        public DbSet<LabelModel> Labels { get; set; }
    }
}
