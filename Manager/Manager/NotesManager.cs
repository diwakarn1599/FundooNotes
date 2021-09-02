using Manager.Interface;
using Models.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
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

        public string ChangeColor(int noteId, string color)
        {
            try
            {
                return this.repository.ChangeColor(noteId,color);
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
                return this.repository.DeleteNote(noteId);
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
                return this.repository.DeleteReminder(noteId);
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
                return this.repository.GetArchiveNotes(userId);
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
                return this.repository.GetNotes(userId);
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
                return this.repository.GetReminderNotes(userId);
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
                return this.repository.GetTrashNotes(userId);
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
                return this.repository.MoveToTrash(noteId);
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
                return this.repository.RestoreNote(noteId);
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
                return this.repository.SetRemainder(noteId, reminder);
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
                return this.repository.ToggleArchive(noteId);
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
                return this.repository.TogglePin(noteId);
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
                return this.repository.UpdateNote(noteData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
