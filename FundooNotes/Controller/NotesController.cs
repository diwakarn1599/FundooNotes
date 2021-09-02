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
    using Microsoft.AspNetCore.Authorization;

    
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

        [HttpGet]
        [Route("api/getNotes")]
        public IActionResult GetNotes(int userId)
        {
            try
            {
                var result = this.manager.GetNotes(userId);
                if (result.Count>0)
                {
                    return this.Ok(new { Status = true, Message = "Notes retreived Successfully",Data=result });
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

        [HttpGet]
        [Route("api/getReminder")]
        public IActionResult GetReminderNotes(int userId)
        {
            try
            {
                var result = this.manager.GetReminderNotes(userId);
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "Reminder Notes retreived Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull...No reminder notes" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/getArchive")]
        public IActionResult GetArchiveNotes(int userId)
        {
            try
            {
                var result = this.manager.GetArchiveNotes(userId);
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "Archive Notes retreived Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull...No archive notes" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/getTrash")]
        public IActionResult GetTrashNotes(int userId)
        {
            try
            {
                var result = this.manager.GetTrashNotes(userId);
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "Trash Notes retreived Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull...No Trash notes" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/updateNote")]
        public IActionResult UpdateNote([FromBody] NotesModel noteData)
        {
            try
            {
                bool result = this.manager.UpdateNote(noteData);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Notes updated Successfully" });
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

        [HttpPut]
        [Route("api/deleteNote")]
        public IActionResult DeleteNote(int noteId)
        {
            try
            {
                bool result = this.manager.DeleteNote(noteId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note Deleted Permanently" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note not present or First Move to Trash" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/moveToTrash")]
        public IActionResult MoveToTrash(int noteId)
        {
            try
            {
                bool result = this.manager.MoveToTrash(noteId);
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

        [HttpPut]
        [Route("api/restoreNote")]
        public IActionResult RestoreNote(int noteId)
        {
            try
            {
                bool result = this.manager.RestoreNote(noteId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note Restored from Trash" });
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
        [HttpPut]
        [Route("api/changeColor")]
        public IActionResult ChangeColor(int noteId,string color)
        {
            try
            {
                string result = this.manager.ChangeColor(noteId, color);
                if (result.Equals("Color Updated Successfully"))
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

        [HttpPut]
        [Route("api/setReminder")]
        public IActionResult SetRemainder(int noteId, string reminder)
        {
            try
            {
                bool result = this.manager.SetRemainder(noteId, reminder);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Reminder set sucessfull" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsucessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/deleteReminder")]
        public IActionResult DeleteReminder(int noteId)
        {
            try
            {
                bool result = this.manager.DeleteReminder(noteId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Remiander deleted" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsucessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/togglePin")]
        public IActionResult TogglePin(int noteId)
        {
            try
            {
                bool result = this.manager.TogglePin(noteId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Pin Updated" });
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

        [HttpPut]
        [Route("api/toggleArchive")]
        public IActionResult ToggleArchive(int noteId)
        {
            try
            {
                bool result = this.manager.ToggleArchive(noteId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Archive Updated" });
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
