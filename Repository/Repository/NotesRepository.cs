using Models.Models;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class NotesRepository : INotesRepository
    {
        /// <summary>
        /// The notes context
        /// </summary>
        private readonly UserContext notesContext;

        public NotesRepository(UserContext notesContext)
        {
            this.notesContext = notesContext;
        }
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

    }
}
