// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelController.cs" company="TVSnxt">
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
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// Label Controller class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// The manager
        /// </summary>
        private readonly ILabelManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Adds the label to note.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>label added or not</returns>
        [HttpPost]
        [Route("api/addLabeltoNote")]
        public IActionResult AddLabeltoNote([FromBody] LabelModel labelData)
        {
            try
            {
                bool result = this.manager.AddLabeltoNote(labelData);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Label added to the note" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Label already exists or note is in trash" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Adds the label to user.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>label added to user or not</returns>
        [HttpPost]
        [Route("api/addLabelToUser")]
        public IActionResult AddLabeltoUser([FromBody] LabelModel labelData)
        {
            try
            {
                bool result = this.manager.AddLabeltoUser(labelData);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Label added to the user" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Label already exists" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the label from user.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>label deleted or not</returns>
        [HttpDelete]
        [Route("api/deleteLabelFromUser")]
        public IActionResult DeleteLabelFromUser([FromBody] LabelModel labelData)
        {
            try
            {
                bool result = this.manager.DeleteLabelFromUser(labelData);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Label deleted from the user" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Label not exists" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the label from note.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>label removed from note or not</returns>
        [HttpDelete]
        [Route("api/deleteLabelFromNote")]
        public IActionResult DeleteLabelFromNote(int labelId)
        {
            try
            {
                bool result = this.manager.DeleteLabelFromNote(labelId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Label removed from the note" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Label not exists" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edits the name of the label.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>label edited or not</returns>
        [HttpPut]
        [Route("api/editLabelName")]
        public IActionResult EditLabelName([FromBody] LabelModel labelData)
        {
            try
            {
                bool result = this.manager.EditLabelName(labelData);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Label name changed" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Label not exists" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the label of user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list of labels</returns>
        [HttpGet]
        [Route("api/getLabelofUser")]
        public IActionResult GetLabelofUser(int userId)
        {
            try
            {
                List<string> result = this.manager.GetLabelofUser(userId);
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "User Label retreived", labels = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "User not exists" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the label notes.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>list of notes associated with notes</returns>
        [HttpGet]
        [Route("api/getLabelNotes")]
        public IActionResult GetLabelNotes([FromBody] LabelModel labelData)
        {
            try
            {
                List<NotesModel> result = this.manager.GetLabelNotes(labelData);
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "User Label retreived", labelData.LabelName, labels = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "User not exists" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the labels of note.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>list of labels</returns>
        [HttpGet]
        [Route("api/getLabelsOfNote")]
        public IActionResult GetLabelsOfNote(int noteId)
        {
            try
            {
                List<LabelModel> result = this.manager.GetLabelsOfNote(noteId);
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "Labels retreived", labels = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "note not exists" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
