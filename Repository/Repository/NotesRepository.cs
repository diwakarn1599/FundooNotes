// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesRepository.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Models.Models;
    using global::Repository.Context;
    using global::Repository.Interface;

    /// <summary>
    /// Notes repository class
    /// </summary>
    /// <seealso cref="Repository.Interface.INotesRepository" />
    public class NotesRepository : INotesRepository
    {
        /// <summary>
        /// The notes context
        /// </summary>
        private readonly UserContext notesContext;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesRepository"/> class.
        /// </summary>
        /// <param name="notesContext">The notes context.</param>
        /// <param name="configuration">The configuration.</param>
        public NotesRepository(UserContext notesContext, IConfiguration configuration)
        {
            this.notesContext = notesContext;
            this.configuration = configuration;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>
        /// boolean of note added or not
        /// </returns>
        /// <exception cref="System.Exception">Throws exception</exception>
        public bool AddNotes(NotesModel noteData)
        {
            try
            {
                if (noteData != null)
                {
                   this.notesContext.Notes.Add(noteData);
                   this.notesContext.SaveChanges();
                   return true;
                }

                return false;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public string ChangeColor(int noteId, string color)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote != null)
                {
                    if (color != null)
                    {
                        verifyNote.Color = color;
                        this.notesContext.SaveChanges();
                        return "Color Updated Successfully";
                    }

                    return "Color Value is Null";
                }

                return "Note Not present";
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public bool DeleteNote(int noteId)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote.Trash == true)
                {
                    this.notesContext.Notes.Remove(verifyNote);
                    this.notesContext.SaveChanges();
                    return true;
                }

                return false;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public List<NotesModel> GetNotes(int userId)
        {
            try
            {
                var getEmail = this.notesContext.Users.Where(x => x.UserId == userId).SingleOrDefault();
                List<NotesModel> getNotes = this.notesContext.Notes.Where(x => x.UserId == userId && (x.Trash == false && x.Archive == false)).ToList();
                List<CollaboratorModel> getCollabNotes = this.notesContext.Collaborators.Where(x => x.CollaboratorEmailId == getEmail.Email).ToList();
                foreach (var data in getCollabNotes)
                {
                    NotesModel getNote = this.notesContext.Notes.Where(x => x.NoteId == data.NoteId && (x.Trash == false && x.Archive == false)).SingleOrDefault();
                    getNotes.Add(getNote);
                }

                return getNotes;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public List<NotesModel> GetReminderNotes(int userId)
        {
            try
            {
                List<NotesModel> getReminderNotes = this.notesContext.Notes.Where(x => x.UserId == userId && (x.Reminder != null && x.Trash == false)).ToList();
                return getReminderNotes;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public List<NotesModel> GetArchiveNotes(int userId)
        {
            try
            {
                List<NotesModel> getArchiveNotes = this.notesContext.Notes.Where(x => x.UserId == userId && (x.Trash == false && x.Archive == true)).ToList();
                return getArchiveNotes;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public bool MoveToTrash(int noteId)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote != null)
                {
                    verifyNote.Trash = true;
                    verifyNote.Pin = false;
                    verifyNote.Reminder = null;
                    this.notesContext.SaveChanges();
                    return true;
                }

                return false;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public bool RestoreNote(int noteId)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote != null)
                {
                    verifyNote.Trash = false;
                    this.notesContext.SaveChanges();
                    return true;
                }

                return false;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public bool SetRemainder(int noteId, string reminder)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote != null)
                {
                    verifyNote.Reminder = reminder;
                    this.notesContext.SaveChanges();
                    return true;
                }

                return false;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public bool DeleteReminder(int noteId)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote != null)
                {
                    verifyNote.Reminder = null;
                    this.notesContext.SaveChanges();
                    return true;
                }

                return false;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public bool ToggleArchive(int noteId)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote != null)
                {
                    verifyNote.Archive = verifyNote.Archive ? false : true;
                    verifyNote.Pin = false;
                    this.notesContext.SaveChanges();
                    return true;
                }

                return false;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public bool TogglePin(int noteId)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote != null)
                {
                    verifyNote.Pin = verifyNote.Pin ? false : true;
                    verifyNote.Archive = false;
                    this.notesContext.SaveChanges();
                    return true;
                }

                return false;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public bool UpdateNote(NotesModel noteData)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteData.NoteId);
                if (verifyNote != null)
                {
                    verifyNote.Title = noteData.Title;
                    verifyNote.Description = noteData.Description;
                    this.notesContext.SaveChanges();
                    return true;
                }

                return false;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public List<NotesModel> GetTrashNotes(int userId)
        {
            try
            {
                List<NotesModel> getNotes = this.notesContext.Notes.Where(x => x.UserId == userId && x.Trash == true).ToList();
                return getNotes;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public bool EmptyTrash(int userId)
        {
            try
            {
                List<NotesModel> getNotes = this.notesContext.Notes.Where(x => x.UserId == userId && x.Trash == true).ToList();
                if (getNotes.Count > 0)
                {
                    this.notesContext.Notes.RemoveRange(getNotes);
                    this.notesContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="imageProps">The image props.</param>
        /// <returns>
        /// boolean of image added or not
        /// </returns>
        /// <exception cref="System.Exception">Throws exception</exception>
        public bool AddImage(int noteId, IFormFile imageProps)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote != null)
                { 
                    Account account = new Account(this.configuration.GetValue<string>("CloudinaryAccount:CloudName"), this.configuration.GetValue<string>("CloudinaryAccount:ApiKey"),  this.configuration.GetValue<string>("CloudinaryAccount:ApiSecret"));
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(imageProps.FileName, imageProps.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    Uri x = uploadResult.Url;
                    verifyNote.Image = x.ToString();
                    this.notesContext.SaveChanges();
                    return true;
                }

                return false;
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
        /// <exception cref="System.Exception">Throws exception</exception>
        public bool DeleteImage(int noteId)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote != null)
                {
                    verifyNote.Image = null;
                    this.notesContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
