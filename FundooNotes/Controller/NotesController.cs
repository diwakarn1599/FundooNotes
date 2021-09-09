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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using global::Models.Models;

    /// <summary>
    /// Notes Controller class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// manager object
        /// </summary>
        private readonly INotesManager manager;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<NotesController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="logger">The logger.</param>
        public NotesController(INotesManager manager, ILogger<NotesController> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>notes added or not</returns>
        [HttpPost]
        [Route("api/addNotes")]
        public IActionResult AddNotes([FromBody] NotesModel noteData)
        {
            try
            {
                this.logger.LogInformation($"User = {noteData.UserId} Adding Note");
                bool result = this.manager.AddNotes(noteData);
                if (result)
                {
                    this.logger.LogInformation($"User = {noteData.UserId} Added a New Note");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Notes Added Successfully" });
                }
                else
                {
                    this.logger.LogInformation($"User ={noteData.UserId} Adding Note is unsuccesfull");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={noteData.UserId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list of notes</returns>
        [HttpGet]
        [Route("api/getNotes")]
        public IActionResult GetNotes(int userId)
        {
            try
            {
                var result = this.manager.GetNotes(userId);
                if (result.Count > 0)
                {
                    this.logger.LogInformation($"User = {userId} notes retreived");
                    return this.Ok(new { Status = true, Message = "Notes retreived Successfully", Data = result });
                }
                else
                {
                    this.logger.LogInformation($"User = {userId} notes not retreived");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={userId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Empties the trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>trash emptied or not</returns>
        [HttpDelete]
        [Route("api/emptyTrash")]
        public IActionResult EmptyTrash(int userId)
        {
            try
            {
                this.logger.LogInformation($"User = {userId} clearing trash");
                bool result = this.manager.EmptyTrash(userId);
                if (result)
                {
                    this.logger.LogInformation($"User = {userId} trash cleared");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Trash Cleared" });
                }
                else
                {
                    this.logger.LogInformation($"User = {userId} trash not cleared");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull...No notes in trash" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={userId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the reminder notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list of reminder notes</returns>
        [HttpGet]
        [Route("api/getReminder")]
        public IActionResult GetReminderNotes(int userId)
        {
            try
            {
                var result = this.manager.GetReminderNotes(userId);
                if (result.Count > 0)
                {
                    this.logger.LogInformation($"User = {userId} retreived reminder notes");
                    return this.Ok(new { Status = true, Message = "Reminder Notes retreived Successfully", Data = result });
                }
                else
                {
                    this.logger.LogInformation($"User = {userId} reminder notes not retreived");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull...No reminder notes" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={userId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the archive notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list of archive notes</returns>
        [HttpGet]
        [Route("api/getArchive")]
        public IActionResult GetArchiveNotes(int userId)
        {
            try
            {
                var result = this.manager.GetArchiveNotes(userId);
                if (result.Count > 0)
                {
                    this.logger.LogInformation($"User = {userId} archive notes retreived");
                    return this.Ok(new { Status = true, Message = "Archive Notes retreived Successfully", Data = result });
                }
                else
                {
                    this.logger.LogInformation($"User = {userId} no archived notes");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull...No archived notes" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={userId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the trash notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list of trashed notes</returns>
        [HttpGet]
        [Route("api/getTrash")]
        public IActionResult GetTrashNotes(int userId)
        {
            try
            {
                var result = this.manager.GetTrashNotes(userId);
                if (result.Count > 0)
                {
                    this.logger.LogInformation($"User = {userId} trash notes retreived");
                    return this.Ok(new { Status = true, Message = "Trash Notes retreived Successfully", Data = result });
                }
                else
                {
                    this.logger.LogInformation($"User = {userId} trash is empty");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull...trash is empty" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={userId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>notes updated or not</returns>
        [HttpPut]
        [Route("api/updateNote")]
        public IActionResult UpdateNote([FromBody] NotesModel noteData)
        {
            try
            {
                this.logger.LogInformation($"User = {noteData.UserId} updating note");
                bool result = this.manager.UpdateNote(noteData);
                if (result)
                {
                    this.logger.LogInformation($"User = {noteData.UserId} note updated");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Notes updated Successfully" });
                }
                else
                {
                    this.logger.LogInformation($"User = {noteData.UserId} updation unsuccessfull");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={noteData.UserId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>note deleted or not</returns>
        [HttpDelete]
        [Route("api/deleteNote")]
        public IActionResult DeleteNote(int noteId)
        {
            try
            {
                bool result = this.manager.DeleteNote(noteId);
                if (result)
                {
                    this.logger.LogInformation($"Note = {noteId} deleted");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note Deleted Permanently" });
                }
                else
                {
                    this.logger.LogInformation($"Note = {noteId} not deleted");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note not present in trash" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Moves to trash.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>note moved to trash or not</returns>
        [HttpPut]
        [Route("api/moveToTrash")]
        public IActionResult MoveToTrash(int noteId)
        {
            try
            {
                bool result = this.manager.MoveToTrash(noteId);
                if (result)
                {
                    this.logger.LogInformation($"Note = {noteId} moved to trash");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note Deleted" });
                }
                else
                {
                    this.logger.LogInformation($"Note = {noteId} not present");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note not present" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Restores the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>notes restored or not</returns>
        [HttpPut]
        [Route("api/restoreNote")]
        public IActionResult RestoreNote(int noteId)
        {
            try
            {
                bool result = this.manager.RestoreNote(noteId);
                if (result)
                {
                    this.logger.LogInformation($"Note = {noteId} restored");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Note Restored from Trash" });
                }
                else
                {
                    this.logger.LogInformation($"Note = {noteId} not present");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note not present" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>string of color changed or not</returns>
        [HttpPut]
        [Route("api/changeColor")]
        public IActionResult ChangeColor(int noteId, string color)
        {
            try
            {
                string result = this.manager.ChangeColor(noteId, color);
                if (result.Equals("Color Updated Successfully"))
                {
                    this.logger.LogInformation($"Note = {noteId} color updated");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    this.logger.LogInformation($"Note = {noteId} color not updated");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Sets the remainder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns>reminder set or not</returns>
        [HttpPut]
        [Route("api/setReminder")]
        public IActionResult SetRemainder(int noteId, string reminder)
        {
            try
            {
                bool result = this.manager.SetRemainder(noteId, reminder);
                if (result)
                {
                    this.logger.LogInformation($"Note = {noteId} assigned reminder");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Reminder set sucessfull" });
                }
                else
                {
                    this.logger.LogInformation($"Note = {noteId} reminder not assigned");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsucessfull" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>reminder deleted or not</returns>
        [HttpPut]
        [Route("api/deleteReminder")]
        public IActionResult DeleteReminder(int noteId)
        {
            try
            {
                bool result = this.manager.DeleteReminder(noteId);
                if (result)
                {
                    this.logger.LogInformation($"Note = {noteId} reminder deleted");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Remiander deleted" });
                }
                else
                {
                    this.logger.LogInformation($"Note = {noteId} reminder not deleted");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsucessfull" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Toggles the pin.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>pin updated or not</returns>
        [HttpPut]
        [Route("api/togglePin")]
        public IActionResult TogglePin(int noteId)
        {
            try
            {
                bool result = this.manager.TogglePin(noteId);
                if (result)
                {
                    this.logger.LogInformation($"Note = {noteId} pin updated");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Pin Updated" });
                }
                else
                {
                    this.logger.LogInformation($"Note = {noteId} pin not updated");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note not present" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Toggles the archive.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>archive updated or not</returns>
        [HttpPut]
        [Route("api/toggleArchive")]
        public IActionResult ToggleArchive(int noteId)
        {
            try
            {
                bool result = this.manager.ToggleArchive(noteId);
                if (result)
                {
                    this.logger.LogInformation($"Note = {noteId} archive updated");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Archive Updated" });
                }
                else
                {
                    this.logger.LogInformation($"Note = {noteId} archive not updated");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note not present" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="imageProps">The image props.</param>
        /// <returns>image added or not</returns>
        [HttpPut]
        [Route("api/addImage")]
        public IActionResult AddImage(int noteId, IFormFile imageProps)
        {
            try
            {
                bool result = this.manager.AddImage(noteId, imageProps);
                if (result)
                {
                    this.logger.LogInformation($"Note = {noteId} image Uploaded");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Image Uploaded" });
                }
                else
                {
                    this.logger.LogInformation($"Note = {noteId} Image not Uploaded");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Image Not Uploaded" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>image deleted or not</returns>
        [HttpPut]
        [Route("api/deleteImage")]
        public IActionResult DeleteImage(int noteId)
        {
            try
            {
                bool result = this.manager.DeleteImage(noteId);
                if (result)
                {
                    this.logger.LogInformation($"Note = {noteId} image deleted");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "image deleted" });
                }
                else
                {
                    this.logger.LogInformation($"Note = {noteId} Image not Uploaded");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Image Not deleted" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"User ={noteId} Exception Occured => {ex.Message}");
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
