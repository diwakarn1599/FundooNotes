﻿using System;
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

        public CollaboratorRepository(UserContext collaboratorContext)
        {
            this.collaboratorContext = collaboratorContext;
        }

        public string AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                if (collaborator.OwnerEmailId != collaborator.CollaboratorEmailId)
                {
                    var checkExists = this.collaboratorContext.Collaborators.Where(x => x.CollaboratorEmailId.Equals(collaborator.CollaboratorEmailId) && x.NoteId == collaborator.NoteId).FirstOrDefault();
                    if (checkExists == null)
                    {
                       this.collaboratorContext.Collaborators.Add(collaborator);
                       this.collaboratorContext.SaveChanges();
                       return "Collaborator succcessfully added";
                    }
                    return "Collaborator already exists to this note";
                }
                return "Owner cant be added as collaborator";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CollaboratorModel> GetCollaborators(int noteId)
        {
            try
            {
                List<CollaboratorModel> getCollaborators = this.collaboratorContext.Collaborators.Where(x =>x.NoteId==noteId).ToList();
                return getCollaborators;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveCollaborator(int collaboratorId, int noteId)
        {
            try
            {

                var verifyNote = this.collaboratorContext.Collaborators.Where(x => x.CollaboratorId == collaboratorId && x.NoteId == noteId).SingleOrDefault();
                if (verifyNote != null)
                {

                    this.collaboratorContext.Collaborators.Remove(verifyNote);
                    this.collaboratorContext.SaveChanges();
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