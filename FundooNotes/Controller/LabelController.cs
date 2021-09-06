using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager manager;

        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }

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
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Label already exists" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

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

        [HttpDelete]
        [Route("api/deleteLabelFromUser")]
        public IActionResult DeleteLabelFromUser(int userId,string labelName)
        {
            try
            {
                bool result = this.manager.DeleteLabelFromUser( userId,  labelName);
                if(result)
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
    }
}
