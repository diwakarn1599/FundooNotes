using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Models;
using Repository.Context;
using Repository.Interface;

namespace Repository.Repository
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly UserContext collaboratorContext;
        private readonly UserContext notesContext;
        private readonly UserContext userContext;

        public CollaboratorRepository(UserContext collaboratorContext)
        {
            this.collaboratorContext = collaboratorContext;
        }

        public string AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                var checkEmail = this.collaboratorContext.Users.Where(x => x.Email.Equals(collaborator.CollaboratorEmailId)).FirstOrDefault();
                var checkNote = this.collaboratorContext.Notes.Where(x => x.NoteId == collaborator.NoteId).FirstOrDefault();
                if (collaborator != null)
                {
                    if(checkNote!=null)
                    {
                        if(checkEmail!=null)
                        {
                            this.collaboratorContext.Collaborators.Add(collaborator);
                            this.collaboratorContext.SaveChanges();
                            return "Collaborator succcessfully added";
                        }
                        return "Email id not registered";
                    }
                    return "Note not present";
                }
                return "Unsuccessfull";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
