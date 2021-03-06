// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelModel.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace Models.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using FundooNotes.Models;

    /// <summary>
    /// label model class
    /// </summary>
    public class LabelModel
    {
        /// <summary>
        /// Gets or sets the label identifier.
        /// </summary>
        /// <value>
        /// The label identifier.
        /// </value>
        [Key]
        public int LabelId { get; set; }

        /// <summary>
        /// Gets or sets the name of the label.
        /// </summary>
        /// <value>
        /// The name of the label.
        /// </value>
        [Required]
        public string LabelName { get; set; }

        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        [ForeignKey("NotesModel")]
        public int? NoteId { get; set; }

        /// <summary>
        /// Gets or sets the notes model.
        /// </summary>
        /// <value>
        /// The notes model.
        /// </value>
        public virtual NotesModel NotesModel { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [ForeignKey("RegisterModel")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the register model.
        /// </summary>
        /// <value>
        /// The register model.
        /// </value>
        public virtual RegisterModel RegisterModel { get; set; }
    }
}
