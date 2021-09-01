// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesController.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controller
{
    using FundooNotes.Managers.Interface;
    using Manager.Interface;
    using Microsoft.AspNetCore.Mvc;
    using global::Models.Models;
    using System;

    public class NotesController : ControllerBase
    {
        /// <summary>
        /// manager object
        /// </summary>
        private readonly INotesManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class
        /// </summary>
        /// <param name="manager">initializes object</param>
        public NotesController(INotesManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/addNotes")]
        public IActionResult AddNotes([FromBody] NotesModel noteData)
        {
            try
            {
                bool result = this.manager.AddNotes(noteData);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Notes Added Successfully" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/deleteNotes")]
        public IActionResult DeleteNote(int noteId)
        {
            try
            {
                bool result = this.manager.DeleteNote(noteId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note Deleted" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note not present" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
