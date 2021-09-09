// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorManager.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Manager
{
    using System;
    using System.Collections.Generic;
    using global::Manager.Interface;
    using Models.Models;
    using Repository.Interface;

    /// <summary>
    /// collaborator manager class
    /// </summary>
    /// <seealso cref="Manager.Interface.ICollaboratorManager" />
    public class CollaboratorManager : ICollaboratorManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly ICollaboratorRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CollaboratorManager(ICollaboratorRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns>
        /// string of success or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public string AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                return this.repository.AddCollaborator(collaborator);
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
        /// <exception cref="System.Exception">throws exception</exception>
        public List<CollaboratorModel> GetCollaborators(int noteId)
        {
            try
            {
                return this.repository.GetCollaborators(noteId);
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
        /// <exception cref="System.Exception">throws exception</exception>
        public bool RemoveCollaborator(int collaboratorId, int noteId)
        {
            try
            {
                return this.repository.RemoveCollaborator(collaboratorId, noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
