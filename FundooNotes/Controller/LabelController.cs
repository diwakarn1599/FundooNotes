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
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsuccessfull" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/addLabelToUser")]
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
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "unsuccessfull" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
