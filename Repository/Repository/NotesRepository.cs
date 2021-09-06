using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Models.Models;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repository
{
    public class NotesRepository : INotesRepository
    {
        /// <summary>
        /// The notes context
        /// </summary>
        private readonly UserContext notesContext;
        private readonly IConfiguration configuration;

        public NotesRepository(UserContext notesContext , IConfiguration configuration)
        {
            this.notesContext = notesContext;
            this.configuration = configuration;
        }
        public bool AddNotes(NotesModel noteData)
        {
            try
            {
                if (noteData!=null)
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

        public bool DeleteNote(int noteId)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote.Trash==true)
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

        public List<NotesModel> GetNotes(int userId)
        {
            try
            {
                var getEmail = this.notesContext.Users.Where(x => x.UserId == userId).SingleOrDefault();
                List<NotesModel> getNotes = this.notesContext.Notes.Where(x => x.UserId == userId && (x.Trash == false && x.Archive==false)).ToList();
                List<CollaboratorModel> getCollabNotes = this.notesContext.Collaborators.Where(x => x.CollaboratorEmailId == getEmail.Email).ToList();
                foreach(var data in getCollabNotes)
                {
                    NotesModel getNote = this.notesContext.Notes.Where(x=>x.NoteId==data.NoteId && (x.Trash == false && x.Archive == false)).SingleOrDefault();
                    getNotes.Add(getNote);
                }
                return getNotes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        public bool MoveToTrash(int noteId)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote!=null)
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

        public bool EmptyTrash(int userId)
        {
            try
            {
                List<NotesModel> getNotes = this.notesContext.Notes.Where(x => x.UserId == userId && x.Trash == true).ToList();
                if(getNotes.Count>0)
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

        public bool AddImage(int noteId, IFormFile imageProps)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if(verifyNote!=null)
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
