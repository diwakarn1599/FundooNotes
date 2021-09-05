using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
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
        
    }
}
