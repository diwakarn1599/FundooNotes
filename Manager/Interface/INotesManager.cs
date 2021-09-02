using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface INotesManager
    {
        bool AddNotes(NotesModel noteData);
        bool DeleteNote(int noteId);
        string ChangeColor(int noteId, string color);
        bool TogglePin(int noteId);
        bool ToggleArchive(int noteId);
        bool MoveToTrash(int noteId);
        bool RestoreNote(int noteId);
        bool UpdateNote(NotesModel noteData);
        List<NotesModel> GetNotes(int userId);
        List<NotesModel> GetReminderNotes(int userId);
        List<NotesModel> GetArchiveNotes(int userId);
        List<NotesModel> GetTrashNotes(int userId);
        bool SetRemainder(int noteId, string reminder);
        bool DeleteReminder(int noteId);
    }
}
