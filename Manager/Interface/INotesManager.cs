// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotesManager.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Interface
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Models.Models;
    
    /// <summary>
    /// interface of notes manager
    /// </summary>
    public interface INotesManager
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>boolean of note added or not</returns>
        bool AddNotes(NotesModel noteData);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>boolean of note deleted or not</returns>
        bool DeleteNote(int noteId);

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>string of color changed or not</returns>
        string ChangeColor(int noteId, string color);

        /// <summary>
        /// Toggles the pin.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>boolean of note pin updated or not</returns>
        bool TogglePin(int noteId);

        /// <summary>
        /// Toggles the archive.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>boolean of note archive updated or not</returns>
        bool ToggleArchive(int noteId);

        /// <summary>
        /// Moves to trash.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>boolean of note trash updated or not</returns>
        bool MoveToTrash(int noteId);

        /// <summary>
        /// Restores the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>boolean of note restored or not</returns>
        bool RestoreNote(int noteId);

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>boolean of note  updated or not</returns>
        bool UpdateNote(NotesModel noteData);

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list of user notes</returns>
        List<NotesModel> GetNotes(int userId);

        /// <summary>
        /// Gets the reminder notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list of user reminder notes</returns>
        List<NotesModel> GetReminderNotes(int userId);

        /// <summary>
        /// Gets the archive notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list of user archive notes</returns>
        List<NotesModel> GetArchiveNotes(int userId);

        /// <summary>
        /// Gets the trash notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list of user trash notes</returns>
        List<NotesModel> GetTrashNotes(int userId);

        /// <summary>
        /// Sets the remainder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns>boolean of reminder set or not</returns>
        bool SetRemainder(int noteId, string reminder);

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>boolean of note  deleted or not</returns>
        bool DeleteReminder(int noteId);

        /// <summary>
        /// Empties the trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>boolean of trash emptied or not</returns>
        bool EmptyTrash(int userId);

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="imageProps">The image props.</param>
        /// <returns>boolean of image added or not</returns>
        bool AddImage(int noteId, IFormFile imageProps);

        /// <summary>
        /// Deletes the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>boolean of image deleted or not</returns>
        bool DeleteImage(int noteId);
    }
}
