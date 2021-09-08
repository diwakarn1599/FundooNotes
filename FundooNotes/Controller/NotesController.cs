// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesController.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controller
{
    using System;
    using Manager.Interface;
    using Microsoft.AspNetCore.Mvc;
    using global::Models.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Http;

    [Authorize]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// manager object
        /// </summary>
        private readonly INotesManager manager;

        private readonly ILogger<NotesController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class
        /// </summary>
        /// <param name="manager">initializes object</param>
        public NotesController(INotesManager manager, ILogger<NotesController> logger)
        {
            this.manager = manager;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/addNotes")]
        public IActionResult AddNotes([FromBody] NotesModel noteData)
        {
            
            try
            {
                _logger.LogInformation($"User = {noteData.UserId} Adding Note");
                bool result = this.manager.AddNotes(noteData);
                if (result)
                {
                    _logger.LogInformation($"User = {noteData.UserId} Added a New Note");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Notes Added Successfully" });
                }
                else
                {
                    _logger.LogInformation($"User ={noteData.UserId} Adding Note is unsuccesfull");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={noteData.UserId} Exception Occured => {ex.Message}");
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
                    _logger.LogInformation($"User = {userId} notes retreived");
                    return this.Ok(new { Status = true, Message = "Notes retreived Successfully",Data=result });
                }
                else
                {
                    _logger.LogInformation($"User = {userId} notes not retreived");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={userId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/emptyTrash")]
        public IActionResult EmptyTrash(int userId)
        {
            try
            {
                _logger.LogInformation($"User = {userId} clearing trash");
                bool result = this.manager.EmptyTrash(userId);
                if (result)
                {
                    _logger.LogInformation($"User = {userId} trash cleared");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Trash Cleared" });
                }
                else
                {
                    _logger.LogInformation($"User = {userId} trash not cleared");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull...No notes in trash" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={userId} Exception Occured => {ex.Message}");
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
                    _logger.LogInformation($"User = {userId} retreived reminder notes");
                    return this.Ok(new { Status = true, Message = "Reminder Notes retreived Successfully", Data = result });
                }
                else
                {
                    _logger.LogInformation($"User = {userId} reminder notes not retreived");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull...No reminder notes" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={userId} Exception Occured => {ex.Message}");
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
                    _logger.LogInformation($"User = {userId} archive notes retreived");
                    return this.Ok(new { Status = true, Message = "Archive Notes retreived Successfully", Data = result });
                }
                else
                {
                    _logger.LogInformation($"User = {userId} no archived notes");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull...No archived notes" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={userId} Exception Occured => {ex.Message}");
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
                    _logger.LogInformation($"User = {userId} trash notes retreived");
                    return this.Ok(new { Status = true, Message = "Trash Notes retreived Successfully", Data = result });
                }
                else
                {
                    _logger.LogInformation($"User = {userId} trash is empty");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull...trash is empty" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={userId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/updateNote")]
        public IActionResult UpdateNote([FromBody] NotesModel noteData)
        {
            try
            {
                _logger.LogInformation($"User = {noteData.UserId} updating note");
                bool result = this.manager.UpdateNote(noteData);
                if (result)
                {
                    _logger.LogInformation($"User = {noteData.UserId} note updated");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Notes updated Successfully" });
                }
                else
                {
                    _logger.LogInformation($"User = {noteData.UserId} updation unsuccessfull");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={noteData.UserId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/deleteNote")]
        public IActionResult DeleteNote(int noteId)
        {
            try
            {
                bool result = this.manager.DeleteNote(noteId);
                if (result)
                {
                    _logger.LogInformation($"Note = {noteId} deleted");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note Deleted Permanently" });
                }
                else
                {
                    _logger.LogInformation($"Note = {noteId} not deleted");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note not present in trash" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
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
                    _logger.LogInformation($"Note = {noteId} moved to trash");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note Deleted" });
                }
                else
                {
                    _logger.LogInformation($"Note = {noteId} not present");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note not present" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
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
                    _logger.LogInformation($"Note = {noteId} restored");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note Restored from Trash" });
                }
                else
                {
                    _logger.LogInformation($"Note = {noteId} not present");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note not present" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
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
                    _logger.LogInformation($"Note = {noteId} color updated");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    _logger.LogInformation($"Note = {noteId} color not updated");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
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
                    _logger.LogInformation($"Note = {noteId} assigned reminder");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Reminder set sucessfull" });
                }
                else
                {
                    _logger.LogInformation($"Note = {noteId} reminder not assigned");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsucessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
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
                    _logger.LogInformation($"Note = {noteId} reminder deleted");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Remiander deleted" });
                }
                else
                {
                    _logger.LogInformation($"Note = {noteId} reminder not deleted");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsucessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
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
                    _logger.LogInformation($"Note = {noteId} pin updated");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Pin Updated" });
                }
                else
                {
                    _logger.LogInformation($"Note = {noteId} pin not updated");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note not present" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
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
                    _logger.LogInformation($"Note = {noteId} archive updated");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Archive Updated" });
                }
                else
                {
                    _logger.LogInformation($"Note = {noteId} archive not updated");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note not present" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/addImage")]
        public IActionResult AddImage(int noteId,IFormFile imageProps)
        {
            try
            {
                bool result = this.manager.AddImage(noteId, imageProps);
                if (result)
                {
                    _logger.LogInformation($"Note = {noteId} image Uploaded");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Image Uploaded" });
                }
                else
                {
                    _logger.LogInformation($"Note = {noteId} Image not Uploaded");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Image Not Uploaded" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/deleteImage")]
        public IActionResult DeleteImage(int noteId)
        {
            try
            {
                bool result = this.manager.DeleteImage(noteId);
                if (result)
                {
                    _logger.LogInformation($"Note = {noteId} image deleted");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "image deleted" });
                }
                else
                {
                    _logger.LogInformation($"Note = {noteId} Image not Uploaded");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Image Not deleted" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }


    }
}
