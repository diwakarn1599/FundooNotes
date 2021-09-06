using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// manager object
        /// </summary>
        private readonly ICollaboratorManager manager;

        public CollaboratorController(ICollaboratorManager manager)
        {
            this.manager = manager;
        }

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

        [HttpDelete]
        [Route("api/removeCollaborator")]
        public IActionResult RemoveCollaborator(int collaboratorId,int noteId)
        {
            try
            {
                bool result = this.manager.RemoveCollaborator(collaboratorId,noteId);
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

        [HttpGet]
        [Route("api/getCollaborators")]
        public IActionResult GetCollaborators(int noteId)
        {
            try
            {
                var result = this.manager.GetCollaborators(noteId);
                if (result.Count>0)
                {
                    return this.Ok(new ResponseModel<List<CollaboratorModel>>() { Status = true, Message = "Collaborator retreived",Data=result });
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
