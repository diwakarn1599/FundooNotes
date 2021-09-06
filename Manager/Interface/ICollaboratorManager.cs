using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface ICollaboratorManager
    {
        string AddCollaborator(CollaboratorModel collaborator);
        bool RemoveCollaborator(int collaboratorId, int noteId);
        List<CollaboratorModel> GetCollaborators(int noteId);
    }
}
