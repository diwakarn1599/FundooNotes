// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorModel.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace Models.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// collaborator model class
    /// </summary>
    public class CollaboratorModel
    {
        /// <summary>
        /// Gets or sets the collaborator identifier.
        /// </summary>
        /// <value>
        /// The collaborator identifier.
        /// </value>
        [Key]
        public int CollaboratorId { get; set; }

        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        [ForeignKey("NotesModel")]
        public int NoteId { get; set; }

        /// <summary>
        /// Gets or sets the notes model.
        /// </summary>
        /// <value>
        /// The notes model.
        /// </value>
        public virtual NotesModel NotesModel { get; set; }

        /// <summary>
        /// Gets or sets the owner email identifier.
        /// </summary>
        /// <value>
        /// The owner email identifier.
        /// </value>
        public string OwnerEmailId { get; set; }

        /// <summary>
        /// Gets or sets the collaborator email identifier.
        /// </summary>
        /// <value>
        /// The collaborator email identifier.
        /// </value>
        public string CollaboratorEmailId { get; set; }
    }
}
