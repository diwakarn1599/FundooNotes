// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelManager.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Interface
{
    using System.Collections.Generic;
    using Models.Models;

    /// <summary>
    /// interface of label manager
    /// </summary>
    public interface ILabelManager
    {
        /// <summary>
        /// Adds the label to note.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>boolean of label added or not</returns>
        bool AddLabeltoNote(LabelModel labelData);

        /// <summary>
        /// Adds the label to user.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>boolean of label added or not</returns>
        bool AddLabeltoUser(LabelModel labelData);

        /// <summary>
        /// Deletes the label from user.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns></returns>
        bool DeleteLabelFromUser(LabelModel labelData);

        /// <summary>
        /// Deletes the label from note.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>boolean of label deleted or not</returns>
        bool DeleteLabelFromNote(int labelId);

        /// <summary>
        /// Edits the name of the label.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>boolean of label edited or not</returns>
        bool EditLabelName(LabelModel labelData);

        /// <summary>
        /// Gets the label of user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list of user labels</returns>
        List<string> GetLabelofUser(int userId);

        /// <summary>
        /// Gets the label notes.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns></returns>
        List<NotesModel> GetLabelNotes(LabelModel labelData);

        /// <summary>
        /// Gets the labels of note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>list of labels</returns>
        List<LabelModel> GetLabelsOfNote(int noteId);
    }
}
