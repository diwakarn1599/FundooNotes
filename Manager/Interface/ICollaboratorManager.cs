// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorManager.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Interface
{
    using System.Collections.Generic;
    using Models.Models;

    /// <summary>
    /// interface of collaborator
    /// </summary>
    public interface ICollaboratorManager
    {
        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns>string of success or not</returns>
        string AddCollaborator(CollaboratorModel collaborator);

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="collaboratorId">The collaborator identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>boolean value of removed or not</returns>
        bool RemoveCollaborator(int collaboratorId, int noteId);

        /// <summary>
        /// Gets the collaborators.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>list of collaborators</returns>
        List<CollaboratorModel> GetCollaborators(int noteId);
    }
}
