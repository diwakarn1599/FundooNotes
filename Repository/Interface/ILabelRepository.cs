using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface ILabelRepository
    {
        bool AddLabeltoNote(LabelModel labelData);
    }
}
