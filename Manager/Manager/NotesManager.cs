// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesManager.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Manager
{
    using System;
    using System.Collections.Generic;
    using global::Manager.Interface;
    using Microsoft.AspNetCore.Http;
    using Models.Models;
    using Repository.Interface;

    /// <summary>
    /// notes manager class
    /// </summary>
    /// <seealso cref="Manager.Interface.INotesManager" />
    public class NotesManager : INotesManager
    {
        /// <summary>
        /// reference for notes repository project
        /// </summary>
        private readonly INotesRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesManager" /> class
        /// </summary>
        /// <param name="repository">initializes object</param>
        public NotesManager(INotesRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="imageProps">The image props.</param>
        /// <returns>
        /// boolean of image added or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool AddImage(int noteId, IFormFile imageProps)
        {
            try
            {
                return this.repository.AddImage(noteId, imageProps);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>
        /// boolean of note added or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool AddNotes(NotesModel noteData)
        {
            try
            {
                return this.repository.AddNotes(noteData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>
        /// string of color changed or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public string ChangeColor(int noteId, string color)
        {
            try
            {
                return this.repository.ChangeColor(noteId, color);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// boolean of image deleted or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool DeleteImage(int noteId)
        {
            try
            {
                return this.repository.DeleteImage(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// boolean of note deleted or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool DeleteNote(int noteId)
        {
            try
            {
                return this.repository.DeleteNote(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// boolean of note  deleted or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool DeleteReminder(int noteId)
        {
            try
            {
                return this.repository.DeleteReminder(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Empties the trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// boolean of trash emptied or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool EmptyTrash(int userId)
        {
            try
            {
                return this.repository.EmptyTrash(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the archive notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// list of user archive notes
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public List<NotesModel> GetArchiveNotes(int userId)
        {
            try
            {
                return this.repository.GetArchiveNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// list of user notes
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public List<NotesModel> GetNotes(int userId)
        {
            try
            {
                return this.repository.GetNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the reminder notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// list of user reminder notes
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public List<NotesModel> GetReminderNotes(int userId)
        {
            try
            {
                return this.repository.GetReminderNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the trash notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// list of user trash notes
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public List<NotesModel> GetTrashNotes(int userId)
        {
            try
            {
                return this.repository.GetTrashNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Moves to trash.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// boolean of note trash updated or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool MoveToTrash(int noteId)
        {
            try
            {
                return this.repository.MoveToTrash(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Restores the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// boolean of note restored or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool RestoreNote(int noteId)
        {
            try
            {
                return this.repository.RestoreNote(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Sets the remainder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns>
        /// boolean of reminder set or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool SetRemainder(int noteId, string reminder)
        {
            try
            {
                return this.repository.SetRemainder(noteId, reminder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Toggles the archive.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// boolean of note archive updated or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool ToggleArchive(int noteId)
        {
            try
            {
                return this.repository.ToggleArchive(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Toggles the pin.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// boolean of note pin updated or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool TogglePin(int noteId)
        {
            try
            {
                return this.repository.TogglePin(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>
        /// boolean of note  updated or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool UpdateNote(NotesModel noteData)
        {
            try
            {
                return this.repository.UpdateNote(noteData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
