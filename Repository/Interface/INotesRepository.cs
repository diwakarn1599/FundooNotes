﻿using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface INotesRepository
    {
        bool AddNotes(NotesModel noteData);
        bool DeleteNote(int noteId);
    }
}