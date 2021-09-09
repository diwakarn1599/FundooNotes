// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorRepository.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Models;
    using global::Repository.Context;
    using global::Repository.Interface;

    /// <summary>
    /// Collaborator repository class
    /// </summary>
    /// <seealso cref="Repository.Interface.ICollaboratorRepository" />
    public class CollaboratorRepository : ICollaboratorRepository
    {
        /// <summary>
        /// The collaborator context
        /// </summary>
        private readonly UserContext collaboratorContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorRepository"/> class.
        /// </summary>
        /// <param name="collaboratorContext">The collaborator context.</param>
        public CollaboratorRepository(UserContext collaboratorContext)
        {
            this.collaboratorContext = collaboratorContext;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns>
        /// string of success or not
        /// </returns>
        /// <exception cref="System.Exception">Throws Exception</exception>
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

        /// <summary>
        /// Gets the collaborators.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// list of collaborators
        /// </returns>
        /// <exception cref="System.Exception">Throws Exception</exception>
        public List<CollaboratorModel> GetCollaborators(int noteId)
        {
            try
            {
                List<CollaboratorModel> getCollaborators = this.collaboratorContext.Collaborators.Where(x => x.NoteId == noteId).ToList();
                return getCollaborators;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="collaboratorId">The collaborator identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// boolean value of removed or not
        /// </returns>
        /// <exception cref="System.Exception">Throws Exception</exception>
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
