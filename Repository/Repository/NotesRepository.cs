﻿using Models.Models;
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
                   if (noteData.Color == null)
                        noteData.Color = "white";
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

        public bool DeleteNote(int noteId)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote != null)
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
    }
}