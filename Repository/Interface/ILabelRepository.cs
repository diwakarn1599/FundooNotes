using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface ILabelRepository
    {
        bool AddLabeltoNote(LabelModel labelData);
        bool AddLabeltoUser(LabelModel labelData);
        bool DeleteLabelFromUser(int userId, string labelName);
        bool DeleteLabelFromNote(int labelId);
        bool EditLabelName(int userId, string oldLabelName, string newLabelName);
        List<string> GetLabelofUser(int userId);
        List<NotesModel> GetLabelNotes(int userId,string labelName);
    }
}
