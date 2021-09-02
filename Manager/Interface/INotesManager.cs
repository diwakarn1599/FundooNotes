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
    }
}
