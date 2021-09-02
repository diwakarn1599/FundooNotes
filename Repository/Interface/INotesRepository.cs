using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface INotesRepository
    {
        bool AddNotes(NotesModel noteData);
        bool DeleteNote(int noteId);
        string ChangeColor(int noteId, string color);
        bool TogglePin(int noteId);
        bool ToggleArchive(int noteId);
        bool RestoreNote(int noteId);
        bool MoveToTrash(int noteId);
        bool UpdateNote(NotesModel noteData);
        List<NotesModel> GetNotes(int userId);
        bool SetRemainder(int noteId, string reminder);
        bool DeleteReminder(int noteId);
    }
}
