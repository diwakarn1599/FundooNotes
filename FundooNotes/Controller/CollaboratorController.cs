// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorController.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controller
{
    using System;
    using System.Collections.Generic;
    using Manager.Interface;
    using Microsoft.AspNetCore.Mvc;
    using global::Models.Models;

    /// <summary>
    /// Collaborator controller class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// manager object
        /// </summary>
        private readonly ICollaboratorManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public CollaboratorController(ICollaboratorManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns>whether collaborator added or not</returns>
        [HttpPost]
        [Route("api/addCollaborator")]
        public IActionResult AddCollaborator([FromBody] CollaboratorModel collaborator)
        {
            try
            {
                string result = this.manager.AddCollaborator(collaborator);
                if (result.Equals("Collaborator succcessfully added"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                   return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="collaboratorId">The collaborator identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>whether collaborator removed or not</returns>
        [HttpDelete]
        [Route("api/removeCollaborator")]
        public IActionResult RemoveCollaborator(int collaboratorId, int noteId)
        {
            try
            {
                bool result = this.manager.RemoveCollaborator(collaboratorId, noteId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Collaborator Removed" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Collaborator not exists to this note" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the collaborators.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>list of collaborators</returns>
        [HttpGet]
        [Route("api/getCollaborators")]
        public IActionResult GetCollaborators(int noteId)
        {
            try
            {
                var result = this.manager.GetCollaborators(noteId);
                if (result.Count > 0)
                {
                    return this.Ok(new ResponseModel<List<CollaboratorModel>>() { Status = true, Message = "Collaborator retreived", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No Collaborators to this note" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
