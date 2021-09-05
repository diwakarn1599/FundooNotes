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
    }
}
