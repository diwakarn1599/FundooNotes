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
    }
}
