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
                if (verifyNote != null)
                {
                    verifyNote.Trash = true;
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

        public bool ToggleArchive(int noteId)
        {
            try
            {
                var verifyNote = this.notesContext.Notes.Find(noteId);
                if (verifyNote != null)
                {
                    verifyNote.Archive = verifyNote.Archive ? false : true;
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
