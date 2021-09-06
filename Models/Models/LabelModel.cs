using FundooNotes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Models
{
    public class LabelModel
    {
        [Key]
        public int LabelId { get; set; }
        [Required]
        public string LabelName { get; set; }
        [ForeignKey("NotesModel")]
        public int? NoteId { get; set; }
        public virtual NotesModel NotesModel { get; set; }

        [ForeignKey("RegisterModel")]
        public int? UserId { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }


    }
}
